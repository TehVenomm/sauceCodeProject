// Decompiled with JetBrains decompiler
// Type: PresentBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PresentBulletObject : MonoBehaviour, IPresentBulletObject
{
  private const string OBJECT_NAME = "PresentBulletObject:";
  private static readonly int ANIM_STATE_PICKED = Animator.StringToHash("PICKED");
  private static readonly int ANIM_STATE_LOOP_INCLUDE_LAYER = Animator.StringToHash("Base Layer.LOOP");
  private static readonly int ANIM_STATE_PICKED_INCLUDE_LAYER = Animator.StringToHash("Base Layer.PICKED");
  private static readonly Vector3 COLLIDER_SIZE = new Vector3(1.2f, 1f, 1.2f);
  private static readonly Vector3 COLLIDER_CENTER = new Vector3(0.0f, 0.5f, 0.0f);
  private PresentBulletObject.STATE m_state;
  private int m_presentBulletId;
  private BulletData m_bulletData;
  private Transform m_cachedTransform;
  private Transform m_cachedEffectTransform;
  private BoxCollider m_cachedCollider;
  private Animator m_effectAnimator;
  private StageObjectManager m_stageObjMgr;
  private SkillInfo.SkillParam m_skillParam;
  private int m_ignoreLayerMask;
  private float m_lifeSpan;
  private EffectCtrl m_effectCtrl;
  private List<int> m_buffIds = new List<int>();
  private BulletData.BulletPresent.LIFE_SPAN_TYPE m_lifeSpanType;
  private Character.HealData m_healData;

  public void Initialize(int id, BulletData bulletData, Transform transform)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
    }
    else
    {
      ((Object) ((Component) this).gameObject).name = "PresentBulletObject:" + id.ToString();
      this.m_presentBulletId = id;
      this.m_bulletData = bulletData;
      this.m_stageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
      this.m_lifeSpan = this.m_bulletData.data.appearTime;
      if (this.m_bulletData.dataPresent != null)
      {
        this.m_lifeSpanType = this.m_bulletData.dataPresent.lifeSpanType;
        this.m_buffIds = this.m_bulletData.dataPresent.buffIds;
      }
      this.m_cachedTransform = ((Component) this).transform;
      this.m_cachedTransform.parent = this.m_stageObjMgr._transform;
      this.m_cachedTransform.position = transform.position;
      this.m_cachedTransform.localScale = Vector3.one;
      if (MonoBehaviourSingleton<EffectManager>.IsValid())
        this.m_cachedEffectTransform = EffectManager.GetEffect(this.m_bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
      if (Object.op_Inequality((Object) this.m_cachedEffectTransform, (Object) null))
      {
        this.m_cachedEffectTransform.position = Vector3.op_Addition(transform.position, bulletData.data.dispOffset);
        this.m_cachedEffectTransform.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
        this.m_effectAnimator = ((Component) this.m_cachedEffectTransform).gameObject.GetComponent<Animator>();
        this.m_effectCtrl = ((Component) this.m_cachedEffectTransform).gameObject.GetComponent<EffectCtrl>();
      }
      ((Component) this).gameObject.layer = 31 /*0x1F*/;
      this.m_ignoreLayerMask |= 41984;
      this.m_ignoreLayerMask |= 20480 /*0x5000*/;
      this.m_ignoreLayerMask |= 2490880;
      this.m_cachedCollider = ((Component) this).gameObject.AddComponent<BoxCollider>();
      this.m_cachedCollider.size = PresentBulletObject.COLLIDER_SIZE;
      this.m_cachedCollider.center = PresentBulletObject.COLLIDER_CENTER;
      ((Collider) this.m_cachedCollider).isTrigger = true;
      ((Collider) this.m_cachedCollider).enabled = false;
      this.m_state = PresentBulletObject.STATE.ACTIVE;
    }
  }

  public void SetPosition(Vector3 position)
  {
    this.m_cachedTransform.position = position;
    if (!Object.op_Inequality((Object) this.m_cachedEffectTransform, (Object) null))
      return;
    this.m_cachedEffectTransform.position = Vector3.op_Addition(position, this.m_bulletData.data.dispOffset);
  }

  public void SetSkillParam(SkillInfo.SkillParam skillParam)
  {
    this.m_skillParam = skillParam;
    this.m_healData = new Character.HealData(this.m_skillParam.healHp, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
    {
      10
    });
  }

  public int GetPresentBulletId() => this.m_presentBulletId;

  private void Update()
  {
    if (Object.op_Inequality((Object) this.m_effectAnimator, (Object) null))
    {
      AnimatorStateInfo animatorStateInfo = this.m_effectAnimator.GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash == PresentBulletObject.ANIM_STATE_LOOP_INCLUDE_LAYER)
        ((Collider) this.m_cachedCollider).enabled = true;
    }
    if (this.m_state != PresentBulletObject.STATE.ACTIVE || this.m_lifeSpanType != BulletData.BulletPresent.LIFE_SPAN_TYPE.TIME)
      return;
    if ((double) this.m_lifeSpan <= 0.0)
      this.OnDisappear();
    this.m_lifeSpan -= Time.deltaTime;
  }

  public void OnDisappear()
  {
    if (Object.op_Inequality((Object) this.m_cachedCollider, (Object) null))
      ((Collider) this.m_cachedCollider).enabled = false;
    this.m_stageObjMgr.RemovePresentBulletObject(this.m_presentBulletId);
    if (Object.op_Inequality((Object) this.m_cachedEffectTransform, (Object) null) && Object.op_Inequality((Object) ((Component) this.m_cachedEffectTransform).gameObject, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.m_cachedEffectTransform).gameObject);
    if (!Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  public void OnPicked()
  {
    this.m_state = PresentBulletObject.STATE.PICKED;
    if (Object.op_Inequality((Object) this.m_cachedCollider, (Object) null))
      ((Collider) this.m_cachedCollider).enabled = false;
    this.StartCoroutine(this.OnPickedEffect());
  }

  private IEnumerator OnPickedEffect()
  {
    if (!Object.op_Equality((Object) this.m_effectAnimator, (Object) null) && !Object.op_Equality((Object) this.m_effectCtrl, (Object) null))
    {
      this.m_effectAnimator.Play(PresentBulletObject.ANIM_STATE_PICKED, 0, 0.0f);
      yield return (object) null;
      AnimatorStateInfo animatorStateInfo;
      while (true)
      {
        animatorStateInfo = this.m_effectAnimator.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != PresentBulletObject.ANIM_STATE_PICKED_INCLUDE_LAYER)
          yield return (object) null;
        else
          break;
      }
      while (true)
      {
        animatorStateInfo = this.m_effectAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          yield return (object) null;
        else
          break;
      }
      if (this.m_effectCtrl.waitParticlePlaying)
      {
        for (int i = 0; i < this.m_effectCtrl.particles.Length; ++i)
        {
          ParticleSystem particle = this.m_effectCtrl.particles[i];
          if (Object.op_Inequality((Object) particle, (Object) null) && particle.isPlaying)
          {
            particle.Stop(true);
            yield return (object) null;
          }
        }
      }
      if (Object.op_Inequality((Object) this.m_cachedEffectTransform, (Object) null) && Object.op_Inequality((Object) ((Component) this.m_cachedEffectTransform).gameObject, (Object) null))
      {
        bool flag = false;
        if (MonoBehaviourSingleton<EffectManager>.IsValid())
          flag = MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(((Component) this.m_cachedEffectTransform).gameObject, false);
        if (!flag)
          Object.Destroy((Object) ((Component) this.m_cachedEffectTransform).gameObject);
      }
      if (Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this).gameObject);
    }
  }

  private void OnTriggerEnter(Collider collider)
  {
    int layer = ((Component) collider).gameObject.layer;
    if ((1 << layer & this.m_ignoreLayerMask) > 0 || layer == 8 && Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null) || this.m_state == PresentBulletObject.STATE.PICKED)
      return;
    if (this.m_skillParam != null)
    {
      int healHp = this.m_skillParam.healHp;
    }
    Self component = ((Component) collider).gameObject.GetComponent<Self>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this.OnPicked();
    component.OnHealReceive(this.m_healData);
    if (!this.m_buffIds.IsNullOrEmpty<int>())
    {
      for (int index = 0; index < this.m_buffIds.Count; ++index)
        component.StartBuffByBuffTableId(this.m_buffIds[index], this.m_skillParam);
    }
    if (Object.op_Inequality((Object) component.playerSender, (Object) null))
      component.playerSender.OnPickPresentBullet(this.m_presentBulletId);
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.RemovePresentBulletObject(this.m_presentBulletId);
  }

  private enum STATE
  {
    INVALID,
    ACTIVE,
    PICKED,
  }
}
