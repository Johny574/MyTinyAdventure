using System;
using System.Collections.Generic;
using System.Linq;

public class LocationManager : Singleton<LocationManager>
{
    public Dictionary<string, Node> Nodes = new();

    protected override void Awake()
    {
        base.Awake();
        Nodes.Add("GnurksCave", new Node("GnurksCave"));
        
        Nodes.Add("Shop", new Node("Shop"));

        Nodes.Add("Forest1-1", new Node("Forest1-1"));
        Nodes.Add("Forest1-2", new Node("Forest1-2"));
        Nodes.Add("Forest1-3", new Node("Forest1-3"));
        Nodes.Add("Forest1-4", new Node("Forest1-4"));
        Nodes.Add("Forest1-5", new Node("Forest1-5"));
        Nodes.Add("Desert2-1", new Node("Desert2-1"));
        Nodes.Add("Desert2-2", new Node("Desert2-2"));
        Nodes.Add("Desert2-3", new Node("Desert2-3"));
        Nodes.Add("Desert2-4", new Node("Desert2-4"));
        Nodes.Add("Desert2-5", new Node("Desert2-5"));
        Nodes.Add("Swamp3-1", new Node("Swamp3-1"));
        Nodes.Add("Swamp3-2", new Node("Swamp3-2"));
        Nodes.Add("Swamp3-3", new Node("Swamp3-3"));
        Nodes.Add("Swamp3-4", new Node("Swamp3-4"));
        Nodes.Add("Swamp3-5", new Node("Swamp3-5"));
        Nodes.Add("Tundra4-1", new Node("Tundra4-1"));
        Nodes.Add("Tundra4-2", new Node("Tundra4-2"));
        Nodes.Add("Tundra4-3", new Node("Tundra4-3"));
        Nodes.Add("Tundra4-4", new Node("Tundra4-4"));
        Nodes.Add("Tundra4-5", new Node("Tundra4-5"));


        Nodes["GnurksCave"].ConnectNode(Nodes["Forest1-1"]);
        Nodes["Shop"].ConnectNode(Nodes["Forest1-1"]);
    }

    public class Node
    {
        public string ID;
        public List<Node> ConnectedNodes;

        public Node(string iD)
        {
            ID = iD;
            ConnectedNodes = new();
        }

        public void ConnectNode(Node node) => ConnectedNodes.Add(node);
    }


    public List<string> BFS(string startID, string targetID)
    {
        Node start = Instance.Nodes[startID];
        Node target = Instance.Nodes[targetID];
        var visited = new HashSet<Node>();
        var queue = new Queue<Node>();
        var parent = new Dictionary<Node, Node?>();

        visited.Add(start);
        queue.Enqueue(start);
        parent[start] = null;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == target)
            {
                // Reconstruct path from target to start
                var path = new List<string>();
                while (current != null)
                {
                    path.Add(current.ID);
                    current = parent[current];
                }
                path.Reverse();
                return path;
            }

            foreach (var neighbor in current.ConnectedNodes)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return new List<string>();
    }       

    static string[] _towns = new string[1] {
        "Forest1-1",
    };
}