using System;
using UnityEngine;

public class UIEvolveGauge : MonoBehaviour
{
	public GameObject longTouchTarget;

	public UISprite gauge;

	public GameObject maxEffect;

	public UISprite weaponIcon;

	public UITexture evolveIcon;

	private Transform effectTrans;

	private Vector3 effectPos = new Vector3(-3f, -26f, 0f);

	private Vector3 effectScale = new Vector3(0.85f, 0.85f, 0f);

	public UIEvolveGauge()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)
	//IL_002a: Unknown result type (might be due to invalid IL or missing references)
	//IL_002f: Unknown result type (might be due to invalid IL or missing references)


	protected void Awake()
	{
		UILongTouch.Set(longTouchTarget, "EVOLVE");
		UITouchAndRelease.Set(longTouchTarget, "EVOLVE_TOUCH");
		effectTrans = EffectManager.GetUIEffect("ef_ui_skillgauge_blue_01", gauge.get_transform().get_parent(), 0f, 1, gauge);
		if (!object.ReferenceEquals(effectTrans, null))
		{
			effectTrans.get_gameObject().SetActive(false);
		}
	}

	private void OnDestroy()
	{
		EffectManager.ReleaseEffect(ref effectTrans);
	}

	public void SetRate(float rate)
	{
		if (rate <= 0f)
		{
			rate = 0f;
		}
		if (rate >= 1f)
		{
			rate = 1f;
		}
		gauge.fillAmount = rate;
		maxEffect.SetActive(rate >= 1f);
		_CalcGaugeEffect(rate);
	}

	public void SetEvolveIcon(uint evolveId)
	{
		ResourceLoad.LoadEvolveIconTexture(evolveIcon, evolveId);
	}

	public void EnableEvolveIcon(bool isEnable)
	{
		weaponIcon.set_enabled(!isEnable);
		evolveIcon.get_gameObject().SetActive(isEnable);
	}

	private void _CalcGaugeEffect(float rate)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (!object.ReferenceEquals(effectTrans, null))
		{
			float num = -28f + 60f * rate;
			effectTrans.set_localPosition(new Vector3(-4f, num, 0f));
			float num2 = 0.85f * Mathf.Sin(rate * (float)Math.PI) + 0.2f;
			if (num2 >= 0.8f)
			{
				num2 = 0.8f;
			}
			if (num2 <= 0f)
			{
				num2 = 0f;
			}
			effectTrans.set_localScale(new Vector3(num2, num2, 1f));
			effectTrans.get_gameObject().SetActive(rate > 0f && rate < 1f);
		}
	}
}
