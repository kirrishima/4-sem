using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Lab02
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            this.Controls.Add(CreateUI());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private static FlowLayoutPanel CreateControlWithLable(string header, Control control)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
            };
            var label = new Label() { Text = header, AutoSize = true };
            flowLayoutPanel.Controls.Add(label);
            flowLayoutPanel.Controls.Add(control);

            return flowLayoutPanel;
        }
    }
}
