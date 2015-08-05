using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleButtonTest : MonoBehaviour {
    public Transform questDropDownPrefab;
    public Transform questButton;

    public QuestAsset[] quests;

    private Transform questDropDown;

    // Debug
    public Object[] otherScenes;

    public void OnClick() {
        if  (this.questDropDown == null) {
            Transform dropDown = Instantiate(this.questDropDownPrefab);

            dropDown.SetParent(this.transform.parent);
            dropDown.SetSiblingIndex(this.transform.GetSiblingIndex()+1);
            this.questDropDown = dropDown;

            foreach (QuestAsset quest in this.quests) {
                Transform questButton = Instantiate(this.questButton);
                questButton.Find("Text").GetComponent<Text>().text = quest.questName;
                questButton.GetComponent<QuestButtonTest>().quest = quest;
                questButton.SetParent(dropDown);
            }

            // Debug
            foreach (Object scene in this.otherScenes) {
                Transform questButton = Instantiate(this.questButton);
                questButton.Find("Text").GetComponent<Text>().text = scene.name;
                questButton.GetComponent<QuestButtonTest>().scene = scene;
                questButton.SetParent(dropDown);
            }
        } else {
            this.questDropDown.gameObject.SetActive(!questDropDown.gameObject.activeSelf);
        }
    }

}
