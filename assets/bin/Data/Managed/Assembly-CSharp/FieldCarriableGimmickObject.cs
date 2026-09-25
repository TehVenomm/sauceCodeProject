// Decompiled with JetBrains decompiler
// Type: FieldCarriableGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldCarriableGimmickObject : FieldGimmickObject
{
  public static readonly string kCarryMarkerName = "ef_btl_target_lift_01";
  public static readonly string kEvolveMarkerName = "ef_btl_target_levelup_01";
  public static readonly string kEvolveEffectName = "ef_btl_trap_01_01";
  public static readonly int kEvolveSEId = 10000075;
  public static readonly Vector3 kCarryOffset = new Vector3(0.0f, 0.0f, 1.86f);
  public static readonly string kCarryNode = "R_Wep";
  private static readonly string kShadowNode = "shadow01";
  private static readonly float kRadius = 2f;
  private Transform parentTrans;
  private Transform carryMarkerTrans;
  private Transform evolveMarkerTrans;
  private Transform shadowTrans;
  protected int modelIndex;
  protected int[] modelIndexes;
  protected bool hasDeploied;
  protected int currentLv;
  protected int maxLv;

  public bool isCarrying { get; protected set; }

  protected override void Awake()
  {
    base.Awake();
    this.modelIndexes = new int[1];
  }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.parentTrans = this.GetTransform().parent;
    this.isCarrying = false;
    if (this.modelIndexes.Length != 0)
      this.modelIndex = this.modelIndexes[0];
    ((Component) this).gameObject.SetActive(false);
    ((Component) this.GetTransform()).gameObject.SetActive(false);
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    List<int> intList = new List<int>();
    intList.Add(0);
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        if (strArray[0].StartsWith("mi"))
        {
          if (strArray[0] == "mi0")
            intList[0] = int.Parse(strArray[1]);
          else
            intList.Add(int.Parse(strArray[1]));
        }
        if (strArray[0] == "ml")
          this.maxLv = Mathf.Max(0, int.Parse(strArray[1]) - 1);
      }
    }
    this.modelIndexes = intList.ToArray();
    intList.Clear();
  }

  public override void RequestDestroy()
  {
    base.RequestDestroy();
    MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, (IFieldGimmickObject) this);
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction(this.GetTargetActionId()))
    {
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this.GetTransform().position);
      Vector3 pos = Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this.GetTransform().position);
      if (self.carryingGimmickObject is FieldCarriableEvolveItemGimmickObject && this.CanEvolve())
      {
        if (Object.op_Equality((Object) this.evolveMarkerTrans, (Object) null))
          this.evolveMarkerTrans = EffectManager.GetEffect(this.GetEvolveMarkerName(), this.GetTransform());
        if (Object.op_Inequality((Object) this.evolveMarkerTrans, (Object) null))
          this.evolveMarkerTrans.Set(pos, rotation);
        if (!Object.op_Inequality((Object) this.carryMarkerTrans, (Object) null))
          return;
        EffectManager.ReleaseEffect(((Component) this.carryMarkerTrans).gameObject);
        this.carryMarkerTrans = (Transform) null;
        return;
      }
      if (!self.IsCarrying() && this.CanCarry())
      {
        if (Object.op_Equality((Object) this.carryMarkerTrans, (Object) null))
          this.carryMarkerTrans = EffectManager.GetEffect(this.GetCarryMarkerName(), this.GetTransform());
        if (Object.op_Inequality((Object) this.carryMarkerTrans, (Object) null))
          this.carryMarkerTrans.Set(pos, rotation);
        if (!Object.op_Inequality((Object) this.evolveMarkerTrans, (Object) null))
          return;
        EffectManager.ReleaseEffect(((Component) this.evolveMarkerTrans).gameObject);
        this.evolveMarkerTrans = (Transform) null;
        return;
      }
    }
    if (Object.op_Inequality((Object) this.carryMarkerTrans, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.carryMarkerTrans).gameObject);
      this.carryMarkerTrans = (Transform) null;
    }
    if (!Object.op_Inequality((Object) this.evolveMarkerTrans, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.evolveMarkerTrans).gameObject);
    this.evolveMarkerTrans = (Transform) null;
  }

  public override string GetObjectName() => "CarriableGimmick";

  public override bool IsSearchableNearest() => this.CanCarry();

  public override float GetTargetRadius() => FieldCarriableGimmickObject.kRadius;

  public override float GetTargetSqrRadius()
  {
    return FieldCarriableGimmickObject.kRadius * FieldCarriableGimmickObject.kRadius;
  }

  protected override void CreateModel()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldGimmickObject.ConvertModelIndexToKey(this.m_gimmickType, this.modelIndex));
    if (loadObject == null)
      return;
    this.modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.m_transform);
    this.shadowTrans = Utility.FindChild(this.modelTrans, FieldCarriableGimmickObject.kShadowNode);
  }

  public virtual Character.ACTION_ID GetTargetActionId() => (Character.ACTION_ID) 44;

  public virtual string GetCarryMarkerName() => FieldCarriableGimmickObject.kCarryMarkerName;

  public virtual string GetEvolveMarkerName() => FieldCarriableGimmickObject.kEvolveMarkerName;

  public virtual bool CanCarry() => ((Component) this).gameObject.activeSelf && !this.isCarrying;

  public virtual bool CanEvolve()
  {
    return this.currentLv < this.maxLv && this.currentLv < this.modelIndexes.Length - 1;
  }

  public virtual bool HasDeploied() => this.hasDeploied;

  public void StartCarry(Player player)
  {
    this.isCarrying = true;
    if (Object.op_Inequality((Object) this.shadowTrans, (Object) null))
      ((Component) this.shadowTrans).gameObject.SetActive(false);
    this.OnStartCarry(player);
  }

  public void EndCarry()
  {
    this.GetTransform().SetParent(this.parentTrans);
    Vector3 localPosition = this.GetTransform().localPosition;
    localPosition.y = 0.0f;
    this.GetTransform().localPosition = localPosition;
    Quaternion localRotation = this.GetTransform().localRotation;
    localRotation.x = 0.0f;
    localRotation.z = 0.0f;
    this.GetTransform().localRotation = localRotation;
    if (!this.hasDeploied)
    {
      if (this.IsDefenseTool() && MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.CountDeploiedCarriableGimmick();
      this.hasDeploied = true;
    }
    if (Object.op_Inequality((Object) this.shadowTrans, (Object) null))
      ((Component) this.shadowTrans).gameObject.SetActive(true);
    this.isCarrying = false;
    this.OnEndCarry();
  }

  protected virtual void OnStartCarry(Player owner)
  {
  }

  protected virtual void OnEndCarry()
  {
  }

  protected virtual bool IsDefenseTool() => true;

  public void Evolve()
  {
    if (!this.CanEvolve())
      return;
    Object.Destroy((Object) ((Component) this.modelTrans).gameObject);
    ++this.currentLv;
    this.modelIndex = this.modelIndexes[this.currentLv];
    this.CreateModel();
    this.OnEvolved();
  }

  protected virtual void OnEvolved()
  {
    EffectManager.OneShot(FieldCarriableGimmickObject.kEvolveEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableGimmickObject.kEvolveSEId, this.GetTransform().position);
  }

  public static List<int> GetModelIndexes(string value2)
  {
    List<int> modelIndexes = new List<int>();
    modelIndexes.Add(0);
    if (!value2.IsNullOrWhiteSpace())
    {
      string str1 = value2;
      char[] chArray1 = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ ':' };
        string[] strArray = str2.Split(chArray2);
        if (strArray != null && strArray.Length == 2 && strArray[0].StartsWith("mi"))
        {
          if (strArray[0] == "mi0")
            modelIndexes[0] = int.Parse(strArray[1]);
          else
            modelIndexes.Add(int.Parse(strArray[1]));
        }
      }
    }
    return modelIndexes;
  }

  public void SetCarriableGimmickInfo(
    Coop_Model_StageInfo.FieldCarriableGimmickInfo info)
  {
    this.GetTransform().position = info.position;
    ((Component) this).gameObject.SetActive(info.enable);
    if (this.currentLv != info.currentLv)
    {
      Object.Destroy((Object) ((Component) this.modelTrans).gameObject);
      this.modelIndex = this.modelIndexes[info.currentLv];
      this.CreateModel();
    }
    this.currentLv = info.currentLv;
  }

  public Coop_Model_StageInfo.FieldCarriableGimmickInfo GetCarriableGimmickInfo()
  {
    return new Coop_Model_StageInfo.FieldCarriableGimmickInfo()
    {
      pointId = this.GetId(),
      position = this.GetTransform().position,
      enable = ((Component) this).gameObject.activeSelf,
      currentLv = this.currentLv
    };
  }
}
