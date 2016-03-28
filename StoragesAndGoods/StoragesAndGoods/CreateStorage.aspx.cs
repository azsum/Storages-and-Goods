namespace StoragesAndGoods
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using BLL;
    using DML;
    using Public_methods;
    using System.Web.Configuration;
    using System.Configuration;
    public partial class CreateStorage : System.Web.UI.Page
    {
        #region Protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["userId"] as string))
                {
                    Response.Redirect("~/Register.aspx");
                }
            }
            lbCapacityError.Visible = false;
            lbNameError.Visible = false;
            lbSuccessMessage.Visible = false;
            errorUploadImage.Visible = false;
        }



        protected void CreateAStorage(object sender, EventArgs e)
        {
            string message = string.Empty;
            Storages storage = new Storages();
            storage.Name = tbStorageName.Text.Trim();
            bool isValidCapacity = PublicMethods.IsValidInput(tbCapacity.Text);
            decimal capacity = 0;
            bool isValidCapacitySize = decimal.TryParse(tbCapacity.Text.Trim(), out capacity);
            if (capacity <= 1)
            {
                message = "Capacity must be greater then 1";
                PublicMethods.Fail(lbCapacityError, message);
            }
            bool isValidNameOfStorage = IsValidStorageName(storage);
            string picturesFolder = "~/Content/Pictures/";

            if (!isValidNameOfStorage)
            {
                message = "Enter name";
                PublicMethods.Fail(lbNameError, message);
            }

            if (!isValidCapacity)
            {
                message = "Incorrect capacity";
                PublicMethods.Fail(lbCapacityError, message);
            }

            else if (isValidCapacity && isValidNameOfStorage)
            {
                capacity = decimal.Parse(tbCapacity.Text.Trim());

                string fileName = Path.GetFileName(uploadImage.PostedFile.FileName);
                string format = Path.GetExtension(fileName);
                bool isFormatValid = IsValidFormat(format);
                bool isSizeValid = IsValidSize();
                if (isFormatValid && isSizeValid)
                {
                    uploadImage.PostedFile.SaveAs(Server.MapPath(picturesFolder) + fileName);
                    HelperCreateStorage(storage, picturesFolder, capacity, fileName);
                }
                else if (fileName == string.Empty)
                {
                    string defaultImageName = "Default.jpg";
                    HelperCreateStorage(storage, picturesFolder, capacity, defaultImageName);
                }
            }
        }

        private void HelperCreateStorage(Storages storage, string picturesFolder, decimal capacity, string fileName)
        {
            storage.ImagePath = picturesFolder + fileName;
            storage.Capacity = capacity;
            storage.UserID = int.Parse((Session["userId"]).ToString());
            StoragesManager.AddStorage(storage);
            PublicMethods.Success(lbSuccessMessage);
        }

        #endregion

        #region Private methods

        private bool IsValidSize()
        {
            int maxRequestLength = 0;
            HttpRuntimeSection section =
            ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
            {
                maxRequestLength = section.MaxRequestLength;
            }

            int fileSize = uploadImage.PostedFile.ContentLength;
            if (maxRequestLength < fileSize)
            {
                string message = message = "The file size is too big";
                PublicMethods.Fail(errorUploadImage, message);
                return false;
            }
            return true;
        }

        private bool IsValidFormat(string format)
        {
            string[] formats = new string[] { ".jpg", ".jpeg", ".gif", ".bmp" };

            if (format != string.Empty)
            {
                foreach (var item in formats)
                {
                    if (format == item)
                    {
                        return true;
                    }
                }
                if (true)
                {

                    string message = "Wrong format";
                    PublicMethods.Fail(errorUploadImage, message);
                    return false;
                }
            }
            return false;
        }
        private bool IsValidStorageName(Storages storage)
        {
            int id = int.Parse((Session["userId"]).ToString());
            List<string> storagesNames = StoragesManager.GetStoragesNamesByUserId(id);
            string message = "The name exist";
            foreach (string item in storagesNames)
            {
                if (storage.Name == item)
                {
                    PublicMethods.Fail(lbNameError, message);
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}