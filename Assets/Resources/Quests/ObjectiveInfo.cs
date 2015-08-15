using UnityEngine;
using System.Collections.Generic;
using QuestObjectives;

public static class ObjectiveInfo {
    public enum Types {
        PopBubbles,
        Multiplier,
        FindSomething
    };
    private static Dictionary<Types, System.Type> typeToClass = new Dictionary<Types, System.Type>() {
        {Types.PopBubbles, typeof(PopBubbles)},
    };
    public static System.Type GetClassFromType(Types type) { return typeToClass[type]; }
}
