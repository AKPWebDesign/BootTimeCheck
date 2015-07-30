using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            EventLogInfoReader eventLog = eL;
            InitializeComponent();
            Random rnd = new Random();

            this.data = eL.ReadEventLogData();
            cbxBoots.SelectedIndex = 0;
        }

        private void drawData(int eventCount)
        {
            List<IEvent> lastX = data.Skip(data.Count - eventCount).ToList();

            SetUpChart(lastX);
            maxBox.Text = FindMaxValue(lastX) / 1000 + "s";
            minBox.Text = FindMinValue(lastX) / 1000 + "s";
            avgBox.Text = FindMean(lastX) / 1000 + "s";
            improvementBox.Text = FindImprovement(lastX) / 1000 + "s";
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

        private void cbxBoots_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int[] math = new int[6];
            math[0] = 2;
            math[1] = 6;
            math[2] = 11;
            math[3] = 16;
            math[4] = 21;
            math[5] = Int32.MaxValue;

            int count = math[cbxBoots.SelectedIndex];

            if (count > data.Count)
            {
                count = data.Count;
            }
            drawData(count);
        }

        public List<IEvent> data { get; set; }
    }
}
