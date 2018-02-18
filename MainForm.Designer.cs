using System.Drawing;

namespace MultiFaceRec
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.BtnSaveFoundFace = new System.Windows.Forms.Button();
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pauseDetectionForTheNext5MinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseDetectionForTheNext15MinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseDetectioinForTheNext60MinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnONDetectionModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnOFFDetectionModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDetectionModeToTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTrainingInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentDectectionModeStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.runningApps = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDonate = new System.Windows.Forms.Button();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtCurrentPicture = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSaveFoundFace
            // 
            this.BtnSaveFoundFace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnSaveFoundFace.Location = new System.Drawing.Point(87, 181);
            this.BtnSaveFoundFace.Name = "BtnSaveFoundFace";
            this.BtnSaveFoundFace.Size = new System.Drawing.Size(87, 31);
            this.BtnSaveFoundFace.TabIndex = 3;
            this.BtnSaveFoundFace.Text = "2. Add face";
            this.BtnSaveFoundFace.UseVisualStyleBackColor = true;
            this.BtnSaveFoundFace.Click += new System.EventHandler(this.BtnSaveFoundFace_Click);
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(67, 155);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(107, 20);
            this.TxtUsername.TabIndex = 7;
            this.TxtUsername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox1.Controls.Add(this.txtCurrentPicture);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPrev);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtUsername);
            this.groupBox1.Controls.Add(this.imageBox1);
            this.groupBox1.Controls.Add(this.BtnSaveFoundFace);
            this.groupBox1.Location = new System.Drawing.Point(342, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 242);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training: ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseDetectionForTheNext5MinutesToolStripMenuItem,
            this.pauseDetectionForTheNext15MinutesToolStripMenuItem,
            this.pauseDetectioinForTheNext60MinutesToolStripMenuItem,
            this.turnONDetectionModeToolStripMenuItem,
            this.turnOFFDetectionModeToolStripMenuItem,
            this.setDetectionModeToTrainingToolStripMenuItem,
            this.showTrainingInterfaceToolStripMenuItem,
            this.currentDectectionModeStatusToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(316, 180);
            // 
            // pauseDetectionForTheNext5MinutesToolStripMenuItem
            // 
            this.pauseDetectionForTheNext5MinutesToolStripMenuItem.Name = "pauseDetectionForTheNext5MinutesToolStripMenuItem";
            this.pauseDetectionForTheNext5MinutesToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.pauseDetectionForTheNext5MinutesToolStripMenuItem.Text = "Pause Detection for the next 5 minutes";
            // 
            // pauseDetectionForTheNext15MinutesToolStripMenuItem
            // 
            this.pauseDetectionForTheNext15MinutesToolStripMenuItem.Name = "pauseDetectionForTheNext15MinutesToolStripMenuItem";
            this.pauseDetectionForTheNext15MinutesToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.pauseDetectionForTheNext15MinutesToolStripMenuItem.Text = "Pause Detection for the next 15 minutes";
            // 
            // pauseDetectioinForTheNext60MinutesToolStripMenuItem
            // 
            this.pauseDetectioinForTheNext60MinutesToolStripMenuItem.Name = "pauseDetectioinForTheNext60MinutesToolStripMenuItem";
            this.pauseDetectioinForTheNext60MinutesToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.pauseDetectioinForTheNext60MinutesToolStripMenuItem.Text = "Pause Detectioin for the next 60 minutes";
            // 
            // turnONDetectionModeToolStripMenuItem
            // 
            this.turnONDetectionModeToolStripMenuItem.Name = "turnONDetectionModeToolStripMenuItem";
            this.turnONDetectionModeToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.turnONDetectionModeToolStripMenuItem.Text = "Set Detection Mode *ON*";
            this.turnONDetectionModeToolStripMenuItem.Click += new System.EventHandler(this.turnONDetectionModeToolStripMenuItem_Click);
            // 
            // turnOFFDetectionModeToolStripMenuItem
            // 
            this.turnOFFDetectionModeToolStripMenuItem.Name = "turnOFFDetectionModeToolStripMenuItem";
            this.turnOFFDetectionModeToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.turnOFFDetectionModeToolStripMenuItem.Text = "Turn *OFF* Detection Mode";
            this.turnOFFDetectionModeToolStripMenuItem.Click += new System.EventHandler(this.turnOFFDetectionModeToolStripMenuItem_Click);
            // 
            // setDetectionModeToTrainingToolStripMenuItem
            // 
            this.setDetectionModeToTrainingToolStripMenuItem.Name = "setDetectionModeToTrainingToolStripMenuItem";
            this.setDetectionModeToTrainingToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.setDetectionModeToTrainingToolStripMenuItem.Text = "Set Detection Mode to *Training*";
            this.setDetectionModeToTrainingToolStripMenuItem.Click += new System.EventHandler(this.setDetectionModeToTrainingToolStripMenuItem_Click);
            // 
            // showTrainingInterfaceToolStripMenuItem
            // 
            this.showTrainingInterfaceToolStripMenuItem.Name = "showTrainingInterfaceToolStripMenuItem";
            this.showTrainingInterfaceToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.showTrainingInterfaceToolStripMenuItem.Text = "Show Training interface";
            // 
            // currentDectectionModeStatusToolStripMenuItem
            // 
            this.currentDectectionModeStatusToolStripMenuItem.Enabled = false;
            this.currentDectectionModeStatusToolStripMenuItem.Name = "currentDectectionModeStatusToolStripMenuItem";
            this.currentDectectionModeStatusToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.currentDectectionModeStatusToolStripMenuItem.Tag = "-=<{ Currently Dectection Mode == STATUS  }>=-";
            this.currentDectectionModeStatusToolStripMenuItem.Text = "-=<{ Currently Dectection Mode == Off  }>=-";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(67, 200);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(14, 22);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(67, 178);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(14, 22);
            this.btnPrev.TabIndex = 10;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 177);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 65);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name: ";
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(11, 18);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(163, 134);
            this.imageBox1.TabIndex = 5;
            this.imageBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.runningApps);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(532, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 242);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Persons present in the scene:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nobody";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(163, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number of faces detected: ";
            // 
            // runningApps
            // 
            this.runningApps.Location = new System.Drawing.Point(10, 160);
            this.runningApps.Name = "runningApps";
            this.runningApps.Size = new System.Drawing.Size(179, 40);
            this.runningApps.TabIndex = 18;
            this.runningApps.Text = "Running Apps...";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(10, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 24);
            this.button1.TabIndex = 2;
            this.button1.Text = "1. Detect and recognize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDonate
            // 
            this.btnDonate.BackgroundImage = global::MultiFaceRec.Properties.Resources.Donate;
            this.btnDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonate.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDonate.Location = new System.Drawing.Point(12, 256);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(95, 30);
            this.btnDonate.TabIndex = 10;
            this.btnDonate.UseVisualStyleBackColor = true;
            this.btnDonate.Click += new System.EventHandler(this.button3_Click);
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.ContextMenuStrip = this.contextMenuStrip1;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(12, 12);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(320, 240);
            this.imageBoxFrameGrabber.TabIndex = 4;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Welcome to my first attempt at avoiding Prying Eyes...";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.BalloonTipClosed += new System.EventHandler(this.notifyIcon_BalloonTipClosed);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // txtCurrentPicture
            // 
            this.txtCurrentPicture.BackColor = System.Drawing.Color.Transparent;
            this.txtCurrentPicture.Location = new System.Drawing.Point(67, 225);
            this.txtCurrentPicture.Name = "txtCurrentPicture";
            this.txtCurrentPicture.Size = new System.Drawing.Size(111, 13);
            this.txtCurrentPicture.TabIndex = 11;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 290);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.btnDonate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Name = "FrmPrincipal";
            this.Text = "Serg3ant\'s face detector and recgonizer :D";
            this.Resize += new System.EventHandler(this.FrmPrincipal_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSaveFoundFace;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDonate;
        private System.Windows.Forms.RichTextBox runningApps;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pauseDetectionForTheNext5MinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseDetectionForTheNext15MinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseDetectioinForTheNext60MinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnONDetectionModeToolStripMenuItem;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem turnOFFDetectionModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTrainingInterfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentDectectionModeStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDetectionModeToTrainingToolStripMenuItem;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtCurrentPicture;
    }
}

