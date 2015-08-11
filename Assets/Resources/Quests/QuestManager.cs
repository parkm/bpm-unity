using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
    public static QuestManager Instance { get; private set; }

    public QuestAreaAsset[] areas;

    public event Action<QuestAsset> OnQuestComplete = delegate(QuestAsset quest) {};

    private Dictionary<string, QuestAsset> questIdDict = new Dictionary<string, QuestAsset>();
    private List<QuestAsset> availableQuests = new List<QuestAsset>();

    public QuestAsset CurrentQuest { get; set; }

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
            foreach (QuestAsset quest in area.quests) {
                quest.area = area;
                this.questIdDict.Add(quest.id, quest);
            }
        }

        foreach (QuestAsset quest in this.GetQuests()) {
            // Have quests keep track of what other quests are needed to unlock the quest.
            if (quest.unlocks.Length > 0) {
                foreach (QuestAsset unlockQuest in quest.unlocks) {
                    if (unlockQuest == null) continue;
                    unlockQuest.requiredToUnlock.Add(quest);
                }
            }

            // Create quest objectives from objective data.
            foreach (QuestAsset.ObjectiveData objectiveData in quest.objectiveData) {
                QuestObjective objective = QuestObjective.CreateObjective(objectiveData);
                objective.OnComplete += OnQuestObjectiveComplete;
                quest.objectives.Add(objective);
            }
        }

        availableQuests.Add(this.GetQuestFromId("town01"));
    }

    void OnQuestObjectiveComplete(QuestObjective objective) {
        Debug.Log("objective complete");
        if (this.CurrentQuest.IsComplete()) {
            OnQuestComplete(this.CurrentQuest);
        }
    }

    public QuestAsset GetQuestFromId(string questId) {
        return this.questIdDict[questId];
    }

    public Dictionary<string, QuestAsset>.ValueCollection GetQuests() {
        return this.questIdDict.Values;
    }

    public List<QuestAreaAsset> GetAvailableAreas() {
        var availableAreas = new List<QuestAreaAsset>();
        foreach (QuestAsset quest in this.availableQuests) {
            if (!availableAreas.Contains(quest.area)) availableAreas.Add(quest.area);
        }
        return availableAreas;
    }

    // Get all available quests.
    public List<QuestAsset> GetAvailableQuests() {
        return availableQuests;
    }
    // Get all available quests for an area.
    public List<QuestAsset> GetAvailableQuests(QuestAreaAsset area) {
        var quests = new List<QuestAsset>();
        foreach (QuestAsset quest in area.quests) {
            if (this.availableQuests.Contains(quest)) quests.Add(quest);
        }
        return quests;
    }

    // Attempts to add new unlocked quests to the availableQuests list.
    // Pass in the recently completed quest.
    public void UnlockNewQuests(QuestAsset completedQuest) {
        availableQuests.Remove(completedQuest);
        foreach (QuestAsset questToUnlock in completedQuest.unlocks) {
            if (this.QuestCanBeUnlocked(questToUnlock)) {
                availableQuests.Add(questToUnlock);
            }
        }
    }

    // Returns if quest can be unlocked.
    public bool QuestCanBeUnlocked(QuestAsset quest) {
        if (quest.requiredToUnlock.Count <= 0) return true;

        foreach (QuestAsset questNeeded in quest.requiredToUnlock) {
            if (!questNeeded.completed) return false;
        }

        return true;
    }

}
