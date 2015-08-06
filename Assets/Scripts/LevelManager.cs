using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    void Start () {
        Debug.Log(QuestManager.Instance.CurrentQuest.questName);
    }

    void Update () {

    }
}
