using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour {

    public GameObject bubble;
    public float spawnTime = 1;

    MeshRenderer mesh;

    // Use this for initialization
    void Start () {
        mesh = this.GetComponent<MeshRenderer>();
        Debug.Log(this.transform.position);
        Debug.Log(mesh.bounds.min);
        Debug.Log(mesh.bounds.max);

        Invoke("spawn", spawnTime);
    }

    // Update is called once per frame
    void Update () {

    }

    void spawn() {
        GameObject bubble = Instantiate(this.bubble);
        Vector3 min = mesh.bounds.min;
        Vector3 max = mesh.bounds.max;
        bubble.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        //Debug.Log (bubble.transform.position);
        Invoke("spawn", spawnTime);
    }
}
