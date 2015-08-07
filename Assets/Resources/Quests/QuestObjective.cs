using UnityEngine;
using System.Collections.Generic;
using System;
using QuestObjectives;

public abstract class QuestObjective {
    public enum Types {PopBubbles, Multiplier, FindSomething};

    public bool completed = false;
    public string status = "";

    public Types type;
    public string goal;

    public Dictionary<string, string> attributes;

    public event Action<QuestObjective> OnComplete = delegate(QuestObjective objective) {
        objective.completed = true;
    };

    public static Dictionary<Types, System.Type> typeToClass = new Dictionary<Types, System.Type>() {
        {Types.PopBubbles, typeof(PopBubbles)},
    };

    public static QuestObjective CreateObjective(QuestAsset.ObjectiveData data) {
        QuestObjective objective = (QuestObjective) System.Activator.CreateInstance(typeToClass[data.objectiveType]);
        objective.SetData(data);
        return objective;
    }

    protected abstract void Init();
    public abstract void Update();

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

        this.Init();
    }

}
