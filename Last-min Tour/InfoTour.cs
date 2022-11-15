using Last_min_Tour.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Last_min_Tour
{
    public partial class InfoTour : Form
    {
        private readonly Tour tours;
        public string text = "Турция";
        public InfoTour()
        {
            InitializeComponent();
            FillDirection();
            this.Text = "Добавление тура";
            tours = new Tour
            {
                Direction = Direction.Turkey,
                Departure = DateTime.Now.AddDays(5),
                NumberOfNights = (int)NumberOfNigthsNumeric.Value,
                CostVacationer = decimal.Parse(CostVacationerTextBox.Text),
                NumberOfVacationers = (int)NumberOfVacationersNumeric.Value,
                Surcharges = decimal.Parse(SurchargesTextBox.Text),


            };
            DirectionComboBox.SelectedItem = tours.Direction;
            DepartureDateTimePicker.Value = tours.Departure;
            NumberOfNigthsNumeric.Value = tours.NumberOfNights;
            CostVacationerTextBox.Text = tours.CostVacationer.ToString();
            NumberOfVacationersNumeric.Value = tours.NumberOfVacationers;
            SurchargesTextBox.Text = tours.Surcharges.ToString();




        }

        public InfoTour(Tour source) : this()
        {
            this.Text = "Редактирование тура";
            DirectionComboBox.SelectedItem = source.Direction;
            DepartureDateTimePicker.Value = source.Departure;
            NumberOfNigthsNumeric.Value = source.NumberOfNights;
            CostVacationerTextBox.Text = source.CostVacationer.ToString();
            NumberOfVacationersNumeric.Value = source.NumberOfVacationers;
            WiFiCheckBox.Checked = source.WiFi;
            SurchargesTextBox.Text = source.Surcharges.ToString();

        }
        public Tour Tours => tours;

        private void FillDirection()
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                DirectionComboBox.Items.Add(dir);

            }
        }

        private void DirectionBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var parent = sender as ComboBox;
            if (parent != null)
            {
                e.DrawBackground();
                Brush brush = new SolidBrush(parent.ForeColor);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }
                if (e.Index >= 0)
                {
                    if (parent.Items[e.Index] is Direction direction)
                    {
                        switch (direction)
                        {
                            case Direction.Turkey:
                                text = "Турция";
                                break;
                            case Direction.Spain:
                                text = "Испания";
                                break;
                            case Direction.Italy:
                                text = "Италия";
                                break;
                            case Direction.France:
                                text = "Франция";
                                break;
                            case Direction.Shushary:
                                text = "Шушары";
                                break;                           
                        }

                        e.Graphics.DrawString(
                            text,
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                    else
                    {
                        e.Graphics.DrawString(
                                parent.Items[e.Index].ToString(),
                                parent.Font,
                                brush,
                                e.Bounds);
                    }
                }

            }
        }

        private void DirectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DirectionComboBox.SelectedIndex >= 0)
            {
                tours.Direction = (Direction)DirectionComboBox.SelectedItem;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tours.Departure = DepartureDateTimePicker.Value;
        }

        private void numericNumberNigth_ValueChanged(object sender, EventArgs e)
        {
            tours.NumberOfNights = (int)NumberOfNigthsNumeric.Value;
        }

        private void CostVacBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void CostVacBox1_TextChanged(object sender, EventArgs e)
        {
            if (CostVacationerTextBox.Text.Length > 0)
            {
                tours.CostVacationer = decimal.Parse(CostVacationerTextBox.Text);
            }

        }

        private void numericNumberVac_ValueChanged(object sender, EventArgs e)
        {
            tours.NumberOfVacationers = (int)NumberOfVacationersNumeric.Value;
        }

        private void Wi_FiChangheBox_CheckedChanged(object sender, EventArgs e)
        {
            tours.WiFi = WiFiCheckBox.Checked;
        }

        private void SurBox_TextChanged(object sender, EventArgs e)
        {
            if (SurchargesTextBox.Text.Length > 0)
            {
                tours.Surcharges = decimal.Parse(SurchargesTextBox.Text);
            }
        }

        private void SurBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
    }

