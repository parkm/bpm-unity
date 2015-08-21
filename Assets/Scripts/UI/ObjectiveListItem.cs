using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveListItem : MonoBehaviour {
    public Text descriptionText;
    public GameObject completedImage;

    QuestObjective objective;

    public void SetObjective(QuestObjective objective) {
        this.objective = objective;
        this.descriptionText.text = objective.GetDescription();
        this.objective.OnUpdate += OnQuestUpdate;
        this.objective.OnComplete += OnQuestComplete;
    }

    void OnQuestUpdate(QuestObjective objective) {
        this.descriptionText.text = objective.GetDescription();
    }

    void OnQuestComplete(QuestObjective objective) {
        completedImage.SetActive(true);
    }

    void OnDestroy() {
        if (this.objective == null) return;
        this.objective.OnUpdate -= OnQuestUpdate;
        this.objective.OnComplete -= OnQuestComplete;
    }
}
