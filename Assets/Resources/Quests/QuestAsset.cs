using UnityEngine;
using System.Collections.Generic;

public class QuestAsset : ScriptableObject {
    public string id;
    public string questName;
    [TextArea()]
    public string description;
    public int rewardXp;
    public Object scene;

    public QuestAsset[] unlocks = new QuestAsset[1];

    [System.Serializable]
    public class ObjectiveData {
        public ObjectiveInfo.Types objectiveType;
        public string goal;

        [System.NonSerialized]
        public bool completed = false;
        [System.NonSerialized]
        public string status = "";

        [System.Serializable]
        public class Attribute {
            public string key;
            public string value;
        }
        // Used for any additional data needed.
        public Attribute[] attributes;
    };

    public ObjectiveData[] objectiveData;

    [System.NonSerialized]
    public QuestAreaAsset area;

    // Quests that are required to be completed before this quest unlocks.
    [System.NonSerialized]
    public List<QuestAsset> requiredToUnlock = new List<QuestAsset>();

    [System.NonSerialized]
    public List<QuestObjective> objectives = new List<QuestObjective>();

    [System.NonSerialized]
    public bool completed = false;

    public bool HasObjective(ObjectiveInfo.Types type) {
        foreach (QuestObjective objective in this.objectives) {
            if (objective.type == type) {
                return true;
            }
        }
        return false;
    }

    public List<QuestObjective> GetObjective(ObjectiveInfo.Types type) {
        List<QuestObjective> list = new List<QuestObjective>();
        foreach (QuestObjective objective in this.objectives) {
            if (objective.type == type) {
                list.Add(objective);
            }
        }
        return list;
    }

    // Returns if quest is complete.
    public bool IsComplete() {
        if (this.completed) return true;
        foreach (QuestObjective objective in this.objectives) {
            if (objective.completed == false) {
                return false;
            }
        }
        this.completed = true;
        return true;
    }
}
