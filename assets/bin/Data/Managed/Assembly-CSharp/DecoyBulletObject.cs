// Decompiled with JetBrains decompiler
// Type: DecoyBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DecoyBulletObject : StageObject
{
  private readonly string OBJ_NAME = "DecoyBullet:";
  private static int sLocalId;
  protected Player ownerPlayer;
  private BulletData bulletData;
  private SkillInfo.SkillParam skillParam;
  private AttackInfo atkInfo;
  private AtkAttribute exAtk = new AtkAttribute();
  private Transform cachedTransform;
  protected Transform cachedEffectTransform;
  private SphereCollider cachedCollider;
  private int ignoreLayerMask;
  private float lifeTime;
  private float dontHitSec;
  private bool isInitialized;
  private float useRate = 1f;
  private bool isHitExplode;
  private float hitNum;
  private float hitInterval;
  private bool isEnableHateInterval;
  private float hateInterval;
  private bool isUseLifeTime;
  private FieldCarriableDecoyGimmickObject carriableGimmick;

  public override AttackInfo[] GetAttackInfos()
  {
    return Object.op_Equality((Object) this.ownerPlayer, (Object) null) ? (AttackInfo[]) null : this.ownerPlayer.GetAttackInfos();
  }

  public virtual void Initialize(
    int playerId,
    int decoyId,
    BulletData bullet,
    Vector3 position,
    SkillInfo.SkillParam skill,
    bool isHit)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize DecoyBulletObject.");
    }
    else
    {
      this.objectType = StageObject.OBJECT_TYPE.DECOY;
      StageObject player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId);
      if (player != null)
        this.ownerPlayer = player as Player;
      this.bulletData = bullet;
      this.skillParam = skill;
      this.hateInterval = 0.0f;
      this.cachedTransform = ((Component) this).transform;
      this.cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
      this.cachedTransform.position = position;
      this.cachedTransform.localScale = Vector3.one;
      if (Object.op_Inequality((Object) this.bulletData, (Object) null))
      {
        this.lifeTime = this.bulletData.data.appearTime;
        this.dontHitSec = this.GetDecoyData(this.bulletData).dontHitSec;
        this.hitNum = (float) this.GetDecoyData(this.bulletData).explodeHitNum;
        this.isHitExplode = (double) this.hitNum > 0.0;
        this.isEnableHateInterval = (double) this.GetDecoyData(this.bulletData).hateInterval > 0.0;
        this.isUseLifeTime = (double) this.bulletData.data.appearTime > 0.0;
        if (MonoBehaviourSingleton<EffectManager>.IsValid())
        {
          this.cachedEffectTransform = EffectManager.GetEffect(this.bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
          this.cachedEffectTransform.position = Vector3.op_Addition(this.cachedTransform.position, this.bulletData.data.dispOffset);
          this.cachedEffectTransform.localRotation = Quaternion.Euler(this.bulletData.data.dispRotation);
        }
      }
      this.id = decoyId;
      if (decoyId > 0)
      {
        ((Object) ((Component) this).gameObject).name = this.OBJ_NAME + (object) decoyId;
        ((Component) this).gameObject.layer = 12;
        this.ignoreLayerMask |= -1073741824 /*0xC0000000*/;
      }
      if (this.ownerPlayer != null & isHit && this.skillParam != null)
      {
        this.cachedCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
        this.cachedCollider.radius = this.bulletData.data.radius;
        ((Collider) this.cachedCollider).isTrigger = true;
        ((Collider) this.cachedCollider).enabled = true;
        if (!string.IsNullOrEmpty(this.skillParam.tableData.attackInfoNames[0]))
          this.atkInfo = this.FindAttackInfo(this.skillParam.tableData.attackInfoNames[0]);
        this.ownerPlayer.GetAtk(this.atkInfo as AttackHitInfo, ref this.exAtk, skill);
      }
      if (QuestManager.IsValidInGameWaveStrategy() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
        MonoBehaviourSingleton<StageObjectManager>.I.SetAllEnemiesTargetDecoy();
      this.isInitialized = true;
    }
  }

  protected virtual BulletData.BulletDecoy GetDecoyData(BulletData bullet) => bullet.dataDecoy;

  private void Destroy() => this.OnDisappear(false);

  protected override void Update()
  {
    if (!this.isInitialized)
      return;
    this.dontHitSec -= Time.deltaTime;
    if ((double) this.hitInterval > 0.0)
      this.hitInterval -= Time.deltaTime;
    if (this.isEnableHateInterval)
    {
      this.hateInterval -= Time.deltaTime;
      if ((double) this.hateInterval <= 0.0)
        this.HateCtrl();
    }
    this.lifeTime -= Time.deltaTime;
    if (!this.isUseLifeTime || (double) this.lifeTime > 0.0)
      return;
    this.OnDisappear(true);
  }

  public virtual void OnDisappear(bool isExplode)
  {
    if (this.cachedEffectTransform != null && ((Component) this.cachedEffectTransform).gameObject != null)
      EffectManager.ReleaseEffect(((Component) this.cachedEffectTransform).gameObject);
    this.cachedEffectTransform = (Transform) null;
    if (isExplode && this.atkInfo != null)
    {
      StageObject stage_object = (StageObject) this.ownerPlayer;
      if (this.ownerPlayer == null)
        stage_object = (StageObject) this;
      AnimEventShot.Create(stage_object, this.atkInfo, this.cachedTransform.position, Quaternion.identity, exAtk: this.exAtk, exSkillParam: this.skillParam);
    }
    this.ownerPlayer = (Player) null;
    this.atkInfo = (AttackInfo) null;
    this.bulletData = (BulletData) null;
    if (((Component) this).gameObject == null)
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  protected virtual bool IsHitExplede() => this.isHitExplode;

  private void OnTriggerStay(Collider collider)
  {
    if (!this.IsHitExplede() || (double) this.dontHitSec > 0.0 || (1 << ((Component) collider).gameObject.layer & this.ignoreLayerMask) > 0 || (double) this.hitInterval > 0.0)
      return;
    this.hitInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.decoyHitInterval;
    if ((double) --this.hitNum > 0.0)
      return;
    if (this.ownerPlayer != null)
      this.ownerPlayer.ExecExplodeDecoyBullet(this.id);
    this.OnDisappear(true);
  }

  public void HateCtrl()
  {
    if (this.bulletData == null || this.GetDecoyData(this.bulletData) == null)
      return;
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (boss == null || !boss.IsOriginal() && !boss.IsCoopNone())
      return;
    EnemyBrain component = ((Component) MonoBehaviourSingleton<StageObjectManager>.I.boss).GetComponent<EnemyBrain>();
    if (component == null)
      return;
    float num = 1f;
    if (this.skillParam != null)
      num = (float) this.skillParam.supportValue[0] * 0.01f;
    if (this.GetDecoyData(this.bulletData).addDecoyHate.Length != 0)
    {
      int index = 0;
      for (int length = this.GetDecoyData(this.bulletData).addDecoyHate.Length; index < length; ++index)
      {
        BulletData.BulletDecoy.HateInfo hateInfo = this.GetDecoyData(this.bulletData).addDecoyHate[index];
        component.HandleEvent(BRAIN_EVENT.DECOY, (object) new DecoyBulletObject.HateInfo()
        {
          target = (StageObject) this,
          value = (int) ((double) hateInfo.value * (double) num * (double) this.useRate),
          type = hateInfo.type
        });
      }
    }
    if (this.ownerPlayer != null && this.GetDecoyData(this.bulletData).addOwnerHate.Length != 0)
    {
      int index = 0;
      for (int length = this.GetDecoyData(this.bulletData).addOwnerHate.Length; index < length; ++index)
      {
        BulletData.BulletDecoy.HateInfo hateInfo = this.GetDecoyData(this.bulletData).addOwnerHate[index];
        component.HandleEvent(BRAIN_EVENT.DECOY, (object) new DecoyBulletObject.HateInfo()
        {
          target = (StageObject) this.ownerPlayer,
          value = (int) ((double) hateInfo.value * (double) num * (double) this.useRate),
          type = hateInfo.type
        });
      }
    }
    this.useRate *= this.GetDecoyData(this.bulletData).hateDecreaseRate;
    this.hateInterval += this.GetDecoyData(this.bulletData).hateInterval;
  }

  public bool IsActive()
  {
    if (!((Component) this).gameObject.activeSelf)
      return false;
    return !Object.op_Inequality((Object) this.carriableGimmick, (Object) null) || this.carriableGimmick.isActive;
  }

  public void SetCarriable(FieldCarriableDecoyGimmickObject carriable)
  {
    this.carriableGimmick = carriable;
  }

  public class HateInfo
  {
    public StageObject target;
    public Hate.TYPE type;
    public int value;
  }
}
