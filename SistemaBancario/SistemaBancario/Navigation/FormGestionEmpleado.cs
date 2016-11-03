﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Platform.Comunication.controller;
using Platform.Object.model;
namespace SistemaBancario.Navigation
{
    public partial class FormGestionEmpleado : Form
    {
        public FormGestionEmpleado()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String cedula = txtCedula.Text;
            String nombre = txtNombre.Text;
            String apellido = txtApellido.Text;
            txtPrueba.Text = dtpFechaNacimiento.Value.ToString("yyyy-MM-dd");
            String fecha = txtPrueba.Text;
            Int32 idCiudad = (Int32)cbCiudad.SelectedValue;
            Int32 idCargo = (Int32)cbCrgo.SelectedValue;
            String cuenta = txtCuenta.Text;
            String contraseña = txtContraseña.Text;
            Int32 idTipoUsu = (Int32)cbTipoUsuario.SelectedValue;

        }

        private void FormGestionEmpleado_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSetBanco.tipo_usuario' Puede moverla o quitarla según sea necesario.
            this.tipo_usuarioTableAdapter.Fill(this.dataSetBanco.tipo_usuario);
            // TODO: esta línea de código carga datos en la tabla 'dataSetBanco.usuario' Puede moverla o quitarla según sea necesario.
            this.usuarioTableAdapter.Fill(this.dataSetBanco.usuario);
            // TODO: esta línea de código carga datos en la tabla 'dataSetBanco.ciudad' Puede moverla o quitarla según sea necesario.
            this.ciudadTableAdapter.Fill(this.dataSetBanco.ciudad);

        }
    }
}
