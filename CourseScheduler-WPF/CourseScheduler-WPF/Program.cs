using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseScheduler
{
    class Graph
    {
        public String[] graphEl;
        private bool[,] graph; //baris menunjuk, kolom ditunjuk
        private bool[] flag;
        public int[] solution;
        private int[] nodeDestroyed; //for timestamp
        private int counter;

        public Graph(String filename)
        {
            readFromFile(filename);
            solution = new int[graphEl.Length];
            nodeDestroyed = new int[graphEl.Length];
            flag = new bool[graphEl.Length];
            counter = 0;
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
                foreach(var k2 in k1)//untuk setiap kode kuliah 
                {
                    //centang kode yang ada pada list kode dan tidak yang sama
                    int j = Array.IndexOf(graphEl,k2);
                    if (j >= 0 && i != j)
                    {
                        graph[j, i] = true;
                    }
                }
                i += 1;
            }

            //print adjacency matrix ke cmd
            //for debugging purpose
            /*
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
            */
        }


        public void DFSSolution()
        {
            //find first node
            int firstNode = -1;

            //for debugging purpose
            /*
            for (int i=0; i<graphEl.Length; i++)
            {
                Console.WriteLine(graphEl[i] + ' ' + i);
            }
            */

            for(int col = 0; col < graphEl.Length; col++)
            {
                bool found = true;
                for(int row = 0; row < graphEl.Length; row++)
                {
                    if (graph[row,col])
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    firstNode = col;
                    break;
                }
            }

            //for debugging purpose
            // Console.WriteLine("First node : " + firstNode);

            counter = 0;
            if (firstNode != -1)
            {
                DFS(firstNode);
            }

            //find adjacentless nodes 
            for(int i=0; i<graphEl.Length; i++)
            {
                bool found = false;
                for(int j=0; j<graphEl.Length; j++)
                {
                    if(graph[i,j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    for(int j=0; j<graphEl.Length; j++)
                    {
                        if (graph[j,i])
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    counter++;
                    nodeDestroyed[i] = counter;
                }
            }

            // for debugging purpose
            /* Console.WriteLine("Node Destroyed : ");
            for(int i=0; i<nodeDestroyed.Length; i++)
            {
                Console.Write(nodeDestroyed[i] + " ");
            }
            Console.Write('\n');
            */

            Dictionary<int, int> nodeTimeStamp = new Dictionary<int, int>();
            for(int i=0; i<nodeDestroyed.Length; i++)
            {
                nodeTimeStamp.Add(i, nodeDestroyed[i]);
            }
            Array.Sort(nodeDestroyed);
            Array.Reverse(nodeDestroyed);
            for(int i=0; i<nodeDestroyed.Length; i++)
            {
                for(int j=0; j<nodeDestroyed.Length; j++)
                {
                    if(nodeTimeStamp[j] == nodeDestroyed[i])
                    {
                        solution[i] = j;
                    }
                }
            }

            //Console.WriteLine("Solution : ");

            //for debugging purpose
            /*
            for(int i=0; i<solution.Length; i++)
            {
                Console.Write(solution[i] + " ");
            }
            Console.Write('\n');
            */

            /*
            for (int i=0; i<graphEl.Length; i++)
            {
                Console.Write(graphEl[solution[i]] + ' ');
            }
            Console.WriteLine("\nPress any key to terminate..\n");
            Console.ReadKey();
            */
        }

        private void DFS(int node)
        {
            counter++;
            flag[node] = true;
            for (int branch = 0; branch < graphEl.Length; branch++)
            {
                if(graph[node, branch] && !flag[branch])
                {
                    DFS(branch);
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
