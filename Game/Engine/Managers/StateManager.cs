using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers {
    public class StateManager {
        public bool victoryConditionAchieved = false;

        public List<GameObjects.IGameObject> GameObjectList;

        public void setObjectList(List<GameObjects.IGameObject> inGameObjectList) {
            GameObjectList = inGameObjectList;
        }

        public void StartAll() {
            foreach (GameObjects.IGameObject GO in GameObjectList) {
                GO.Start();
            }
        }

        public void TickAll() {
            foreach(GameObjects.IGameObject GO in GameObjectList) {
                GO.Tick();
            }
        }
    }
}
