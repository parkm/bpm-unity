﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestMenuManager : MonoBehaviour {

    public Transform questsPanel;
    public Transform questButton;
    public QuestMenuAreaButton titleButton;

    // Debug
    [Tooltip("Used for other scenes that you want to access.")]
    public Object[] otherScenes;

    private QuestManager questMan;

    // Use this for initialization
    void Start () {
        questMan = QuestManager.Instance;

        foreach (QuestAreaAsset area in questMan.GetAvailableAreas()) {
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.GetComponent<QuestMenuAreaButton>().quests = questMan.GetAvailableQuests(area).ToArray();
            titleButton.Find("Text").GetComponent<Text>().text = area.areaName;
            titleButton.SetParent(questsPanel);
        }

        // Debug
        if (this.otherScenes.Length > 0) {
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.Find("Text").GetComponent<Text>().text = "Other Scenes";
            titleButton.GetComponent<QuestMenuAreaButton>().otherScenes = this.otherScenes;
            titleButton.SetParent(questsPanel);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
