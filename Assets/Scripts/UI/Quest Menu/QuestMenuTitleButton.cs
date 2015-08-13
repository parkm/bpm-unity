using UnityEngine;
using System.Collections;

public class QuestMenuTitleButton : MonoBehaviour {
    public Quest quest;

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
        QuestManager.Instance.CurrentQuest = quest;
        Application.LoadLevel(quest.asset.scene.name);
    }
}
