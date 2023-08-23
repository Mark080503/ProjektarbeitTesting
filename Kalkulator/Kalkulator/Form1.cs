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
            string formattedCalculation = currentCalculation.Replace("X", "*");

            try
            {
                double result = Convert.ToDouble(new DataTable().Compute(formattedCalculation, null));

                // Check if the result is within the displayable range
                if (Math.Abs(result) > double.MaxValue)
                {
                    textBoxOutput.Text = "0"; // Set to 0 if result is too large
                }
                else
                {
                    // Format the result based on whether it has decimal places or not
                    string formattedResult = result % 1 == 0 ? result.ToString("0") : result.ToString("0.##");
                    textBoxOutput.Text = formattedResult;
                }

                currentCalculation = textBoxOutput.Text;
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = "0";
                currentCalculation = "";
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
