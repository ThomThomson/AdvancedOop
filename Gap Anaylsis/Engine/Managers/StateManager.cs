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
        /// handles resetting all gameobjects to their default values at the beginning of each round
        /// </summary>
        /// <param name="inInputManager">The InputManager from the GameObject which called the victory</param>
        public void RestartGame(Managers.InputManager inInputManager) {
            Program.startup(inInputManager, this);
            StartAll();
            gameTimer.Enabled = true;
        }


        /// <summary>
        /// The object list of the StateManager should be changable after each round of the game.
        /// </summary>
        /// <param name="inGameObjectList">The GameObject List, created newly each round</param>
        /// <param name="numBalls">The number of balls that start this round</param>
        public void setObjectList(List<GameObjects.IGameObject> inGameObjectList) {
            GameObjectList = inGameObjectList;
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
    }
}
