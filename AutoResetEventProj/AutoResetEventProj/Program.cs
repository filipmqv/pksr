using System;
using System.Threading;

class MainClass
{
	static void Main()
	{
		int n = 2;
		AutoResetEvent[] waitHandles = new AutoResetEvent[n];
		for (int i = 0; i < n; i++) {
			waitHandles[i] = new AutoResetEvent(false);
			Console.WriteLine (i);
		};

		for (int i = 0; i < n; i++) {
			Console.WriteLine (i);
			int temp = i;
			Thread t = new Thread(() => ThreadProc2(waitHandles[temp]));
			t.Name = "Thread_" + i;
			t.Start();
		};


		Console.WriteLine ("waiting");
		WaitHandle.WaitAll (waitHandles);
		Console.WriteLine ("Done");


	}

	static void ThreadProc2(AutoResetEvent ev)
	{
		string name = Thread.CurrentThread.Name;

		Thread.Sleep(250);

		Console.WriteLine("{0} sets AutoResetEvent #1.", name);
		ev.Set();
		Console.WriteLine("{0} AutoResetEvent #1 has been set.", name);
	}


}