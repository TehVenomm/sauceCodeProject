// Decompiled with JetBrains decompiler
// Type: DropTargetMarkerManeger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DropTargetMarkerManeger : MonoBehaviourSingleton<DropTargetMarkerManeger>
{
  private InGameSettingsManager.DropMaker param;
  private Mesh drowMesh;
  private Material drowMaterial;
  private Quaternion defRot;
  private List<uint> targetIDList = new List<uint>();
  private List<uint> targetPortalIDList = new List<uint>();
  private List<DropTargetMarkerManeger.TargetInfo> targetList = new List<DropTargetMarkerManeger.TargetInfo>();
  private List<DropTargetMarkerManeger.TargetInfo> targetStockList = new List<DropTargetMarkerManeger.TargetInfo>();
  private List<DropTargetMarkerManeger.TargetInfo> delList = new List<DropTargetMarkerManeger.TargetInfo>();

  public bool active { get; set; }

  public static void Create()
  {
    if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      return;
    ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).gameObject.AddComponent<DropTargetMarkerManeger>();
  }

  protected override void Awake()
  {
    base.Awake();
    this.active = true;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      this.param = MonoBehaviourSingleton<InGameSettingsManager>.I.dropMaker;
      this.drowMesh = this.param.mesh;
      this.drowMaterial = this.param.material;
      this.defRot = Quaternion.Euler(this.param.rotOffset);
    }
    this.UpdateList();
    if (Object.op_Inequality((Object) this.drowMesh, (Object) null))
      return;
    this.drowMesh = new Mesh();
    this.drowMesh.vertices = new Vector3[4]
    {
      new Vector3(0.03f, 0.03f, 0.0f),
      new Vector3(-0.03f, -0.03f, 0.0f),
      new Vector3(-0.03f, 0.03f, 0.0f),
      new Vector3(0.03f, -0.03f, 0.0f)
    };
    this.drowMesh.uv = new Vector2[4]
    {
      new Vector2(1f, 1f),
      new Vector2(0.0f, 0.0f),
      new Vector2(0.0f, 1f),
      new Vector2(1f, 0.0f)
    };
    this.drowMesh.triangles = new int[6]{ 0, 1, 2, 3, 1, 0 };
    this.drowMesh.RecalculateNormals();
    this.drowMesh.RecalculateBounds();
  }

  public void UpdateList()
  {
    int index1 = 0;
    for (int count = this.targetList.Count; index1 < count; ++index1)
      this.targetStockList.Add(this.targetList[index1]);
    this.targetList.Clear();
    this.targetIDList.Clear();
    this.targetPortalIDList.Clear();
    Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    int mapId1 = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
    int index2 = 0;
    for (int length1 = deliveryList.Length; index2 < length1; ++index2)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) deliveryList[index2].dId);
      if (deliveryTableData != null && deliveryTableData.needs != null)
      {
        uint idx = 0;
        for (uint length2 = (uint) deliveryTableData.needs.Length; idx < length2; ++idx)
        {
          uint mapId2 = deliveryTableData.GetMapID(idx);
          if (mapId2 <= 0U || (long) mapId2 == (long) mapId1)
          {
            uint enemyId = deliveryTableData.GetEnemyID(idx);
            if (!this.targetIDList.Contains(enemyId) && (mapId2 != 0U || enemyId != 0U))
            {
              int have = 0;
              int need = 0;
              MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryList[index2].dId, out have, out need, idx);
              if (have < need)
                this.targetIDList.Add(enemyId);
            }
          }
        }
        uint index3 = 0;
        for (uint length3 = (uint) deliveryTableData.targetPortalID.Length; index3 < length3; ++index3)
        {
          uint num = (uint) deliveryTableData.targetPortalID[(int) index3];
          if (!this.targetPortalIDList.Contains(num) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryList[index2].dId))
            this.targetPortalIDList.Add(num);
        }
      }
    }
    List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
    int count1 = enemyList.Count;
    for (int index4 = 0; index4 < count1; ++index4)
      this.CheckTarget(enemyList[index4] as Enemy);
    if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList == null)
      return;
    int index5 = 0;
    for (int count2 = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; index5 < count2; ++index5)
      this.CheckTarget(MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[index5]);
  }

  private void _AddTargetInfo(Enemy enemy)
  {
    DropTargetMarkerManeger.TargetInfo targetInfo = (DropTargetMarkerManeger.TargetInfo) null;
    if (this.targetStockList.Count > 0)
    {
      targetInfo = this.targetStockList[0];
      this.targetStockList.RemoveAt(0);
    }
    if (targetInfo == null)
      targetInfo = new DropTargetMarkerManeger.TargetInfo();
    targetInfo.targetEnemy = enemy;
    targetInfo.target = enemy._transform;
    targetInfo.offset.y = enemy.uiHeight;
    if (enemy.enemyTableData != null)
      targetInfo.offset.y *= enemy.enemyTableData.modelScale;
    targetInfo.initOffset = true;
    this.targetList.Add(targetInfo);
  }

  private void _AddTargetInfo(PortalObject portal)
  {
    DropTargetMarkerManeger.TargetInfo targetInfo = (DropTargetMarkerManeger.TargetInfo) null;
    if (this.targetStockList.Count > 0)
    {
      targetInfo = this.targetStockList[0];
      this.targetStockList.RemoveAt(0);
    }
    if (targetInfo == null)
      targetInfo = new DropTargetMarkerManeger.TargetInfo();
    targetInfo.targetEnemy = (Enemy) null;
    targetInfo.target = portal._transform;
    targetInfo.offset.y = this.param.portalHeight;
    targetInfo.initOffset = true;
    this.targetList.Add(targetInfo);
  }

  public void CheckTarget(Enemy enemy)
  {
    if (!enemy.isInitialized)
      return;
    int index1 = 0;
    for (int count = this.targetList.Count; index1 < count; ++index1)
    {
      if (Object.op_Equality((Object) this.targetList[index1].target, (Object) enemy._transform))
        return;
    }
    bool flag = this.targetIDList.Contains(0U);
    int index2 = 0;
    for (int count = this.targetIDList.Count; index2 < count; ++index2)
    {
      if ((long) this.targetIDList[index2] == (long) enemy.enemyID || flag)
      {
        this._AddTargetInfo(enemy);
        break;
      }
    }
  }

  public void CheckTarget(PortalObject portal)
  {
    if (Object.op_Equality((Object) portal, (Object) null))
      return;
    int index1 = 0;
    for (int count = this.targetList.Count; index1 < count; ++index1)
    {
      if (Object.op_Equality((Object) this.targetList[index1].target, (Object) portal._transform))
        return;
    }
    int index2 = 0;
    for (int count = this.targetPortalIDList.Count; index2 < count; ++index2)
    {
      if ((int) this.targetPortalIDList[index2] == (int) portal.portalID)
      {
        this._AddTargetInfo(portal);
        break;
      }
    }
  }

  public void RemoveTarget(Transform target)
  {
    int index = 0;
    for (int count = this.targetList.Count; index < count; ++index)
    {
      if (!Object.op_Inequality((Object) this.targetList[index].target, (Object) target))
      {
        this.targetList[index].targetEnemy = (Enemy) null;
        this.targetStockList.Add(this.targetList[index]);
        this.targetList.Remove(this.targetList[index]);
        break;
      }
    }
  }

  private void Update()
  {
    if (!this.active)
      return;
    int index1 = 0;
    for (int count = this.targetList.Count; index1 < count; ++index1)
    {
      if (Object.op_Equality((Object) this.targetList[index1].target, (Object) null))
        this.delList.Add(this.targetList[index1]);
      else if (!((Component) this.targetList[index1].target).gameObject.activeSelf)
      {
        this.targetList[index1].active = false;
      }
      else
      {
        if (Object.op_Inequality((Object) this.targetList[index1].targetEnemy, (Object) null))
        {
          if (this.targetList[index1].targetEnemy.isHiding || this.targetList[index1].targetEnemy.isSummonAttack)
          {
            this.targetList[index1].active = false;
            continue;
          }
          if (Object.op_Inequality((Object) this.targetList[index1].targetEnemy.uiEnemyStatusGizmo, (Object) null))
          {
            this.targetList[index1].targetEnemy.uiEnemyStatusGizmo.SetTargetIcon(this.drowMaterial.mainTexture);
            this.targetList[index1].active = false;
            continue;
          }
        }
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(this.targetList[index1].target.position);
        if ((double) viewportPoint.x < -0.0 || (double) viewportPoint.x > 1.0 || (double) viewportPoint.y < -0.0 || (double) viewportPoint.y > 1.0)
        {
          this.targetList[index1].active = false;
        }
        else
        {
          if (!this.targetList[index1].initOffset)
          {
            this.targetList[index1].offset.y = this.CalcHight(this.targetList[index1].target);
            if ((double) this.targetList[index1].offset.y != 0.0)
              this.targetList[index1].initOffset = true;
          }
          this.targetList[index1].pos = Vector3.op_Addition(Vector3.op_Addition(this.targetList[index1].target.position, this.targetList[index1].offset), this.param.offset);
          this.targetList[index1].rot = Quaternion.op_Multiply(Quaternion.Euler(-MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles.x, 0.0f, 0.0f), this.defRot);
          this.targetList[index1].active = true;
        }
      }
    }
    int index2 = 0;
    for (int count = this.delList.Count; index2 < count; ++index2)
    {
      this.targetStockList.Add(this.delList[index2]);
      this.targetList.Remove(this.delList[index2]);
    }
    this.delList.Clear();
  }

  private float CalcHight(Transform target)
  {
    ((Component) target).GetComponentsInChildren<Collider>(Temporary.colliderList);
    float num1 = 0.0f;
    int index = 0;
    for (int count = Temporary.colliderList.Count; index < count; ++index)
    {
      Collider collider = Temporary.colliderList[index];
      Bounds bounds1 = collider.bounds;
      double y1 = (double) ((Bounds) ref bounds1).center.y;
      Bounds bounds2 = collider.bounds;
      double y2 = (double) ((Bounds) ref bounds2).size.y;
      float num2 = (float) (y1 + y2);
      if ((double) num1 < (double) num2)
        num1 = num2;
    }
    Temporary.colliderList.Clear();
    return num1;
  }

  public void OnPostRender()
  {
    if (!this.active || Object.op_Equality((Object) this.drowMaterial, (Object) null))
      return;
    bool flag = false;
    int index = 0;
    for (int count = this.targetList.Count; index < count; ++index)
    {
      if (this.targetList[index].active)
      {
        if (!flag)
        {
          this.drowMaterial.SetPass(0);
          flag = true;
        }
        Graphics.DrawMeshNow(this.drowMesh, this.targetList[index].pos, this.targetList[index].rot);
      }
    }
  }

  public class TargetInfo
  {
    public Enemy targetEnemy;
    public Transform target;
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 pos;
    public Quaternion rot;
    public bool initOffset;
    public bool active;
  }
}
