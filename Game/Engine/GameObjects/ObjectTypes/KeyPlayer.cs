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
        private int gracePeriodTime = 100;
        private int graceCounter = 0;

        public int health;
        public int startingHealth;

        public bool invincible;
        public Rectangle bounds;
        public Rectangle attackBounds;
        int landscapeRow;
        int landscapeCol;
        int pixelOffsetRow = 0;
        int pixelOffsetCol = 0;
        int[] lastFacingDirection = new int[2];

        float timeDigging = 0;
        float digCooldown = 3f;
        bool digHeld = false;
        bool digColorSwitch = false;
        bool invincibleColorSwitch = false;
        public Color playerColor;

        BallManager enemies;
        RenderLayer renderLayer = RenderLayer.entity;
        InputManager inputManager;
        StateManager stateManager;
        Landscape landscape;
        Random rand;
        Color invincibleColor;
        Brush playerBrush;
        Brush diggerBrush1;
        Brush diggerBrush2;

        public KeyPlayer(Managers.InputManager inInputManager, StateManager stateManager, Landscape inLandscape, int inHealth)  {
            //invincible = true;
            health = inHealth;
            startingHealth = inHealth;
            inputManager = inInputManager;
            this.stateManager = stateManager;
            landscape = inLandscape;
            rand = new Random();
            invincibleColor = Color.Aqua;

            playerColor = Color.FromArgb(255 - ((255 / startingHealth) * health), 0, (255 / startingHealth) * health);

            playerBrush = new SolidBrush(playerColor);
            Color diggerColor = Color.FromArgb(255, 138, 0);
            diggerBrush1 = new SolidBrush(diggerColor);
            diggerBrush2 = new SolidBrush(Color.White);
        }

        public RenderLayer GetRenderLayer() { return renderLayer; }

        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            if (invincible && invincibleColorSwitch) {
                playerBrush = new SolidBrush(invincibleColor);
                invincibleColorSwitch = false;
            }
            else {
                playerBrush = new SolidBrush(playerColor);
                invincibleColorSwitch = true;
            }
            if(lastFacingDirection[0] == -1) {
                if (digColorSwitch) {inGraphics.FillEllipse(diggerBrush1, attackBounds); digColorSwitch = false; } 
                else { inGraphics.FillEllipse(diggerBrush2, attackBounds); digColorSwitch = true; }
                inGraphics.FillRectangle(playerBrush, bounds);
            }else {
                inGraphics.FillRectangle(playerBrush, bounds);
                if (digColorSwitch) { inGraphics.FillEllipse(diggerBrush1, attackBounds); digColorSwitch = false; } 
                else { inGraphics.FillEllipse(diggerBrush2, attackBounds); digColorSwitch = true; }
            }
        }

        public void Start() {
            //randomly place 
            while (true) {
                int row = rand.Next(0, landscape.landscapeHeight);
                int col = rand.Next(0, landscape.landscapeWidth);
                if (landscape.tilesMap[row][col].tileType != LandscapeType.rock) {
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
            bounds.Width = landscape.pixelWidthPerTile / 2;
            bounds.Height = landscape.pixelHeightPerTile * 2;
            int pixelSpeed = landscape.pixelWidthPerTile / 5;
            if(landscapeRow > 1 && landscapeRow < landscape.landscapeHeight - 1 && 
               landscapeCol > 1 && landscapeCol < landscape.landscapeWidth - 1) {
                foreach (KeyEventArgs key in inputManager.keysHeld) {
                    if (key.KeyCode == Keys.W) {
                        lastFacingDirection[0] = -1; lastFacingDirection[1] = 0;
                        if (pixelOffsetRow > -bounds.Height) { pixelOffsetRow -= pixelSpeed; } 
                        else if (landscape.tilesMap[landscapeRow - 1][landscapeCol].tileType != LandscapeType.rock) {
                            landscapeRow--;
                            pixelOffsetRow = landscape.pixelHeightPerTile - bounds.Height - pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.A) {
                        lastFacingDirection[0] = 0; lastFacingDirection[1] = -1;
                        if (pixelOffsetCol > 0) { pixelOffsetCol -= pixelSpeed; } 
                        else if (landscape.tilesMap[landscapeRow][landscapeCol - 1].tileType != LandscapeType.rock) {
                            landscapeCol--;
                            pixelOffsetCol = landscape.pixelWidthPerTile - pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.S) {
                        lastFacingDirection[0] = 1; lastFacingDirection[1] = 0;
                        if (pixelOffsetRow < landscape.pixelHeightPerTile - bounds.Height) { pixelOffsetRow += pixelSpeed; } 
                        else if (landscape.tilesMap[landscapeRow + 1][landscapeCol].tileType != LandscapeType.rock) {
                            landscapeRow++;
                            pixelOffsetRow = -bounds.Height + pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.D) {
                        lastFacingDirection[0] = 0; lastFacingDirection[1] = 1;
                        if (pixelOffsetCol < landscape.pixelWidthPerTile - bounds.Width) { pixelOffsetCol += pixelSpeed; } 
                        else if (landscape.tilesMap[landscapeRow][landscapeCol + 1].tileType != LandscapeType.rock) {
                            landscapeCol++;
                            pixelOffsetCol = -bounds.Width + pixelSpeed;
                        }
                    }
                    if (key.KeyCode == Keys.Space) {
                        digHeld = true;
                        attackBounds.Width = landscape.pixelWidthPerTile * 2;
                        attackBounds.Height = landscape.pixelHeightPerTile * 2;
                        attackBounds.Y = bounds.Y;
                        attackBounds.X = bounds.X + (bounds.Width / 2 - attackBounds.Width / 2);
                        if (lastFacingDirection[1] > 0) { attackBounds.X = bounds.X + bounds.Width;  }
                        else if(lastFacingDirection[1] < 0) { attackBounds.X = bounds.X - attackBounds.Width; }
                        if (timeDigging >= digCooldown) {
                            landscape.tilesMap[landscapeRow + lastFacingDirection[0]]
                                              [landscapeCol + lastFacingDirection[1]].tileType = LandscapeType.dirt; 
                            timeDigging = 0;
                        }else {
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
            }else {
                stateManager.keyboardVictory(inputManager, stateManager);
            }
            bounds.X = landscapeCol * landscape.pixelWidthPerTile + pixelOffsetCol;
            bounds.Y = landscapeRow * landscape.pixelHeightPerTile + pixelOffsetRow;
        }
    }
}
