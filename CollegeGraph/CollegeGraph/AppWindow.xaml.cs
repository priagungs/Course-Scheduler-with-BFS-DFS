using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GraphView;
using GraphView;

namespace CollegeGraph
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        string filename;
        public AppWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filename = Filepath.Text;
                Graph graph = new Graph(filename);
                graph.DFSSolution();
                GView graphView = new GView();
                graphView.Owner = this;
                graphView.Show();
            }
            catch (FileNotFoundException Ex)
            {
                fnfound.IsOpen = true;
            }
        }

        bool hasBeenClicked = false;

        private void TextBox_Focus(object sender, RoutedEventArgs e)
        {
            if (!hasBeenClicked)
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                hasBeenClicked = true;
            }
        }

        private void Filepath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    class Graph
    {
        static private string[] graphEl;
        static private bool[,] graph; //baris ditunjuk, kolom menunjuk
        private int[] solution;
        private int[] nodeDestroyed; //for timestamp

        public static string[] getKodeKuliah {
            get { return graphEl; }
        }

        public static bool[,] getAdjMx
        {
            get { return graph; }
        }

        public Graph(String filename)
        {
            readFromFile(filename);
            solution = new int[graphEl.Length];
            nodeDestroyed = new int[graphEl.Length];
        }

        //Method yang digunakan untuk membaca dan parsing file *.txt
        private void readFromFile(String filename)
        {
            //VARIABEL LOKAL
            int i = 0;
            char[] delimiterChars = { ' ', ',', '.' };
            //chae untuk split kode/nama kuliah
            string[] lines = File.ReadAllLines(@filename);
            string[][] kode = new string[lines.Length][];

            //INSTANTIASI VARIABEL KELAS GRAPH
            graphEl = new string[lines.Length];

            //Parse kode kuliah(tidak boleh ada spasi)
            for (i = 0; i < lines.Length; i++)
            {
                kode[i] = lines[i].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                graphEl[i] = kode[i][0];
            }

            //instantiasi variabel kelas graph, Matriks Ketetanggaan
            graph = new bool[kode.Length, kode.Length];
            i = 0;
            foreach (var k1 in kode)//untuk setiap baris
            {
                foreach (var k2 in k1)//untuk setiap kode kuliah 
                {
                    //centang kode yang ada pada list kode dan tidak yang sama
                    int j = Array.IndexOf(graphEl, k2);
                    if (j >= 0 && i != j)
                    {
                        graph[j, i] = true;
                    }
                }
                i += 1;
            }

            //print adjacency matrix ke cmd
            for (i = 0; i < graphEl.Length; i++)
            {
                for (int j = 0; j < graphEl.Length; j++)
                {
                    if (graph[i, j])
                    {
                        Console.Write("1 ");
                    }
                    else
                    {
                        Console.Write("0 ");
                    }
                }
                Console.WriteLine(" ");
            }
        }

        public void DFSSolution()
        {
            //find first node
            int firstNode = -1;
            for (int row = 0; row < graphEl.Length; row++)
            {
                bool found = true;
                for (int col = 0; col < graphEl.Length; col++)
                {
                    if (graph[row, col])
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    firstNode = row;
                    break;
                }
            }

            int count = 0;
            if (firstNode != -1)
            {
                DFS(firstNode, ref count);
            }

            //find adjacentless nodes 
            for (int i = 0; i < graphEl.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < graphEl.Length; j++)
                {
                    if (graph[i, j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    for (int j = 0; j < graphEl.Length; j++)
                    {
                        if (graph[j, i])
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    count++;
                    nodeDestroyed[i] = count;
                }
            }

            int index = 0;
            while (nodeDestroyed.Length != 0)
            {
                int maxval = nodeDestroyed.Max();
                int max_el_idx = Array.IndexOf(nodeDestroyed, maxval);
                solution[index] = max_el_idx;
                nodeDestroyed = nodeDestroyed.Where(val => val != maxval).ToArray();
                index++;
            }

            for (int i = 0; i < graphEl.Length; i++)
            {
                Console.WriteLine(graphEl[solution[i]] + ' ');
            }
            //Console.WriteLine(nodeDestroyed);

        }

        private void DFS(int node, ref int counter)
        {
            counter++;
            for (int branch = 0; branch < graphEl.Length; branch++)
            {
                if (graph[branch, node])
                {
                    DFS(branch, ref counter);
                }
            }
            counter++;
            nodeDestroyed[node] = counter;
        }

        public void BFS()
        {

        }

        public void Visualize()
        {

        }
    }
}
