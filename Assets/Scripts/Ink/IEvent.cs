using UnityEngine;
public enum EventType
{
    play_sound
}
public interface IEvent
{
    EventType type { get; }

    void Execute(string tagValue);
}
