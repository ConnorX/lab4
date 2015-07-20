<%@ Page Title="Contoso University - Departments" Language="C#" MasterPageFile="~/monday.Master" AutoEventWireup="true" CodeBehind="departments.aspx.cs" Inherits="comp2007_lesson11_mon.departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Departments</h1>

    <a href="department.aspx">Add Department</a>

    <asp:GridView ID="grdDepartments" runat="server" CssClass="table table-striped"
        AutoGenerateColumns="false" OnRowDeleting="grdDepartments_RowDeleting"
        DataKeyNames="DepartmentID">
        <Columns>        
            <asp:BoundField DataField="Name" HeaderText="Department Name" />
            <asp:BoundField DataField="Budget" HeaderText="Budget" DataFormatString="{0:c}" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="department.aspx" 
                 Text="Edit" DataNavigateUrlFields="DepartmentID"
                 DataNavigateUrlFormatString="department.aspx?DepartmentID={0}" />
            <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
