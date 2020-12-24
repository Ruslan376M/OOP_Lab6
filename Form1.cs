using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_работа__6
{
    public partial class MainForm : Form
    {
        Model model;
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            model = new Model();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            model.mouseIsPressed = true;
            model.doTheRightThing(e.X, e.Y);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            model.doTheRightThing(e.X, e.Y);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            model.mouseIsPressed = false;
            model.doTheRightThing(e.X, e.Y);
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            model.setMode(0);
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            model.setMode(1);
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            model.setMode(2);
        }

        private void colorPickerButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            { 
                colorLabel.BackColor = colorDialog.Color;
                model.setColor(colorDialog.Color);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox.Image = model.image;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Control:
                    model.ctrlIsPressed = true;
                    break;
                case Keys.Alt:
                    model.altIsPressed = true;
                    break;
                case Keys.W:
                    model.wIsPressed = true;
                    model.move();
                    break;
                case Keys.A:
                    model.aIsPressed = true;
                    model.move();
                    break;
                case Keys.S:
                    model.sIsPressed = true;
                    model.move();
                    break;
                case Keys.D:
                    model.dIsPressed = true;
                    model.move();
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Control:
                    model.ctrlIsPressed = false;
                    break;
                case Keys.Alt:
                    model.altIsPressed = false;
                    break;
                case Keys.W:
                    model.wIsPressed = false;
                    break;
                case Keys.A:
                    model.aIsPressed = false;
                    break;
                case Keys.S:
                    model.sIsPressed = false;
                    break;
                case Keys.D:
                    model.dIsPressed = false;
                    break;
            }
        }
    }
}
