<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Interfaz_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 class="text-center m-4">Ingresa a tu cuenta</h1>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="form-outline mb-4">
                    <asp:TextBox runat="server" TextMode="Email" CssClass="form-control" ID="txtEmail" />
                    <label class="form-label" for="txtEmail">Email</label>
                </div>
                <div class="form-outline mb-4">
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtPass" />
                    <label class="form-label" for="txtPass">Contraseña</label>
                </div>
            </div>
        </div>

        <div class="row mb-4 justify-content-center">
            <div class="col-md-6 d-flex justify-content-center">
                <asp:Button Text="Ingresar" ID="btnIngresar" OnClick="btnIngresar_Click" style="width:80%;" CssClass="btn btn-primary btn-block" runat="server" />
            </div>
        </div>
        <div class="text-center">
            <p>Todavia no te registrates? <a href="Registro.aspx">Registrate</a></p>
        </div>
    </div>
</asp:Content>
