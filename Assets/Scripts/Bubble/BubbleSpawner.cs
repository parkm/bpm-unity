using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour {

    public Bubble bubble;
    public float spawnTime = 1;
    public int minHealth = 1;
    public int maxHealth = 20;

    [System.Serializable]
    public class Wave {
        public GameObject objectToSpawn;
        public float spawnAtTime;
        public int amountToSpawn;

        [System.NonSerialized]
        public bool spawned = false;
    }
    public Wave[] waves;

    public float waveTimer = 0;

    MeshRenderer mesh;

    void Start () {
        this.mesh = this.GetComponent<MeshRenderer>();
        this.waveTimer = 0;

        Invoke("Spawn", spawnTime);
    }

    void Update () {
        this.waveTimer += Time.deltaTime;
        foreach (Wave wave in this.waves) {
            if (wave.spawned) continue;
            if (this.waveTimer >= wave.spawnAtTime) {
                this.SpawnWave(wave);
                wave.spawned = true;
            }
        }
    }

    void Spawn() {
        Bubble bubble = Instantiate(this.bubble);
        Vector3 min = mesh.bounds.min;
        Vector3 max = mesh.bounds.max;
        bubble.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        bubble.maxHealth = Random.Range(minHealth, maxHealth);
        Invoke("Spawn", spawnTime);
    }

    void SpawnWave(Wave wave) {
        Vector3 min = mesh.bounds.min;
        Vector3 max = mesh.bounds.max;
        for (int i=0; i<wave.amountToSpawn; ++i) {
            GameObject spawn = (GameObject) Instantiate(wave.objectToSpawn);
            spawn.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}
