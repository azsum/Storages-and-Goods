namespace StoragesAndGoods
{
    using System;
    using System.Web.Security;

    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["userId"] = null;
            Session["storageId"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}