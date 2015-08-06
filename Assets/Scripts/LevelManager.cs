using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    void Start () {
        Debug.Log(QuestManager.Instance.CurrentQuest.questName);
        Bubble.OnPop += OnBubblePop;
    }

    void OnBubblePop(Bubble bubble) {
        Debug.Log("POP");
    }

    void Update () {

    }
}
