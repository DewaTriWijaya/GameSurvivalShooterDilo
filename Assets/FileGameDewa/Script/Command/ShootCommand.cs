using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command
{
    PlayerShoot playerShoot;

    public ShootCommand(PlayerShoot _playerShoot)
    {
        playerShoot = _playerShoot;
    }

    public override void Execute()
    {
        // player menembak
        //playerShoot.Shoot();
    }

    public override void UnExecute()
    {
        
    }
}
