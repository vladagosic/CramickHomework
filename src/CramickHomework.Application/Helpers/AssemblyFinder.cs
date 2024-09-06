using System.Reflection;

namespace CramickHomework.Application.Helpers
{
	public static class AssemblyFinder
	{
		public static Assembly DomainAssembly => Assembly.Load("CramickHomework.Domain");
		public static Assembly ApplicationAssembly => Assembly.Load("CramickHomework.Application");
		public static Assembly InfrastructureAssembly => Assembly.Load("CramickHomework.Infrastructure");
		public static Assembly ServerAssembly => Assembly.Load("CramickHomework.Server");
	}
}
