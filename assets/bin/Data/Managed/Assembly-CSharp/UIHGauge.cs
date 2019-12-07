using UnityEngine;

public class UIHGauge : MonoBehaviour
{
	public enum ANIM_PHASE
	{
		NONE,
		WAIT,
		MOVE
	}

	protected const float ANIM_WAIT_TIME = 3f;

	protected const float ANIM_MOVE_TIME = 0.5f;

	[SerializeField]
	protected UISlider gaugeUI;

	[SerializeField]
	protected UISlider gaugeEffectUI;

	protected bool initialized;

	protected float oldPercent = 1f;

	protected ANIM_PHASE animPhase;

	protected float animTime;

	public float nowPercent
	{
		get;
		protected set;
	}

	public UIHGauge()
	{
		nowPercent = 1f;
	}

	private void Awake()
	{
		if (gaugeUI != null)
		{
			initialized = true;
		}
	}

	public void SetPercent(float percent, bool anim = true)
	{
		if (percent < 0f)
		{
			percent = 0f;
		}
		if (percent > 1f)
		{
			percent = 1f;
		}
		float nowPercent = this.nowPercent;
		this.nowPercent = percent;
		oldPercent = percent;
		if (anim)
		{
			oldPercent = nowPercent;
			animPhase = ANIM_PHASE.WAIT;
			animTime = 3f;
		}
		else
		{
			animPhase = ANIM_PHASE.NONE;
		}
	}

	private void LateUpdate()
	{
		if (animPhase == ANIM_PHASE.WAIT)
		{
			animTime -= Time.deltaTime;
			if (animTime <= 0f)
			{
				animPhase = ANIM_PHASE.MOVE;
				animTime = 0.5f;
			}
		}
		else if (animPhase == ANIM_PHASE.MOVE)
		{
			animTime -= Time.deltaTime;
			if (animTime <= 0f)
			{
				animPhase = ANIM_PHASE.NONE;
				animTime = 0f;
			}
		}
		UpdateGauge();
	}

	protected virtual void UpdateGauge()
	{
		if (!gaugeUI)
		{
			return;
		}
		gaugeUI.value = nowPercent;
		if (gaugeEffectUI != null)
		{
			float value = nowPercent;
			if (animPhase == ANIM_PHASE.WAIT)
			{
				value = oldPercent;
			}
			else if (animPhase == ANIM_PHASE.MOVE)
			{
				value = nowPercent + (oldPercent - nowPercent) * (animTime / 0.5f);
			}
			gaugeEffectUI.value = value;
		}
	}

	public Transform GetGaugeTransform()
	{
		if ((bool)gaugeUI)
		{
			return gaugeUI.gameObject.transform;
		}
		return base.gameObject.transform;
	}
}
