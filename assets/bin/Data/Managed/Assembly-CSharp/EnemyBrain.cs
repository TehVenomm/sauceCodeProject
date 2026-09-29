// Decompiled with JetBrains decompiler
// Type: EnemyBrain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyBrain : Brain
{
  protected Enemy enemy;
  public EnemyActionController actionCtrl;

  protected override void Awake()
  {
    base.Awake();
    this.enemy = this.owner as Enemy;
  }

  protected override void Update()
  {
    base.Update();
    if (!QuestManager.IsValidInGameWaveStrategy() || !Object.op_Equality((Object) this.enemy.actionTarget, (Object) null) && !(this.enemy.actionTarget is Player))
      return;
    this.SetNearWaveMatchTarget();
  }

  protected override void OnInitialize()
  {
    base.OnInitialize();
    if (this.enemy.brainParam != null)
    {
      this.param = this.enemy.brainParam;
      FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, this.enemy.enemyPopIndex);
      if (enemyPopData != null && enemyPopData.autoActivate)
        this.param.scoutParam = enemyPopData.scoutingParam;
    }
    this.opponentMemSpanTimer.PauseOn();
    this.targetUpdateSpanTimer.PauseOn();
    if (this.enemy.isBoss)
      this.opponentMem.SetHateParam(this.enemy.enemyTableData.personality);
    else
      this.opponentMem.SetHateParam(HateParam.GetDefault());
    this.fsm = new StateMachine((Brain) this);
    if (this.enemy.enemyTableData.active)
      this.fsm.ChangeState(STATE_TYPE.ACTIVE);
    else
      this.fsm.ChangeState(STATE_TYPE.NONACTIVE);
    if (QuestManager.IsValidInGameWaveMatch())
      this.SetNearWaveMatchTarget();
    if (QuestManager.IsValidInGameWaveStrategy())
    {
      this.opponentMem.SetHateParam((HateParam) null);
      this.SetNearDecoyTarget();
    }
    this.actionCtrl = new EnemyActionController((Brain) this);
    this.actionCtrl.LoadTable();
  }

  protected override void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    base.OnDestroy();
  }

  public override float GetScale()
  {
    return this.enemy.enemyTableData == null ? 1f : this.enemy.enemyTableData.modelScale;
  }

  public override Transform GetFront() => this.enemy.head;

  public override Transform GetBack() => this.enemy.hip;

  public override List<StageObject> GetTargetObjectList()
  {
    List<StageObject> targetObjectList = new List<StageObject>();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      targetObjectList.AddRange((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.playerList);
      targetObjectList.AddRange((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.decoyList);
    }
    if (QuestManager.IsValidInGameWaveMatch())
      targetObjectList.AddRange((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList);
    return targetObjectList;
  }

  public override List<StageObject> GetAllyObjectList()
  {
    return !MonoBehaviourSingleton<StageObjectManager>.IsValid() ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
  }

  private void SetNearWaveMatchTarget()
  {
    if (this.fsm == null || this.targetCtrl == null)
      return;
    StageObject target_obj = (StageObject) null;
    float num = float.MaxValue;
    for (int index = 0; index < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; ++index)
    {
      FieldWaveTargetObject waveTarget = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[index] as FieldWaveTargetObject;
      if (!Object.op_Equality((Object) waveTarget, (Object) null) && !waveTarget.isDead)
      {
        Vector3 vector3 = Vector3.op_Subtraction(waveTarget._position, this.enemy._position);
        float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
        if ((double) sqrMagnitude < (double) num)
        {
          num = sqrMagnitude;
          target_obj = (StageObject) waveTarget;
        }
      }
    }
    if (Object.op_Equality((Object) target_obj, (Object) null))
      return;
    this.fsm.ChangeState(STATE_TYPE.SELECT);
    this.targetCtrl.SetCurrentTarget(target_obj);
    this.HandleEvent(BRAIN_EVENT.WAVE_TARGET, (object) target_obj);
  }

  public void SetNearDecoyTarget()
  {
    if (this.targetCtrl == null)
      return;
    StageObject nearestDecoyObject = (StageObject) AIUtility.GetNearestDecoyObject(this.owner._position);
    if (Object.op_Equality((Object) nearestDecoyObject, (Object) null))
      return;
    this.fsm.ChangeState(STATE_TYPE.SELECT);
    this.targetCtrl.SetCurrentTarget(nearestDecoyObject);
  }

  public void MissDecoyTarget(StageObject decoyObj)
  {
    if (this.targetCtrl == null || Object.op_Equality((Object) decoyObj, (Object) null))
      return;
    DecoyBulletObject currentTarget = this.targetCtrl.GetCurrentTarget() as DecoyBulletObject;
    if (Object.op_Equality((Object) currentTarget, (Object) null) || currentTarget.IsActive())
      return;
    this.fsm.ChangeState(STATE_TYPE.SELECT);
    this.targetCtrl.MissCurrentTarget();
  }

  public override void HandleEvent(BRAIN_EVENT ev, object param = null)
  {
    switch (ev)
    {
      case BRAIN_EVENT.END_ACTION:
        if (this.opponentMem != null)
        {
          this.opponentMem.Update();
          break;
        }
        break;
      case BRAIN_EVENT.ATTACKED_HIT:
        AttackedHitStatusOwner attackedHitStatusOwner1 = (AttackedHitStatusOwner) param;
        if (this.opponentMem != null && this.opponentMem.haveHateControl)
        {
          OpponentMemory opponentMem = this.opponentMem;
          Vector3 vector3 = Vector3.op_Subtraction(attackedHitStatusOwner1.fromPos, attackedHitStatusOwner1.hitPos);
          double sqrMagnitude = (double) ((Vector3) ref vector3).sqrMagnitude;
          DISTANCE distance = opponentMem.GetDistance((float) sqrMagnitude);
          int hate_val = (int) ((double) attackedHitStatusOwner1.damage * (double) this.opponentMem.hateParam.distanceAttackRatio[(int) distance]);
          if (this.isNPC(attackedHitStatusOwner1.fromObject))
            hate_val = (int) ((double) hate_val * 0.5);
          this.opponentMem.AddHate(attackedHitStatusOwner1.fromObject, hate_val, Hate.TYPE.Damage);
          break;
        }
        break;
      case BRAIN_EVENT.ATTACKED_WEAK_POINT:
        AttackedHitStatusOwner attackedHitStatusOwner2 = (AttackedHitStatusOwner) param;
        if (this.opponentMem != null && this.opponentMem.haveHateControl)
        {
          int attackedWeakPointHate = this.opponentMem.hateParam.attackedWeakPointHate;
          OpponentMemory opponentMem = this.opponentMem;
          Vector3 vector3 = Vector3.op_Subtraction(attackedHitStatusOwner2.fromPos, attackedHitStatusOwner2.hitPos);
          double sqrMagnitude = (double) ((Vector3) ref vector3).sqrMagnitude;
          DISTANCE distance = opponentMem.GetDistance((float) sqrMagnitude);
          int hate_val = (int) ((double) attackedWeakPointHate * (double) this.opponentMem.hateParam.distanceAttackRatio[(int) distance]);
          if (this.isNPC(attackedHitStatusOwner2.fromObject))
            hate_val = (int) ((double) hate_val * 0.5);
          this.opponentMem.AddHate(attackedHitStatusOwner2.fromObject, hate_val, Hate.TYPE.SpecialDamage);
          break;
        }
        break;
      case BRAIN_EVENT.OWN_ATTACK_HIT:
        if (this.opponentMem != null)
        {
          OpponentMemory.OpponentRecord opponentRecord = this.opponentMem.Find(param as StageObject);
          if (opponentRecord != null)
          {
            opponentRecord.record.isDamaged = true;
            break;
          }
          break;
        }
        break;
      case BRAIN_EVENT.PLAYER_HEAL:
        if (this.opponentMem != null)
        {
          Player.HateInfo hateInfo = param as Player.HateInfo;
          this.opponentMem.AddHate(hateInfo.target, hateInfo.val, Hate.TYPE.Heal);
          break;
        }
        break;
      case BRAIN_EVENT.PLAYER_SKILL:
        if (this.opponentMem != null)
        {
          this.opponentMem.AddHate(param as StageObject, this.opponentMem.hateParam.skillHate, Hate.TYPE.Skill);
          break;
        }
        break;
      case BRAIN_EVENT.REVIVE_REGION:
        if (this.actionCtrl != null)
        {
          this.actionCtrl.OnReviveRegion((int) param);
          break;
        }
        break;
      case BRAIN_EVENT.DECOY:
        if (this.opponentMem != null)
        {
          DecoyBulletObject.HateInfo hateInfo = param as DecoyBulletObject.HateInfo;
          this.opponentMem.AddHate(hateInfo.target, hateInfo.value, hateInfo.type);
          break;
        }
        break;
      case BRAIN_EVENT.WAVE_TARGET:
        if (this.opponentMem != null)
        {
          this.opponentMem.AddHate(param as StageObject, 1000, Hate.TYPE.Damage);
          break;
        }
        break;
    }
    base.HandleEvent(ev, param);
  }

  private bool isNPC(StageObject obj)
  {
    Player player = obj as Player;
    return Object.op_Inequality((Object) player, (Object) null) && player.isNpc;
  }
}
