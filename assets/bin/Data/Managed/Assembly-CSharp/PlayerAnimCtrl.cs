// Decompiled with JetBrains decompiler
// Type: PlayerAnimCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class PlayerAnimCtrl : MonoBehaviour
{
  public static string[] animStateNames;
  public static int[] animStateHashs;
  public const string BASE_LAYER = "Base Layer.";
  public static readonly PLCA[] idleAnims_m = new PLCA[3]
  {
    PLCA.IDLE_01,
    PLCA.IDLE_02,
    PLCA.IDLE_03
  };
  public static readonly PLCA[] idleAnims_f = new PLCA[3]
  {
    PLCA.IDLE_01_F,
    PLCA.IDLE_02,
    PLCA.IDLE_03
  };
  public static readonly PLCA[] emotionAnims = new PLCA[3]
  {
    PLCA.EMOTION_01,
    PLCA.EMOTION_02,
    PLCA.EMOTION_03
  };
  public static readonly PLCA[] talkAnims = new PLCA[2]
  {
    PLCA.TALK_01,
    PLCA.TALK_02
  };
  public static readonly PLCA[] battleAnims = new PLCA[6]
  {
    PLCA.BATTLE_00,
    PLCA.BATTLE_01,
    PLCA.BATTLE_02,
    PLCA.END,
    PLCA.BATTLE_04,
    PLCA.BATTLE_05
  };
  private int viaAnimHash;
  private int loopAnimHash;
  private int endAnimHash;
  private int lastAnimHash;
  private PLCA lastPlayingAnim;

  public static PlayerAnimCtrl Get(
    Animator _animator,
    PLCA default_anim,
    Action<PlayerAnimCtrl, PLCA> on_play = null,
    Action<PlayerAnimCtrl, PLCA> on_change = null,
    Action<PlayerAnimCtrl, PLCA> on_end = null)
  {
    if (Object.op_Equality((Object) _animator, (Object) null))
      return (PlayerAnimCtrl) null;
    PlayerAnimCtrl.InitTable();
    PlayerAnimCtrl playerAnimCtrl = ((Component) _animator).GetComponent<PlayerAnimCtrl>();
    if (Object.op_Equality((Object) playerAnimCtrl, (Object) null))
      playerAnimCtrl = ((Component) _animator).gameObject.AddComponent<PlayerAnimCtrl>();
    playerAnimCtrl.animator = _animator;
    playerAnimCtrl.onPlay = on_play;
    playerAnimCtrl.onChange = on_change;
    playerAnimCtrl.onEnd = on_end;
    playerAnimCtrl.transitionDuration = 0.1f;
    playerAnimCtrl.defaultAnim = default_anim;
    playerAnimCtrl.Play(default_anim, true);
    return playerAnimCtrl;
  }

  private static void InitTable()
  {
    if (PlayerAnimCtrl.animStateNames != null)
      return;
    PlayerAnimCtrl.animStateNames = Enum.GetNames(typeof (PLCA));
    PlayerAnimCtrl.animStateHashs = new int[PlayerAnimCtrl.animStateNames.Length];
    int index = 0;
    for (int length = PlayerAnimCtrl.animStateNames.Length; index < length; ++index)
      PlayerAnimCtrl.animStateHashs[index] = Animator.StringToHash("Base Layer." + PlayerAnimCtrl.animStateNames[index]);
  }

  public static PLCA StringToEnum(string state_name)
  {
    PlayerAnimCtrl.InitTable();
    int index = 0;
    for (int length = PlayerAnimCtrl.animStateNames.Length; index < length; ++index)
    {
      if (PlayerAnimCtrl.animStateNames[index] == state_name)
        return (PLCA) index;
    }
    return PLCA.IDLE;
  }

  public Animator animator { get; private set; }

  public PLCA playingAnim { get; private set; }

  public PLCA defaultAnim { get; set; }

  public PLCA moveAnim { get; set; }

  public float transitionDuration { get; set; }

  public Action<PlayerAnimCtrl, PLCA> onPlay { get; set; }

  public Action<PlayerAnimCtrl, PLCA> onChange { get; set; }

  public Action<PlayerAnimCtrl, PLCA> onEnd { get; set; }

  private void Awake() => this.moveAnim = PLCA.WALK;

  private void FixedUpdate() => this.UpdateAnim();

  private void UpdateAnim()
  {
    if (Object.op_Equality((Object) this.animator, (Object) null) || Object.op_Equality((Object) this.animator.runtimeAnimatorController, (Object) null))
      return;
    AnimatorStateInfo animatorStateInfo1 = this.animator.GetCurrentAnimatorStateInfo(0);
    AnimatorStateInfo animatorStateInfo2 = this.animator.GetNextAnimatorStateInfo(0);
    int num1 = PlayerAnimCtrl.animStateHashs[68];
    if (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == num1 || ((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash == num1)
    {
      PLCA plca;
      if (this.lastAnimHash == 0)
      {
        plca = this.playingAnim;
        this.Play(this.defaultAnim);
      }
      else
      {
        plca = this.lastPlayingAnim;
        this.Play(this.playingAnim);
      }
      if (this.onEnd != null)
        this.onEnd(this, plca);
    }
    int num2 = PlayerAnimCtrl.animStateHashs[(int) this.playingAnim];
    if (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == num2 || ((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash == num2 || ((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == this.viaAnimHash || this.loopAnimHash != 0 && (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == this.loopAnimHash || ((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash == this.loopAnimHash) || this.lastAnimHash != 0 && (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == this.lastAnimHash || ((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash == this.lastAnimHash))
      return;
    this.PlayAnimator(this.playingAnim);
    if (this.onChange == null)
      return;
    this.onChange(this, this.playingAnim);
  }

  public void SetMoveRunAnim(int sex) => this.moveAnim = sex == 0 ? PLCA.RUN : PLCA.RUN_F;

  private void PlayAnimator(PLCA anim, bool instant = false)
  {
    if (!this.animator.HasState(0, PlayerAnimCtrl.animStateHashs[(int) anim]))
      return;
    string animStateName = PlayerAnimCtrl.animStateNames[(int) anim];
    if (instant)
      this.animator.Play(animStateName);
    else
      this.animator.CrossFade(animStateName, this.transitionDuration, 0);
  }

  public void Play(PLCA anim, bool instant = false)
  {
    if (this.playingAnim == anim)
    {
      this.lastAnimHash = 0;
    }
    else
    {
      if (instant)
        this.PlayAnimator(anim, instant);
      else if (this.loopAnimHash != 0 && this.endAnimHash != 0)
      {
        AnimatorStateInfo animatorStateInfo1 = this.animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo animatorStateInfo2 = this.animator.GetNextAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == this.loopAnimHash || ((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash == this.loopAnimHash)
        {
          this.animator.CrossFade(this.endAnimHash, this.transitionDuration, 0);
          this.lastAnimHash = this.endAnimHash;
          this.lastPlayingAnim = this.playingAnim;
        }
      }
      string animStateName = PlayerAnimCtrl.animStateNames[(int) anim];
      this.viaAnimHash = Animator.StringToHash($"Base Layer.{animStateName}_VIA");
      if (!this.animator.HasState(0, this.viaAnimHash))
        this.viaAnimHash = 0;
      this.loopAnimHash = Animator.StringToHash($"Base Layer.{animStateName}_LOOP");
      if (!this.animator.HasState(0, this.loopAnimHash))
        this.loopAnimHash = 0;
      if (this.loopAnimHash != 0)
      {
        this.endAnimHash = Animator.StringToHash($"Base Layer.{animStateName}_END");
        if (!this.animator.HasState(0, this.endAnimHash))
          this.endAnimHash = 0;
      }
      this.playingAnim = anim;
      if (this.onPlay == null)
        return;
      this.onPlay(this, anim);
    }
  }

  public void PlayDefault(bool instant = false) => this.Play(this.defaultAnim, instant);

  public void PlayMove(bool instant = false) => this.Play(this.moveAnim, instant);

  public void Play(PLCA[] anims, bool instant = false)
  {
    if (this.IsPlaying(anims))
      return;
    this.Play(anims[Random.Range(0, anims.Length)], instant);
  }

  public void PlayIdleAnims(int sex, bool instant = false)
  {
    this.Play(sex == 0 ? PlayerAnimCtrl.idleAnims_m : PlayerAnimCtrl.idleAnims_f, instant);
  }

  public void PlayRunAnim(int sex, bool instant = false)
  {
    this.Play(sex == 0 ? PLCA.RUN : PLCA.RUN_F, instant);
  }

  public bool IsPlaying(PLCA[] anims)
  {
    PLCA playingAnim = this.playingAnim;
    int index = 0;
    for (int length = anims.Length; index < length; ++index)
    {
      if (anims[index] == playingAnim)
        return true;
    }
    return false;
  }

  public bool IsPlayingIdleAnims(int sex)
  {
    return this.IsPlaying(sex == 0 ? PlayerAnimCtrl.idleAnims_m : PlayerAnimCtrl.idleAnims_f);
  }

  public bool IsCurrentState(PLCA anim)
  {
    int index = (int) anim;
    if (0 > index || index >= PlayerAnimCtrl.animStateHashs.Length)
      return false;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    return ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash == PlayerAnimCtrl.animStateHashs[index];
  }
}
