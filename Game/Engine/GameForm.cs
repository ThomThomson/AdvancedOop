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

        public String message = "";

        public GameForm(Managers.InputManager inInputManager) {
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
            Timer.Start();
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
            if (message != "")
            {
                Font drawFont = new Font("Impact", 50);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                PointF drawPoint = new PointF(150.0F, 250.0F);

                // Draw string to screen.
                paintArgs.Graphics.DrawString(message, drawFont, drawBrush, drawPoint);
            }
        }

        private void Resized(object sender, EventArgs e) {
            Renderer.screenSizeChanged(this.DisplayRectangle);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e) {
            if (Timer.Enabled){
                Inputs.addKey(e);
            }else if(e.KeyCode == Keys.Enter){
                State.RestartGame(Inputs, State);
                Timer.Enabled = true;
            }
        }
        private void GameForm_KeyUp(object sender, KeyEventArgs e) { Inputs.removeKey(e); }

        private void GameForm_MouseMove(object sender, MouseEventArgs e) { Inputs.changeMouseCoords(e); }

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
