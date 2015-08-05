using UnityEngine;
using System.Collections;

public class QuestButtonTest : MonoBehaviour {
    public QuestAsset quest;

    // Debug
    public Object scene;

    public void OnClick() {
        // Debug
        if (scene != null) {
            Application.LoadLevel(scene.name);
            return;
        }

        if (quest == null) {
            Debug.LogError("No quest associated with this button.");
            return;
        }
        Application.LoadLevel(quest.scene.name);
    }
}
