using Entity;
using Logic;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Org.BouncyCastle.Crmf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class FormInventarioUser : Form
    {
        //Colores
        Color ColorAzulOscuro = Color.FromArgb(64, 131, 146);
        ProductManagement productManagement = new Logic.ProductManagement(ConfigConnection.connectionString);
        ProductInventoryManagement productInventoryManagement = new ProductInventoryManagement(ConfigConnection.connectionString);
        InventoryManagment inventoryManagment = new InventoryManagment(ConfigConnection.connectionString);
        private List<Product_InventoryJoin> ProductsJoin;
        private List<Product> products;
        Inventory Inventory = new Inventory();
        User user = new User();
        private string Name_user;
        string CodeProductDelete = null;
        public FormInventarioUser(string User)
        {
            InitializeComponent();
            Name_user = User;
            this.ControlBox = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnClearFiles.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSingOf.FlatAppearance.BorderSize = 0;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatAppearance.BorderSize = 0;
            Btnsee.FlatAppearance.BorderSize = 0;   
            Btnsee.Enabled = true;
            groupBox2.BackColor = ColorAzulOscuro;
        }
        private void FormInventarioUser_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadInventoryComboBox();
            GetAllProducts();
            GetAllProductInventories();
            cbIva.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIdInventory.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void btnSingOf_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveInventoryProduct())
            {
                Message(true);
                GetAllProductInventories();
            }
            else
            {
                Message(false);
            }

        }


        private Inventory ValidateInventory()
        {
            List<Inventory> inventories;
            inventories = inventoryManagment.GetAll();
            foreach (var item in inventories)
            {
                if (item.Id_Inventory == Inventory.Id_Inventory)
                {
                    return item;
                }
            }
            return Inventory;
        }

        public void LoadInventoryComboBox()
        {
            cbIdInventory.DataSource = inventoryManagment.GetAllInventoryIds();
        }

        private Product_Inventory Save()
        {
            try
            {
                Product_Inventory product_Inventory = new Product_Inventory();
                product_Inventory.Code = txtCodeProduct.Text;
                int selectedIndex = cbIdInventory.SelectedIndex + 1;
                product_Inventory.Id_Inventory = selectedIndex;

                // Eliminar todas las comas del valor del precio
                string priceWithoutCommas = txtPrice.Text.Replace(",", "");

                // Convertir el valor del precio a double y guardarlo en la entidad
                product_Inventory.Price = Convert.ToDouble(priceWithoutCommas, CultureInfo.InvariantCulture);

                product_Inventory.Margin = Convert.ToDouble(txtMargin.Text, CultureInfo.InvariantCulture);
                product_Inventory.Amount = Convert.ToDouble(txtAmout.Text);

                // Obtener el valor seleccionado en el ComboBox de IVA
                string selectedIva = cbIva.SelectedItem.ToString();

                // Establecer el valor de IVA según la opción seleccionada
                double ivaValue = 1.0; // Por defecto, sin IVA
                if (selectedIva == "19%")
                {
                    ivaValue = 1.19;
                }
                else if (selectedIva == "5%")
                {
                    ivaValue = 1.05;
                }
                // No hacemos nada si es 0%

                // Calcular el valor de PriceToCost según el IVA seleccionado
                double temporaryPriceToCost = product_Inventory.CalculatePriceToCost();
                product_Inventory.PriceToCost = temporaryPriceToCost / ivaValue;

                product_Inventory.Iva = selectedIva;
                product_Inventory.NameUser = Name_user; // Aquí va el nombre de Usuario que hace las operaciones de digitador
                return product_Inventory;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        private bool SaveRegisterHistory(Product_Inventory product_Inventory, string description)
        {
            try
            {
                RegisterHistory registerHistory = new RegisterHistory();
                registerHistory.Id_Inventory = product_Inventory.Id_Inventory;
                registerHistory.Id_Product = product_Inventory.Code;
                registerHistory.UserName = Name_user;
                registerHistory.Date_Register = DateTime.Now;
                registerHistory.Description = description;

                return productInventoryManagement.AddRegisterHistory(registerHistory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }

        private bool SaveInventoryProduct()
        {
            try
            {
                Product_Inventory product_Inventory = Save();
                if (product_Inventory != null)
                {
                    return (productInventoryManagement.Add(product_Inventory) && SaveRegisterHistory(product_Inventory, "Inserto producto"));
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        private void GetAllProductInventories()
        {
            if(cbIdInventory != null && cbIdInventory.SelectedValue != null)
            {
                string id = cbIdInventory.SelectedValue.ToString();
                ProductsJoin = productInventoryManagement.GetAllProductInventorys(id);
            }
            
            if(ProductsJoin != null)
            {
                var filteredData = ProductsJoin.Select(p => new
                {
                    Code = p.Code,
                    Barcode = p.Barcode,
                    Name_product = p.Name_product,
                    Refence = p.Refence,
                    Brand = p.Brand,
                    Price = p.Price,
                    Margin = p.Margin,
                    PriceToCost = p.PriceToCost,
                    Amount = p.Amount,
                    Iva = p.Iva,
                }).ToList();
                dataGridView1.DataSource = filteredData;
                dataGridView1.Columns["Code"].HeaderText = "Código";
                dataGridView1.Columns["Barcode"].HeaderText = "Código de barras";
                dataGridView1.Columns["Name_product"].HeaderText = "Nombre";
                dataGridView1.Columns["Refence"].HeaderText = "Referencia";
                dataGridView1.Columns["Brand"].HeaderText = "Marca";
                dataGridView1.Columns["PriceToCost"].HeaderText = "Precio de costo";
                dataGridView1.Columns["Margin"].HeaderText = "Margen";
                dataGridView1.Columns["Iva"].HeaderText = "Iva";
                dataGridView1.Columns["Price"].HeaderText = "Precio de venta";
                dataGridView1.Columns["Amount"].HeaderText = "Cantidad";
            }
            
        }
        private void GetAllProducts()
        {
            products = productManagement.GetAll();
            var filteredData = products.Select(p => new {
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                Reference = p.Reference,
                Brand = p.Brand,
            }).ToList();
            dataGridView2.DataSource = filteredData;
            dataGridView2.Columns["Code"].HeaderText = "Código";
            dataGridView2.Columns["Barcode"].HeaderText = "Código de barras";
            dataGridView2.Columns["Name"].HeaderText = "Nombre";
            dataGridView2.Columns["Reference"].HeaderText = "Referencia";
            dataGridView2.Columns["Brand"].HeaderText = "Marca";
        }
        public void Message(bool boolean)
        {
            if (boolean == true)
            {
                MessageBox.Show("Proceso realizado exitosamente");
            }
            else
            {
                MessageBox.Show("¡Error! proceso no realizado correctamente, revise nuevamente.");
            }
        }

        void SearchProductInventary(string search)
        {
            try
            {
                List<Product_InventoryJoin> ProductsJoinFilter = new List<Product_InventoryJoin>();
                var searchLower = search.ToUpper();
                foreach (var item in ProductsJoin)
                {
                if (ProductsJoin != null)
                {
                    if (item.Code.Contains(searchLower) || (item.Barcode != null && item.Barcode.Contains(searchLower)) || item.Name_product.ToUpper().Contains(searchLower) )
                    {
                        ProductsJoinFilter.Add(item);
                    }
                }

            }
                dataGridView1.DataSource = ProductsJoinFilter;
        }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

}
        private void ViewProduct(string code)
        {
            try
            {
                Product product = new Product();
                foreach (var item in products)
                {
                    if (item.Code == code)
                    {
                        product = item;
                        break;
                    }
                }
                if (ProductsJoin != null)
                {
                    foreach (var item in ProductsJoin)
                    {
                        if (item.Code == product.Code)
                        {
                            txtCodeProduct.Text = item.Code;
                            txtNameProduct.Text = item.Name_product;

                            // Aquí se establece el valor "0" en txtAmount
                            txtAmout.Text = "0";

                            // Aquí se formatea el valor de txtMargin
                            txtMargin.Text = item.Margin.ToString().Replace(',', '.');

                            txtPrice.Text = item.Price.ToString(); // Asumiendo que Price es numérico
                            cbIva.Text = item.Iva;
                            cbIva.SelectedIndex = item.Iva == "19%" ? 0 : item.Iva == "5%" ? 1 : 2;
                            btnSave.Enabled = false;
                            btnUpdate.Enabled = true;
                            break;
                        }
                        else
                        {
                            txtCodeProduct.Text = product.Code;
                            txtNameProduct.Text = product.Name;
                            txtAmout.Text = string.Empty;
                            txtMargin.Text = string.Empty;
                            txtPrice.Text = string.Empty;
                            btnSave.Enabled = true;
                            btnUpdate.Enabled = false;
                        }
                    }
                }
                else
                {
                    txtCodeProduct.Text = product.Code;
                    txtNameProduct.Text = product.Name;
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = false;
                }
                
            }
            catch (Exception exe)
            {
                Console.WriteLine($"Error: {exe.Message}");
            }
        }


        private void ViewProductInventory(string product)
        {

            foreach (var item in ProductsJoin)
            {
                if (item.Code == product)
                {
                    txtCodeProduct.Text = item.Code;
                    txtNameProduct.Text = item.Name_product;

                    // Aquí se establece el valor "0" en txtAmount
                    txtAmout.Text = "0";

                    // Aquí se formatea el valor de txtMargin
                    txtMargin.Text = item.Margin.ToString().Replace(',', '.');

                    txtPrice.Text = item.Price.ToString(); // Asumiendo que Price es numérico
                    cbIva.Text = item.Iva;
                    cbIva.SelectedIndex = item.Iva == "19%" ? 0 : item.Iva == "5%" ? 1 : 2;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    break;
                }
            }


        }
        private void IndividualSearchProducts(string code)
        {
            ViewProduct(code);
        }
        int file;




        private void UpdateInventoryProduct()
        {
            Product_Inventory product_Inventory = new Product_Inventory();
            product_Inventory = Save();
            if (product_Inventory != null)
            {
                Message(/*SaveRegisterHistory(product_Inventory, "Modifico producto") &&*/
                    productInventoryManagement.Update(product_Inventory)
                    );
            }
        }

        void SearchProduct(string search)
        {
            List<Product> products = new List<Product>();
            var searchLower = search.ToUpper();
            var filteredProducts = productManagement.SearchXProducts(searchLower);
            dataGridView2.DataSource = filteredProducts;
            //if (filteredProducts != null)
            //{
            //    dataGridView2.DataSource = filteredProducts;
            //}
            //else
            //{
            //    products = productManagement.SearchXProductsALLDB(searchLower);
            //    dataGridView2.DataSource = products;
            //}

        }

        private object SearchXProducts(string worth)
        {
            List<Product> filter = new List<Product>();
            foreach (var item in products)
            {
                if (item.Name.ToUpper().Contains(worth) || item.Code.ToUpper().Contains(worth) || item.Brand.ToUpper().Contains(worth) || (item.Barcode != null && item.Barcode.ToUpper().Contains(worth))
                    || (item.Reference != null && item.Reference.ToUpper().Contains(worth)))
                {
                    filter.Add(item);
                }
            }
            return filter;
        }

        private void btnClearFiles_Click_1(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtCodeProduct.Text = string.Empty;
            cbIdInventory.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtMargin.Text = string.Empty;
            txtAmout.Text = string.Empty;
            txtNameProduct.Text = string.Empty;
            cbIva.Text = string.Empty;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateInventoryProduct();
            GetAllProductInventories();
        }


        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            deleteProductInInvetary();
            GetAllProductInventories();
        }

        private void deleteProductInInvetary()
        {
            if (CodeProductDelete != null)
            {
                DialogResult result = MessageBox.Show("¿Seguro que quieres cambiar eliminar este producto del invetario?", "Confirmación", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Product_Inventory product_Inventory = new Product_Inventory();
                    product_Inventory = productInventoryManagement.GetByCode(CodeProductDelete);
                    Message(SaveRegisterHistory(product_Inventory, "Elimino producto") && productInventoryManagement.remove(product_Inventory));
                }
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            SearchProduct(search);
        }


        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                file = e.RowIndex;
                string product = dataGridView1.Rows[file].Cells[0].Value.ToString();
                foreach (var item in ProductsJoin)
                {
                    if (item.Code == product)
                    {
                        CodeProductDelete = item.Code;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void txtSearch2_TextChanged_1(object sender, EventArgs e)
        {
            var search = txtSearch2.Text;
            SearchProductInventary(search);
        }
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPrice_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMargin_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtAmout_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') != -1)
            {
                e.Handled = true;
            }
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex == -1 || e.ColumnIndex == -1)) // Si la celda clicada es una celda de encabezado
            {
                file = e.RowIndex;
                string product = dataGridView2.Rows[file].Cells[0].Value.ToString();
                IndividualSearchProducts(product);
                txtCodeProduct.Enabled = false;
                txtNameProduct.Enabled = false;
                tabControl1.SelectTab(0);
                
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        int indexDataGripJoin = 0;
        string ProductoJoinInRow;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView.HitTestInfo hitTestInfo = dataGridView1.HitTest(e.X, e.Y);

                    if (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
                    {
                        dataGridView1.Rows[indexDataGripJoin].Selected = false;
                        indexDataGripJoin = hitTestInfo.RowIndex;
                        ProductoJoinInRow = dataGridView1.Rows[indexDataGripJoin].Cells[0].Value.ToString();
                        dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;
                        contextMenuStrip1.Show(dataGridView1, e.Location);
                    }
                }
            }
            catch(Exception exe)
            {
                Console.WriteLine(exe.ToString());
            }
        }

  
        private void cbIdInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllProductInventories();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            //ViewProductInventory(ProductoJoinInRow);
            IndividualSearchProducts(ProductoJoinInRow);
            txtCodeProduct.Enabled = false;
            txtNameProduct.Enabled = false;
            tabControl1.SelectTab(0);
            btnUpdate.Enabled = true; 

        }

    

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtIva_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodeProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmout_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMargin_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            string textWithoutSeparators = txtPrice.Text.Replace(",", "").Replace(".", "");

            // Tratar de analizar el valor actual del TextBox como un número entero
            if (int.TryParse(textWithoutSeparators, NumberStyles.Integer, CultureInfo.InvariantCulture, out int number))
            {
                // Formatear el número con separadores de miles y mostrarlo en el TextBox
                txtPrice.TextChanged -= txtPrice_TextChanged; // Temporalmente deshabilitar el evento para evitar un bucle infinito
                txtPrice.Text = FormatNumberWithSeparators(number);
                txtPrice.SelectionStart = txtPrice.Text.Length; // Colocar el cursor al final
                txtPrice.TextChanged += txtPrice_TextChanged; // Volver a habilitar el evento
            }
        }


        private string FormatNumberWithSeparators(decimal number)
        {
            // Obtener la cadena formateada con separadores de miles
            string formattedNumber = string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", number);
            return formattedNumber;
        }

        private void label12_Click(object sender, EventArgs e)
        {

            
        }

        private void txtSee_Click(object sender, EventArgs e)
        {
            if ((btnSave.Enabled || btnUpdate.Enabled) && txtCodeProduct.Text != string.Empty)
            {
                tabControl1.SelectedIndex = 2;
                txtSearch2.Text = txtCodeProduct.Text;
            }
        }

        private void FormInventarioUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                if ((btnSave.Enabled || btnUpdate.Enabled) && !string.IsNullOrEmpty(txtCodeProduct.Text))
                {
                    tabControl1.SelectedIndex = 2;
                    txtSearch2.Text = txtCodeProduct.Text;
                }
            }
        }
    }
}

