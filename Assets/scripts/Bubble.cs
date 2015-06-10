﻿using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {
	public float descendSpeed = 0.25f;
	public GameObject bubblePopped;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3 (0, -descendSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "BubbleDestroyer") {
			Destroy(this.gameObject);
		}
	}

	public void onDeathFromPin() {
		Instantiate (bubblePopped, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
