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
        private const string helpDirectory = "Help";
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
            Assembly localAssembly = Assembly.GetExecutingAssembly();
            string[] resourceFiles = localAssembly.GetManifestResourceNames();

            try
            {
                Directory.CreateDirectory(tmpPath);

                foreach (string resourceName in resourceFiles)
                {
                    string[] nameParts = resourceName.Split('.');
                    if (nameParts[1] != helpDirectory)
                        continue;

                    Stream resourceStream = localAssembly.GetManifestResourceStream(resourceName);

                    string storeFilename = tmpPath + '/' 
                        + nameParts[nameParts.Length - 2] 
                        + '.' + nameParts[nameParts.Length - 1];

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
