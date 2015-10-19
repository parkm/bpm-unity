using UnityEngine;
using System.Collections.Generic;
using System;

public class Quest {
    public QuestAsset asset;
    public QuestAreaAsset area;

    // Quests that are required to be completed before this quest unlocks.
    public List<Quest> requiredToUnlock = new List<Quest>();

    public List<QuestObjective> objectives = new List<QuestObjective>();

    public bool completed = false;
    public bool standardObjectivesCompleted = false;
    public bool started = false; // Has the quest been started?

    public event Action<Quest> OnQuestStart = delegate(Quest quest) {};

    public Quest(QuestAsset asset) {
        this.asset = asset;
    }

    // Starts the quest by allowing the objectives to attach events.
    public void Start(LevelManager levelMan) {
        if (this.started) {
            Debug.LogError("Quest attempted a restart without ending.");
            return;
        }
        this.started = true;
        foreach (QuestObjective objective in this.objectives) {
            objective.OnQuestStart(levelMan);
        }
        this.OnQuestStart(this);
    }

    // Ends the quest which will remove events from the objectives.
    public void End(LevelManager levelMan) {
        if (!this.started) {
            Debug.LogError("Quest cannot be ended until it has been started.");
            return;
        }
        this.started = false;
        foreach (QuestObjective objective in this.objectives) {
            objective.OnQuestEnd(levelMan);
        }
    }

    // Create quest objectives from asset objective data.
    public void CreateObjectives() {
        foreach (QuestAsset.ObjectiveData objectiveData in this.asset.objectiveData) {
            QuestObjective objective = QuestObjective.CreateObjective(objectiveData);
            objective.OnComplete += QuestManager.Instance.OnQuestObjectiveComplete;
            objective.OnFail += QuestManager.Instance.OnQuestObjectiveFail;
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

    // Returns if non-endurance quest objectives have been completed.
    public bool StandardObjectivesCompleted() {
        if (this.standardObjectivesCompleted) return true;
        foreach (QuestObjective objective in this.objectives) {
            if (objective.endurance) continue;
            if (objective.completed == false) {
                return false;
            }
        }
        this.standardObjectivesCompleted = true;
        return true;
    }
}
