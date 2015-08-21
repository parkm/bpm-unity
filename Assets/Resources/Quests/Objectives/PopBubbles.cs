using UnityEngine;
using System.Collections;
using System;

namespace QuestObjectives {
    public class PopBubbles : QuestObjective {
        int goalInt = 0;
        int statusInt = 0;

        public override string GetDescription() {
            return String.Format("Pop {0}/{1} bubbles", this.statusInt, this.goalInt);
        }

        public override void OnQuestStart(LevelManager levelMan) {
            if (this.status.Length <= 0) {
                this.status = "0";
                this.statusInt = 0;
            } else {
                this.statusInt = int.Parse(this.status);
            }
            this.goalInt = int.Parse(this.goal);
            Bubble.OnPop += OnBubblePop;
        }

        public override void OnQuestEnd(LevelManager levelMan) {
            Bubble.OnPop -= OnBubblePop;
        }

        void OnBubblePop(Bubble bubble) {
            this.statusInt++;
            Debug.Log(this.statusInt);
            this.Update();
            if (this.statusInt >= this.goalInt) {
                this.Complete();
            }
        }

    }
}

