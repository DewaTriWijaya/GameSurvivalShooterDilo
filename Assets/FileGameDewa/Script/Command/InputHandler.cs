using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMove playerMove;
    public PlayerShoot playerShoot;

    // Queue untuk menyimpan list command
    Queue<Command> commands = new Queue<Command>();

    void FixedUpdate()
    {
        // Menghandle input move
        Command moveCommand = InputMoveHandling();
        if(moveCommand != null)
        {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }
    }

    void Update()
    {
        // Menghandle shoot
        Command shootCommand = InputShootHandling();
        if(shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    Command InputMoveHandling()
    {
        // Check jika move sesuai dengan key nya
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMove, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMove, -1, 0); 
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMove, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMove, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            // undo move
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMove, 0, 0);
        }
    }

    Command Undo()
    {
        // jika Queue command tidak kosong, lakukan perintah undo
        if(commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }
        return null;
    }

    Command InputShootHandling()
    {
        // jika menembak trigger shoot command
        if (Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(playerShoot);
        }
        else
        {
            return null;
        }
    }
}
