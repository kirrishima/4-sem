using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Lab02
{
    public partial class Form1 : Form
    {
        string jsonPath = "data.json";
        string lastSavedJsonTxtPath = "last_save_path.txt";

        public Form1()
        {
            InitializeComponent();

            //var pp = CreateAddTeachersUI();
            var fd = CreateCurrentTeachersUI();
            var p = CreateExpander(fd, "Список Преподавателей", teachersIsExpanded, teachersExpanderContentPanel);

            //tabControl1.TabPages[0].Controls.Add(pp);
            tabControl1.TabPages[0].Controls.Add(p);
            tabControl1.TabPages[0].Controls.Add(CreateAddTeacherToCourseUI());

            tabControl1.TabPages[1].Controls.Add(CreateAddCoursesUI());

            this.FormClosing += Form1_FormClosing;
            this.AutoScroll = true;
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;

            try
            {
                if (File.Exists(lastSavedJsonTxtPath))
                {
                    var path = File.ReadAllText(lastSavedJsonTxtPath);
                    jsonPath = File.Exists(path) ? path : jsonPath;
                }
            }
            catch
            {

            }

            jsonPathtextBox.Text = jsonPath;

            LoadCoursesFromJson(jsonPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCoursesToJson(jsonPath);
        }

        private TableLayoutPanel CreateControlWithLabel(string header, Control control)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
            };

            Label label = new Label
            {
                Text = header,
                AutoSize = true,
            };

            control.Dock = DockStyle.Fill;
            panel.Controls.Add(label, 0, 0);
            panel.Controls.Add(control, 0, 1);

            return panel;
        }

        private void ShowErrorDialog(string message)
        {
            using (var dlg = new ErrorDialog(message))
            {
                dlg.ShowDialog(this);
            }
        }

        private void ShowErrorDialog(Exception ex)
        {
            using (var dlg = new ErrorDialog(ex))
            {
                dlg.ShowDialog(this);
            }
        }

        private void saveToJsonButon_Click(object sender, EventArgs e)
        {
            SaveCoursesToJson(this.jsonPath);
        }

        private void loadFromJsonButton_Click(object sender, EventArgs e)
        {
            LoadCoursesFromJson(this.jsonPath);
        }

        private void jsonPathtextBox_TextChanged(object sender, EventArgs e)
        {
            jsonPath = jsonPathtextBox.Text;

            try
            {
                File.WriteAllText(lastSavedJsonTxtPath, jsonPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
        }
    }
}
