﻿<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StoragesAndGoods.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
                    <div class="row">
                        <div class="col-md-8">
                            <section id="loginForm">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                                CssClass="text-danger" ErrorMessage="The email field is required." />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-offset-2 col-md-10">
                                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
                                            <asp:Label runat="server" ID="loginMessage" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </section>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
