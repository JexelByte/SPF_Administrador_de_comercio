using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

namespace SPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new Size(285, 185);
            panel2.Enabled = false;

            if (!Directory.Exists("DB"))
            {
                Directory.CreateDirectory("DB");
            }
        }

        string empleado = "Admin";

        private void button5_Click(object sender, EventArgs e)
        {
            if (!File.Exists("DB/DATABASE.jex"))
            {
                if (MessageBox.Show("No se encontraron datos, Desea crear un Nuevo Usuario Administrador?", "No se encontraron datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Admin ad = new Admin();
                    ad.usuario = textBox2.Text;
                    ad.contraseña = Contraseña.Text;

                    string a = JsonConvert.SerializeObject(ad);
                    File.WriteAllText("DB/DATABASE.jex", a);

                    MessageBox.Show("su usuario se ha guardado, debe volver a introducir los datos para entrar.", "Operacion exitosa");
                }
                Contraseña.Text = "";
                textBox2.Text = "";
            }
            else
            {
                Admin a = JsonConvert.DeserializeObject<Admin>(File.ReadAllText("DB/DATABASE.jex"));

                if (a.usuario == textBox2.Text && a.contraseña == Contraseña.Text)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.Size = new Size(820, 510);
                    this.MinimumSize = new Size(800, 510);
                    panel2.Visible = true;
                    panel2.Enabled = true;

                    return;
                }
                else if (File.Exists("DB/TR.jex"))
                {
                    List<Trabajador> listr = JsonConvert.DeserializeObject<List<Trabajador>>(File.ReadAllText("DB/TR.jex"));

                    foreach  (Trabajador  t in listr)
                    {
                        if (t.usuario == textBox2.Text && t.contraseña == Contraseña.Text)
                        {
                            this.FormBorderStyle = FormBorderStyle.Sizable;
                            this.Size = new Size(800, 480);
                            panel2.Visible = true;
                            panel2.Enabled = true;

                            button6.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button13.Enabled = false;
                            button9.Enabled = false;
                            button20.Enabled = false;

                            empleado = t.Cedula;

                            return;
                        }
                    }
                }

                MessageBox.Show("Datos invalidos", "Error");

            }
        }
        
        private void cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 57 && e.KeyChar >= 48) || e.KeyChar == 13 || e.KeyChar == 8 || e.KeyChar == ',')
            {
            }
            else
            {
                e.Handled = true;
            }
        }


        //                   ADD Empleados                       ****************************************************

        private void button6_Click(object sender, EventArgs e)
        {
            Empleados.Visible = true;
            Empleados.Dock = DockStyle.Fill;
            Empleados.BringToFront();

            List<Trabajador> listr;

            if (File.Exists("DB/TR.jex"))
            {
                listr = JsonConvert.DeserializeObject<List<Trabajador>>(File.ReadAllText("DB/TR.jex"));

                listBox1.Items.Clear();

                foreach (Trabajador t in listr)
                {
                    listBox1.Items.Add(t.Nombre_Completo + " - " + t.Cedula + " - " + t.Departamento + " - " + t.Cargo);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nombre.Text == "")
            {
                MessageBox.Show("Debe tener un nombre, Error");
                return;
            }
            if (cedula.Text == "")
            {
                MessageBox.Show("Debe tener una cedula valida, Error");
                return;
            }
            if (Departamento.Text == "")
            {
                MessageBox.Show("Debe tener un departamento, Error");
                return;
            }
            if (cargo.Text == "")
            {
                MessageBox.Show("Debe tener un cargo, Error");
                return;
            }
            if (usuario.Text == "")
            {
                MessageBox.Show("Debe tener un usuario, Error");
                return;
            }
            if (key.Text == "" || keyRepeat.Text == "")
            {
                MessageBox.Show("Debe tener una contraseña, Error");
                return;
            }
            if (key.Text != keyRepeat.Text)
            {
                MessageBox.Show("Las contraseñas deben coincidir, Error");
                return;
            }

            Trabajador tr = new Trabajador();

            tr.Nombre_Completo = nombre.Text;
            tr.Cedula = cedula.Text;
            tr.Departamento = Departamento.Text;
            tr.Cargo = cargo.Text;
            tr.usuario = usuario.Text;
            tr.contraseña = key.Text;

            List<Trabajador> listr;

            if (!File.Exists("DB/TR.jex"))
            {
                listr = new List<Trabajador>();

            }
            else
            {
                listr = JsonConvert.DeserializeObject<List<Trabajador>>(File.ReadAllText("DB/TR.jex"));
            }
            
            tr.fechaDeIngreso = DateTime.Now;

            listr.Add(tr);

            try
            {
                File.WriteAllText("DB/TR.jex", JsonConvert.SerializeObject(listr));

            }
            catch (Exception)
            {
                MessageBox.Show("Errores al guardar los cambios","Error");
                return;
            }

            MessageBox.Show("Se ah guardado correctamente","Operacion Exitosa");

            listBox1.Items.Clear();

            nombre.Text = "";
            cedula.Text = "";
            Departamento.Text = "";
            cargo.Text = "";
            usuario.Text = "";
            key.Text = "";
            keyRepeat.Text = "";

            foreach (Trabajador t in listr)
            {
                listBox1.Items.Add(t.Nombre_Completo + " - " + t.Cedula + " - " + t.Departamento + " - " + t.Cargo);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<Trabajador> listr;

            if (File.Exists("DB/TR.jex"))
            {
                listr = JsonConvert.DeserializeObject<List<Trabajador>>(File.ReadAllText("DB/TR.jex"));

                foreach (Trabajador t in listr)
                {
                    if (listBox1.Text == t.Nombre_Completo + " - " + t.Cedula + " - " + t.Departamento + " - " + t.Cargo)
                    {
                        listr.Remove(t);
                        break;
                    }
                }

                try
                {
                    File.WriteAllText("DB/TR.jex", JsonConvert.SerializeObject(listr));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                listBox1.Items.Clear();

                foreach (Trabajador t in listr)
                {
                    listBox1.Items.Add(t.Nombre_Completo + " - " + t.Cedula + " - " + t.Departamento + " - " + t.Cargo);
                }
            }
        }

        //                  ADD Productos                          *********************************************************

        private void button9_Click(object sender, EventArgs e)
        {
            Agregar_Producto.Visible = true;
            Agregar_Producto.Dock = DockStyle.Fill;
            Agregar_Producto.BringToFront();

            if (File.Exists("DB/PD.jex"))
            {
                lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                lista_p.Items.Clear();

                foreach (Producto t in lisp)
                {
                    lista_p.Items.Add(t.Nombre + " - " + t.Marca);
                }
            }
        }


        List<Producto> lisp;

        private void button7_Click(object sender, EventArgs e)
        {
            if (Nombre_p.Text != "" && marca_p.Text != "" && pvp.Text != "" && pcp.Text != "")
            {
                Producto p = new Producto();

                p.Nombre = Nombre_p.Text;
                p.Marca = marca_p.Text;

                p.Precio_venta = Convert.ToDouble(pvp.Text);
                p.Precio_Compra = Convert.ToDouble( pcp.Text );

                p.cantidad = 0;
                
                if (File.Exists("DB/PD.jex"))
                {
                    lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));
                    
                }
                else
                {
                    lisp = new List<Producto>();
                }
                
                if (lisp.Contains(p))
                {
                    MessageBox.Show("Este producto ya existe", "Error");
                    return;
                }

                lisp.Add(p);

                try
                {
                    File.WriteAllText("DB/PD.jex", JsonConvert.SerializeObject(lisp));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                Nombre_p.Text = "";
                marca_p.Text = "";
                pvp.Text = "";
                pcp.Text = "";

                lista_p.Items.Clear();

                foreach (Producto t in lisp)
                {
                    lista_p.Items.Add(t.Nombre + " - " + t.Marca);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<Producto> lisp;

            if (File.Exists("DB/PD.jex"))
            {
                lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                foreach  (Producto  p in lisp)
                {
                    if (lista_p.Text == p.Nombre + " - " + p.Marca)
                    {
                        lisp.Remove(p);
                        break;
                    }
                }

                try
                {
                    File.WriteAllText("DB/PD.jex", JsonConvert.SerializeObject(lisp));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                lista_p.Items.Clear();

                foreach (Producto t in lisp)
                {
                    lista_p.Items.Add(t.Nombre + " - " + t.Marca);
                }
            }
        }

        private void lista_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Producto p in lisp)
            {
                if ((p.Nombre + " - " + p.Marca) == lista_p.Text)
                {
                    richTextBox3.Text = "Nombre: " + p.Nombre + "\nMarca: " + p.Marca + "\nPrecio de compra: " + p.Precio_Compra + "\nPrecio de venta: " + p.Precio_venta + "\nCantidad: " + p.cantidad;
                }
            }
        }

        //                  Distribuidores!                        **********************************************************

        private void button3_Click(object sender, EventArgs e)
        {
            Distribuidores.Visible = true;
            Distribuidores.Dock = DockStyle.Fill;
            Distribuidores.BringToFront();

            List<Distribuidores> lisd;

            if (File.Exists("DB/Dsp.jex"))
            {
                lisd = JsonConvert.DeserializeObject<List<Distribuidores>>(File.ReadAllText("DB/Dsp.jex"));

                lista_D.Items.Clear();

                foreach (Distribuidores t in lisd)
                {
                    lista_D.Items.Add(t.Nombre + " - " + t.rif);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Nombre_D.Text != "" && Telefono_D.Text != "" && Direccion_D.Text != "" && Rif_D.Text != "")
            {
                Distribuidores d = new Distribuidores();

                d.Nombre = Nombre_D.Text;
                d.telefono = Telefono_D.Text;
                d.Direccion = Direccion_D.Text;
                d.rif = Rif_D.Text;
            
                List<Distribuidores> lisd;

                if (File.Exists("DB/Dsp.jex"))
                {
                    lisd = JsonConvert.DeserializeObject<List<Distribuidores>>(File.ReadAllText("DB/Dsp.jex"));

                }
                else
                {
                    lisd = new List<Distribuidores>();
                }
                
                if (lisd.Contains(d))
                {
                    MessageBox.Show("Este producto ya existe", "Error");
                    return;
                }

                lisd.Add(d);

                try
                {
                    File.WriteAllText("DB/Dsp.jex", JsonConvert.SerializeObject(lisd));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                Nombre_D.Text = "";
                Telefono_D.Text = "";
                Direccion_D.Text = "";
                Rif_D.Text = "";

                lista_D.Items.Clear();

                foreach (Distribuidores t in lisd)
                {
                    lista_D.Items.Add(t.Nombre + " - " + t.rif);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está seguro que desea eliminar a " + lista_D.Text, "¡Advertencia!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<Distribuidores> lisd;

                if (File.Exists("DB/Dsp.jex"))
                {
                    lisd = JsonConvert.DeserializeObject<List<Distribuidores>>(File.ReadAllText("DB/Dsp.jex"));

                    foreach (Distribuidores d in lisd)
                    {
                        if (lista_D.Text == d.Nombre + " - " + d.rif)
                        {
                            lisd.Remove(d);
                            break;
                        }
                    }

                    try
                    {
                        File.WriteAllText("DB/Dsp.jex", JsonConvert.SerializeObject(lisd));

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Errores al guardar los cambios", "Error");
                        return;
                    }

                    MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                    lista_D.Items.Clear();

                    foreach (Distribuidores t in lisd)
                    {
                        lista_D.Items.Add(t.Nombre + " - " + t.rif);
                    }
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        //                     Comprar                   *************************************************************************

        List<string> BusP = new List<string>();

        List<Producto> lisProduct;

        List<compra1> compra = new List<compra1>();

        double monto;

        private void button2_Click(object sender, EventArgs e)
        {
            Compras.Visible = true;
            Compras.Dock = DockStyle.Fill;
            Compras.BringToFront();

            List<Distribuidores> lisd;

            lisProduct = new List<Producto>();
            lisProduct.Clear();


            if (File.Exists("DB/Dsp.jex"))
            {
                lisd = JsonConvert.DeserializeObject<List<Distribuidores>>(File.ReadAllText("DB/Dsp.jex"));

                Distribuidor.Items.Clear();

                foreach (Distribuidores d in lisd)
                {
                    Distribuidor.Items.Add(d.Nombre + " - " + d.rif);
                }
            }

            if (File.Exists("DB/PD.jex"))
            {
                lisProduct = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                Producto.Items.Clear();
                BusP.Clear();

                foreach (Producto p in lisProduct)
                {
                    Producto.Items.Add(p.Nombre + " - " + p.Marca);
                    BusP.Add(p.Nombre + " - " + p.Marca);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Producto.Items.Clear();

            foreach (string i in BusP)
            {
                if (i.Contains(Buscar_Produc.Text))
                {
                    Producto.Items.Add(i);
                }
            }
        }

        private void Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Producto p in lisProduct)
            {
                if (p.Nombre + " - " + p.Marca == Producto.Text)
                {
                    detalles.Text = "Nombre: " + p.Nombre + "\n Marca: " + p.Marca + "\n Precio de compra: " + p.Precio_Compra + "\n Precio de venta: " + p.Precio_venta;
                    return;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value >= 0)
            {
                foreach (Producto p in lisProduct)
                {
                    if (p.Nombre + " - " + p.Marca == Producto.Text)
                    {
                        compra1 cm = new compra1();
                        cm.producto = p;
                        cm.cantidad = Convert.ToInt32(numericUpDown1.Value);
                        
                        numericUpDown1.Value = 0;

                        foreach (compra1 i in compra)
                        {
                            if (i.producto == p)
                            {
                                compra.Remove(i);
                                break;
                            }
                        }

                        if (cm.cantidad > 0)
                            compra.Add(cm);
                        
                        break;
                    }
                }

                monto = 0;

                listBox2.Items.Clear();

                foreach (compra1 c in compra)
                {
                    monto += c.cantidad * c.producto.Precio_Compra;
                    listBox2.Items.Add(c.producto.Nombre + " - " + c.producto.Marca + " - " + c.cantidad);
                }

                MontoTotalCompra.Text = "Monto Total: " + monto;
            }
            else
            {
                MessageBox.Show("Debe asignar una cantidad","Error");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count <= 0)
            {
                return;
            }

            Factura_Compra Fc = new Factura_Compra();

            Fc.fecha = DateTime.Now;
            Fc.productos = compra;
            Fc.total = monto;

            List<Factura_Compra> lisf = new List<Factura_Compra>();

            if (MessageBox.Show("¿Seguro que quiere realizar esta compra? \n Le aconsejamos revisar todos los datos antes de proceder.","¡Advertencia!" ,MessageBoxButtons.YesNo ) == DialogResult.Yes)
            {
                if (File.Exists("DB/FC.jex"))
                {
                    lisf = JsonConvert.DeserializeObject<List<Factura_Compra>>(File.ReadAllText("DB/FC.jex"));

                }
                else
                {
                    lisf = new List<Factura_Compra>();
                }


                lisf.Add(Fc);

                try
                {
                    foreach (compra1 i in Fc.productos)
                    {
                        foreach (Producto p in lisp)
                        {
                            if (i.producto.Nombre + i.producto.Marca == p.Nombre + p.Marca)
                            {
                                p.cantidad += i.cantidad;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                    foreach (compra1 i in Fc.productos)
                    {
                        foreach (Producto p in lisp)
                        {
                            if (i.producto.Nombre + i.producto.Marca == p.Nombre + p.Marca)
                            {
                                p.cantidad += i.cantidad;
                                break;
                            }
                        }
                    }
                }

                try
                {
                    File.WriteAllText("DB/PD.jex", JsonConvert.SerializeObject(lisp));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }
                
                try
                {
                    File.WriteAllText("DB/FC.jex", JsonConvert.SerializeObject(lisf));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                listBox2.Items.Clear();
                MontoTotalCompra.Text = "Monto Total: ";
                
                compra.Clear();

                monto = 0;
            }
        }

        //                  Facturas de Compra                   ***************************************************************

        List<Factura_Compra> lisf;

        private void button4_Click(object sender, EventArgs e)
        {
            Facturas_Compras.Visible = true;
            Facturas_Compras.Dock = DockStyle.Fill;
            Facturas_Compras.BringToFront();

            lisf = new List<Factura_Compra>();
            
            Lista_Fc_Cp.Items.Clear();


            if (File.Exists("DB/FC.jex"))
            {
                lisf = JsonConvert.DeserializeObject<List<Factura_Compra>>(File.ReadAllText("DB/FC.jex"));
                
                foreach (Factura_Compra fc in lisf)
                {
                    Lista_Fc_Cp.Items.Add(fc.total + " - " + fc.fecha);
                }
            }
        }

        private void Lista_Fc_Cp_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Factura_Compra fc in lisf)
            {
                if (fc.total + " - " + fc.fecha == Lista_Fc_Cp.Text)
                {
                    Detalles_FC_CP.Text = "Monto total: " + fc.total;

                    Detalles_FC_CP.Text += "\nFecha: " + fc.fecha;

                    foreach (compra1 p in fc.productos)
                    {
                        Detalles_FC_CP.Text += "\nCantidad: "+ p.cantidad + " - " + p.producto.Nombre + " - " + p.producto.Marca;
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            Lista_Fc_Cp.Items.Clear();

            foreach  (Factura_Compra fc in lisf)
            {
                if (fc.fecha.ToString().Contains(textBox3.Text) || fc.total.ToString().Contains(textBox3.Text))
                {
                    Lista_Fc_Cp.Items.Add(fc.total + " - " + fc.fecha);
                }
            }
        }

        //                       Vender                            **************************************************************
        

        List<compra1> Cp = new List<compra1>();
        
        private void button14_Click(object sender, EventArgs e)
        {
            vender.Visible = true;
            vender.Dock = DockStyle.Fill;
            vender.BringToFront();
            

            if (File.Exists("DB/PD.jex"))
            {
                lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                listBox3.Items.Clear();

                foreach (Producto item in lisp)
                {
                    listBox3.Items.Add(item.Nombre + " - " + item.Marca);
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            foreach (Producto t in lisp)
            {
                if (t.Nombre.Contains(textBox4.Text) || t.Marca.Contains(textBox4.Text))
                {
                    listBox3.Items.Add(t.Nombre + " - " + t.Marca);
                }
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Producto t in lisp)
            {
                if (t.Nombre + " - " + t.Marca == listBox3.Text)
                {
                    richTextBox1.Text = "Nombre: " + t.Nombre + "\n Marca: " + t.Marca + "\n Precio de Venta: " + t.Precio_venta + "\n Disponibilidad: " + t.cantidad;
                    break;
                }
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            compra1 c = new compra1();

            foreach (Producto t in lisp)
            {
                if (t.Nombre + " - " + t.Marca == listBox3.Text)
                {
                    c.producto = t;
                    c.cantidad = Convert.ToInt32(numericUpDown2.Value);

                    foreach (compra1 i in Cp)
                    {
                        if (i.producto == t)
                        {
                            Cp.Remove(i);
                            break;
                        }
                    }

                    if(c.cantidad > 0)
                    Cp.Add(c);
                    
                    break;
                }
            }

            listBox4.Items.Clear();

            foreach (compra1 t in Cp)
            {
                listBox4.Items.Add(t.producto.Nombre + " - " + t.producto.Marca + " - Cantidad: " + t.cantidad);
            }

            monto = 0;
            
            foreach (compra1 m in Cp)
            {
                monto += m.cantidad * m.producto.Precio_venta;
            }

            label21.Text = "Monto Total:" + monto;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Factura fc = new Factura();

            fc.Fecha = DateTime.Now;
            fc.productos = Cp;
            fc.Total = monto;
            fc.trabajador = empleado;
            fc.cliente = cliente.Text;

            List<Factura> fac;
            

            if (File.Exists("DB/FV.jex"))
            {
                fac = JsonConvert.DeserializeObject<List<Factura>>(File.ReadAllText("DB/FV.jex"));
            }
            else
            {
                fac = new List<Factura>();
            }

            fac.Add(fc);

            try
            {
                foreach (compra1 i in fc.productos)
                {
                    foreach (Producto p in lisp)
                    {
                        if (i.producto.Nombre + i.producto.Marca == p.Nombre + p.Marca)
                        {
                            p.cantidad -= i.cantidad;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                lisp = JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText("DB/PD.jex"));

                foreach (compra1 i in fc.productos)
                {
                    foreach (Producto p in lisp)
                    {
                        if (i.producto.Nombre + i.producto.Marca == p.Nombre + p.Marca)
                        {
                            p.cantidad -= i.cantidad;
                            break;
                        }
                    }
                }
            }

            try
            {
                File.WriteAllText("DB/PD.jex", JsonConvert.SerializeObject(lisp));

            }
            catch (Exception)
            {
                MessageBox.Show("Errores al guardar los cambios", "Error");
                return;
            }

            if (MessageBox.Show("Asegurese de que todos los datos son correctos antes de proceder. \n ¿Proceder?","¡Alerta!",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    File.WriteAllText("DB/FV.jex", JsonConvert.SerializeObject(fac));

                }
                catch (Exception)
                {
                    MessageBox.Show("Errores al guardar los cambios", "Error");
                    return;
                }

                MessageBox.Show("Se ah guardado correctamente", "Operacion Exitosa");

                Cp.Clear();
                listBox4.Items.Clear();
                cliente.Text = "";
            }
        }


        //                        Facturas Ventas                           ***********************************************************************************

        List<Factura> Fcv;

        private void button4_Click_1(object sender, EventArgs e)
        {
            Facturas_Ventas.Visible = true;
            Facturas_Ventas.Dock = DockStyle.Fill;
            Facturas_Ventas.BringToFront();


            if (File.Exists("DB/FV.jex"))
            {
                Fcv = JsonConvert.DeserializeObject<List<Factura>>(File.ReadAllText("DB/FV.jex"));

                listBox6.Items.Clear();

                foreach (Factura item in Fcv)
                {
                    listBox6.Items.Add(item.Total + " - " + item.Fecha + " - " + item.cliente);
                }
            }
            else
            {
                Fcv = new List<Factura>();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            listBox6.Items.Clear();

            foreach (Factura t in Fcv)
            {
                if (t.Total.ToString().Contains(textBox5.Text) || t.Fecha.ToString().Contains(textBox5.Text) || t.cliente.Contains(textBox5.Text))
                {
                    listBox6.Items.Add(t.Total + " - " + t.Fecha + " - " + t.cliente);
                }
            }
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Factura t in Fcv)
            {
                if (t.Total + " - " + t.Fecha + " - " + t.cliente == listBox6.Text)
                {
                    richTextBox2.Text = "Empleado: " + t.trabajador + "\nTotal: " + t.Total + "\nFecha: " + t.Fecha + "\nCliente: " + t.cliente + "\n\n";

                    foreach (compra1 p in t.productos)
                    {
                        richTextBox2.Text += p.producto.Nombre + " - " + p.producto.Marca + " - Cantidad: " + p.cantidad + "\n";
                    }


                    break;
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Configuracion.Dock = DockStyle.Fill;
            Configuracion.BringToFront();
            Configuracion.Visible = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Admin a = JsonConvert.DeserializeObject<Admin>(File.ReadAllText("DB/DATABASE.jex"));
            
            if (a.contraseña == Interaction.InputBox("Esta accion requiere la contraseña del administrador"))
            {
                respaldo r = new respaldo();
                r.resp = new List<string>();

                r.resp.Add(File.ReadAllText("DB/DATABASE.jex"));
                r.resp.Add(File.ReadAllText("DB/Dsp.jex"));
                r.resp.Add(File.ReadAllText("DB/FC.jex"));
                r.resp.Add(File.ReadAllText("DB/FV.jex"));
                r.resp.Add(File.ReadAllText("DB/PD.jex"));
                r.resp.Add(File.ReadAllText("DB/TR.jex"));

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(r));
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                respaldo crg = JsonConvert.DeserializeObject<respaldo>(File.ReadAllText(openFileDialog1.FileName));

                Admin ad = JsonConvert.DeserializeObject<Admin>(crg.resp[0]);

                if (ad.contraseña == Interaction.InputBox("Se requiere la contraseña del administrador de este respaldo"))
                {
                    Admin a = JsonConvert.DeserializeObject<Admin>(File.ReadAllText("DB/DATABASE.jex"));

                    if (a.contraseña == Interaction.InputBox("Esta accion requiere la contraseña del administrador"))
                    {
                        if (MessageBox.Show("¿Desea reemplazar su base de datos actual por esta otra?", "!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            File.WriteAllText("DB/Dsp.jex", crg.resp[1]);
                            File.WriteAllText("DB/FC.jex", crg.resp[2]);
                            File.WriteAllText("DB/FV.jex", crg.resp[3]);
                            File.WriteAllText("DB/PD.jex", crg.resp[4]);
                            File.WriteAllText("DB/TR.jex", crg.resp[5]);
                        }
                        else if (MessageBox.Show("¿Sesea combinar esta base de datos con su base de datos actual?", "!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                        }
                    }
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Admin ad = JsonConvert.DeserializeObject<Admin>(File.ReadAllText("DB/DATABASE.jex"));

            if (ad.contraseña == Interaction.InputBox("Se requiere la contraseña actual del administrador"))
            {
                string nk = Interaction.InputBox("Escriba la nueva contaseña");

                if (nk != "")
                {
                    if (nk == Interaction.InputBox("Repita la nueva contaseña"))
                    {
                        ad.contraseña = nk;

                        File.WriteAllText("DB/DATABASE.jex", JsonConvert.SerializeObject(ad));

                        MessageBox.Show("Operacion realizada con exito, se reiniciará la aplicacion!", "!");

                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas proporcionadas no coinciden", "!");
                    }
                }
            }
        }
    }
}
