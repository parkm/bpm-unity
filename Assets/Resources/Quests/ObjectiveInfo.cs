using UnityEngine;
using System.Collections.Generic;
using QuestObjectives;

public static class ObjectiveInfo {
    public enum Types {
        PopBubbles,
        Multiplier,
        Collect
    };
    private static Dictionary<Types, System.Type> typeToClass = new Dictionary<Types, System.Type>() {
        {Types.PopBubbles, typeof(PopBubbles)},
        {Types.Collect, typeof(Collect)}
    };
    public static System.Type GetClassFromType(Types type) { return typeToClass[type]; }
}
