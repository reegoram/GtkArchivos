using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Gtk;

namespace GtkArchivos
{
	public class op_Telefono
	{
		FileStream fs;
		BinaryWriter bw;
		//BinaryReader br;

		string ruta = @"telefono.dat";  /* Ruta Linux(root) */

		public int id { get; set; }
		public string nombre { get; set; }
		public string marca { get; set; }
		public string modelo { get; set; }
		public string compania { get; set; }
		public System.Drawing.Image imagen_telefono { get; set; }

		public string InsertarDatos()
		{
			try
			{
				fs = new FileStream(this.ruta, FileMode.Append, FileAccess.Write, FileShare.None);

				string data = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n\n",
						this.id.ToString(), this.nombre, this.marca, this.modelo,
						this.compania, CodificarImagen(this.imagen_telefono));

				using (bw = new BinaryWriter(fs))
				{
					char[] arreglo_data = data.ToCharArray();
					foreach (char i in arreglo_data)
					{
						bw.Write(i);
					}
				}
				fs.Close();
				fs.Dispose();

			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return "Guardado";
		}

		public string EliminarDatos() { 
			try
			{
				if (File.Exists(ruta))
					File.Delete(ruta);
				else if (!File.Exists(ruta))
					return "No Existe";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return "Eliminado";
		}

		public string CodificarImagen(System.Drawing.Image imagen)
		{
			string imagenCodificada = string.Empty;
			using (MemoryStream ms = new MemoryStream())
			{
				imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				imagenCodificada = Convert.ToBase64String(ms.ToArray());
			}
			return imagenCodificada;
		}

		public System.Drawing.Image DescodificarImagen(string imagenCodificada)
		{
			byte[] imagen_array = Convert.FromBase64String(imagenCodificada);
			System.Drawing.Image imagen;
			using (MemoryStream ms = new MemoryStream(imagen_array, 0, imagen_array.Length))
			{
				ms.Write(imagen_array, 0, imagen_array.Length);
				imagen = System.Drawing.Image.FromStream(ms);
			}
			return imagen;
		}

		public List<op_Telefono> LeerDatos()
		{
			List<op_Telefono> lista = new List<op_Telefono>();
			op_Telefono tel = new op_Telefono();
			try
			{
				using (FileStream fs = new FileStream(this.ruta, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					using (BinaryReader br = new BinaryReader(fs))
					{
						int pos = 0;
						int attr = 0;
						char last;
						int length = (int)br.BaseStream.Length;
						string temp = string.Empty;
						while (pos < length)
						{
							char c = br.ReadChar();
							if (c != '\n')
							{
								temp += c;
							}
							else
							{
								//Aquí desenmaraño lo que tiene el telefon.dat
								switch (++attr)
								{
									case 1: //id
										tel.id = int.Parse(temp);
										break;
									case 2: //nombre
										tel.nombre = temp;
										break;
									case 3: //marca
										tel.marca = temp;
										break;
									case 4: //modelo
										tel.modelo = temp;
										break;
									case 5: //compañia
										tel.compania = temp;
										break;
									case 6: //imagen
										tel.imagen_telefono = DescodificarImagen(temp);
										break;
									case 7: //reset
										lista.Add(tel);
										tel = new op_Telefono();
										attr = 0;
										break;
								}
								temp = string.Empty;
							}
							pos += sizeof(char);
						}
					}
				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}

			return lista;
		}


		public ListStore GenerarTreeView(Gtk.TreeView tree, ListStore ls)
		{
			TreeViewColumn colID = new TreeViewColumn();
			colID.Title = "ID";
			TreeViewColumn colNombre = new TreeViewColumn();
			colNombre.Title = "Nombre";
			TreeViewColumn colMarca = new TreeViewColumn();
			colMarca.Title = "Marca";
			TreeViewColumn colModelo = new TreeViewColumn();
			colModelo.Title = "Modelo";
			TreeViewColumn colCompania = new TreeViewColumn();
			colCompania.Title = "Compañia";

			tree.AppendColumn(colID);
			tree.AppendColumn(colNombre);
			tree.AppendColumn(colMarca);
			tree.AppendColumn(colModelo);
			tree.AppendColumn(colCompania);

			ls = new Gtk.ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			tree.Model = ls;

			CellRendererText celID = new CellRendererText();
			colID.PackStart(celID, true);
			CellRendererText celNombre = new CellRendererText();
			colNombre.PackStart(celNombre, true);
			CellRendererText celMarca = new CellRendererText();
			colMarca.PackStart(celMarca, true);
			CellRendererText celModelo = new CellRendererText();
			colModelo.PackStart(celModelo, true);
			CellRendererText celCompania = new CellRendererText();
			colCompania.PackStart(celCompania, true);

			colID.AddAttribute(celID, "text", 0);
			colNombre.AddAttribute(celNombre, "text", 1);
			colMarca.AddAttribute(celMarca, "text", 2);
			colModelo.AddAttribute(celModelo, "text", 3);
			colCompania.AddAttribute(celCompania, "text", 4);

			return ls;
		}

		public void Filtro(FileChooserDialog fChooser)
		{
			FileFilter filter = new FileFilter();
			filter.Name = "Archivo de Imágen (*.png)";
			filter.AddPattern("*.png");
			fChooser.AddFilter(filter);
		}


		public void ValidarEntry(Entry ent1, Entry ent2, Entry ent3, Entry ent4, Entry ent5)
		{
			if (ent1.Text == string.Empty || ent2.Text == string.Empty || ent3.Text == string.Empty
				|| ent4.Text == string.Empty || ent5.Text == string.Empty)
			{
				MessageBox.Show("Es necesario rellenar todo el formulario", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			ResetEntry(ent1, ent2, ent3, ent4, ent5);

		}

		public void ResetEntry(Entry ent1, Entry ent2, Entry ent3, Entry ent4, Entry ent5)
		{
			ent1.Text = string.Empty;
			ent2.Text = string.Empty;
			ent3.Text = string.Empty;
			ent4.Text = string.Empty;
			ent5.Text = string.Empty;
		}

		public void SeleccionarImagen(Gtk.Image imgV, Window win)
		{
			FileChooserDialog filechooser = new Gtk.FileChooserDialog("Seleccionar imágen", win, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);

			Filtro(filechooser);

			if (filechooser.Run() == (int)ResponseType.Accept)
			{
				FileStream file = File.OpenRead(filechooser.Filename);
				imgV.Pixbuf = new Gdk.Pixbuf(file, 150, 165);
				imgV.Pixbuf.ScaleSimple(imgV.Pixbuf.Width, imgV.Pixbuf.Height, 0);
				file.Close();
			}

			try
			{
				this.imagen_telefono = System.Drawing.Image.FromFile(filechooser.Filename);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.Write(e);
			}

			filechooser.Destroy();
		}

	}
}