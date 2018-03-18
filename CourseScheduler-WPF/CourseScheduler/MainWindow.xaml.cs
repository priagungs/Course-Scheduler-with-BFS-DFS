using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CourseScheduler
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

        private void DFS_Button_Click(object sender, RoutedEventArgs e)
        {
            scheduleGraph = new Graph(fileName);
            scheduleGraph.DFSSolution();
            String s = "";
            int smt = 1;
            s += "Semester " + smt + ": ";
            int[] smtCourses = new int[scheduleGraph.solution.Length];
            for(int i=0; i<smtCourses.Length; i++)
            {
                smtCourses[i] = -1;
            }
            int idSmtCourses = 0;
            smtCourses[idSmtCourses] = scheduleGraph.solution[0];
            for (int i = 1; i < scheduleGraph.graphEl.Length; i++)
            {
                bool proper = true;
                foreach(int element in smtCourses)
                {
                    Console.WriteLine(element);
                    Console.WriteLine();
                    if(element != -1 && scheduleGraph.graph[element, scheduleGraph.solution[i]])
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
                    foreach(int element in smtCourses)
                    {
                        if(element != -1)
                        {
                            s += scheduleGraph.graphEl[element] + " ";
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
                    s += scheduleGraph.graphEl[element] + " ";
                }
            }
            textBoxSol.Text = s;
        }

        private void BFS_Button_Click(object sender, RoutedEventArgs e)
        {

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
