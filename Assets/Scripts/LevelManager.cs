using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject onQuestCompleteUi;

    private QuestAsset quest;
    private QuestManager questMan;

    void Start() {
        if (QuestManager.Instance != null) {
            questMan = QuestManager.Instance;
            quest = questMan.CurrentQuest;

            questMan.OnQuestComplete += OnQuestComplete;
        }

        Bubble.OnPop += OnBubblePop;
    }

    void OnDestroy() {
        Bubble.OnPop -= OnBubblePop;

        if (questMan != null) {
            questMan.OnQuestComplete -= OnQuestComplete;
        }
    }

    void OnBubblePop(Bubble bubble) {
        if (quest == null) return;
        if (quest.HasObjective(ObjectiveInfo.Types.PopBubbles)) {
            quest.GetObjective(ObjectiveInfo.Types.PopBubbles)[0].Update();
        }
    }

    void OnQuestComplete(QuestAsset quest) {
        Debug.Log("quest complete!");
        this.onQuestCompleteUi.SetActive(true);

        questMan.UnlockNewQuests(questMan.CurrentQuest);

        //temp
        Button button = this.onQuestCompleteUi.transform.Find("Panel/Button").GetComponent<Button>();
        button.onClick.AddListener(onContinueButtonClick);
    }
    //temp
    void onContinueButtonClick() {
        Application.LoadLevel("questMenu");
    }

    void Update() {

    }
}
