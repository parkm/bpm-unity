using UnityEngine;
using System.Collections;

public class QuestButtonTest : MonoBehaviour {
    public QuestAsset quest;
    public void OnClick() {
        if (quest == null) {
            Debug.LogError("No quest associated with this button.");
            return;
        }
        Application.LoadLevel(quest.scene.name);
    }
}
