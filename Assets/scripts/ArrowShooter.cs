using UnityEngine;
using System.Collections;

public class ArrowShooter : MonoBehaviour {
	public Camera cam;
	public GameObject pin;
	public int ammoMax = 10;
	public float ammoRegenTime = 2;

	private int ammo = 0;
	private float ammoRegenTimer = 0;

	// Use this for initialization
	void Start () {
		this.ammo = this.ammoMax;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		float angle = Mathf.Atan2 (mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (0, 0, angle);

		if (Input.GetMouseButtonDown (0)) {
			onMouseDown();
		}

		if (ammo < ammoMax) {
			this.ammoRegenTimer += Time.deltaTime;
			if (this.ammoRegenTimer >= this.ammoRegenTime) {
				this.ammo++;
				this.ammoRegenTimer = 0;
			}
		}
	}

	void onMouseDown() {
		if (this.ammo <= 0)
			return;

		this.ammo--;

		GameObject pin = Instantiate (this.pin);
		pin.transform.position = this.transform.position;
		pin.transform.rotation = this.transform.rotation;

		Rigidbody2D a = pin.GetComponent<Rigidbody2D> ();
		float angle = this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		float mag = 200;
		a.AddForce (new Vector2(Mathf.Cos(angle)*mag, Mathf.Sin(angle)*mag));
	}
}
