using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
    public static QuestManager Instance { get; private set; }

    public QuestAreaAsset[] areas;

    private Dictionary<string, QuestAsset> questIdDict = new Dictionary<string, QuestAsset>();

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

        // Have quests keep track of what other quests are needed to unlock the quest.
        foreach (QuestAsset quest in this.GetQuests()) {
            if (quest.unlocks.Length > 0) {
                foreach (QuestAsset unlockQuest in quest.unlocks) {
                    if (unlockQuest == null) continue;
                    unlockQuest.requiredToUnlock.Add(quest.id);
                }
            }
        }
    }

    public QuestAsset GetQuestFromId(string questId) {
        return this.questIdDict[questId];
    }

    public Dictionary<string, QuestAsset>.ValueCollection GetQuests() {
        return this.questIdDict.Values;
    }

}
