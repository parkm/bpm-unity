using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class QuestObjective {
    public bool completed = false;
    public string status = "";

    public ObjectiveInfo.Types type;
    public string goal;

    public Dictionary<string, string> attributes;

    public event Action<QuestObjective> OnUpdate = delegate(QuestObjective objective) {};
    public event Action<QuestObjective> OnComplete = delegate(QuestObjective objective) {
        objective.completed = true;
    };

    public static QuestObjective CreateObjective(QuestAsset.ObjectiveData data) {
        QuestObjective objective = (QuestObjective) System.Activator.CreateInstance(ObjectiveInfo.GetClassFromType(data.objectiveType));
        objective.SetData(data);
        return objective;
    }

    public abstract void OnQuestStart(LevelManager levelMan);
    public abstract void OnQuestEnd(LevelManager levelMan);
    public abstract string GetDescription();

    public void Update() {
        this.OnUpdate(this);
    }

    protected void Complete() {
        // Events cannot be called on inherited members, so we wrap around it.
        OnComplete(this);
    }

    public void SetData(QuestAsset.ObjectiveData data) {
        this.type = data.objectiveType;
        this.goal = data.goal;

        foreach (QuestAsset.ObjectiveData.Attribute attribute in data.attributes) {
            this.attributes.Add(attribute.key, attribute.value);
        }
    }

}
