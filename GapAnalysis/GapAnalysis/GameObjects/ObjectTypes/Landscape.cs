using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;

namespace Engine.GameObjects.ObjectTypes {
    public enum LandscapeType { grass, dirt, rock }

    public class LandscapeTile {
        public int brightnessOffset;
        public bool tileChanged { get; set; }
        public LandscapeType tileType { get; set; }
        public List<PointF> randomShape;
        public LandscapeTile(LandscapeType inType, int inBrightnessOffset, List<PointF> inRandomShape) {
            tileType = inType;
            randomShape = inRandomShape;
            tileChanged = false;
            brightnessOffset = inBrightnessOffset;
        }
    }

    public class Landscape : IGameObject {
        List<List<LandscapeTile>> tilesMap;
        RenderLayer renderLayer = RenderLayer.terrain;
        private int landscapeWidth;
        private int landscapeHeight;
        Random rand;

        public Landscape(int inWidth, int inHeight) {
            rand = new Random();
            this.landscapeWidth = inWidth;
            this.landscapeHeight = inHeight;
        }//E N D  C O N S T R U C T O R

        public RenderLayer GetRenderLayer() {return renderLayer;}

        public void RenderSelf(Graphics graphics, Rectangle viewPort) {
            Console.WriteLine("rendered");
            int pixelHeightPerTile = viewPort.Height / landscapeHeight;
            int pixelWidthPerTile = viewPort.Width / landscapeWidth;
            int originX = 0, originY = 0;
            foreach(List<LandscapeTile> row in tilesMap) {
                originX = 0;
                foreach (LandscapeTile tile in row) {
                    if(tile.tileType == LandscapeType.dirt) {
                        Color dirtColor = Color.FromArgb(140 + tile.brightnessOffset, 100 + tile.brightnessOffset, 44 + tile.brightnessOffset);
                        Brush brush = new SolidBrush(dirtColor);
                        Rectangle dirtBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        graphics.FillRectangle(brush, dirtBounds);
                    }
                    if (tile.tileType == LandscapeType.rock) {
                        Color rockHighlightColor = Color.FromArgb(170 + tile.brightnessOffset, 170 + tile.brightnessOffset, 150 + tile.brightnessOffset);
                        Color rockBackColor = Color.FromArgb(150 + tile.brightnessOffset, 150 + tile.brightnessOffset, 130 + tile.brightnessOffset);
                        Brush highlightBrush = new SolidBrush(rockHighlightColor);
                        Brush backBrush = new SolidBrush(rockBackColor);
                        Rectangle rockBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        List<PointF> scaledShape = new List<PointF>();
                        foreach(PointF point in tile.randomShape) {
                            scaledShape.Add(new PointF(rockBounds.X + (point.X * rockBounds.Width /10), (rockBounds.Y - (rockBounds.Height / 2)) + (point.Y * rockBounds.Height / 10)));
                        }
                        graphics.FillRectangle(backBrush, rockBounds);
                        graphics.FillPolygon(highlightBrush, scaledShape.ToArray());
                    }
                    originX += pixelWidthPerTile;
                }
                originY += pixelHeightPerTile;
            }
        }


        public void Start() {
            Array landscapeTypes = Enum.GetValues(typeof(LandscapeType));
            int numPoints = (landscapeWidth * landscapeHeight) / (landscapeWidth + landscapeHeight / 2);
            tilesMap = new List<List<LandscapeTile>>();
            if (numPoints > 1000000) {
                //EVENTUALLY COOLER GENERATION HERE YEAHHHH
            } else {
                Console.WriteLine("Tilemap too small for voronoi generation BECAUSE I DISABLED IT");
                for (int row = 0; row < landscapeHeight; row++) {
                    List<LandscapeTile> currentRow = new List<LandscapeTile>();
                    for (int col = 0; col < landscapeWidth; col++) {
                        LandscapeType randomLandscapeType = (LandscapeType)landscapeTypes.GetValue(rand.Next(landscapeTypes.Length));
                        currentRow.Add(new LandscapeTile(randomLandscapeType, rand.Next(-30, 30), generateRandomShape()));
                    }
                    tilesMap.Add(currentRow);
                }//E N D  L O O P 
            }
        }

        public List<PointF> generateRandomShape() {
            int numPoints = rand.Next(5, 8);
            List<PointF> shape = new List<PointF>();
            for(int i = 0; i < 4; i++) {
                int xMin = 0, xMax = 5, yMin = 0, yMax = 5;
                if (i == 1 || i == 3) { xMin = 5; xMax = 10; }
                if (i == 2 || i == 3) { yMin = 5; yMax = 10; }
                for (int j = 0; j < numPoints / 4; j++) {
                    shape.Add(new PointF(rand.Next(xMin, xMax +1), rand.Next(yMin, yMax + 1)));
                }
            }
            return shape;
        }

        public void Tick() {
            //Console.WriteLine("Landscape Tick");
        }
    }
}
