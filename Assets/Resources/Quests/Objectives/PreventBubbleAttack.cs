using UnityEngine;
using System.Collections;
using System;

namespace QuestObjectives {
    public class PreventBubbleAttack : QuestObjective {
        int tries;

        public PreventBubbleAttack() {
            this.endurance = true;
        }

        public override string GetDescription() {
            return String.Format("Prevent the bubble attacks ({0} tries remain)", this.tries);
        }

        public override void OnQuestStart(LevelManager levelMan) {
            this.tries = int.Parse(this.goal);
            Bubble.OnAttack += OnAttack;
            this.AddEnduranceCompleteEvents();
        }
        public override void OnQuestEnd(LevelManager levelMan) {
            Bubble.OnAttack -= OnAttack;
            this.RemoveEnduranceCompleteEvents();
        }

        void OnAttack(Bubble bubble) {
            this.tries--;
            this.Update();
            if (this.tries <= 0) {
                this.Fail();
            }
        }
    }
}
