using System;
using System.IO;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HelloWorld
{
  public class Methods
  {
		#region RunTestForExceptionCatch()
		public static string InnerMethod(string str)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(str))
				{
					throw new NotImplementedException("Null detected");
				}

				return str;
			}

			catch (ArgumentNullException)
			{
				throw;
			}
		}
		#endregion

		#region RunTestForIdGeneration()
		public static void OldIdGenerator()
		{
			StringBuilder id = new StringBuilder();

			//DateTime dt = DateTime.Now;
			DateTime dt = new DateTime(year: 2012, month: 1, day: 1);

			string date = dt.Date.ToString("yyyyMMdd");
			string sequence = "00001";

			id.Append(date);
			id.Append(sequence);

			if (id.Length != 13)
				throw new Exception();

			Console.WriteLine(id);
		}

		public static void NewIdGenerator()
		{
			StringBuilder id = new StringBuilder();

			//DateTime dt = DateTime.Now;
			DateTime dt = new DateTime(year: 2020, month: 12, day: 1);

			string date = dt.Date.ToString("yyMMdd");
			string sequence = "01000";
			string hash = "01";

			id.Append(date);
			id.Append(sequence);
			id.Append(hash);

			if (id.Length != 13)
				throw new Exception();

			Console.WriteLine(id);
		}
		#endregion

		#region RunTestForDelegateAndFunc()
		public static int Add(int a, int b)
		{
			return a + b;
		}

		public static int Multiple(int a, int b)
		{
			return a * b;
		}

		public static string PrintGrade(Person x)
		{
			return ((Student)x).Grade;
		}

		public static void Framework(Func<int, int, int> func)
		{
			Console.WriteLine(func(3, 10));
			Console.WriteLine(func(3, 10) + 1000000);
		}

		public static void ObjectFramework(Func<Person, string> func, string grade)
		{
			Console.WriteLine("Grade:");
			Console.WriteLine(func(new Student(grade)));
		}

		public static void ListReferenceTest(List<Student> list, Student student)
		{
			foreach (Student i in list)
			{
				i.Name = "Apple";
			}

			student.Name = "JOHNNY";
		}
		#endregion

		#region RunTestForReadingJson()
		public static void ReadGenericJsonObjectPropertiesAndValueV1()
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

		public static void ReadGenericJsonObjectPropertiesAndValueV2()
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

		public static void ReadJsonObjectPropertiesAndValue(JToken token, JObject jsonObj)
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

		public static void ReadObjectPropertiesAndValue(object obj, string propertyName = null)
		{
			if (obj.GetType().IsValueType || obj.GetType().Name == "String")
			{
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

		public static void DataTableTest()
		{
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

			foreach (DataRow r in dt.Rows)
			{
				Console.WriteLine(r["Name"]);
			}
		}
		#endregion
	}
}
