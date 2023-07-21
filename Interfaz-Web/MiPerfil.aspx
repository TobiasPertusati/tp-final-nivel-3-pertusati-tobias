<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Interfaz_Web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-auto mx-auto">
                <div class="mb-5 pt-3">
                    <h1>Mi Perfil</h1>
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-12 col-md-6 col-lg-5">
                <div style="margin-bottom:36px;">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingrese su nombre" CssClass="text-danger" ControlToValidate="txtNombre" runat="server" />
                </div>
                <div class="mb-3">
                    <label for="txtApellido" class="form-label">Apellido</label>
                    <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingrese su apellido" CssClass="text-danger" ControlToValidate="txtApellido" runat="server" />
                </div>
                <asp:Label Text="Completar todos los campos" ID="lbFaltanCampos" CssClass="text-danger" Visible="false" runat="server" />
            </div>
            <div class="col-12 col-md-6 col-lg-5">
                <div class="mb-1">
                    <label for="txtImagen" class="form-label">Imagen Perfil</label>
                    <input type="file" id="txtImagen" runat="server" class="form-control mb-3" />
                </div>
                <div class="mb-4 d-flex justify-content-center align-items-center">
                    <asp:Image ImageUrl="https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM="
                        ID="imgPerfil" runat="server" CssClass="img-fluid mb-3" Width="250" Height="250" />
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-auto col-max">
                <asp:Button Text="Guardar" class="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" />
                <a href="Default.aspx" class="btn btn-secondary">Regresar</a>
            </div>
        </div>
    </div>
</asp:Content>
