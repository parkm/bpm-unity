using UnityEngine;
using System.Collections;
using System;

public class Bubble : MonoBehaviour {
    public float descendSpeed = 0.25f;
    public int maxHealth = 1;
    public int health;
    public GameObject bubblePopped;
    public BubbleArmor bubbleArmor;

    public CollectableQuestItem[] drops;

    public static event Action<Bubble> OnPop = delegate(Bubble bubble) {};
    public static event Action<Bubble> OnAttack = delegate(Bubble bubble) {};

    // Use this for initialization
    void Start () {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update () {
        this.transform.position += new Vector3(0, -descendSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "BubbleDestroyer") {
            OnAttack(this);
            Destroy(this.gameObject);
        }
    }

    public void Damage(int amt) {
        health -= amt;
        bubbleArmor.Damage(amt);
    }

    public void OnProjectileCollision(Projectile projectile) {
        Damage(projectile.damage);
        if (health < 1) {
            OnDeathFromProjectile();
        } else {
            projectile.Die();
        }
    }

    public void OnDeathFromProjectile() {
        foreach (CollectableQuestItem drop in drops) {
            if (UnityEngine.Random.value < drop.spawnChance) {
                Instantiate(drop, this.transform.position, Quaternion.identity);
            }
        }

        OnPop(this);
        Instantiate(bubblePopped, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
