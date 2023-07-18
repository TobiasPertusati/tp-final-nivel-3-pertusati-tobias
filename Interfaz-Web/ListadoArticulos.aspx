<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ListadoArticulos.aspx.cs" Inherits="Interfaz_Web.ListadoArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 class="mb-4 mt-3 text-center">Listado de Articulos</h1>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row justify-content-between mb-2">
                    <div class="col-12 col-lg-10 col-md-9 d-flex">
                        <label for="txtFiltroRapido" class="fs-5">Buscar:</label>
                        <asp:TextBox runat="server" CssClass="form-control mt-1 ms-2" Style="width: 180px; height: 26px;" AutoPostBack="true" OnTextChanged="txtFiltroRapido_TextChanged" ID="txtFiltroRapido" />
                        <asp:ImageButton ImageUrl="~/Icons/refresh_64px.png" ID="btnRefresh" CssClass="btn" Style="width: 50px;" OnClick="btnRefresh_Click" runat="server" />
                    </div>
                    <div class="col-12 col-lg-2 col-md-3 d-flex mb-2">
                        <asp:CheckBox CssClass="ps-2 pt-2" Text="Filtro Avanzado" AutoPostBack="true" ID="chkFiltroAvanzado" OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" runat="server" />
                    </div>
                </div>
                <%if (FiltroAvanzado == true)
                  {%>
                <div class="row">
                    <div class="col-7 col-lg-2 col-md-3">
                        <div class="mb-3">
                            <label class="form-label">Campo:</label>
                            <asp:DropDownList ID="ddlCampo" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" runat="server">
                                <asp:ListItem Text="Precio" />
                                <asp:ListItem Text="Codigo" />
                                <asp:ListItem Text="Marca" />
                                <asp:ListItem Text="Categoria" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-7 col-lg-2 col-md-4">
                        <div class="mb-3">
                            <label class="form-label">Criterio:</label>
                            <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-12 col-lg-2 d-flex col-md-3">
                        <div class="mb-3">
                            <label class="form-label">Filtro:</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtFiltroAvanzado" />
                        </div>
                    </div>
                    <div class="align-items-md-end col-md-2 col-12 col-lg-2 d-flex justify-content-start">
                        <div class="mb-3">
                            <asp:Button Text="Buscar" ID="btnBusqueda" CssClass="btn btn-outline-primary" OnClick="btnBusqueda_Click" runat="server" />
                        </div>
                    </div>
                </div>
                <%}%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row justify-content-center mt-3 mb-3">
            <div class="col-12">
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
