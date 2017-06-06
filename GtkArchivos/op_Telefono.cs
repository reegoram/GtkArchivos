using System;
using System.IO;
using System.Windows.Forms;
using Gtk;

namespace GtkArchivos
{
	public class op_Telefono
	{
		FileStream fs;
		//StreamReader sr;
		BinaryWriter bw;
		//BinaryReader br;

		public string nombre { get; set; }
		public string marca { get; set; }
		public string modelo { get; set; }
		public string compania { get; set;}

		public op_Telefono (string ruta)
		{
			fs = new FileStream (ruta, FileMode.Append, FileAccess.Write);
			fs.Close ();


		}


		public string InsertarDatos(string ruta, int ID, string nombre, string marca, string modelo, string compania){
			try {
				fs = new FileStream(ruta, FileMode.Append, FileAccess.Write, FileShare.None);
				bw = new BinaryWriter(fs);

				bw.Write(ID.ToString());
				bw.Write(nombre.ToString());
				bw.Write(marca.ToString());
				bw.Write(modelo.ToString());
				bw.Write(compania.ToString());

				fs.Close();
				bw.Close();


			} catch (Exception ex) {
				return ex.Message;
			}
			return "Guardado";


		}

		public string LeerDatos (string ruta){
			try {


			} catch (Exception ex) {
				return ex.Message;
			}
			return "Leido";
		}


		public ListStore GenerarTreeView(Gtk.TreeView tree, ListStore ls){
			TreeViewColumn colID = new TreeViewColumn();
			colID.Title = "ID";
			TreeViewColumn colNombre = new TreeViewColumn ();
			colNombre.Title = "Nombre";
			TreeViewColumn colMarca = new TreeViewColumn ();
			colMarca.Title = "Marca";
			TreeViewColumn colModelo = new TreeViewColumn ();
			colModelo.Title = "Modelo";
			TreeViewColumn colCompania = new TreeViewColumn ();
			colCompania.Title = "Compañia";

			tree.AppendColumn (colID);
			tree.AppendColumn (colNombre);
			tree.AppendColumn (colMarca);
			tree.AppendColumn (colModelo);
			tree.AppendColumn (colCompania);

			ls = new Gtk.ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			tree.Model = ls;

			CellRendererText celID = new CellRendererText();
			colID.PackStart (celID, true);
			CellRendererText celNombre = new CellRendererText ();
			colNombre.PackStart (celNombre, true);
			CellRendererText celMarca = new CellRendererText ();
			colMarca.PackStart (celMarca, true);
			CellRendererText celModelo = new CellRendererText ();
			colModelo.PackStart (celModelo, true);
			CellRendererText celCompania = new CellRendererText ();
			colCompania.PackStart (celCompania, true);

			colID.AddAttribute (celID, "text", 0);
			colNombre.AddAttribute (celNombre, "text", 1);
			colMarca.AddAttribute (celMarca, "text", 2);
			colModelo.AddAttribute (celModelo, "text", 3);
			colCompania.AddAttribute (celCompania, "text", 4);

			return ls;
		}

		public void Filtro(FileChooserDialog fChooser){
			FileFilter filter = new FileFilter ();
			filter.Name = "Archivo de Imágen (*.jpg, *.jpeg, *.png)";
			filter.AddPattern ("*.jpg");
			filter.AddPattern ("*.jpeg");
			filter.AddPattern ("*.png");
			fChooser.AddFilter (filter);

		}


		public void ValidarEntry(Entry ent1, Entry ent2, Entry ent3, Entry ent4, Entry ent5){
			if (ent1.Text == string.Empty || ent2.Text == string.Empty || ent3.Text == string.Empty 
				|| ent4.Text == string.Empty || ent5.Text == string.Empty) {
				MessageBox.Show("Es necesario rellenar todo el formulario", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			ResetEntry (ent1, ent2, ent3, ent4, ent5);

		}

		public void ResetEntry(Entry ent1, Entry ent2, Entry ent3, Entry ent4, Entry ent5){
			ent1.Text = string.Empty;
			ent2.Text = string.Empty;
			ent3.Text = string.Empty;
			ent4.Text = string.Empty;
			ent5.Text = string.Empty;
		}

		public void SeleccionarImagen(Image imgV, Window win){
			FileChooserDialog filechooser = new Gtk.FileChooserDialog ("Seleccionar imágen", win ,FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);

			Filtro (filechooser);

			if (filechooser.Run () == (int)ResponseType.Accept) {

				FileStream file = File.OpenRead (filechooser.Filename);
				imgV.Pixbuf = new Gdk.Pixbuf (file, 150, 165);
				imgV.Pixbuf.ScaleSimple (imgV.Pixbuf.Width, imgV.Pixbuf.Height, 0);
				file.Close ();

			}

			filechooser.Destroy ();


		}











	}
}

