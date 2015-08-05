using UnityEngine;
using System.Collections;

public class FireElement : MonoBehaviour {
    public Bubble parentBubble;
    public int fireDamage;

    void Update() {
        if (parentBubble == null) {
            Destroy(this.gameObject);
        } else {
            Vector3 parentPos = parentBubble.transform.position;
            transform.position = new Vector3(parentPos.x, parentPos.y, transform.position.z);
            parentBubble.health -= fireDamage;
        }
    }
}
