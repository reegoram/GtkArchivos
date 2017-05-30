using System;
using System.Windows.Forms;
using Gtk;

namespace GtkArchivos
{
	public partial class Win_Telefono : Gtk.Window
	{
		
		// string ruta = @"C:\TELEFONO\telefono.dat";	/* Ruta Windows */
		string ruta = @"telefono.dat";	/* Ruta Linux(root) */
		
		op_Telefono Telefono;
		
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

			Gtk.TreeViewColumn colID = new Gtk.TreeViewColumn();
			colID.Title = "ID";
			Gtk.TreeViewColumn colNombre = new Gtk.TreeViewColumn ();
			colNombre.Title = "Nombre";
			Gtk.TreeViewColumn colMarca = new Gtk.TreeViewColumn ();
			colMarca.Title = "Marca";
			Gtk.TreeViewColumn colModelo = new Gtk.TreeViewColumn ();
			colModelo.Title = "Modelo";
			Gtk.TreeViewColumn colCompania = new Gtk.TreeViewColumn ();
			colCompania.Title = "Compañia";

			tvVerDatos.AppendColumn (colID);
			tvVerDatos.AppendColumn (colNombre);
			tvVerDatos.AppendColumn (colMarca);
			tvVerDatos.AppendColumn (colModelo);
			tvVerDatos.AppendColumn (colCompania);

			Gtk.ListStore DataTel = new Gtk.ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			tvVerDatos.Model = DataTel;

			DataTel.AppendValues ("1", "Moto G4", "Motorola", "XT1627", "Libre");
			DataTel.AppendValues ("2", "Moto G3", "Motorola", "XT1038", "AT&T");
			DataTel.AppendValues ("3", "Huawei P8", "Huawei", "P8", "AT&T");

			Gtk.CellRendererText celID = new Gtk.CellRendererText();
			colID.PackStart (celID, true);
			Gtk.CellRendererText celNombre = new Gtk.CellRendererText ();
			colNombre.PackStart (celNombre, true);
			Gtk.CellRendererText celMarca = new Gtk.CellRendererText ();
			colMarca.PackStart (celMarca, true);
			Gtk.CellRendererText celModelo = new Gtk.CellRendererText ();
			colModelo.PackStart (celModelo, true);
			Gtk.CellRendererText celCompania = new Gtk.CellRendererText ();
			colCompania.PackStart (celCompania, true);

			colID.AddAttribute (celID, "text", 0);
			colNombre.AddAttribute (celNombre, "text", 1);
			colMarca.AddAttribute (celMarca, "text", 2);
			colModelo.AddAttribute (celModelo, "text", 3);
			colCompania.AddAttribute (celCompania, "text", 4);


		}


		protected void OnBtnGuardarClicked (object sender, EventArgs e)
		{
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
	}
}

