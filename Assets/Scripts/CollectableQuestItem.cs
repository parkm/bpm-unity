using UnityEngine;
using System.Collections;
using System;

public class CollectableQuestItem : MonoBehaviour {

    Rigidbody2D rigidBody;
    public float torqueAmount = 100;
    public string itemName;

    public static event Action<CollectableQuestItem> OnCollect = delegate(CollectableQuestItem item) {};

    void Start() {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        int mod = 0;
        if (UnityEngine.Random.value < 0.5)
            mod = -1;
        else
            mod = 1;
        this.rigidBody.AddTorque(torqueAmount * mod);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "BubbleDestroyer") {
            OnCollect(this);
            Destroy(this.gameObject);
        }
    }

}
