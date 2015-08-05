using UnityEngine;
using System.Collections;

public class QuestAsset : ScriptableObject {
    public string id;
    public string questName;
    [TextArea()]
    public string description;
    public int rewardXp;
	public Object scene;

    [System.Serializable]
    public class Detail {
        public enum DetailNames {Multiplier, PopBubbles};
        public DetailNames detailName;
        public string value;
    };
    public Detail[] details;
}
