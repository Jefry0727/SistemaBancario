﻿using Platform.Comunication.controller;
using SistemaBancario.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBancario
{
    public partial class Login : Form
    {


        LoginController login = new LoginController();
        public Login()
        {
            InitializeComponent();
            tBClave.Text = "a";
            tBusuario.Text = "a";
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            String clave = tBClave.Text;
            String usuario = tBusuario.Text;
            if (!usuario.Equals(""))
            {
                if (!clave.Equals(""))
                {
                    if (login.solicitudLogin(usuario, clave))
                    {
                        Int32 tipo = login.getIdTipo();
                        Thread hiloInterfaz;
                        switch (tipo)
                        {
                            case 1:
                                hiloInterfaz = new System.Threading.Thread(new System.Threading.ThreadStart(abrirAdministrador));
                                this.Close();
                                hiloInterfaz.SetApartmentState(System.Threading.ApartmentState.STA);
                                hiloInterfaz.Start();
                                break;
                            case 2:
                                hiloInterfaz = new System.Threading.Thread(new System.Threading.ThreadStart(abrirGerente));
                                this.Close();
                                hiloInterfaz.SetApartmentState(System.Threading.ApartmentState.STA);
                                hiloInterfaz.Start();
                                break;
                            case 3:
                                hiloInterfaz = new System.Threading.Thread(new System.Threading.ThreadStart(abrirAsesor));
                                this.Close();
                                hiloInterfaz.SetApartmentState(System.Threading.ApartmentState.STA);
                                hiloInterfaz.Start();
                                break;
                            case 4:
                                hiloInterfaz = new System.Threading.Thread(new System.Threading.ThreadStart(abrirCajero));
                                this.Close();
                                hiloInterfaz.SetApartmentState(System.Threading.ApartmentState.STA);
                                hiloInterfaz.Start();
                                break;
                            case 5:
                                hiloInterfaz = new System.Threading.Thread(new System.Threading.ThreadStart(abrirCliente));
                                this.Close();
                                hiloInterfaz.SetApartmentState(System.Threading.ApartmentState.STA);
                                hiloInterfaz.Start();
                                break;
                            default:
                                MessageBox.Show("El usuario y la contraseña son incorrectos");
                                limpiarCampos();
                                break;
                        }
                    }
                }
                else
                    MessageBox.Show("Ingrese por favor clave");
            }
            else
                MessageBox.Show("Ingrese por fovor su nombre de usuario");



        }

        public void limpiarCampos()
        {
            tBClave.Text = "";
            tBusuario.Text = "";
        }

        public void abrirAdministrador()
        {
            FormAdministrador menu = new FormAdministrador();
            menu.ShowDialog();
        }

        public void abrirGerente()
        {
            FormGerente menu = new FormGerente();
            menu.ShowDialog();
        }
        public void abrirAsesor()
        {
            FormAsesor menu = new FormAsesor();
            menu.ShowDialog();
        }

        public void abrirCajero()
        {
            FormCajero menu = new FormCajero();
            menu.ShowDialog();
        }

        public void abrirCliente()
        {
            FormCliente menu = new FormCliente();
            menu.ShowDialog();
        }

        public void abrirGestionCliente()
        {
            FormGestionCliente menu = new FormGestionCliente();
            menu.ShowDialog();
        }

        private void tBClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            abrirGestionCliente();
        }

    }
}
