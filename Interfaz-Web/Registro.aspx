<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Interfaz_Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container h-100">
        <div class="row d-flex justify-content-center align-items-center h-100 my-3">
            <div class="col-lg-12 col-xl-11">
                <div class="card text-black" style="border-radius: 25px;">
                    <div class="card-body p-md-5">
                        <div class="row justify-content-center">
                            <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">

                                <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Crea tu cuenta</p>

                                <div class="mx-1 mx-md-4">
                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <%-- imagen --%>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" TextMode="Email" />
                                            <label class="form-label" for="txtEmail">Tu Email</label>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <%-- imagen --%>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtPass" TextMode="Password" />
                                            <label class="form-label" for="txtPass">Contraseña</label>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <%-- imagen --%>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmaPass" TextMode="Password" />
                                            <label class="form-label" for="txtConfirmaPass">Repeti tu contraseña</label>
                                        </div>
                                    </div>

                                    <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                                        <asp:Label Text="" Visible="false" ID="lbError" runat="server" />
                                    </div>
                                    <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                                        <asp:Button Text="Registrarse" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" CssClass="btn btn-primary btn-lg" runat="server" />
                                    </div>

                                </div>

                            </div>
                            <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
                                <img src="https://img.freepik.com/free-vector/access-control-system-abstract-concept_335657-3180.jpg?w=826&t=st=1688943291~exp=1688943891~hmac=15f256d873d1623219450e4f1624619208eaff594e4eb888100daf543046b4e9"
                                    class="img-fluid" alt="Sample image">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
