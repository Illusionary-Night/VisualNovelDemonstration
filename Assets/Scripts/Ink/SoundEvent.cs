using UnityEngine;

public class SoundEvent : MonoBehaviour, IEvent
{
    // 指定這個腳本專門負責處理 play_sound 標籤
    public EventType type => EventType.play_sound;

    public void Execute(string tagValue)
    {
        // 這裡替換成你們實際的 AudioSource 播放邏輯
        Debug.Log($"[SoundEvent] 接收到指令，準備播放音效：{tagValue}");
    }
}