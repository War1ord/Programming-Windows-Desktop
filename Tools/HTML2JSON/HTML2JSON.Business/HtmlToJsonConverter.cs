using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML2JSON.Business
{
	public class HtmlToJsonConverter
	{
		public string Html { get; set; }

		public HtmlToJsonConverter(string html)
		{
			Html = html;
		}

		public string ToJson()
		{
			var htmlDocument = new HtmlAgilityPack.HtmlDocument { OptionOutputAsXml = true };
			htmlDocument.LoadHtml(Html);
			var inputs = htmlDocument.DocumentNode.Descendants("input")
				.Where(a => !a.Attributes.Any(t => t.Name == "type"
					&& (t.Value == "button"
						|| t.Value == "file"
						|| t.Value == "submit"
						))
					&& a.Attributes.Any(t => t.Name  == "name" && t.Value != "__RequestVerificationToken")).ToList();
			var selects = htmlDocument.DocumentNode.Descendants("select").ToList();
			var textareas = htmlDocument.DocumentNode.Descendants("textarea").ToList();

			var sb = new StringBuilder();
			sb.Append("var json = {");
			sb.AppendLine();
			foreach (var tag in inputs)
			{
				sb.Append(tag.Id);
				sb.Append(": ");
				sb.Append("$('#");
				sb.Append(tag.Id);
				sb.Append("').val()");
				/*sb.Append("'");*/
				/*sb.Append(tag.GetAttributeValue("value", ""));*/
				/*sb.Append("'");*/
				sb.Append(",");
				sb.AppendLine();
			}
			foreach (var tag in selects)
			{
				sb.Append(tag.Id);
				sb.Append(": ");
				sb.Append("$('#");
				sb.Append(tag.Id);
				sb.Append("').is(':checked')");
				/*sb.Append("'");*/
				/*sb.Append(tag.Descendants("option").Where(o => o.Attributes.Any(t => t.Name == "selected")).Select(o => o.GetAttributeValue("value", "")).FirstOrDefault());*/
				/*sb.Append("'");*/
				sb.Append(",");
				sb.AppendLine();
			}
			foreach (var tag in textareas)
			{
				sb.Append(tag.Id);
				sb.Append(": ");
				sb.Append("$('#");
				sb.Append(tag.Id);
				sb.Append("').val()");
				/*sb.Append("'");*/
				/*sb.Append(tag.GetAttributeValue("value", ""));*/
				/*sb.Append("'");*/
				sb.Append(",");
				sb.AppendLine();
			}
			sb.Append("}");

			var json = sb.ToString();

			return json;
		}
	}
}
