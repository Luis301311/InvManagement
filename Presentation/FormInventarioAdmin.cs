using DocumentFormat.OpenXml.Spreadsheet;
using Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Logic;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using SpreadsheetLight;
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
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentation
{
    public partial class FormInventarioAdmin : Form
    {
        ProductManagement productManagement = new Logic.ProductManagement(ConfigConnection.connectionString);
        ProductInventoryManagement productInventoryManagement = new ProductInventoryManagement(ConfigConnection.connectionString);
        InventoryManagment inventoryManagment = new InventoryManagment(ConfigConnection.connectionString);
        private List<Product_InventoryJoin> ProductsJoin;
        Inventory Inventory = new Inventory();
        User user = new User();
        string Id_Inventary;

        
        public FormInventarioAdmin(string user)
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        
        private void FormInventario_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            //GetAllProducts();
            GetAllProductInventories();
            LoadInventoryComboBox();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private Inventory ValidateInventory()
        {
            List<Inventory> inventories;
            inventories = inventoryManagment.GetAll();
            foreach(var item in  inventories)
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
            //cbIdInventory.DataSource = inventoryManagment.GetAllInventoryIds();
        }

        ////private Product_Inventory Save()
        //{
        //    /*Product_Inventory product_Inventory = new Product_Inventory();
        //    product_Inventory.Code = txtCodeProduct.Text;
        //    int selectedIndex = cbIdInventory.SelectedIndex + 1;
        //    product_Inventory.Id_Inventory = selectedIndex; 
        //    product_Inventory.Price = Convert.ToDouble(txtPrice.Text);
        //    product_Inventory.Margin = Convert.ToDouble(txtMargin.Text, CultureInfo.InvariantCulture);
        //    product_Inventory.Amount= Convert.ToDouble(txtAmout.Text);
        //    product_Inventory.PriceToCost = product_Inventory.CalculatePriceToCost();
        //    return product_Inventory;*/
        //}

        //private void SaveInventoryProduct()
        //{
        //    if (Save() != null)
        //    {
        //        Message(productInventoryManagement.Add(Save()));
        //        GetAllProductInventories();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Error, " + Save() + "fue null");
        //    }
        //}

        private void GetAllProductInventories()
        {
            //dataGridView1.DataSource = productInventoryManagement.GetAll();
            try
            {
                string id = "1";
                ProductsJoin = productInventoryManagement.GetAllProductInventorys(id);
                var filteredData = ProductsJoin.Select(p => new {
                    Code = p.Code,
                    Barcode = p.Barcode,
                    Name = p.Name_product,
                    Reference = p.Refence,
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
                dataGridView1.Columns["Name"].HeaderText = "Nombre";
                dataGridView1.Columns["Reference"].HeaderText = "Referencia";
                dataGridView1.Columns["Brand"].HeaderText = "Marca";
                dataGridView1.Columns["PriceToCost"].HeaderText = "Precio de costo";
                dataGridView1.Columns["Margin"].HeaderText = "Margen";
                dataGridView1.Columns["Iva"].HeaderText = "Iva";
                dataGridView1.Columns["Price"].HeaderText = "Precio de venta";
                dataGridView1.Columns["Amount"].HeaderText = "Cantidad";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormSuperUser formSuperUser = new FormSuperUser("");
            this.Dispose();
            formSuperUser.ShowDialog();
        }

        private void btnSingOf_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }

        //private void GetAllProducts()
        //{
        //    dataGridView2.DataSource = productManagement.GetAll();
        //}
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

        //void Search(string search)
        //{
        //    List<Product> products = new List<Product>();
        //    var searchLower = search.ToUpper();
        //    var filteredProducts = productManagement.SearchXProducts(searchLower);
        //    dataGridView2.DataSource = filteredProducts;
        //}
        void SearchProductsINventory(string searchLower)
        {
            List<Product_InventoryJoin> ProductsJoinFilter = new List<Product_InventoryJoin>();
            foreach (var item in ProductsJoin)
            {
                if (item.Code.Contains(searchLower) || item.Barcode.Contains(searchLower) || item.Name_product.ToUpper().Contains(searchLower) || item.Refence.ToUpper().Contains(searchLower) || item.Brand.ToUpper().Contains(searchLower))
                {
                    ProductsJoinFilter.Add(item);
                }
            }
            dataGridView1.DataSource = ProductsJoinFilter;

        }
 

        private void ViewProduct(Product product)
        {
            if (product != null)
            {
                /*txtCodeProduct.Text = product.Code;
                btnSave.Enabled = false;   
                btnUpdate.Enabled = true;*/
            }
        }
        private void ViewProductInventory(Product_Inventory product)
        {
            if (product != null)
            {
                /*txtCodeProduct.Text = product.Code;
                txtPrice.Text = product.Price.ToString();
                txtMargin.Text = product.Margin.ToString();
                txtAmout.Text = product.Amount.ToString();
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;*/
            }
        }
        private void IndividualSearchProducts(string code)
        {
            var product = productManagement.GetByCode(code);
            ViewProduct(product);
        }
        int file;

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*file = e.RowIndex;
            string product = dataGridView2.Rows[file].Cells[0].Value.ToString();
            IndividualSearchProducts(product);
            txtCodeProduct.Enabled = false;
            tabControl1.SelectTab(0);
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;*/
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMargin_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAmout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        Product_Inventory productoRepsoitori = null; 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            file = e.RowIndex;
            string IdProducto = dataGridView1.Rows[file].Cells[0].Value.ToString();
            productoRepsoitori = productInventoryManagement.GetByCode(IdProducto);
        }

        private void IndividualSearchProductInventory(string code)
        {
            var product = productInventoryManagement.GetByCode(code);
            ViewProductInventory(product);
        }


        //Full necesario xd
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ProductsJoin == null || ProductsJoin.Any()) {
                MessageBox.Show("No hay datos para exportar");
            }
            else
            {
                SLDocument sl = new SLDocument();
                DateTime año = DateTime.Now;

                // Crear estilo para el título
                SLStyle titleStyle = sl.CreateStyle();
                titleStyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                titleStyle.Alignment.Vertical = VerticalAlignmentValues.Center;
                titleStyle.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightBlue, System.Drawing.Color.White); // Color de fondo

                // Combinar celdas para el título
                sl.MergeWorksheetCells(1, 2, 1, 11);
                sl.SetCellValue(1, 2, $"Reporte Inventario Central de Herramientas - {año.Year}");
                sl.SetCellStyle(1, 2, titleStyle);

                // Ajustar el tamaño de la celda al texto del título
                sl.AutoFitRow(1);

                // Establecer encabezados de columna
                sl.SetCellValue(2, 2, "CODIGO"); // Codigo
                sl.SetCellValue(2, 3, "CODIGO DE BARRAS"); // Codigo de Barra
                sl.SetCellValue(2, 4, "DESCRIPCION DE PRODUCTO"); // Descripcion de producto
                sl.SetCellValue(2, 5, "CANTIDAD EN FISICO"); // Cantidad en fisico
                sl.SetCellValue(2, 6, "PRECIO COSTO UNITARIO"); // Precio Costo Unitario
                sl.SetCellValue(2, 7, "PRECIO COSTO TOTAL"); // Precio costo total --- Va en blanco
                sl.SetCellValue(2, 8, "PRECIO DE VENTA UNITARIO"); // Precio de venta unitario
                sl.SetCellValue(2, 9, "PRECIO DE VENTA TOTAL"); // Precio de venta total ---- Va en blanco
                sl.SetCellValue(2, 10, "MARGEN"); // Margen
                sl.SetCellValue(2, 11, "TARIFA IVA"); // Tarifa IVA

                // Agregar datos de producto
                int iR = 3;
                foreach (var row in ProductsJoin)
                {
                    sl.SetCellValue(iR, 2, row.Code);
                    sl.SetCellValue(iR, 3, row.Barcode);
                    sl.SetCellValue(iR, 4, row.Name_product);
                    sl.SetCellValue(iR, 5, row.Amount);
                    sl.SetCellValue(iR, 6, row.PriceToCost);
                    // sl.SetCellValue(iR, 7, row.Price); 
                    sl.SetCellValue(iR, 8, row.Price);
                    //  sl.SetCellValue(iR, 9, row.Code); 
                    sl.SetCellValue(iR, 10, row.Margin);
                    sl.SetCellValue(iR, 11, row.Iva);

                    iR++;
                }

                // Guardar el archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos de Excel (.xlsx)|.xlsx|Todos los archivos (.)|.";
                saveFileDialog.Title = "Guardar archivo de Excel";
                saveFileDialog.FileName = "Reporte.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Default Path
                    sl.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Archivo guardado exitosamente.", "Guardar Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Operación cancelada.", "Guardar Archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            string seach = txtSearch2.Text;
            SearchProductsINventory(seach.ToUpper());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

