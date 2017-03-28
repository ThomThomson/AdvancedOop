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
        private int gracePeriodTime = 15;
        private int graceCounter = 0;

        public int health;
        public int startingHealth;

        public bool invincible;
        public Rectangle bounds;
        public Rectangle imageBounds;
        public Rectangle attackBounds;
        int landscapeRow;
        int landscapeCol;
        int pixelOffsetRow = 0;
        int pixelOffsetCol = 0;
        int[] lastFacingDirection = new int[2];

        float timeDigging = 0;
        float digCooldown = 3f;
        bool digHeld = false;
        RenderLayer renderLayer = RenderLayer.entity;
        InputManager inputManager;
        StateManager stateManager;
        Landscape landscape;
        Random rand;

        Brush diggerBrush;
        public Image image;

        public KeyPlayer(Managers.InputManager inInputManager, StateManager stateManager, Landscape inLandscape, int inHealth)  {
            //invincible = true;
            health = inHealth;
            startingHealth = inHealth;
            inputManager = inInputManager;
            this.stateManager = stateManager;
            landscape = inLandscape;
            rand = new Random();
            image = Image.FromFile("../../images/charHealth-4.png");

            Color diggerColor = Color.FromArgb(255, 250, 119);
            diggerBrush = new SolidBrush(diggerColor);
        }

        public RenderLayer GetRenderLayer() { return renderLayer; }

        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            if (lastFacingDirection[0] == -1) {
                inGraphics.FillEllipse(diggerBrush, attackBounds);
                inGraphics.DrawImage(image, imageBounds);
            } else {
                inGraphics.DrawImage(image, imageBounds);
                inGraphics.FillEllipse(diggerBrush, attackBounds);
            }
        }

        public void Start() {
            //randomly place 
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

        public void Tick() {
            if (invincible && graceCounter < gracePeriodTime) {
                graceCounter++;
            }else {
                invincible = false;
                graceCounter = 0;
            }
            digHeld = false;
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

            if (landscape.tilesMap[landscapeRow][landscapeCol - 1].tileType == LandscapeType.goal) {
                stateManager.keyboardVictory(inputManager, stateManager);
            } else {
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
                
            //bounds.X = landscapeCol * landscape.pixelWidthPerTile + pixelOffsetCol;
            //bounds.Y = landscapeRow * landscape.pixelHeightPerTile + pixelOffsetRow;
        }
    }
}
