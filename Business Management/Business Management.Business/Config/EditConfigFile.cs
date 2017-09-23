using Business_Management.Business.Extentions;
using Business_Management.Business.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace Business_Management.Business.Config
{
	public static class EditConfigFile
	{
		public static Result UpdateConnectionString(string assemblyLocation, string connectionString)
		{
			try
			{
				string configFileLocation = string.Format("{0}.config", assemblyLocation);
				var xml = new XmlDocument();
				xml.Load(configFileLocation);
				XmlNode configuration = null;
				foreach (XmlNode node in xml.ChildNodes)
				{
					if (node.Name.Equals("configuration", StringComparison.InvariantCultureIgnoreCase))
					{
						configuration = node;
						continue;
					}
				}
				if (configuration.IsSet())
				{
					XmlNode connectionStrings = null;
					foreach (XmlNode node in configuration.ChildNodes)
					{
						if (node.Name.Equals("connectionStrings", StringComparison.InvariantCultureIgnoreCase))
						{
							connectionStrings = node;
							continue;
						}
					}
					if (connectionStrings.IsSet())
					{
						foreach (XmlNode node in connectionStrings.ChildNodes)
						{
							if (node.Attributes.IsNotSet()) continue;
							var connectionStringValue = node.Attributes["connectionString"];
							var name = node.Attributes["name"];
							var providerName = node.Attributes["providerName"];
							if (name.IsSet())
							{
								switch (name.Value)
								{
									case "DefaultConnection":
										connectionStringValue.Value = connectionString;
										break;
									default:
										break;
								}
							}
						}
						xml.Save(configFileLocation);
						ConfigurationManager.RefreshSection("connectionStrings");
						return Results.SuccessResult();
					}
					else
					{
						return Results.InvalidResult("invalid connectionStrings xml node.");
					}
				}
				else
				{
					return Results.InvalidResult("invalid configuration xml node.");
				}
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e);
			}
		}
		public static Result UpdateAppSettings(string assemblyLocation, params KeyValuePair<string, string>[] settings)
		{
			try
			{
				string configFileLocation = string.Format("{0}.config", assemblyLocation);
				var xml = new XmlDocument();
				xml.Load(configFileLocation);
				XmlNode configuration = null;
				foreach (XmlNode node in xml.ChildNodes)
				{
					if (node.Name.Equals("configuration", StringComparison.InvariantCultureIgnoreCase))
					{
						configuration = node;
						continue;
					}
				}
				if (configuration.IsSet())
				{
					XmlNode appSettings = null;
					foreach (XmlNode node in configuration.ChildNodes)
					{
						if (node.Name.Equals("appSettings", StringComparison.InvariantCultureIgnoreCase))
						{
							appSettings = node;
							continue;
						}
					}
					if (appSettings.IsSet())
					{
						foreach (XmlNode node in appSettings.ChildNodes)
						{
							if (node.Attributes.IsNotSet()) continue;
							var value = node.Attributes["value"];
							var key = node.Attributes["key"];
							if (key.IsSet())
							{
								var search = settings.Where(i => key.Value.Equals(i.Key, StringComparison.InvariantCultureIgnoreCase));
								if (search.Any())
								{
									//update
									var setting = search.First();
									value.Value = setting.Value;
								}
								else
								{
									//TODO: build functionality to add the appSettings
								}
							}
						}
						xml.Save(configFileLocation);
						ConfigurationManager.RefreshSection("appSettings");
						return Results.SuccessResult();
					}
					else
					{
						return Results.InvalidResult("invalid appSettings xml node.");
					}
				}
				else
				{
					return Results.InvalidResult("invalid configuration xml node.");
				}
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e);
			}
		}
	}
}
