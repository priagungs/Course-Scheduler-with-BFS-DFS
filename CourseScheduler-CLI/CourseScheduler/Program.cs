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
        private String[] graphEl;
        private bool[,] graph; //baris menunjuk, kolom ditunjuk
        private bool[] flag;
        private int[] solution;
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
            //for debugging purpose
            /*
            for (int i=0; i<graphEl.Length; i++)
            {
                Console.WriteLine(graphEl[i] + ' ' + i);
            }
            */

            for (int col = 0; col < graphEl.Length; col++)
            {
                bool found = true;
                for (int row = 0; row < graphEl.Length; row++)
                {
                    if (graph[row, col])
                    {
                        found = false;
                    }
                }
                if (found && nodeDestroyed[col] == 0)
                {
                    DFS(col);
                }
            }

            //for debugging purpose
            // Console.WriteLine("First node : " + firstNode);

            // for debugging purpose
            Console.WriteLine("Node Destroyed : ");
            for(int i=0; i<nodeDestroyed.Length; i++)
            {
                Console.Write(nodeDestroyed[i] + " ");
            }
            Console.Write('\n');
            

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

            Console.WriteLine("Solution : ");

            //for debugging purpose
            /*
            for(int i=0; i<solution.Length; i++)
            {
                Console.Write(solution[i] + " ");
            }
            Console.Write('\n');
            */

            for (int i=0; i<graphEl.Length; i++)
            {
                Console.Write(graphEl[solution[i]] + ' ');
            }
            Console.WriteLine("\nPress any key to terminate..\n");
            Console.ReadKey();
        
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
           derajatMasuk = new int[graphEl.Length];

            //hitung semua derajat-masuk (in-degree) setiap simpul
            for(int col = 0; col < graphEl.Length; col++)
            {
                int count = 0;
                for(int row = 0; row < graphEl.Length; row++)
                {
                    if (graph[row,col])
                    {
                        count++;
                    }
                }
                derajatMasuk[col] = count;
            }

            //for debugging purpose
            /*
            for (int i = 0; i < graphEl.Length; i++)
            {
                Console.Write(graphEl[i] + " ");
            }
            Console.Write('\n');

            for (int i = 0; i < derajatMasuk.Length; i++)
            {
                Console.Write(derajatMasuk[i] + " ");
            }
            Console.Write('\n');
            */
            
            int idxs = -1;
            int idxbawah = -1;
            
            //diulang sampai semua simpul terpilih
            while(idxs < graphEl.Length-1)
            {
                //Pilih simpul yang memiliki derajat-masuk 0(masuk ke array solusi), hilangkan simpul tersebut(derajatMasuk = -1)
                for(int col = 0; col < graphEl.Length; col++)
                {
                    if(derajatMasuk[col] == 0)
                    {
                        idxs++;
                        solution[idxs] = col;
                        derajatMasuk[col] = -1;
                    }
                }

                //for debugging purpose & for path to solution
                for (int i = 0; i < graphEl.Length; i++)
                {
                    Console.Write(graphEl[solution[i]] + " ");
                }
                Console.Write('\n');
                

                //kurangi derajat simpul yang berhubungan dengan simpul yang diambil dengan 1
                for (int col = 0; col < graphEl.Length; col++)
                {
                    for(int row = 0; row < graphEl.Length; row++)
                    {
                        int i = 0;
                        while(i <= idxs)
                        {
                            if ((graph[row,col]) && (row == solution[i]) && (i > idxbawah))
                            {
                                derajatMasuk[col]--;
                            }
                            i++;
                        }
                    }
                }
                idxbawah = idxs;
            }
        }

        public void Visualize()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert file name : ");
            String filename = Console.ReadLine();
            Graph graph = new Graph(filename);
            graph.DFSSolution();
            Console.ReadKey();
        }
    }
}
