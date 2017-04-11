using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.AStar {
    public class SearchParameters {
        public Point CharLocation { get; set; }

        public Point GoalLocation { get; set; }

        public GameObjects.ObjectTypes.Landscape Map { get; set; }

        public SearchParameters(Point charLocation, Point goalLocation, GameObjects.ObjectTypes.Landscape map) {
            this.CharLocation = charLocation;
            this.GoalLocation = goalLocation;
            this.Map = map;
        }
    }
}
