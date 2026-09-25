// Decompiled with JetBrains decompiler
// Type: FieldSupplyGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldSupplyGimmickObject : FieldGimmickObject
{
  public static readonly string kSupplyMarkerName = "ef_btl_target_unknown_01";
  public static readonly string kBreakEffectName = "ef_btl_bg_woodbreak_01";
  public static readonly int kBreakSEId = 20000039;
  private static readonly int kShiftIndex = 1000;
  private static readonly int kIntervalFrameForObserve = 30;
  private static readonly float kRadius = 2f;
  public int modelIndex;
  protected float coolTime;
  protected float maxCoolTime = 3f;
  protected List<uint> supplyGimmickIds;
  protected int suppliedCount;
  protected bool canUse;
  protected Transform targetMarkerTrans;
  protected bool isHost;
  protected List<FieldSupplyGimmickObject.ActiveCondition> activeConditions;

  protected override void Awake()
  {
    base.Awake();
    this.supplyGimmickIds = new List<uint>();
    this.activeConditions = new List<FieldSupplyGimmickObject.ActiveCondition>();
  }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    ((Component) this.modelTrans).gameObject.SetActive(false);
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    int result1 = 1;
    uint result2 = 0;
    string[] strArray1 = value2.Split(',');
    for (int index = 0; index < strArray1.Length; ++index)
    {
      string[] strArray2 = strArray1[index].Split(':');
      if (strArray2 != null && strArray2.Length == 2)
      {
        switch (strArray2[0])
        {
          case "ct":
            float.TryParse(strArray2[1], out this.maxCoolTime);
            continue;
          case "mi":
            int.TryParse(strArray2[1], out this.modelIndex);
            continue;
          case "n":
            int.TryParse(strArray2[1], out result1);
            continue;
          case "id":
            uint.TryParse(strArray2[1], out result2);
            continue;
          default:
            continue;
        }
      }
    }
    for (int index = 0; index < result1; ++index)
    {
      this.activeConditions.Add(new FieldSupplyGimmickObject.ActiveCondition());
      this.supplyGimmickIds.Add(result2);
    }
    string[] strArray3 = new string[5]
    {
      "id",
      "hp",
      "de",
      "et",
      "gn"
    };
    for (int index1 = 0; index1 < strArray1.Length; ++index1)
    {
      string[] strArray4 = strArray1[index1].Split(':');
      if (strArray4 != null && strArray4.Length == 2)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          int result3 = 0;
          if (strArray4[0].StartsWith(strArray3[index2]) && int.TryParse(strArray4[0].Replace(strArray3[index2], ""), out result3) && result3 < result1)
          {
            switch (strArray3[index2])
            {
              case "id":
                this.supplyGimmickIds[result3] = uint.Parse(strArray4[1]);
                continue;
              case "hp":
                this.activeConditions[result3].maxDefenseTargetHp = float.Parse(strArray4[1]);
                continue;
              case "de":
                this.activeConditions[result3].minDestroiedEnemyCount = int.Parse(strArray4[1]);
                continue;
              case "et":
                this.activeConditions[result3].minElapsedTime = float.Parse(strArray4[1]);
                continue;
              case "gn":
                this.activeConditions[result3].minDeploiedGimmickNum = int.Parse(strArray4[1]);
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
  }

  protected override void CreateModel()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldSupplyGimmickObject.ConvertModelIndexToKey(this.modelIndex));
    if (loadObject == null)
      return;
    this.modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.m_transform);
  }

  public static uint ConvertModelIndexToKey(int idx)
  {
    return (uint) (idx * FieldSupplyGimmickObject.kShiftIndex + 20);
  }

  public static string ConvertModelIndexToName(int idx) => $"CMN_supply{idx + 1:D2}";

  public static int GetModelIndex(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return 0;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "mi")
        return int.Parse(strArray[1]);
    }
    return 0;
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (this.CanUse() & isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 44))
    {
      if (Object.op_Equality((Object) this.targetMarkerTrans, (Object) null))
        this.targetMarkerTrans = EffectManager.GetEffect(FieldSupplyGimmickObject.kSupplyMarkerName, this.GetTransform());
      if (!Object.op_Inequality((Object) this.targetMarkerTrans, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this.GetTransform().position);
      this.targetMarkerTrans.Set(Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3).normalized, FieldSupplyGimmickObject.kRadius), Vector3.up), this.GetTransform().position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetMarkerTrans, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetMarkerTrans).gameObject);
    }
  }

  private void LateUpdate()
  {
    if ((double) this.coolTime > 0.0)
    {
      this.coolTime -= Time.deltaTime;
      this.coolTime = Mathf.Max(0.0f, this.coolTime);
    }
    if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && Time.frameCount % FieldSupplyGimmickObject.kIntervalFrameForObserve == 0)
    {
      FieldCarriableGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, (int) this.supplyGimmickIds[this.suppliedCount]) as FieldCarriableGimmickObject;
      this.canUse = (double) this.coolTime <= 0.0 && this.supplyGimmickIds.Count > this.suppliedCount && Object.op_Inequality((Object) fieldGimmickObj, (Object) null) && !((Component) fieldGimmickObj).gameObject.activeSelf && (double) MonoBehaviourSingleton<StageObjectManager>.I.GetWaveMatchTargetHpRate() <= (double) this.activeConditions[this.suppliedCount].maxDefenseTargetHp && MonoBehaviourSingleton<InGameProgress>.I.partyDefeatCount >= this.activeConditions[this.suppliedCount].minDestroiedEnemyCount && (double) MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime() >= (double) this.activeConditions[this.suppliedCount].minElapsedTime && MonoBehaviourSingleton<InGameProgress>.I.GetCarriableGimmickDeploiedCount() >= this.activeConditions[this.suppliedCount].minDeploiedGimmickNum;
    }
    if (!((Component) this.modelTrans).gameObject.activeSelf && this.CanUse())
    {
      this.Active();
    }
    else
    {
      if (!((Component) this.modelTrans).gameObject.activeSelf || this.CanUse())
        return;
      ((Component) this.modelTrans).gameObject.SetActive(false);
    }
  }

  public virtual void Active()
  {
    ((Component) this.modelTrans).gameObject.SetActive(true);
    this.canUse = true;
    this.OnActive();
    if (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.isStageHost)
      return;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.packetSender.OnActiveSupply(this.GetId());
  }

  protected virtual void OnActive()
  {
    MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlaySupplyInformation();
    SoundManager.PlayOneshotJingle(40000155);
    if (!MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) this);
  }

  public override float GetTargetSqrRadius()
  {
    return FieldSupplyGimmickObject.kRadius * FieldSupplyGimmickObject.kRadius;
  }

  public virtual bool CanUse() => this.canUse;

  public override bool IsSearchableNearest()
  {
    return this.CanUse() && ((Component) this.modelTrans).gameObject.activeSelf;
  }

  public override void RequestDestroy()
  {
    base.RequestDestroy();
    MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.SupplyGimmick, (IFieldGimmickObject) this);
  }

  public FieldCarriableGimmickObject SupplyGimmick()
  {
    if (!this.CanUse())
      return (FieldCarriableGimmickObject) null;
    this.coolTime = this.maxCoolTime;
    FieldCarriableGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, (int) this.supplyGimmickIds[this.suppliedCount]) as FieldCarriableGimmickObject;
    if (Object.op_Equality((Object) fieldGimmickObj, (Object) null))
      return (FieldCarriableGimmickObject) null;
    fieldGimmickObj.GetTransform().position = this.GetTransform().position;
    fieldGimmickObj.GetTransform().rotation = this.GetTransform().rotation;
    ((Component) fieldGimmickObj).gameObject.SetActive(true);
    if (MonoBehaviourSingleton<MiniMap>.IsValid())
      MonoBehaviourSingleton<MiniMap>.I.Detach((MonoBehaviour) this);
    ++this.suppliedCount;
    if (this.suppliedCount >= this.supplyGimmickIds.Count)
    {
      this.RequestDestroy();
    }
    else
    {
      ((Component) this.modelTrans).gameObject.SetActive(false);
      this.canUse = false;
    }
    EffectManager.OneShot(FieldSupplyGimmickObject.kBreakEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldSupplyGimmickObject.kBreakSEId, this.GetTransform().position);
    return fieldGimmickObj;
  }

  public void SetSupplyGimmickInfo(Coop_Model_StageInfo.FieldSupplyGimmickInfo info)
  {
    this.suppliedCount = info.suppliedCount;
    if (info.canUse)
    {
      this.Active();
    }
    else
    {
      ((Component) this.modelTrans).gameObject.SetActive(false);
      this.canUse = false;
    }
  }

  public Coop_Model_StageInfo.FieldSupplyGimmickInfo GetSupplyGimmickInfo()
  {
    return new Coop_Model_StageInfo.FieldSupplyGimmickInfo()
    {
      pointId = this.GetId(),
      suppliedCount = this.suppliedCount,
      canUse = this.canUse
    };
  }

  public class ActiveCondition
  {
    public float maxDefenseTargetHp = 100f;
    public int minDestroiedEnemyCount;
    public float minElapsedTime;
    public int minDeploiedGimmickNum;
  }
}
