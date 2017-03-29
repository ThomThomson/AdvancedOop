using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;

namespace Engine.GameObjects.ObjectTypes {
    class BallManager : IGameObject {
        #region P A R A M S  &  S T A R T U P
        //Game References
        private InputManager inputManager;
        private StateManager stateManager;
        private Landscape landscape;
        private KeyPlayer player;
        //rendering
        private Rectangle explosion;
        private Image explosionImage = Image.FromFile("../../images/Explosion.png");
        //internal objects
        private Random rand = new Random();
        private List<Ball> balls;
        //settings
        private int ballCount;
        private int explodeThreshold = 3;
        private int solidifyThreshold = 3;

        /// <summary>
        /// The Ballmanager's constructor is used to inject required references.
        /// </summary>
        /// <param name="inInputManager">The input manager reference to determine the mouse position etc</param>
        /// <param name="inStateManager">The state manager for declaring victory conditions</param>
        /// <param name="inLandscape">The landscape reference </param>
        /// <param name="inStartingBalls">The number of balls to start with this game</param>
        public BallManager(InputManager inInputManager, StateManager inStateManager, Landscape inLandscape, int inStartingBalls, KeyPlayer inPlayer) {
            ballCount = inStartingBalls;
            player = inPlayer;
            stateManager = inStateManager;
            inputManager = inInputManager;
            landscape = inLandscape;
        }
        /// <summary>
        /// BallManager's Startup method is in charge of randomly spawning a ball on a rock in the landscape 
        /// and determining a random direction for it to travel in
        /// </summary>
        public void Start() {
            balls = new List<Ball>();
            for (int ball = 0; ball < ballCount; ball++) {
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
        #endregion

        #region T I C K
        /// <summary>
        /// The BallManager's tick method simply calls the move method on all the balls
        /// </summary>
        public void Tick() {
            foreach (Ball ball in balls) {
                ball.DoMove(player, landscape, inputManager, stateManager, explodeThreshold);
            }
        }
        #endregion

        #region R E N D E R I N G
        /// <summary>
        /// The ball's renderSelf method renders each ball in the list and 
        /// takes care of destroying any balls that were marked for destruction in the last tick
        /// </summary>
        /// <param name="inGraphics">The graphics given to the GameObject by the form</param>
        /// <param name="viewPort">The rectangle representing the current screen size</param>
        public void RenderSelf(Graphics inGraphics, Rectangle viewPort) {
            int i = 0;//int iterator used instead of for loop due to deletion of 
            while (i < balls.Count()) {//L O O P through list of balls.
                bool deleteBall = false;

                //draw the ball e x p l o d i n g
                if (balls[i].exploding > explodeThreshold) {
                    deleteBall = true;
                } else if (balls[i].exploding > 0) {
                    explosion = new Rectangle(balls[i].bounds.X - balls[i].bounds.Width / 2,
                        balls[i].bounds.Y - balls[i].bounds.Height / 2, balls[i].bounds.Width, balls[i].bounds.Height);
                    inGraphics.DrawImage(explosionImage, explosion);
                }//E N D exploding

                //draw the ball s o l i d i f y i n g
                else if (balls[i].solidifying > solidifyThreshold) {
                    deleteBall = true;
                } else if (balls[i].solidifying > 0) {
                    Brush currentBallBrush = new SolidBrush(Color.Black);
                    balls[i].bounds.Width /= 2;
                    balls[i].bounds.Height /= 2;
                    inGraphics.FillEllipse(currentBallBrush, balls[i].bounds);
                }//E N D solidifying

                else {//draw the ball n o r m a l l y
                    Brush currentBallBrush = new SolidBrush(Color.White);
                    inGraphics.FillEllipse(currentBallBrush, balls[i].bounds);
                }
                //delete the ball if need be
                if (deleteBall) { balls.Remove(balls[i]); } else { i++; }
            }//E N D looping through balls
        }//E N D method RenderSelf

        public RenderLayer GetRenderLayer() {
            return RenderLayer.entity;
        }
        #endregion
    }//E N D class
}
