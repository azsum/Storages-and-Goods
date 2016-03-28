namespace StoragesAndGoods
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using BLL;
    using DML;
    using Public_methods;
    using Enums;
    public partial class Storage : System.Web.UI.Page
    {
        private int storageId = 0;
        private decimal capacity = 0;
        #region Protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
            storageId = (int)(Session["storageId"]);
            capacity = StoragesManager.GetStorageCapacity(storageId);

            if (!IsPostBack)
            {
                hfstorageId.Value = storageId.ToString();
                FillDropdown(storageId);
                SetStorageImageAndName(storageId);
            }
            operationMessage.Text = string.Empty;
            AddProgressBars(storageId);
            lv.DataBind();
        }

        protected void btnChangeGoodAmount(object sender, EventArgs e)
        {
            if (ddlChooseGood.Items.Count == 1)
            {
                string messageError = "Please add good from the menu";
                PublicMethods.Fail(ddlChooseGoodError, messageError);
            }
            else {
                string message = string.Empty;
                bool isGoodSelected = IsGoodSelected();
                if (isGoodSelected)
                {
                    ddlChooseGoodError.Visible = false;
                    string tbAmount = tbInput.Text;
                    bool isValidAmount = PublicMethods.IsValidInput(tbAmount);
                    string btnText = GetButtonText(sender);
                    if (isValidAmount)
                    {
                        decimal tbValue = decimal.Parse(tbAmount);
                        string selectedValue = ddlChooseGood.SelectedItem.Text;
                        decimal TotalAmount = GoodsManager.GetGoodAmountByName(selectedValue);
                        int goodId = GoodsManager.GetGoodIdByNameAndStorageId(selectedValue, this.storageId);
                        GoodsStatus goodStatus = new GoodsStatus();
                        goodStatus.GoodID = goodId;
                        goodStatus.StatusDataFrom = DateTime.Now;
                        goodStatus.GoodAmount = tbValue;
                        bool isPossibleOperation = IsOperationPossible(TotalAmount, tbValue, btnText);
                        if (isPossibleOperation)
                        {
                            if (btnText == OperationId.Remove.ToString())
                            {
                                goodStatus.GoodOperationID = (int)OperationId.Remove;
                                GoodsManager.RemoveGoodAmountById(goodId, tbValue);
                                HelperAddRemoveOperations(goodStatus);
                            }
                            else if (btnText == OperationId.Add.ToString())
                            {
                                goodStatus.GoodOperationID = (int)OperationId.Add;
                                GoodsManager.AddGoodAmountById(goodId, tbValue);
                                HelperAddRemoveOperations(goodStatus);
                            }
                        }
                        else
                        {
                            message = "Good amount in storage must be between  0 - " + this.capacity;
                            PublicMethods.Fail(operationMessage, message);
                        }
                    }
                    else
                    {
                        message = "Please enter different value";
                        PublicMethods.Fail(operationMessage, message);
                    }
                }
                else
                {
                    message = "Please select a good";
                    PublicMethods.Fail(ddlChooseGoodError, message);
                }
            }
        }

        protected void OnDeleting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            int id = Convert.ToInt32(e.InputParameters["ID"]);
            GoodsStatus goodStatus = GoodsStatusManager.GetGoodStatusById(id);
            Goods good = GoodsManager.GetGoodById(goodStatus.GoodID);
            string btnText = GetButtonText(sender);
            bool isDeletePossible = IsOperationPossible(good.TotalAmount, goodStatus.GoodAmount, btnText);
            if (isDeletePossible)
            {
                GoodsManager.RemoveGoodAmountById(goodStatus.GoodID, goodStatus.GoodAmount);
                PublicMethods.Success(operationMessage);
                SetPreviousPageDataPager();
                lv.DataBind();
                AddProgressBars(storageId);
            }
            else
            {
                string message = "Storage amount is lesser";
                PublicMethods.Fail(operationMessage, message);
            }
        }
        #endregion

        #region Private Methods

        private void FillDropdown(int id)
        {
            ddlChooseGood.Items.Clear();
            Dictionary<int, string> goodsNames = GoodsManager.LoadGoodsByStorageId(id);
            ddlChooseGood.Items.Insert(0, new ListItem("Choose good type", "0"));
            int index = 0;

            foreach (var item in goodsNames)
            {
                ddlChooseGood.Items.Insert(index + 1, new ListItem(item.Value, item.Key.ToString()));
                index++;
            }
            ddlChooseGood.SelectedIndex = 0;
        }

        private void HelperAddRemoveOperations(GoodsStatus goodStatus)
        {
            GoodsStatusManager.Save(goodStatus);
            PublicMethods.Success(operationMessage);
        }

        private static string GetButtonText(object sender)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                string btnText = btn.Text;
                return btnText;
            }
            return string.Empty;
        }

        private bool IsGoodSelected()
        {

            if (ddlChooseGood.SelectedIndex > 0)
            {
                return true;
            }
            return false;
        }

        private void AddProgressBars(int storageId)
        {
            progressSection.Controls.Clear();
            List<Goods> list =GoodsManager.LoadAllGoodsByStorageId(storageId);
            for (int i = 0; i < list.Count; i++)
            {
                string name = list[i].Name.ToUpper();
                int amount = Convert.ToInt32(list[i].TotalAmount);
                string text = name + " " + amount.ToString();
                string index = (i + 1).ToString();
                HtmlGenericControl mainDiv = new HtmlGenericControl("div");
                HtmlGenericControl progress = new HtmlGenericControl("div");
                string goodAmount = ((int)(amount / (this.capacity / 100))).ToString();
                mainDiv.Attributes.Add("class", "progress");
                progress.Attributes.Add("aria - valuemin", "0");
                progress.Attributes.Add("aria - valuemax", "100");
                progress.Attributes.Add("class", "progress-bar progress-bar-striped active textLeft");
                progress.Attributes.Add("role", "progressbar");
                progress.Attributes.Add("style", "width:" + goodAmount + "%;");
                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerText = text;
                progress.Controls.Add(span);
                mainDiv.Controls.Add(progress);
                progressSection.Controls.Add(mainDiv);
            }
        }

        private void SetPreviousPageDataPager()
        {
            DataPager dp = lv.FindControl("dp") as DataPager;
            if (dp != null && lv.Items.Count != dp.TotalRowCount && lv.Items.Count < 2)
            {
                dp.SetPageProperties(0, dp.MaximumRows, true);
            }
        }

        private void SetStorageImageAndName(int storageId)
        {
            imgStorage.ImageUrl = StoragesManager.GetImagePath(storageId);
            lbStorageName.Text = StoragesManager.GetStorageNameById(storageId);
            lbStorageName.Visible = true;
            lbStorageName.CssClass = "font-size";
        }

        private bool IsOperationPossible(decimal storageAmount, decimal clientAmount, string buttonText)
        {
            decimal result = 0;

            if (buttonText == OperationId.Add.ToString())
            {
                result = storageAmount + clientAmount;
            }
            else if (buttonText == OperationId.Remove.ToString() || buttonText == string.Empty)
            {
                result = storageAmount - clientAmount;
            }

            if (result >= 0 && result <= capacity)
            {
                return true;
            }
            return false;
        }
        #endregion

        protected void upDown_Unload(object sender, EventArgs e)
        {
            AddProgressBars(storageId);
            lv.DataBind();
        }
    }
}