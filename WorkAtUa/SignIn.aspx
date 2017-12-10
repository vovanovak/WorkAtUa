<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="WorkAtUa.SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/sign_in_style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
   
        
        <div id="mainSignIn">
            <div id="mainSignInExp">
                <span style="font-size: 20pt; display: block; margin-top: 20px; font-weight: bold;">Access to all posibilites of Work.at.ua</span><br />
                <span style="font-size: 15pt; margin-top: 100px;">Some functions are availible only for registered users</span>
            </div>

            <div>

            <div style="width: 880px; margin-left: auto; margin-right: auto;">
                <div id="mainSignInSelectType">
                    <div id="mainSignInSelectTypeHeader">Not Registred?</div>
                    <div id="mainSignInSelectControls">
                        <div id="mainSignInSelectControlsExp">After spending about 5 minutes you'll be able to:</div>
                    
                        <div runat="server" id="mainSignInSelectControlsListContainer">
                            <ul id="mainSignInSelectControlsList">
                                <li><img src="Images/login-li.gif" />&nbsp;Create resume</li>
                                <li><img src="Images/login-li.gif" />&nbsp;Receive new vacancies by email</li>
                                <li><img src="Images/login-li.gif" />&nbsp;Select liked vacancies</li>
                            </ul>
                        </div>
                            
                        <asp:RadioButtonList runat="server" ID="userRole" CssClass="mainSignInSelectControlsRoles" RepeatDirection="Horizontal" CellSpacing="10" CellPadding="10" AutoPostBack="true" OnSelectedIndexChanged="userRole_SelectedIndexChanged">
                            <asp:ListItem Selected="True">I'm employee</asp:ListItem>
                            <asp:ListItem>I'm employer</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button runat="server" ID="btnRegister" UseSubmitBehavior="true" Text="Register" OnClick="btnRegister_Click" CssClass="headerMenuButton" />
                    </div>
                </div>

                <div id="mainSignInForm" style="margin-left: 30px;">
                    <div id="mainSignInFormHeader">Registred?</div>
                    <div id="mainSignInFormControls">
                         <div><asp:Label runat="server" ID="lblEmail" Text="Email: " Font-Bold="true" /><asp:TextBox runat="server" ID="txtEmail" Width="170" CssClass="txtEmail" /></div>
                         <div style="margin-top: 12px;"><asp:Label runat="server" ID="lblPassword" Text="Password: " Font-Bold="true" /><asp:TextBox runat="server" ID="txtPassword" TextMode="Password"  Width="170"  /></div>
                         <div style="margin-top: 12px;"><asp:CheckBox runat="server" ID="rememberMe" Text="Remember me?"/></div>
                         <asp:Button runat="server" ID="btnSubmit" UseSubmitBehavior="true" Text="Sign In" OnClick="btnSubmit_Click" CssClass="headerMenuButton" />
                    </div>
                </div>

            </div>
            </div>
        </div>
    
</asp:Content>
