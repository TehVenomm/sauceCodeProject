// Decompiled with JetBrains decompiler
// Type: LoungeTableSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungeTableSet : MonoBehaviour
{
  public bool isInitialized { get; private set; }

  public List<TablePoint> tablePoints { get; private set; }

  public List<ChairPoint> chairSitPoints { get; private set; }

  public ChairPoint GetNearSitPoint(Vector3 pos)
  {
    float num1 = float.MaxValue;
    ChairPoint nearSitPoint = (ChairPoint) null;
    Vector3 vector3 = pos;
    for (int index = 0; index < this.chairSitPoints.Count; ++index)
    {
      float num2 = Vector3.Distance(vector3, ((Component) this.chairSitPoints[index]).transform.position);
      if ((double) num1 > (double) num2)
      {
        nearSitPoint = this.chairSitPoints[index];
        num1 = num2;
      }
    }
    return nearSitPoint;
  }

  public TablePoint GetNearTablePoint()
  {
    float num1 = float.MaxValue;
    TablePoint nearTablePoint = (TablePoint) null;
    Vector3 vector3 = !MonoBehaviourSingleton<LoungeManager>.IsValid() ? ((Component) MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara).transform.position : ((Component) MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara).transform.position;
    for (int index = 0; index < this.tablePoints.Count; ++index)
    {
      float num2 = Vector3.Distance(vector3, ((Component) this.tablePoints[index]).transform.position);
      if ((double) num1 > (double) num2)
      {
        nearTablePoint = this.tablePoints[index];
        num1 = num2;
      }
    }
    return nearTablePoint;
  }

  private IEnumerator Start()
  {
    yield return (object) this.StartCoroutine(this.CreateTable());
    yield return (object) this.StartCoroutine(this.CreateChair());
    this.isInitialized = true;
  }

  private IEnumerator CreateTable()
  {
    string str = !MonoBehaviourSingleton<LoungeManager>.IsValid() ? "ClanTablePoints" : "LoungeTablePoints";
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadTablePoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
    {
      str
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes(loadTablePoints.loadedObject, ((Component) this).transform);
    this.tablePoints = new List<TablePoint>(2);
    Predicate<Transform> callback = (Predicate<Transform>) (o =>
    {
      if (Object.op_Inequality((Object) ((Component) o).GetComponent<TablePoint>(), (Object) null))
        this.tablePoints.Add(((Component) o).GetComponent<TablePoint>());
      return false;
    });
    Utility.ForEach(transform, callback);
  }

  private IEnumerator CreateChair()
  {
    string str = !MonoBehaviourSingleton<LoungeManager>.IsValid() ? "ClanChairPoints" : "LoungeChairPoints";
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadChairPoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
    {
      str
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes(loadChairPoints.loadedObject, ((Component) this).transform);
    this.chairSitPoints = new List<ChairPoint>(8);
    Predicate<Transform> callback = (Predicate<Transform>) (o =>
    {
      if (((Object) o).name.StartsWith("SIT"))
        this.chairSitPoints.Add(((Component) o).GetComponent<ChairPoint>());
      return false;
    });
    Utility.ForEach(transform, callback);
  }
}
