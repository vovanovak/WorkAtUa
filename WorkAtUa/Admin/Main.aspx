<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WorkAtUa.AdminMainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Styles/null_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/admin_page_style.css" />
</head>
<body>
    <form id="form1" runat="server" style="min-height: 100%;">
        
        <div style="width: 100%; text-align:center; position: absolute; top:50%;margin-top: -100px;">
            <asp:Label runat="server" ID="h1" Text="Select table to administrate" Font-Size="XX-Large"></asp:Label>
        </div>
        <div id="sidebar" style="position: absolute; top:50%;margin-top: -30px;">
            
            <div style="width: 1100px;  margin: 0 auto;">
                <div runat="server" id="itemUsers" class="sidebarItem"><a href="Other/Users.aspx" style="color: white; text-decoration: none;">Users</a></div>
                <div runat="server" id="itemCategories" class="sidebarItem"><a href="Other/Categories.aspx" style="color: white; text-decoration: none;">Categories</a></div></>
                <div runat="server" id="itemCities" class="sidebarItem"><a href="Other/Cities.aspx" style="color: white; text-decoration: none;">Cities</a></div>
                <div runat="server" id="itemVacancies" class="sidebarItem"><a href="Other/Vacancies.aspx" style="color: white; text-decoration: none;">Vacancies</a></div>
                <div runat="server" id="itemComments" class="sidebarItem"><a href="Other/Comments.aspx" style="color: white; text-decoration: none;">Comments</a></div>
                <div runat="server" id="itemResumes" class="sidebarItem"><a href="Other/Resumes.aspx" style="color: white; text-decoration: none;">Resumes</a></div>
                <div runat="server" id="itemCompanies" class="sidebarItem"><a href="Other/Companies.aspx" style="color: white; text-decoration: none;">Companies</a></div>
            </div>
        </div>
        
    </form>
</body>
</html>
