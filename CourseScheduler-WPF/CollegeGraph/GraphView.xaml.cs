using System.Windows;
using QuickGraph;
using GraphSharp;
using System;

namespace CollegeGraph
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GView : Window
    {
        public BidirectionalGraph<object, IEdge<object>> graph;
        private IBidirectionalGraph<object, IEdge<object>> _g2Visual;

        public IBidirectionalGraph<object, IEdge<object>> g2Visual {
            get { return _g2Visual; }
        }

        public GView()
        {
            CreateGraph();
            InitializeComponent();
            Animate();
        }

        private void CreateGraph() {
            graph = new BidirectionalGraph<object, IEdge<object>>();
            _g2Visual = graph;  
            
        }
        public void Animate() {
            string[] vertices = Graph.graphEl;
            bool[,] adjmx = Graph.graph;
            foreach (int element in Graph.visitedNode)
            {
                Console.WriteLine(element);
                graph.AddVertex(vertices[element]);
            }
            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = 0; j < vertices.Length; j++)
                {
                    if (adjmx[i, j])
                    {
                        graph.AddEdge(new Edge<object>(vertices[i], vertices[j]));
                    }
                }
            }
            _g2Visual = graph;


        }
    }
}
