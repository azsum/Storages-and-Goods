namespace StoragesAndGoods
{
    using System;
    using System.Web.UI;
    using BLL;
    using DML;
    using Security;
    public partial class Register : Page
    {
        #region Protected methods
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            bool isValid = IsValidRegistration();
            if (isValid)
            {
                Users user = new Users();
                user.Email = tbEmail.Text;
                user.Password = PasswordHash.CreatePasswordSalt(Password.Text);
                user.Username = Username.Text;
                UsersManager.SaveUser(user);
                Response.Redirect("Login.aspx");
            }
            else
            {
               string message = "The password must be minimum 6 symbols";
                Public_methods.PublicMethods.Fail(ErrorMessage, message);
            }
        }
        #endregion

        #region Private methods
        private bool IsValidRegistration()
        {
            string email = tbEmail.Text;
            bool isValidEmail = UsersManager.IsEmailExist(email);
            bool isValidPassword = Password.Text.Length >= 6;
            bool isRetypedPasswordSame = Password.Text == ConfirmPassword.Text;
            if (isValidEmail && isValidPassword && isRetypedPasswordSame)
            {
                Public_methods.PublicMethods.Success(ErrorMessage);
                return true;
            }
            if (!isRetypedPasswordSame)
            {
                string message = "The password and retyped password are not the same";
                Public_methods.PublicMethods.Fail(ErrorMessage, message);
                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}