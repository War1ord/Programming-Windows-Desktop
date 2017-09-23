using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.IO;

namespace Toggl_To_Axosoft_Integration.Business
{
	public sealed class TemplateEngine
	{
		#region Singleton Design Pattern
		public static TemplateEngine Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (SyncObject)
					{
						if (_instance == null)
						{
							_instance = new TemplateEngine();
						}
					}
				}
				return _instance;
			}
		}
		private static TemplateEngine _instance;
		private static readonly Object SyncObject = new object();
		#endregion

		private string _templatePath;
		private IRazorEngineService _engine;
		private ITemplateKey _templateKeyReport;

		private TemplateEngine()
		{
			//_engine = RazorEngine.Engine.Razor;
			var config = new TemplateServiceConfiguration
			{
				//Debug = true, //NOTE: Debug can't be enabled when DisableTempFileLocking is enabled also. 
				DisableTempFileLocking = true,
				CachingProvider = new DefaultCachingProvider(t => { }), // or this

			};
			_engine = RazorEngineService.Create(config);
			_templatePath = Path.Combine(Environment.CurrentDirectory, "RazorViews");
			_templateKeyReport = AddTemplate("Report", _templatePath);
		}

		private IRazorEngineService Engine
		{
			get { return Instance._engine; }
			set { Instance._engine = value; }
		}

		public string RunAndCompileIfNeeded_Report(ViewModels.MainViewModel model)
		{
			return RunAndCompileIfNeeded(model, Instance._templateKeyReport);
		}

		#region Private Helpers
		private ITemplateKey AddTemplate(string name, string path)
		{
			var templateKey = _engine.GetKey(name);
			var razor = File.ReadAllText(Path.Combine(path, string.Format("{0}.cshtml", name)));
			var source = new LoadedTemplateSource(razor);
			_engine.AddTemplate(templateKey, source);
			return templateKey;
		}
		private string RunAndCompileIfNeeded(ViewModels.MainViewModel model, ITemplateKey templateKey)
		{
			var modelType = model.GetType();
			var html = !Instance.Engine.IsTemplateCached(templateKey, modelType)
				? Instance.Engine.RunCompile(templateKey, modelType, model)
				: Instance.Engine.Run(templateKey, modelType, model);
			return html;
		}
		#endregion

	}
}