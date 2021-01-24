namespace SerialCOMManager
{
    partial class DeviceInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceInfoForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDeviceBufferSize = new System.Windows.Forms.TextBox();
            this.txtDevicePassword = new System.Windows.Forms.TextBox();
            this.txtDeviceSSID = new System.Windows.Forms.TextBox();
            this.drpDeviceMode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeviceIPAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.drpDeviceBaudRate = new System.Windows.Forms.ComboBox();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.footerPanel2 = new System.Windows.Forms.Panel();
            this.btnSetConfig = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnGetConfig = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2.SuspendLayout();
            this.footerPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Menu;
            this.panel2.Controls.Add(this.txtDeviceBufferSize);
            this.panel2.Controls.Add(this.txtDevicePassword);
            this.panel2.Controls.Add(this.txtDeviceSSID);
            this.panel2.Controls.Add(this.drpDeviceMode);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtDeviceIPAddress);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.drpDeviceBaudRate);
            this.panel2.Controls.Add(this.txtDeviceName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 425);
            this.panel2.TabIndex = 5;
            // 
            // txtDeviceBufferSize
            // 
            this.txtDeviceBufferSize.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceBufferSize.ForeColor = System.Drawing.Color.Black;
            this.txtDeviceBufferSize.Location = new System.Drawing.Point(210, 331);
            this.txtDeviceBufferSize.Name = "txtDeviceBufferSize";
            this.txtDeviceBufferSize.Size = new System.Drawing.Size(250, 27);
            this.txtDeviceBufferSize.TabIndex = 13;
            this.txtDeviceBufferSize.TextChanged += new System.EventHandler(this.txtDeviceBufferSize_TextChanged);
            // 
            // txtDevicePassword
            // 
            this.txtDevicePassword.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDevicePassword.ForeColor = System.Drawing.Color.Black;
            this.txtDevicePassword.Location = new System.Drawing.Point(210, 285);
            this.txtDevicePassword.Name = "txtDevicePassword";
            this.txtDevicePassword.Size = new System.Drawing.Size(250, 27);
            this.txtDevicePassword.TabIndex = 12;
            this.txtDevicePassword.TextChanged += new System.EventHandler(this.txtDevicePassword_TextChanged);
            // 
            // txtDeviceSSID
            // 
            this.txtDeviceSSID.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceSSID.ForeColor = System.Drawing.Color.Black;
            this.txtDeviceSSID.Location = new System.Drawing.Point(210, 238);
            this.txtDeviceSSID.Name = "txtDeviceSSID";
            this.txtDeviceSSID.Size = new System.Drawing.Size(250, 27);
            this.txtDeviceSSID.TabIndex = 11;
            this.txtDeviceSSID.TextChanged += new System.EventHandler(this.txtDeviceSSID_TextChanged);
            // 
            // drpDeviceMode
            // 
            this.drpDeviceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpDeviceMode.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpDeviceMode.ForeColor = System.Drawing.Color.Black;
            this.drpDeviceMode.FormattingEnabled = true;
            this.drpDeviceMode.Items.AddRange(new object[] {
            "Server",
            "Client"});
            this.drpDeviceMode.Location = new System.Drawing.Point(210, 139);
            this.drpDeviceMode.Name = "drpDeviceMode";
            this.drpDeviceMode.Size = new System.Drawing.Size(250, 28);
            this.drpDeviceMode.TabIndex = 5;
            this.drpDeviceMode.SelectedIndexChanged += new System.EventHandler(this.drpDeviceMode_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(71, 335);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Buffer Size";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(71, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(71, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "SSID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(71, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "IP Address";
            // 
            // txtDeviceIPAddress
            // 
            this.txtDeviceIPAddress.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceIPAddress.ForeColor = System.Drawing.Color.Black;
            this.txtDeviceIPAddress.Location = new System.Drawing.Point(210, 192);
            this.txtDeviceIPAddress.Name = "txtDeviceIPAddress";
            this.txtDeviceIPAddress.Size = new System.Drawing.Size(250, 27);
            this.txtDeviceIPAddress.TabIndex = 10;
            this.txtDeviceIPAddress.TextChanged += new System.EventHandler(this.txtDeviceIPAddress_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(71, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mode";
            // 
            // drpDeviceBaudRate
            // 
            this.drpDeviceBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpDeviceBaudRate.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpDeviceBaudRate.ForeColor = System.Drawing.Color.Black;
            this.drpDeviceBaudRate.FormattingEnabled = true;
            this.drpDeviceBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400"});
            this.drpDeviceBaudRate.Location = new System.Drawing.Point(210, 92);
            this.drpDeviceBaudRate.Name = "drpDeviceBaudRate";
            this.drpDeviceBaudRate.Size = new System.Drawing.Size(250, 28);
            this.drpDeviceBaudRate.TabIndex = 3;
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceName.ForeColor = System.Drawing.Color.Black;
            this.txtDeviceName.Location = new System.Drawing.Point(210, 47);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(250, 27);
            this.txtDeviceName.TabIndex = 2;
            this.txtDeviceName.TextChanged += new System.EventHandler(this.txtDeviceName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(71, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Baud Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(71, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Device Name";
            // 
            // footerPanel2
            // 
            this.footerPanel2.BackColor = System.Drawing.SystemColors.Menu;
            this.footerPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.footerPanel2.Controls.Add(this.btnSetConfig);
            this.footerPanel2.Controls.Add(this.btnNext);
            this.footerPanel2.Controls.Add(this.btnGetConfig);
            this.footerPanel2.Controls.Add(this.btnBack);
            this.footerPanel2.Location = new System.Drawing.Point(-1, 425);
            this.footerPanel2.Name = "footerPanel2";
            this.footerPanel2.Size = new System.Drawing.Size(760, 66);
            this.footerPanel2.TabIndex = 6;
            // 
            // btnSetConfig
            // 
            this.btnSetConfig.BackColor = System.Drawing.SystemColors.Menu;
            this.btnSetConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetConfig.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetConfig.ForeColor = System.Drawing.Color.Black;
            this.btnSetConfig.Location = new System.Drawing.Point(506, 15);
            this.btnSetConfig.Name = "btnSetConfig";
            this.btnSetConfig.Size = new System.Drawing.Size(137, 31);
            this.btnSetConfig.TabIndex = 2;
            this.btnSetConfig.Text = "Set Config";
            this.btnSetConfig.UseVisualStyleBackColor = false;
            this.btnSetConfig.Click += new System.EventHandler(this.btnSetConfig_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Menu;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(649, 15);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(87, 31);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnGetConfig
            // 
            this.btnGetConfig.BackColor = System.Drawing.SystemColors.Menu;
            this.btnGetConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetConfig.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetConfig.ForeColor = System.Drawing.Color.Black;
            this.btnGetConfig.Location = new System.Drawing.Point(360, 15);
            this.btnGetConfig.Name = "btnGetConfig";
            this.btnGetConfig.Size = new System.Drawing.Size(140, 31);
            this.btnGetConfig.TabIndex = 1;
            this.btnGetConfig.Text = "Get Config";
            this.btnGetConfig.UseVisualStyleBackColor = false;
            this.btnGetConfig.Click += new System.EventHandler(this.btnGetConfig_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Menu;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(267, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(86, 31);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DeviceInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 489);
            this.Controls.Add(this.footerPanel2);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DeviceInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeviceInfoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.DeviceInfoForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.footerPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtDeviceBufferSize;
        private System.Windows.Forms.TextBox txtDevicePassword;
        private System.Windows.Forms.TextBox txtDeviceSSID;
        private System.Windows.Forms.ComboBox drpDeviceMode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeviceIPAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox drpDeviceBaudRate;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel footerPanel2;
        private System.Windows.Forms.Button btnSetConfig;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnGetConfig;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}