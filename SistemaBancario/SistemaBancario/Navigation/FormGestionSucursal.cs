﻿using Platform.Comunication.controller;
using Platform.Object.model;
using SistemaBancario.Resources;
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

namespace SistemaBancario.Navigation
{
    public partial class FormGestionSucursal : Form
    {
        DepartamentoController deptoController = new DepartamentoController();
        CiudadController ciudadController = new CiudadController();
        EmpleadoController empleadoController = new EmpleadoController();
        BancoController banco = new BancoController();
        SucursalController sucursalrControlador = new SucursalController();
        PaisController paisControlador = new PaisController();
        int aux;
        public FormGestionSucursal()
        {
            InitializeComponent();
            cargarComboGerente();
        }


        private void FormGestionSucursal_Load(object sender, EventArgs e)
        {
            cargarTabla();
            cargarPais();
        }

        public void cargarTabla()
        {
            this.listarSucursalTableAdapter.Fill(this.dataSetBanco.listarSucursal);
        }

        public void cargarPais()
        {
            this.paisTableAdapter.Fill(this.dataSetBanco.pais);
        }


        private void cBPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBPais.SelectedValue != null)
            {
                Int32 idPais = (int)cBPais.SelectedValue;
                LinkedList<Departamento> lista = deptoController.solicitudListaDeptosPorPais(idPais);
                BindingList<Item> deptos = new BindingList<Item>();
                for (int i = 0; i < lista.Count; i++)
                {
                    deptos.Add(new Item((lista.ElementAt(i)).getNombre(), (lista.ElementAt(i)).getId()));

                }

                cBDepartamento.DisplayMember = "Name";
                cBDepartamento.ValueMember = "Value";
                cBDepartamento.DataSource = deptos;
            }




        }

        private void cBDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cBDepartamento.SelectedItem;
            if (itm != null)
            {
                Int32 idPais = (int)cBPais.SelectedValue;
                LinkedList<Ciudad> lista = ciudadController.solicitudListaCiudadPorDeptos(itm.Value);
                BindingList<Item> ciudad = new BindingList<Item>();
                for (int i = 0; i < lista.Count; i++)
                {
                    ciudad.Add(new Item((lista.ElementAt(i)).getNombre(), (lista.ElementAt(i)).getId()));

                }

                cBCiudad.DisplayMember = "Name";
                cBCiudad.ValueMember = "Value";
                cBCiudad.DataSource = ciudad;
            }


        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Int32 ciudadId;
            if (tBNombre.Text != "")
            {
                if ((cBCiudad.SelectedItem) != null)
                {
                    if (tBDireccion.Text != "")
                    {
                        String nombre = tBNombre.Text.ToUpper();
                        Int32 bancoId = (banco.solicitudObtenerBanco()).getId();
                        ciudadId = ((Item)cBCiudad.SelectedItem).Value;
                        Int32 gerenteId = ((Item)cBGerente.SelectedItem).Value;
                        String direccion = tBDireccion.Text;


                        if (sucursalrControlador.solicitudGuardar(-1, nombre, bancoId, ciudadId, gerenteId, direccion))
                        {

                            MessageBox.Show("Exito al guardar");
                            deshabilitarCampos();
                            cargarTabla();
                        }
                        else
                            MessageBox.Show("Ocurrió un error al guardar");
                        deshabilitarCampos();
                    }
                    else
                        MessageBox.Show("Por favor ingresa la dirección");
                }
                else
                    MessageBox.Show("Por favor selecciona la ciudad");
            }
            else
                MessageBox.Show("Por favor ingresa el nombre de la sucursal");





        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            String nombre = tBNombre.Text.ToUpper();
            Int32 bancoId = (banco.solicitudObtenerBanco()).getId();
            Int32 ciudadId = ((Item)cBCiudad.SelectedItem).Value;
            Int32 gerenteId = ((Item)cBGerente.SelectedItem).Value;
            String direccion = tBDireccion.Text;

            if (sucursalrControlador.solicitudModificar(aux, nombre, bancoId, ciudadId, gerenteId, direccion))
            {
                MessageBox.Show("La sucursal se modificó correctamente");
                deshabilitarCampos();
                cargarTabla();
                aux = 0;
            }
            else
                MessageBox.Show("ocurrió un error al modificar");
            deshabilitarCampos();



        }


        public void cargarComboGerente()
        {
            LinkedList<Empleado> lista = empleadoController.solicitudObtenerGerente();
            BindingList<Item> gerentes = new BindingList<Item>();
            for (int i = 0; i < lista.Count; i++)
            {
                gerentes.Add(new Item((lista.ElementAt(i)).getNombre(), (lista.ElementAt(i)).getId()));

            }

            cBGerente.DisplayMember = "Name";
            cBGerente.ValueMember = "Value";
            cBGerente.DataSource = gerentes;
        }



        private void tBNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                String nombre = tBNombre.Text;
                if (nombre.Length != 0)
                {
                    Sucursal s = sucursalrControlador.solicitudBuscar(nombre);
                    if (s != null)
                    {

                        cBPais.SelectedValue = s.idPais;
                        cBDepartamento.SelectedValue = s.idDepartamento;
                        cBCiudad.SelectedValue = s.getCiudadId();
                        cBGerente.SelectedValue = s.getGerenteId();
                        tBDireccion.Text = s.getDireccion();
                        aux = s.getId();
                        habilitarCampos();
                        btnGuardar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnEliminar.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe, por favor adicionalo");
                        habilitarCampos();
                        btnGuardar.Enabled = true;
                        btnEditar.Enabled = false;
                        btnEliminar.Enabled = false;

                    }


                }
            }
        }

        public void habilitarCampos()
        {
            cBCiudad.Enabled = true;
            cBDepartamento.Enabled = true;
            cBPais.Enabled = true;
            cBGerente.Enabled = true;
            tBDireccion.ReadOnly = false;
            tBNombre.ReadOnly = true;
        }

        public void deshabilitarCampos()
        {
            cBCiudad.Enabled = false;
            cBDepartamento.Enabled = false;
            cBPais.Enabled = false;
            cBGerente.Enabled = false;
            tBDireccion.ReadOnly = true;
            tBNombre.ReadOnly = false;
            limpiarCampos();
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        public void limpiarCampos()
        {
            tBNombre.Text = "";
            tBDireccion.Text = "";
            cBPais.SelectedValue = 1;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String info = sucursalrControlador.solicutudEliminar(aux);
            MessageBox.Show(info);
            deshabilitarCampos();
            aux = 0;
            cargarTabla();
        }
    }
}
