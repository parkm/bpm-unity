using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject onQuestCompleteUi;

    private QuestAsset quest;
    private QuestManager questMan;

    void Start() {
        questMan = QuestManager.Instance;
        quest = questMan.CurrentQuest;

        Bubble.OnPop += OnBubblePop;
        questMan.OnQuestComplete += OnQuestComplete;
    }

    void OnDestroy() {
        Bubble.OnPop -= OnBubblePop;
        questMan.OnQuestComplete -= OnQuestComplete;
    }

    void OnBubblePop(Bubble bubble) {
        if (quest.HasObjective(QuestObjective.Types.PopBubbles)) {
            quest.GetObjective(QuestObjective.Types.PopBubbles)[0].Update();
        }
    }

    void OnQuestComplete(QuestAsset quest) {
        Debug.Log("quest complete!");
        this.onQuestCompleteUi.SetActive(true);

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
