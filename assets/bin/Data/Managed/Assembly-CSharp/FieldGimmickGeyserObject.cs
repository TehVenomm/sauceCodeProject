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

	private readonly int IDLE_ANIM_HASH = Animator.StringToHash("START1");

	private readonly int READY_ANIM_HASH = Animator.StringToHash("START2");

	private readonly int ACTION_ANIM_HASH = Animator.StringToHash("LOOP2");

	private readonly int STATE_LENGTH = Enum.GetValues(typeof(STATE)).Length;

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
		if (effect != null)
		{
			effectCtrl = effect.GetComponent<EffectCtrl>();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		}
		if (self != null)
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
		if (actCollider != null)
		{
			float num = (actionData != null) ? actionData.radius : 1f;
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
		SetEnableAction(value: false);
		if (effectCtrl != null)
		{
			EffectManager.ReleaseEffect(effectCtrl.gameObject);
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
		if (effectCtrl != null)
		{
			effectCtrl.gameObject.SetActive(value);
		}
	}

	private void Update()
	{
		if (effectCtrl == null)
		{
			return;
		}
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
		if (actCollider != null)
		{
			actCollider.enabled = value;
		}
	}

	public void ReactPlayer(Player self)
	{
		self.isGatherInterruption = true;
		Vector3 normalized = (self._transform.position - m_transform.position).normalized;
		Character.REACTION_TYPE rEACTION_TYPE = reactionType;
		if ((uint)(rEACTION_TYPE - 2) <= 1u || rEACTION_TYPE == Character.REACTION_TYPE.FALL_BLOW || rEACTION_TYPE == Character.REACTION_TYPE.CHARM_BLOW)
		{
			self._forward = -normalized;
		}
		normalized = Quaternion.AngleAxis(actionData.angle, self._right) * normalized;
		normalized *= actionData.force;
		Character.ReactionInfo reactionInfo = new Character.ReactionInfo();
		reactionInfo.reactionType = reactionType;
		reactionInfo.blowForce = normalized;
		reactionInfo.loopTime = actionData.loopTime;
		reactionInfo.targetId = self.id;
		self.ActReaction(reactionInfo, isSync: true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (selfInstanceId == other.gameObject.GetInstanceID() && self != null)
		{
			bool flag = self.hitOffFlag == StageObject.HIT_OFF_FLAG.NONE;
			switch (self.actionID)
			{
			case Character.ACTION_ID.DAMAGE:
			case Character.ACTION_ID.MAX:
			case (Character.ACTION_ID)20:
			case (Character.ACTION_ID)33:
				flag = true;
				break;
			}
			if (self.isActSpecialAction)
			{
				flag = true;
			}
			if (flag && base.enabled)
			{
				SetEnableCollider(value: false);
				StartCoroutine(SetEnableCollider(value: true, 1f));
				ReactPlayer(self);
			}
		}
	}

	private IEnumerator SetEnableCollider(bool value, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (state == STATE.ACTION)
		{
			SetEnableCollider(value);
		}
	}

	protected override void Awake()
	{
	}
}
