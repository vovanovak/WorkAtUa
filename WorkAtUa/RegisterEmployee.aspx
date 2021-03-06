﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterEmployee.aspx.cs" Inherits="WorkAtUa.RegisterEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="Styles/register_employer_style.css" />
     <script>
         function CheckMyCompanyWebSite(sender, args) {
             args.IsValid = args.Value.match("^+$");
             return;
         }
         function CheckMyText(sender, args) {
             args.IsValid = args.Value.match("^[a-zA-Z]+$");
             return;
         }
         function CheckMyPhone(sender, args) {
             args.IsValid = args.Value.match("^[0-9]+$");
             return;
         }
         function CheckMyEmail(sender, args) {
             args.IsValid = args.Value.match("^[\w-]+@([\w-]+\.)+[\w-]+$");
             return;
         }
         function CheckMyPass(sender, args) {
             args.IsValid = args.Value.match("^[a-zA-Z0-9]+$");
             return;
         }
         function ValidateModuleList(source, args) {
             var chkListModules = document.getElementById('<%= userCategories.ClientID %>');
             var chkListinputs = chkListModules.getElementsByTagName("input");
             for (var i = 0; i < chkListinputs.length; i++) {
                 if (chkListinputs[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div id="mainContent">
        <div id="mainContentExp">
            <h1 id="mainContentExpTitle">Registration of employee</h1>
            <div id="mainContentExpFeautures">After you will spend a pair of minutes you will be able to create resumes and view contancts of employers</div>
        </div>
        <div id="mainContentForm">
            <div id="mainContentFormCategoryFirst"><span style="margin-left: 10px;">Authorization</span></div>
            <div class="mainContentFormControlContainer">
                <div class="mainContentFormControlContainerItem">
                   <div><asp:Label runat="server" ID="labelEmail" Text="Email: " CssClass="mainContentFormLabel" /></div>
                   <asp:TextBox runat="server" ID="userEmail" CssClass="mainContentFormTextBox" />
                   <asp:RegularExpressionValidator runat="server" ID="emailValid1"
                            ControlToValidate="userEmail"
                            ValidationExpression="^[\w-]+@([\w-]+\.)+[\w-]+$"
                            Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Email should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userEmail" ID="reqEmail" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write email!</span>
                    </asp:RequiredFieldValidator>
                </div> 
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelPassword" Text="Password: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="userPassword" CssClass="mainContentFormTextBox" TextMode="Password"/> 
                   <asp:RegularExpressionValidator runat="server" ID="passwValid1" ControlToValidate="userPassword" ValidationExpression="^[a-zA-Z0-9]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Password should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userPassword" ID="reqPassword" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write password!</span>
                    </asp:RequiredFieldValidator>
                </div>
              
            </div>
               
            <div class="mainContentFormCategory"><span style="margin-left: 10px;">Personal info</span></div>
            <div class="mainContentFormControlContainer">
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelCity" Text="City: " CssClass="mainContentFormLabel" /></div>
                    <asp:DropDownList runat="server" ID="userCity" CssClass="mainContentFormTextBox" />
                    
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelBirthday" Text="Birthday: " CssClass="mainContentFormLabel" /></div>
                    <input runat="server" id="userBirthay" type="date" class="mainContentFormTextBox" />
                    <asp:RequiredFieldValidator runat="server" ID="reqBirthday" ControlToValidate="userBirthay">
                       <br /> 
                       <span style="color:red;">* Please, enter your birhday!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelName" Text="Name: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="userName" CssClass="mainContentFormTextBox"/>
                    <asp:RegularExpressionValidator runat="server" ID="regUserName" ControlToValidate="userName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Name should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userName" ID="reqName" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write name!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelSurname" Text="Surname: " CssClass="mainContentFormLabel" /> </div>
                    <asp:TextBox runat="server" ID="userSurname" CssClass="mainContentFormTextBox" />
                   
                    <asp:RegularExpressionValidator runat="server" ID="regSurname" ControlToValidate="userSurname" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Surname should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userSurname" ID="reqUserSurname" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write surname!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelFathersName" Text="Fathers name: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="userFathersName" CssClass="mainContentFormTextBox" />
                   <asp:RegularExpressionValidator runat="server" ID="regFathersName" ControlToValidate="userFathersName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Fathers name should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userFathersName" ID="reqFathersName" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write fathers name!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelPhone" Text="Phone: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="userPhone" CssClass="mainContentFormTextBox" />
                     <asp:RegularExpressionValidator runat="server" ID="regUserPhone" ControlToValidate="userPhone" ValidationExpression="^[0-9]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Phone should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userPhone" ID="reqUserPhone" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write phone!</span>
                    </asp:RequiredFieldValidator>
                </div>
                
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelCategories" Text="Categories: " CssClass="mainContentFormLabel" /></div>
                    <asp:CheckBoxList runat="server" Id="userCategories" CssClass="mainContentFormControlContainerItemCompanyType" CellSpacing="4" Font-Size="Larger"></asp:CheckBoxList>
                     <asp:CustomValidator runat="server" ID="reqCategories"
                      ClientValidationFunction="ValidateModuleList"
                      ErrorMessage="* Please, select categories!" ForeColor="Red" ></asp:CustomValidator>
                </div>
            </div>

            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" UseSubmitBehavior="true" Text="Register" CssClass="headerMenuButton"/>
        </div>
        
    </div>
</asp:Content>
