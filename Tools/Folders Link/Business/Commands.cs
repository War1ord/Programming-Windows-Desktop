namespace Business
{
	public static class Commands
	{
		#region Creates a symbolic link
		//MKLINK [[/D] | [/H] | [/J]] Link Target
		//		/D      Creates a directory symbolic link.  Default is a file
		//				symbolic link.
		//		/H      Creates a hard link instead of a symbolic link.
		//		/J      Creates a Directory Junction.
		//		Link    specifies the new symbolic link name.
		//		Target  specifies the path (relative or absolute) that the new link
		//				refers to.
		#endregion

		/// <summary>
		/// The help command
		/// </summary>
		public static readonly string Help = "mklink /?";
		/// <summary>
		/// The symbolic link string format, mklink /D {Link} {Target}
		/// </summary>
		public static readonly string CreateSymbolicLinkStringFormat = "mklink /D {0} {1}";
	}
}