using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.GameFramework
{
    public enum PlayerActions
    {
        None = 0,
        MoveLeft = 1,
        StopMoveLeft = 2,
        MoveRight = 3,
        StopMoveRight = 4,
        Jump = 5,
        StopJump = 6
    }
}
