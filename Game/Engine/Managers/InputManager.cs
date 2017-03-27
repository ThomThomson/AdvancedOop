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

        public void changeMouseCoords(MouseEventArgs e) {
            mouseCoords[0] = e.X;
            mouseCoords[1] = e.Y;
        }

        public void addKey(KeyEventArgs e) {
            if (keysHeld.Find(r => r.KeyCode == e.KeyCode) == null) {
                keysHeld.Add(e);
            }
        }

        public void removeKey(KeyEventArgs e) {
            KeyEventArgs keyToRemove = keysHeld.SingleOrDefault(r => r.KeyCode == e.KeyCode);
            if (keyToRemove != null) { keysHeld.Remove(keyToRemove); }
        }

        public void printKeys() {
            String keysHeldStr = "";
            foreach(KeyEventArgs key in keysHeld) {
                keysHeldStr += key.KeyCode.ToString();
            }
            Console.WriteLine("KEYS: " + keysHeldStr);
        }
    }
}
