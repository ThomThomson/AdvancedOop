using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Engine.Managers;
using System.Threading.Tasks;

namespace Engine.GameObjects.ObjectTypes {
    class Ball {
        #region S T A R T U P 
        public Rectangle bounds;
        public int exploding = 0;
        public int solidifying = 0;

        public int bounceCooldown = 5;
        public int bounceTime = 0;

        public int landscapeRow;
        public int landscapeCol;
        public float[] headingDirection = new float[2];

        /// <summary>
        /// The multiparameter Constructor initializes the ball the the values set out by the ballManager
        /// </summary>
        /// <param name="inBounds">The Rectangle for the size of the ball</param>
        /// <param name="headingX">The startingX direction of the ball</param>
        /// <param name="headingY">The startingY direction of the ball</param>
        /// <param name="inRow">The randomly generated row for the ball</param>
        /// <param name="inCol">The randomly generated column for the ball</param>
        public Ball(Rectangle inBounds, float headingX, float headingY, int inRow, int inCol) {
            bounds = inBounds;
            headingDirection = new float[2];
            headingDirection[0] = headingX;
            headingDirection[1] = headingY;
            landscapeRow = inRow;
            landscapeCol = inCol;
        }
        #endregion

        #region D O M O V E
        /// <summary>
        /// doMove handles the moving of the ball, checking whether the ball has been clicked, starting its explosion
        /// determining collisions with the walls, and collisions with the keyboard player.
        /// </summary>
        /// <param name="player">Player reference handed in from the BallManager</param>
        /// <param name="landscape">Landscape reference from the BallManager</param>
        /// <param name="inputManager">InputManager reference from the BallManager</param>
        /// <param name="stateManager">StateManager reference from the BallManager</param>
        /// <param name="threshold">explode and solidify threshold passed in from the BallManager</param>
        public void DoMove(KeyPlayer player, Landscape landscape, InputManager inputManager, StateManager stateManager, int threshold) {
            bounceTime++;
            bounds.Width = landscape.pixelWidthPerTile;
            bounds.Height = landscape.pixelWidthPerTile;

            //handle E x p l o s i o n s
            if (exploding == threshold) {
                if ((landscapeCol > 2 && landscapeCol < landscape.landscapeWidth - 2) &&
                (landscapeRow > 2 && landscapeRow < landscape.landscapeHeight - 2)) {
                    for (int row = -2; row < 3; row++) {
                        for (int col = -1; col < 2; col++) {
                            landscape.tilesMap[landscapeRow + row][landscapeCol + col].tileType = LandscapeType.dirt;
                        }
                    }
                }
                exploding++;
            } else if (exploding == 1) {
                bounds.Width *= 5;
                bounds.Height *= 5;
                exploding++;
            } else if (exploding > 0) {
                exploding++;
            }

            //handle S o l i d i f y i n g
            if (solidifying == threshold) {
                if ((landscapeCol > 2 && landscapeCol < landscape.landscapeWidth - 2) &&
                (landscapeRow > 2 && landscapeRow < landscape.landscapeHeight - 2)) {
                    for (int row = -2; row < 3; row++) {
                        for (int col = -1; col < 2; col++) {
                            landscape.tilesMap[landscapeRow + row][landscapeCol + col].tileType = LandscapeType.bedrock;
                        }
                    }
                }
                solidifying++;
            } else if (solidifying > 0) {
                solidifying++;
            }

            //on spawn set the bounds to the correct location
            if (bounds.X < 0 && bounds.Y < 0 && landscape.pixelHeightPerTile > 0) {
                bounds.X = (landscapeCol * landscape.pixelWidthPerTile);
                bounds.Y = (landscapeRow * landscape.pixelHeightPerTile);
            } else {
                if (bounceTime > bounceCooldown) {
                    if (bounds.X <= 0 || bounds.Right >= landscape.landscapeWidth * landscape.pixelWidthPerTile) {
                        headingDirection[0] *= -1;
                        bounceTime = 0;
                    }
                    if (bounds.Y <= 0 || bounds.Bottom >= landscape.landscapeHeight * landscape.pixelHeightPerTile) {
                        headingDirection[1] *= -1;
                        bounceTime = 0;
                    }
                }
                bounds.X = (int)(bounds.X + headingDirection[0]);
                bounds.Y = (int)(bounds.Y + headingDirection[1]);
                //mouse influence (IT'S A FEATURE)
                //bounds.X = (int)(bounds.X + (inputManager.mouseCoords[0] - bounds.X) * 0.008);
                //bounds.Y = (int)(bounds.Y + (inputManager.mouseCoords[1] - bounds.Y) * 0.008);
            }
            if (landscape.pixelWidthPerTile != 0 && landscape.pixelHeightPerTile != 0) {
                landscapeCol = (bounds.X + (bounds.Width / 2)) / landscape.pixelWidthPerTile;
                landscapeRow = (bounds.Y + (bounds.Height / 2)) / landscape.pixelHeightPerTile;
            }
            //Mouse clicking on ball
            if ((inputManager.mouseCoords[0] > bounds.X && inputManager.mouseCoords[0] < bounds.X + bounds.Width) &&
               (inputManager.mouseCoords[1] > bounds.Y && inputManager.mouseCoords[1] < bounds.Y + bounds.Height)) {
                if (inputManager.clickDown && exploding == 0 && solidifying == 0) {
                    exploding = 1;
                } else if (inputManager.rightClickDown && exploding == 0 && solidifying == 0) {
                    solidifying = 1;
                }
            }
            //ball hitting player
            if (!player.invincible && bounds.IntersectsWith(player.bounds)) {
                player.invincible = true;
                player.health -= 1;
                if (player.health <= 0) {
                    stateManager.ballVictory();
                } else {
                    player.image = Image.FromFile(String.Format("../../images/charHealth-{0}.png", player.health));
                }
            }
        }
        #endregion
    }
}
