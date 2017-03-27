using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.GameObjects {
    public interface IGameObject {
        void Start();
        void Tick();

        void RenderSelf(Graphics inGraphics, Rectangle viewPort);

        Managers.RenderLayer GetRenderLayer();
    }
}
