using UnityEngine;
using System.Collections;

public class AmmoStatusBar : MonoBehaviour {

    public ArrowShooter arrowShooter;
    public BoundRotator boundRotator;

    void Awake() {
        arrowShooter.ammoRegenTimerUpdate += updateRegenTimerGui;
    }

    void OnDestroy() {
        arrowShooter.ammoRegenTimerUpdate -= updateRegenTimerGui;
    }

    void updateRegenTimerGui(float timer, float time) {
        Debug.Log(timer / time);
        boundRotator.setRotation(1 - (timer / time));
    }
}
