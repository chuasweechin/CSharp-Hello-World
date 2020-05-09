namespace HelloWorld
{
	public class Diet : Enumeration
	{
		public static Diet FRUI = new Diet(1, "FRUI", "Fruit");
		public static Diet VEGE = new Diet(2, "VEGE", "Vegatable");
		public static Diet MEAT = new Diet(3, "MEAT", "Meat such as pork");

		public string Description { get; }

		public Diet(int id, string code, string description) : base(id, code)
		{
			Description = description;
		}
	}
}