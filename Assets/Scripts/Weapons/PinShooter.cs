using UnityEngine;
using System.Collections;

public class PinShooter : Weapon {
    public Projectile pin;
    public float baseMagnitude = 200;

    float pinSpeedMod;

    void Start() {
        this.pinSpeedMod = UpgradeManager.Instance.abilityMan.GetAbilityValue("pinSpeed");
    }

    public override void Shoot(Vector3 position, Quaternion rotation, float zAngle) {
        Projectile pin = Instantiate(this.pin);
        pin.transform.position = position;
        pin.transform.rotation = rotation;

        float mag = this.baseMagnitude * (this.pinSpeedMod+1);
        pin.Launch(mag, zAngle);
    }

}
