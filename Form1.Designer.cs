
namespace Лабораторная_работа__8
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.ungroupButton = new System.Windows.Forms.Button();
            this.groupButton = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorPickerButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lineButton = new System.Windows.Forms.Button();
            this.rectangleButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.stickyObjectButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.stickyObjectButton);
            this.panel1.Controls.Add(this.treeView);
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.loadButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.ungroupButton);
            this.panel1.Controls.Add(this.groupButton);
            this.panel1.Controls.Add(this.colorLabel);
            this.panel1.Controls.Add(this.colorPickerButton);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(618, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 540);
            this.panel1.TabIndex = 0;
            // 
            // clearButton
            // 
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.clearButton.Location = new System.Drawing.Point(3, 80);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(174, 25);
            this.clearButton.TabIndex = 11;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.loadButton.Location = new System.Drawing.Point(3, 204);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(174, 25);
            this.loadButton.TabIndex = 10;
            this.loadButton.Text = "Загрузить";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.saveButton.Location = new System.Drawing.Point(3, 173);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(174, 25);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // ungroupButton
            // 
            this.ungroupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ungroupButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ungroupButton.Location = new System.Drawing.Point(3, 142);
            this.ungroupButton.Name = "ungroupButton";
            this.ungroupButton.Size = new System.Drawing.Size(174, 25);
            this.ungroupButton.TabIndex = 8;
            this.ungroupButton.Text = "Разгруппировка";
            this.ungroupButton.UseVisualStyleBackColor = true;
            this.ungroupButton.Click += new System.EventHandler(this.ungroupButton_Click);
            // 
            // groupButton
            // 
            this.groupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.groupButton.Location = new System.Drawing.Point(3, 111);
            this.groupButton.Name = "groupButton";
            this.groupButton.Size = new System.Drawing.Size(174, 25);
            this.groupButton.TabIndex = 7;
            this.groupButton.Text = "Группировка";
            this.groupButton.UseVisualStyleBackColor = true;
            this.groupButton.Click += new System.EventHandler(this.groupButton_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.BackColor = System.Drawing.Color.Black;
            this.colorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorLabel.Location = new System.Drawing.Point(3, 45);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(30, 25);
            this.colorLabel.TabIndex = 6;
            // 
            // colorPickerButton
            // 
            this.colorPickerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorPickerButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.colorPickerButton.Location = new System.Drawing.Point(39, 45);
            this.colorPickerButton.Name = "colorPickerButton";
            this.colorPickerButton.Size = new System.Drawing.Size(138, 25);
            this.colorPickerButton.TabIndex = 5;
            this.colorPickerButton.Text = "Выбор цвета";
            this.colorPickerButton.UseVisualStyleBackColor = true;
            this.colorPickerButton.Click += new System.EventHandler(this.colorPickerButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lineButton);
            this.flowLayoutPanel1.Controls.Add(this.rectangleButton);
            this.flowLayoutPanel1.Controls.Add(this.ellipseButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 39);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // lineButton
            // 
            this.lineButton.BackColor = System.Drawing.Color.White;
            this.lineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lineButton.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lineButton.Location = new System.Drawing.Point(3, 3);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(30, 30);
            this.lineButton.TabIndex = 0;
            this.lineButton.Text = "╲";
            this.lineButton.UseVisualStyleBackColor = false;
            this.lineButton.Click += new System.EventHandler(this.lineButton_Click);
            // 
            // rectangleButton
            // 
            this.rectangleButton.BackColor = System.Drawing.Color.White;
            this.rectangleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rectangleButton.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectangleButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.rectangleButton.Location = new System.Drawing.Point(39, 3);
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(30, 30);
            this.rectangleButton.TabIndex = 1;
            this.rectangleButton.Text = "▭";
            this.rectangleButton.UseVisualStyleBackColor = false;
            this.rectangleButton.Click += new System.EventHandler(this.rectangleButton_Click);
            // 
            // ellipseButton
            // 
            this.ellipseButton.BackColor = System.Drawing.Color.White;
            this.ellipseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ellipseButton.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellipseButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ellipseButton.Location = new System.Drawing.Point(75, 3);
            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(30, 30);
            this.ellipseButton.TabIndex = 2;
            this.ellipseButton.Text = "◯";
            this.ellipseButton.UseVisualStyleBackColor = false;
            this.ellipseButton.Click += new System.EventHandler(this.ellipseButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(618, 540);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(5, 266);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(172, 269);
            this.treeView.TabIndex = 12;
            // 
            // stickyObjectButton
            // 
            this.stickyObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stickyObjectButton.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.stickyObjectButton.Location = new System.Drawing.Point(3, 235);
            this.stickyObjectButton.Name = "stickyObjectButton";
            this.stickyObjectButton.Size = new System.Drawing.Size(174, 25);
            this.stickyObjectButton.TabIndex = 13;
            this.stickyObjectButton.Text = "Сделать объект липким";
            this.stickyObjectButton.UseVisualStyleBackColor = true;
            this.stickyObjectButton.Click += new System.EventHandler(this.stickyObjectButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Text = "Визуальный редактор";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ellipseButton;
        private System.Windows.Forms.Button rectangleButton;
        private System.Windows.Forms.Button lineButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button colorPickerButton;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button ungroupButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button stickyObjectButton;
        private System.Windows.Forms.TreeView treeView;
    }
}

