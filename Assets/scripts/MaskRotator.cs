using UnityEngine;
using System.Collections;

public class MaskRotator : MonoBehaviour { 
	[Tooltip("Stops this child object from rotating with the mask parent.")]
	public Transform child;

	[Tooltip("The rotation the mask will be at when the ratio is 0.0")]
	public float minRotation;

	[Tooltip("The rotation the mask will be at when the ratio is 1.0")]
	public float maxRotation;

	[Range(0, 1)]
	public float startingRatio = 0;

	void Start () {
		this.setMaskRotation (startingRatio);
	}

	// Rotates the mask based on the ratio. 0.0 = minRotation, 1.0 = maxRotation
	void setMaskRotation(float ratio) {
		Vector3 origin = child.transform.position;
		child.transform.SetParent (null);

		float rotation = ((maxRotation - minRotation) * ratio) + minRotation;
		this.transform.rotation = Quaternion.Euler (0,0, rotation);

		child.transform.SetParent(this.transform);
		child.transform.position = origin;
	}
}
