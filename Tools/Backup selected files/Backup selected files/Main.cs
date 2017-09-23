using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;

namespace SingleInstanceApp
{
	class Global
	{
		private static List<string> argumentList;
		public static List<string> ArgumentList { get { return argumentList ?? (argumentList = new List<string>()); } }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			new SingleInstanceApplication()
				.Run(args);
		}

		/// <summary>
		/// We inherit from the VB.NET WindowsFormApplicationBase class, which has the 
		/// single-instance functionality.
		/// </summary>
		class SingleInstanceApplication : WindowsFormsApplicationBase
		{
			public SingleInstanceApplication()
			{
				// Make this a single-instance application
				IsSingleInstance = true;
				EnableVisualStyles = true;

				// There are some other things available in the VB application model, for
				// instance the shutdown style:
				ShutdownStyle = ShutdownMode.AfterMainFormCloses;

				// Add StartupNextInstance handler
				StartupNextInstance += new StartupNextInstanceEventHandler(SIApp_StartupNextInstance);
			}

			/// <summary>
			/// We are responsible for creating the application's main form in this override.
			/// </summary>
			protected override void OnCreateMainForm()
			{
				// Create an instance of the main form and set it in the application; 
				// but don't try to run it.
				MainForm = new MainForm();

				// We want to pass along the command-line arguments to this first instance

				// Allocate room in our string array
				((MainForm)MainForm).Args = new string[CommandLineArgs.Count];

				// And copy the arguments over to our form
				CommandLineArgs.CopyTo(((MainForm)MainForm).Args, 0);
			}

			/// <summary>
			/// This is called for additional instances. The application model will call this 
			/// function, and terminate the additional instance when this returns.
			/// </summary>
			/// <param name="eventArgs"></param>
			protected void SIApp_StartupNextInstance(object sender, StartupNextInstanceEventArgs eventArgs)
			{
				// Copy the arguments to a string array
				string[] args = new string[eventArgs.CommandLine.Count];
				eventArgs.CommandLine.CopyTo(args, 0);

				// Need to use invoke to b/c this is being called from another thread.
				MainForm.Invoke(new MainForm.ProcessParametersDelegate(
					((MainForm)MainForm).ProcessParameters),
					MainForm, args);
			}
		}
	}
}
