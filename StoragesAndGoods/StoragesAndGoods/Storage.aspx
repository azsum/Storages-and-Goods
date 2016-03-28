<%@ Page Title="Home" Language="C#" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Storage.aspx.cs" Inherits="StoragesAndGoods.Storage" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel runat="server" CssClass="row mainContent">
        <asp:HiddenField ID="hfstorageId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="hfSelectedStorage" runat="server" Visible="False" />
        <asp:Panel runat="server" CssClass="margin-top-1 col-md-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
            <asp:Panel runat="server" CssClass="margin-top-1 col-md-6">
                        <div runat="server" id="progressSection" class=" margin-top-1">
                        </div>
                </asp:Panel>
            <asp:Panel runat="server" CssClass="margin-top-1 col-md-6">
                <asp:Label runat="server" ID="lbStorageName" CssClass="color-lila font-size" AssociatedControlID="imgStorage" />
                <asp:Image ID="imgStorage" runat="server" CssClass="img-responsive img-storage" />
                <asp:DropDownList ID="ddlChooseGood" runat="server" CssClass="color-lila  bg-color" />
                <asp:Label runat="server" ID="ddlChooseGoodError"></asp:Label>
                <br />
                <asp:Label ID="lbInput" runat="server" CssClass=" color-white" AssociatedControlID="tbInput"
                    Text="Please enter amount" />
                <br />
                <asp:TextBox ID="tbInput" placeholder="type a good amount" CssClass="color-gold form-control" runat="server" />
                <asp:Label ID="operationMessage" runat="server" Font-Bold="True" Font-Names="Bookman Old Style" Font-Size="1em" ForeColor="Red" Width="60%"></asp:Label>
                <br />
                        <asp:LinkButton ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnChangeGoodAmount" />
                        <asp:LinkButton ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" OnClick="btnChangeGoodAmount" />
                   
            </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel runat="server" ID="upDown" CssClass="col-md-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:ListView ID="lv" runat="server" DataSourceID="ods" DataKeyNames="ID">
                        <AlternatingItemTemplate>
                            <tr style="background-color: #FFF8DC;">
                                <td>
                                    <asp:LinkButton ID="DeleteButton" CssClass="bgColorYellow btn btn-default" runat="server" CommandName="Delete" Text="Delete" />
                                </td>
                                <td>
                                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="AmountLabel" runat="server" Text='<%# Eval("Amount") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OperationTypeLabel" runat="server" Text='<%# Eval("OperationType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DateFromLabel" runat="server" Text='<%# Eval("DateFrom") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="background-color: #008A8C; color: #FFFFFF;">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="AmountTextBox" runat="server" Text='<%# Bind("Amount") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="OperationTypeTextBox" runat="server" Text='<%# Bind("OperationType") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DateFromTextBox" runat="server" Text='<%# Bind("DateFrom") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="AmountTextBox" runat="server" Text='<%# Bind("Amount") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="OperationTypeTextBox" runat="server" Text='<%# Bind("OperationType") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DateFromTextBox" runat="server" Text='<%# Bind("DateFrom") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #DCDCDC; color: #000000;">
                                <td>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="bg-color-orange btn btn-default" CommandName="Delete" Text="Delete" />
                                </td>
                                <td>
                                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="AmountLabel" runat="server" Text='<%# Eval("Amount") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OperationTypeLabel" runat="server" Text='<%# Eval("OperationType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DateFromLabel" runat="server" Text='<%# Eval("DateFrom") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                            <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                                <th runat="server"></th>
                                                <th runat="server">ID</th>
                                                <th runat="server">Name</th>
                                                <th runat="server">Amount</th>
                                                <th runat="server">OperationType</th>
                                                <th runat="server">DateFrom</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                        <asp:DataPager ID="dp" runat="server">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                <asp:NumericPagerField />
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                </td>
                                <td>
                                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="AmountLabel" runat="server" Text='<%# Eval("Amount") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OperationTypeLabel" runat="server" Text='<%# Eval("OperationType") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DateFromLabel" runat="server" Text='<%# Eval("DateFrom") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:ObjectDataSource ID="ods" runat="server" OnDeleting="OnDeleting" SelectMethod="LoadGoodsStatusByStorageId" TypeName="BLL.GoodsStatusManager" DeleteMethod="Delete">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="hfstorageId" Name="storageId" PropertyName="Value" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
