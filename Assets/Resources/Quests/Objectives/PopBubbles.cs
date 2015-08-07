using UnityEngine;
using System.Collections;

namespace QuestObjectives {
    public class PopBubbles : QuestObjective {
        int goalInt = 0;
        int statusInt = 0;

        protected override void Init() {
            if (this.status.Length <= 0) {
                this.status = "0";
                this.statusInt = 0;
            } else {
                this.statusInt = int.Parse(this.status);
            }
            this.goalInt = int.Parse(this.goal);
        }

        public override void Update() {
            this.statusInt++;
            Debug.Log(this.statusInt);
            if (this.statusInt >= this.goalInt) {
                this.Complete();
            }
        }
    }
}

