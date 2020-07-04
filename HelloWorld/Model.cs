using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelloWorld
{
  class MyJsonType
	{
		public MyJsonSubDocumentType MySubDocument { get; set; }
		public MyJsonSubDocumentType MySubDocument2 { get; set; }
		public List<MyJsonSubDocumentType> MyListObject { get; set; }
		public List<string> MyListProperty { get; set; }
  }

  class MyJsonSubDocumentType
  {
		public string SubDocumentProperty { get; set; }
		public string MyStringProperty { get; set; }
		public int MyIntegerProperty { get; set; }
  }

	class Order
	{
		public string Customer { get; set; }
		public decimal Amount { get; set; }
	}

	[Serializable]
	public abstract class Person
	{
		public string Name { get; set; }
	}

	[Serializable]
	public class Student : Person, ICloneable
	{
		public string Grade { get; set; }
		public Diet Diet;

		public Student(string grade)
		{
			Grade = grade;
			Diet = Diet.VEGE;
		}

		public object Clone()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				if (GetType().IsSerializable)
				{
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(stream, this);
					stream.Position = 0;

					return formatter.Deserialize(stream);
				}
				return null;
			}
		}
	}
}