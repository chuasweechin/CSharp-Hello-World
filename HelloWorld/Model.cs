using System.Collections.Generic;

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

	public abstract class Person
	{
		public string Name { get; set; }
	}

	public class Student : Person
	{
		public string Grade { get; set; }

		public Student(string grade)
		{
			Grade = grade;

		}
	}
}