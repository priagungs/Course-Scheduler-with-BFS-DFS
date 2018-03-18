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
            scheduleGraph.DFSSolution();
            String s = "";
            for(int i=0; i<scheduleGraph.solution.Length; i++)
            {
                s += scheduleGraph.graphEl[scheduleGraph.solution[i]] + ", ";
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
            if(openFileDialog.ShowDialog() == true)
            {
                fileName = System.IO.Path.GetFullPath(openFileDialog.FileName);
                directoryName.Text = fileName;
                scheduleGraph = new Graph(fileName);
            }
        }
    }
}
