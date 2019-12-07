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
		if (state != 0)
		{
			return state == eState.Sleep;
		}
		return true;
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
		if ((object)effectTrans == null)
		{
			effectTrans = EffectManager.GetUIEffect("ef_btl_soul_energy_01", parent);
		}
		if ((object)effectTrans != null)
		{
			effectTrans.localScale = (isJustTap ? kJustScale : kStartScale);
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
			if ((object)effectTrans != null)
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
			if ((object)cacheOwner != null)
			{
				cacheOwner.IncreaseSoulGauge(baseValue, isJustTap);
			}
			Sleep();
		}
	}

	public void Sleep()
	{
		if ((object)effectTrans != null)
		{
			EffectManager.ReleaseEffect(effectTrans.gameObject);
			effectTrans = null;
		}
		state = eState.Sleep;
	}

	private void OnDestroy()
	{
		if ((object)effectTrans != null)
		{
			EffectManager.ReleaseEffect(effectTrans.gameObject);
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
