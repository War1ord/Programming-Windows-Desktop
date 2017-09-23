using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace RulesEngine.Business
{
	/* sites to execute code from text 
	 * RunCode : http://www.codeproject.com/Articles/9019/Compiling-and-Executing-Code-at-Runtime
	 * http://west-wind.com/presentations/dynamicCode/DynamicCode.htm
	 * https://codeinject.codeplex.com/
	 * http://msdn.microsoft.com/en-us/vstudio/roslyn.aspx
	 * http://csharp-eval.com/HowTo.php
	 */

	public static class RunCode
	{
		public static object ExecuteCode(string code,
			string namespacename, string classname,
			string functionname, bool isstatic, 
			string[] references, params object[] args)
		{
			object returnval = null;
			Assembly asm = BuildAssembly(code, references);
			object instance = null;
			Type type = null;
			const string codeSeperator = ".";
			if (isstatic)
			{
				type = asm.GetType(namespacename + codeSeperator + classname);
			}
			else
			{
				instance = asm.CreateInstance(namespacename + codeSeperator + classname);
				type = instance.GetType();
			}
			MethodInfo method = type.GetMethod(functionname);
			returnval = method.Invoke(instance, args);
			return returnval;
		}

		private static Assembly BuildAssembly(string code, params string[] references)
		{
			var provider = new CSharpCodeProvider();
			var compiler = provider.CreateCompiler();
			var compilerparams = new CompilerParameters
			                     {
				                     GenerateExecutable = false,
				                     GenerateInMemory = true
			                     };
			compilerparams.ReferencedAssemblies.AddRange(references);
			var results = compiler.CompileAssemblyFromSource(compilerparams, code);
			if (results.Errors.HasErrors)
			{
				var errors = new StringBuilder("Compiler Errors :\r\n");
				foreach (CompilerError error in results.Errors)
				{
					errors.AppendFormat("Line {0},{1}\t: {2}\n",
						error.Line, error.Column, error.ErrorText);
				}
				throw new Exception(errors.ToString());
			}
			else
			{
				return results.CompiledAssembly;
			}
		}
	}

}