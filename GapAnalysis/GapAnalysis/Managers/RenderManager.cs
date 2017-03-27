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

        public void screenSizeChanged(Rectangle newScreensize) {
            Screensize = newScreensize;
        }

    }//E N D  C L A S S 
}
