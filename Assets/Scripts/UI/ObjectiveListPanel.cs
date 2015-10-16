using UnityEngine;
using System.Collections;

public class ObjectiveListPanel : MonoBehaviour {
    public ObjectiveListItem objectiveListItemPrefab;

    QuestManager questMan;

    void Start() {
        bool view = true;
        if (QuestManager.Instance != null) {
            this.questMan = QuestManager.Instance;
            if (this.questMan.CurrentQuest == null) {
                view = false;
            } else {
                this.questMan.CurrentQuest.OnQuestStart += OnQuestStart;
            }

        } else {
            view = false;
        }
        this.gameObject.SetActive(view);
    }

    void OnDestroy() {
        questMan.CurrentQuest.OnQuestStart -= OnQuestStart;
    }

    void OnQuestStart(Quest quest) {
        foreach (QuestObjective objective in quest.objectives) {
            var listItem = (ObjectiveListItem) Instantiate(objectiveListItemPrefab);
            listItem.SetObjective(objective);
            listItem.transform.SetParent(this.transform);
        }
    }
}
