using System;
using System.IO;

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
				fs = new FileStream(ruta, FileMode.Append, FileAccess.Write);
				bw = new BinaryWriter(fs);

				bw.Write(ID);
				bw.Write(nombre);
				bw.Write(marca);
				bw.Write(modelo);
				bw.Write(compania);

				fs.Close();
				bw.Close();


			} catch (Exception ex) {
				return ex.Message;
			}
			return "Guardado";


		}





	}
}

