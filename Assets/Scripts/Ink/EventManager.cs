using System;
using System.Collections.Generic;
using UnityEngine;

// 3. EventManager 實作
public class EventManager : MonoBehaviour
{
    // 改用 Dictionary 來儲存，Key 是 Enum，Value 是對應的 IEvent 腳本
    private Dictionary<EventType, IEvent> eventDict = new Dictionary<EventType, IEvent>();

    void Start()
    {
        IEvent[] foundEvents = GetComponentsInChildren<IEvent>();

        foreach (var e in foundEvents)
        {
            // TryAdd 會嘗試加入，如果 Key 已經存在會回傳 false，我們就可以直接報錯
            if (!eventDict.TryAdd(e.type, e))
            {
                Debug.LogWarning($"[EventManager] 警告：場景中發現多個負責 {e.type} 的腳本！已略過重複項目。");
            }
        }
    }

    // 觸發事件的方法
    public void InvokeEvent(string key, string value)
    {
        // 1. 先將 Ink 傳來的字串，安全地轉成 Enum
        if (Enum.TryParse(key, true, out EventType type))
        {
            // 2. 利用 TryGetValue 直接從 Dictionary 裡面撈出對應的腳本
            if (eventDict.TryGetValue(type, out IEvent targetEvent))
            {
                // 3. 執行！
                targetEvent.Execute(value);
            }
            else
            {
                // 如果 Enum 有定義，但場景中沒有人掛載對應的腳本
                Debug.LogWarning($"[EventManager] 標籤 '{type}' 轉換成功，但場景中找不到處理它的腳本！");
            }
        }
        else
        {
            Debug.LogWarning($"[EventManager] 劇本中出現未定義的標籤字串：{key}");
        }
    }
}