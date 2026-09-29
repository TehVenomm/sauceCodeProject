// Decompiled with JetBrains decompiler
// Type: FieldGimmickGeyserObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class FieldGimmickGeyserObject : FieldGimmickObject
{
  private const string ANIM_STATE_IDLE = "START1";
  private const string ANIM_STATE_READY = "START2";
  private const string ANIM_STATE_ACTION = "LOOP2";
  private readonly int IDLE_ANIM_HASH = Animator.StringToHash("START1");
  private readonly int READY_ANIM_HASH = Animator.StringToHash("START2");
  private readonly int ACTION_ANIM_HASH = Animator.StringToHash("LOOP2");
  private readonly int STATE_LENGTH = Enum.GetValues(typeof (FieldGimmickGeyserObject.STATE)).Length;
  public const string EFFECT_NAME = "ef_btl_bg_geyser_01";
  public Character.REACTION_TYPE reactionType;
  private EffectCtrl effectCtrl;
  private float INTERVAL = 5f;
  private float DURATION = 5f;
  protected CapsuleCollider actCollider;
  private Self self;
  private int selfInstanceId;
  private float timer;
  private const float COOL_TIME = 1f;

  public override string GetObjectName() => "GeyserGimmick";

  public FieldGimmickGeyserObject.STATE state { get; private set; }

  public FieldMapTable.FieldGimmickActionTableData actionData { get; protected set; }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.actionData = Singleton<FieldMapTable>.I.GetFieldGimmickActionData((uint) this.m_pointData.value1);
    Transform effect = EffectManager.GetEffect("ef_btl_bg_geyser_01", ((Component) this).transform);
    if (Object.op_Inequality((Object) effect, (Object) null))
      this.effectCtrl = ((Component) effect).GetComponent<EffectCtrl>();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      this.self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) this.self, (Object) null))
      this.selfInstanceId = ((Object) ((Component) this.self).gameObject).GetInstanceID();
    this.reactionType = this.actionData.reactionType;
    this.actCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    this.Reset();
  }

  public void Reset()
  {
    this.state = FieldGimmickGeyserObject.STATE.IDLE;
    if (this.actionData != null)
    {
      this.timer = (double) this.actionData.start >= 0.0 ? this.actionData.start : Random.value * this.INTERVAL;
      this.INTERVAL = this.actionData.interval;
      this.DURATION = this.actionData.duration;
    }
    if (Object.op_Inequality((Object) this.actCollider, (Object) null))
    {
      float num1 = this.actionData != null ? this.actionData.radius : 1f;
      float num2 = (float) ((double) num1 * 2.0 + 3.0);
      this.actCollider.radius = num1;
      this.actCollider.height = num2;
      this.actCollider.center = new Vector3(0.0f, num2 / 2f - num1, 0.0f);
      ((Collider) this.actCollider).isTrigger = true;
      ((Collider) this.actCollider).enabled = false;
    }
    ((Behaviour) this).enabled = true;
  }

  public override void RequestDestroy()
  {
    this.SetEnableAction(false);
    if (Object.op_Inequality((Object) this.effectCtrl, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.effectCtrl).gameObject);
    base.RequestDestroy();
  }

  public override void OnNotify(object value)
  {
    base.OnNotify(value);
    this.SetEnableAction(!(bool) value);
  }

  private void SetEnableAction(bool value)
  {
    if (((Behaviour) this).enabled != value)
      this.Reset();
    ((Behaviour) this).enabled = value;
    if (!Object.op_Inequality((Object) this.effectCtrl, (Object) null))
      return;
    ((Component) this.effectCtrl).gameObject.SetActive(value);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.effectCtrl, (Object) null))
      return;
    this.timer += Time.deltaTime;
    switch (this.state)
    {
      case FieldGimmickGeyserObject.STATE.IDLE:
        if ((double) this.timer <= (double) this.INTERVAL)
          break;
        this.effectCtrl.Play(this.READY_ANIM_HASH);
        this.NextState();
        break;
      case FieldGimmickGeyserObject.STATE.READY:
        if (!this.effectCtrl.IsCurrentState(this.ACTION_ANIM_HASH))
          break;
        ((Collider) this.actCollider).enabled = true;
        this.NextState();
        break;
      case FieldGimmickGeyserObject.STATE.ACTION:
        if ((double) this.timer <= (double) this.DURATION)
          break;
        ((Collider) this.actCollider).enabled = false;
        this.effectCtrl.CrossFade(this.IDLE_ANIM_HASH, 0.3f);
        this.NextState();
        break;
    }
  }

  private void NextState()
  {
    this.timer = 0.0f;
    int num = (int) (this.state + 1);
    if (num < this.STATE_LENGTH)
      this.state = (FieldGimmickGeyserObject.STATE) num;
    else
      this.state = FieldGimmickGeyserObject.STATE.IDLE;
  }

  public void SetEnableCollider(bool value)
  {
    if (!Object.op_Inequality((Object) this.actCollider, (Object) null))
      return;
    ((Collider) this.actCollider).enabled = value;
  }

  public void ReactPlayer(Player self)
  {
    self.isGatherInterruption = true;
    Vector3 vector3_1 = Vector3.op_Subtraction(self._transform.position, this.m_transform.position);
    Vector3 normalized = ((Vector3) ref vector3_1).normalized;
    switch (this.reactionType)
    {
      case Character.REACTION_TYPE.BLOW:
      case Character.REACTION_TYPE.STUNNED_BLOW:
      case Character.REACTION_TYPE.FALL_BLOW:
      case Character.REACTION_TYPE.CHARM_BLOW:
        self._forward = Vector3.op_UnaryNegation(normalized);
        break;
    }
    Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(this.actionData.angle, self._right), normalized), this.actionData.force);
    self.ActReaction(new Character.ReactionInfo()
    {
      reactionType = this.reactionType,
      blowForce = vector3_2,
      loopTime = this.actionData.loopTime,
      targetId = self.id
    }, true);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (this.selfInstanceId != ((Object) ((Component) other).gameObject).GetInstanceID() || !Object.op_Inequality((Object) this.self, (Object) null))
      return;
    bool flag = this.self.hitOffFlag == StageObject.HIT_OFF_FLAG.NONE;
    switch (this.self.actionID)
    {
      case Character.ACTION_ID.DAMAGE:
      case Character.ACTION_ID.MAX:
      case (Character.ACTION_ID) 20:
      case (Character.ACTION_ID) 33:
        flag = true;
        break;
    }
    if (this.self.isActSpecialAction)
      flag = true;
    if (!flag || !((Behaviour) this).enabled)
      return;
    this.SetEnableCollider(false);
    this.StartCoroutine(this.SetEnableCollider(true, 1f));
    this.ReactPlayer((Player) this.self);
  }

  private IEnumerator SetEnableCollider(bool value, float delay)
  {
    yield return (object) new WaitForSeconds(delay);
    if (this.state == FieldGimmickGeyserObject.STATE.ACTION)
      this.SetEnableCollider(value);
  }

  protected override void Awake()
  {
  }

  public enum STATE
  {
    IDLE,
    READY,
    ACTION,
  }
}
