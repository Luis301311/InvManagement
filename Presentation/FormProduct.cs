using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Logic;
using Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentation
{
    public partial class FormProduct : Form
    {
        ProductManagement productManagement = new Logic.ProductManagement(ConfigConnection.connectionString);
        private string NameUser; 
        public FormProduct(string User)
        {
            InitializeComponent();
            this.NameUser = User;   
            GetProduct();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveProduct();
        }

        private void LoadJSON_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = openFileDialog.FileName;
                try
                {
                    productManagement.LoadProductsFromJsonAndSaveToDatabase(jsonFilePath);
                    MessageBox.Show("Datos cargados y guardados correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar y guardar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            GetProduct();
        }

        private void GetProduct()
        {
            var filteredData = productManagement.GetAll().Select(p => new {
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                Reference = p.Reference,    
                Brand = p.Brand,
            }).ToList();
            dataGridViewProduct.DataSource = filteredData;
            dataGridViewProduct.Columns["Code"].HeaderText = "Código";
            dataGridViewProduct.Columns["Barcode"].HeaderText = "Código de barras";
            dataGridViewProduct.Columns["Name"].HeaderText = "Nombre";
            dataGridViewProduct.Columns["Reference"].HeaderText = "Referencia";
            dataGridViewProduct.Columns["Brand"].HeaderText = "Marca";
        }
        private Product ValidateTxtProduct()
        {
            Product product = new Product();
            if(productManagement.Exist(product) == true)
            {
                Message(false); 
            }
            else
            {
                if (txtCode.Text != string.Empty && txtBarcode.Text != string.Empty && txtName.Text != string.Empty && txtReference.Text != string.Empty && txtBrand.Text != string.Empty)
                {
                    if (txtCode.Text.Length > 13)
                    {
                        MessageBox.Show("El código del producto no puede ser mayor a 13 dígitos");
                    }
                    else
                    {
                        product.Code = txtCode.Text;
                    }
                    if (txtBarcode.Text.Length > 13)
                    {
                        MessageBox.Show("El código de barras del producto no puede ser mayor a 13 dígitos");
                    }
                    else
                    {
                        product.Barcode = txtBarcode.Text;
                    }
                    product.Name = txtName.Text;
                    product.Reference = txtReference.Text;
                    product.Brand = txtBrand.Text;
                    product.statu = "1";
                }
                return product;
            }
            return null;
        }

        private void SaveProduct()
        {
            if(ValidateTxtProduct()!= null)Message(productManagement.Add(ValidateTxtProduct()));
            GetProduct();
        }

        public void Message(bool boolean)
        {
            if (boolean == true)
            {
                MessageBox.Show("Proceso realizado exitosamente");
            }
            else
            {
                MessageBox.Show("¡Error! proceso no realizado correcatamente, revise nuevamente.");
            }
        }

        void ClearFields()
        {
            txtCode.Enabled = true;
            txtCode.Text = string.Empty;
            txtBarcode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtReference.Text = string.Empty;
            txtBrand.Text = string.Empty;
        }

        private void btnSingOf_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormSuperUser formSuperUser = new FormSuperUser(NameUser);
            this.Dispose();
            formSuperUser.ShowDialog();
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void Search(string search)
        {
            List<Product> products = new List<Product>();
            var searchLower = search.ToUpper();
            var filteredProducts = productManagement.SearchXProducts(searchLower);
            dataGridViewProduct.DataSource = filteredProducts;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            Search(search);
        }

        private Product ValidadtxtUpdate()
        {
            Product product = new Product();
            if (txtCode.Text != string.Empty && txtBarcode.Text != string.Empty && txtName.Text != string.Empty  && txtBrand.Text != string.Empty)
            {
                if (txtCode.Text.Length > 13)
                {
                    MessageBox.Show("El código del producto no puede ser mayor a 13 dígitos");
                }
                else
                {
                    product.Code = txtCode.Text;
                }
                if (txtBarcode.Text.Length > 13)
                {
                    MessageBox.Show("El código de barras del producto no puede ser mayor a 13 dígitos");
                }
                else
                {
                    product.Barcode = txtBarcode.Text;
                }
                product.Reference = txtReference.Text;
                product.Name = txtName.Text;
                product.Brand = txtBrand.Text;
            }
            return product;
        }

        void UpdateProduct()
        {
            Message(productManagement.Update(ValidadtxtUpdate()));
            GetProduct();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
        }

        private void View(Product product)
        {
            if (product != null)
            {
                txtCode.Text = product.Code;
                txtBarcode.Text = product.Barcode;
                txtName.Text = product.Name;
                txtReference.Text = product.Reference;
                txtBrand.Text = product.Brand;
                btnUpdate.Enabled = true;
                btnGuardar.Enabled = false;
            }
        }

        int file;

        private void IndividualSearch(string code)
        {
            var product = productManagement.GetByCode(code);
            View(product);
        }
        private void dataGridViewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            file = e.RowIndex; 
            string product = dataGridViewProduct.Rows[file].Cells[0].Value.ToString();
            IndividualSearch(product);
            txtCode.Enabled = false;
            tabControl1.SelectTab(0);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtCode.Enabled = true;
            btnGuardar.Enabled = true; 
            btnUpdate.Enabled = false;
        }
    }
}
