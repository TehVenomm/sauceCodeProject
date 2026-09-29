// Decompiled with JetBrains decompiler
// Type: FieldHealingPointObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldHealingPointObject : FieldGimmickObject
{
  private readonly Color COLOR_CRYSTAL_IS_READY = new Color(0.0f, 0.274509817f, 0.184313729f);
  private readonly Color COLOR_CRYSTAL_IS_NOT_READY = new Color(0.117647059f, 0.117647059f, 0.117647059f);
  private readonly int STATE_END = Animator.StringToHash("END");
  private readonly int STATE_END_INCLUDE_LAYER = Animator.StringToHash("Base Layer.END");
  private readonly int STATE_HEAL_POINT_ACTIVE = Animator.StringToHash("CMN_healpoint01");
  public const string EFFECT_NAME_HEALING_POINT_FOR_BASE = "ef_btl_heal_spot_01_01";
  public const string EFFECT_NAME_HEALING_POINT_FOR_CRYSTAL = "ef_btl_heal_spot_01_02";
  public const int SE_HEALING = 30000038;
  private float m_healInterval;
  private float healTimer;
  private bool isReady = true;
  private Renderer[] rendererArray;
  private Transform effectTransForBase;
  private Transform effectTransForCrystal;
  private Animator animForModel;
  private Animator animForEffectOnBase;
  private Animator animForEffectOnCrystal;
  private Character.HealData healData;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_healInterval = pointData.value1;
    this.animForModel = ((Component) this.m_transform).GetComponentInChildren<Animator>();
    this.rendererArray = ((Component) this.m_transform).GetComponentsInChildren<Renderer>(true);
    if (this.rendererArray != null)
      this.SetReadyForHeal();
    this.healData = new Character.HealData(Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null) ? 0 : MonoBehaviourSingleton<StageObjectManager>.I.self.hpMax, HEAL_TYPE.ALL_BADSTATUS, HEAL_EFFECT_TYPE.BASIS, (List<int>) null);
  }

  private void SetReadyForHeal()
  {
    this.isReady = true;
    this.healTimer = 0.0f;
    if (Object.op_Inequality((Object) this.effectTransForBase, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.effectTransForBase).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.effectTransForBase).gameObject);
      this.effectTransForBase = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.effectTransForCrystal, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.effectTransForCrystal).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.effectTransForCrystal).gameObject);
      this.effectTransForCrystal = (Transform) null;
    }
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      this.effectTransForBase = EffectManager.GetEffect("ef_btl_heal_spot_01_01", this.modelTrans.Find("base01"));
      this.effectTransForCrystal = EffectManager.GetEffect("ef_btl_heal_spot_01_02", this.modelTrans.Find("crystal01"));
      if (Object.op_Inequality((Object) this.effectTransForBase, (Object) null))
        this.animForEffectOnBase = ((Component) this.effectTransForBase).GetComponent<Animator>();
      if (Object.op_Inequality((Object) this.effectTransForCrystal, (Object) null))
        this.animForEffectOnCrystal = ((Component) this.effectTransForCrystal).GetComponent<Animator>();
    }
    if (Object.op_Inequality((Object) this.animForModel, (Object) null))
      this.animForModel.Play(this.STATE_HEAL_POINT_ACTIVE, 0);
    if (this.rendererArray == null)
      return;
    Utility.MaterialForEach(this.rendererArray, (Action<Material>) (material =>
    {
      if (!material.HasProperty("_SpeLightColor"))
        return;
      material.SetColor("_SpeLightColor", this.COLOR_CRYSTAL_IS_READY);
    }));
  }

  private void SetNotReadyForHeal()
  {
    this.isReady = false;
    if (Object.op_Inequality((Object) this.animForModel, (Object) null))
      this.animForModel.Play(this.STATE_END, 0);
    this.StartCoroutine(this.EndEffectOnBase());
    this.StartCoroutine(this.EndEffectOnCrystal());
    if (this.rendererArray == null)
      return;
    Utility.MaterialForEach(this.rendererArray, (Action<Material>) (material =>
    {
      if (!material.HasProperty("_SpeLightColor"))
        return;
      material.SetColor("_SpeLightColor", this.COLOR_CRYSTAL_IS_NOT_READY);
    }));
  }

  private IEnumerator EndEffectOnBase()
  {
    if (!Object.op_Equality((Object) this.animForEffectOnBase, (Object) null))
    {
      this.animForEffectOnBase.Play(this.STATE_END, 0, 0.0f);
      yield return (object) null;
      AnimatorStateInfo animatorStateInfo;
      while (true)
      {
        animatorStateInfo = this.animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != this.STATE_END_INCLUDE_LAYER)
          yield return (object) null;
        else
          break;
      }
      while (true)
      {
        animatorStateInfo = this.animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime <= 1.0)
          yield return (object) null;
        else
          break;
      }
      if (Object.op_Inequality((Object) this.effectTransForBase, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.effectTransForBase).gameObject);
        this.effectTransForBase = (Transform) null;
      }
    }
  }

  private IEnumerator EndEffectOnCrystal()
  {
    if (!Object.op_Equality((Object) this.animForEffectOnCrystal, (Object) null))
    {
      this.animForEffectOnCrystal.Play(this.STATE_END, 0, 0.0f);
      yield return (object) null;
      AnimatorStateInfo animatorStateInfo;
      while (true)
      {
        animatorStateInfo = this.animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != this.STATE_END_INCLUDE_LAYER)
          yield return (object) null;
        else
          break;
      }
      while (true)
      {
        animatorStateInfo = this.animForEffectOnCrystal.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime <= 1.0)
          yield return (object) null;
        else
          break;
      }
      if (Object.op_Inequality((Object) this.effectTransForCrystal, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.effectTransForCrystal).gameObject);
        this.effectTransForCrystal = (Transform) null;
      }
    }
  }

  private void Update()
  {
    if (!this.isReady)
      this.healTimer += Time.deltaTime;
    if ((double) this.m_healInterval > (double) this.healTimer)
      return;
    this.SetReadyForHeal();
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) ((Component) collider).gameObject.GetComponent<Self>(), (Object) null) || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null) || self.isDead || !this.isReady)
      return;
    self.OnHealReceive(this.healData);
    SoundManager.PlayOneShotSE(30000038, self._position);
    this.SetNotReadyForHeal();
  }

  public override void RequestDestroy()
  {
    if (Object.op_Inequality((Object) this.effectTransForBase, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.effectTransForBase).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.effectTransForBase).gameObject);
      this.effectTransForBase = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.effectTransForCrystal, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.effectTransForCrystal).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.effectTransForCrystal).gameObject);
      this.effectTransForCrystal = (Transform) null;
    }
    base.RequestDestroy();
  }

  public override string GetObjectName() => "HealingPoint";
}
