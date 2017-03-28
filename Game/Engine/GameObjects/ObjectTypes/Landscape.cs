using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;

namespace Engine.GameObjects.ObjectTypes {
    public enum LandscapeType { grass, dirt, rock, bedrock, goal }

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
        RenderLayer renderLayer = RenderLayer.terrain;
        private int generationIterations;
        private int edgeBuffer;
        private int grassSpawnThreshold;
        private int rockSpawnThreshold;

        public List<List<LandscapeTile>> tilesMap { get; set; }
        public int landscapeWidth { get; set; }
        public int landscapeHeight { get; set; }
        public int pixelHeightPerTile { get; set; }
        public int pixelWidthPerTile { get; set; }
        Random rand;

        public Landscape(int inWidth, int inHeight, int generationIterations, int edgeBuffer, int grassSpawnThreshold, int rockSpawnThreshold) {
            rand = new Random();
            pixelHeightPerTile = 0;
            pixelHeightPerTile = 0;
            this.landscapeWidth = inWidth;
            this.landscapeHeight = inHeight;
            this.generationIterations = generationIterations;
            this.grassSpawnThreshold = grassSpawnThreshold;
            this.rockSpawnThreshold = rockSpawnThreshold;
            if (edgeBuffer > 1) { this.edgeBuffer = edgeBuffer; } 
            else { edgeBuffer = 1; }
            
        }//E N D  C O N S T R U C T O R

        public RenderLayer GetRenderLayer() {return renderLayer;}

        public void RenderSelf(Graphics graphics, Rectangle viewPort) {
            pixelHeightPerTile = viewPort.Height / landscapeHeight;
            pixelWidthPerTile = viewPort.Width / landscapeWidth;
            int originX = 0, originY = 0;
            foreach(List<LandscapeTile> row in tilesMap) {
                originX = 0;
                foreach (LandscapeTile tile in row) {
                    if(tile.tileType == LandscapeType.dirt) {
                        Color dirtColor = Color.FromArgb(224 + tile.brightnessOffset, 188 + tile.brightnessOffset, 116 + tile.brightnessOffset);
                        Brush brush = new SolidBrush(dirtColor);
                        Rectangle dirtBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        graphics.FillRectangle(brush, dirtBounds);
                    }
                    else if (tile.tileType == LandscapeType.rock) {
                        Color rockColor = Color.FromArgb(100 + tile.brightnessOffset, 100 + tile.brightnessOffset, 80 + tile.brightnessOffset);
                        Brush brush = new SolidBrush(rockColor);
                        Rectangle rockBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        List<PointF> scaledShape = new List<PointF>();
                        foreach(PointF point in tile.randomShape) {
                            scaledShape.Add(new PointF(rockBounds.X + (point.X * rockBounds.Width /10), (rockBounds.Y - (rockBounds.Height)) + (point.Y * rockBounds.Height / 10)));
                        }
                        graphics.FillRectangle(brush, rockBounds);
                        graphics.FillPolygon(brush, scaledShape.ToArray());
                    }
                    else if(tile.tileType == LandscapeType.grass) {
                        Color grassColor = Color.FromArgb(200 + tile.brightnessOffset, 224 + tile.brightnessOffset, 30 + tile.brightnessOffset);
                        Brush brush = new SolidBrush(grassColor);
                        Rectangle grassbounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        graphics.FillRectangle(brush, grassbounds);
                        graphics.FillEllipse(brush, new Rectangle(originX, originY - grassbounds.Height / 2, pixelWidthPerTile, pixelHeightPerTile));
                    }
                    else if(tile.tileType == LandscapeType.bedrock){
                        Brush brush = new SolidBrush(Color.FromArgb(Math.Abs(tile.brightnessOffset), Math.Abs(tile.brightnessOffset), Math.Abs(tile.brightnessOffset)));
                        Rectangle rockBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        List<PointF> scaledShape = new List<PointF>();
                        foreach (PointF point in tile.randomShape){
                            scaledShape.Add(new PointF(rockBounds.X + (point.X * rockBounds.Width / 10), (rockBounds.Y - (rockBounds.Height)) + (point.Y * rockBounds.Height / 10)));
                        }
                        graphics.FillRectangle(brush, rockBounds);
                        graphics.FillPolygon(brush, scaledShape.ToArray());
                    }
                    else if(tile.tileType == LandscapeType.goal){
                        Brush brush = new SolidBrush(Color.AliceBlue);
                        Rectangle goalBounds = new Rectangle(originX, originY, pixelWidthPerTile, pixelHeightPerTile);
                        graphics.FillRectangle(brush, goalBounds);
                    }
                    originX += pixelWidthPerTile;
                }
                originY += pixelHeightPerTile;
            }
        }


        public void Start() {
            Array landscapeTypes = Enum.GetValues(typeof(LandscapeType));
            tilesMap = new List<List<LandscapeTile>>();
            int goalRow = rand.Next(3, landscapeHeight - 3);
            //seed initial map
            for (int row = 0; row < landscapeHeight; row++) {
                List<LandscapeTile> currentRow = new List<LandscapeTile>();
                for (int col = 0; col < landscapeWidth; col++){
                    if (row == goalRow && col == 0){
                        currentRow.Add(new LandscapeTile(LandscapeType.goal, rand.Next(-30, 30), generateRandomShape()));
                    }
                    else if (row == 0 || col == 0 || col == landscapeWidth - 1 || row == landscapeHeight - 1) {
                        currentRow.Add(new LandscapeTile(LandscapeType.bedrock, rand.Next(-30, 30), generateRandomShape()));
                    }
                    else {
                        if (col < edgeBuffer || col > landscapeWidth - edgeBuffer - 1 || row < edgeBuffer || row > landscapeHeight - edgeBuffer - 1) {
                            currentRow.Add(new LandscapeTile(LandscapeType.rock, rand.Next(-30, 30), generateRandomShape()));
                        }
                        else {
                            LandscapeType randomLandscapeType = (LandscapeType)landscapeTypes.GetValue(rand.Next(landscapeTypes.Length - 2));
                            currentRow.Add(new LandscapeTile(randomLandscapeType, rand.Next(-30, 30), generateRandomShape()));
                        }
                    }
                }
                tilesMap.Add(currentRow);
            }//end seed
            //iterate on generation (cell generation)
            for(int iteration = 0; iteration < generationIterations; iteration++) {
                for(int row = edgeBuffer; row < landscapeHeight - edgeBuffer; row++) {
                    for(int col = edgeBuffer; col < landscapeWidth - edgeBuffer; col++) {
                        int rocksCount = 0; int grassCount = 0;
                        //proximity search of all neighbors.
                        for(int searchRow = row - 1; searchRow < row + 2; searchRow++) {
                            for(int searchCol = col - 1; searchCol < col + 2; searchCol++) {
                                if(searchRow != row || searchCol != col) {
                                    if (tilesMap[searchRow][searchCol].tileType == LandscapeType.rock) { rocksCount++; }
                                    if (tilesMap[searchRow][searchCol].tileType == LandscapeType.grass) { grassCount++; }
                                }
                            }
                        }//end proximity search
                        if (rocksCount > rockSpawnThreshold) { tilesMap[row][col].tileType = LandscapeType.rock; }
                        //else if( grassCount > grassSpawnThreshold) { tilesMap[row][col].tileType = LandscapeType.grass; } 
                        //else { tilesMap[row][col].tileType = LandscapeType.dirt; }
                    }
                }
            }
            //clean up excess rocks
            for (int row = edgeBuffer; row < landscapeHeight - edgeBuffer; row++) {
                for (int col = edgeBuffer; col < landscapeWidth - edgeBuffer; col++) {
                    if(tilesMap[row][col].tileType == LandscapeType.rock) {
                        int rocksCount = 0; int grassCount = 0;
                        //proximity search of all neighbors.
                        for (int searchRow = row - 1; searchRow < row + 2; searchRow++) {
                            for (int searchCol = col - 1; searchCol < col + 2; searchCol++) {
                                if (searchRow != row || searchCol != col) {
                                    if (tilesMap[searchRow][searchCol].tileType == LandscapeType.rock) { rocksCount++; }
                                    if (tilesMap[searchRow][searchCol].tileType == LandscapeType.grass) { grassCount++; }
                                }
                            }
                        }//end proximity search
                        if (rocksCount < rockSpawnThreshold) {
                            if (grassCount > grassSpawnThreshold) { tilesMap[row][col].tileType = LandscapeType.grass; }
                            else { tilesMap[row][col].tileType = LandscapeType.dirt; }
                        }
                    }
                }
            }//end clean up

        }

        public List<PointF> generateRandomShape() {
            List<PointF> shape = new List<PointF>();
            int numPoints = rand.Next(5, 9);
            for (int point = 0; point < numPoints; point++) {
                if (point == 0) { shape.Add(new PointF(rand.Next(0, 2), 10f)); }
                else if(point == 1) { shape.Add(new PointF(rand.Next(0, 2), 7f)); }
                else if(point == numPoints - 2) { shape.Add(new PointF(rand.Next(8, 10), 7f)); }
                else if (point == numPoints - 1) { shape.Add(new PointF(rand.Next(8, 10), 10f)); }
                else if(point < numPoints / 2) { shape.Add(new PointF(rand.Next(0, 3), rand.Next(0, 7))); } 
                else { shape.Add(new PointF(rand.Next(7, 10), rand.Next(0, 7))); }
            }
            return shape;
        }

        public void Tick() {
            //Console.WriteLine("Landscape Tick");
        }
    }
}
