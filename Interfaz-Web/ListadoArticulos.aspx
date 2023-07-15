<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ListadoArticulos.aspx.cs" Inherits="Interfaz_Web.ListadoArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Listado de Articulos</h1>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-12 col-lg-11">
                <label for="txtFiltroRapido" class="form-label fs-5">Buscar:</label>
                <asp:TextBox runat="server" CssClass="form-control-sm" ID="txtFiltroRapido" />
            </div>
        </div>
        <div class="row justify-content-center mt-3 mb-3">
            <div class="col-12 col-lg-11">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="dgvArticulos" AutoGenerateColumns="false" OnPageIndexChanging="dgvArticulos_PageIndexChanging" CssClass="table table-bordered table-light table-responsive-sm"
                            DataKeyNames="Id" AllowPaging="true" PageSize="20" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
                                <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                                <asp:CommandField HeaderText="Editar" ShowSelectButton="true" SelectText="✏" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-auto mx-auto">
                <a href="FormularioArticulo.aspx" class="btn btn-primary">Agregar</a>
            </div>
        </div>
    </div>
</asp:Content>
<%--     Id 
         Codigo
         Nombre 
         Descripcion 
         Marca     
         Categoria 
         UrlImagen 
         Precio --%>