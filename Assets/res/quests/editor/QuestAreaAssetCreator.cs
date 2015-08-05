using UnityEngine;
using UnityEditor;

public class QuestAreaAssetCreator {
    [MenuItem("Assets/Create/Quest Area")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<QuestAreaAsset>();
    }
}
