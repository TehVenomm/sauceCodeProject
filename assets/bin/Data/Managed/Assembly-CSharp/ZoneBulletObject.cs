// Decompiled with JetBrains decompiler
// Type: ZoneBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZoneBulletObject : MonoBehaviour
{
  private readonly string OBJ_NAME = "ZoneBullet:";
  private readonly string HEALATK_NAME = "HealAttackObject";
  private Player ownerPlayer;
  private Player usePlayer;
  private BulletData bulletData;
  private SkillInfo.SkillParam skillParam;
  private Transform cachedPlayerTransform;
  private Transform cachedTransform;
  private Transform cachedEffectTransform;
  private SphereCollider cachedCollider;
  private int ignoreLayerMask;
  private int healValue;
  private float lifeTime;
  private float intervalTime;
  private bool isInitialized;
  private Vector3 tmpVector;
  private Character.HealData healData;
  private Dictionary<int, float> validSecCollection = new Dictionary<int, float>();

  public void Initialize(
    Player player,
    BulletData bullet,
    Vector3 position,
    SkillInfo.SkillParam skill,
    bool isHealDamgeEnemy,
    bool isOwner)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
    }
    else
    {
      this.ownerPlayer = isOwner ? player : (Player) null;
      this.usePlayer = player;
      this.bulletData = bullet;
      this.skillParam = skill;
      this.healValue = this.bulletData.dataZone.type == BulletData.BulletZone.TYPE.HEAL ? skill.supportValue[0] : 0;
      this.lifeTime = skill.supportTime[0];
      this.intervalTime = bullet.dataZone.intervalTime;
      ((Object) ((Component) this).gameObject).name = this.OBJ_NAME + (object) skill.tableData.id;
      this.cachedPlayerTransform = ((Component) player).transform;
      this.cachedTransform = ((Component) this).transform;
      this.cachedTransform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
      this.cachedTransform.position = position;
      this.cachedTransform.localScale = Vector3.one;
      if (MonoBehaviourSingleton<EffectManager>.IsValid())
      {
        this.cachedEffectTransform = EffectManager.GetEffect(this.bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
        if (this.cachedEffectTransform != null)
        {
          this.cachedEffectTransform.position = Vector3.op_Addition(this.cachedTransform.position, this.bulletData.data.dispOffset);
          this.cachedEffectTransform.localRotation = Quaternion.Euler(this.bulletData.data.dispRotation);
        }
      }
      ((Component) this).gameObject.layer = 31 /*0x1F*/;
      this.ignoreLayerMask |= 41984;
      this.ignoreLayerMask |= 20480 /*0x5000*/;
      this.ignoreLayerMask |= 2490880;
      this.cachedCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
      this.cachedCollider.radius = this.bulletData.data.radius;
      ((Collider) this.cachedCollider).isTrigger = true;
      ((Collider) this.cachedCollider).enabled = true;
      this.validSecCollection.Clear();
      if (isHealDamgeEnemy && this.healValue > 0)
        new GameObject(this.HEALATK_NAME).AddComponent<HealAttackZoneObject>().Setup(this.ownerPlayer, this.cachedTransform, bullet, skill);
      this.healData = new Character.HealData(this.healValue, this.bulletData.dataZone.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>()
      {
        10
      });
      this.isInitialized = true;
    }
  }

  private void Destroy() => this.OnDisappear();

  private void Update()
  {
    if (!this.isInitialized)
      return;
    if (this.bulletData.dataZone.isCarry)
    {
      if (Object.op_Inequality((Object) this.usePlayer, (Object) null) && !this.usePlayer.isDead)
      {
        this.tmpVector = this.cachedPlayerTransform.position;
        this.tmpVector.y = this.bulletData.data.dispOffset.y;
        this.cachedTransform.position = this.tmpVector;
        this.cachedEffectTransform.position = this.tmpVector;
      }
      else
        this.lifeTime = 0.0f;
    }
    this.lifeTime -= Time.deltaTime;
    if ((double) this.lifeTime > 0.0)
      return;
    this.OnDisappear();
  }

  private void OnDisappear()
  {
    this.ownerPlayer = (Player) null;
    this.usePlayer = (Player) null;
    this.bulletData = (BulletData) null;
    this.skillParam = (SkillInfo.SkillParam) null;
    this.cachedPlayerTransform = (Transform) null;
    this.cachedTransform = (Transform) null;
    if (this.cachedCollider != null)
      ((Collider) this.cachedCollider).enabled = false;
    this.cachedCollider = (SphereCollider) null;
    if (Object.op_Inequality((Object) this.cachedEffectTransform, (Object) null) && Object.op_Inequality((Object) ((Component) this.cachedEffectTransform).gameObject, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.cachedEffectTransform).gameObject);
    this.cachedEffectTransform = (Transform) null;
    if (((Component) this).gameObject != null)
      Object.Destroy((Object) ((Component) this).gameObject);
    this.isInitialized = false;
  }

  private void OnTriggerEnter(Collider collider)
  {
    Player validPlayer = this._GetValidPlayer(collider);
    if (validPlayer == null)
      return;
    if (!this.validSecCollection.ContainsKey(validPlayer.id))
      this.validSecCollection.Add(validPlayer.id, 0.0f);
    else
      this.validSecCollection[validPlayer.id] = 0.0f;
  }

  private void OnTriggerStay(Collider collider)
  {
    Player validPlayer = this._GetValidPlayer(collider);
    if (validPlayer == null)
      return;
    if (!this.validSecCollection.ContainsKey(validPlayer.id))
      this.validSecCollection.Add(validPlayer.id, 0.0f);
    this.validSecCollection[validPlayer.id] += Time.deltaTime;
    if ((double) this.validSecCollection[validPlayer.id] < (double) this.intervalTime)
      return;
    this.validSecCollection[validPlayer.id] -= this.intervalTime;
    if (this.healValue > 0)
      validPlayer.OnHealReceive(this.healData);
    if (this.bulletData.dataZone.buffType == BuffParam.BUFFTYPE.NONE)
      return;
    validPlayer.SetSelfBuff(this.skillParam.tableData.id, this.bulletData.dataZone.buffType, this.skillParam.supportValue[1], this.skillParam.supportTime[1], this.skillParam.skillIndex);
  }

  private void OnTriggerExit(Collider collider)
  {
    Player validPlayer = this._GetValidPlayer(collider);
    if (validPlayer == null || !this.validSecCollection.ContainsKey(validPlayer.id))
      return;
    this.validSecCollection.Remove(validPlayer.id);
  }

  private Player _GetValidPlayer(Collider collider)
  {
    if ((1 << ((Component) collider).gameObject.layer & this.ignoreLayerMask) > 0)
      return (Player) null;
    Player component = ((Component) collider).gameObject.GetComponent<Player>();
    if (component == null)
      return (Player) null;
    return component.IsCoopNone() || component.IsOriginal() || this.ownerPlayer != null && component.isNpc ? component : (Player) null;
  }
}
