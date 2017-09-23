using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RulesEngine.Models
{
	public class CodeBlock : ModelBase
	{
		public CodeBlock(){}
		public CodeBlock(string name, string code)
		{
			Name = name;
			Code = code;
		}

		public string Name { get; set; }
		public string Code { get; set; }
		public bool IsStatic { get; set; }

		public Guid RuleID { get; set; }

		[ForeignKey("RuleID")]
		public Rule Rule { get; set; }

		public List<AssemblyReference> AssemblyReferences { get; set; }

	}
}