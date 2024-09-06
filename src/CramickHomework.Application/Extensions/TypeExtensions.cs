namespace CramickHomework.Application.Extensions
{
	public static class TypeExtensions
	{
		public static string GetGenericTypeName(this Type type, bool includeNamespace = false)
		{
			string typeName;

			if (type.IsGenericType)
			{
				var genericTypes =
					string.Join(",",
						type.GetGenericArguments()
							.Select(t => t.GetGenericTypeName())
							.ToArray());
				typeName =
					$"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
			}
			else
			{
				typeName = type.Name;
			}

			return includeNamespace ? $"{type.Namespace}.{typeName}" : typeName;
		}

		public static string GetGenericTypeName(this object? @object, bool includeNamespace = false) =>
			@object?.GetType().GetGenericTypeName(includeNamespace) ?? typeof(object).GetGenericTypeName(includeNamespace);

		public static bool ImplementsInterface(this Type concreteType, Type interfaceType) =>
		   concreteType.GetInterfaces().Any(t =>
			   (interfaceType.IsGenericTypeDefinition && t.IsGenericType ? t.GetGenericTypeDefinition() : t) == interfaceType);
	}
}
