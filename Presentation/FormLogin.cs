using Entity;
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
    public partial class FormLogin : Form
    {
        User user = new User();
        List<User> users = new List<User>();
        Logic.UserManagement userManagement = new Logic.UserManagement(ConfigConnection.connectionString);
        public FormLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        bool passwordVisible = false;

        private void ViewPassword_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                txtPassword.PasswordChar = '●';
            }
            else
            {
                txtPassword.PasswordChar = '\0';
            }
            passwordVisible = !passwordVisible;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            try
            {
                if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty)
                {
                    MessageBox.Show("ERROR. No puede dejar datos en blanco");
                }
                else
                {
                    users = userManagement.GetAll();
                    bool usuarioEncontrado = false;
                    foreach (var item in users)
                    {
                        if ((txtUserName.Text == item.Name_User) && (txtPassword.Text == item.User_Password))
                        {
                            usuarioEncontrado = true;
                            MessageBox.Show("Bienvenido " + item.First_Name);
                            if(item.Id_Role == 1)
                            {
                                FormSuperUser formSuperUser = new FormSuperUser(item.Name_User);
                                this.Hide();
                                formSuperUser.ShowDialog();
                            }else if(item.Id_Role == 2)
                            {
                                FormInventarioUser formProduct = new FormInventarioUser(item.Name_User);
                                this.Hide();
                                formProduct.ShowDialog();
                            }
                            
                            txtUserName.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                            break;
                        }
                        
                    }
                    if (!usuarioEncontrado)
                    {
                        MessageBox.Show("El nombre de usuario o contraseña son incorrectos o no existen");
                        txtPassword.Text = string.Empty;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FormCreateInventory formCreateInventory = new FormCreateInventory("");
            FormInventarioAdmin formInventarioAdmin = new FormInventarioAdmin("Vegam");
            FormInventarioUser formInventoryUser = new FormInventarioUser("");
            FormProduct formProduct = new FormProduct("");
            FormSuperUser formSuperUser = new FormSuperUser("");
            FormUser formUser = new FormUser("");

            formCreateInventory.Dispose();
            formInventarioAdmin.Dispose();
            formInventoryUser.Dispose();
            formProduct.Dispose();
            formUser.Dispose();
            formSuperUser.Dispose();
        }
    }
}
