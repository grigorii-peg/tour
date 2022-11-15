using Last_min_Tour.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Last_min_Tour
{
    public partial class Form1 : Form
    {
        private readonly List<Tour> tours;

        private readonly BindingSource BinSource;
        private decimal sum = 0;
        public Form1()
        {
            InitializeComponent();
            TourGridView.AutoGenerateColumns = false;
            tours = new List<Tour>();

            BinSource = new BindingSource();
            BinSource.DataSource = tours;
            TourGridView.DataSource = BinSource;
        }

        private void Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пегушин Григорий, ИП-20-3", "Горящие туры",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddTool_Click(object sender, EventArgs e)
        {
            var infoForm = new InfoTour();

            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                tours.Add(infoForm.Tours);
                BinSource.ResetBindings(false);
                CalculatScroll();

            }

        }

        private void DeliteTool_Click(object sender, EventArgs e)
        {
            var id = (Tour)TourGridView.Rows[TourGridView.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы действительно хотите удалить {id.Direction} в {id.Departure:D}",
                "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                tours.Remove(id);
                BinSource.ResetBindings(false);
                CalculatScroll();



            }
        }


        private void ChangeTool_Click(object sender, EventArgs e)
        {
            var id = (Tour)TourGridView.Rows[TourGridView.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new InfoTour(id);
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                id.Direction = infoForm.Tours.Direction;
                id.Departure = infoForm.Tours.Departure;
                id.NumberOfNights = infoForm.Tours.NumberOfNights;
                id.CostVacationer = infoForm.Tours.CostVacationer;
                id.NumberOfVacationers = infoForm.Tours.NumberOfVacationers;
                id.WiFi = infoForm.Tours.WiFi;
                id.Surcharges = infoForm.Tours.Surcharges;

                BinSource.ResetBindings(false);
                CalculatScroll();


            }
        }

        private void AddMenu_Click(object sender, EventArgs e)
        {
            AddTool_Click(sender, e);
        }

        private void DeliteMenu_Click(object sender, EventArgs e)
        {
            DeliteTool_Click(sender, e);
        }

        private void ChangeMenu_Click(object sender, EventArgs e)
        {
            ChangeTool_Click(sender, e);
        }

        private void ToursGridViev_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (TourGridView.Columns[e.ColumnIndex].Name == "AmountAllColumn")
            {
                var data = (Tour)TourGridView.Rows[e.RowIndex].DataBoundItem;
                sum += (data.NumberOfNights * data.NumberOfVacationers * data.CostVacationer) + data.Surcharges;
                e.Value = sum;
                sum = 0;
            }

            if (TourGridView.Columns[e.ColumnIndex].Name == "DirectColumn")
            {
                var val = (Direction)e.Value;
                switch (val)
                {
                    case Direction.Turkey:
                        e.Value = "Турция";
                        break;
                    case Direction.Spain:
                        e.Value = "Испания";
                        break;
                    case Direction.Italy:
                        e.Value = "Италия";
                        break;
                    case Direction.France:
                        e.Value = "Франция";
                        break;
                    case Direction.Shushary:
                        e.Value = "Шушары";
                        break;
            
                }
            }
            if (TourGridView.Columns[e.ColumnIndex].Name == "WiFiColumn")
            {
                var val = (bool)e.Value;
                switch (val)
                {
                    case true:
                        e.Value = "Есть";
                        break;
                    case false:
                        e.Value = "Нет";
                        break;

                }
            }


        }

        private void ToursGridViev_SelectionChanged(object sender, EventArgs e)
        {
            DeleteMenuItem.Enabled =
            EditMenuItem.Enabled =
            DeleteStripButton.Enabled =
            EditStripButton.Enabled =
            TourGridView.SelectedRows.Count > 0;
        }
        private void CalculatScroll()
        {
            NumberOfToursStatus.Text = $"Всего туров {tours.Count.ToString()}";

            decimal sumAll = tours.Sum(x => (x.NumberOfNights * x.NumberOfVacationers * x.CostVacationer) + x.Surcharges);
            decimal sumAllSur = tours.Sum(x => x.Surcharges);

            TotalCostStatus.Text = $"Общая сумма {sumAll}";
            int SurCount = tours.Where(x => x.Surcharges != 0).Count();
            NumberOfToursWithSurchargesStatus.Text = $"Кол-во туров с доплатами {SurCount}";
            TotalCostSurchargesStatus.Text = $"Общая сумма доплат {sumAllSur}";

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
