using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestManTest : MonoBehaviour {

    public Transform questsPanel;
    public QuestAreaAsset[] questAreas;

    public Transform questButton;
    public TitleButtonTest titleButton;

    // Debug
    [Tooltip("Used for other scenes that you want to access.")]
    public Object[] otherScenes;

    // Use this for initialization
    void Start () {
        foreach (QuestAreaAsset a in this.questAreas) {
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.GetComponent<TitleButtonTest>().quests = a.quests;
            titleButton.Find("Text").GetComponent<Text>().text = a.areaName;
            titleButton.SetParent(questsPanel);
        }

        // Debug
        if (this.otherScenes.Length > 0) {
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.Find("Text").GetComponent<Text>().text = "Other Scenes";
            titleButton.GetComponent<TitleButtonTest>().otherScenes = this.otherScenes;
            titleButton.SetParent(questsPanel);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
