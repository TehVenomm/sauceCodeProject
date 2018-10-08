using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class SpeedTest_ForEach
{
	public SpeedTest_ForEach()
		: this()
	{
	}

	private void Awake()
	{
		Execute();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void Log(string log)
	{
		Naka.Log(log);
	}

	public void Execute()
	{
		Prepare();
		int[] array = new int[15]
		{
			100,
			100,
			1000,
			1500,
			2000,
			2500,
			3000,
			5000,
			10000,
			50000,
			100000,
			150000,
			500000,
			700000,
			1000000
		};
		foreach (int num in array)
		{
			Log($"===== [Count:{num}] =====");
			List<int> theList = new List<int>(Enumerable.Range(1, num));
			Stopwatch stopwatch = Stopwatch.StartNew();
			Sum_while(theList);
			stopwatch.Stop();
			Log($"while:\t\t\t\t\t\t\t{stopwatch.Elapsed}");
			stopwatch = Stopwatch.StartNew();
			Sum_foreach(theList);
			stopwatch.Stop();
			Log($"foreach:\t\t\t\t\t\t{stopwatch.Elapsed}");
			stopwatch = Stopwatch.StartNew();
			Sum_List_ForEach(theList);
			stopwatch.Stop();
			Log($"List.ForEach:\t\t\t\t{stopwatch.Elapsed}");
			stopwatch = Stopwatch.StartNew();
			Sum_List_ForEachLambda(theList);
			stopwatch.Stop();
			Log($"List.ForEachLambda:\t\t{stopwatch.Elapsed}");
			stopwatch = Stopwatch.StartNew();
			Sum_while(theList);
			stopwatch.Stop();
			Log($"while:\t\t\t\t\t\t\t{stopwatch.Elapsed}");
			stopwatch = Stopwatch.StartNew();
			Sum_List_ForEachLambda(theList);
			stopwatch.Stop();
			Log($"List.ForEachLambda:\t\t{stopwatch.Elapsed}");
			theList = null;
			GC.Collect();
		}
	}

	private void Prepare()
	{
		int result = 0;
		foreach (int item in new List<int>(Enumerable.Range(1, 1000)))
		{
			result += item;
		}
		result = 0;
		new List<int>(Enumerable.Range(1, 1000)).ForEach(delegate(int x)
		{
			result += x;
		});
	}

	private int Sum_foreach(List<int> theList)
	{
		int num = 0;
		foreach (int the in theList)
		{
			num += the;
		}
		return num;
	}

	private int Sum_while(List<int> theList)
	{
		int num = 0;
		List<int>.Enumerator enumerator = theList.GetEnumerator();
		while (enumerator.MoveNext())
		{
			num += enumerator.Current;
		}
		return num;
	}

	private int Sum_List_ForEach(List<int> theList)
	{
		int result = 0;
		theList.ForEach(delegate(int x)
		{
			result += x;
		});
		return result;
	}

	private int Sum_List_ForEachLambda(List<int> theList)
	{
		int result = 0;
		theList.ForEach(delegate(int x)
		{
			result += x;
		});
		return result;
	}
}
