using UnityEngine;
using UnityEditor;

public class CutsceneAssetCreator {
    [MenuItem("Assets/Create/Cutscene")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<CutsceneAsset>();
    }
}
