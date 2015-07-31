using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleButtonTest : MonoBehaviour {
    public Transform questDropDownPrefab;
    public Transform questButton;

    public string[] questStrings;

    public QuestAsset[] quests;

    private Transform questDropDown;

    public void OnClick() {
        if  (this.questDropDown == null) {
            Transform dropDown = Instantiate(this.questDropDownPrefab);

            dropDown.SetParent(this.transform.parent);
            dropDown.SetSiblingIndex(this.transform.GetSiblingIndex()+1);
            this.questDropDown = dropDown;

            foreach (string questName in this.questStrings) {
                Transform questButton = Instantiate(this.questButton);
                questButton.Find("Text").GetComponent<Text>().text = questName;
                questButton.SetParent(dropDown);
            }

            foreach (QuestAsset quest in this.quests) {
                Transform questButton = Instantiate(this.questButton);
                questButton.Find("Text").GetComponent<Text>().text = quest.questName;
                questButton.GetComponent<QuestButtonTest>().quest = quest;
                questButton.SetParent(dropDown);
            }
        } else {
            this.questDropDown.gameObject.SetActive(!questDropDown.gameObject.activeSelf);
        }
    }

}
