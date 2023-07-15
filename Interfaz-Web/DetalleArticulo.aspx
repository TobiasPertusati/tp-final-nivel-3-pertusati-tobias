<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Interfaz_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if ((Dominio.Articulo)Session["articulodetalle"] != null)
        {%>
    <section class="darticulo container my-5">
        <div class="row mt-5">
            <%Dominio.Articulo articulo = (Dominio.Articulo)Session["articulodetalle"];%>
            <div class="col-lg-5 col-md-12 col-12">
                <h5 class="mt-3 mb-3 me-5 pe-5 d-inline"><%:articulo.Categoria.Descripcion%> / <%:articulo.Marca.Descripcion%></h5>
                <div class="d-inline-block">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button Text="Favorito" CssClass="btn btn-outline-primary ms-5 pb-1" ID="btnFavorito" OnClick="btnFavorito_Click" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <img src="<%:articulo.UrlImagen%>" class="img-fluid object-fit-contain" style="width: 500px; height: 450px;" alt="Alternate Text" />
            </div>
            <div class="col-lg-6 col-md-12 col-12">
                <h3 class="py-4"><%:articulo.Nombre %></h3>
                <h2>$ <%:articulo.Precio%></h2>
                <h4 class="mt-5 mb-5">Detalle del Articulo</h4>
                <span><%:articulo.Descripcion%></span>
            </div>
        </div>
    </section>
    <section id="relacionados" class="my-5 pb-5">
        <div class="container text-center">
            <h3>Productos Relacionados</h3>
            <hr />
        </div>
        <%-- Leer de base de datos productos relacionados diferenciandolos x categoria (MOSTRAR 4 PRODUCTOS RELACIONADOS) --%>
        <div class="row mx-auto container-fluid">
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
