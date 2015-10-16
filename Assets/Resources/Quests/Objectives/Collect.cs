using UnityEngine;
using System.Collections;
using System;

namespace QuestObjectives {
    public class Collect : QuestObjective {
        int goalInt = 0;
        int statusInt = 0;

        CollectableQuestItem itemToCollect;

        public override string GetDescription() {
            return String.Format("Collect {0}/{1} {2}", this.statusInt, this.goalInt, this.itemToCollect.itemName);
        }

        public override void OnQuestStart(LevelManager levelMan) {
            if (this.status.Length <= 0) {
                this.status = "0";
                this.statusInt = 0;
            } else {
                this.statusInt = int.Parse(this.status);
            }
            this.goalInt = int.Parse(this.goal);
            this.itemToCollect = ((GameObject) this.relatedObject).GetComponent<CollectableQuestItem>();
            CollectableQuestItem.OnCollect += OnItemCollected;
        }

        public override void OnQuestEnd(LevelManager levelMan) {
            CollectableQuestItem.OnCollect -= OnItemCollected;
        }

        void OnItemCollected(CollectableQuestItem item) {
            if (item.itemName == this.itemToCollect.itemName) {
                this.statusInt++;
                this.Update();
                if (this.statusInt >= this.goalInt) {
                    this.Complete();
                }
            }
        }

    }
}
