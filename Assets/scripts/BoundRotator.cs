using UnityEngine;
using System.Collections;

public class BoundRotator : MonoBehaviour {
    [Tooltip("The rotation the mask will be at when the ratio is 0.0")]
    public float minRotation;

    [Tooltip("The rotation the mask will be at when the ratio is 1.0")]
    public float maxRotation;

    [Range(0, 1)]
    public float startingRatio = 0;

    void Start () {
        this.setRotation (startingRatio);
    }

    // Rotates the transform based on the ratio. 0.0 = minRotation, 1.0 = maxRotation
    public void setRotation(float ratio) {
        float rotation = ((maxRotation - minRotation) * ratio) + minRotation;
        this.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
