using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwoTasksProj
{
	class MainClass
	{
		public static int GetFileData() {
			Thread.Sleep (2000);
			return 33;
		}

		public static int Analyze(int i) {
			return i + 1;
		}

		public static string Summarize(int j) {
			return (j + 1).ToString();
		}

		private static int Sum(CancellationToken ct, int n) {
			for (;;) {
				ct.ThrowIfCancellationRequested();
				Thread.Sleep (1000);
				Console.WriteLine (n++);
				if (n == 1500)
					break;
			}
			return n;
		}

		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			/*Task.Factory.StartNew(() => Console.WriteLine("Hello from task."));
			Task<double> taskRetVal = Task<double>.Factory.StartNew(() => {return 1.44;});
			Console.WriteLine (taskRetVal.Result);*/

			//////////////////////////////////////////////////
			/*var task1 = Task.Factory.StartNew(() => {
				throw new Exception("I'm bad, but not too bad!");
			});
			try {
				task1.Wait();
			}
			catch (AggregateException ae) {
				foreach (var e in ae.InnerExceptions){
					if (e is Exception) {
						Console.WriteLine (e.Message);
					}
				}
			}*/

			///////////////////////////////////////////////////////
			/*Task<string> reportData2 = Task.Factory.StartNew(() => GetFileData())
				.ContinueWith((x) => Analyze(x.Result))
				.ContinueWith((y) => Summarize(y.Result));

			Console.WriteLine ("waiting");
			Console.WriteLine (reportData2.Result);*/

			///////////////////////////////////////////////////////////
			/*Task[] tasks = new Task[3] {
				Task.Factory.StartNew (() => {Thread.Sleep (2000); Console.WriteLine("t1");}),
				Task.Factory.StartNew (() => {Thread.Sleep (3000); Console.WriteLine("t2");}),
				Task.Factory.StartNew (() => {Thread.Sleep (4000); Console.WriteLine("t3");})
			};
			Task.WaitAny (tasks);
			Console.WriteLine ("not waiting");*/

			///////////////////////////////////////////////////////////////
			/*var parent = Task.Factory.StartNew(() => {
				Console.WriteLine("Parent task beginning.");
				Task.Factory.StartNew(() => {
					Thread.Sleep(2000); Console.WriteLine("Child completed.");
				}, TaskCreationOptions.AttachedToParent);
			});
			parent.Wait();
			Console.WriteLine("Parent task completed.");*/

			////////////////////////////////////////////////////////////////
			CancellationTokenSource cts = new CancellationTokenSource();
			Task<int> t = new Task<int>(() => Sum(cts.Token, 1000), cts.Token);
			t.Start();
			Thread.Sleep (3000);
			cts.Cancel();
			try {
				// po anulowaniu Result rzuci AggregateException
				Console.WriteLine("The sum is: " + t.Result);
			} catch (AggregateException ae) {
				ae.Handle(e => e is OperationCanceledException);
				Console.WriteLine("Sum was canceled");
			}
		}
	}
}
