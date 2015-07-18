using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleArmor : MonoBehaviour {
    public Sprite[] armorSprites;
    public int[] healthLimits;
    public Sprite currentArmor;

    private Bubble parentBubble;
    private SpriteRenderer spriteRenderer;

    void Start() {
        parentBubble = this.transform.parent.gameObject.GetComponent<Bubble>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateArmor();
    }

    void Update() {
        UpdateArmor();
    }

    void UpdateArmor() {
        for(int i = 0; i < armorSprites.Length; i++) {
            int healthLimit = healthLimits[i];
            Sprite armorSprite = armorSprites[i];
            if (parentBubble.health < healthLimit) {
                if (parentBubble.health == 1) {
                    // don't render armor if bubble is bare
                    spriteRenderer.sprite = null;
                } else {
                    spriteRenderer.sprite = armorSprite;
                }

                break;
            }
        }
    }
}
