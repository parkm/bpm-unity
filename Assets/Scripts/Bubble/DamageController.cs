using UnityEngine;
using System.Collections;

/* Keeps track of total/current damage and assigns
 * an incremental sprite from a sprite sheet to each point of damage */

// TODO: Implement Add(int amt) - amt not working
public class DamageController : MonoBehaviour {
    public Texture2D damageTexture;
    public Sprite[] damageSprites;
    public int damage;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageSprites = Resources.LoadAll<Sprite>("Graphics/Bubbles/" + damageTexture.name);
        damage = 0;
    }

    public void SetSprite(float ratio) {
        int index = Mathf.CeilToInt(ratio * (damageSprites.Length-1));
        spriteRenderer.sprite = (Sprite) damageSprites[index];
    }

    // true if damage successful
    // false if too much damage (armor breaks)
    public bool Add(int amt) {
        if (damage < damageSprites.Length) {
            spriteRenderer.sprite = (Sprite) damageSprites[damage++];
            Debug.Log("Set damage to " + damage.ToString());
            return true;
        } else {
            spriteRenderer.sprite = null;
            return false;
        }
    }

    // TODO: fix or remove
    // not sure if this will ever be used, but why not
    /*
    bool Remove() {
        if (damage == 0) {
            --damage;
            spriteRenderer.sprite = null;
            return true;
        } else if (damage > 0) {
            spriteRenderer.sprite = damageSprites[--damage];
            return true;
        } else {
            // undamaged
            return false;
        }
    }
    */
}
