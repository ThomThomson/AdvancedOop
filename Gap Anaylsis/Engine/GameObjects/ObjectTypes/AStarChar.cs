using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.AStar;

namespace Engine.GameObjects.ObjectTypes {
    class AStarChar : IGameObject {
        public Point goalCoords;
        public Point goalLandscapePos;
        public Point landscapePos;

        List<Point> path = new List<Point>();

        Landscape landscape;
        InputManager inputManager;
        public Rectangle bounds;
        public Rectangle imageBounds;
        public Image image = Image.FromFile("../../images/charHealth-4.png");
        private Random rand = new Random();
        private int placeInPath;

        public AStarChar(Landscape inLandscape, InputManager inInputManager) {
            landscape = inLandscape;
            inputManager = inInputManager;
        }

        public RenderLayer GetRenderLayer() {
            return RenderLayer.entity;
        }

        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            inGraphics.DrawImage(image, imageBounds);
        }

        public void Start() {
            while (true) {
                int row = rand.Next(0, landscape.landscapeHeight);
                int col = rand.Next(0, landscape.landscapeWidth);
                if (landscape.tilesMap[row][col].tileType == LandscapeType.grass) {
                    landscapePos.X = col; landscapePos.Y = row;
                    bounds = new Rectangle();
                    break;
                }
            }
        }

        public void Tick() {
            /*s i z i n g,  originally  l o c a t i n g,  and determining 
             * the  s p e e d  of the character based on the size of the landscape */
            if (landscape.pixelHeightPerTile != 0) {
                bounds.X = (landscapePos.X * landscape.pixelWidthPerTile);
                bounds.Y = (landscapePos.Y * landscape.pixelHeightPerTile);
                bounds.Width = landscape.pixelWidthPerTile / 2;
                bounds.Height = landscape.pixelHeightPerTile * 3;
                imageBounds.X = bounds.X - bounds.Width / 2;
                imageBounds.Y = bounds.Y - bounds.Height / 2;
                imageBounds.Width = landscape.pixelWidthPerTile;
                imageBounds.Height = landscape.pixelHeightPerTile * 5;
            }

            if(placeInPath != path.Count) {
                landscape.tilesMap[landscapePos.Y][landscapePos.X].tileType = LandscapeType.dirt;
                landscapePos = path[placeInPath];
                placeInPath++;
            }

            if (inputManager.clickDown) {
                placeInPath = 0;
                goalCoords = new Point(inputManager.mouseCoords[0], inputManager.mouseCoords[1]);
                goalLandscapePos.X = (int)(goalCoords.X / landscape.pixelWidthPerTile);
                goalLandscapePos.Y = (int)(goalCoords.Y / landscape.pixelHeightPerTile);

                //Perform the AStar pathfind here
                PathFinder pathfinder = new PathFinder(new SearchParameters(landscapePos, goalLandscapePos, landscape));
                path = pathfinder.FindPath();
                
            }
        }
    }
}
