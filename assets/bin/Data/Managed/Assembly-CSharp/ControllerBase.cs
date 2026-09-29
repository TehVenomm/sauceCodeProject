// Decompiled with JetBrains decompiler
// Type: ControllerBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class ControllerBase : MonoBehaviour
{
  protected Character character;

  public Brain brain { get; private set; }

  public ControllerBase.DISABLE_FLAG disableFlag { get; private set; }

  public ControllerBase() => this.disableFlag = ControllerBase.DISABLE_FLAG.NONE;

  public void InitializeDisableFlag() => this.disableFlag = ControllerBase.DISABLE_FLAG.NONE;

  protected virtual void Awake()
  {
    this.character = ((Component) this).GetComponentInParent<Character>();
    if (!Object.op_Inequality((Object) this.character, (Object) null))
      return;
    this.character.controller = this;
    if (!this.character.isLoading)
      return;
    ((Behaviour) this).enabled = false;
  }

  protected virtual void Start()
  {
  }

  protected virtual void Update()
  {
  }

  protected virtual T AttachBrain<T>() where T : Brain
  {
    this.brain = (Brain) ((Component) this).gameObject.GetComponent<T>();
    if (Object.op_Equality((Object) this.brain, (Object) null))
      this.brain = (Brain) ((Component) this).gameObject.AddComponent<T>();
    return this.brain as T;
  }

  public virtual bool IsEnableControll() => this.disableFlag == ControllerBase.DISABLE_FLAG.NONE;

  public virtual void SetEnableControll(bool enable, ControllerBase.DISABLE_FLAG flag = ControllerBase.DISABLE_FLAG.DEFAULT)
  {
    int num1 = this.IsEnableControll() ? 1 : 0;
    if (enable)
      this.disableFlag &= ~flag;
    else
      this.disableFlag |= flag;
    int num2 = this.IsEnableControll() ? 1 : 0;
    if (num1 == num2)
      return;
    this.OnChangeEnableControll(this.IsEnableControll());
  }

  public virtual void OnChangeEnableControll(bool enable)
  {
    if (enable || this.character.actionID != Character.ACTION_ID.MOVE)
      return;
    this.character.ActIdle();
  }

  protected virtual void OnEnable()
  {
    if (Object.op_Inequality((Object) this.brain, (Object) null))
      ((Behaviour) this.brain).enabled = true;
    if (!this.IsEnableControll())
      return;
    this.OnChangeEnableControll(true);
  }

  protected virtual void OnDisable()
  {
    if (Object.op_Inequality((Object) this.brain, (Object) null))
      ((Behaviour) this.brain).enabled = false;
    if (!this.IsEnableControll())
      return;
    this.OnChangeEnableControll(false);
  }

  public virtual void OnDetachedObject(StageObject stage_object)
  {
    if (!Object.op_Inequality((Object) this.brain, (Object) null))
      return;
    this.brain.HandleEvent(BRAIN_EVENT.DESTROY_OBJECT, (object) stage_object);
  }

  public virtual void OnCharacterInitialized()
  {
    if (!Object.op_Inequality((Object) this.brain, (Object) null))
      return;
    this.brain.Initialize();
  }

  public virtual void OnCharacterPlayMotion(int motion_id)
  {
    if (!Object.op_Inequality((Object) this.brain, (Object) null))
      return;
    this.brain.HandleEvent(BRAIN_EVENT.PLAY_MOTION, (object) motion_id);
  }

  public virtual void OnCharacterEndAction(int action_id)
  {
    if (!Object.op_Inequality((Object) this.brain, (Object) null))
      return;
    this.brain.HandleEvent(BRAIN_EVENT.END_ACTION, (object) action_id);
  }

  public virtual void OnCharacterAttackedHitOwner(AttackedHitStatusOwner status)
  {
    if (!Object.op_Inequality((Object) this.brain, (Object) null))
      return;
    if ((double) status.downAddWeak > 0.0)
      this.brain.HandleEvent(BRAIN_EVENT.ATTACKED_WEAK_POINT, (object) status);
    else
      this.brain.HandleEvent(BRAIN_EVENT.ATTACKED_HIT, (object) status);
  }

  public virtual void OnActReaction()
  {
  }

  [Flags]
  public enum DISABLE_FLAG
  {
    NONE = 0,
    DEFAULT = 1,
    INPUT_DISABLE = 2,
    BATTLE_START = 4,
    BATTLE_END = 8,
    HAPPEN_QUEST = BATTLE_END, // 0x00000008
    CHANGE_UNIQUE_EQUIPMENT = 16, // 0x00000010
  }
}
