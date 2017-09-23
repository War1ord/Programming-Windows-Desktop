using System.Collections.Generic;

namespace RulesEngine.Models
{
	public class AssemblyReference : ModelBase
	{
		public AssemblyReference(){}
		public AssemblyReference(string fullName, string description)
		{
			FullName = fullName;
			Description = description;
		}

		public string FullName { get; set; }
		public string Description { get; set; }

		public List<CodeBlock> CodeBlocks { get; set; }
	}
}