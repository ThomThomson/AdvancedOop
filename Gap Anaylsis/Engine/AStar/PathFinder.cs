using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engine.GameObjects.ObjectTypes;

namespace Engine.AStar {
    class PathFinder {
        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;
        private SearchParameters searchParameters;

        public PathFinder(SearchParameters searchParameters) {
            this.searchParameters = searchParameters;
            InitializeNodes(searchParameters.Map);
            this.startNode = this.nodes[searchParameters.CharLocation.X, searchParameters.CharLocation.Y];
            this.startNode.State = NodeState.Open;
            this.endNode = this.nodes[searchParameters.GoalLocation.X, searchParameters.GoalLocation.Y];
        }

        public List<Point> FindPath() {
            List<Point> path = new List<Point>();
            bool success = Search(startNode);
            if (success) {
                Node node = this.endNode;
                while (node.ParentNode != null) {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }

            return path;
        }

        private void InitializeNodes(Landscape inLandscape) {
            this.width = inLandscape.landscapeWidth;
            this.height = inLandscape.landscapeHeight;
            this.nodes = new Node[this.width, this.height];
            for (int y = 0; y < this.height; y++) {
                for (int x = 0; x < this.width; x++) {
                    this.nodes[x, y] = new Node(x, y, inLandscape.tilesMap[y][x].tileType != LandscapeType.rock
                        , this.searchParameters.GoalLocation);
                }
            }
        }

        private bool Search(Node currentNode) {
            currentNode.State = NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes) {
                if (nextNode.Location == this.endNode.Location) {
                    return true;
                } else {
                    if (Search(nextNode))
                        return true;
                }
            }

            return false;
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode) {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations) {
                int x = location.X;
                int y = location.Y;
                if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                    continue;

                Node node = this.nodes[x, y];
                if (!node.IsWalkable)
                    continue;

                if (node.State == NodeState.Closed)
                    continue;

                if (node.State == NodeState.Open) {
                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G) {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                } else {
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }
            return walkableNodes;
        }

        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation) {
            return new Point[]
            {
                new Point(fromLocation.X-1, fromLocation.Y-1),
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X-1, fromLocation.Y+1),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X+1, fromLocation.Y-1),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
