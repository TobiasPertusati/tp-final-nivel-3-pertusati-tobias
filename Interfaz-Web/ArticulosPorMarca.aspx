<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ArticulosPorMarca.aspx.cs" Inherits="Interfaz_Web.ArticulosPorMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-lg">
        <%if ((List<Dominio.Articulo>)Session["listaxmarca"] != null)
          {%>
        <h1 class="text-center" runat="server" id="titulo"></h1>
        <div class="row gy-4 mb-3 mt-1">
            <%foreach (Dominio.Articulo articulo in (List<Dominio.Articulo>)Session["listaxmarca"])
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
        <%}%>
    </div>
</asp:Content>
