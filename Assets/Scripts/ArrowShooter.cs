using UnityEngine;
using System.Collections;
using System;

public class ArrowShooter : MonoBehaviour {
    public GameObject pin;
    public int ammoMax = 10;
    public float ammoRegenTime = 2;

    public event Action<float, float> ammoRegenTimerUpdate = delegate(float timer, float time) {};
    public event Action<int> ammoChange = delegate(int ammo) {};

    private int ammo = 0;
    private float ammoRegenTimer = 0;

    float pinSpeedMod;

    // Use this for initialization
    void Start () {
        this.ammo = this.ammoMax;
        ammoChange(this.ammo);
        this.pinSpeedMod = UpgradeManager.Instance.abilityMan.GetAbilityValue("pinSpeed");
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

        Pin pin = Instantiate(this.pin).GetComponent<Pin>();
        pin.transform.position = this.transform.position;
        pin.transform.rotation = this.transform.rotation;

        float angle = this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float mag = 200 * (this.pinSpeedMod+1);
        pin.Launch(mag, angle);
    }
}
