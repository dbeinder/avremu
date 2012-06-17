using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace avrEmu
{
    public partial class AboutForm : Form
    {
        private string[] helpFiles = new string[] 
        {
            "Help/index.html",
            "Help/std.css"
        };

        private const string startHelpFile = "index.html";

        string tmpPath = Path.GetTempPath() + "avrEmuHelp-" + Guid.NewGuid().ToString("N").Substring(24);

        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string nameSpace = this.GetType().Namespace;
            Assembly localAssembly = Assembly.GetExecutingAssembly();

            try
            {
                Directory.CreateDirectory(tmpPath);

                foreach (string filename in this.helpFiles)
                {
                    string resName = nameSpace + "." + filename.Replace('/', '.');
                    Stream resourceStream = localAssembly.GetManifestResourceStream(resName);
                    string storeFilename = tmpPath + '/' + filename.Split('/').Last();

                    using (FileStream fs = new FileStream(storeFilename, FileMode.Create))
                    {
                        resourceStream.CopyTo(fs);
                    }
                }

                Process proc = new Process();
                proc.StartInfo = new ProcessStartInfo(tmpPath + '/' + startHelpFile);
                proc.Start();
            }
            catch
            {
                MessageBox.Show("A problem occured while creating the Helpfiles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Directory.Delete(tmpPath, true);
            }
            catch { }
        }
    }
}
