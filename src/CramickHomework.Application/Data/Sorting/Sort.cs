namespace CramickHomework.Application.Data.Sorting
{
	public class Sort
	{
		private static readonly string DescString = "desc";
		private static readonly string EmptyField = string.Empty;
		private static readonly string DefaultField = "Id";

		public static readonly Sort Default = By(DefaultField, true);
		public static readonly Sort Empty = new();

		public string Field { get; set; } = EmptyField;
		public bool Descending { get; set; } = false;

		public static Sort By(string? field, bool descending = false)
		{
			return new Sort
			{
				Field = field ?? EmptyField,
				Descending = descending
			};
		}

		public static Sort Parse(string sort)
		{
			if (string.IsNullOrWhiteSpace(sort))
				throw new ArgumentNullException(nameof(sort));

			var parts = sort.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			return By(parts[0], ParseIsDescending(parts));
		}

		public static implicit operator string(Sort value)
		{
			return $"{value.Field} {(value.Descending ? DescString : string.Empty)}";
		}

		public static implicit operator Sort(string value)
		{
			return Parse(value);
		}

		private static bool ParseIsDescending(string[] parts)
		{
			return parts.Length > 1 && string.Equals(parts[1], DescString, StringComparison.InvariantCultureIgnoreCase);
		}

		protected IEnumerable<object?> GetValues()
		{
			yield return Field;
			yield return Descending;
		}
	}
}
