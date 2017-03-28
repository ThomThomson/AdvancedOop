using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;

namespace Engine.GameObjects.ObjectTypes {
    class BallManager : IGameObject {
        int ballCount;
        public int originalBallCount;
        Rectangle explosion;
        int explodeThreshold = 3;
        int solidifyThreshold = 3;

        InputManager inputManager;
        StateManager stateManager;
        Landscape landscape;
        KeyPlayer player;
        private Random rand;
        List<Ball> balls;
        Image explosionImage;

        public BallManager(InputManager inInputManager, StateManager inStateManager, Landscape inLandscape, int inStartingBalls) {
            ballCount = inStartingBalls;
            stateManager = inStateManager;
            inputManager = inInputManager;
            landscape = inLandscape;
            explosionImage = Image.FromFile("../../images/Explosion.png");
            rand = new Random();
        }
        public void setPlayer(KeyPlayer inPlayer) { player = inPlayer; }

        public RenderLayer GetRenderLayer() { return RenderLayer.entity; }

        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            int i = 0;
            while (i < balls.Count()) {
                bool deletedThisBall = false;
                Color currentBallColor = Color.FromArgb(255, 255, 255);
                if (balls[i].exploding > explodeThreshold) {
                    deletedThisBall = true;
                } else if (balls[i].exploding > 0) {
                    balls[i].bounds.Width *= 5;
                    balls[i].bounds.Height *= 5;
                    explosion = new Rectangle(balls[i].bounds.X - balls[i].bounds.Width / 2,
                        balls[i].bounds.Y - balls[i].bounds.Height / 2, balls[i].bounds.Width, balls[i].bounds.Height);
                    inGraphics.DrawImage(explosionImage, explosion);
                } else if(balls[i].solidifying > solidifyThreshold) {
                    deletedThisBall = true;
                } else if(balls[i].solidifying > 0) {
                    currentBallColor = Color.FromArgb(0, 0, 0);
                    Brush currentBallBrush = new SolidBrush(currentBallColor);
                    balls[i].bounds.Width /= 2;
                    balls[i].bounds.Height /= 2;
                    inGraphics.FillEllipse(currentBallBrush, balls[i].bounds);
                } else {
                    Brush currentBallBrush = new SolidBrush(currentBallColor);
                    inGraphics.FillEllipse(currentBallBrush, balls[i].bounds);
                }
                if (!player.invincible && balls[i].bounds.IntersectsWith(player.bounds)) {
                    player.invincible = true;
                    player.health -= 1;
                    if(player.health <= 0){
                        stateManager.ballVictory(inputManager, stateManager);
                    }else {
                        player.image = Image.FromFile(String.Format("../../images/charHealth-{0}.png", player.health));
                    }
                }

                if (deletedThisBall) { balls.Remove(balls[i]); } 
                else { i++; }
            }
        }

        public void Start() {
            balls = new List<Ball>();
            for(int ball = 0; ball < ballCount; ball++) {
                while (true) {
                    int row = rand.Next(3, landscape.landscapeHeight - 3);
                    int col = rand.Next(3, landscape.landscapeWidth - 3);
                    if (landscape.tilesMap[row][col].tileType == LandscapeType.rock) {
                        float headingX = rand.Next(2, 10);
                        float headingY = rand.Next(2, 10);
                        int xMult = (rand.Next(2) == 0) ? -1 : 1;
                        int yMult = (rand.Next(2) == 0) ? -1 : 1;
                        balls.Add(new Ball(new Rectangle(-100, -100, -1, -1), headingX * xMult, headingY * yMult, row, col));
                        break;
                    }
                }
            }
        }

        public void Tick() {
            foreach (Ball ball in balls) {
                ball.bounceTime++;
                ball.bounds.Width = landscape.pixelWidthPerTile;
                ball.bounds.Height = landscape.pixelWidthPerTile;

                if (ball.exploding == explodeThreshold) {
                    if ((ball.landscapeCol > 2 && ball.landscapeCol < landscape.landscapeWidth - 2) &&
                    (ball.landscapeRow > 2 && ball.landscapeRow < landscape.landscapeHeight - 2)) {
                        for (int row = -2; row < 3; row++) {
                            for (int col = -1; col < 2; col++) {
                                landscape.tilesMap[ball.landscapeRow + row][ball.landscapeCol + col].tileType = LandscapeType.dirt;
                            }
                        }
                    }
                    ball.exploding++;
                } else if (ball.exploding > 0) {
                    ball.exploding++;
                }

                if (ball.solidifying == solidifyThreshold) {
                    if ((ball.landscapeCol > 2 && ball.landscapeCol < landscape.landscapeWidth - 2) &&
                    (ball.landscapeRow > 2 && ball.landscapeRow < landscape.landscapeHeight - 2)) {
                        for (int row = -2; row < 3; row++) {
                            for (int col = -1; col < 2; col++) {
                                landscape.tilesMap[ball.landscapeRow + row][ball.landscapeCol + col].tileType = LandscapeType.rock;
                            }
                        }
                    }
                    ball.solidifying++;
                } else if (ball.solidifying > 0) {
                    ball.solidifying++;
                }

                if (ball.bounds.X < 0 && ball.bounds.Y < 0 && landscape.pixelHeightPerTile > 0) {
                    ball.bounds.X = (ball.landscapeCol * landscape.pixelWidthPerTile);
                    ball.bounds.Y = (ball.landscapeRow * landscape.pixelHeightPerTile);
                }else {
                    if(ball.bounceTime > ball.bounceCooldown){
                        if (ball.bounds.X <= 0 || ball.bounds.Right >= landscape.landscapeWidth * landscape.pixelWidthPerTile){
                            ball.headingDirection[0] *= -1;
                            ball.bounceTime = 0;
                        }
                        if (ball.bounds.Y <= 0 || ball.bounds.Bottom >= landscape.landscapeHeight * landscape.pixelHeightPerTile){
                            ball.headingDirection[1] *= -1;
                            ball.bounceTime = 0;
                        }
                    }
                    ball.bounds.X = (int)(ball.bounds.X + ball.headingDirection[0]);
                    ball.bounds.Y = (int)(ball.bounds.Y + ball.headingDirection[1]);
                    //mouse influence
                    //ball.bounds.X = (int)(ball.bounds.X + (inputManager.mouseCoords[0] - ball.bounds.X) * 0.008);
                    //ball.bounds.Y = (int)(ball.bounds.Y + (inputManager.mouseCoords[1] - ball.bounds.Y) * 0.008);
                }
                if(landscape.pixelWidthPerTile != 0 && landscape.pixelHeightPerTile != 0){
                    ball.landscapeCol = (ball.bounds.X + (ball.bounds.Width / 2)) / landscape.pixelWidthPerTile;
                    ball.landscapeRow = (ball.bounds.Y + (ball.bounds.Height / 2)) / landscape.pixelHeightPerTile;                        
                }
                if((inputManager.mouseCoords[0] > ball.bounds.X && inputManager.mouseCoords[0] < ball.bounds.X + ball.bounds.Width) &&
                   (inputManager.mouseCoords[1] > ball.bounds.Y && inputManager.mouseCoords[1] < ball.bounds.Y + ball.bounds.Height)){
                    if (inputManager.clickDown && ball.exploding == 0 && ball.solidifying == 0) {
                        ball.exploding = 1;
                    }else if (inputManager.rightClickDown && ball.exploding == 0 && ball.solidifying == 0) {
                        ball.solidifying = 1;
                    }
                }
            }
        }
    }
}
