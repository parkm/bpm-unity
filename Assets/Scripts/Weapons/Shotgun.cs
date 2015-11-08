using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {
    public Projectile shotgunBullet;
    public float baseMagnitude = 200;
    public float angularRange = 90;
    public int bulletCount = 3;

    public override void Shoot(Vector3 position, Quaternion rotation, float zAngle) {
        // The angular distance between each bullet
        float bulletSpread = this.angularRange / this.bulletCount;
        // The angle of the center bullet
        float centerBulletOffset = (this.bulletCount / 2) * bulletSpread;

        for (int i=0; i<this.bulletCount; ++i) {
            Projectile bullet = Instantiate(this.shotgunBullet);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;

            float localAngle = (i * bulletSpread) - centerBulletOffset;
            bullet.Launch(this.baseMagnitude, (localAngle * Mathf.Deg2Rad) + zAngle);
        }
    }

}
