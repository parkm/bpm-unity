using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
    public static QuestManager Instance { get; private set; }

    public QuestAreaAsset[] areas;
    public QuestAsset startingQuest;
    public bool unlockAllQuests = false;

    public event Action<Quest> OnQuestComplete = delegate(Quest quest) {};
    public event Action<Quest> OnQuestFail = delegate(Quest quest) {};

    public event Action OnStandardObjectivesCompleted = delegate() {};

    private HashSet<Quest> availableQuests = new HashSet<Quest>();

    private Dictionary<QuestAsset, Quest> questAssetToQuest = new Dictionary<QuestAsset, Quest>();

    public Quest CurrentQuest { get; set; }

    void Awake() {
        if (QuestManager.Instance == null) {
            QuestManager.Instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        // Add quests from areas to the dictionary.
        foreach (QuestAreaAsset area in this.areas) {
            foreach (QuestAsset questAsset in area.quests) {
                Quest quest = new Quest(questAsset);
                quest.area = area;
                this.questAssetToQuest.Add(questAsset, quest);
            }
        }

        foreach (Quest quest in this.GetQuests()) {
            // Have quests keep track of what other quests are needed to unlock the quest.
            if (quest.asset.unlocks.Length > 0) {
                foreach (QuestAsset questAsset in quest.asset.unlocks) {
                    if (questAsset == null) continue;
                    Quest unlockQuest = this.GetQuestByAsset(questAsset);
                    unlockQuest.requiredToUnlock.Add(quest);
                }
            }

            quest.CreateObjectives();
        }

        availableQuests.Add(this.GetQuestByAsset(startingQuest));
    }

    public void OnQuestObjectiveComplete(QuestObjective objective) {
        Debug.Log("objective complete");

        // Check if non-endurance objectives have all been completed.
        if (this.CurrentQuest.standardObjectivesCompleted == false) {
            if (this.CurrentQuest.StandardObjectivesCompleted()) {
                OnStandardObjectivesCompleted();
            }
        }

        if (this.CurrentQuest.IsComplete()) {
            OnQuestComplete(this.CurrentQuest);
        }
    }

    public void OnQuestObjectiveFail(QuestObjective objective) {
        Debug.Log("objective failed");
        OnQuestFail(this.CurrentQuest);
    }

    public Quest GetQuestByAsset(QuestAsset asset) {
        return this.questAssetToQuest[asset];
    }

    public Dictionary<QuestAsset, Quest>.ValueCollection GetQuests() {
        return this.questAssetToQuest.Values;
    }

    public List<QuestAreaAsset> GetAvailableAreas() {
        var availableAreas = new List<QuestAreaAsset>();
        foreach (Quest quest in this.availableQuests) {
            if (!availableAreas.Contains(quest.area)) availableAreas.Add(quest.area);
        }
        return availableAreas;
    }

    // Get all available quests.
    public HashSet<Quest> GetAvailableQuests() {
        return availableQuests;
    }
    // Get all available quests for an area.
    public List<Quest> GetAvailableQuests(QuestAreaAsset area) {
        var quests = new List<Quest>();
        foreach (QuestAsset questAsset in area.quests) {
            Quest quest = this.GetQuestByAsset(questAsset);
            if (this.availableQuests.Contains(quest)) quests.Add(quest);
        }
        return quests;
    }

    // Attempts to add new unlocked quests to the availableQuests list.
    // Pass in the recently completed quest.
    public void UnlockNewQuests(Quest completedQuest) {
        availableQuests.Remove(completedQuest);
        foreach (QuestAsset questAsset in completedQuest.asset.unlocks) {
            Quest questToUnlock = this.GetQuestByAsset(questAsset);
            if (this.QuestCanBeUnlocked(questToUnlock)) {
                availableQuests.Add(questToUnlock);
            }
        }
    }

    // Returns if quest can be unlocked.
    public bool QuestCanBeUnlocked(Quest quest) {
        if (quest.requiredToUnlock.Count <= 0) return true;

        foreach (Quest questNeeded in quest.requiredToUnlock) {
            if (!questNeeded.completed) return false;
        }

        return true;
    }

    public void UnlockAllQuests() {
        foreach (Quest quest in this.GetQuests()) {
            availableQuests.Add(quest);
        }
    }
}
