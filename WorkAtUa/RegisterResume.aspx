<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterResume.aspx.cs" Inherits="WorkAtUa.RegisterResume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/register_employer_style.css" />
    <script src="Scripts/jquery-2.1.1.js"></script>
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
            var chkListModules = document.getElementById('<%= chkBoxCategories.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidateModuleList1(source, args) {
            var chkListModules = document.getElementById('<%= chkBoxTypeOfEmployment.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        var noJobChecked = false;
        var jobHtml;
        function jobExp() {
            if (noJobChecked == false) {
                noJobChecked = true;
                $('#jobExpDiv').slideUp();
                jobHtml = $('#jobExpDiv').html();
                $('#jobExpDiv').remove();
            }
            else {
                noJobChecked = false;
                $('#jobExpOuterDiv').append(jobHtml);
                $('#jobExpDiv').slideDown();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
     <div id="mainContent">
        <div id="mainContentExp">
            <h1 id="mainContentExpTitle">Registration of resume</h1>
            <div id="mainContentExpFeautures">After you will spend a pair of minutes you will create resume</div>
        </div>
        <div id="mainContentForm">
            <div id="mainContentFormCategoryFirst"><span style="margin-left: 10px;">Resume Info</span></div>
            <div class="mainContentFormControlContainer">
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblVacancyHeader" Text="Resume header: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtVacancyHeader" CssClass="mainContentFormTextBox" />
                    <asp:RegularExpressionValidator runat="server" ID="regVacancyHeader" ControlToValidate="txtVacancyHeader" ValidationExpression="^[\w\s]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Vacancy header should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVacancyHeader" ID="reqVacancyHeader" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write vacancy header!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblCategories" Text="Categories: " CssClass="mainContentFormLabel" /></div>
                    <asp:CheckBoxList runat="server" ID="chkBoxCategories" CssClass="mainContentFormControlContainerItemCompanyType" CellSpacing="4" Font-Size="Larger">
                        
                    </asp:CheckBoxList>
                    <asp:CustomValidator runat="server" ID="reqCategories"
                      ClientValidationFunction="ValidateModuleList"
                      ErrorMessage="* Please, select categories!" ForeColor="Red" ></asp:CustomValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblCity" Text="City: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtCity" CssClass="mainContentFormTextBox" />

                    <asp:RegularExpressionValidator runat="server" ID="regCity" ControlToValidate="txtCity" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* City should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCity" ID="reqCity" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write city!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblTypeOfEmployment" Text="Type of employment: " CssClass="mainContentFormLabel" /></div>
                    <asp:CheckBoxList runat="server" ID="chkBoxTypeOfEmployment" CssClass="mainContentFormControlContainerItemCompanyType" CellSpacing="4" Font-Size="Larger">
                        <asp:ListItem Text="Full time" />
                        <asp:ListItem Text="Part time" />
                        <asp:ListItem Text="Distance job" />
                    </asp:CheckBoxList>
                     <asp:CustomValidator runat="server" ID="reqTypeOfEmp"
                      ClientValidationFunction="ValidateModuleList1"
                      ErrorMessage="* Please, select type of employment!" ForeColor="Red" ></asp:CustomValidator>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblSalary" Text="Salary: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtSalary" TextMode="Number" CssClass="mainContentFormTextBox" />
                     
                    <asp:RegularExpressionValidator runat="server" ID="regSalary" ControlToValidate="txtSalary" ValidationExpression="^[0-9]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Salary should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSalary" ID="reqSalary" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write salary!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormCategory"><span style="margin-left: 10px;">Job experience</span></div>
                <div class="mainContentFormControlContainerItem">
                    
                   <%-- <input runat="server" type="checkbox" value="No job experience" id="noJobExp" onclick="jobExp()" /> No job experience--%>
                </div>
               
                <div id="jobExpOuterDiv">
                <div id="jobExpDiv">
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblCompany" Text="Company: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtCompany" CssClass="mainContentFormTextBox" />
                    
                        <asp:RegularExpressionValidator runat="server" ID="regCompany" ControlToValidate="txtCompany" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Company name should be written correctly!</span>
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompany" ID="reqCompany" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write company name!</span>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblCityExp" Text="City: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtCityExp" CssClass="mainContentFormTextBox" />
                         <asp:RegularExpressionValidator runat="server" ID="regExpCity" ControlToValidate="txtCityExp" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* City where the firm is situated should be written correctly!</span>
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCityExp" ID="reqExpCity" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write city where the firm is situated!</span>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblPost" Text="Post: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtPost" CssClass="mainContentFormTextBox" />

                         <asp:RegularExpressionValidator runat="server" ID="regPost" ControlToValidate="txtPost" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Post should be written correctly!</span>
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPost" ID="reqPost" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write your post!</span>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblStart" Text="Start date of work: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtStartDate" TextMode="Date" CssClass="mainContentFormTextBox" />

                         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate" ID="reqStart" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write your start date of work!</span>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblEnd" Text="End date of work: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtEndDate" TextMode="Date" CssClass="mainContentFormTextBox" />

                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate" ID="reqEnd" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write your end date of work!</span>
                        </asp:RequiredFieldValidator>

                        <asp:CompareValidator runat="server" ID="cmpDates" ControlToValidate="txtEndDate" ControlToCompare="txtStartDate"
                            Type="Date" Operator="GreaterThan" Display="Dynamic">
                            <br />
                            <span style="color:red;">* End date must be bigger than start date!</span>
                        </asp:CompareValidator>
                    </div>
                    <div class="mainContentFormControlContainerItem">
                        <div><asp:Label runat="server" ID="lblDesc" Text="Your work description: " CssClass="mainContentFormLabel" /></div>
                        <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Width="550" Height="200" CssClass="mainContentFormTextBox" />

                         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDesc" ID="reqExpDesc" Display="Dynamic">
                            <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                            <br /> 
                            <span style="color:red;">* Please, write your prev job description!</span>
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
             </div>
                <div class="mainContentFormCategory"><span style="margin-left: 10px;">Education</span></div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblEduLevel" Text="Education level: " CssClass="mainContentFormLabel" /></div>
                    <asp:DropDownList runat="server" ID="dropEduLevel" CssClass="mainContentFormTextBox">
                        <asp:ListItem Text="Higher" />
                        <asp:ListItem Text="Not ended higher" />
                        <asp:ListItem Text="Special avarage" />
                        <asp:ListItem Text="Avarage" />
                    </asp:DropDownList>
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblEduPlace" Text="Place: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtPlace" CssClass="mainContentFormTextBox" />

                     <asp:RegularExpressionValidator runat="server" ID="regPlace" ControlToValidate="txtPlace" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Place should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPlace" ID="reqPlace" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write your education place!</span>
                    </asp:RequiredFieldValidator>
                   
                </div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblSpeciality" Text="Speciality: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtSpeciality" CssClass="mainContentFormTextBox" />

                     <asp:RegularExpressionValidator runat="server" ID="regSpec" ControlToValidate="txtSpeciality" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Speciality should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSpeciality" ID="reqSpec" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write your speciality!</span>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mainContentFormCategory"><span style="margin-left: 10px;">Additional</span></div>
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblSomeWordsAboutU" Text="Some words about you: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtSelfDesc" TextMode="MultiLine" Width="550" Height="200" CssClass="mainContentFormTextBox" />

                    
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSelfDesc" ID="reqSelfDesc" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write your self description!</span>
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Register" CssClass="headerMenuButton"/>
        </div>
    </div>
</asp:Content>
