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
        Managers.StateManager State;
        Managers.RenderManager Renderer;
        Managers.InputManager Inputs;

        public GameForm(Managers.StateManager inStateManager, Managers.InputManager inInputManager) {
            InitializeComponent();
            State = inStateManager;
            Inputs = inInputManager;
            Renderer = new Managers.RenderManager(State, this.DisplayRectangle);
        }

        /* U s e  O n L o a d  to define all  O b j e c t s  existing at the start of the game*/
        private void OnLoad(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
            Renderer.screenSizeChanged(this.DisplayRectangle);
            /*Gameplay Starts here*/
            State.StartAll();
            Timer.Start();
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
        }

        private void Resized(object sender, EventArgs e) {
            Renderer.screenSizeChanged(this.DisplayRectangle);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e) { Inputs.addKey(e); }
        private void GameForm_KeyUp(object sender, KeyEventArgs e) { Inputs.removeKey(e); }

        private void GameForm_MouseMove(object sender, MouseEventArgs e) { Inputs.changeMouseCoords(e); }

        private void GameForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Inputs.clickDown = true;
            } else if (e.Button == MouseButtons.Right) {
                Inputs.rightClickDown = true;
            }
        }

        private void GameForm_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Inputs.clickDown = false;
            } else if (e.Button == MouseButtons.Right) {
                Inputs.rightClickDown = false;
            }
        }
    }
}
