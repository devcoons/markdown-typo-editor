using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkdownTypo.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();            
            viewSelection.SelectedIndex = viewSelection.Items.Count - 1;
            this.ActiveControl = this.menuStrip1;
            openFileDialog1.Filter = "MD (*.md)|*.md|RAW (*.rmdt)|*.rmdt";
            saveFileDialog1.Filter = "MD (*.md)|*.md|RAW (*.rmdt)|*.rmdt|HTML (*.html)|*.html";

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void viewSelection_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = this.menuStrip1;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.ActiveControl = this.menuStrip1;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void viewSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(viewSelection.SelectedItem.ToString())
            {
                case "Editor Only":
                    splitContainer1.Panel1Collapsed = false;
                    splitContainer1.Panel2Collapsed = true;

                    break;
                case "Preview Only":
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel1Collapsed = true;
                    break;
                default:
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel1Collapsed = false;
                    break;
            }
            
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            webBrowser1.DocumentText = CommonMark.CommonMarkConverter.Convert(richTextBox1.Text);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = openFileDialog1.FileName;

            string text = File.ReadAllText(filename);

            string ext = Path.GetExtension(openFileDialog1.FileName);

            switch(ext)
            {
                case ".rmdt":
                    viewSelection.SelectedItem = "Editor & Preview";
                    richTextBox1.Text = text;
                    webBrowser1.DocumentText = CommonMark.CommonMarkConverter.Convert(richTextBox1.Text);
                    break;
                case ".md":
                    viewSelection.SelectedItem = "Preview Only";
                    richTextBox1.Text = text;
                    webBrowser1.DocumentText = CommonMark.CommonMarkConverter.Convert(richTextBox1.Text);
                    break;
                default:
                    viewSelection.SelectedItem = "Editor Only";
                    richTextBox1.Text = text;
                    webBrowser1.DocumentText = CommonMark.CommonMarkConverter.Convert(richTextBox1.Text);
                    break;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filename = saveFileDialog1.FileName;
            string ext = Path.GetExtension(saveFileDialog1.FileName);
            switch (ext)
            {
                case ".rmdt":
                case ".md":
                    File.WriteAllText(saveFileDialog1.FileName,  richTextBox1.Text);
                    break;
                default:
                    File.WriteAllText(saveFileDialog1.FileName, webBrowser1.DocumentText);
                    break;
            }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Forms.Help()).ShowDialog();
        }
    }
}
