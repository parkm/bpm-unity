using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleArmor : MonoBehaviour {
    public int[] healthLimits; // minimum health per each new bubble armor image
    public Texture2D armorTexture;

    private Sprite[] armorSprites;
    private Bubble bubble;
    private DamageController armorDamage;
    private SpriteRenderer spriteRenderer;
    private int[] damageIntervals;

    void Start() {
        bubble = this.transform.parent.gameObject.GetComponent<Bubble>();
        armorDamage = this.transform.GetChild(0).gameObject.GetComponent<DamageController>();
        armorSprites = Resources.LoadAll<Sprite>("Graphics/" + armorTexture.name);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetSprite();

        damageIntervals = GetArmorDamageIntervals(armorDamage.damageSprites.Length);
        for (int i = 0; i < damageIntervals.Length; i++) {
            Debug.Log(damageIntervals[i]);
        }
    }

    public void Damage(int amt) {
        /*Debug.Log("Damage: " + armorDamage.damage.ToString());
        // TODO: figure out equation to add damage in even intervals
        for (int i = armorDamage.damage; i < damageIntervals.Length; i++) {
            Debug.Log("i: " + i.ToString() + "  damInt: " + damageIntervals[i].ToString());
            if (bubble.health < damageIntervals[i]) {
                Debug.Log("bubble.health: " + bubble.health.ToString());
                armorDamage.Add(amt);
            }
        }*/
        float ratio = (1 - (float) bubble.health / bubble.maxHealth);
        armorDamage.SetSprite(ratio);
    }

    Sprite GetSprite() {
        Sprite armorSprite = null;
        for(int i = 0; i < healthLimits.Length; i++) {
            int healthLimit = healthLimits[i];
            if (i < armorSprites.Length) {
                armorSprite = armorSprites[i];
            }
            if (bubble.maxHealth <= healthLimit) {
                if (bubble.maxHealth == 1) {
                    // don't render armor if bubble is bare
                    return null;
                } else {
                    break;
                }
            }
        }

        return armorSprite;
    }

    int[] GetArmorDamageIntervals(int numIntervals) {
        int[] intervals = new int[numIntervals];

        for (int i = 0; i < numIntervals; i++) {
            int intervalWidth = Mathf.CeilToInt((float) bubble.maxHealth / numIntervals);
            Debug.Log("intervalWidth: " + intervalWidth.ToString());
            intervals[numIntervals - i - 1] = (int) intervalWidth * (i + 1);
        }

        return intervals;
    }
}
