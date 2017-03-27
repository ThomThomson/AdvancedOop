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

        public GameForm(Managers.StateManager inStateManager) {
            InitializeComponent();
            State = inStateManager;
            Renderer = new Managers.RenderManager(State, this.DisplayRectangle);
        }

        /* U s e  O n L o a d  to define all  O b j e c t s  existing at the start of the game*/
        private void OnLoad(object sender, EventArgs e) {
            GameObjects.ObjectTypes.Landscape landscape = new GameObjects.ObjectTypes.Landscape(50, 50);
            State.GameObjectList.Add(landscape);


            /*Gameplay Starts here*/
            State.StartAll();
            Timer.Start();
        }

        private void OnTick(object sender, EventArgs e) {
            State.TickAll();
            Invalidate();
        }

        private void onPaint(object sender, PaintEventArgs paintArgs) {
            Renderer.RenderAll(paintArgs.Graphics);
        }

        private void Resized(object sender, EventArgs e) {
            Renderer.screenSizeChanged(this.DisplayRectangle);
        }
    }
}
