// Decompiled with JetBrains decompiler
// Type: SpeedTest_ForEach
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

#nullable disable
public class SpeedTest_ForEach : MonoBehaviour
{
  private void Awake() => this.Execute();

  private void Start()
  {
  }

  private void Update()
  {
  }

  private void Log(string log) => Naka.Log(log);

  public void Execute()
  {
    this.Prepare();
    int[] numArray = new int[15]
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
    foreach (int count in numArray)
    {
      this.Log($"===== [Count:{count}] =====");
      List<int> theList = new List<int>(Enumerable.Range(1, count));
      Stopwatch stopwatch1 = Stopwatch.StartNew();
      this.Sum_while(theList);
      stopwatch1.Stop();
      this.Log($"while:\t\t\t\t\t\t\t{stopwatch1.Elapsed}");
      Stopwatch stopwatch2 = Stopwatch.StartNew();
      this.Sum_foreach(theList);
      stopwatch2.Stop();
      this.Log($"foreach:\t\t\t\t\t\t{stopwatch2.Elapsed}");
      Stopwatch stopwatch3 = Stopwatch.StartNew();
      this.Sum_List_ForEach(theList);
      stopwatch3.Stop();
      this.Log($"List.ForEach:\t\t\t\t{stopwatch3.Elapsed}");
      Stopwatch stopwatch4 = Stopwatch.StartNew();
      this.Sum_List_ForEachLambda(theList);
      stopwatch4.Stop();
      this.Log($"List.ForEachLambda:\t\t{stopwatch4.Elapsed}");
      Stopwatch stopwatch5 = Stopwatch.StartNew();
      this.Sum_while(theList);
      stopwatch5.Stop();
      this.Log($"while:\t\t\t\t\t\t\t{stopwatch5.Elapsed}");
      Stopwatch stopwatch6 = Stopwatch.StartNew();
      this.Sum_List_ForEachLambda(theList);
      stopwatch6.Stop();
      this.Log($"List.ForEachLambda:\t\t{stopwatch6.Elapsed}");
      GC.Collect();
    }
  }

  private void Prepare()
  {
    int result = 0;
    foreach (int num in new List<int>(Enumerable.Range(1, 1000)))
      result += num;
    result = 0;
    new List<int>(Enumerable.Range(1, 1000)).ForEach((Action<int>) (x => result += x));
  }

  private int Sum_foreach(List<int> theList)
  {
    int num = 0;
    foreach (int the in theList)
      num += the;
    return num;
  }

  private int Sum_while(List<int> theList)
  {
    int num = 0;
    List<int>.Enumerator enumerator = theList.GetEnumerator();
    while (enumerator.MoveNext())
      num += enumerator.Current;
    return num;
  }

  private int Sum_List_ForEach(List<int> theList)
  {
    int result = 0;
    theList.ForEach((Action<int>) (x => result += x));
    return result;
  }

  private int Sum_List_ForEachLambda(List<int> theList)
  {
    int result = 0;
    theList.ForEach((Action<int>) (x => result += x));
    return result;
  }
}
