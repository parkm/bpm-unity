using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoStatusBar : MonoBehaviour {

    public ArrowShooter arrowShooter;
    public BoundRotator boundRotator;

    void Awake() {
        arrowShooter.ammoRegenTimerUpdate += updateRegenTimerGui;
        arrowShooter.ammoChange += updateAmmoText;
    }

    void OnDestroy() {
        arrowShooter.ammoRegenTimerUpdate -= updateRegenTimerGui;
        arrowShooter.ammoChange -= updateAmmoText;
    }

    void updateRegenTimerGui(float timer, float time) {
        Debug.Log(timer / time);
        boundRotator.setRotation(1 - (timer / time));
    }

    void updateAmmoText(int ammo) {
        Text ammoText = this.transform.Find("ammoText").GetComponent<Text>();
        ammoText.text = ammo.ToString();
    }
}
