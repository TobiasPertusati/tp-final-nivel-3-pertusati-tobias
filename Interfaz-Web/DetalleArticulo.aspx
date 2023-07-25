<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Interfaz_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if ((Dominio.Articulo)Session["articulodetalle"] != null)
      {%>
    <section class="darticulo container mt-5 mb-3">
        <div class="row">
            <%Dominio.Articulo articulo = (Dominio.Articulo)Session["articulodetalle"];%>
            <div class="col-lg-5 col-md-6 col-12 position-relative">
                <h5 class="my-3 d-inline"><%:articulo.Categoria.Descripcion%> / <%:articulo.Marca.Descripcion%></h5>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Button Text="Favorito" CssClass="btn btn-outline-primary pb-1 position-absolute end-0 top-0 me-2" ID="btnFavorito" OnClick="btnFavorito_Click" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <img src="<%:articulo.UrlImagen%>" class="img-fluid object-fit-contain" style="width: 500px; height: 450px;" alt="Alternate Text" />
            </div>
            <div class="col-lg-6 col-md-6 col-12">
                <h3 class="py-4"><%:articulo.Nombre %></h3>
                <h2>$ <%:articulo.Precio%></h2>
                <h4 class="mt-5 mb-5">Detalle del Articulo</h4>
                <span><%:articulo.Descripcion%></span>
            </div>
        </div>
    </section>
    <section id="relacionados" class="my-4">
        <div class="container text-center">
            <h3>Productos Relacionados</h3>
            <hr />
        </div>
        <%-- Genero una lista de 4 prodructos relacionados x categoria (MUESTRO 4 PRODUCTOS RELACIONADOS) --%>
        <div class="row mx-auto container-fluid gy-3">
            <%foreach (Dominio.Articulo articulosRelacionado in (List<Dominio.Articulo>)Session["listaxcategoria"])
              {%>
            <div class="articulo text-center col-lg-3 col-md-4 col-12">
                <img src="<%:articulosRelacionado.UrlImagen %>" class="object-fit-contain mb-3 card-img-top" style="width: 200px; height: 200px;" alt="Alternate Text" />
                <h5 class="a-nombre"><%:articulosRelacionado.Nombre%></h5>
                <h4 class="a-precio">$<%:articulosRelacionado.Precio%></h4>
                <a href="DetalleArticulo.aspx?id=<%:articulosRelacionado.Id%>" class="btn btn-outline-dark">Ver</a>
            </div>
            <%}%>
        </div>
    </section>
    <%}%>
</asp:Content>
