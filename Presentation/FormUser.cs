using Entity;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentation
{
    public partial class FormUser : Form
    {
        Logic.UserManagement userManagement = new Logic.UserManagement(ConfigConnection.connectionString);
        List<User> listUsers = new List<User>();
        List<Product> products = new List<Product>();
        private string NameUser; 
        public FormUser(string user)
        {
            InitializeComponent();
            NameUser = user;
            GetUsers();
        }

        private User ValidateTxtUser()
        {
            User user = new User();
            Role role = new Role();

            if (userManagement.Exist(user) == true)
            {
                MessageBox.Show("Error, ya hay un usuario con esta identificación, vuelva a intentarlo");
                return null;
            }
            if ((txtIdentification.Text != string.Empty) && (txtName.Text != string.Empty) && (txtLastName.Text != string.Empty) && (txtUserName.Text != string.Empty) && (cbRole.Items!=null) && (txtPassword.Text != string.Empty) && (cbStatu.Items != null)) 
            {
                if (txtIdentification.Text.Length > 10)
                {
                    MessageBox.Show(txtIdentification.Text + ": es incorrecto, debes ingresar un Documento de Identidad no mayor a 10 digitos");
                }
                else
                {
                    user.Identification = txtIdentification.Text;
                }
                user.First_Name = txtName.Text;
                user.Last_Name = txtLastName.Text;

                int selectedIndexRol = cbRole.SelectedIndex + 1;
                user.Id_Role = selectedIndexRol;
                user.Name_User = txtUserName.Text;
                user.Email = string.IsNullOrEmpty(txtEmail.Text) ? null : txtEmail.Text;
                int selectedIndexStatu =cbStatu.SelectedIndex + 1;
                user.Statu = int.Parse(selectedIndexStatu.ToString());
                if (txtPassword.Text.Length < 4 && txtPassword.Text.Length > 12)
                {
                    MessageBox.Show("Debe ingresar una contraseña no menor a cuatro dígitos ni mayor a doce dígitos");
                }
                else
                {
                    user.User_Password = txtPassword.Text;
                }
                return user;
            }
            else
            {
                MessageBox.Show("Ingresar datos obligatorios");
                return null;
            }
        }
        private User ValidateTxUpdate()
        {
            User user = new User();
            Role role = new Role();
            if ((txtIdentification.Text != string.Empty) && (txtName.Text != string.Empty) && (txtLastName.Text != string.Empty) && (txtUserName.Text != string.Empty) && (cbRole.Items != null) && (txtEmail.Text != string.Empty) && (txtPassword.Text != string.Empty) && (cbStatu.Items != null))
            {
                if (txtIdentification.Text.Length > 10)
                {
                    MessageBox.Show(txtIdentification.Text + ": es incorrecto, debes ingresar un Documento de Identidad no mayor a 10 digitos");
                }
                else
                {
                    user.Identification = txtIdentification.Text;
                }
                user.First_Name = txtName.Text;
                user.Last_Name = txtLastName.Text;

                int selectedIndex = cbRole.SelectedIndex + 1;
                user.Id_Role = selectedIndex;
                user.Name_User = txtUserName.Text;
                user.Email = txtEmail.Text;
                user.Statu = int.Parse(cbStatu.SelectedItem.ToString());
                if (txtPassword.Text.Length < 4 && txtPassword.Text.Length > 12)
                {
                    MessageBox.Show("Debe ingresar una contraseña no menor a cuatro dígitos ni mayor a doce dígitos");
                }
                else
                {
                    user.User_Password = txtPassword.Text;
                }
                return user;
            }
            else
            {
                MessageBox.Show("No puede dejar campos en blanco");
                return null;
            }
        }
        
        private void GetUsers()
        {
            var filteredData = userManagement.GetAll().Select(p => new {
                Identification = p.Identification,
                First_Name = p.First_Name,
                Barcode = p.Last_Name,
                Name_User = p.Name_User,
                User_Password = p.User_Password,
                Email = p.Email,
                Rol = (p.Id_Role == 1) ? "Administrador" : "Usuario",
                stattu = (p.Statu ==1) ? "Habilitado" : "Inhabilitado"
            }).ToList();

            dataGridViewUser.DataSource = filteredData;
            dataGridViewUser.Columns["Identification"].HeaderText = "Cedula";
            dataGridViewUser.Columns["First_Name"].HeaderText = "Nombre";
            dataGridViewUser.Columns["Barcode"].HeaderText = "Apellido";
            dataGridViewUser.Columns["Name_User"].HeaderText = "Nombre de usuario";
            dataGridViewUser.Columns["User_Password"].HeaderText = "Contraseña";
            dataGridViewUser.Columns["Email"].HeaderText = "Correo";
            dataGridViewUser.Columns["Rol"].HeaderText = "Rol";
            dataGridViewUser.Columns["stattu"].HeaderText = "Estado";

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

        public void Save()
        {
            Message(userManagement.Add(ValidateTxtUser()));
            GetUsers();         
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void txtIdentification_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private bool passwordVisible = false;

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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                
            }
            else if (e.KeyCode == Keys.F4)
            {
                label1.Text = "F4 presionado";
            }
            else if (e.KeyCode == Keys.F7)
            {
                label1.Text = "F7 presionado";
            }
            else if (e.KeyCode == Keys.F9)
            {
                label1.Text = "F9 presionado";
            }
        }

        private void ClearFields ()
        {
            txtIdentification.Text = string.Empty;
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cbRole.Text = string.Empty;
            cbStatu.Text = string.Empty;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormSuperUser formSuperUser = new FormSuperUser(NameUser);
            this.Dispose();
            formSuperUser.ShowDialog();
        }

        private void btnSingOf_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Dispose();
            formLogin.ShowDialog();
        }

        private void Search(string search)
        {
            List<User> users = new List<User>();
            var searchLower = search.ToUpper();
            var filteredUsers = userManagement.SearchXUsers(searchLower);
            dataGridViewUser.DataSource = filteredUsers; 
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            Search(search);
        }
        int file;
        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            file = e.RowIndex;
            string user = dataGridViewUser.Rows[file].Cells[0].Value.ToString();
            IndividualSearch(user);
            txtIdentification.Enabled = false;
            tabControl1.SelectTab(0);
            btnGuardar.Enabled = false; 
        }

        private void IndividualSearch(string id)
        {
            var user = userManagement.GetByID(id);
            View(user);
        }

        private void View(User user)
        {
            if(user != null)
            {
                txtIdentification.Text = user.Identification;
                txtName.Text = user.First_Name;
                txtLastName.Text = user.Last_Name;
                txtUserName.Text = user.Name_User;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.User_Password;
                cbRole.Text = user.Id_Role == 1 ? "Administrador" : "Usuario";
                cbStatu.Text = (user.Statu) == 1 ? "Habilitado" : "Inhabilitado";
            }
        }
        

        private void UpdateUser()
        {
            Message(userManagement.Update(ValidateTxtUser()));
            GetUsers();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtIdentification.Enabled = true;
            ClearFields();
        }

        private bool Exist(User user)
        {
            List<User> users = new List<User>();
            users = userManagement.GetAll();

            if(user.Identification == user.Identification)
            {
                return true;
            }
            return false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

    }
}
