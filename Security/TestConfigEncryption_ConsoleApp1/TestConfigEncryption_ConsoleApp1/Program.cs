using System;
using System.Configuration;

public class UsingRsaProtectedConfigurationProvider
{
	public static void Main(string[] args)
	{
		ProtectConfiguration();
		//UnProtectConfiguration();
	}

	// Protect the connectionStrings section.
	private static void ProtectConfiguration()
	{

		// Get the application configuration file.
		System.Configuration.Configuration config =
				ConfigurationManager.OpenExeConfiguration(
				ConfigurationUserLevel.None);

		// Define the Rsa provider name.
		string provider =
			"RsaProtectedConfigurationProvider";

		// Get the section to protect.
		ConfigurationSection connStrings =
			config.ConnectionStrings;

		if (connStrings != null)
		{
			if (!connStrings.SectionInformation.IsProtected)
			{
				if (!connStrings.ElementInformation.IsLocked)
				{
					// Protect the section.
					connStrings.SectionInformation.ProtectSection(provider);

					connStrings.SectionInformation.ForceSave = true;
					config.Save(ConfigurationSaveMode.Full);

					Console.WriteLine("Section {0} is now protected by {1}",
						connStrings.SectionInformation.Name,
						connStrings.SectionInformation.ProtectionProvider.Name);

				}
				else
					Console.WriteLine(
						 "Can't protect, section {0} is locked",
						 connStrings.SectionInformation.Name);
			}
			else
				Console.WriteLine(
					"Section {0} is already protected by {1}",
					connStrings.SectionInformation.Name,
					connStrings.SectionInformation.ProtectionProvider.Name);

		}
		else
			Console.WriteLine("Can't get the section {0}",
				connStrings.SectionInformation.Name);

	}

	// Unprotect the connectionStrings section.
	private static void UnProtectConfiguration()
	{

		// Get the application configuration file.
		System.Configuration.Configuration config =
				ConfigurationManager.OpenExeConfiguration(
				ConfigurationUserLevel.None);

		// Get the section to unprotect.
		ConfigurationSection connStrings =
			config.ConnectionStrings;

		if (connStrings != null)
		{
			if (connStrings.SectionInformation.IsProtected)
			{
				if (!connStrings.ElementInformation.IsLocked)
				{
					// Unprotect the section.
					connStrings.SectionInformation.UnprotectSection();

					connStrings.SectionInformation.ForceSave = true;
					config.Save(ConfigurationSaveMode.Full);

					Console.WriteLine("Section {0} is now unprotected.",
						connStrings.SectionInformation.Name);

				}
				else
					Console.WriteLine(
						 "Can't unprotect, section {0} is locked",
						 connStrings.SectionInformation.Name);
			}
			else
				Console.WriteLine(
					"Section {0} is already unprotected.",
					connStrings.SectionInformation.Name);

		}
		else
			Console.WriteLine("Can't get the section {0}",
				connStrings.SectionInformation.Name);

	}


}
