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
		Transform effect = EffectManager.GetEffect("ef_btl_bg_geyser_01", this.get_transform());
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
			selfInstanceId = self.get_gameObject().GetInstanceID();
		}
		reactionType = actionData.reactionType;
		actCollider = this.get_gameObject().AddComponent<CapsuleCollider>();
		Reset();
	}

	public void Reset()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		state = STATE.IDLE;
		if (actionData != null)
		{
			if (actionData.start < 0f)
			{
				timer = Random.get_value() * INTERVAL;
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
			float num = (actionData == null) ? 1f : actionData.radius;
			float num2 = num * 2f + 3f;
			actCollider.set_radius(num);
			actCollider.set_height(num2);
			actCollider.set_center(new Vector3(0f, num2 / 2f - num, 0f));
			actCollider.set_isTrigger(true);
			actCollider.set_enabled(false);
		}
		this.set_enabled(true);
	}

	public override void RequestDestroy()
	{
		SetEnableAction(value: false);
		if (effectCtrl != null)
		{
			EffectManager.ReleaseEffect(effectCtrl.get_gameObject());
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
		if (this.get_enabled() != value)
		{
			Reset();
		}
		this.set_enabled(value);
		if (effectCtrl != null)
		{
			effectCtrl.get_gameObject().SetActive(value);
		}
	}

	private void Update()
	{
		if (effectCtrl == null)
		{
			return;
		}
		timer += Time.get_deltaTime();
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
				actCollider.set_enabled(true);
				NextState();
			}
			break;
		case STATE.ACTION:
			if (timer > DURATION)
			{
				actCollider.set_enabled(false);
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
			actCollider.set_enabled(value);
		}
	}

	public void ReactPlayer(Player self)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		self.isGatherInterruption = true;
		Vector3 val = self._transform.get_position() - m_transform.get_position();
		Vector3 normalized = val.get_normalized();
		switch (reactionType)
		{
		case Character.REACTION_TYPE.BLOW:
		case Character.REACTION_TYPE.STUNNED_BLOW:
		case Character.REACTION_TYPE.FALL_BLOW:
		case Character.REACTION_TYPE.CHARM_BLOW:
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
		self.ActReaction(reactionInfo, isSync: true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (selfInstanceId == other.get_gameObject().GetInstanceID() && self != null)
		{
			bool flag = self.hitOffFlag == StageObject.HIT_OFF_FLAG.NONE;
			Character.ACTION_ID actionID = self.actionID;
			if (actionID == Character.ACTION_ID.DAMAGE || actionID == Character.ACTION_ID.MAX || actionID == (Character.ACTION_ID)20 || actionID == (Character.ACTION_ID)33)
			{
				flag = true;
			}
			if (self.isActSpecialAction)
			{
				flag = true;
			}
			if (flag && this.get_enabled())
			{
				SetEnableCollider(value: false);
				this.StartCoroutine(SetEnableCollider(value: true, 1f));
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
