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

	protected void Awake()
	{
		UILongTouch.Set(longTouchTarget, "EVOLVE", null);
		UITouchAndRelease.Set(longTouchTarget, "EVOLVE_TOUCH", null, null);
		effectTrans = EffectManager.GetUIEffect("ef_ui_skillgauge_blue_01", gauge.transform.parent, 0f, 1, gauge);
		if (!object.ReferenceEquals(effectTrans, null))
		{
			effectTrans.gameObject.SetActive(false);
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
		weaponIcon.enabled = !isEnable;
		evolveIcon.gameObject.SetActive(isEnable);
	}

	private void _CalcGaugeEffect(float rate)
	{
		if (!object.ReferenceEquals(effectTrans, null))
		{
			float y = -28f + 60f * rate;
			effectTrans.localPosition = new Vector3(-4f, y, 0f);
			float num = 0.85f * Mathf.Sin(rate * 3.14159274f) + 0.2f;
			if (num >= 0.8f)
			{
				num = 0.8f;
			}
			if (num <= 0f)
			{
				num = 0f;
			}
			effectTrans.localScale = new Vector3(num, num, 1f);
			effectTrans.gameObject.SetActive(rate > 0f && rate < 1f);
		}
	}
}
