using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class InkDialogueManager : MonoBehaviour
{
    [Header("系統")]
    public EventManager eventManager;

    [Header("Ink 檔案")]
    public TextAsset inkJSONAsset;

    [Header("UI 元件")]
    public TextMeshProUGUI dialogueText;
    public Button buttonPrefab;
    public Transform buttonContainer;

    private Story currentStory;

    void Start()
    {
        // 初始化故事
        currentStory = new Story(inkJSONAsset.text);

        // 開始顯示第一段劇情
        RefreshDialogue();
    }

    void RefreshDialogue()
    {
        // 清空舊的對話文字
        dialogueText.text = "";

        // 讀取這一段落所有的文字，直到遇到選項或結束
        while (currentStory.canContinue)
        {
            // 每呼叫一次 Continue()，Ink 就會把那一行的 Tag 暫存到 currentTags 裡
            dialogueText.text += currentStory.Continue();

            // 讀完字馬上解析這一行的 Tag，並丟給 EventManager
            ParseTags();
        }

        // 顯示對應的選項
        ShowChoices();
    }

    void ParseTags()
    {
        // 如果這行沒有 Tag，直接跳過
        if (currentStory.currentTags.Count == 0) return;

        foreach (string tag in currentStory.currentTags)
        {
            // 將 "# bg: office" 切割成 key 和 value
            string[] splitTag = tag.Split(':');
            if (splitTag.Length == 2)
            {
                string key = splitTag[0].Trim();
                string value = splitTag[1].Trim();

                // 呼叫 EventManager 去派發任務
                if (eventManager != null)
                {
                    eventManager.InvokeEvent(key, value);
                }
                else
                {
                    Debug.LogWarning("[InkDialogueManager] 尚未綁定 EventManager！");
                }
            }
        }
    }

    void ShowChoices()
    {
        // 生成新選項前，先清空容器內舊的按鈕
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // 檢查故事目前是否有選項
        if (currentStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < currentStory.currentChoices.Count; i++)
            {
                Choice choice = currentStory.currentChoices[i];

                // 動態生成按鈕，並將其放在 buttonContainer 底下
                Button newButton = Instantiate(buttonPrefab, buttonContainer);

                // 抓取按鈕底下的 TextMeshProUGUI，並替換成選項文字
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choice.text;
                }

                // 綁定點擊事件：當按鈕被點擊時，呼叫 OnClickChoice 並傳入該選項的索引
                newButton.onClick.AddListener(() =>
                {
                    OnClickChoice(choice.index);
                });
            }
        }
    }

    void OnClickChoice(int choiceIndex)
    {
        // 告訴 Ink 玩家選了哪一個選項
        currentStory.ChooseChoiceIndex(choiceIndex);

        // 刷新畫面，顯示下一段劇情與新選項
        RefreshDialogue();
    }
}