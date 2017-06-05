using System;
using System.IO;
using System.Windows.Forms;
using Gtk;

namespace GtkArchivos
{
	public class op_Telefono
	{
		FileStream fs;
		BinaryWriter bw;
		BinaryReader br;

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

		public string LeerDatos (string ruta, Gtk.Entry entry){
			try {
				StreamReader sr = new StreamReader (ruta);
				entry.Text = sr.ReadLine ();
				sr.Close ();
				br = new BinaryReader(File.Open(ruta, FileMode.Open));
				System.Diagnostics.Debug.WriteLine(br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
				br.Close();
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












	}
}

