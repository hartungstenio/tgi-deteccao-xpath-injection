using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XPathExec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("aprovval.xml");

            try
            {
                var n = doc.SelectNodes(consulta.Text);

                textBox1.Text = string.Empty;

                for (int i = 0; i < n.Count; i++)
                    textBox1.Text = textBox1.Text +
                        n.Item(i).OuterXml +
                        Environment.NewLine + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Expressão inválida" + Environment.NewLine +
                                ex.Message);

            }
        }
    }
}
