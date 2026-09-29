// Decompiled with JetBrains decompiler
// Type: UIEnemyStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIEnemyStatus : MonoBehaviourSingleton<UIEnemyStatus>
{
  [SerializeField]
  protected UILabel enemyName;
  [SerializeField]
  protected UILabel level;
  [SerializeField]
  protected UIHGauge hpGaugeUI;
  [SerializeField]
  protected GameObject hpGaugeBase;
  [SerializeField]
  protected UIHGauge shieldGaugeUI;
  [SerializeField]
  protected UIHGauge aegisGaugeUI;
  [SerializeField]
  protected UIHGauge downGaugeUI;
  [SerializeField]
  protected float downEffectTime = 1f;
  [SerializeField]
  protected AnimationCurve downEffectEaseCurve = Curves.CreateEaseInCurve();
  [SerializeField]
  protected float downEffectAddRandomMax;
  [SerializeField]
  protected AnimationCurve downEffectAddCurve;
  [SerializeField]
  protected UIStatusIcon statusIcons;
  [SerializeField]
  protected UILabel weakRootName;
  [SerializeField]
  protected UILabel nonWeakElementName;
  [SerializeField]
  protected UISprite sprWeakElement;
  [SerializeField]
  protected UISprite sprElement;
  [SerializeField]
  protected float shieldChargeTime = 2f;
  [SerializeField]
  protected GameObject shakeTarget;
  [SerializeField]
  protected GameObject hpFocusFrame;
  [SerializeField]
  protected GameObject shadowSealingRoot;
  [SerializeField]
  protected GameObject[] shadowSealingPartitions;
  [SerializeField]
  protected UIHGauge shadowSealingGaugeUI;
  [SerializeField]
  protected GameObject concussionRoot;
  [SerializeField]
  protected UIHGauge concussionGaugeUI;
  [SerializeField]
  protected UITweenCtrl hpMultiXTween;
  [SerializeField]
  protected GameObject tutorialObj;
  [SerializeField]
  protected UILabel multiX;
  private Coroutine shakeCoroutine;
  private Vector3 shakeTargetDefaultPos = Vector3.zero;
  protected float downPercent;
  protected int playEffectCount;
  private List<GameObject> playEffects = new List<GameObject>();
  protected float shadowSealingPercent;
  protected int playShadowSealingEffectCount;
  private List<GameObject> playShadowSealingEffects = new List<GameObject>();
  protected float concussionPercent;
  protected int playConcussionEffectCount;
  private List<GameObject> playConcussionEffects = new List<GameObject>();
  protected UISprite downGaugeUISpr;
  protected bool isChangeDownGaugeSpr;
  private UIEnemyStatus.GaugeType currentGaugeType;
  private bool isLoadEffectComplete;
  private bool isPlayShieldGaugeAnimation;
  private const float kShadowSealingGaugeWidth = 150f;
  private readonly Vector3 kShadowSealingGaugeEffectScale = new Vector3(0.6f, 1f, 1f);
  private readonly Vector3 kConcussionGaugeEffectScale = new Vector3(0.66f, 1f, 1f);

  public Enemy targetEnemy { get; protected set; }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.SetActive(false);
    this.SetGaugeType(UIEnemyStatus.GaugeType.HP);
    this.SetActiveTutorialObj(false);
  }

  private void Start()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    if (Object.op_Implicit((Object) this.hpGaugeUI))
      this.hpGaugeUI.SetPercent(this.targetEnemy.hpMax > 0 ? (float) this.targetEnemy.hp / (float) this.targetEnemy.hpMax : 0.0f, false);
    if (Object.op_Implicit((Object) this.downGaugeUI))
    {
      this.downGaugeUISpr = ((Component) this.downGaugeUI).GetComponent<UISprite>();
      float percent = this.CalcDownPercent();
      this.downGaugeUI.SetPercent(percent, false);
      this.downPercent = percent;
    }
    if (Object.op_Implicit((Object) this.shadowSealingGaugeUI))
    {
      float percent = this.CalcShadowSealingPercent();
      this.shadowSealingGaugeUI.SetPercent(percent, false);
      this.shadowSealingPercent = percent;
    }
    if (Object.op_Implicit((Object) this.concussionGaugeUI))
    {
      float percent = this.CalcConcussionPercent();
      this.concussionGaugeUI.SetPercent(percent, false);
      this.concussionPercent = percent;
    }
    if (Object.op_Implicit((Object) this.shieldGaugeUI))
    {
      this.isLoadEffectComplete = false;
      this.StartCoroutine(this.DoLoadEffect());
      this.shieldGaugeUI.SetPercent(0.0f, false);
    }
    if (!Object.op_Inequality((Object) this.shakeTarget, (Object) null))
      return;
    this.shakeTargetDefaultPos = this.shakeTarget.transform.localPosition;
  }

  private IEnumerator DoLoadEffect()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_shieldgauge_01");
    while (load_queue.IsLoading())
      yield return (object) null;
    this.isLoadEffectComplete = true;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    int index1 = 0;
    for (int count = this.playEffects.Count; index1 < count; ++index1)
      EffectManager.ReleaseEffect(this.playEffects[index1]);
    this.playEffects.Clear();
    this.playEffectCount = 0;
    int index2 = 0;
    for (int count = this.playShadowSealingEffects.Count; index2 < count; ++index2)
      EffectManager.ReleaseEffect(this.playShadowSealingEffects[index2]);
    this.playShadowSealingEffects.Clear();
    this.playShadowSealingEffectCount = 0;
  }

  public void SetTarget(Enemy enemy)
  {
    this.targetEnemy = enemy;
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      this.statusIcons.target = (Character) enemy;
      if (Object.op_Implicit((Object) this.enemyName))
      {
        string charaName = enemy.charaName;
        this.enemyName.text = charaName;
        int enemyLevel = (int) enemy.enemyLevel;
        if (Object.op_Equality((Object) this.level, (Object) null) && enemyLevel > 0)
          this.enemyName.text = $"Lv{enemyLevel.ToString()} {charaName}";
      }
      if (Object.op_Implicit((Object) this.level))
        this.level.text = enemy.enemyLevel.ToString();
      if (Object.op_Implicit((Object) this.hpGaugeUI))
      {
        float percent = this.targetEnemy.hpMax > 0 ? (float) this.targetEnemy.hp / (float) this.targetEnemy.hpMax : 0.0f;
        if ((double) this.hpGaugeUI.nowPercent != (double) percent)
          this.hpGaugeUI.SetPercent(percent, false);
      }
      if (Object.op_Implicit((Object) this.downGaugeUI))
      {
        float percent = this.CalcDownPercent();
        if ((double) this.downGaugeUI.nowPercent != (double) percent)
        {
          this.downGaugeUI.SetPercent(percent, false);
          this.downPercent = percent;
        }
      }
      if (Object.op_Implicit((Object) this.shadowSealingGaugeUI))
      {
        float percent = this.CalcShadowSealingPercent();
        if ((double) this.shadowSealingGaugeUI.nowPercent != (double) percent)
        {
          this.shadowSealingGaugeUI.SetPercent(percent, false);
          this.shadowSealingPercent = percent;
        }
      }
      if (Object.op_Implicit((Object) this.concussionGaugeUI))
      {
        float percent = this.CalcConcussionPercent();
        if ((double) this.concussionGaugeUI.nowPercent != (double) percent)
        {
          this.concussionGaugeUI.SetPercent(percent, false);
          this.concussionPercent = percent;
        }
      }
      this.UpdateElementIcon();
      this.UpDateStatusIcon();
      if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
        return;
      ((Component) this).gameObject.SetActive(true);
    }
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    if (Object.op_Implicit((Object) this.hpGaugeUI))
    {
      float percent = this.targetEnemy.hpMax > 0 ? (float) this.targetEnemy.hpShow / (float) this.targetEnemy.hpMax : 0.0f;
      if ((double) this.hpGaugeUI.nowPercent != (double) percent)
        this.hpGaugeUI.SetPercent(percent);
    }
    if (Object.op_Implicit((Object) this.downGaugeUI) && (this.playEffectCount == 0 || this.targetEnemy.IsActDown()))
    {
      float percent = this.CalcDownPercent();
      if ((double) this.downGaugeUI.nowPercent != (double) percent)
      {
        this.downGaugeUI.SetPercent(percent, false);
        this.downPercent = percent;
      }
    }
    if (Object.op_Implicit((Object) this.shadowSealingGaugeUI) && (this.playShadowSealingEffectCount == 0 || this.targetEnemy.IsDebuffShadowSealing()))
    {
      float percent = this.CalcShadowSealingPercent();
      if ((double) this.shadowSealingGaugeUI.nowPercent != (double) percent)
      {
        this.shadowSealingGaugeUI.SetPercent(percent, false);
        this.shadowSealingPercent = percent;
      }
    }
    if (Object.op_Implicit((Object) this.concussionGaugeUI) && (this.playConcussionEffectCount == 0 || this.targetEnemy.IsConcussion()))
    {
      float percent = this.CalcConcussionPercent();
      if ((double) this.concussionGaugeUI.nowPercent != (double) percent)
      {
        this.concussionGaugeUI.SetPercent(percent, false);
        this.concussionPercent = percent;
      }
    }
    if (Object.op_Implicit((Object) this.shieldGaugeUI) && this.targetEnemy.IsValidShield())
    {
      if (this.currentGaugeType != UIEnemyStatus.GaugeType.SHIELD)
      {
        this.SetGaugeType(UIEnemyStatus.GaugeType.SHIELD);
      }
      else
      {
        float percent = Mathf.Max(0.0f, (float) (int) this.targetEnemy.ShieldHp / (float) (int) this.targetEnemy.ShieldHpMax);
        if ((double) this.shieldGaugeUI.nowPercent < (double) percent)
        {
          if (this.isPlayShieldGaugeAnimation)
            return;
          this.isPlayShieldGaugeAnimation = true;
          this.StartCoroutine(this.PlayShieldGaugeAnimation());
        }
        else
        {
          if ((double) this.shieldGaugeUI.nowPercent <= (double) percent || this.isPlayShieldGaugeAnimation)
            return;
          this.shieldGaugeUI.SetPercent(percent);
        }
      }
    }
    else
    {
      if (this.currentGaugeType == UIEnemyStatus.GaugeType.HP)
        return;
      this.SetGaugeType(UIEnemyStatus.GaugeType.HP);
    }
  }

  public void PlayShakeHpGauge(float power, float shakeTime, float cycleTime, bool reset = true)
  {
    if ((double) cycleTime == 0.0)
      Log.Error(LOG.INGAME, "cycleTime is 0 at PlayShakeHpGauge in UIEnemyStatus.cs");
    else if (Object.op_Equality((Object) this.shakeTarget, (Object) null))
    {
      Log.Error(LOG.INGAME, "shakeTarget is null in UIEnemyStatus:InGameMain/StaticPanel/EnemyStatus");
    }
    else
    {
      if (reset && this.shakeCoroutine != null)
      {
        this.StopCoroutine(this.shakeCoroutine);
        this.shakeCoroutine = (Coroutine) null;
      }
      if (this.shakeCoroutine != null)
        return;
      this.shakeCoroutine = this.StartCoroutine(this._PlayShakeHpGauge(power, shakeTime, cycleTime));
    }
  }

  private IEnumerator _PlayShakeHpGauge(float power, float shakeTime, float cycleTime)
  {
    float timer = 0.0f;
    while ((double) timer < (double) shakeTime)
    {
      timer += Time.deltaTime;
      this.shakeTarget.transform.localPosition = Vector3.op_Addition(this.shakeTargetDefaultPos, Vector3.op_Multiply(power * Mathf.Sin(6.28f * timer / cycleTime), Vector3.up));
      power -= power * Time.deltaTime / shakeTime;
      yield return (object) null;
    }
    this.shakeTarget.transform.localPosition = this.shakeTargetDefaultPos;
  }

  private IEnumerator PlayShieldGaugeAnimation()
  {
    Renderer component1 = ((Component) this.shieldGaugeUI).GetComponent<Renderer>();
    Renderer component2 = ((Component) this.hpGaugeUI).GetComponent<Renderer>();
    UISprite component3 = this.hpGaugeBase.GetComponent<UISprite>();
    component2.material.renderQueue = component3.material.renderQueue + 1;
    component1.material.renderQueue = component2.material.renderQueue + 1;
    Transform effectTrans = (Transform) null;
    if (this.isLoadEffectComplete)
    {
      effectTrans = EffectManager.GetUIEffect("ef_ui_shieldgauge_01", (UIWidget) component3, add_render_queue: component1.material.renderQueue + 10);
      if (Object.op_Inequality((Object) effectTrans, (Object) null))
      {
        effectTrans.rotation = Quaternion.identity;
        effectTrans.localScale = Vector3.one;
      }
    }
    float width = (float) (component3.width - component3.GetAtlasSprite().borderLeft - component3.GetAtlasSprite().borderRight);
    Vector3 offset = Vector3.op_Addition(Vector3.op_Multiply(Vector3.right, (float) component3.GetAtlasSprite().borderLeft), Vector3.op_Division(Vector3.op_Multiply(Vector3.down, (float) component3.height), 2f));
    float shieldPercent = Mathf.Max(0.0f, (float) (int) this.targetEnemy.ShieldHp / (float) (int) this.targetEnemy.ShieldHpMax);
    while ((double) this.shieldGaugeUI.nowPercent < (double) shieldPercent)
    {
      this.shieldGaugeUI.SetPercent(this.shieldGaugeUI.nowPercent + Time.deltaTime / this.shieldChargeTime);
      if (Object.op_Inequality((Object) effectTrans, (Object) null))
        effectTrans.localPosition = Vector3.op_Addition(offset, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.right, width), this.shieldGaugeUI.nowPercent));
      shieldPercent = Mathf.Max(0.0f, (float) (int) this.targetEnemy.ShieldHp / (float) (int) this.targetEnemy.ShieldHpMax);
      yield return (object) null;
    }
    yield return (object) null;
    if (Object.op_Inequality((Object) effectTrans, (Object) null))
      EffectManager.ReleaseEffect(((Component) effectTrans).gameObject);
    this.isPlayShieldGaugeAnimation = false;
  }

  public void DirectionDownGauge(AttackedHitStatusFix status)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this._DirectionDownGauge(status));
  }

  private IEnumerator _DirectionDownGauge(AttackedHitStatusFix status)
  {
    Vector3 hitPos = status.hitPos;
    float down_percent = this.targetEnemy.downMax > 0 ? this.targetEnemy.downTotal / (float) this.targetEnemy.downMax : 0.0f;
    float workDownTotal = 0.0f;
    int effect_num = (int) (((double) down_percent - (double) this.downPercent) * 10.0) + 1;
    this.downPercent = down_percent;
    ++this.playEffectCount;
    yield return (object) this.StartCoroutine(this._DirectionDownParticle(effect_num, hitPos));
    EffectManager.GetUIEffect("ef_ui_downgauge_01", this.downGaugeUI.GetGaugeTransform());
    if (!this.targetEnemy.IsActDown())
    {
      this.downGaugeUI.SetPercent(down_percent, false);
    }
    else
    {
      workDownTotal = this.targetEnemy.CalcWorkDownTotal(status.downAddBase, status.downAddWeak, status.regionID);
      this.targetEnemy.IncreaseDownTimeByAttack(workDownTotal);
    }
    yield return (object) new WaitForSeconds(1f);
    if ((double) down_percent >= 1.0 && !this.targetEnemy.IsActDown())
      yield return (object) this.StartCoroutine(this._DirectionDownCompleteEffect(false));
    else if ((double) workDownTotal > 0.0 && this.targetEnemy.IsActDown())
      yield return (object) this.StartCoroutine(this._DirectionDownCompleteEffect(true));
    --this.playEffectCount;
  }

  private IEnumerator _DirectionDownParticle(int effect_num, Vector3 world_hit_pos)
  {
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
    {
      float num = this.targetEnemy.IsActDown() ? 2f : 1f;
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos));
      List<GameObject> effects = new List<GameObject>();
      for (int index = 0; index < effect_num; ++index)
      {
        Transform uiEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", this.downGaugeUI.GetGaugeTransform());
        if (!Object.op_Equality((Object) uiEffect, (Object) null))
        {
          worldPoint.z = 1f;
          uiEffect.position = worldPoint;
          GameObject gameObject = ((Component) uiEffect).gameObject;
          TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
          if (!Object.op_Equality((Object) transformInterpolator, (Object) null))
          {
            Vector3 add_value;
            // ISSUE: explicit constructor call
            ((Vector3) ref add_value).\u002Ector(Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), 0.0f);
            transformInterpolator.Translate(this.downEffectTime / num, Vector3.zero, this.downEffectEaseCurve, add_value, this.downEffectAddCurve);
            effects.Add(gameObject);
            this.playEffects.Add(gameObject);
          }
        }
      }
      yield return (object) new WaitForSeconds(this.downEffectTime / num);
      effects.ForEach((Action<GameObject>) (obj =>
      {
        this.playEffects.Remove(obj);
        EffectManager.ReleaseEffect(obj);
      }));
    }
  }

  private IEnumerator _DirectionDownCompleteEffect(bool isInActDown)
  {
    Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", this.downGaugeUI.GetGaugeTransform());
    if (!Object.op_Equality((Object) trans, (Object) null))
    {
      this.playEffects.Add(((Component) trans).gameObject);
      bool isDownMotion = this.targetEnemy.IsActDown();
      while (isInActDown != isDownMotion)
        yield return (object) null;
      this.playEffects.Remove(((Component) trans).gameObject);
      EffectManager.ReleaseEffect(((Component) trans).gameObject);
    }
  }

  public void UpDateStatusIcon() => this.statusIcons.UpDateStatusIcon();

  private void UpdateElementIcon()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      Log.Error("targetEnemy is null !!");
    else if (this.targetEnemy.enemyTableData == null)
    {
      Log.Error("enemyData is null !!");
    }
    else
    {
      this.SetElementIcon(this.targetEnemy.changeElementIcon != ELEMENT_TYPE.MAX ? this.targetEnemy.changeElementIcon : this.targetEnemy.GetElementTypeByRegion());
      this.SetWeakElementIcon(this.targetEnemy.changeWeakElementIcon != ELEMENT_TYPE.MAX ? this.targetEnemy.changeWeakElementIcon : this.targetEnemy.GetAntiElementTypeByRegion());
    }
  }

  public void SetElementIcon(ELEMENT_TYPE element)
  {
    if (!Object.op_Inequality((Object) this.sprElement, (Object) null))
      return;
    ((Component) this.sprElement).gameObject.SetActive(false);
    string elementSpriteName = ItemIcon.GetIconElementSpriteName(element);
    if (string.IsNullOrEmpty(elementSpriteName))
      return;
    this.sprElement.spriteName = elementSpriteName;
    ((Component) this.sprElement).gameObject.SetActive(true);
  }

  public void SetWeakElementIcon(ELEMENT_TYPE weakElement)
  {
    if (Object.op_Inequality((Object) this.weakRootName, (Object) null))
      ((Component) this.weakRootName).gameObject.SetActive(true);
    if (Object.op_Inequality((Object) this.sprWeakElement, (Object) null))
      ((Component) this.sprWeakElement).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.nonWeakElementName, (Object) null))
      ((Component) this.nonWeakElementName).gameObject.SetActive(false);
    if (weakElement == ELEMENT_TYPE.MAX)
    {
      if (!Object.op_Inequality((Object) this.nonWeakElementName, (Object) null))
        return;
      ((Component) this.nonWeakElementName).gameObject.SetActive(true);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.sprWeakElement, (Object) null))
        return;
      string elementSpriteName = ItemIcon.GetIconElementSpriteName(weakElement);
      if (string.IsNullOrEmpty(elementSpriteName))
        return;
      this.sprWeakElement.spriteName = elementSpriteName;
      ((Component) this.sprWeakElement).gameObject.SetActive(true);
    }
  }

  private void SetGaugeType(UIEnemyStatus.GaugeType type)
  {
    this.currentGaugeType = type;
    if (type == UIEnemyStatus.GaugeType.HP)
    {
      ((Component) this.shieldGaugeUI).gameObject.SetActive(false);
      this.hpFocusFrame.SetActive(false);
    }
    else
    {
      if (type != UIEnemyStatus.GaugeType.SHIELD)
        return;
      ((Component) this.shieldGaugeUI).gameObject.SetActive(true);
      this.hpFocusFrame.SetActive(true);
    }
  }

  private float CalcDownPercent()
  {
    bool flag = false;
    float num = this.targetEnemy.downMax > 0 ? this.targetEnemy.downTotal / (float) this.targetEnemy.downMax : 0.0f;
    if (this.targetEnemy.IsActDown())
    {
      if (this.targetEnemy.stackBuffCtrl.GetStackCount(StackBuffController.STACK_TYPE.SNATCH) > 0)
        flag = true;
      num = this.targetEnemy.GetDownTimeRate();
    }
    if (this.isChangeDownGaugeSpr != flag && this.downGaugeUISpr != null)
    {
      this.downGaugeUISpr.spriteName = flag ? "enemy_kagenui_over" : "enemy_down_over";
      this.isChangeDownGaugeSpr = flag;
    }
    return num;
  }

  public void ShowShadowSealing(bool isVisible)
  {
    int num1 = 0;
    if (this.targetEnemy == null)
    {
      isVisible = false;
    }
    else
    {
      num1 = this.targetEnemy.GetShadowSealingNum();
      if (num1 == 0)
        isVisible = false;
    }
    this.shadowSealingRoot.SetActive(false);
    if (!isVisible)
      return;
    int shadowSealingStuckNum = this.targetEnemy.GetShadowSealingStuckNum();
    float num2 = 150f / (float) num1;
    int index = 0;
    for (int length = this.shadowSealingPartitions.Length; index < length; ++index)
    {
      if (index < num1 - 1)
      {
        this.shadowSealingPartitions[index].transform.localPosition = new Vector3((float) ((double) num2 * (double) (index + 1) - 61.5), 2f, 0.0f);
        this.shadowSealingPartitions[index].SetActive(true);
      }
      else
        this.shadowSealingPartitions[index].SetActive(false);
    }
    this.shadowSealingGaugeUI.SetPercent((float) shadowSealingStuckNum / (float) num1, false);
    this.shadowSealingRoot.SetActive(true);
  }

  private float CalcShadowSealingPercent()
  {
    if (this.targetEnemy.IsDebuffShadowSealing())
      return this.targetEnemy.GetDebuffShadowSealingTimeRate();
    int shadowSealingNum = this.targetEnemy.GetShadowSealingNum();
    int shadowSealingStuckNum = this.targetEnemy.GetShadowSealingStuckNum();
    return shadowSealingNum <= 0 && shadowSealingStuckNum <= 0 ? 0.0f : (float) shadowSealingStuckNum / (float) shadowSealingNum;
  }

  public void DirectionShadowSealingGauge(Vector3 world_hit_pos)
  {
    if (!((Component) this).gameObject.activeInHierarchy || !this.shadowSealingRoot.activeInHierarchy)
      return;
    this.StartCoroutine(this._DirectionShadowSealingGauge(world_hit_pos));
  }

  private IEnumerator _DirectionShadowSealingGauge(Vector3 world_hit_pos)
  {
    float per = this.CalcShadowSealingPercent();
    int num = (int) (((double) per - (double) this.shadowSealingPercent) * 10.0) + 1;
    this.shadowSealingPercent = per;
    ++this.playShadowSealingEffectCount;
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
    {
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos));
      List<GameObject> effects = new List<GameObject>();
      for (int index = 0; index < num; ++index)
      {
        Transform uiEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", this.shadowSealingGaugeUI.GetGaugeTransform());
        if (!Object.op_Equality((Object) uiEffect, (Object) null))
        {
          worldPoint.z = 1f;
          uiEffect.position = worldPoint;
          GameObject gameObject = ((Component) uiEffect).gameObject;
          TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
          if (!Object.op_Equality((Object) transformInterpolator, (Object) null))
          {
            Vector3 add_value;
            // ISSUE: explicit constructor call
            ((Vector3) ref add_value).\u002Ector(Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), 0.0f);
            transformInterpolator.Translate(this.downEffectTime, Vector3.zero, this.downEffectEaseCurve, add_value, this.downEffectAddCurve);
            effects.Add(gameObject);
            this.playShadowSealingEffects.Add(gameObject);
          }
        }
      }
      yield return (object) new WaitForSeconds(this.downEffectTime);
      effects.ForEach((Action<GameObject>) (obj =>
      {
        this.playShadowSealingEffects.Remove(obj);
        EffectManager.ReleaseEffect(obj);
      }));
      effects = (List<GameObject>) null;
    }
    Transform uiEffect1 = EffectManager.GetUIEffect("ef_ui_downgauge_01", this.shadowSealingGaugeUI.GetGaugeTransform());
    if (Object.op_Inequality((Object) uiEffect1, (Object) null))
      uiEffect1.localScale = this.kShadowSealingGaugeEffectScale;
    this.shadowSealingGaugeUI.SetPercent(per, false);
    yield return (object) new WaitForSeconds(1f);
    if ((double) per >= 1.0 && !this.targetEnemy.IsDebuffShadowSealing())
    {
      Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", this.shadowSealingGaugeUI.GetGaugeTransform());
      if (Object.op_Inequality((Object) trans, (Object) null))
      {
        this.playShadowSealingEffects.Add(((Component) trans).gameObject);
        while (this.targetEnemy.actionID == (Character.ACTION_ID) 19)
          yield return (object) null;
        this.playShadowSealingEffects.Remove(((Component) trans).gameObject);
        EffectManager.ReleaseEffect(((Component) trans).gameObject);
      }
      trans = (Transform) null;
    }
    --this.playShadowSealingEffectCount;
  }

  public void ShowConcussion(bool isVisible)
  {
    if (this.targetEnemy == null)
      isVisible = false;
    this.concussionRoot.SetActive(false);
    if (!isVisible)
      return;
    this.concussionGaugeUI.SetPercent(this.CalcConcussionPercent(), false);
    this.concussionRoot.SetActive(true);
  }

  private float CalcConcussionPercent()
  {
    if (this.targetEnemy.IsConcussion())
      return this.targetEnemy.GetConcussionTimeRate();
    return (double) this.targetEnemy.concussionMax <= 0.0 ? 0.0f : this.targetEnemy.concussionTotal / this.targetEnemy.concussionMax;
  }

  public void DirectionConcussionGauge(Vector3 world_hit_pos)
  {
    if (!((Component) this).gameObject.activeInHierarchy || !this.concussionRoot.activeInHierarchy)
      return;
    this.StartCoroutine(this._DirectionConcussionGauge(world_hit_pos));
  }

  private IEnumerator _DirectionConcussionGauge(Vector3 world_hit_pos)
  {
    float per = this.CalcConcussionPercent();
    int num = (int) (((double) per - (double) this.concussionPercent) * 10.0) + 1;
    this.concussionPercent = per;
    ++this.playConcussionEffectCount;
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0)
    {
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos));
      List<GameObject> effects = new List<GameObject>();
      for (int index = 0; index < num; ++index)
      {
        Transform uiEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", this.concussionGaugeUI.GetGaugeTransform());
        if (!Object.op_Equality((Object) uiEffect, (Object) null))
        {
          worldPoint.z = 1f;
          uiEffect.position = worldPoint;
          GameObject gameObject = ((Component) uiEffect).gameObject;
          TransformInterpolator transformInterpolator = gameObject.AddComponent<TransformInterpolator>();
          if (!Object.op_Equality((Object) transformInterpolator, (Object) null))
          {
            Vector3 add_value;
            // ISSUE: explicit constructor call
            ((Vector3) ref add_value).\u002Ector(Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), Random.Range(-this.downEffectAddRandomMax, this.downEffectAddRandomMax), 0.0f);
            transformInterpolator.Translate(this.downEffectTime, Vector3.zero, this.downEffectEaseCurve, add_value, this.downEffectAddCurve);
            effects.Add(gameObject);
            this.playConcussionEffects.Add(gameObject);
          }
        }
      }
      yield return (object) new WaitForSeconds(this.downEffectTime);
      effects.ForEach((Action<GameObject>) (obj =>
      {
        this.playConcussionEffects.Remove(obj);
        EffectManager.ReleaseEffect(obj);
      }));
      effects = (List<GameObject>) null;
    }
    Transform uiEffect1 = EffectManager.GetUIEffect("ef_ui_downgauge_01", this.concussionGaugeUI.GetGaugeTransform());
    if (Object.op_Inequality((Object) uiEffect1, (Object) null))
      uiEffect1.localScale = this.kConcussionGaugeEffectScale;
    this.concussionGaugeUI.SetPercent(per, false);
    yield return (object) new WaitForSeconds(1f);
    if ((double) per >= 1.0 && !this.targetEnemy.IsConcussion())
    {
      Transform trans = EffectManager.GetUIEffect("ef_ui_downgauge_02", this.concussionGaugeUI.GetGaugeTransform());
      if (Object.op_Inequality((Object) trans, (Object) null))
      {
        this.playConcussionEffects.Add(((Component) trans).gameObject);
        while (this.targetEnemy.actionID == (Character.ACTION_ID) 25)
          yield return (object) null;
        this.playConcussionEffects.Remove(((Component) trans).gameObject);
        EffectManager.ReleaseEffect(((Component) trans).gameObject);
      }
      trans = (Transform) null;
    }
    --this.playConcussionEffectCount;
  }

  public void SetAegisBarPercent(float percent)
  {
    if (this.hpFocusFrame == null)
      return;
    if ((double) percent <= 0.0)
    {
      ((Component) this.aegisGaugeUI).gameObject.SetActive(false);
      this.hpFocusFrame.SetActive(false);
    }
    else
    {
      this.aegisGaugeUI.SetPercent(percent, false);
      if (((Component) this.aegisGaugeUI).gameObject.activeSelf)
        return;
      ((Component) this.aegisGaugeUI).gameObject.SetActive(true);
      this.hpFocusFrame.SetActive(true);
    }
  }

  public void PlayShakeHpMultiX()
  {
    if (!Object.op_Inequality((Object) this.hpMultiXTween, (Object) null))
      return;
    Debug.Log((object) "Shake");
    this.hpMultiXTween.Reset();
    this.hpMultiXTween.Play();
  }

  public void SetHpMultiX(int x)
  {
    if (!Object.op_Inequality((Object) this.tutorialObj, (Object) null))
      return;
    this.multiX.text = $"x.{x}";
  }

  public void SetActiveTutorialObj(bool isActive)
  {
    if (!Object.op_Inequality((Object) this.tutorialObj, (Object) null))
      return;
    this.tutorialObj.SetActive(isActive);
    ((Component) this.weakRootName).gameObject.SetActive(!isActive);
    ((Component) this.enemyName).gameObject.SetActive(!isActive);
    ((Component) this.sprElement).gameObject.SetActive(!isActive);
  }

  public enum GaugeType
  {
    HP,
    SHIELD,
  }
}
