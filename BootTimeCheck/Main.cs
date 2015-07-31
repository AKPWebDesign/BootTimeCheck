using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BootTimeCheck
{
    public partial class Main : Form
    {
        public Main(EventLogInfoReader eL)
        {
            this.eventLog = eL;
            InitializeComponent();
            Random rnd = new Random();

            this.data = eL.ReadEventLogData();
            cbxBoots.SelectedIndex = 0;

            this.printPreviewDialog = new PrintPreviewDialog();
            this.printDocument = new PrintDocument();
            this.pageSetupDialog = new PageSetupDialog();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        }

        private void drawData(int eventCount)
        {
            List<IEvent> lastX = data.Skip(data.Count - eventCount).ToList();

            SetUpChart(lastX);
            maxBox.Text = FindMaxValue(lastX) / 1000 + "s";
            minBox.Text = FindMinValue(lastX) / 1000 + "s";
            avgBox.Text = FindMean(lastX) / 1000 + "s";
            improvementBox.Text = FindImprovement(lastX) / 1000 + "s";
            barProgress.Value = 50;
        }

        private int FindMaxValue(List<IEvent> list)
        {
            int max = 0;

            foreach (var item in list)
            {
                if (max < item.BootTime())
                {
                    max = item.BootTime();
                }
            }

            return max;
        }

        private int FindMinValue(List<IEvent> list)
        {
            int min = Int32.MaxValue;

            foreach (var item in list)
            {
                if (min > item.BootTime())
                {
                    min = item.BootTime();
                }
            }

            return min;
        }

        private int FindMean(List<IEvent> list)
        {
            int values = list.Count;
            int sum = 0;
            foreach (var item in list)
            {
                sum += item.BootTime();
            }
            return sum / values;
        }

        private int FindImprovement(List<IEvent> list)
        {
            return list.ToArray()[0].BootTime() - list.ToArray()[list.Count() - 1].BootTime();
        }

        private void SetUpChart(List<IEvent> list)
        {
            chart1.Series["Series"].Points.Clear();

            //gather data points from EventLogInfoReader
            for (int events = 0; events < list.Count(); events++)
            {
                barProgress.Value = 0;
                int bootTime = list.ToArray()[events].BootTime() / 1000;
                int mainPath = list.ToArray()[events].MainPath() / 1000;
                int postBoot = list.ToArray()[events].PostBoot() / 1000;
                Color color = Color.LimeGreen;

                switch (list.ToArray()[events].Priority())
                {
                    case 1:
                        color = Color.DarkRed;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                    default:
                        color = Color.LimeGreen;
                        break;
                }
                barProgress.Value = 50;

                createPoint(events, bootTime, color, list);

                //if (postBoot > mainPath)
                //{
                //    createPoint(events, postBoot, Color.Teal, list);
                //    createPoint(events, mainPath, Color.LightSeaGreen, list);
                //}
                //else
                //{
                //    createPoint(events, mainPath, Color.LightSeaGreen, list);
                //    createPoint(events, postBoot, Color.Teal, list);
                //}

                chart1.Series["Series"].Points[events].AxisLabel = list.ToArray()[events].DateTime().Date.ToShortDateString();
                barProgress.Value = 100;
            }
        }

        private int createPoint(int events, int value, Color color, List<IEvent> list)
        {
            int point = chart1.Series["Series"].Points.AddXY(events, value);
            chart1.Series["Series"].Points[point].Color = color;
            chartLabelCheck(list, events, point);
            return point;
        }

        private void chartLabelCheck(List<IEvent> list, int events, int point)
        {
            if (((list.Count() - 1) % 5) != (events % 5) && events != 0 && events != list.Count() - 1)
            {
                chart1.Series["Series"].Points[point].LabelBackColor = Color.Transparent;
                chart1.Series["Series"].Points[point].LabelForeColor = Color.Transparent;
            }
        }

        private void refreshData()
        {
            barProgress.Value = 0;
            lblStatus.Text = "Refreshing...";
            this.data = this.eventLog.ReadEventLogData();
            barProgress.Value = 25;
            showData();
            lblStatus.Text = "Data Refreshed!";
        }

        private void showData()
        {
            int[] math = new int[6];
            math[0] = 2;
            math[1] = 6;
            math[2] = 11;
            math[3] = 16;
            math[4] = 21;
            math[5] = Int32.MaxValue;

            barProgress.Value = 30;

            int count = math[cbxBoots.SelectedIndex];

            if (count > data.Count)
            {
                count = data.Count;
            }

            drawData(count);
            barProgress.Value = 100;
            lblStatus.Text = "Data loaded!";
        }

        private void cbxBoots_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            showData();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font printFont = new Font("Segoe UI", 12);
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            Rectangle titleRect = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, 40);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString("Boot Time Report", titleFont, Brushes.Black, titleRect, format);


            Rectangle rect = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top + 40, e.MarginBounds.Width, e.MarginBounds.Height/2);
            chart1.Printing.PrintPaint(e.Graphics, rect);
            int lineLocation = e.MarginBounds.Top + e.MarginBounds.Height/2 + 50;
            Rectangle pageBounds = new Rectangle(e.MarginBounds.Left, lineLocation, e.MarginBounds.Width, e.MarginBounds.Height / 2);

            int pointCount = (chart1.Series["Series"].Points.Count - 1);
            String pointWord = (pointCount == 1) ? "boot" : "boots";

            double percentageDifference = chart1.Series["Series"].Points[chart1.Series["Series"].Points.Count-1].YValues[0] / chart1.Series["Series"].Points[0].YValues[0];
            percentageDifference = 100 - Math.Round(percentageDifference, 2) * 100;

            String statsTitleString = "Statistics for last " + pointCount + " " + pointWord + ":\n";
            Font statsTitleFont = new Font("Segoe UI", 14, FontStyle.Bold | FontStyle.Underline);

            String statsString = "Average boot time: " + avgBox.Text + "\n" +
                                 "Minimum boot time: " + minBox.Text + "\n" +
                                 "Maximum boot time: " + maxBox.Text + "\n" +
                                 "Time savings: " + percentageDifference + "%\n" +
                                 "Time savings (in seconds): " + improvementBox.Text + "\n";

            e.Graphics.DrawString(statsTitleString, statsTitleFont, Brushes.Black, pageBounds, format);
            pageBounds.Offset(0, 25);
            e.Graphics.DrawString(statsString, printFont, Brushes.Black, pageBounds, format);

            System.Reflection.Assembly myAss = System.Reflection.Assembly.GetExecutingAssembly();
            if (myAss.GetManifestResourceNames().Contains("BootTimeCheck.logo.png"))
            {
                Stream myStream = myAss.GetManifestResourceStream("BootTimeCheck.logo.png");
                Bitmap logo = new Bitmap(myStream);
                Rectangle logoRect = new Rectangle(e.MarginBounds.Right - logo.Width / 5, e.MarginBounds.Bottom - logo.Height / 5, logo.Width / 5, logo.Height / 5);
                e.Graphics.DrawImage(logo, logoRect);
            }
        }
        
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.SetDesktopBounds(100, 100, 600, 800);
            printPreviewDialog.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.P))
            {
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();
                return true;
            }

            if (keyData == (Keys.Control | Keys.X))
            {
                Application.Exit();
                return true;
            }

            if (keyData == Keys.F5)
            {
                refreshData();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public List<IEvent> data { get; set; }

        public EventLogInfoReader eventLog { get; set; }

        public PrintPreviewDialog printPreviewDialog { get; set; }

        public PrintDocument printDocument { get; set; }

        public PageSetupDialog pageSetupDialog { get; set; }
    }
}
