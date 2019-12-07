using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MultiThreadTaskRunner
{
	private class TaskParam
	{
		public string name = "";

		public Action act;
	}

	private Thread[] threads;

	private volatile List<TaskParam> tasks = new List<TaskParam>();

	private volatile object lockObject = new object();

	private volatile int workingCount;

	private volatile bool stopAllThreads;

	private const int THREAD_COUNT = 1;

	public bool isWorking
	{
		get
		{
			if (threads == null)
			{
				return false;
			}
			bool flag = false;
			lock (lockObject)
			{
				if (tasks.Count > 0)
				{
					return true;
				}
				return workingCount != 0;
			}
		}
	}

	public void CreateThread()
	{
		if (threads == null)
		{
			workingCount = 0;
			stopAllThreads = false;
			int num = 1;
			threads = new Thread[num];
			for (int i = 0; i < num; i++)
			{
				threads[i] = new Thread(Worker);
				threads[i].IsBackground = true;
				threads[i].Start();
			}
		}
	}

	public void DestroyThread()
	{
		if (threads != null)
		{
			stopAllThreads = true;
			int i = 0;
			for (int num = threads.Length; i < num; i++)
			{
				threads[i].Join();
			}
			threads = null;
		}
	}

	private void Worker()
	{
		try
		{
			TaskParam taskParam = null;
			while (!stopAllThreads)
			{
				if (tasks.Count > 0 || taskParam != null)
				{
					lock (lockObject)
					{
						if (taskParam != null)
						{
							workingCount--;
							taskParam = null;
						}
						if (tasks.Count > 0)
						{
							taskParam = tasks[0];
							tasks.RemoveAt(0);
							workingCount++;
						}
					}
					if (taskParam != null && taskParam.act != null)
					{
						taskParam.act();
					}
				}
			}
		}
		catch (Exception ex)
		{
			if (!(ex is ThreadAbortException))
			{
				Debug.LogError("MultiThreadTaskRunner : " + ex.ToString());
			}
		}
	}

	public void Add(string name, Action act)
	{
		if (threads != null)
		{
			TaskParam taskParam = new TaskParam();
			taskParam.name = name;
			taskParam.act = act;
			lock (lockObject)
			{
				tasks.Add(taskParam);
			}
		}
	}

	public void ChangePriorityTop(string name)
	{
		lock (lockObject)
		{
			TaskParam taskParam = tasks.Find((TaskParam o) => o.name == name);
			if (taskParam != null)
			{
				tasks.Remove(taskParam);
				tasks.Insert(0, taskParam);
			}
		}
	}
}
