using Entity;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class FormCreateInventory : Form
    {
        private string User_name;
        public FormCreateInventory(string User)
        {
            InitializeComponent();
            this.User_name = User;  
            GetInventorys();
        }

        Logic.InventoryManagment inventoryManagment = new Logic.InventoryManagment(ConfigConnection.connectionString);
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void FormCreateInventory_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private Inventory ValidateInventory()
        {
            Inventory inventory = new Inventory();

            if (StartDate.SelectionStart == DateTime.MinValue && FinalDate.SelectionStart == DateTime.MinValue)
            {
                MessageBox.Show("Por favor, seleccione las fechas del inventario para poder guardar.");
            }
            else
            {
                DateTime startDate = StartDate.SelectionStart;
                DateTime finalDate = FinalDate.SelectionStart;

                if (startDate < SqlDateTime.MinValue.Value || startDate > SqlDateTime.MaxValue.Value || finalDate < SqlDateTime.MinValue.Value || finalDate > SqlDateTime.MaxValue.Value)
                {
                    MessageBox.Show("Las fechas del inventario deben estar dentro del rango permitido.");
                }
                else
                {
 
                        if (startDate < finalDate)
                        { 
                            inventory.Inv_Date = startDate;
                            inventory.FinalDate = finalDate;
                            
                        }
                        else if (startDate >= finalDate)
                        {
                            MessageBox.Show("ERROR. La fecha final no puede ser mayor o igual a la fecha inicial");
                        }
               
                }
            }
            inventory.UserName = User_name; 

            return inventory;
        }
        public void Message(bool boolean)
        {
            if (boolean == true)
            {
                MessageBox.Show("Proceso realizado exitosamente.");
            }
            else
            {
                MessageBox.Show("¡Error! proceso no realizado correcatamente, revise nuevamente.");
            }
        }

        private void Save()
        {
  
                Message(inventoryManagment.Add(ValidateInventory()));
                //GetInventorys();
 
        }
        private void GetInventorys()
        {
            var filteredData = inventoryManagment.GetAll().Select(p => new {
                Id_Inventory = p.Id_Inventory,
                Inv_Date = p.Inv_Date,
                FinalDate = p.FinalDate,
                UserName = p.UserName,
                statu = (Convert.ToInt32(p.statu) == 1) ? "Activo" : "Cerrado"
            }).ToList();

            dataGridView1.DataSource = filteredData;
            dataGridView1.Columns["Id_Inventory"].HeaderText = "Id del Inventario";
            dataGridView1.Columns["Inv_Date"].HeaderText = "Fecha de Inicio";
            dataGridView1.Columns["FinalDate"].HeaderText = "Fecha de Cierre";
            dataGridView1.Columns["UserName"].HeaderText = "Creador de Del Inventario";
            dataGridView1.Columns["statu"].HeaderText = "Estado";
        }

        private void btnCreateInventory_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            GetInventorys();
        }

        private void btnSingOf_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormSuperUser formSuperUser = new FormSuperUser(User_name);
            this.Dispose();
            formSuperUser.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInventarioAdmin formAdmin = new FormInventarioAdmin("Vegam");
            this.Dispose();
            formAdmin.ShowDialog();
        }
    }
}
