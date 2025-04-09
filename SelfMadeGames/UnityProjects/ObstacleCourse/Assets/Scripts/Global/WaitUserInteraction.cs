using UnityEngine;

public class WaitUserInteraction : CustomYieldInstruction
{
    // YieldInstruction to wait for User Interaction
    public override bool keepWaiting => !Input.anyKeyDown &&
                                        !Input.GetMouseButtonDown(0) &&
                                        !Input.GetMouseButtonDown(1) &&
                                        Input.touchCount == 0;
}
