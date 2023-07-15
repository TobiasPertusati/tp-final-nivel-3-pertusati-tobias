<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Interfaz_Web.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center mb-3">Hubo un problema.</h1>
    <asp:Label Text="" CssClass="text-center d-block mb-5" ID="lblMensaje" runat="server" />
</asp:Content>
