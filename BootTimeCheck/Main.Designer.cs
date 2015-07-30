namespace BootTimeCheck
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem4 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem5 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem6 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.avgLabel = new System.Windows.Forms.Label();
            this.avgBox = new System.Windows.Forms.TextBox();
            this.minBox = new System.Windows.Forms.TextBox();
            this.minLabel = new System.Windows.Forms.Label();
            this.maxBox = new System.Windows.Forms.TextBox();
            this.maxLabel = new System.Windows.Forms.Label();
            this.improvementBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxBoots = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legendItem4.Color = System.Drawing.Color.DarkRed;
            legendItem4.Name = "Poor";
            legendItem5.Color = System.Drawing.Color.Yellow;
            legendItem5.Name = "Fair";
            legendItem6.Color = System.Drawing.Color.LimeGreen;
            legendItem6.Name = "Good";
            legend2.CustomItems.Add(legendItem4);
            legend2.CustomItems.Add(legendItem5);
            legend2.CustomItems.Add(legendItem6);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 67);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsValueShownAsLabel = true;
            series2.IsVisibleInLegend = false;
            series2.LabelBackColor = System.Drawing.Color.Black;
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Legend = "Legend1";
            series2.Name = "Series";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(766, 456);
            this.chart1.TabIndex = 0;
            title2.Name = "TableTitle";
            title2.Text = "Historical Boot Times (in seconds)";
            this.chart1.Titles.Add(title2);
            // 
            // avgLabel
            // 
            this.avgLabel.AutoSize = true;
            this.avgLabel.Location = new System.Drawing.Point(13, 10);
            this.avgLabel.Name = "avgLabel";
            this.avgLabel.Size = new System.Drawing.Size(101, 13);
            this.avgLabel.TabIndex = 1;
            this.avgLabel.Text = "Average Boot Time:";
            // 
            // avgBox
            // 
            this.avgBox.Enabled = false;
            this.avgBox.Location = new System.Drawing.Point(120, 7);
            this.avgBox.Name = "avgBox";
            this.avgBox.Size = new System.Drawing.Size(74, 20);
            this.avgBox.TabIndex = 2;
            // 
            // minBox
            // 
            this.minBox.Enabled = false;
            this.minBox.Location = new System.Drawing.Point(405, 7);
            this.minBox.Name = "minBox";
            this.minBox.Size = new System.Drawing.Size(74, 20);
            this.minBox.TabIndex = 4;
            // 
            // minLabel
            // 
            this.minLabel.AutoSize = true;
            this.minLabel.Location = new System.Drawing.Point(297, 10);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(102, 13);
            this.minLabel.TabIndex = 3;
            this.minLabel.Text = "Minimum Boot Time:";
            // 
            // maxBox
            // 
            this.maxBox.Enabled = false;
            this.maxBox.Location = new System.Drawing.Point(704, 7);
            this.maxBox.Name = "maxBox";
            this.maxBox.Size = new System.Drawing.Size(74, 20);
            this.maxBox.TabIndex = 6;
            // 
            // maxLabel
            // 
            this.maxLabel.AutoSize = true;
            this.maxLabel.Location = new System.Drawing.Point(593, 10);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(105, 13);
            this.maxLabel.TabIndex = 5;
            this.maxLabel.Text = "Maximum Boot Time:";
            // 
            // improvementBox
            // 
            this.improvementBox.Enabled = false;
            this.improvementBox.Location = new System.Drawing.Point(624, 39);
            this.improvementBox.Name = "improvementBox";
            this.improvementBox.Size = new System.Drawing.Size(74, 20);
            this.improvementBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Change over X boots:";
            // 
            // cbxBoots
            // 
            this.cbxBoots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBoots.FormattingEnabled = true;
            this.cbxBoots.Items.AddRange(new object[] {
            "1 boot",
            "5 boots",
            "10 boots",
            "15 boots",
            "20 boots",
            "all boots"});
            this.cbxBoots.Location = new System.Drawing.Point(497, 39);
            this.cbxBoots.Name = "cbxBoots";
            this.cbxBoots.Size = new System.Drawing.Size(121, 21);
            this.cbxBoots.TabIndex = 9;
            this.cbxBoots.SelectedIndexChanged += new System.EventHandler(this.cbxBoots_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 535);
            this.Controls.Add(this.cbxBoots);
            this.Controls.Add(this.improvementBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxBox);
            this.Controls.Add(this.maxLabel);
            this.Controls.Add(this.minBox);
            this.Controls.Add(this.minLabel);
            this.Controls.Add(this.avgBox);
            this.Controls.Add(this.avgLabel);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Boot Time Diagnostics";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label avgLabel;
        private System.Windows.Forms.TextBox avgBox;
        private System.Windows.Forms.TextBox minBox;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.TextBox maxBox;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.TextBox improvementBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxBoots;
    }
}