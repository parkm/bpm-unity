﻿using UnityEngine;
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

    public bool OnFire { get; private set; }
    float fireTimer = 0;
    float fireTime = 5f;
    float fireDamageTimer = 0;
    float fireDamageInterval = 1f;
    public GameObject bubbleFireEffectPrefab;
    public GameObject chainLightningPrefab;

    // Will the bubble be slowed when any damage is taken?
    bool slowedOnDamage = false;
    float slowedSpeedMod = 1f;

    // Use this for initialization
    void Start () {
        health = maxHealth;
        this.OnFire = false;
        this.slowedOnDamage = UpgradeManager.Instance.abilityMan.GetAbilityBoolValue("slowIce");
    }

    // Update is called once per frame
    void Update () {
        this.transform.position += new Vector3(0, -(this.descendSpeed * this.slowedSpeedMod) * Time.deltaTime);
        if (this.OnFire) {
            this.fireTimer += Time.deltaTime;
            this.fireDamageTimer += Time.deltaTime;
            if (this.fireDamageTimer >= this.fireDamageInterval) {
                Damage(1);
                if (health < 1) this.OnDeathFromProjectile();
                this.fireDamageTimer = 0;
            }
            if (this.fireTimer >= this.fireTime) {
                this.OnFire = false;
            }
        }
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
        if (this.slowedOnDamage) {
            this.slowedSpeedMod = UpgradeManager.Instance.abilityMan.GetAbilityValue("slowIceSpeedMod");
        }
    }

    public void OnLightningAttack() {
        Damage(1);
        if (health < 1) {
            OnDeathFromProjectile();
        }
    }

    public void OnProjectileCollision(Projectile projectile) {
        if (UpgradeManager.Instance.abilityMan.GetAbilityBoolValue("chainLightning"))
            this.CreateLightningChain();
        Damage(projectile.damage);
        if (health < 1) {
            OnDeathFromProjectile();
        } else {
            if (projectile.CanIgnite) this.Ignite();
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

    // Adds fire ability effect to the bubble
    public void Ignite() {
        this.OnFire = true;
        this.fireTimer = 0;
        GameObject fireEffect = (GameObject) Instantiate(this.bubbleFireEffectPrefab);
        fireEffect.transform.position = Vector3.zero;
        fireEffect.transform.SetParent(this.transform, false);
    }

    // Continues a chain if prevChain is provided otherwise it creates a new chain.
    public void CreateLightningChain(ChainLightning prevChain=null) {
        ChainLightning chain = Instantiate(this.chainLightningPrefab).GetComponent<ChainLightning>();
        chain.originBubble = this;
        if (prevChain == null)
            chain.chainCount = 8;
        else
            chain.InheritChain(prevChain);
        chain.transform.position = this.transform.position;
    }
}
