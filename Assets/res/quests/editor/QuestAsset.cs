using UnityEngine;
using UnityEditor;

public class QuestAsset {
    [MenuItem("Assets/Create/Quest")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Quest>();
    }
}
