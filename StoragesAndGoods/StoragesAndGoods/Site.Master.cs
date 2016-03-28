namespace Storehouse
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BLL;
    using DML;
    public partial class SiteMaster : MasterPage
    {
        #region Protected methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenuStorages();
            }
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            string storageName = e.Item.Text;
            int storageId = StoragesManager.GetStorageIdByName(storageName);
            Session["storageId"] = storageId;
            if (e.Item.Text == "Log Out")
            {
                navigationMenu.Controls.Clear();
            }
            Response.Redirect("Storage.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadMenuStorages()
        {
            string id = Session["userId"] as string;
            navigationMenu.Controls.Clear();
            List<string> menuStorages = null;
            MenuItem wellcome = null;
            int ID = 0;
            if (id != null)
            {
                ID = int.Parse(id);
                menuStorages = StoragesManager.GetStoragesNamesByUserId(ID);
                string userName = UsersManager.GetUsernamById(ID);
                wellcome = new MenuItem("Wellcome " + userName);
                wellcome.Selectable = false;
            }
            MenuItem storageMenuItem = new MenuItem("Storages And Goods");
            MenuItem addStorage = new MenuItem("Add new storage");
            MenuItem addGoods = new MenuItem("Add Goods");
            addStorage.NavigateUrl = "~/CreateStorage.aspx";
            addGoods.NavigateUrl = "~/AddGoods.aspx";
            storageMenuItem.ChildItems.AddAt(0, addStorage);
            storageMenuItem.ChildItems.AddAt(1, addGoods);
            storageMenuItem.Selectable = false;
            navigationMenu.Items.AddAt(1, storageMenuItem);
            if (menuStorages != null)
            {
                foreach (var item in menuStorages)
                {
                    MenuItem menuItem = new MenuItem(item.ToString());
                    storageMenuItem.ChildItems.AddAt(0, menuItem);
                }
            }
            if (wellcome != null)
            {
                MenuItem menuItem = new MenuItem("Log out");
                menuItem.NavigateUrl = "~/LogOut.aspx";
                navigationMenu.Items.Add(menuItem);
                navigationMenu.Items.Add(wellcome);
            }
        }


        #endregion

        //protected void BtnSearchClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(tbSearch.Text))
        //    {
        //    bool isUserLogged=Session["userId"].ToString()!=null;
           
        //        if (isUserLogged)
        //        {
        //            int userId = int.Parse(Session["userId"].ToString());
        //            List<int> userStorages = StoragesManager.GetStoragesIdsByUserId(userId);
        //            List<string> goodsNames = null;
        //            string searchedGood = tbSearch.Text;
        //            foreach (int storageId in userStorages)
        //            {
        //                goodsNames = GoodsManager.GetGoodsNamesByStorageId(storageId);
        //                foreach (string goodName in goodsNames)
        //                {
        //                    if (searchedGood == goodName)
        //                    {
        //                        Response.Redirect("Storage.aspx");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            Response.Write("greshkaaaaaaaaaaaa");
        }
    }
}