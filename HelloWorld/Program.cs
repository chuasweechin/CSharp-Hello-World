using System;
using System.IO;
using Newtonsoft.Json;
using Confluent.Kafka;
using System.Threading;
using System.Collections.Generic;

namespace HelloWorld
{
	class Program
	{
		delegate int DelegateTest(int i, int z);

		static void Main(string[] args)
		{
			KafkaLongRunningProcessTest();
		}

		static void KafkaLongRunningProcessTest()
		{
			var topic = "SampleEvent";

			var conf = new ConsumerConfig
			{
				GroupId = "sample-event-consumer-group-2",
				BootstrapServers = "localhost:9092",
				AutoOffsetReset = AutoOffsetReset.Earliest,
				EnableAutoCommit = true, 
				EnableAutoOffsetStore = false
			};

			using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
			{
				consumer.Subscribe(topic);

				CancellationTokenSource cts = new CancellationTokenSource();

				Console.CancelKeyPress += (_, e) => {
					e.Cancel = true; 
					cts.Cancel();
				};

				try
				{
					while (true)
					{
						try
						{
							var result = consumer.Consume(cts.Token);

							Console.WriteLine(
								$"Consumed message '{ result.Message.Value }' at: { DateTime.Now }'."
							);

							// Test 100 seconds - works
							// Test 200 seconds - works
							Thread.Sleep(TimeSpan.FromMilliseconds(200000));
							consumer.StoreOffset(result);

							Console.WriteLine($"Commit: { DateTime.Now }");
						}
						catch (ConsumeException ex)
						{
							Console.WriteLine($"Error occured: { ex.Error.Reason }");
						}
						catch (TopicPartitionException ex)
						{
							Console.WriteLine($"Commit error: {ex.Error.Reason}");
						}
						catch (KafkaException ex)
						{
							Console.WriteLine($"Commit error: {ex.Error.Reason}");
						}
						catch (Exception ex)
						{
							Console.WriteLine($"General error: {ex.Message}");
						}
					}
				}
				catch (OperationCanceledException)
				{
					// Ensure the consumer leaves the group cleanly and final offsets are committed.
					consumer.Close();
				}
				finally
				{
					consumer.Close();
				}
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
