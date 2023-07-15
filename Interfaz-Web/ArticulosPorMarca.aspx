<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ArticulosPorMarca.aspx.cs" Inherits="Interfaz_Web.ArticulosPorMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center" runat="server" id="titulo"></h1>
    <div class="container-lg">
        <div class="row gy-4 mb-3 mt-1">
            <%foreach (Dominio.Articulo articulo in (List<Dominio.Articulo>)Session["listaxmarca"])
              {%>
            <div class="col-md-3">
                <div class="card">
                    <img src="<%:articulo.UrlImagen%>" class="card-img-top object-fit-contain" style="width: auto; height: 214px;" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%: articulo.Nombre %></h5>
                        <p class="card-text"><%: articulo.Descripcion %></p>
                    </div>
                    <div class="card-footer d-flex justify-content-between">
                        <b class="text-body-emphasis pt-2">$ <%:articulo.Precio%></b>
                        <a href="DetalleArticulo.aspx?id=<%:articulo.Id%>" class="btn btn-link">Ver en detalle</a>
                    </div>
                </div>
            </div>
            <%}%>
        </div>
    </div>
</asp:Content>
