﻿using UnityEngine;
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
    public class Objective {
        public enum Types {Multiplier, PopBubbles};
        public Types objectiveType;
        public string value;
    };
    public Objective[] objectives;

    [System.NonSerialized]
    public QuestAreaAsset area;

    // Quests that are required to be completed before this quest unlocks.
    [System.NonSerialized]
    public List<string> requiredToUnlock = new List<string>();
}
