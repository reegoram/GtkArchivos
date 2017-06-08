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
		}

		void RevisarLista()
		{
			if (telefonos != null)
			{
				// Llenar el dgv desde la lista
				for (int i = 0; i < telefonos.Count; i++)
				{
					//Agregas los valores al datagridview ** Ok/2
						lsDataTel.AppendValues(telefonos[0].id.ToString(), telefonos[0].nombre.ToString(), 
					                           telefonos[0].marca.ToString(), telefonos[0].modelo.ToString(), telefonos[0].compania.ToString());
				}
			}
		}

		//Cuando se dé clic en el datagridview 
		//TODO: Agregar el método manejador que escuche el clic en el datagridview
		void Mostrar_imagen(int id)
		{
			// Buscar el telefono del id en la lista
			//imgVisual.Pixbuf = new Gdk.Pixbuf(Telefono.Imagen(telefonos[id_en_lista].imagen_telefono));
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

