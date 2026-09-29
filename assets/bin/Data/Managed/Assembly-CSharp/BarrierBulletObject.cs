// Decompiled with JetBrains decompiler
// Type: BarrierBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BarrierBulletObject : StageObject
{
  private int hpMax;
  private int hp;
  private AtkAttribute def = new AtkAttribute();
  private AtkAttribute tol = new AtkAttribute();
  private BulletObject bulletObj;
  private EffectColorCtrl effectColorCtrl;
  private string effectNameInBarrier = string.Empty;
  private Player owner;
  private List<Player> playerList = new List<Player>();
  private bool isDead;

  public int GetOwnerId() => this.owner.id;

  public int GetHp() => this.hp;

  public int GetMaxHp() => this.hpMax;

  public float GetDefAndTolValue() => this.def.normal;

  public float GetTimeCount() => this.bulletObj.timeCount;

  public bool IsDead() => this.isDead;

  public string GetEffectNameInBarrier() => this.effectNameInBarrier;

  public void Initialize(BulletObject bulletObj)
  {
    ((Component) this).gameObject.layer = 31 /*0x1F*/;
    this.bulletObj = bulletObj;
    this.owner = bulletObj.stageObject as Player;
    if (Object.op_Inequality((Object) this.owner, (Object) null))
    {
      int num1 = this.owner.id % 110000;
      int weaponIndex = this.owner.weaponIndex;
      int skillIndex = bulletObj.masterSkill.skillIndex;
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        num1 = 0;
      if (QuestManager.IsValidTrial())
        num1 = 0;
      if (QuestManager.IsValidInGameSeriesArena())
        num1 = 0;
      this.id = 1000000;
      string s = num1.ToString() + weaponIndex.ToString() + skillIndex.ToString() + this.owner.bulletIndex.ToString("D3");
      int num2 = -1;
      ref int local = ref num2;
      if (int.TryParse(s, out local))
        this.id = 1000000 + num2;
    }
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._rigidbody.useGravity = false;
    this._rigidbody.isKinematic = true;
    if (Object.op_Inequality((Object) bulletObj, (Object) null) && Object.op_Inequality((Object) bulletObj.bulletEffect, (Object) null))
      this.effectColorCtrl = ((Component) bulletObj.bulletEffect).GetComponent<EffectColorCtrl>();
    int base_value = bulletObj.bulletData.dataBarrier.baseHp;
    int num = bulletObj.bulletData.dataBarrier.baseDef;
    if (bulletObj.masterSkill != null)
    {
      GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(bulletObj.masterSkill.tableData.growID, bulletObj.masterSkill.baseInfo.level, bulletObj.masterSkill.baseInfo.exceedCnt);
      if (growSkillItemData != null)
      {
        base_value = growSkillItemData.GetGrowResultSupportValue(base_value, 0);
        num = growSkillItemData.GetGrowResultSupportValue(num, 1);
      }
    }
    this.hp = this.hpMax = base_value;
    this.def.normal = (float) num;
    this.tol.AddElementOnly((float) num);
    if (this.owner.IsOriginal())
    {
      this.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
      if (Object.op_Inequality((Object) this.packetSender, (Object) null))
        this.packetSender.OnSetCoopMode(StageObject.COOP_MODE_TYPE.PUPPET);
    }
    this.effectNameInBarrier = bulletObj.bulletData.dataBarrier.effectNameInBarrier;
    if (Object.op_Inequality((Object) this.owner.activeBulletBarrierObject, (Object) null))
      this.owner.activeBulletBarrierObject.bulletObj.ForceBreak();
    this.owner.activeBulletBarrierObject = this;
    this.isInitialized = true;
  }

  protected override void Update()
  {
    base.Update();
    if (!Object.op_Inequality((Object) this.effectColorCtrl, (Object) null))
      return;
    this.effectColorCtrl.UpdateColor((float) this.hp / (float) this.hpMax);
  }

  protected override bool IsValidAttackedHit(StageObject from_object) => from_object is Enemy;

  protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
  {
    base.OnAttackedHitDirection(status);
    if (status.hitParam.processor != null)
    {
      BulletObject colliderInterface = status.hitParam.processor.colliderInterface as BulletObject;
      if (Object.op_Inequality((Object) colliderInterface, (Object) null))
      {
        status.atk = colliderInterface.masterAtk;
        status.skillParam = colliderInterface.masterSkill;
      }
      else
      {
        AtkAttribute atk = new AtkAttribute();
        status.fromObject.GetAtk(status.attackInfo, ref atk);
        status.atk = atk;
      }
    }
    if (status.fromType != StageObject.OBJECT_TYPE.ENEMY)
      return;
    status.validDamage = true;
    if (!Object.op_Inequality((Object) this.effectColorCtrl, (Object) null))
      return;
    this.effectColorCtrl.PlayHitEffect();
  }

  protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
    if (!status.validDamage)
      return;
    AtkAttribute damage_details = new AtkAttribute();
    status.damage = this.CalcDamage(status, ref damage_details);
    status.damageDetails = damage_details;
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    status.afterHP = this.hp;
    if (!status.validDamage)
      return;
    status.afterHP -= status.damage;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    this.hp = status.afterHP;
    if (!this.bulletObj.IsEnable())
      return;
    if (this.hp <= 0)
    {
      this.isDead = true;
      if (!this.playerList.IsNullOrEmpty<Player>())
      {
        int index = 0;
        for (int count = this.playerList.Count; index < count; ++index)
        {
          if (!this.playerList[index].isDead)
            this.playerList[index].MakeInvincible(MonoBehaviourSingleton<InGameSettingsManager>.I.player.barrierBrokenReaction.invincibleDuration);
        }
      }
      this.bulletObj.OnDestroy();
    }
    else
    {
      if (string.IsNullOrEmpty(status.attackInfo.remainEffectName))
        return;
      EffectManager.GetEffect(status.attackInfo.remainEffectName, this._transform).position = status.hitPos;
    }
  }

  private int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
  {
    AtkAttribute atkAttribute = new AtkAttribute();
    atkAttribute.Add(status.atk);
    atkAttribute.Mul(status.attackInfo.atkRate);
    int enemyLevel = 1;
    Enemy fromObject = status.fromObject as Enemy;
    if (Object.op_Inequality((Object) fromObject, (Object) null))
      enemyLevel = (int) fromObject.enemyLevel;
    float levelRate = InGameUtility.CalcLevelRate(enemyLevel);
    damage_details.normal = (float) (int) ((double) atkAttribute.normal * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.normal, 0.0f));
    damage_details.fire = (float) (int) ((double) atkAttribute.fire * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.fire, this.tol.fire));
    damage_details.water = (float) (int) ((double) atkAttribute.water * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.water, this.tol.water));
    damage_details.thunder = (float) (int) ((double) atkAttribute.thunder * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.thunder, this.tol.thunder));
    damage_details.soil = (float) (int) ((double) atkAttribute.soil * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.soil, this.tol.soil));
    damage_details.light = (float) (int) ((double) atkAttribute.light * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.light, this.tol.light));
    damage_details.dark = (float) (int) ((double) atkAttribute.dark * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, this.def.dark, this.tol.dark));
    damage_details.CheckMinus();
    int num = Mathf.FloorToInt(damage_details.CalcTotal());
    if (num < 1)
      num = 1;
    return num;
  }

  public void AddPlayer(Player player)
  {
    if (this.playerList.Contains(player))
      return;
    this.playerList.Add(player);
  }

  public void RemovePlayer(Player player)
  {
    if (!this.playerList.Contains(player))
      return;
    this.playerList.Remove(player);
  }

  public void ForceExitThroughProcessor(Collider exitCollider)
  {
    if (Object.op_Equality((Object) exitCollider, (Object) null) || Object.op_Equality((Object) this.bulletObj, (Object) null))
      return;
    this.bulletObj.OnTriggerExit(exitCollider);
  }

  public override void OnRecvSetCoopMode(Coop_Model_ObjectCoopInfo model, CoopPacket packet)
  {
    StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model.id);
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return;
    stageObject.SetCoopMode(model.CoopModeType, packet.fromClientId);
  }

  public override bool DestroyObject()
  {
    if (this.playerList.IsNullOrEmpty<Player>())
      return base.DestroyObject();
    int index = 0;
    for (int count = this.playerList.Count; index < count; ++index)
      this.playerList[index].ForceClearByBarrier(this);
    return base.DestroyObject();
  }
}
