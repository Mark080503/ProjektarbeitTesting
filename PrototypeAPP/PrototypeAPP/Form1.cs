using System;
using System.Windows.Forms;

namespace PrototypeAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set up the SplitContainer
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill; // Fill the entire form with the SplitContainer

            // Set IsSplitterFixed property to true to prevent splitter movement.
            splitContainer.IsSplitterFixed = true;

            // Optionally, set a minimum width for the left panel.
            splitContainer.Panel1MinSize = 150;

            // Add controls to the left and right panels
            // For example:
            // splitContainer.Panel1.Controls.Add(yourLeftPanelControl);
            // splitContainer.Panel2.Controls.Add(yourRightPanelControl);

            Controls.Add(splitContainer); // Add the SplitContainer to the form
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
