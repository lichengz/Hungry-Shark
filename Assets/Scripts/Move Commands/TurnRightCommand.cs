using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark
{
    public class TurnRightCommand : Command
    {
        private MoveObject moveObject;
    

        public TurnRightCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }


        public override void Execute()
        {
            moveObject.TurnRight();
        }


        //Undo is just the opposite
        public override void Undo()
        {
            moveObject.TurnLeft();
        }
    }
}
