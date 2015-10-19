using UnityEngine;
using System.Collections.Generic;

public class QuestAsset : ScriptableObject {
    public string id;
    public string questName;
    [TextArea()]
    public string description;
    [Tooltip("Quest time limit in seconds.")]
    public float time = 0;
    public int rewardXp;
    public Object scene;
    public CutsceneAsset startingCutscene;
    public CutsceneAsset endingCutscene;

    public QuestAsset[] unlocks = new QuestAsset[1];

    [System.Serializable]
    public class ObjectiveData {
        public ObjectiveInfo.Types objectiveType;
        public string goal;
        public UnityEngine.Object relatedObject;

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
