<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PrintPrototype._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


            <asp:Button ID="btToPDF" OnClick="btToPDF_Click" CssClass="btn btn-block" runat="server" Text="TO PDF" />
            <asp:Button ID="btPrint" OnClick="btPrint_Click" CssClass="btn btn-block" runat="server" Text="TO PRINT" />

          
</asp:Content>
