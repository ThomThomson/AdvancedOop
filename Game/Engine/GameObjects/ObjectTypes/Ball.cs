using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.GameObjects.ObjectTypes {
    class Ball {
        public Rectangle bounds;
        public int exploding = 0;
        public int solidifying = 0;

        public int landscapeRow;
        public int landscapeCol;
        public float[] headingDirection = new float[2];

        public Ball(Rectangle inBounds, float headingX, float headingY, int inRow, int inCol) {
            bounds = inBounds;
            headingDirection = new float[2];
            headingDirection[0] = headingX;
            headingDirection[1] = headingY;
            landscapeRow = inRow;
            landscapeCol = inCol;
        }
    }
}
