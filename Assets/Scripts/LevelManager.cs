using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
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
        Debug.Log("POP");
        if (quest.HasObjective(QuestObjective.Types.PopBubbles)) {
            quest.GetObjective(QuestObjective.Types.PopBubbles)[0].Update();
        }
    }

    void OnQuestComplete(QuestAsset quest) {
        Debug.Log("quest complete!");
    }

    void Update() {

    }
}
