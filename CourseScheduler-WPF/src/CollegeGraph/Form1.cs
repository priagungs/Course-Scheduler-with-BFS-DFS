using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;

namespace CollegeGraph
{
    public partial class GraphLayout : Form
    {
        public int idx;
        public static List<Microsoft.Msagl.Drawing.Graph> log;
        public GraphLayout()
        {
            InitializeComponent();
            Generate();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleName = "form";
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(392, 280);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // GraphLayout
            // 
            this.ClientSize = new System.Drawing.Size(416, 304);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GraphLayout";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        GViewer viewer = new GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

        public Microsoft.Msagl.Drawing.Graph ConstGraph() {
            var g = new Microsoft.Msagl.Drawing.Graph();
            for (int i = 0; i < Graph.graphEl.Length; i++)
            {
                g.AddNode(Graph.graphEl[i]).Attr.FillColor = Color.LightBlue;
            }
            bool[,] AdjMx = Graph.graph;
            for (int i = 0; i < Graph.graphEl.Length; i++)
            {
                for (int j = 0; j < Graph.graphEl.Length; j++)
                {
                    if (AdjMx[i, j])
                    {
                        g.AddEdge(Graph.graphEl[i], Graph.graphEl[j]);
                    }
                }
            }
            return g;
        }

        public void Generate()
        {
            log = new List<Microsoft.Msagl.Drawing.Graph>();
            graph = ConstGraph();
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form
            viewer.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(viewer);
            //show the form 
            foreach (var elem in Graph.visitedNode)
            {
                Microsoft.Msagl.Drawing.Graph gtemp = ConstGraph();
                gtemp.FindNode(Graph.graphEl[elem]).Attr.FillColor = Color.Yellow;
                log.Add(gtemp);
            }
            log.Add(graph);
            timer2.Interval = 1300;
            idx = 0;
            timer2.Tick += new EventHandler(Animate);
            timer2.Enabled = true;
        }

        public void Animate(object sender, EventArgs e)
        {
            if (idx < Graph.graphEl.Length)
            {
                viewer.Graph = log[idx];
                viewer.Dock = DockStyle.Fill;
                pictureBox1.Controls.Add(viewer);
                pictureBox1.ResumeLayout();
                pictureBox1.Show();
                idx += 1;
            }
            else
            {
                timer2.Stop();
            }
            //show the form 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }
    }
}
