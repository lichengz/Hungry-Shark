using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark
{ 
    public class PlayerManager : MonoBehaviour
    {
        public MoveObject player;
        
        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;
        
        private Stack<Command> undoCommands = new Stack<Command>();
        private Stack<Command> redoCommands = new Stack<Command>();

        private bool isReplaying = false;

        private Vector3 startPos;

        private const float REPLAY_PAUSE_TIMER = 0.5f;

        private bool isPlayerMoving = false;



        void Start()
        {
            //Bind the keys to default commands
            buttonW = new TurnRightCommand(player);
            buttonA = new MoveForwardCommand(player);
            buttonS = new TurnLeftCommand(player);
            buttonD = new MoveBackCommand(player);

            startPos = player.transform.position;
            player.onFinishMoving += ResetMovingState;
        }



        void Update()
        {
            if (isReplaying)
            {
                return;
            }

            if (!isPlayerMoving)
            {
                if (player.transform.position.y < 0 && Input.GetKeyDown(KeyCode.W))
                {
                    ExecuteNewCommand(buttonW);
                }
                else if (player.transform.position.x > -10 && Input.GetKeyDown(KeyCode.A))
                {
                    ExecuteNewCommand(buttonA);
                }
                else if (player.transform.position.y > -10 && Input.GetKeyDown(KeyCode.S))
                {
                    ExecuteNewCommand(buttonS);
                }
                else if (player.transform.position.x < 10 && Input.GetKeyDown(KeyCode.D))
                {
                    ExecuteNewCommand(buttonD);
                }
            }
        }

        private void ResetMovingState()
        {
            isPlayerMoving = false;
        }


        private IEnumerator Replay()
        {
            player.transform.position = startPos;

            yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);

            Command[] oldCommands = undoCommands.ToArray();
            
            for (int i = oldCommands.Length - 1; i >= 0; i--)
            {
                Command currentCommand = oldCommands[i];

                currentCommand.Execute();

                yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);
            }

            isReplaying = false;
        }

        private void ExecuteNewCommand(Command commandButton)
        {
            isPlayerMoving = true;
            
            commandButton.Execute();

            undoCommands.Push(commandButton);

            redoCommands.Clear();
        }
    }
}

