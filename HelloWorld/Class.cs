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
}