using System;
using UnityEngine;

public interface ICanUseBonus
{
    event Action PlayerObjectInteractionE;

    Transform FollowCamera {get;}

    void UseBonus(IBuff buff);
}
