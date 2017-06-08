using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GtkArchivos
{
	public partial class Win_Telefono : Gtk.Window
	{
		op_Telefono Telefono;
		Gtk.ListStore lsDataTel;
		List<op_Telefono> telefonos;

		public Win_Telefono() :
			base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			Telefono = new op_Telefono();
			telefonos = Telefono.LeerDatos();
			lsDataTel = Telefono.GenerarTreeView(tvVerDatos, lsDataTel);
			RevisarLista();
			//Mostrar_imagen(telefonos[0].id);
		}

		void RevisarLista()
		{
			if (telefonos != null)
			{
				// Llenar el dgv desde la lista
				for (int i = 0; i < telefonos.Count; i++)
				{
					//Agregas los valores al datagridview ** Ok/2
					lsDataTel.AppendValues(telefonos[i].id.ToString(), telefonos[i].nombre.ToString(), 
					                           telefonos[i].marca.ToString(), telefonos[i].modelo.ToString(), telefonos[i].compania.ToString());
				}
			}
		}

		//Cuando se dé clic en el datagridview 
		//TODO: Agregar el método manejador que escuche el clic en el datagridview
		void Mostrar_imagen(int id_en_lista)
		{
			if (telefonos != null)
			{
				// Llenar el dgv desde la lista
				for (int i = 0; i<telefonos.Count; i++)
				{
					imgVisual.Pixbuf = new Gdk.Pixbuf(Telefono.Imagen(telefonos[i].imagen_telefono), 150, 165);
				}
			}
		}

		protected void OnBtnGuardarClicked(object sender, EventArgs e)
		{
			try
			{

				Telefono.id = int.Parse(entryID.Text);
				Telefono.nombre = entryNombre.Text;
				Telefono.marca = entryMarca.Text;
				Telefono.modelo = entryModelo.Text;
				Telefono.compania = entryCompania.Text;

				string d = Telefono.InsertarDatos();
				if (d == "Guardado")
				{
					MessageBox.Show("Datos Guardados Exitosamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
					/* Agregando Datos al Modelo */
					lsDataTel.AppendValues(this.Telefono.id.ToString(), this.Telefono.nombre.ToString(), this.Telefono.marca.ToString(),
										   this.Telefono.modelo.ToString(), this.Telefono.compania.ToString());
				}
				else
				{
					MessageBox.Show("Error de Guardado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
				Telefono.ResetEntry(entryID, entryNombre, entryMarca, entryModelo, entryCompania);

			}
			catch (Exception ex)
			{
				Telefono.ValidarEntry(entryID, entryNombre, entryMarca, entryModelo, entryCompania);
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		protected void OnBtnActualizarClicked(object sender, EventArgs e)
		{

		}

		protected void OnBtnEliminarClicked(object sender, EventArgs e)
		{
			string m = Telefono.EliminarDatos();
			if (m == "Eliminado")
				MessageBox.Show("El Archivo ha sido Eliminado", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (m == "No Existe")
				MessageBox.Show("El Archivo No Existe", "No Esiste", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
				MessageBox.Show("ERROR: Archivo no eliminado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected void OnBtnSalirClicked(object sender, EventArgs e)
		{
			Gtk.Application.Quit();
		}

		protected void OnBtnSelecImagenClicked(object sender, EventArgs e)
		{
			Telefono.SeleccionarImagen(imgVisual, this);
		}

		protected void OnTvVerDatosButtonReleaseEvent(object o, Gtk.ButtonReleaseEventArgs args)
		{
			Gtk.TreeIter iter;
			if (tvVerDatos.Selection.CountSelectedRows() > 0)
			{
				tvVerDatos.Model.GetIter(out iter, tvVerDatos.Selection.GetSelectedRows()[0]);
				entryID.Text = tvVerDatos.Model.GetValue(iter, 0).ToString();
				entryNombre.Text = tvVerDatos.Model.GetValue(iter, 1).ToString();
				entryMarca.Text = tvVerDatos.Model.GetValue(iter, 2).ToString();
				entryModelo.Text = tvVerDatos.Model.GetValue(iter, 3).ToString();
				entryCompania.Text = tvVerDatos.Model.GetValue(iter, 4).ToString();
			}
		}
	}
}

