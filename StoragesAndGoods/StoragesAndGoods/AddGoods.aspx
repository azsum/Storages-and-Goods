<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGoods.aspx.cs" Inherits="StoragesAndGoods.AddGoods" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <div runat="server" class="container  width50 margin-top-15">
        <div class="col-md-10 col-md-offset-4">
            <div class="form-group">
                <div class="col-md-10">
                    <asp:Label Text="Name of good" runat="server" AssociatedControlID="tbGoodName" CssClass=" control-label"></asp:Label>
                    <asp:TextBox runat="server" ID="tbGoodName" CssClass=" form-control"></asp:TextBox>
                    <asp:Label runat="server" ID="lbNameError" Visible="false" CssClass=""></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ForeColor="red" ControlToValidate="tbGoodName" CssClass="" ErrorMessage="The name field is required." />
                </div>
                <br />
                <div class="form-group">
                    <div class="col-md-10">
                        <asp:Label Text="Amount of good" runat="server" CssClass=" control-label" AssociatedControlID="tbGoodAmount"></asp:Label>
                        <asp:TextBox runat="server" ID="tbGoodAmount" CssClass=" form-control"></asp:TextBox>
                        <asp:Label runat="server" ID="lbAmountErrorMessage" Visible="false" CssClass=""></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="tbGoodAmount" ErrorMessage="The amount field is required." />
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="col-md-10">
                        <asp:DropDownList ID="ddlStorages" runat="server" CssClass=" color-lila  bg-color"></asp:DropDownList>
                        <asp:Label runat="server" ID="lbDdlError" Visible="false" CssClass=""></asp:Label>
                        <br />
                        <asp:Button runat="server" ID="btnAddGood" Text="Add good" OnClick="AddGood_Click" CssClass="margin-top-3 btn btn-primary"></asp:Button>
                        <asp:Label runat="server" ID="lbSuccessMessage" Visible="false" CssClass=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
</asp:Content>
