using UnityEngine;
using System.Collections;

public class ElementController : MonoBehaviour {
    public Transform fire;
    public Transform ice;
    public Transform lightning;

    void Start() {
        CreateFire();
    }

    private void CreateFire() {
        fire.GetComponent<FireElement>().parentBubble = transform.parent.gameObject.GetComponent<Bubble>();
        Instantiate(fire, transform.position, transform.rotation);
    }
}
