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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (object.ReferenceEquals(effectTrans, null))
		{
			effectTrans = EffectManager.GetUIEffect("ef_btl_soul_energy_01", parent);
		}
		if (!object.ReferenceEquals(effectTrans, null))
		{
			effectTrans.set_localScale((!isJustTap) ? kStartScale : kJustScale);
		}
		else
		{
			Absorbed();
		}
		return effectTrans;
	}

	public void Tap()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (state == eState.CanTap)
		{
			if (!object.ReferenceEquals(effectTrans, null))
			{
				effectTrans.get_transform().set_localScale(kJustScale);
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
			EffectManager.ReleaseEffect(effectTrans.get_gameObject());
			effectTrans = null;
		}
		state = eState.Sleep;
	}

	private void OnDestroy()
	{
		if (!object.ReferenceEquals(effectTrans, null))
		{
			EffectManager.ReleaseEffect(effectTrans.get_gameObject());
			effectTrans = null;
		}
	}

	private void Update()
	{
		if (state == eState.CanTap)
		{
			counter -= Time.get_deltaTime();
			if (counter <= 0f)
			{
				state = eState.CannotTap;
			}
		}
	}
}
