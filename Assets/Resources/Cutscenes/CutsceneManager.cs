using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CutsceneManager : MonoBehaviour {
    // Shows this cutscene unless it is null.
    private static CutsceneAsset cutsceneToShow;
    // Goes to this scene once the cutscene has finished unless it is null.
    private static string cutsceneFinishDest;
    public static void ShowCutscene(CutsceneAsset cutscene, string sceneDest) {
        cutsceneToShow = cutscene;
        cutsceneFinishDest = sceneDest;
        Application.LoadLevel("Cutscene");
    }
    public static void ShowCutscene(CutsceneAsset cutscene, Object sceneDest) {
        CutsceneManager.ShowCutscene(cutscene, sceneDest.name);
    }

    public CutsceneAsset cutscene;
    public Text speakerText;
    public Text dialogText;
    public GameObject characterPanel;

    [System.Serializable]
    public class CharacterMap {
        public string characterId;
        public Sprite characterSprite;
    }
    public CharacterMap[] characters;

    int sceneIndex = 0;
    Dictionary<string, CharacterMap> characterById = new Dictionary<string, CharacterMap>();
    Dictionary<string, GameObject> addedCharacterById = new Dictionary<string, GameObject>();

    void Start() {
        if (CutsceneManager.cutsceneToShow != null) {
            this.cutscene = CutsceneManager.cutsceneToShow;
            CutsceneManager.cutsceneToShow = null;
        }

        foreach (CharacterMap cm in this.characters) {
            characterById.Add(cm.characterId, cm);
        }
        this.UpdateSceneUi();
    }

    public void GotoNextScene() {
        this.sceneIndex++;
        if (this.sceneIndex >= this.cutscene.scenes.Length) {
            this.EndCutscene();
        } else {
            this.UpdateSceneUi();
        }
    }

    public void EndCutscene() {
        if (CutsceneManager.cutsceneFinishDest == null) {
            Destroy(this.gameObject);
            return;
        } else {
            Application.LoadLevel(CutsceneManager.cutsceneFinishDest);
            CutsceneManager.cutsceneFinishDest = null;
        }
    }

    void UpdateSceneUi() {
        var scene = this.GetCurrentScene();
        this.speakerText.text = scene.speaker;
        this.dialogText.text = scene.dialog;

        // Add specified characters
        foreach (string charId in scene.addCharacters) {
            GameObject charObject = new GameObject();
            Image charImage = charObject.AddComponent<Image>();
            charImage.sprite = this.characterById[charId].characterSprite;
            charObject.transform.SetParent(this.characterPanel.transform);
            this.addedCharacterById.Add(charId, charObject);
        }

        // Remove specified characters
        foreach (string charId in scene.removeCharacters) {
            GameObject charObject = this.addedCharacterById[charId];
            Destroy(charObject);
        }
    }

    CutsceneAsset.Scene GetCurrentScene() {
        return cutscene.scenes[this.sceneIndex];
    }
}
