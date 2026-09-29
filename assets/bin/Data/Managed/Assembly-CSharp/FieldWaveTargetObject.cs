// Decompiled with JetBrains decompiler
// Type: FieldWaveTargetObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldWaveTargetObject : StageObject, IFieldGimmickObject
{
  private const int kDefaultModelIndex = 3;
  private const int kShiftIndex = 1000;
  private const float kHateUpdateInterval = 30f;
  private FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;
  private Transform m_modelTrans;
  private int _maxHp;
  private int _nowHp;
  private InGameSettingsManager.WaveMatchParam param;
  private float hateWaitSec;
  private FieldWaveTargetObject.TargetInfo _info = new FieldWaveTargetObject.TargetInfo();
  private int modelIndex = 3;
  private bool isBarrier;
  private Transform barrierEffect;

  public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType => this.m_gimmickType;

  public int maxHp => this._maxHp;

  public int nowHp => this._nowHp;

  public bool isDead => this._nowHp <= 0;

  public FieldWaveTargetObject.TargetInfo info => this._info;

  public static int GetModelIndex(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return 3;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "mi")
      {
        int result = 0;
        if (int.TryParse(strArray[1], out result))
          return result;
      }
    }
    return 3;
  }

  public static string[] GetEffectNamesByModelIndex(int modelIndex)
  {
    List<string> stringList = new List<string>();
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsWaveStrategyMatch())
      stringList.Add(FieldWaveTargetObject.MakeBarrierEffectNameByModelName(modelIndex));
    return stringList.ToArray();
  }

  public static string MakeBarrierEffectNameByModelName(int modelIndex)
  {
    return $"ef_btl_defense_wavetarget_barrier_{modelIndex:D2}";
  }

  public static string ConvertModelIndexToName(int idx) => $"CMN_wavetarget{idx:D2}";

  public static uint ConvertModelIndexToKey(int idx) => (uint) (idx * 1000 + 16 /*0x10*/);

  public float GetRate()
  {
    return this._maxHp <= 0 || this._nowHp <= 0 ? 0.0f : (float) this._nowHp / (float) this._maxHp;
  }

  public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    this.Initialize();
    this.param = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
    this.objectType = StageObject.OBJECT_TYPE.WAVE_TARGET;
    this.id = (int) pointData.pointID;
    this.m_gimmickType = pointData.gimmickType;
    this.ParseParam(pointData.value2);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
    {
      uint key = (uint) this.m_gimmickType;
      if (this.m_gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
        key = FieldWaveTargetObject.ConvertModelIndexToKey(this.modelIndex);
      LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(key);
      if (loadObject != null)
        this.m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this._transform);
    }
    this._maxHp = this._nowHp = (int) pointData.value1;
    this.coopMode = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isStageHost ? StageObject.COOP_MODE_TYPE.ORIGINAL : StageObject.COOP_MODE_TYPE.MIRROR;
    if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateWaveTarget(this);
    if (MonoBehaviourSingleton<MiniMap>.IsValid())
      MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) this);
    if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid() || this.m_gimmickType != FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
      return;
    MonoBehaviourSingleton<SceneSettingsManager>.I.AddWaveTarget(((Component) this).gameObject);
  }

  private void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "n":
            this.info.name = strArray[1];
            continue;
          case "i":
            this.info.iconName = strArray[1];
            continue;
          case "ie":
            bool.TryParse(strArray[1], out this.info.iconEvent);
            continue;
          case "r":
            if (float.TryParse(strArray[1], out this.info.radius))
            {
              this.SetColliderRadius(this.info.radius);
              continue;
            }
            continue;
          case "d":
            this.info.dispName = strArray[1];
            continue;
          case "mi":
            int.TryParse(strArray[1], out this.modelIndex);
            continue;
          default:
            continue;
        }
      }
    }
  }

  public void SetColliderRadius(float radius)
  {
    if ((double) radius <= 0.0 || Object.op_Equality((Object) this.m_modelTrans, (Object) null))
      return;
    SphereCollider component = ((Component) this.m_modelTrans).GetComponent<SphereCollider>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    component.radius = radius;
  }

  protected override bool IsValidAttackedHit(StageObject fromObject)
  {
    return !this.isDead && !this.isBarrier && fromObject is Enemy && base.IsValidAttackedHit(fromObject);
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    if (this.isDead || this.isBarrier)
      return;
    Enemy fromObject = status.fromObject as Enemy;
    if (Object.op_Equality((Object) fromObject, (Object) null))
      return;
    status.damage = fromObject.isWaveMatchBoss ? this.param.enemyBossDamage : this.param.enemyNormalDamage;
    status.afterHP = this._nowHp - status.damage;
    base.OnAttackedHitOwner(status);
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    if (this.isDead || this.isBarrier)
      return;
    this._nowHp = status.afterHP;
    if (this._nowHp <= 0)
    {
      this._nowHp = 0;
      SoundManager.PlayOneShotSE(this.param.targetBreakSeId, status.hitPos);
      if (((Component) this).gameObject == null)
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }
    else
    {
      EffectManager.OneShot(this.param.targetHitEffect, new Vector3(status.hitPos.x, 0.0f, status.hitPos.z), Quaternion.identity, this.param.targetHitEffectScale);
      SoundManager.PlayOneShotSE(this.param.targetHitSeId, status.hitPos);
    }
  }

  public int GetId() => this.id;

  public Transform GetTransform() => this._transform;

  public string GetObjectName() => "WaveTarget";

  public void SetTransform(Transform trans) => this.m_modelTrans = trans;

  public float GetTargetRadius() => 0.0f;

  public float GetTargetSqrRadius() => 0.0f;

  public void UpdateTargetMarker(bool isNear)
  {
  }

  public bool IsSearchableNearest() => true;

  public void RequestDestroy()
  {
  }

  public string GetIconName(int hpRate = 100)
  {
    if (this.param == null)
      return "";
    if (!this.param.isEvent && !this.info.iconEvent)
      return this._info.iconName;
    if (hpRate > 60)
      return "wme_03";
    if (hpRate > 30)
      return "wme_02";
    return hpRate > 0 ? "wme_01" : "wme_00";
  }

  public string GetRaderIconName()
  {
    if (this.param == null)
      return "";
    if (!this.param.isEvent && !this.info.iconEvent)
      return this._info.iconName;
    return this._nowHp == 0 ? "wme_dead" : "wme";
  }

  protected override void Update()
  {
    base.Update();
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.enemyList == null)
      return;
    this.hateWaitSec -= Time.deltaTime;
    if ((double) this.hateWaitSec > 0.0)
      return;
    this.hateWaitSec = 30f;
    for (int index = 0; index < MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; ++index)
    {
      StageObject enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[index];
      if (!Object.op_Equality((Object) enemy, (Object) null) && !Object.op_Equality((Object) enemy.controller, (Object) null))
      {
        Brain brain = enemy.controller.brain;
        if (!Object.op_Equality((Object) brain, (Object) null))
          brain.HandleEvent(BRAIN_EVENT.WAVE_TARGET, (object) this);
      }
    }
  }

  public void SetHp(int now, int max, bool changeOwner = false)
  {
    this._nowHp = now;
    if (this._nowHp <= 0)
    {
      this._nowHp = 0;
      if (((Component) this).gameObject != null)
        Object.Destroy((Object) ((Component) this).gameObject);
    }
    if (max > 0)
      this._maxHp = max;
    if (this._maxHp < this._nowHp)
      this._maxHp = this._nowHp;
    if (!changeOwner)
      return;
    this.SetOwner(false);
  }

  public void SetOwner(bool isOwner)
  {
    this.coopMode = isOwner ? StageObject.COOP_MODE_TYPE.ORIGINAL : StageObject.COOP_MODE_TYPE.MIRROR;
  }

  public void Barrier()
  {
    if (this.isBarrier)
      return;
    this.isBarrier = true;
    this.barrierEffect = EffectManager.GetEffect(FieldWaveTargetObject.MakeBarrierEffectNameByModelName(this.modelIndex), this._transform);
  }

  protected override void OnDisable()
  {
    if (Object.op_Inequality((Object) this.barrierEffect, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.barrierEffect).gameObject);
    base.OnDisable();
  }

  public class TargetInfo
  {
    public string name = "";
    public float radius;
    public string iconName = "";
    public bool iconEvent;
    public string dispName = "";
  }
}
