<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Interfaz_Web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-light-emphasis mb-2">Favoritos</h1>
    <p class="text-muted text-center">Aquí encontras todos tus articuos favoritos</p>
    <div class="container">
        <%if ((List<Dominio.Articulo>)Session["listaFavoritos"] != null)
          {%>
        <asp:Repeater runat="server" ID="repFavs">
            <ItemTemplate>
                <div class="card mb-3" style="max-width: 1200px;">
                    <div class="row g-0 justify-content-center">
                        <div class="col-md-4">
                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>" style="width: auto; height: 214px;" class="justify-content-center d-flex">
                                <img src="<%#Eval("UrlImagen") %>" class="object-fit-contain img-fluid rounded-start" style="width: auto; height: 214px;" alt="...">
                            </a>                           
                        </div>
                        <div class="col-md-8">
                            <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CommandArgument='<%#Eval("Id")%>' CommandName="idArticulo" CssClass="bottom-0 btn btn-outline-danger end-0 mb-3 me-3 position-absolute" runat="server" />
                            <div class="card-body">
                                <h6 class="card-subtitle"><%#Eval("Marca.Descripcion")%> / <%#Eval("Categoria.Descripcion")%></h6>
                                <h5 class="card-title"><%#Eval("Nombre")%></h5>
                                <p class="card-text"><%#Eval("Descripcion")%></p>
                                <b class="text-body-emphasis pt-2">$ <%#Eval("Precio")%></b>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%}%>
    </div>
</asp:Content>
