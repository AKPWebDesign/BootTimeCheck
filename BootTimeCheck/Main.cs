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

            SetUpChart(data, 2);
            maxBox.Text = FindMaxValue(data) / 1000 + "s";
            minBox.Text = FindMinValue(data) / 1000 + "s";
            avgBox.Text = FindMean(data) / 1000 + "s";
            cbxBoots.SelectedIndex = 0;
            improvementBox.Text = FindImprovement(data, 2) / 1000 + "s";
        }

        private int FindMaxValue(List<int> list)
        {
            int max = 0;

            foreach (var item in list)
            {
                if (max < item)
                {
                    max = item;
                }
            }

            return max;
        }

        private int FindMinValue(List<int> list)
        {
            int min = Int32.MaxValue;

            foreach (var item in list)
            {
                if (min > item)
                {
                    min = item;
                }
            }

            return min;
        }

        private int FindMean(List<int> list)
        {
            int values = list.Count;
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum / values;
        }

        private int FindImprovement(List<int> list, int numberOfEvents)
        {
            if (numberOfEvents > list.Count)
            {
                numberOfEvents = list.Count;
            }
            List<int> query = list.Distinct().ToList();
            List<int> lastX = query.Skip(query.Count - numberOfEvents).ToList();

            return lastX.ToArray()[0] - lastX.ToArray()[numberOfEvents - 1];
        }

        private void SetUpChart(List<int> list, int numberOfEvents)
        {
            if (numberOfEvents > list.Count)
            {
                numberOfEvents = list.Count;
            }

            chart1.Series["Series"].Points.Clear();

            List<int> query = list.Distinct().ToList();
            List<int> lastX = query.Skip(query.Count - numberOfEvents).ToList();

            if (numberOfEvents > lastX.Count)
            {
                numberOfEvents = lastX.Count;
            }

            //gather data points from EventLogInfoReader
            for (int events = 0; events < numberOfEvents; events++)
            {
                chart1.Series["Series"].Points.AddY(lastX.ToArray()[events] / 1000);
                chart1.Series["Series"].Points[events].AxisLabel = "Boot "+(events+1);
                if ((events % 5) != 0 && events != 0 && events != numberOfEvents-1)
                {
                    chart1.Series["Series"].Points[events].LabelBackColor = Color.Transparent;
                    chart1.Series["Series"].Points[events].LabelForeColor = Color.Transparent;
                }
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

            improvementBox.Text = FindImprovement(data, math[cbxBoots.SelectedIndex]) / 1000 + "s";
            SetUpChart(data, math[cbxBoots.SelectedIndex]);
        }

        public List<int> data { get; set; }
    }
}
