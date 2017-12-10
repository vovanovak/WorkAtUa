<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterVacancy.aspx.cs" Inherits="WorkAtUa.RegisterVacancy" %>
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
            var chkListModules = document.getElementById('<%= vacCategories.ClientID %>');
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
            <h1 id="mainContentExpTitle">Registration of vacancy</h1>
            <div id="mainContentExpFeautures">After you will spend a pair of minutes you will create vacancy</div>
        </div>
        <div id="mainContentForm">
            <div id="mainContentFormCategoryFirst"><span style="margin-left: 10px;">Vacancy Info</span></div>
            <div class="mainContentFormControlContainer">
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelProfession" Text="Profession: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="userProfession" CssClass="mainContentFormTextBox" />

                     <asp:RegularExpressionValidator runat="server" ID="regProf" ControlToValidate="userProfession" ValidationExpression="^[\w\s]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Profession should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userProfession" ID="reqProf" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write profession!</span>
                    </asp:RequiredFieldValidator>
                </div> 
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelCategories" Text="Categories: " CssClass="mainContentFormLabel" /></div>
                    <asp:CheckBoxList runat="server" Id="vacCategories" CssClass="mainContentFormControlContainerItemCompanyType" CellSpacing="4" Font-Size="Larger">
                        
                    </asp:CheckBoxList>

                     <asp:CustomValidator runat="server" ID="CustomValidator1"
                      ClientValidationFunction="ValidateModuleList"
                      ErrorMessage="* Please, select categories!" ForeColor="Red" ></asp:CustomValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelTypeOfEmp" Text="Type of employment: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtTypeOfEmp" CssClass="mainContentFormTextBox" />

                    
                    <asp:RegularExpressionValidator runat="server" ID="regType" ControlToValidate="txtTypeOfEmp" ValidationExpression="^[\w\s]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Type of employment should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTypeOfEmp" ID="reqType" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write type of employment!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelCity" Text="City: " CssClass="mainContentFormLabel" /></div>
                    <asp:DropDownList runat="server" ID="userCity" CssClass="mainContentFormTextBox" />
                    
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelRequirments" Text="Requirments: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtReq" CssClass="mainContentFormTextBox" Width="550"/>

                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReq" ID="reqReq" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write requirments!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelMinSalary" Text="Min salary: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox TextMode="Number" runat="server" ID="txtMinSalary"  CssClass="mainContentFormTextBox" />
                    <asp:RegularExpressionValidator runat="server" ID="regMinSalary" ControlToValidate="txtMinSalary" ValidationExpression="^[0-9]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Min salary should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMinSalary" ID="reqMinSalary" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write min salary!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelMaxSalary" Text="Max salary: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox TextMode="Number" runat="server" ID="txtMaxSalary"  CssClass="mainContentFormTextBox" />
                     <asp:RegularExpressionValidator runat="server" ID="regMaxSalary" ControlToValidate="txtMaxSalary" ValidationExpression="^[0-9]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Max salary should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMaxSalary" ID="reqMaxSalary" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write max salary!</span>
                    </asp:RequiredFieldValidator>
                    
                    <asp:CompareValidator runat="server" ID="cmpSalaries" ControlToValidate="txtMaxSalary" ControlToCompare="txtMinSalary"
                        Type="Integer" Operator="GreaterThan" Display="Dynamic">
                        <br />
                        <span style="color:red;">* Max salary must be bigger than min salary!</span>
                    </asp:CompareValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="labelDesc" Text="Description: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtDesc" CssClass="mainContentFormTextBox" Width="550" Height="200" />
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc" ID="reqDesc" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write description!</span>
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <asp:Button runat="server" ID="btnSubmit" Text="Register" CssClass="headerMenuButton" OnClick="btnSubmit_Click"/>
        </div>
    </div>
</asp:Content>
