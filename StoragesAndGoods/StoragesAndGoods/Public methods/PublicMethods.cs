namespace StoragesAndGoods.Public_methods
{
    using System.Web.UI.WebControls;

    public class PublicMethods
    {
        public static void Fail(Label label, string message)
        {
            label.Visible = true;
            label.ForeColor = System.Drawing.Color.Red;
            label.Font.Bold = true;
            label.Text = message;
        }
        public static void Success(Label label)
        {
            label.Visible = true;
            label.ForeColor = System.Drawing.Color.Green;
            label.Font.Bold = true;
            string successMessage = "Success";
            label.Text = successMessage;
        }

        public static bool IsValidInput(string textboxText)
        {
            textboxText = textboxText.Trim();
            if (string.IsNullOrEmpty(textboxText))
            {
                return false;
            }
            foreach (char c in textboxText)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
 
            return true;
        }
    }
}