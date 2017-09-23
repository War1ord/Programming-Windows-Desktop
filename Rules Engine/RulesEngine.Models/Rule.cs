using System.Collections.Generic;

namespace RulesEngine.Models
{
	public class Rule : ModelBase
	{
		public string Namespace { get; set; }
		public string Class { get; set; }

		public string Description { get; set; }

		public List<CodeBlock> CodeBlocks { get; set; }
	}
}
