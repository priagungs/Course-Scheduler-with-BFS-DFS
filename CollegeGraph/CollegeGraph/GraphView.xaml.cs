using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CollegeGraph;
using QuickGraph;

namespace GraphView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GView : Window
    {
        private IBidirectionalGraph<object, IEdge<object>> _g2Visual;

        public IBidirectionalGraph<object, IEdge<object>> g2Visual {
            get { return _g2Visual; }
        }

        public GView()
        {
            CreateGraph();
            InitializeComponent();
        }

        private void CreateGraph() {
            var g = new BidirectionalGraph<object, IEdge<object>>();
            string[] vertices = Graph.getKodeKuliah;
            for(int i = 0; i < vertices.Length; i++)
            {
                g.AddVertex(vertices[i]);
            }
            bool[,] adjmx = Graph.getAdjMx;
            for(int i = 0; i < vertices.Length; i++)
            {
                for(int j = 0; j < vertices.Length; j++)
                {
                    if (adjmx[i, j])
                    {
                        g.AddEdge(new Edge<object>(vertices[i],vertices[j]));
                    }
                }
            }
            _g2Visual = g;
        }
    }
}
