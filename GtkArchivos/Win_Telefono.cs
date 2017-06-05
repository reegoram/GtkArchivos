using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Gtk;

namespace GtkArchivos
{
	public partial class Win_Telefono : Gtk.Window
	{
		
		// string ruta = @"C:\TELEFONO\telefono.dat";	/* Ruta Windows */
		string ruta = @"telefono.dat";	/* Ruta Linux(root) */

		BinaryReader br;
		
		op_Telefono Telefono;
		Gtk.ListStore DataTel;
		
		int id = 0; 
		string nombre = string.Empty;
		string marca = string.Empty;
		string modelo = string.Empty;
		string compania = string.Empty;

		public Win_Telefono () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			Telefono = new op_Telefono (ruta);
			Telefono.LeerDatos (ruta, entryID);
			DataTel = Telefono.GenerarTreeView (tvVerDatos, DataTel);

		}

		protected void OnBtnGuardarClicked (object sender, EventArgs e)
		{			
			try {				
				id = int.Parse (entryID.Text);
				nombre = entryNombre.Text;
				marca = entryMarca.Text;
				modelo = entryModelo.Text;
				compania = entryCompania.Text;

				
				string d = Telefono.InsertarDatos (ruta, id, nombre, marca, modelo, compania);
				if (d == "Guardado")
					MessageBox.Show ("Datos Guardados Exitosamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show ("Error de Guardado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

				string y = Telefono.LeerDatos(ruta, entryID);
				if (y == "Leido") {					
					DataTel.AppendValues(br.ReadInt32(), br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
					
					/* Agregando Datos al Modelo */
					DataTel.AppendValues (id.ToString (), nombre.ToString (), marca.ToString (), modelo.ToString (), compania.ToString ()); 
					
				}
				else{
					MessageBox.Show("No Leido");
				}


				ResetEntry();
				
			} catch (Exception ex) {
				ValidarEntry ();
				System.Diagnostics.Debug.WriteLine (ex.ToString());
			}


		}

		protected void OnBtnActualizarClicked (object sender, EventArgs e)
		{
			
		}

		protected void OnBtnEliminarClicked (object sender, EventArgs e)
		{
			
		}

		protected void OnBtnSalirClicked (object sender, EventArgs e)
		{
			Gtk.Application.Quit ();
		}

		protected void OnBtnSelecImagenClicked (object sender, EventArgs e)
		{
			SeleccionarImagen ();
		}



		/*** FUNCIONES ***/

		public void ValidarEntry(){
			if (entryID.Text == string.Empty || entryNombre.Text == string.Empty || entryMarca.Text == string.Empty 
				|| entryModelo.Text == string.Empty || entryCompania.Text == string.Empty) {
				MessageBox.Show("Es necesario rellenar todo el formulario", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			ResetEntry ();

		}

		public void ResetEntry(){
			entryID.Text = string.Empty;
			entryNombre.Text = string.Empty;
			entryMarca.Text = string.Empty;
			entryModelo.Text = string.Empty;
			entryCompania.Text = string.Empty;
		}

		public void SeleccionarImagen(){
			FileChooserDialog filechooser = new Gtk.FileChooserDialog ("Seleccionar imágen", 
				this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
			
			Telefono.Filtro (filechooser);



			if (filechooser.Run () == (int)ResponseType.Accept) {

				FileStream file = File.OpenRead (filechooser.Filename);
				this.imgVisual.Pixbuf = new Gdk.Pixbuf (file);
				imgVisual.Pixbuf.ScaleSimple (imgVisual.Pixbuf.Width, imgVisual.Pixbuf.Height, 0);
				file.Close ();

			}

			filechooser.Destroy ();


		}


		public void EliminarDatosTreeView(Gtk.TreeView treeV){
			
		}










		
	}
}

