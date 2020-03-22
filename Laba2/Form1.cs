using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                try
                {
                    ImageMagick.MagickImage img = new ImageMagick.MagickImage(filePath);

                    dataGridView1.Rows.Add(Path.GetFileName(img.FileName),
                        img.Format,
                        img.BaseWidth + "x" + img.BaseHeight,
                        img.Density,
                        img.Depth,
                        img.Compression,
                        img.TotalColors
                        );
                }
                catch
                {
                    MessageBox.Show("format not supported");
                }
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);

                Task.Factory.StartNew(() =>
                {
                    foreach (var item in files)
                    {
                        try
                        {
                            ImageMagick.MagickImage img = new ImageMagick.MagickImage(item);
                            dataGridView1.Invoke((Action)(() =>
                            {
                                dataGridView1.Rows.Add(Path.GetFileName(img.FileName),
                              img.Format,
                              img.BaseWidth + "x" + img.BaseHeight,
                              img.Density,
                              img.Depth,
                              img.Compression,
                              img.TotalColors
                                            );
                            }
                            ));
                        }catch
                        {

                        }

                    }
                }
                );
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}