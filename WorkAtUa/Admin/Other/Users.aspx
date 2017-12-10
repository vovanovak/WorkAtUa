<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WorkAtUa.Admin.Other.AdminUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../Styles/null_style.css" />
    <link rel="stylesheet" href="../../Styles/admin_page_style.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="sidebar">
            <div style="width: 1100px;  margin: 0 auto;">
                <div runat="server" id="itemUsers" class="sidebarItem"><a href="Users.aspx" style="color: white; text-decoration: underline;">Users</a></div>
                <div runat="server" id="itemCategories" class="sidebarItem"><a href="Categories.aspx" style="color: white; text-decoration: none;">Categories</a></div></>
                <div runat="server" id="itemCities" class="sidebarItem"><a href="Cities.aspx" style="color: white; text-decoration: none;">Cities</a></div>
                <div runat="server" id="itemVacancies" class="sidebarItem"><a href="Vacancies.aspx" style="color: white; text-decoration: none;">Vacancies</a></div>
                <div runat="server" id="itemComments" class="sidebarItem"><a href="Comments.aspx" style="color: white; text-decoration: none;">Comments</a></div>
                <div runat="server" id="itemResumes" class="sidebarItem"><a href="Resumes.aspx" style="color: white; text-decoration: none;">Resumes</a></div>
                <div runat="server" id="itemCompanies" class="sidebarItem"><a href="Companies.aspx" style="color: white; text-decoration: none;">Companies</a></div>
            </div>
        </div>
        <div id="main">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="updatePanel" >
                <ContentTemplate>
                    <div style="padding-top: 100px;">
                        <asp:Repeater runat="server" ID="repUsers" OnItemCommand="repUsers_ItemCommand1">
                           <HeaderTemplate>
                                <table>
                                    <th>Id</th>
                                    <th>Email</th>
                                    <th>Password</th>
                                    <th>Birthday</th>
                                    <th>Name</th>
                                    <th>Surname</th>
                                    <th>Fathers name</th>
                                    <th>Phone</th>
                                    <th>City</th>
                                    <th><asp:LinkButton runat="server" 
                                            CommandName="Add"
                                            Text="Add" Font-Underline="false" ForeColor="White"></asp:LinkButton></th>
                                    <th>Delete</th>
                                    <th>Edit</th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                      <%#Eval("Id")%>
                                    </td>
                                    <td>
                                        <%#Eval("Email")%>
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="txt" TextMode="Email" ToolTip="Email"  />
                                        
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPassword" CssClass="txt" TextMode="Password" ToolTip="Password"  />
                                    </td>
                                    <td>
                                        <%#Eval("Birthday")%>
                                        <asp:TextBox runat="server" ID="txtBirthday" CssClass="txt" TextMode="Date" ToolTip="Birthday" />
                                    </td>
                                    <td>
                                        <%#Eval("Name")%>
                                        <asp:TextBox runat="server" ID="txtName" CssClass="txt" ToolTip="Name" />
                                    </td>
                                    <td>
                                        <%#Eval("Surname")%>
                                        <asp:TextBox runat="server" ID="txtSurname" CssClass="txt" ToolTip="Surname" />
                                    </td>
                                    <td>
                                        <%#Eval("FathersName")%>
                                        <asp:TextBox runat="server" ID="txtFathersName" CssClass="txt" ToolTip="FathersName" />
                                    </td>
                                    <td>
                                        <%#Eval("Phone")%>
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="txt" TextMode="Number" ToolTip="Phone" />
                                    </td>
                                    <td>
                                        <%#Eval("City")%>
                                        <asp:DropDownList runat="server" ID="dropCity" CssClass="txt" ToolTip="City" />
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" 
                                            CommandName="Add"
                                            Text="Add"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" 
                                            CommandName="Delete"
                                            CommandArgument='<%# Eval("Id") %>'
                                            Text="Delete"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" 
                                            CommandName="Edit"
                                            CommandArgument='<%# Eval("Id") %>'
                                            Text="Edit"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <div style="float:left;margin-left: 46%;">
                            <asp:Button runat="server" ID="btnSave" CommandName="Save" OnClick="btnSave_Click" CssClass="txt" Text="Save" />
                            <asp:Button runat="server" ID="btnCancel" CommandName="Cancel" OnClick="btnCancel_Click" CssClass="txt" Text="Cancel" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
