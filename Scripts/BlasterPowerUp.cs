using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/BlasterPowerUp")]
public class BlasterPowerUp : PowerupEffect
{
    public float blastDuration;

    public override void ApplyPowerUpEffect()
    {
        Player.playerInstance.BlasterActivate(blastDuration);
    }
}
