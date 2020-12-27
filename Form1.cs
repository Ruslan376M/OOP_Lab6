using System;
using System.Windows.Forms;

namespace Лабораторная_работа__8
{
    public partial class MainForm : Form
    {
        Model model;
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            model = new Model();
            model.treeView = treeView;
            model.treeView.AfterSelect += model.TreeView_AfterSelect;
            treeView.Focus();
            model.refresh = refresh;
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

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
                model.doTheRightThing(e.X, e.Y);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (model.creatingObject)
                    model.drawOnline(e.X, e.Y);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (model.creatingObject == true)
                {
                    model.creatingObject = false;
                    model.add();
                }
        }

        private void colorPickerButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            { 
                colorLabel.BackColor = colorDialog.Color;
                model.setColor(colorDialog.Color);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.ControlKey:
                    model.ctrlIsPressed = true;
                    break;
                case Keys.ShiftKey:
                    model.shiftIsPressed = true;
                    break;
                case Keys.W:
                    model.wIsPressed = true;
                    break;
                case Keys.A:
                    model.aIsPressed = true;
                    break;
                case Keys.S:
                    model.sIsPressed = true;
                    break;
                case Keys.D:
                    model.dIsPressed = true;
                    break;
                case Keys.Delete:
                    model.delete();
                    GC.Collect();
                    break;
                default:
                    return ;
            }
            if (model.wIsPressed || model.aIsPressed || model.sIsPressed || model.dIsPressed)
                if (model.shiftIsPressed)
                    model.changeSize();
                else
                    model.move();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    model.ctrlIsPressed = false;
                    break;
                case Keys.ShiftKey:
                    model.shiftIsPressed = false;
                    model.correctSelected();
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
            model.velocity = 5;
        }

        private void refresh()
        {
            pictureBox.Image = model.image;
        }

        private void groupButton_Click(object sender, EventArgs e)
        {
            model.group();
        }

        private void ungroupButton_Click(object sender, EventArgs e)
        {
            model.ungroup();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            model.save();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            model.load();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            model.clear();
        }

        private void stickyObjectButton_Click(object sender, EventArgs e)
        {
            model.makeStickyObject();
        }
    }
}
