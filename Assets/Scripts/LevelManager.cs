using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject onQuestCompleteUi;

    private Quest quest;
    private QuestManager questMan;

    void Start() {
        if (QuestManager.Instance != null) {
            questMan = QuestManager.Instance;
            quest = questMan.CurrentQuest;

            if (this.quest != null)
                quest.Start(this);
            questMan.OnQuestComplete += OnQuestComplete;
        }
    }

    void OnDestroy() {
        if (questMan != null) {
            if (this.quest != null)
                quest.End(this);
            questMan.OnQuestComplete -= OnQuestComplete;
        }
    }

    void OnQuestComplete(Quest quest) {
        Debug.Log("quest complete!");
        this.onQuestCompleteUi.SetActive(true);

        questMan.UnlockNewQuests(questMan.CurrentQuest);
    }

    public void ExitLevel() {
        if (this.quest == null || this.quest.asset.endingCutscene == null) {
            Application.LoadLevel("questMenu");
        } else {
            CutsceneManager.ShowCutscene(this.quest.asset.endingCutscene, "questMenu");
        }
    }

    void Update() {

    }
}
