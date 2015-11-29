using UnityEngine;
using System.Collections.Generic;

public class ChainLightning : MonoBehaviour {
    // The amount of chains that can be created.
    public int chainCount;
    public Bubble originBubble;

    ChainLightning prevChain;

    HashSet<GameObject> visitedBubbles = new HashSet<GameObject>();

    SpriteRenderer spriteRenderer;

    float lifeTimer = 0;
    float lifeTime = 0.5f;

    void Start() {
        if (this.chainCount <= 0) {
            Destroy(this.gameObject);
            return;
        }

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        float leastDistance = Mathf.Infinity;
        GameObject nearestBubbleObj = null;

        this.visitedBubbles.Add(originBubble.gameObject);

        // Find nearest bubble
        foreach (GameObject bubbleObj in GameObject.FindGameObjectsWithTag("Bubble")) {
            if (this.visitedBubbles.Contains(bubbleObj)) continue;
            // Avoids using squared root operation, yet still gives us which is closer.
            float distance = (bubbleObj.transform.position - this.transform.position).sqrMagnitude;
            if (distance < leastDistance) {
                leastDistance = distance;
                nearestBubbleObj = bubbleObj;
            }
        }

        if (nearestBubbleObj == null) {
            Destroy(this.gameObject);
        } else {
            this.chainCount--;
            Bubble nearestBubble = nearestBubbleObj.GetComponent<Bubble>();

            float angle = Mathf.Atan2(this.transform.position.y - nearestBubble.transform.position.y, this.transform.position.x - nearestBubble.transform.position.x);
            this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            this.transform.localScale = new Vector3(Mathf.Sqrt(leastDistance) / this.spriteRenderer.sprite.bounds.size.x, 1, 1);

            // Set lightning position to the midpoint.
            this.transform.position = new Vector3((this.transform.position.x + nearestBubble.transform.position.x)/2, (this.transform.position.y + nearestBubble.transform.position.y)/2);

            nearestBubble.OnLightningAttack();
            nearestBubble.CreateLightningChain(this);
        }
    }

    void Update() {
        this.lifeTimer += Time.deltaTime;
        this.spriteRenderer.color = new Color(1, 1, 1, 1 - (this.lifeTimer/this.lifeTime));
        if (this.lifeTimer >= this.lifeTime) {
            Destroy(this.gameObject);
        }
    }

    public void InheritChain(ChainLightning chain) {
        this.prevChain = chain;
        this.chainCount = chain.chainCount;
        this.visitedBubbles = chain.visitedBubbles;
    }

}
