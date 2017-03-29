using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine {
    public partial class GameForm : Form {
        public Managers.StateManager State;
        Managers.RenderManager Renderer;
        public Managers.InputManager Inputs;

        public string message = "";
        public string failureMessage = "You hecked up. Press enter to restart game";
        public string startupMessage = "P A T H B U I L D E R";
        public string startupSubMessage = "press enter to start \nPlayer 1 uses the Keyboard. Get to the blue square\n" +
                                          "\tControls:\n" +
                                          "\tWASD: movement\n" +
                                          "\tSPACE: hold for one second to dig stone\n\n" +
                                          "Player 2 uses the Mouse. Kill player 1\n" +
                                          "\tControls:\n" +
                                          "\tLeft click on a ball for an explosion\n" +
                                          "\tRight click on a ball to spawn bedrock\n";

        public string subMessage = "";

        public GameForm(Managers.InputManager inInputManager) {
            message = startupMessage;
            subMessage = startupSubMessage;
            State = new Managers.StateManager(this);
            InitializeComponent();
            Inputs = inInputManager;
            Renderer = new Managers.RenderManager(State, this.DisplayRectangle);
        }

        private void OnLoad(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
            Renderer.screenSizeChanged(this.DisplayRectangle);
            /*Gameplay Starts here*/
            State.StartAll();
            //Timer.Start();
            State.setTimer(Timer);
        }

        private void OnTick(object sender, EventArgs e) {
            if (!State.victoryConditionAchieved) {
                State.TickAll();
                Invalidate();
            } else {
                Application.Exit();
            }
        }

        private void onPaint(object sender, PaintEventArgs paintArgs) {
            Renderer.RenderAll(paintArgs.Graphics);
            if (message != ""){
                paintArgs.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), this.DisplayRectangle);
                Font drawFont = new Font("Impact", 50);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                PointF drawPoint = new PointF(50.0F, 30.0F);

                // Draw string to screen.
                paintArgs.Graphics.DrawString(message, drawFont, drawBrush, drawPoint);
                if(subMessage != "") {
                    PointF subPoint = new PointF(50.0F, 150.0F);
                    Font subFont = new Font("Impact", 20);
                    paintArgs.Graphics.DrawString(subMessage, subFont, drawBrush, subPoint);
                }
            }
        }

        private void Resized(object sender, EventArgs e) {
            Renderer.screenSizeChanged(this.DisplayRectangle);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                State.showControls();
                Timer.Enabled = false;
                Invalidate();
                //State.RestartGame(Inputs);
            } else if (Timer.Enabled){
                Inputs.addKey(e);
            }else if (e.KeyCode == Keys.Enter){
                if(message == failureMessage) { 
                    State.showControls();
                    Invalidate();
                }else {
                    State.RestartGame(Inputs);
                    Timer.Enabled = true;
                }
            }
        }
        private void GameForm_KeyUp(object sender, KeyEventArgs e) {
            Inputs.removeKey(e);
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e) {
            Inputs.changeMouseCoords(e);
        }

        private void GameForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Inputs.clickDown = true;
            } else if (e.Button == MouseButtons.Right) {
                Inputs.rightClickDown = true;
            }
        }

        private void GameForm_MouseUp(object sender, MouseEventArgs e){
            if (e.Button == MouseButtons.Left){
                Inputs.clickDown = false;
            }
            else if (e.Button == MouseButtons.Right){
                Inputs.rightClickDown = false;
            }
        }
    }
}
