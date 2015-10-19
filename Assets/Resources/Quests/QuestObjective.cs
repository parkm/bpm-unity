using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class QuestObjective {
    // In order to complete an endurance objective you must endure the entire quest time, or until all other objectives are complete.
    public bool endurance = false;

    public bool completed = false;
    public bool failed = false;
    public string status = "";

    public ObjectiveInfo.Types type;
    public string goal;
    public UnityEngine.Object relatedObject;

    public Dictionary<string, string> attributes;

    public event Action<QuestObjective> OnUpdate = delegate(QuestObjective objective) {};
    public event Action<QuestObjective> OnComplete = delegate(QuestObjective objective) {};
    public event Action<QuestObjective> OnFail = delegate(QuestObjective objective) {};

    public static QuestObjective CreateObjective(QuestAsset.ObjectiveData data) {
        QuestObjective objective = (QuestObjective) System.Activator.CreateInstance(ObjectiveInfo.GetClassFromType(data.objectiveType));
        objective.SetData(data);
        return objective;
    }

    public abstract void OnQuestStart(LevelManager levelMan);
    public abstract void OnQuestEnd(LevelManager levelMan);
    public abstract string GetDescription();

    // Adds the events that complete endurance objectives.
    public void AddEnduranceCompleteEvents() {
        QuestManager.Instance.OnStandardObjectivesCompleted += OnEnduranceComplete;
        //TODO: Add an event listener for OnQuestTimeComplete which should probably be in LevelManager when implemented
    }

    // Removes the events that complete endurance objectives.
    public void RemoveEnduranceCompleteEvents() {
        QuestManager.Instance.OnStandardObjectivesCompleted -= OnEnduranceComplete;
    }

    public void Update() {
        this.OnUpdate(this);
    }

    protected void Complete() {
        if (!this.failed) {
            this.completed = true;
            OnComplete(this);
        }
    }

    protected void Fail() {
        if (!this.completed) {
            this.failed = true;
            OnFail(this);
        }
    }

    public void SetData(QuestAsset.ObjectiveData data) {
        this.type = data.objectiveType;
        this.goal = data.goal;
        this.relatedObject = data.relatedObject;

        foreach (QuestAsset.ObjectiveData.Attribute attribute in data.attributes) {
            this.attributes.Add(attribute.key, attribute.value);
        }
    }

    void OnEnduranceComplete() {
        this.Complete();
    }
}
