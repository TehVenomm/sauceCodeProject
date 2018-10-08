using UnityEngine;

public class SoulEnergy
{
	public enum eState
	{
		None,
		CanTap,
		CannotTap,
		Sleep
	}

	private Vector3 kStartScale;

	private Vector3 kJustScale;

	private float kJustTapSec;

	private eState state;

	private Player cacheOwner;

	private Transform effectTrans;

	private float counter;

	private float baseValue;

	private bool isJustTap;

	public bool canWork()
	{
		return state == eState.None || state == eState.Sleep;
	}

	public void Init()
	{
		InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
		kStartScale = new Vector3(twoHandSwordActionInfo.soulSoulEnergyNormalScale, twoHandSwordActionInfo.soulSoulEnergyNormalScale, twoHandSwordActionInfo.soulSoulEnergyNormalScale);
		kJustScale = new Vector3(twoHandSwordActionInfo.soulSoulEnergyJustTapScale, twoHandSwordActionInfo.soulSoulEnergyJustTapScale, twoHandSwordActionInfo.soulSoulEnergyJustTapScale);
		kJustTapSec = twoHandSwordActionInfo.soulJustTapEnableSec;
	}

	public void Exec(Player owner, float value)
	{
		cacheOwner = owner;
		baseValue = value;
		isJustTap = false;
		counter = kJustTapSec;
		state = eState.CanTap;
	}

	public Transform GetEffectTrans(Transform parent)
	{
		if (object.ReferenceEquals(effectTrans, null))
		{
			effectTrans = EffectManager.GetUIEffect("ef_btl_soul_energy_01", parent, -0.001f, 0, null);
		}
		if (!object.ReferenceEquals(effectTrans, null))
		{
			effectTrans.localScale = ((!isJustTap) ? kStartScale : kJustScale);
		}
		else
		{
			Absorbed();
		}
		return effectTrans;
	}

	public void Tap()
	{
		if (state == eState.CanTap)
		{
			if (!object.ReferenceEquals(effectTrans, null))
			{
				effectTrans.transform.localScale = kJustScale;
			}
			isJustTap = true;
			state = eState.CannotTap;
		}
	}

	public void Absorbed()
	{
		if (!canWork())
		{
			if (!object.ReferenceEquals(cacheOwner, null))
			{
				cacheOwner.IncreaseSoulGauge(baseValue, isJustTap);
			}
			Sleep();
		}
	}

	public void Sleep()
	{
		if (!object.ReferenceEquals(effectTrans, null))
		{
			EffectManager.ReleaseEffect(effectTrans.gameObject, true, false);
			effectTrans = null;
		}
		state = eState.Sleep;
	}

	private void OnDestroy()
	{
		if (!object.ReferenceEquals(effectTrans, null))
		{
			EffectManager.ReleaseEffect(effectTrans.gameObject, true, false);
			effectTrans = null;
		}
	}

	private void Update()
	{
		if (state == eState.CanTap)
		{
			counter -= Time.deltaTime;
			if (counter <= 0f)
			{
				state = eState.CannotTap;
			}
		}
	}
}
