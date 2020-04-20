using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace HelloWorld
{
  class Program
  {
		delegate int DelegateTest(int i, int z);

		static void Main(string[] args)
		{
			//string myJsonString = File.ReadAllText("./myfile.json");
			//object myJsonObject = JsonConvert.DeserializeObject<MyJsonType>(myJsonString);
			//ReadObjectPropertiesAndValue(myJsonObject);
			//ReadGenericJsonObjectPropertiesAndValueV2();
			//DateTimeTest();
			DelegateTest DelegateSample = new DelegateTest(Add);
			Func<int, int, int> FuncSample = Add;

			//Console.WriteLine(DelegateSample(10, 20));
			//Console.WriteLine(FuncSample(100, 20));
			//Framework(Add);
			//Framework(Multiple);
			//Framework((x, y) => x % y);

			ObjectFramework(PrintGrade, "A+");
			ObjectFramework((x) => ((Student)x).Grade, "C+");
		}

		static int Add(int a, int b)
		{
			return a + b;
		}

		static int Multiple(int a, int b)
		{
			return a * b;
		}

		static string PrintGrade(Person x)
		{
			return ((Student)x).Grade;
		}

		static void Framework(Func<int, int, int> func)
		{
			Console.WriteLine(func(3, 10));
			Console.WriteLine(func(3, 10) + 1000000);
		}

		static void ObjectFramework(Func<Person, string> func, string grade)
		{
			Console.WriteLine("Grade:");
			Console.WriteLine( func( new Student(grade) ) );
		}

		static void DateTimeTest()
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

		static void ReadGenericJsonObjectPropertiesAndValueV1()
		{
			string filepath = "./myfile.json";

			using (StreamReader r = new StreamReader(filepath))
			{
				var json = r.ReadToEnd();
				JObject jsonObj = JObject.Parse(json);

				foreach (JToken property in jsonObj["data"].Children())
				{
					Console.WriteLine("\nOBJECT HEADER");

					foreach (JToken abc in property.Values())
					{
						Console.WriteLine(abc.GetType() + "----" + abc.Type.ToString());

						if (abc.Type.ToString() == "Object")
						{
							foreach (JToken zzz in abc.Values())
							{
							Console.WriteLine("* {0}: {1} ---- {2}", zzz.Path, jsonObj.SelectToken(zzz.Path), jsonObj.SelectToken(zzz.Path).GetType());
							}
						}
						else
						{
							Console.WriteLine("{0}: {1} ---- {2}", abc.Path, jsonObj.SelectToken(abc.Path), jsonObj.SelectToken(abc.Path).GetType());
						}
					}
				}
			}
		}

		static void ReadGenericJsonObjectPropertiesAndValueV2()
		{
			string filepath = "./myfile.json";

			using (StreamReader r = new StreamReader(filepath))
			{
				var json = r.ReadToEnd();
				JObject jsonObj = JObject.Parse(json);

				jsonObj.Add("filename", "abc");
				Console.WriteLine(jsonObj["filename"]);

				foreach (JToken property in jsonObj["data"].Children())
				{	
					Console.WriteLine("\nOBJECT HEADER");
					ReadJsonObjectPropertiesAndValue(property, jsonObj);
				}
			}
		}

		static void ReadJsonObjectPropertiesAndValue(JToken token, JObject jsonObj)
		{
			if (jsonObj.SelectToken(token.Path).GetType().ToString() == "Newtonsoft.Json.Linq.JValue")
			{
				Console.WriteLine("{0}: {1}", token.Path, jsonObj.SelectToken(token.Path));
				return;
			}

			foreach (JToken obj in token.Values())
			{
				if (obj.Type.ToString() == "Object")
				{
					foreach (JToken innerObj in obj.Values())
					{
						ReadJsonObjectPropertiesAndValue(innerObj, jsonObj);
					}
				}
				else
				{
					ReadJsonObjectPropertiesAndValue(obj, jsonObj);
				}
			}
		}

		static void ReadObjectPropertiesAndValue(object obj, string propertyName = null)
		{
			if (obj.GetType().IsValueType || obj.GetType().Name == "String") {
				Console.WriteLine(propertyName);
				Console.WriteLine(obj);
				return;
			}

			foreach (PropertyInfo property in obj.GetType().GetProperties())
			{
				Console.WriteLine(property.Name);

				if (property.GetValue(obj, null).GetType().IsClass == true && property.GetValue(obj, null).GetType().GetInterface(nameof(ICollection)) != null)
				{
						foreach (object e in property.GetValue(obj, null) as IList)
						{
								ReadObjectPropertiesAndValue(e, property.Name);
						}
				}
				else
				{
						ReadObjectPropertiesAndValue(property.GetValue(obj, null), propertyName != null ? String.Concat(propertyName, ".", property.Name) : String.Concat(property.Name));
				}
			}
		}

		static void DataTableTest() {
			DataTable dt = new DataTable();
			dt.Columns.Add("Name");
			dt.Columns.Add("Marks");

			DataRow _row = dt.NewRow();
			_row["Name"] = "apple";
			_row["Marks"] = "500";

			dt.Rows.Add(_row);

			DataRow _row2 = dt.NewRow();
			_row2["Name"] = "orange";
			_row2["Marks"] = "200";
			dt.Rows.Add(_row2);

			foreach (DataRow r in dt.Rows) {
				Console.WriteLine(r["Name"]);
			}
		}
	}
}
