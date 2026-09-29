// Decompiled with JetBrains decompiler
// Type: ItemDetailDistanceDecayMiniDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailDistanceDecayMiniDialog : GameSection
{
  private const float GRAPH_HEIGHT = 150f;
  private const float GRAPH_WIDTH = 300f;
  private const float MAX_RATE = 1f;
  private const float MAX_DISTANCE = 30f;
  private GameObject circlePrefab;
  private GameObject barPrefab;
  private Transform startPoint;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "DamageDistanceTable";
    }
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject distanceCircleObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, "DistanceCircle");
    LoadObject distanceBarObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, "DistanceBar");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.circlePrefab = distanceCircleObject.loadedObject as GameObject;
    this.barPrefab = distanceBarObject.loadedObject as GameObject;
    this.startPoint = this.GetCtrl((Enum) ItemDetailDistanceDecayMiniDialog.UI.GRAPH_START);
    this.CreateGraph((uint) (int) (GameSection.GetEventData() as object[])[0]);
    base.Initialize();
  }

  private void CreateGraph(uint id)
  {
    DamageDistanceTable.DamageDistanceData data = Singleton<DamageDistanceTable>.I.GetData(id);
    for (int index = 0; index < data.points.Length; ++index)
    {
      DamageDistanceTable.DamagePoint point = data.points[index];
      this.CreatePoint(point);
      int num = index >= data.points.Length - 1 ? 1 : 0;
      DamageDistanceTable.DamagePoint damagePoint = (DamageDistanceTable.DamagePoint) null;
      if (num == 0)
        damagePoint = data.points[index + 1];
      else if ((double) (float) point.distance < 30.0)
      {
        damagePoint = new DamageDistanceTable.DamagePoint();
        damagePoint.distance = (XorFloat) 30f;
        damagePoint.rate = point.rate;
      }
      if (damagePoint != null)
      {
        this.CreatePoint(damagePoint);
        this.CreateBar(point, damagePoint);
      }
    }
  }

  private void CreatePoint(DamageDistanceTable.DamagePoint point)
  {
    ((Component) ResourceUtility.Realizes((Object) this.circlePrefab, this.startPoint, 5)).transform.localPosition = this.GetPos(point);
  }

  private void CreateBar(
    DamageDistanceTable.DamagePoint point,
    DamageDistanceTable.DamagePoint nextPoint)
  {
    Vector3 pos1 = this.GetPos(point);
    Vector3 pos2 = this.GetPos(nextPoint);
    float angle = this.GetAngle(pos1, pos2);
    float num = Vector3.Distance(pos1, pos2);
    Transform transform = ResourceUtility.Realizes((Object) this.barPrefab, this.startPoint, 5);
    ((Component) transform).transform.localPosition = pos1;
    ((Component) transform).transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    ((Component) transform).GetComponent<UISprite>().width = (int) num;
  }

  private Vector3 GetPos(DamageDistanceTable.DamagePoint point)
  {
    return new Vector3((float) ((double) (float) point.distance / 30.0 * 300.0), (float) ((double) (float) point.rate / 1.0 * 150.0), 0.0f);
  }

  private float GetAngle(Vector3 pos, Vector3 next)
  {
    Vector3 vector3 = Vector3.op_Subtraction(next, pos);
    Vector3 normalized = ((Vector3) ref vector3).normalized;
    float num = Vector3.Angle(Vector3.right, normalized);
    return (double) Vector3.Cross(Vector3.right, normalized).z < 0.0 ? -num : num;
  }

  public override void UpdateUI() => base.UpdateUI();

  private enum UI
  {
    GRAPH_START,
  }
}
