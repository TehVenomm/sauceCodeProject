// Decompiled with JetBrains decompiler
// Type: MultiThreadTaskRunner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

#nullable disable
public class MultiThreadTaskRunner
{
  private Thread[] threads;
  private volatile List<MultiThreadTaskRunner.TaskParam> tasks = new List<MultiThreadTaskRunner.TaskParam>();
  private volatile object lockObject = new object();
  private volatile int workingCount;
  private volatile bool stopAllThreads;
  private const int THREAD_COUNT = 3;

  public bool isWorking
  {
    get
    {
      if (this.threads == null)
        return false;
      lock (this.lockObject)
        return this.tasks.Count > 0 || this.workingCount != 0;
    }
  }

  public void CreateThread()
  {
    if (this.threads != null)
      return;
    this.workingCount = 0;
    this.stopAllThreads = false;
    int length = 3;
    this.threads = new Thread[length];
    for (int index = 0; index < length; ++index)
    {
      this.threads[index] = new Thread(new ThreadStart(this.Worker));
      this.threads[index].IsBackground = true;
      this.threads[index].Start();
    }
  }

  public void DestroyThread()
  {
    if (this.threads == null)
      return;
    this.stopAllThreads = true;
    int index = 0;
    for (int length = this.threads.Length; index < length; ++index)
      this.threads[index].Join();
    this.threads = (Thread[]) null;
  }

  private void Worker()
  {
    try
    {
      MultiThreadTaskRunner.TaskParam taskParam = (MultiThreadTaskRunner.TaskParam) null;
      while (!this.stopAllThreads)
      {
        if (this.tasks.Count > 0 || taskParam != null)
        {
          lock (this.lockObject)
          {
            if (taskParam != null)
            {
              --this.workingCount;
              taskParam = (MultiThreadTaskRunner.TaskParam) null;
            }
            if (this.tasks.Count > 0)
            {
              taskParam = this.tasks[0];
              this.tasks.RemoveAt(0);
              ++this.workingCount;
            }
          }
          if (taskParam != null && taskParam.act != null)
            taskParam.act();
        }
      }
    }
    catch (Exception ex)
    {
      if (ex is ThreadAbortException)
        return;
      Debug.LogError((object) ("MultiThreadTaskRunner : " + ex.ToString()));
    }
  }

  public void Add(string name, System.Action act)
  {
    if (this.threads == null)
      return;
    MultiThreadTaskRunner.TaskParam taskParam = new MultiThreadTaskRunner.TaskParam();
    taskParam.name = name;
    taskParam.act = act;
    lock (this.lockObject)
      this.tasks.Add(taskParam);
  }

  public void ChangePriorityTop(string name)
  {
    lock (this.lockObject)
    {
      MultiThreadTaskRunner.TaskParam taskParam = this.tasks.Find((Predicate<MultiThreadTaskRunner.TaskParam>) (o => o.name == name));
      if (taskParam == null)
        return;
      this.tasks.Remove(taskParam);
      this.tasks.Insert(0, taskParam);
    }
  }

  private class TaskParam
  {
    public string name = "";
    public System.Action act;
  }
}
