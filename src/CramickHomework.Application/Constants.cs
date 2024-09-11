namespace CramickHomework.Application
{
	public static class Constants
	{
		public static class Application
		{
			public static string Title => "Cramick Homework API";
			public static string Version => "v1.0";
			public static string Description => "Homework assignment application for Fullstack .NET Angular.js Developer job opportunity";
		}

		public static class System
		{
			public static Guid UserId => new("b59b00e0-d70e-499f-bfaf-dca40561fa65");
			public static string Email => "cramickhomework@gmail.com";
			public static string Name => "SYSTEM";

			public static Guid AdministratorRoleId => new("b920cbd7-d17e-44b0-82c9-89c7ed51d7a8");
			public static string AdministratorRoleName => "Administrator";
			public static Guid UserRoleId => new("6e0da5cf-14f0-4f25-8785-8715f608756b");
			public static string UserRoleName => "User";
		}

		public static class Identity
		{
			public static bool DefaultRequireConfirmedEmail => false;
		}

		public static class User
		{
			public static int FullNameLength => 256;
			public static int EmailLength => 256;
			public static int PhoneLength => 20;
		}

		public static class Environment
		{
			public static string VariableName => "ASPNETCORE_ENVIRONMENT";

			public static string Development => nameof(Development);
			public static string Production => nameof(Production);

			public static readonly string[] Names =
			{
				Development, Production
			};
		}

		public static class Performance
		{
			public static int LongRunngingTaskTresholdSeconds => 5;
		}

		public static class ClaimTypes
		{
			/// <summary>
			/// Subject claim used to uniquely identify the principal per https://www.rfc-editor.org/rfc/rfc7519#section-4.1
			/// </summary>
			public static string Sub = "sub";
		}

		public static class Email
		{
			public static string FromEmail => "playawardshomework@gmail.com";
			public static string FromName => "Homework No-Reply";
		}
	}
}
