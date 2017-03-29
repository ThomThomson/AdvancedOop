using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using System.Windows.Forms;

namespace Engine.GameObjects.ObjectTypes {
    class KeyPlayer : IGameObject {
        #region P A R A M S  &  S T A R T U P
        //Game References
        InputManager inputManager;
        StateManager stateManager;
        Landscape landscape;
        //rendering
        public Rectangle bounds;
        public Rectangle imageBounds;
        public Rectangle attackBounds;
        public Image image = Image.FromFile("../../images/charHealth-4.png");
        private Brush diggerBrush = new SolidBrush(Color.White);
        private int landscapeRow;
        private int landscapeCol;
        private int[] lastFacingDirection = new int[2];
        //internal objects
        private Random rand = new Random();
        //settings
        public int health;
        public int startingHealth;
        public bool invincible;
        private int invincibilityTime = 15;
        private int timeInvincible = 0;
        private float timeDigging = 0;
        private float digCooldown = 3f;
        private bool digHeld = false;

        /// <summary>
        /// The KeyPlayer's constructor handles injection of necessary game elements.
        /// </summary>
        /// <param name="inInputManager">The input manager reference to determine the mouse position etc</param>
        /// <param name="inStateManager">The state manager for declaring victory conditions</param>
        /// <param name="inLandscape">The landscape reference </param>
        /// <param name="inHealth">The starting health for the KeyPlayer</param>
        public KeyPlayer(Managers.InputManager inInputManager, StateManager inStateManager, Landscape inLandscape, int inHealth)  {
            health = inHealth;
            startingHealth = inHealth;
            inputManager = inInputManager;
            stateManager = inStateManager;
            landscape = inLandscape;
        }

        /// <summary>
        /// The KeyPlayer's start method spawns the player on a grass tile in the landscape.
        /// </summary>
        public void Start() {
            while (true) {
                int row = rand.Next(0, landscape.landscapeHeight);
                int col = rand.Next(0, landscape.landscapeWidth);
                if (landscape.tilesMap[row][col].tileType == LandscapeType.grass) {
                    landscapeCol = col; landscapeRow = row;
                    bounds = new Rectangle();
                    break;
                }
            }
        }
        #endregion

        #region T I C K
        /// <summary>
        /// The KeyPlayer's Tick method controls invincibility, collision with landscape, movement and digging.
        /// </summary>
        public void Tick() {
            //i n v i n c i b i l i t y  handling
            if (invincible && timeInvincible < invincibilityTime) {
                timeInvincible++;
            } else {
                invincible = false;
                timeInvincible = 0;
            }
            
            /*s i z i n g,  originally  l o c a t i n g,  and determining 
             * the  s p e e d  of the character based on the size of the landscape */
            int pixelSpeed = 0;
            if (landscape.pixelHeightPerTile != 0) {
                bounds.Width = landscape.pixelWidthPerTile / 2;
                bounds.Height = landscape.pixelHeightPerTile * 3;
                imageBounds.X = bounds.X - bounds.Width / 2;
                imageBounds.Y = bounds.Y - bounds.Height / 2;
                imageBounds.Width = landscape.pixelWidthPerTile;
                imageBounds.Height = landscape.pixelHeightPerTile * 5;
                pixelSpeed = landscape.pixelWidthPerTile / 4;

                if (bounds.X == 0 && bounds.Y == 0) {
                    bounds.X = (landscapeCol * landscape.pixelWidthPerTile);
                    bounds.Y = (landscapeRow * landscape.pixelHeightPerTile);
                } else {
                    landscapeCol = (int)((bounds.X + (bounds.Width / 2)) / landscape.pixelWidthPerTile);
                    landscapeRow = (int)((bounds.Y + (bounds.Height)) / landscape.pixelHeightPerTile);

                    landscape.tilesMap[landscapeRow][landscapeCol].tileType = LandscapeType.dirt;
                }
            }

            //M o v e m e n t code
            digHeld = false;
            if (landscape.tilesMap[landscapeRow][landscapeCol - 1].tileType == LandscapeType.goal) {//Victory condition
                stateManager.keyboardVictory();
            } else {//loop through all the keys currently held to determine what actions to take.
                foreach (KeyEventArgs key in inputManager.keysHeld) {
                    if (key.KeyCode == Keys.W) {
                        lastFacingDirection[0] = -1; lastFacingDirection[1] = 0;
                        LandscapeType forwardTile = landscape.tilesMap[landscapeRow - 1][landscapeCol].tileType;
                        if (forwardTile == LandscapeType.grass || forwardTile == LandscapeType.dirt ||
                                bounds.Y + bounds.Height > (landscapeRow * landscape.pixelHeightPerTile) + pixelSpeed) {
                            bounds.Y -= pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.A) {
                        lastFacingDirection[0] = 0; lastFacingDirection[1] = -1;
                        LandscapeType forwardTile = landscape.tilesMap[landscapeRow][landscapeCol - 1].tileType;
                        if (forwardTile == LandscapeType.grass || forwardTile == LandscapeType.dirt ||
                                bounds.X > (landscapeCol * landscape.pixelWidthPerTile) + pixelSpeed) {
                            bounds.X -= pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.S) {
                        lastFacingDirection[0] = 1; lastFacingDirection[1] = 0;
                        LandscapeType forwardTile = landscape.tilesMap[landscapeRow + 1][landscapeCol].tileType;
                        if (forwardTile == LandscapeType.grass || forwardTile == LandscapeType.dirt || bounds.Y + bounds.Height <
                                (landscapeRow * landscape.pixelHeightPerTile) - landscape.pixelHeightPerTile - pixelSpeed) {
                            bounds.Y += pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.D) {
                        lastFacingDirection[0] = 0; lastFacingDirection[1] = 1;
                        LandscapeType forwardTile = landscape.tilesMap[landscapeRow][landscapeCol + 1].tileType;
                        if (forwardTile == LandscapeType.grass || forwardTile == LandscapeType.dirt || bounds.X + bounds.Width <
                                (landscapeCol * landscape.pixelWidthPerTile) - landscape.pixelWidthPerTile - pixelSpeed) {
                            bounds.X += pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.Space) {
                        digHeld = true;
                        attackBounds.Width = landscape.pixelWidthPerTile;
                        attackBounds.Height = landscape.pixelHeightPerTile * 2;
                        attackBounds.Y = bounds.Y;
                        attackBounds.X = bounds.X + (bounds.Width / 2 - attackBounds.Width / 2);
                        if (lastFacingDirection[1] > 0) { attackBounds.X = bounds.X + bounds.Width; } else if (lastFacingDirection[1] < 0) { attackBounds.X = bounds.X - attackBounds.Width; }
                        if (timeDigging >= digCooldown) {
                            if (landscape.tilesMap[landscapeRow + lastFacingDirection[0]]
                                [landscapeCol + lastFacingDirection[1]].tileType != LandscapeType.bedrock) {
                                landscape.tilesMap[landscapeRow + lastFacingDirection[0]]
                                                [landscapeCol + lastFacingDirection[1]].tileType = LandscapeType.dirt;
                                timeDigging = 0;
                            }
                        } else {
                            digHeld = true;
                            timeDigging += 0.1f;
                        }
                    }
                }//end inputs
                if (!digHeld) {
                    timeDigging = 0;
                    attackBounds.Width = 0;
                    attackBounds.Height = 0;
                }
            }
        }
        #endregion

        #region R E N D E R I N G
        /// <summary>
        /// The KeyPlayer's render method is in charge of drawing the character and its attack to the screen in the right order.
        /// </summary>
        /// <param name="inGraphics">The graphics given to the GameObject by the form</param>
        /// <param name="viewPort">The rectangle representing the current screen size</param>
        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            if (lastFacingDirection[0] == -1) {
                inGraphics.FillEllipse(diggerBrush, attackBounds);
                inGraphics.DrawImage(image, imageBounds);
            } else {
                inGraphics.DrawImage(image, imageBounds);
                inGraphics.FillEllipse(diggerBrush, attackBounds);
            }
        }

        public RenderLayer GetRenderLayer() {
            return RenderLayer.entity;
        }
        #endregion
    }//E N D class
}
