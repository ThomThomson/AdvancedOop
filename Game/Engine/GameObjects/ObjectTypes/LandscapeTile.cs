using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engine.GameObjects.ObjectTypes {
    public enum LandscapeType { grass, rock, dirt, bedrock, goal }

    public class LandscapeTile {
        public int brightnessOffset;
        public bool tileChanged { get; set; }
        public LandscapeType tileType { get; set; }
        public List<PointF> randomShape;
        /// <summary>
        /// The constructor for a Landscape tile is just a plain old C# object constructor.
        /// </summary>
        /// <param name="inType">The type of tile, using enum LandscapeType</param>
        /// <param name="inBrightnessOffset">a random int to brighten or darken the color of the tile. Stored to make it consistent</param>
        /// <param name="inRandomShape">The random shape of this tile. Only used for rocks</param>
        public LandscapeTile(LandscapeType inType, int inBrightnessOffset, List<PointF> inRandomShape) {
            tileType = inType;
            randomShape = inRandomShape;
            tileChanged = false;
            brightnessOffset = inBrightnessOffset;
        }
    }
}
