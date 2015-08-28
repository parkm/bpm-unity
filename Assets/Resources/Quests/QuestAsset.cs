using UnityEngine;
using System.Collections.Generic;

public class QuestAsset : ScriptableObject {
    public string id;
    public string questName;
    [TextArea()]
    public string description;
    public int rewardXp;
    public Object scene;
    public CutsceneAsset startingCutscene;

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
}
