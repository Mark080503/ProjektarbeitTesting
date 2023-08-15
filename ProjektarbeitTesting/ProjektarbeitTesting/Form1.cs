using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektarbeitTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "Hello World!";
            // Define the relative increase values for font size and dimensions
            int fontSizeIncrease = 2; // points
            int dimensionsIncrease = 20; // pixels

            // Increase font size by the defined amount
            float newFontSize = Math.Max(label1.Font.Size + fontSizeIncrease, 6); // Minimum font size of 6 points
            label1.Font = new Font(label1.Font.FontFamily, newFontSize);

            // Increase label dimensions by the defined amount
            label1.Width += dimensionsIncrease;
            label1.Height += dimensionsIncrease;
        }
    }
}
