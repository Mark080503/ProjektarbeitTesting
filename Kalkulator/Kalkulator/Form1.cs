using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        private string currentCalculation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            currentCalculation += (sender as Button).Text;
            textBoxOutput.Text = currentCalculation;
        }
        
        private void button_Equals_Click(object sender, EventArgs e)
        {
            string formattedCalculation = currentCalculation.ToString().Replace("X", "*");

            try
            {
                textBoxOutput.Text = new DataTable().Compute(formattedCalculation, null).ToString();
                currentCalculation = textBoxOutput.Text;
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = "0";
                currentCalculation = null;
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            currentCalculation = "";
        }

        private void button_ClearEntry_Click(object sender, EventArgs e)
        {
            if (currentCalculation.Length > 0)
            {
                currentCalculation = currentCalculation.Remove(currentCalculation.Length - 1, 1);
            }

            textBoxOutput.Text = currentCalculation;
        }
    }
}
