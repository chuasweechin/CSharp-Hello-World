using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Globalization;

namespace HelloWorld
{

	class Program
	{
		delegate int DelegateTest(int i, int z);

		static void Main(string[] args)
		{
			Console.WriteLine("Test");
		}

		static bool IsOnlyZeroTest(string value)
		{
			return string.IsNullOrWhiteSpace(value.Trim('0'));
		}

		static void IsValidDateTest()
		{
			string date = "20201212";
			string format = "yyyyMMdd";

			if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDateValue))
				Console.WriteLine("Ok");
		}

		static void ListFilterTest()
		{
			List<Student> list = new List<Student>
			{
				new Student("A"),
				new Student("A"),
				new Student("B"),
				new Student("B")
			};

			list.RemoveAll(s => s.Grade == "A");
			var filter = list.Where(s => s.Grade == "C").FirstOrDefault();

			Console.WriteLine(filter);
		}

		static void AsyncTest()
		{
			int ii = 0;
			var tokenSource = new CancellationTokenSource();
			var token = tokenSource.Token;

			var _ignored = Task.Run(() =>
			{
				Methods.BackgroundThread(token);
			}, token);

			while (true)
			{
				ii++;

				Console.WriteLine($"{ii}: Method Thread running....");

				if (ii == 100)
				{
					tokenSource.Cancel();
					Console.WriteLine("Method Thread cancel background task.....");
					tokenSource.Dispose();
					break;
				}
			}
		}

		static void RegexTest()
		{
			string amount = "1.0";
			var rx = new Regex(@"^[0-9]+(\.[0-9]{1,2})?$", RegexOptions.Compiled);
			bool result = rx.IsMatch(amount);

			Console.WriteLine(amount.ToString());
			Console.WriteLine(result.ToString());
		}

		static void RunTestForToString()
		{
			decimal amount = 100.1200m;
			DateTimeOffset dt = DateTimeOffset.UtcNow;
			DateTimeOffset dt2 = DateTimeOffset.Now;

			DateTimeOffset dt3 = DateTimeOffset.Parse("21/05/2020 05:43:43".ToString());
			Console.WriteLine(amount.ToString());
			Console.WriteLine(dt.ToString());
			Console.WriteLine(dt2.ToString());

			Console.WriteLine(dt3.ToString());
		}

		static void RunTestForAmount()
		{
			ulong cents = 100 / 3;
			string dollar = string.Format("{0:0.00}", (decimal)cents / 100);
			Console.WriteLine(dollar);
		}

		static void RunTestForObjectReference()
		{
			List<Student> students = new List<Student>();
			students.Add(new Student("A"));
			Student clone = null;

			foreach (var student in students)
			{
				clone = student.Clone() as Student;
				clone.Grade = "clone";
			}

			students.Add(clone);

			foreach (var student in students)
			{
				Console.WriteLine(student.Grade);
			}
		}

		static void RunTestForEnum()
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
