using UnityEngine;

public interface IInput
{
    Vector3 ReturnDirection();

    bool ReturnJumpAction();

    void DisableInput();

    void EnableInput();
}
