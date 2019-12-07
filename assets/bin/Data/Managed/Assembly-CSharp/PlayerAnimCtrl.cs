using System;
using UnityEngine;

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

	public Animator animator
	{
		get;
		private set;
	}

	public PLCA playingAnim
	{
		get;
		private set;
	}

	public PLCA defaultAnim
	{
		get;
		set;
	}

	public PLCA moveAnim
	{
		get;
		set;
	}

	public float transitionDuration
	{
		get;
		set;
	}

	public Action<PlayerAnimCtrl, PLCA> onPlay
	{
		get;
		set;
	}

	public Action<PlayerAnimCtrl, PLCA> onChange
	{
		get;
		set;
	}

	public Action<PlayerAnimCtrl, PLCA> onEnd
	{
		get;
		set;
	}

	public static PlayerAnimCtrl Get(Animator _animator, PLCA default_anim, Action<PlayerAnimCtrl, PLCA> on_play = null, Action<PlayerAnimCtrl, PLCA> on_change = null, Action<PlayerAnimCtrl, PLCA> on_end = null)
	{
		if (_animator == null)
		{
			return null;
		}
		InitTable();
		PlayerAnimCtrl playerAnimCtrl = _animator.GetComponent<PlayerAnimCtrl>();
		if (playerAnimCtrl == null)
		{
			playerAnimCtrl = _animator.gameObject.AddComponent<PlayerAnimCtrl>();
		}
		playerAnimCtrl.animator = _animator;
		playerAnimCtrl.onPlay = on_play;
		playerAnimCtrl.onChange = on_change;
		playerAnimCtrl.onEnd = on_end;
		playerAnimCtrl.transitionDuration = 0.1f;
		playerAnimCtrl.defaultAnim = default_anim;
		playerAnimCtrl.Play(default_anim, instant: true);
		return playerAnimCtrl;
	}

	private static void InitTable()
	{
		if (animStateNames == null)
		{
			animStateNames = Enum.GetNames(typeof(PLCA));
			animStateHashs = new int[animStateNames.Length];
			int i = 0;
			for (int num = animStateNames.Length; i < num; i++)
			{
				animStateHashs[i] = Animator.StringToHash("Base Layer." + animStateNames[i]);
			}
		}
	}

	public static PLCA StringToEnum(string state_name)
	{
		InitTable();
		int i = 0;
		for (int num = animStateNames.Length; i < num; i++)
		{
			if (animStateNames[i] == state_name)
			{
				return (PLCA)i;
			}
		}
		return PLCA.IDLE;
	}

	private void Awake()
	{
		moveAnim = PLCA.WALK;
	}

	private void FixedUpdate()
	{
		UpdateAnim();
	}

	private void UpdateAnim()
	{
		if (animator == null || animator.runtimeAnimatorController == null)
		{
			return;
		}
		AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);
		int num = animStateHashs[68];
		if (currentAnimatorStateInfo.fullPathHash == num || nextAnimatorStateInfo.fullPathHash == num)
		{
			PLCA playingAnim;
			if (lastAnimHash == 0)
			{
				playingAnim = this.playingAnim;
				Play(defaultAnim);
			}
			else
			{
				playingAnim = lastPlayingAnim;
				Play(this.playingAnim);
			}
			if (onEnd != null)
			{
				onEnd(this, playingAnim);
			}
		}
		num = animStateHashs[(int)this.playingAnim];
		if (currentAnimatorStateInfo.fullPathHash != num && nextAnimatorStateInfo.fullPathHash != num && currentAnimatorStateInfo.fullPathHash != viaAnimHash && (loopAnimHash == 0 || (currentAnimatorStateInfo.fullPathHash != loopAnimHash && nextAnimatorStateInfo.fullPathHash != loopAnimHash)) && (lastAnimHash == 0 || (currentAnimatorStateInfo.fullPathHash != lastAnimHash && nextAnimatorStateInfo.fullPathHash != lastAnimHash)))
		{
			PlayAnimator(this.playingAnim);
			if (onChange != null)
			{
				onChange(this, this.playingAnim);
			}
		}
	}

	public void SetMoveRunAnim(int sex)
	{
		moveAnim = ((sex == 0) ? PLCA.RUN : PLCA.RUN_F);
	}

	private void PlayAnimator(PLCA anim, bool instant = false)
	{
		if (animator.HasState(0, animStateHashs[(int)anim]))
		{
			string stateName = animStateNames[(int)anim];
			if (instant)
			{
				animator.Play(stateName);
			}
			else
			{
				animator.CrossFade(stateName, transitionDuration, 0);
			}
		}
	}

	public void Play(PLCA anim, bool instant = false)
	{
		if (playingAnim == anim)
		{
			lastAnimHash = 0;
			return;
		}
		if (instant)
		{
			PlayAnimator(anim, instant);
		}
		else if (loopAnimHash != 0 && endAnimHash != 0)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
			AnimatorStateInfo nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.fullPathHash == loopAnimHash || nextAnimatorStateInfo.fullPathHash == loopAnimHash)
			{
				animator.CrossFade(endAnimHash, transitionDuration, 0);
				lastAnimHash = endAnimHash;
				lastPlayingAnim = playingAnim;
			}
		}
		string str = animStateNames[(int)anim];
		viaAnimHash = Animator.StringToHash("Base Layer." + str + "_VIA");
		if (!animator.HasState(0, viaAnimHash))
		{
			viaAnimHash = 0;
		}
		loopAnimHash = Animator.StringToHash("Base Layer." + str + "_LOOP");
		if (!animator.HasState(0, loopAnimHash))
		{
			loopAnimHash = 0;
		}
		if (loopAnimHash != 0)
		{
			endAnimHash = Animator.StringToHash("Base Layer." + str + "_END");
			if (!animator.HasState(0, endAnimHash))
			{
				endAnimHash = 0;
			}
		}
		playingAnim = anim;
		if (onPlay != null)
		{
			onPlay(this, anim);
		}
	}

	public void PlayDefault(bool instant = false)
	{
		Play(defaultAnim, instant);
	}

	public void PlayMove(bool instant = false)
	{
		Play(moveAnim, instant);
	}

	public void Play(PLCA[] anims, bool instant = false)
	{
		if (!IsPlaying(anims))
		{
			Play(anims[UnityEngine.Random.Range(0, anims.Length)], instant);
		}
	}

	public void PlayIdleAnims(int sex, bool instant = false)
	{
		PLCA[] anims = (sex == 0) ? idleAnims_m : idleAnims_f;
		Play(anims, instant);
	}

	public void PlayRunAnim(int sex, bool instant = false)
	{
		Play((sex == 0) ? PLCA.RUN : PLCA.RUN_F, instant);
	}

	public bool IsPlaying(PLCA[] anims)
	{
		PLCA playingAnim = this.playingAnim;
		int i = 0;
		for (int num = anims.Length; i < num; i++)
		{
			if (anims[i] == playingAnim)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsPlayingIdleAnims(int sex)
	{
		PLCA[] anims = (sex == 0) ? idleAnims_m : idleAnims_f;
		return IsPlaying(anims);
	}

	public bool IsCurrentState(PLCA anim)
	{
		if (PLCA.NORMAL > anim || (int)anim >= animStateHashs.Length)
		{
			return false;
		}
		if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != animStateHashs[(int)anim])
		{
			return false;
		}
		return true;
	}
}
