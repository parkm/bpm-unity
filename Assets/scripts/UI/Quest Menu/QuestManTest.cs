using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestManTest : MonoBehaviour {

    public Transform questsPanel;
    public QuestAreaAsset[] questAreas;

    public Transform questButton;
    public TitleButtonTest titleButton;

    public class Area {
        public string[] quests;
        public string name;
        public Area(string areaName, string[] quests) {
            this.name = areaName;
            this.quests = quests;
        }
    }

    // Use this for initialization
    void Start () {
        Area[] areas = new Area[]{
            new Area("Area", new string[]{"Pop Bubs", "Pop Quiz"}),
            new Area("Forest", new string[]{"Pop Forest Bubs", "In the Forest", "Forest Fight"}),
            new Area("Bubble Land", new string[]{"Pop Bubble Land Bubs", "The Land Of Bubs"})
        };

        foreach (Area a in areas) {
            Debug.Log(a.name);
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.GetComponent<TitleButtonTest>().questStrings = a.quests;
            titleButton.Find("Text").GetComponent<Text>().text = a.name;
            titleButton.SetParent(questsPanel);
        }

        foreach (QuestAreaAsset a in this.questAreas) {
            Transform titleButton = Instantiate(this.titleButton.transform);
            titleButton.GetComponent<TitleButtonTest>().quests = a.quests;
            titleButton.Find("Text").GetComponent<Text>().text = a.areaName;
            titleButton.SetParent(questsPanel);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
