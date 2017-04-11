using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers {
    public enum RenderLayer {//in order from lowest to highest
        terrain, entity, ui
    }

    public class RenderManager {
        public StateManager gameState;
        public Rectangle Screensize;

        public RenderManager(StateManager inStateManager, Rectangle inScreensize) {
            gameState = inStateManager;
            Screensize = inScreensize;
        }
        /// <summary>
        /// calls the render method for each gameobject currently active in the game's state
        /// </summary>
        /// <param name="screen">Graphics object passed in from the Game Form</param>
        public void RenderAll(Graphics screen) {
            if (gameState != null) {
                foreach(RenderLayer currentLayer in Enum.GetValues(typeof(RenderLayer))) {
                    foreach (GameObjects.IGameObject GO in gameState.GameObjectList) {
                        if (GO.GetRenderLayer() == currentLayer) {
                            GO.RenderSelf(screen, Screensize);
                        }
                    }//gameobjects loop
                }//renderlayers loop
            }
        }//E N D  M E T H O D  RenderAll

        /// <summary>
        /// handles changing of the screensize
        /// </summary>
        /// <param name="newScreensize">The Rectangle representing the screen at its current size</param>
        public void screenSizeChanged(Rectangle newScreensize) {
            Screensize = newScreensize;
        }

    }//E N D  C L A S S 
}
