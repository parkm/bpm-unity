using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {



    void Awake() {
        if (Application.loadedLevelName == "main") {
            //Application.LoadLevel("SpinnyLevel");
        }
    }

    void Update() {
        int toSwitch = (int) Input.GetAxisRaw("SceneSwitcher");
        if (toSwitch != 0) {
            Application.LoadLevel(Application.loadedLevel + toSwitch);
        }

        if (Input.GetKey(KeyCode.Escape)) {
            Application.LoadLevel("questMenu");
        }
    }

}
