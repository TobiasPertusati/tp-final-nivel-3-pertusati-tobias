<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Interfaz_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-light-emphasis mb-2">Nuestros productos</h1>
    <p class="text-muted text-center">Aquí encontras todos nuestros productos</p>
    <div class="container">
        <div class="row gy-4 mb-4 mt-1">
            <%foreach (Dominio.Articulo articulo in listaArticulos)
              {%>
            <div class="col-md-4 col-12 col-lg-3">
                <div class="card">
                    <a href="DetalleArticulo.aspx?id=<%:articulo.Id%>" style="width: auto; height: 214px;" class="justify-content-center d-flex">
                        <img src="<%:articulo.UrlImagen%>" class="object-fit-contain img-fluid" style="width: auto; height: 214px;" alt="...">
                    </a>
                    <div class="card-body">
                        <h5 class="card-title"><%: articulo.Nombre %></h5>
                    </div>
                    <div class="card-footer d-flex justify-content-between">
                        <b class="text-body-emphasis pt-2">$ <%:articulo.Precio%></b>
                    </div>
                </div>
            </div>
            <%}%>
        </div>
        <h2 class="text-center text-light-emphasis mb-4">Nuestras Marcas</h2>
        <div class="row gy-4 mb-4 justify-content-center">
            <% // Aca las marcas podrian tener asociadas una IMAGEN/URL en la base, así no las tendria que cargar en el load
            foreach (Dominio.Marca marca in listaMarcas)
            {%>
            <div class="col-md-4 col-sm-10 col-lg-2">
                <div class="card">
                    <img src="<%:marca.UrlImagen%>" class="card-img-top object-fit-contain" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%:marca.Descripcion%></h5>
                        <a href="ArticulosPorMarca.aspx?Marca=<%:marca.Descripcion%>" class="btn btn-outline-primary">Ver productos</a>
                    </div>
                </div>
            </div>
          <%}%>
        </div>
    </div>
</asp:Content>
