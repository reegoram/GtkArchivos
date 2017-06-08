using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Gtk;

namespace GtkArchivos
{
	public partial class Win_Telefono : Gtk.Window
	{
		op_Telefono Telefono;
		Gtk.ListStore DataTel;

		public Win_Telefono() :
			base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			Telefono = new op_Telefono();
			Telefono.LeerDatos();
			DataTel = Telefono.GenerarTreeView(tvVerDatos, DataTel);

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
					MessageBox.Show("Datos Guardados Exitosamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("Error de Guardado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

				/*string y = Telefono.LeerDatos();
				if (y == "Leido")
				{
					//DataTel.AppendValues(br.ReadInt32(), br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());

					/* Agregando Datos al Modelo */
				//DataTel.AppendValues(id.ToString(), nombre.ToString(), marca.ToString(), modelo.ToString(), compania.ToString());

				/*}
				else
				{
					MessageBox.Show("No Leido");
				}

				*/
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
	}
}

