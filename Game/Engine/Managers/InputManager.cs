using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Managers {
    public class InputManager {
        public List<KeyEventArgs> keysHeld { get; set; }
        public int[] mouseCoords;

        public bool clickDown;
        public bool rightClickDown;

        public InputManager() {
            keysHeld = new List<KeyEventArgs>();
            mouseCoords = new int[2];
        }

        /// <summary>
        /// changes the stored mouse position
        /// </summary>
        /// <param name="inArgs">The mouseEventArgs containing the position of the mouse</param>
        public void changeMouseCoords(MouseEventArgs inArgs) {
            mouseCoords[0] = inArgs.X;
            mouseCoords[1] = inArgs.Y;
        }
    
        /// <summary>
        /// manages adding keys to the list of currently held keys
        /// </summary>
        /// <param name="inArgs">The arguments containing which keycode was pressed</param>
        public void addKey(KeyEventArgs inArgs) {
            if (keysHeld.Find(r => r.KeyCode == inArgs.KeyCode) == null) {
                keysHeld.Add(inArgs);
            }
        }

        /// <summary>
        /// manages removing keys from the list of currently held keys
        /// </summary>
        /// <param name="inArgs">The arguments containing which keycode was pressed</param>
        public void removeKey(KeyEventArgs inArgs) {
            KeyEventArgs keyToRemove = keysHeld.SingleOrDefault(r => r.KeyCode == inArgs.KeyCode);
            if (keyToRemove != null) { keysHeld.Remove(keyToRemove); }
        }
    }
}
