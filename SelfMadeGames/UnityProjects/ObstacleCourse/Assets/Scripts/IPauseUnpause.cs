using UnityEngine;

public interface IPauseUnpause
{
    bool IsPaused { get; }

    void Pause();

    void Unpause();
}
