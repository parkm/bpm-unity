using UnityEngine;
using UnityEditor;

public class QuestAssetCreator {
    [MenuItem("Assets/Create/Quest")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<QuestAsset>();
    }
}
