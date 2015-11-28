using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
    public float maxVelocity = 5;
    // Max velocity for each axis
    Vector2 maxDirectionVelocity;

    public int damage = 1;

    public float lifeTime = 10;
    float lifeTimer = 0;

    public GameObject destroyEffectPrefab;

    public bool CanIgnite { get; private set; }

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.CanIgnite = UpgradeManager.Instance.abilityMan.GetAbilityBoolValue("fire");
    }

    // Update is called once per frame
    void Update () {
        lifeTimer += Time.deltaTime;
        float lifeRatio = lifeTimer / lifeTime;

        // y = sqrt(1 -x) + x/4
        spriteRenderer.color = new Color(1, 1, 1, Mathf.Sqrt(1-lifeRatio) + (lifeRatio)/4);

        if (lifeTimer >= lifeTime) {
            Die();
        }
    }

    void FixedUpdate() {
        CapVelocity(this.maxDirectionVelocity.x, this.maxDirectionVelocity.y);
    }

    public void Die() {
        Instantiate(this.destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void CapVelocity(float x, float y) {
        Vector2 vel = rigidBody.velocity;

        if (rigidBody.velocity.x > x)
            vel.x = x;
        else if (rigidBody.velocity.x < -x)
            vel.x = -x;

        if (rigidBody.velocity.y > y)
            vel.y = y;
        else if (rigidBody.velocity.y < -y)
            vel.y = -y;

        rigidBody.velocity = vel;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Bubble") {
            collider.gameObject.GetComponent<Bubble>().OnProjectileCollision(this);
        }
    }

    // Updates the rotation of the projectile to match the direction that the rigid body is moving towards.
    void UpdateAngle() {
        float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void OnCollisionEnter2D() { this.UpdateAngle(); }
    void OnCollisionExit2D()  { this.UpdateAngle(); }

    // Launches the projectile at the angle and force specified
    public void Launch(float force, float angle) {
        float xDir = Mathf.Cos(angle);
        float yDir = Mathf.Sin(angle);
        this.maxDirectionVelocity = new Vector2(Mathf.Abs(xDir * this.maxVelocity), Mathf.Abs(yDir * this.maxVelocity));
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * force, yDir * force));
    }


}
