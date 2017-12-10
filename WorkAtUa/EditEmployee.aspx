<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="WorkAtUa.EditEmployee" %>
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
            <h1 id="mainContentExpTitle">Edit of employee</h1>
            <div id="mainContentExpFeautures">After you will spend a pair of minutes you will change your resume</div>
        </div>
        <div id="mainContentForm">
            
            <div id="mainContentFormCategoryFirst"><span style="margin-left: 10px;">Personal info</span></div>
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

                <div class="mainContentFormCategory"><span style="margin-left: 10px;">Resume Info</span></div>
            <div class="mainContentFormControlContainer">
                <div class="mainContentFormControlContainerItem">
                    <div><asp:Label runat="server" ID="lblVacancyHeader" Text="Resume header: " CssClass="mainContentFormLabel" /></div>
                    <asp:TextBox runat="server" ID="txtVacancyHeader" CssClass="mainContentFormTextBox" />
                    <asp:RegularExpressionValidator runat="server" ID="regVacancyHeader" ControlToValidate="txtVacancyHeader" ValidationExpression="^[\w\s]+$" Display="Dynamic"> 
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Resume header should be written correctly!</span>
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVacancyHeader" ID="reqVacancyHeader" Display="Dynamic">
                        <img src="Images/err.gif" style="position: relative; left: -26px; top: 2px;" /> 
                        <br /> 
                        <span style="color:red;">* Please, write vacancy header!</span>
                    </asp:RequiredFieldValidator>
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
            </div>

            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" UseSubmitBehavior="true" Text="Register" CssClass="headerMenuButton"/>
        </div>
        
    </div>
</asp:Content>
