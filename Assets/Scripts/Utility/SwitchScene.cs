using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {
    public Object scene;

    void Start() {
        Application.LoadLevel(scene.name);
    }

}
