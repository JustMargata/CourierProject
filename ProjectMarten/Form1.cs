using ProjectMarten.Controller;
using ProjectMarten.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMarten
{
    public partial class Form1 : Form
    {
        ParcelController parcelController = new ParcelController();
        ParcelTypeController parcelTypeController = new ParcelTypeController();
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadRecord(Parcel parcel)
        {
            numId.Text = parcel.Id.ToString();
            txtName.Text = parcel.Name;
            txtDescription.Text = parcel.Description.ToString();
            numPrice.Text = parcel.Price.ToString();
            numWeight.Text = parcel.Weight.ToString();
            cmbType.Text = parcel.ParcelTypes.Name;
        }
        private void ClearScreen()
        {
            txtDescription.Clear();
            txtName.Clear();
            numId.Value = 0;
            numPrice.Value = 0;
            numWeight.Value = 0;
            cmbType.Text = "";
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckIfNull();
            Parcel newParcel = new Parcel
            {
                Id = (int)numId.Value,
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = numPrice.Value,
                Weight = numWeight.Value,
                ParcelTypeId = (int)cmbType.SelectedValue
            };
            parcelController.Create(newParcel);
            MessageBox.Show("Успешно добавихте пратка", "ГОТОВО!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearScreen();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Parcel> allParcels = parcelController.GetAll();
            List<ParcelType> allParcelTypes = parcelTypeController.GetParcelTypes();
            cmbType.DataSource = allParcelTypes;
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "Id";
            foreach (var item in allParcels)
            {
                listBox1.Items.Add($"Id: {item.Id}, Name: {item.Name}, Description: {item.Description}," +
                    $" Price: {item.Price}, Weight: {item.Weight}, Type: {item.ParcelTypes.Name}");
            }

        }
        private void CheckIfNull()
        {
            if (numId.Value == 0)
            {
                MessageBox.Show("Не сте въвели Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Не сте въвели име", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            if (txtDescription.Text == "")
            {
                MessageBox.Show("Не сте въвели описание", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
                return;
            }
            if (numPrice.Value == 0)
            {
                MessageBox.Show("Не сте въвели цена", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numPrice.Focus();
                return;
            }
            if (numWeight.Value == 0)
            {
                MessageBox.Show("Не сте въвели тегло", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numWeight.Focus();
                return;
            }
            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Не сте избрали тип", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbType.Focus();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (numId.Value == 0)
            {
                MessageBox.Show("Не сте въвели Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                findId = (int)numId.Value;
            }
            Parcel findedParcel = parcelController.Get(findId);
            if (findedParcel == null)
            {
                MessageBox.Show("Не е намерена пратка с такова Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                LoadRecord(findedParcel);
                Parcel updatedParcel = new Parcel
                {
                    Id = (int)numId.Value,
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Price = numPrice.Value,
                    Weight = numWeight.Value,
                    ParcelTypeId = (int)cmbType.SelectedValue
                };
                parcelController.Update(findId, updatedParcel);
                MessageBox.Show($"Успешно обновихте пратка номер {findId}", "ГОТОВО!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearScreen();
            btnSelectAll_Click(sender, e);   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (numId.Value == 0)
            {
                MessageBox.Show("Не сте въвели Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                findId = (int)numId.Value;
            }
            Parcel findedParcel = parcelController.Get(findId);
            if (findedParcel == null)
            {
                MessageBox.Show("Не е намерена пратка с такова Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Сигурни ли сте, че искате да изтриете пратка номер {findId}?", "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    parcelController.Delete(findId);
                    MessageBox.Show($"Успешно изтрихте пратка номер {findId}", "ГОТОВО!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearScreen();
                }
            }
            btnSelectAll_Click(sender, e);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int findId = 0;
            if (numId.Value == 0)
            {
                MessageBox.Show("Не сте въвели Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                findId = (int)numId.Value;
            }
            Parcel findedParcel = parcelController.Get(findId);
            if (findedParcel == null)
            {
                MessageBox.Show("Не е намерена пратка с такова Id", "ГРЕШКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numId.Focus();
                return;
            }
            else
            {
                LoadRecord(findedParcel);
                MessageBox.Show($"Успешно намерихте пратка номер {findId}", "ГОТОВО!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<Parcel> allParcels = parcelController.GetAll();
            listBox1.Items.Clear();
            foreach(var item in allParcels)
            {
                listBox1.Items.Add($"Id: {item.Id}, Name: {item.Name}, Description: {item.Description}," +
                    $" Price: {item.Price}, Weight: {item.Weight}, Type: {item.ParcelTypes.Name}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
