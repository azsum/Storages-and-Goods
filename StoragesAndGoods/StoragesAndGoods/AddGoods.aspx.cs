namespace StoragesAndGoods
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using BLL;
    using DML;
    using Public_methods;
    public partial class AddGoods : System.Web.UI.Page
    {
        private int userId = 0;

        #region Protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
            lbSuccessMessage.Visible = false;
            lbNameError.Visible = false;
            lbDdlError.Visible = false;
            lbAmountErrorMessage.Visible = false;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["userId"] as string))
                {
                    Response.Redirect("~/Register.aspx");
                }
                else
                {
                    string userID = Session["userId"] as string;
                    this.userId = int.Parse(userID);
                    FillDropdown();
                }
            }
        }

        protected void AddGood_Click(object sender, EventArgs e)
        {
            bool isValidGood = ValidateGood();
            Goods good = new Goods();
            if (isValidGood)
            {
                good.Name = tbGoodName.Text;
                good.TotalAmount = decimal.Parse(tbGoodAmount.Text);
                good.StorageID = StoragesManager.GetStorageIdByName(ddlStorages.SelectedItem.Text);
                GoodsManager.AddGoodToStorage(good);
                PublicMethods.Success(lbSuccessMessage);
            }
        }
        #endregion

        #region Private methods
        private void FillDropdown()
        {
            List<string> storagesNames = StoragesManager.GetStoragesNamesByUserId(this.userId);
            ddlStorages.Items.Insert(0, new ListItem("Choose a storage", "0"));
            int index = 0;
            foreach (var item in storagesNames)
            {
                ddlStorages.Items.Insert(index + 1, new ListItem(item));
                index++;
            }
            ddlStorages.SelectedIndex = 0;
        }

        private bool ValidateGood()
        {
            decimal amount = 0;
            string message = string.Empty;
            bool goodAmount = decimal.TryParse(tbGoodAmount.Text, out amount);
            string storageName = ddlStorages.SelectedItem.Text;
            if (ddlStorages.SelectedIndex != 0 && goodAmount)
            {
                decimal storageAmount = StoragesManager.GetStorageCapacityByName(storageName);
                if (storageAmount < amount)
                {
                    message = "amount must be lesser then " + storageAmount;
                    PublicMethods.Fail(lbAmountErrorMessage, message);
                    return false;
                }
            }

            if (!goodAmount)
            {
                message = "Amount must be lesser";
                PublicMethods.Fail(lbAmountErrorMessage, message);
                return false;
            }

            if (string.IsNullOrEmpty(tbGoodName.Text))
            {
                message = "Please enter good name";
                PublicMethods.Fail(lbNameError, message);
                return false;
            }

            else if (string.IsNullOrEmpty(tbGoodAmount.Text))
            {
                message = "Please enter good amount";
                PublicMethods.Fail(lbAmountErrorMessage, message);
                return false;
            }

            else if (ddlStorages.SelectedIndex == 0)
            {
                message = "Please select a storage";
                PublicMethods.Fail(lbDdlError, message);
                return false;
            }
            else
            {
                int storageId = StoragesManager.GetStorageIdByName(storageName);
                List<string> storagesNames = GoodsManager.GetGoodsNamesByStorageId(storageId);
                foreach (string item in storagesNames)
                {
                    string textboxGoodName = tbGoodName.Text;
                    if (item == textboxGoodName)
                    {
                        message = "Name of good exist";
                        PublicMethods.Fail(lbNameError, message);
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion
    }
}