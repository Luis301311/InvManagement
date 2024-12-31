using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class FormSuperUser : Form
    {
        private string Name_user; 
        public FormSuperUser(string User)
        {
            InitializeComponent();
            Name_user = User;   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void FormSuperUser_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            FormUser formUser = new FormUser(Name_user);
            this.Dispose();
            formUser.ShowDialog();
        }

        private void btnCrearProducto_Click(object sender, EventArgs e)
        {
            FormProduct formProduct = new FormProduct(Name_user);
            this.Dispose();
            formProduct.ShowDialog();
        }

        private void btnNuevoInventario_Click(object sender, EventArgs e)
        {
            FormCreateInventory formInventario = new FormCreateInventory(Name_user);
            this.Dispose();
            formInventario.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }
    }
}
