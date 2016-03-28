<%@ Page Title="Create Storage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateStorage.aspx.cs" Inherits="StoragesAndGoods.CreateStorage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" class="container width50 margin-top-15">
        <div class="col-md-10 col-md-offset-4">
            <div class="form-group">
                <div class="col-md-10">
                    <asp:Label runat="server" CssClass="control-label" Text="Name of storage" AssociatedControlID="tbStorageName"></asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="tbStorageName" CssClass="form-control" />
                    <asp:Label runat="server" ID="lbNameError"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbStorageName"
                        CssClass="text-danger" ErrorMessage="The Name field is required." />
                </div>
            </div>
            <br />
            <div class="form-group">
                <div class="col-md-10">
                    <asp:Label runat="server" CssClass="control-label" Text="Capacity of storage" AssociatedControlID="tbStorageName"></asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="tbCapacity" CssClass="form-control" />
                    <asp:Label runat="server" ID="lbCapacityError" ></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCapacity"
                        CssClass="text-danger" ErrorMessage="The Name field is required." />

                </div>
            </div>
            <br />
            <div class="form-group margin-top-7">
                <div class="col-md-10">
                    <asp:Label runat="server" CssClass="control-label"  Text="Add image for storage" AssociatedControlID="uploadImage"></asp:Label>
                    <br />
                    <asp:FileUpload runat="server" ID="uploadImage" />
                    <asp:Label runat="server" ID="errorUploadImage" Text="Max 4mb"></asp:Label>
                </div>
            </div>
            <br />
            <asp:UpdatePanel runat="server" class="form-group margin-top-7" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="col-md-10">
                    <asp:LinkButton runat="server" ID="btnSubmit" OnClick="CreateAStorage" CssClass="btn btn-primary" Text="Create storage" />
                    <asp:Label runat="server" ID="lbSuccessMessage" ></asp:Label>
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
