using UnityEngine;
using System.Collections.Generic;

public class Quest {
    public QuestAsset asset;
    public QuestAreaAsset area;

    // Quests that are required to be completed before this quest unlocks.
    public List<Quest> requiredToUnlock = new List<Quest>();

    public List<QuestObjective> objectives = new List<QuestObjective>();

    public bool completed = false;

    public Quest(QuestAsset asset) {
        this.asset = asset;
    }

    // Create quest objectives from asset objective data.
    public void CreateObjectives() {
        foreach (QuestAsset.ObjectiveData objectiveData in this.asset.objectiveData) {
            QuestObjective objective = QuestObjective.CreateObjective(objectiveData);
            objective.OnComplete += QuestManager.Instance.OnQuestObjectiveComplete;
            this.objectives.Add(objective);
        }
    }

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
