using UnityEngine;
using System.Collections;

public class ObjectiveListPanel : MonoBehaviour {
    public ObjectiveListItem objectiveListItemPrefab;

    QuestManager questMan;

    void Start() {
        if (QuestManager.Instance != null) {
            questMan = QuestManager.Instance;
            if (questMan.CurrentQuest == null) return;

            foreach (QuestObjective objective in questMan.CurrentQuest.objectives) {
                var listItem = (ObjectiveListItem) Instantiate(objectiveListItemPrefab);
                listItem.SetObjective(objective);
                listItem.transform.SetParent(this.transform);
            }
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
