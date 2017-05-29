using System;
using Gtk;

namespace GtkArchivos
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Win_Telefono win = new Win_Telefono ();
			win.Show ();
			Application.Run ();
		}
	}
}
