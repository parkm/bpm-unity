using UnityEngine;
using System.Collections;

public class OneShotParticleSystem : MonoBehaviour {
    public ParticleSystem system;
    void Update () {
        if (!this.system.IsAlive()) {
            Destroy(this.gameObject);
        }
    }
}
