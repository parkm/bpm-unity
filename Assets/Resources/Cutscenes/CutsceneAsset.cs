using UnityEngine;
using System.Collections;

public class CutsceneAsset : ScriptableObject {
    [System.Serializable]
    public class Scene {
        public string speaker;
        [TextArea()]
        public string dialog;
        public string[] addCharacters;
        public string[] removeCharacters;
    }
    public Scene[] scenes;
}
