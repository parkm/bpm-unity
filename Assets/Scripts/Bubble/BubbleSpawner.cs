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

    [System.Serializable]
    public class RepeatedWave {
        public GameObject objectToSpawn;
        public float spawnTime;
        [System.NonSerialized]
        public float spawnTimer = 0;
    }
    public RepeatedWave[] repeatedWaves;

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
                this.Spawn(wave.objectToSpawn, wave.amountToSpawn);
                wave.spawned = true;
            }
        }
        foreach (RepeatedWave wave in this.repeatedWaves) {
            wave.spawnTimer += Time.deltaTime;
            if (wave.spawnTimer >= wave.spawnTime) {
                this.Spawn(wave.objectToSpawn);
                wave.spawnTimer = 0;
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

    void Spawn(GameObject toSpawn, int amount=1) {
        Vector3 min = mesh.bounds.min;
        Vector3 max = mesh.bounds.max;
        for (int i=0; i<amount; ++i) {
            GameObject spawn = (GameObject) Instantiate(toSpawn);
            spawn.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}
