using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/BarrierPowerUp")]
public class BarrierPowerUp : PowerupEffect
{
    public float BarrierDuartion;
    public override void ApplyPowerUpEffect()
    {
        Player.playerInstance.BarrierActivate(BarrierDuartion);
    }
}
