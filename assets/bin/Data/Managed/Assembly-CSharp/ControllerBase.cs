using System;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
	[Flags]
	public enum DISABLE_FLAG
	{
		NONE = 0x0,
		DEFAULT = 0x1,
		INPUT_DISABLE = 0x2,
		BATTLE_START = 0x4,
		BATTLE_END = 0x8,
		HAPPEN_QUEST = 0x8,
		CHANGE_UNIQUE_EQUIPMENT = 0x10
	}

	protected Character character;

	public Brain brain
	{
		get;
		private set;
	}

	public DISABLE_FLAG disableFlag
	{
		get;
		private set;
	}

	public ControllerBase()
		: this()
	{
		disableFlag = DISABLE_FLAG.NONE;
	}

	public void InitializeDisableFlag()
	{
		disableFlag = DISABLE_FLAG.NONE;
	}

	protected virtual void Awake()
	{
		character = this.GetComponentInParent<Character>();
		if (character != null)
		{
			character.controller = this;
			if (character.isLoading)
			{
				this.set_enabled(false);
			}
		}
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
	}

	protected virtual T AttachBrain<T>() where T : Brain
	{
		brain = this.get_gameObject().GetComponent<T>();
		if (brain == null)
		{
			brain = this.get_gameObject().AddComponent<T>();
		}
		return brain as T;
	}

	public virtual bool IsEnableControll()
	{
		return disableFlag == DISABLE_FLAG.NONE;
	}

	public virtual void SetEnableControll(bool enable, DISABLE_FLAG flag = DISABLE_FLAG.DEFAULT)
	{
		bool flag2 = IsEnableControll();
		if (enable)
		{
			disableFlag &= ~flag;
		}
		else
		{
			disableFlag |= flag;
		}
		if (flag2 != IsEnableControll())
		{
			OnChangeEnableControll(IsEnableControll());
		}
	}

	public virtual void OnChangeEnableControll(bool enable)
	{
		if (!enable && character.actionID == Character.ACTION_ID.MOVE)
		{
			character.ActIdle();
		}
	}

	protected virtual void OnEnable()
	{
		if (brain != null)
		{
			brain.set_enabled(true);
		}
		if (IsEnableControll())
		{
			OnChangeEnableControll(enable: true);
		}
	}

	protected virtual void OnDisable()
	{
		if (brain != null)
		{
			brain.set_enabled(false);
		}
		if (IsEnableControll())
		{
			OnChangeEnableControll(enable: false);
		}
	}

	public virtual void OnDetachedObject(StageObject stage_object)
	{
		if (brain != null)
		{
			brain.HandleEvent(BRAIN_EVENT.DESTROY_OBJECT, stage_object);
		}
	}

	public virtual void OnCharacterInitialized()
	{
		if (brain != null)
		{
			brain.Initialize();
		}
	}

	public virtual void OnCharacterPlayMotion(int motion_id)
	{
		if (brain != null)
		{
			brain.HandleEvent(BRAIN_EVENT.PLAY_MOTION, motion_id);
		}
	}

	public virtual void OnCharacterEndAction(int action_id)
	{
		if (brain != null)
		{
			brain.HandleEvent(BRAIN_EVENT.END_ACTION, action_id);
		}
	}

	public virtual void OnCharacterAttackedHitOwner(AttackedHitStatusOwner status)
	{
		if (brain != null)
		{
			if (status.downAddWeak > 0f)
			{
				brain.HandleEvent(BRAIN_EVENT.ATTACKED_WEAK_POINT, status);
			}
			else
			{
				brain.HandleEvent(BRAIN_EVENT.ATTACKED_HIT, status);
			}
		}
	}

	public virtual void OnActReaction()
	{
	}
}
