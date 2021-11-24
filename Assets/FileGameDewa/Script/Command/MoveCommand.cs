using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    PlayerMove playerMove;
    float h, v;

    public MoveCommand(PlayerMove _playerMove, float _h, float _v)
    {
        this.playerMove = _playerMove;
        this.h = _h;
        this.v = _v;
    }

    // Trigger perintah move
    public override void Execute()
    {
        //playerMove.Move(h, v);
        // Menganimasikan player
        //playerMove.Animating(h, v);
    }

    public override void UnExecute()
    {
        // Invers arah dari move player
        //playerMove.Move(-h, -v);
        // Menganimasikan player
        //playerMove.Animating(h, v);
    }
}
