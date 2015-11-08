using UnityEngine;
using System.Collections;
using System;

public class ArrowShooter : MonoBehaviour {
    public int ammoMax = 10;
    public float ammoRegenTime = 2;

    public event Action<float, float> ammoRegenTimerUpdate = delegate(float timer, float time) {};
    public event Action<int> ammoChange = delegate(int ammo) {};

    public Weapon weapon;

    private int ammo = 0;
    private float ammoRegenTimer = 0;

    // Use this for initialization
    void Start () {
        this.ammo = this.ammoMax;
        ammoChange(this.ammo);
    }

    // Update is called once per frame
    void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0)) {
            onMouseDown();
        }

        if (ammo < ammoMax) {
            this.ammoRegenTimer += Time.deltaTime;
            ammoRegenTimerUpdate(this.ammoRegenTimer, this.ammoRegenTime);
            if (this.ammoRegenTimer >= this.ammoRegenTime) {
                this.ammo++;
                ammoChange(this.ammo);
                this.ammoRegenTimer = 0;
            }
        }
    }

    void onMouseDown() {
        if (this.ammo <= 0)
            return;

        this.ammo--;
        ammoChange(this.ammo);

        float angle = this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        this.weapon.Shoot(this.transform.position, this.transform.rotation, angle);
    }
}
