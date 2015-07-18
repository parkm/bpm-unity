using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    public Vector2 maxVelocity = new Vector2(5,5);

    public int damage = 1;

    public float lifeTime = 10;
    float lifeTimer = 0;

    public GameObject pinDestroyed;

    // Use this for initialization
    void Start () {
        rigidBody = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //Invoke ("destroy", 10);
    }

    // Update is called once per frame
    void Update () {
        capVelocity(maxVelocity.x, maxVelocity.y);

        lifeTimer += Time.deltaTime;
        float lifeRatio = lifeTimer / lifeTime;

        // y = sqrt(1 -x) + x/4
        spriteRenderer.color = new Color(1, 1, 1, Mathf.Sqrt(1-lifeRatio) + (lifeRatio)/4);

        if (lifeTimer >= lifeTime) {
            Die();
        }
    }

    public void Die() {
        Instantiate(pinDestroyed, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void capVelocity(float x, float y) {
        if (Mathf.Abs(rigidBody.velocity.x) > Mathf.Abs(x)) {
            if (rigidBody.velocity.x >= 0) {
                rigidBody.velocity = new Vector2(x, rigidBody.velocity.y);
            } else {
                rigidBody.velocity = new Vector2(-x, rigidBody.velocity.y);
            }
        }

        if (Mathf.Abs(rigidBody.velocity.y) > Mathf.Abs(y)) {
            if (rigidBody.velocity.y >= 0) {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, y);
            } else {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Bubble") {
            collider.gameObject.GetComponent<Bubble>().OnPinCollision(this);
        }
    }

    void OnCollisionEnter2D() {
        float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
