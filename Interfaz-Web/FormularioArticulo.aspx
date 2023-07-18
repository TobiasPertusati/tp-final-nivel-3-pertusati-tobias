<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="Interfaz_Web.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Formulario Articulo</h1>
    <div class="container">
        <div class="row justify-content-center gy-3"> 
            <div class="col-md-5">
                <div class="mb-3 pb-3">
                    <label for="txtId" class="form-label">ID</label>
                    <asp:TextBox ID="txtId" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="mb-2">
                    <label for="txtCodigo" class="form-label">Codigo</label>
                    <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="txtCodigo" runat="server" />
                </div>
                <div class="mb-2">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="txtNombre" runat="server" />
                </div>
                <div class="mb-2">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="txtDescripcion" runat="server" />
                </div>
                <div class="mb-2">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="ddlMarca" runat="server" />
                </div>
                <div class="mb-2">
                    <label for="ddlCategoria" class="form-label">Categoria</label>
                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="ddlCategoria" runat="server" />
                </div>
            </div>
            <div class="col-md-5">
                <div class="mb-2">
                    <label for="txtPrecio" class="form-label">Precio</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPrecio" />
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger text-start" ControlToValidate="txtPrecio" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="Ingrese un monto valido" ValidationExpression="\s*([\d,]+)" CssClass="text-danger" ControlToValidate="txtPrecio" runat="server" />
                </div>
                <div class="mb-2">
                    <label for="txtUrlImagen" class="form-label">Url Imagen</label>
                    <asp:TextBox runat="server" OnTextChanged="txtUrlImagen_TextChanged" CssClass="form-control" ID="txtUrlImagen" />
                    <asp:RequiredFieldValidator ErrorMessage="Este campo es requerido" CssClass="text-danger" ControlToValidate="txtUrlImagen" runat="server" />
                </div>
                <div class="mb-2">
                    <asp:Image style="width:100%; height:350px" ID="imgArticulo" CssClass="object-fit-contain" ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png" runat="server" />
                </div>
            </div>
        </div>
        <div class="row justify-content-center mt-3 mb-4">
            <div class="col-auto">
                <div class="mb-3">
                    <asp:Button Text="" ID="btnAgregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" runat="server" />
                    <a href="ListadoArticulos.aspx" class="btn btn-secondary">Cancelar</a>
                    <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>