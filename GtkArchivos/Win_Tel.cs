using System;

namespace GtkArchivos
{
	public partial class Win_Tel : Gtk.Window
	{
		public Win_Tel () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

