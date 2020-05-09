using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HelloWorld
{
	class Program
	{
		delegate int DelegateTest(int i, int z);

		static void Main(string[] args)
		{
			RunEnumTest();
		}

		static void RunEnumTest()
		{
			Student student = new Student("B");

			if (student.Diet == Diet.VEGE)
			{
				Console.WriteLine("Works!");
			}
		}

		static void RunTestForExceptionCatch(string str)
		{
			string something = Methods.InnerMethod(str);
		}

		static void RunTestForIdGeneration()
		{
			Methods.OldIdGenerator();
			Methods.NewIdGenerator();
		}

		static void RunTestForDelegateAndFunc()
		{
			DelegateTest DelegateSample = new DelegateTest(Methods.Add);
			Func<int, int, int> FuncSample = Methods.Add;

			Console.WriteLine(DelegateSample(10, 20));
			Console.WriteLine(FuncSample(100, 20));
			Methods.Framework(Methods.Add);
			Methods.Framework(Methods.Multiple);
			Methods.Framework((x, y) => x % y);

			Methods.ObjectFramework(Methods.PrintGrade, "A+");
			Methods.ObjectFramework((x) => ((Student)x).Grade, "C+");

			List<Student> list = new List<Student> {
				new Student("A"),
				new Student("B"),
				new Student("D")
			};

			Student student = new Student("ZZZZ");
			Console.WriteLine($"Student Name: { student.Name }");

			foreach (Student i in list)
			{
				Console.WriteLine($"Name: { i.Name }");
			}

			Methods.ListReferenceTest(list, student);
			Console.WriteLine($"Student Name: { student.Name }");

			foreach (Student i in list)
			{
				Console.WriteLine($"Name: { i.Name }");
			}
		}

		static void RunTestForReadingJson()
		{
			string myJsonString = File.ReadAllText("./myfile.json");
			object myJsonObject = JsonConvert.DeserializeObject<MyJsonType>(myJsonString);
			Methods.ReadObjectPropertiesAndValue(myJsonObject);
			Methods.ReadGenericJsonObjectPropertiesAndValueV2();
		}

		static void RunTestForDateTimeUTC()
		{
			DateTime DateTime1 = DateTime.Now;
			DateTime DateTime2 = DateTime.UtcNow;

			DateTimeOffset DateTimeOffset1 = DateTimeOffset.Now;
			DateTimeOffset DateTimeOffset2 = DateTimeOffset.UtcNow;

			Console.WriteLine($"DateTimeNow: { DateTime1 }");
			Console.WriteLine($"DateTimeUtcNow: { DateTime2 }");

			Console.WriteLine($"DateTimeOffsetNow: { DateTimeOffset1 }");
			Console.WriteLine($"DateTimeOffsetUtcNow: { DateTimeOffset2 }");
		}
	}
}
