using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {
    public GameObject onQuestCompleteUi;

    public event Action OnQuestTimerComplete = delegate() {};

    private Quest quest;
    private QuestManager questMan;

    float questTimer = 0;
    float questTime;

    void Start() {
        if (QuestManager.Instance != null) {
            questMan = QuestManager.Instance;
            quest = questMan.CurrentQuest;

            if (this.quest != null) {
                quest.Start(this);
                this.questTime = quest.asset.time;
            }
            questMan.OnQuestComplete += OnQuestComplete;
            questMan.OnQuestFail += OnQuestFail;
        }
    }

    void Update() {
        if (this.questTime > 0) {
            this.questTimer += Time.deltaTime;
            if (this.questTimer >= this.questTime) {
                OnQuestTimerComplete();
                if (!this.quest.IsComplete()) {
                    this.OnQuestFail(this.quest);
                }
                this.questTime = 0;
            }
        }
    }

    void OnDestroy() {
        if (questMan != null) {
            if (this.quest != null)
                quest.End(this);
            questMan.OnQuestComplete -= OnQuestComplete;
            questMan.OnQuestFail -= OnQuestFail;
        }
    }

    void OnQuestComplete(Quest quest) {
        Debug.Log("quest complete!");
        this.onQuestCompleteUi.SetActive(true);

        questMan.UnlockNewQuests(questMan.CurrentQuest);
    }

    void OnQuestFail(Quest quest) {
        Debug.Log("quest failed");
    }

    public void ExitLevel() {
        if (this.quest == null || this.quest.asset.endingCutscene == null) {
            Application.LoadLevel("questMenu");
        } else {
            CutsceneManager.ShowCutscene(this.quest.asset.endingCutscene, "questMenu");
        }
    }
}
