using System;
using System.Collections;
using UnityEngine;

public class FieldGimmickGeyserObject : FieldGimmickObject
{
	public enum STATE
	{
		IDLE,
		READY,
		ACTION
	}

	private const string ANIM_STATE_IDLE = "START1";

	private const string ANIM_STATE_READY = "START2";

	private const string ANIM_STATE_ACTION = "LOOP2";

	public const string EFFECT_NAME = "ef_btl_bg_geyser_01";

	private const float COOL_TIME = 1f;

	private readonly int IDLE_ANIM_HASH = Animator.StringToHash("START1");

	private readonly int READY_ANIM_HASH = Animator.StringToHash("START2");

	private readonly int ACTION_ANIM_HASH = Animator.StringToHash("LOOP2");

	private readonly int STATE_LENGTH = Enum.GetValues(typeof(STATE)).Length;

	public Character.REACTION_TYPE reactionType;

	private EffectCtrl effectCtrl;

	private float INTERVAL = 5f;

	private float DURATION = 5f;

	protected CapsuleCollider actCollider;

	private Self self;

	private int selfInstanceId;

	private float timer;

	public STATE state
	{
		get;
		private set;
	}

	public FieldMapTable.FieldGimmickActionTableData actionData
	{
		get;
		protected set;
	}

	public override string GetObjectName()
	{
		return "GeyserGimmick";
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		actionData = Singleton<FieldMapTable>.I.GetFieldGimmickActionData((uint)base.m_pointData.value1);
		Transform effect = EffectManager.GetEffect("ef_btl_bg_geyser_01", base.transform);
		if ((UnityEngine.Object)effect != (UnityEngine.Object)null)
		{
			effectCtrl = effect.GetComponent<EffectCtrl>();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		}
		if ((UnityEngine.Object)self != (UnityEngine.Object)null)
		{
			selfInstanceId = self.gameObject.GetInstanceID();
		}
		reactionType = actionData.reactionType;
		actCollider = base.gameObject.AddComponent<CapsuleCollider>();
		Reset();
	}

	public void Reset()
	{
		state = STATE.IDLE;
		if (actionData != null)
		{
			if (actionData.start < 0f)
			{
				timer = UnityEngine.Random.value * INTERVAL;
			}
			else
			{
				timer = actionData.start;
			}
			INTERVAL = actionData.interval;
			DURATION = actionData.duration;
		}
		if ((UnityEngine.Object)actCollider != (UnityEngine.Object)null)
		{
			float num = (actionData == null) ? 1f : actionData.radius;
			float num2 = num * 2f + 3f;
			actCollider.radius = num;
			actCollider.height = num2;
			actCollider.center = new Vector3(0f, num2 / 2f - num, 0f);
			actCollider.isTrigger = true;
			actCollider.enabled = false;
		}
		base.enabled = true;
	}

	public override void RequestDestroy()
	{
		SetEnableAction(false);
		if ((UnityEngine.Object)effectCtrl != (UnityEngine.Object)null)
		{
			EffectManager.ReleaseEffect(effectCtrl.gameObject, true, false);
		}
		base.RequestDestroy();
	}

	public override void OnNotify(object value)
	{
		base.OnNotify(value);
		bool flag = (bool)value;
		SetEnableAction(!flag);
	}

	private void SetEnableAction(bool value)
	{
		if (base.enabled != value)
		{
			Reset();
		}
		base.enabled = value;
		if ((UnityEngine.Object)effectCtrl != (UnityEngine.Object)null)
		{
			effectCtrl.gameObject.SetActive(value);
		}
	}

	private void Update()
	{
		if (!((UnityEngine.Object)effectCtrl == (UnityEngine.Object)null))
		{
			timer += Time.deltaTime;
			switch (state)
			{
			case STATE.IDLE:
				if (timer > INTERVAL)
				{
					effectCtrl.Play(READY_ANIM_HASH);
					NextState();
				}
				break;
			case STATE.READY:
				if (effectCtrl.IsCurrentState(ACTION_ANIM_HASH))
				{
					actCollider.enabled = true;
					NextState();
				}
				break;
			case STATE.ACTION:
				if (timer > DURATION)
				{
					actCollider.enabled = false;
					effectCtrl.CrossFade(IDLE_ANIM_HASH, 0.3f);
					NextState();
				}
				break;
			}
		}
	}

	private void NextState()
	{
		timer = 0f;
		int num = (int)(state + 1);
		if (num < STATE_LENGTH)
		{
			state = (STATE)num;
		}
		else
		{
			state = STATE.IDLE;
		}
	}

	public void SetEnableCollider(bool value)
	{
		if ((UnityEngine.Object)actCollider != (UnityEngine.Object)null)
		{
			actCollider.enabled = value;
		}
	}

	public void ReactPlayer(Player self)
	{
		self.isGatherInterruption = true;
		Vector3 normalized = (self._transform.position - m_transform.position).normalized;
		switch (reactionType)
		{
		case Character.REACTION_TYPE.BLOW:
		case Character.REACTION_TYPE.STUNNED_BLOW:
		case Character.REACTION_TYPE.FALL_BLOW:
			self._forward = -normalized;
			break;
		}
		normalized = Quaternion.AngleAxis(actionData.angle, self._right) * normalized;
		normalized *= actionData.force;
		Character.ReactionInfo reactionInfo = new Character.ReactionInfo();
		reactionInfo.reactionType = reactionType;
		reactionInfo.blowForce = normalized;
		reactionInfo.loopTime = actionData.loopTime;
		reactionInfo.targetId = self.id;
		self.ActReaction(reactionInfo, true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (selfInstanceId == other.gameObject.GetInstanceID() && (UnityEngine.Object)self != (UnityEngine.Object)null)
		{
			bool flag = self.hitOffFlag == StageObject.HIT_OFF_FLAG.NONE;
			Character.ACTION_ID actionID = self.actionID;
			if (actionID == Character.ACTION_ID.DAMAGE || actionID == Character.ACTION_ID.MAX || actionID == (Character.ACTION_ID)19 || actionID == (Character.ACTION_ID)32)
			{
				flag = true;
			}
			if (self.isActSpecialAction)
			{
				flag = true;
			}
			if (flag && base.enabled)
			{
				SetEnableCollider(false);
				StartCoroutine(SetEnableCollider(true, 1f));
				ReactPlayer(self);
			}
		}
	}

	private IEnumerator SetEnableCollider(bool value, float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		if (state == STATE.ACTION)
		{
			SetEnableCollider(value);
		}
	}

	protected override void Awake()
	{
	}
}
