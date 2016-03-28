namespace StoragesAndGoods
{
    using System;
    using System.Web.UI;
    using BLL;
    using Security;
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string email = Email.Text;
                bool isEmailExist = UsersManager.IsEmailExist(email);
                bool isValidPassword = false;
                if (isEmailExist)
                {
                    string hashedPassword = UsersManager.LoadHashedPassword(email);
                    isValidPassword = PasswordHash.IsPasswordValid(Password.Text.Trim(), hashedPassword);
                    if (isValidPassword)
                    {
                        Session["userId"] = (UsersManager.GetUserIdByEmail(email)).ToString();
                        Response.Redirect("Home.aspx");
                    }
                }
                else {
                    loginMessage.Visible = true;
                    loginMessage.Text = "Incorrect e-mail or password";
                }
            }
        }
    }
}