﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Interfaz_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            userNegocio userNegocio = new userNegocio();
            User user = new User();
            try
            {
                //validaciones
                Page.Validate();
                if (!Page.IsValid)
                    return;
                Helper helper = new Helper();
                if (helper.estaVacio(txtEmail.Text) || helper.estaVacio(txtEmail.Text))
                {
                    lbIncorrecto.Visible = true; 
                    lbIncorrecto.Text = "Completar todos los campos!";
                    return;
                }

                user.Email = txtEmail.Text;
                user.Pass = txtPass.Text;
                if (userNegocio.login(user))
                {
                    Session.Add("usuario", user);
                    string paginaAnterior = (string)Session["paginaAnterior"] ?? "Default.aspx";
                    Response.Redirect(paginaAnterior, false);
                }
                else
                {
                    lbIncorrecto.Text = "Contraseña o email incorrectos!";
                    lbIncorrecto.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}