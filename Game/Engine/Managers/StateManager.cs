using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Engine.Managers {
    public class StateManager {
        public bool victoryConditionAchieved = false;
        public List<GameObjects.IGameObject> GameObjectList;
        public int ballsNum;
        public int originalBalls;
        public int level = 1;
        Timer gameTimer;
        GameForm parentForm;

        public StateManager(GameForm parentform){
            this.parentForm = parentform;
        }

        /// <summary>
        /// the StateManager requires a timer so it can pause on victory or loss.
        /// </summary>
        /// <param name="inTimer">The Timer object passed in from the Game Form</param>
        public void setTimer(Timer inTimer){
            gameTimer = inTimer;
        }

        /// <summary>
        /// The object list of the StateManager should be changable after each round of the game.
        /// </summary>
        /// <param name="inGameObjectList">The GameObject List, created newly each round</param>
        /// <param name="numBalls">The number of balls that start this round</param>
        public void setObjectList(List<GameObjects.IGameObject> inGameObjectList, int numBalls) {
            GameObjectList = inGameObjectList;
            ballsNum = numBalls;
            originalBalls = numBalls;
        }

        /// <summary>
        /// loops through all objects in the game's state and starts each one
        /// </summary>
        public void StartAll() {
            foreach (GameObjects.IGameObject GO in GameObjectList) {
                GO.Start();
            }
        }

        /// <summary>
        /// loops through all objects in the game's state and ticks each one
        /// </summary>
        public void TickAll() {
            foreach(GameObjects.IGameObject GO in GameObjectList) {
                GO.Tick();
            }
        }

        /// <summary>
        /// handles the victory of the balls and sends a message to the Game Form
        /// </summary>
        public void ballVictory() {
            gameTimer.Enabled = false;
            level = 1;
            parentForm.message = "You hecked up. Press enter to restart game";
            parentForm.Invalidate();
            ballsNum = 10;
        }
        /// <summary>
        /// handles the victory of the keyboard player
        /// </summary>
        public void keyboardVictory() {
            gameTimer.Enabled = false;
            level++;
            parentForm.message = "VICTORY. Press enter to load level " + level;
            ballsNum += 5;
        }

        /// <summary>
        /// handles resetting all gameobjects to their default values at the beginning of each round
        /// </summary>
        /// <param name="inInputManager">The InputManager from the GameObject which called the victory</param>
        public void RestartGame(Managers.InputManager inInputManager) {
            Program.startup(ballsNum, inInputManager, this);
            parentForm.message = "";
            StartAll();
            gameTimer.Enabled = true;
        }
    }
}
