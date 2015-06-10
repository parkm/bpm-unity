using UnityEngine;
using System.Collections;

public class OneShotParticleSystem : MonoBehaviour {
    public ParticleSystem particleSystem;
    void Update () {
        if (!this.particleSystem.IsAlive()) {
            Destroy(this.gameObject);
        }
    }
}
