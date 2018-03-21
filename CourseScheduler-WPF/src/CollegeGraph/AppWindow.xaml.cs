using System;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;

namespace CollegeGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String fileName;
        private Graph scheduleGraph;

        public MainWindow()
        {
            InitializeComponent();
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

        private void Print_Solution()
        //assign every course to proper semester
        {
            String s = "";
            int smt = 1;
            s += "Semester " + smt + ": ";
            int[] smtCourses = new int[scheduleGraph.solution.Length];
            for (int i = 0; i < smtCourses.Length; i++)
            {
                smtCourses[i] = -1;
            }
            int idSmtCourses = 0;
            smtCourses[idSmtCourses] = scheduleGraph.solution[0];
            for (int i = 1; i < Graph.graphEl.Length; i++)
            {
                bool proper = true;
                foreach (int element in smtCourses)
                {
                    if (element != -1 && Graph.graph[element, scheduleGraph.solution[i]])
                    {
                        proper = false;
                    }
                }
                if (proper)
                {
                    idSmtCourses++;
                    smtCourses[idSmtCourses] = scheduleGraph.solution[i];
                }
                else
                {
                    foreach (int element in smtCourses)
                    {
                        if (element != -1)
                        {
                            s += Graph.graphEl[element] + " ";
                        }
                    }
                    idSmtCourses = 0;
                    for (int j = 0; j < smtCourses.Length; j++)
                    {
                        smtCourses[j] = -1;
                    }
                    smtCourses[idSmtCourses] = scheduleGraph.solution[i];
                    smt++;
                    s += "\nSemester " + smt + ": ";
                }
            }
            foreach (int element in smtCourses)
            {
                if (element != -1)
                {
                    s += Graph.graphEl[element] + " ";
                }
            }
            textBoxSol.Text = s;
        }

        private void DFS_Button_Click(object sender, RoutedEventArgs e)
        {
            scheduleGraph = new Graph(fileName);
            scheduleGraph.DFSSolution();
            Print_Solution();
            var LayoutDFS = new GraphLayout();
            LayoutDFS.Show();

        }

        private void BFS_Button_Click(object sender, RoutedEventArgs e)
        {
            scheduleGraph = new Graph(fileName);
            scheduleGraph.BFSSolution();
            Print_Solution();
            var LayoutBFS = new GraphLayout();
            LayoutBFS.Show();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = System.IO.Path.GetFullPath(openFileDialog.FileName);
                directoryName.Text = fileName;

            }
        }
    }
}
