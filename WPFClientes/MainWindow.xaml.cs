using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BiblioClase;

namespace WPFClientes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (EstadoCivil i in Enum.GetValues(typeof(EstadoCivil)))
            {
                cmbEstCiv.Items.Add(i);
            }
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (ColeccionClientes.BuscarRut(txtRut.Text) == null) 
            {
                try
                {
                    Cliente c = new Cliente();
                    c.Rut = txtRut.Text;
                    c.Nombre = txtNombre.Text;
                    c.Apellido = txtApellido.Text;

                    c.EstCiv = (EstadoCivil)cmbEstCiv.SelectedIndex;
                    if (rbtFem.IsChecked == true)
                    {
                        rbtMasc.IsChecked = false;
                        rbtOtro.IsChecked = false;
                        c.Sexo = Sexo.Femenino;
                    } //Radiobutton Femenino
                    if (rbtMasc.IsChecked == true)
                    {
                        rbtFem.IsChecked = false;
                        rbtOtro.IsChecked = false;
                        c.Sexo = Sexo.Masculino;
                    } //Radiobutton Masculino
                    if (rbtOtro.IsChecked == true)
                    {
                        rbtFem.IsChecked = false;
                        rbtMasc.IsChecked = false;
                        c.Sexo = Sexo.Otro;
                    } //Radiobutton Otro
                    if (rbtFem.IsChecked == false && rbtMasc.IsChecked == false && rbtOtro.IsChecked == false)
                    {
                        c.Sexo = Sexo.nulo;
                    }//Si hay radiobutton sin selección, se marca Nulo como Sexo y lanza error.
                    c.FechaNacimiento = datNacto.SelectedDate.Value.Date;
                    if (c.Validacion())
                    {
                        ColeccionClientes.Agregar(c);
                        dgClientes.ItemsSource = ColeccionClientes.ListarTodos();
                        dgClientes.Items.Refresh();
                        MessageBox.Show("Agregado con exito");
                    }//Si están todos los datos, se añade al nuevo Cliente, sino, se envía un mensaje con los datos faltantes.
                    else
                    {
                        MessageBox.Show("Error al agregar");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR " + ex.Message);
                }
            }//Si el rut no está ingresado, se procede a leer los campos para añadir al nuevo Cliente
            else
            {
                MessageBox.Show("ERROR\nEl rut ya fue ingresado");
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = new Cliente();
            if (txtRut.Text.Length == 0)
            {
                MessageBox.Show("Escriba un Rut");
            }
            else
            {
                try
                {
                    if (ColeccionClientes.BuscarRut(txtRut.Text) != null)
                    {
                        c = ColeccionClientes.BuscarRut(txtRut.Text);
                        txtNombre.Text = c.Nombre;
                        txtApellido.Text = c.Apellido;
                        datNacto.SelectedDate = c.FechaNacimiento;
                        cmbEstCiv.Text = c.EstCiv.ToString();
                        //Lee el contenido de propiedad Sexo del objeto y en función de ello marca un radiobutton
                        if (c.Sexo == Sexo.Otro)
                        {
                            rbtOtro.IsChecked = true;
                        }
                        if (c.Sexo == Sexo.Masculino)
                        {
                            rbtMasc.IsChecked = true;
                        }
                        if (c.Sexo == Sexo.Femenino)
                        {
                            rbtFem.IsChecked = true;
                        }
                    }
                    else
                    {

                        MessageBox.Show("Rut no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtRut.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            datNacto.DataContext = null;
            cmbEstCiv.Text = EstadoCivil.Seleccione.ToString();
            rbtFem.IsChecked = false;
            rbtMasc.IsChecked = false;
            rbtOtro.IsChecked = false;
        }
        private void btnEncontrarInicial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmpiezaPor.Text.Length != 0)
                {
                    dgClientes.ItemsSource = ColeccionClientes.NombreComienzaPor(txtEmpiezaPor.Text);
                    dgClientes.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Error, debe escribir al menos un caracter");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        } // OFICIAL 
        private void txtEmpiezaPor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtEmpiezaPor.Text.Length != 0)
                {
                    dgClientes.ItemsSource = ColeccionClientes.NombreComienzaPor(txtEmpiezaPor.Text);
                    dgClientes.Items.Refresh();
                }
                else
                {
                    dgClientes.ItemsSource = ColeccionClientes.ListarTodos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.Rut = txtRut.Text;
                c.Nombre = txtNombre.Text;
                c.Apellido = txtApellido.Text;

                c.EstCiv = (EstadoCivil)cmbEstCiv.SelectedIndex;
                if (rbtFem.IsChecked == true)
                {
                    rbtMasc.IsChecked = false;
                    rbtOtro.IsChecked = false;
                    c.Sexo = Sexo.Femenino;
                } //Radiobutton Femenino
                if (rbtMasc.IsChecked == true)
                {
                    rbtFem.IsChecked = false;
                    rbtOtro.IsChecked = false;
                    c.Sexo = Sexo.Masculino;
                } //Radiobutton Masculino
                if (rbtOtro.IsChecked == true)
                {
                    rbtFem.IsChecked = false;
                    rbtMasc.IsChecked = false;
                    c.Sexo = Sexo.Otro;
                } //Radiobutton Otro
                if (rbtFem.IsChecked == false && rbtMasc.IsChecked == false && rbtOtro.IsChecked == false)
                {
                    c.Sexo = Sexo.nulo;
                }//Si hay radiobutton sin selección, se marca Nulo como Sexo y lanza error.
                c.FechaNacimiento = datNacto.SelectedDate.Value.Date;

                    MessageBox.Show(ColeccionClientes.Modificar(c));
                    dgClientes.ItemsSource = ColeccionClientes.ListarTodos();
                    dgClientes.Items.Refresh();
                   
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.Message);
            }
                //Si el rut no está ingresado, se procede a leer los campos para añadir al nuevo Cliente
}
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ColeccionClientes.Borrar(txtRut.Text);
            dgClientes.ItemsSource = ColeccionClientes.ListarTodos();
            dgClientes.Items.Refresh();
        }
    }
}
