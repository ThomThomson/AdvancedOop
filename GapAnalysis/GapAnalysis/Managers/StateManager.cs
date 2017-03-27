using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers {
    public class StateManager {
        public List<GameObjects.IGameObject> GameObjectList;

        public StateManager() {
            GameObjectList = new List<GameObjects.IGameObject>();
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
