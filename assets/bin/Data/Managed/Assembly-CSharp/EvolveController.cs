using UnityEngine;

public class EvolveController
{
	private const float kGaugeMax = 1000f;

	private const uint kLeviathanId = 10000u;

	private const uint kSphinxId = 10001u;

	public const int kEvolveGaugeMaxSeId = 10000091;

	private const string TYPE10000_EXEC_EFFECT = "ef_btl_wex1_spear_01_01";

	private const string TYPE10000_EFFECT = "ef_btl_wex1_spear_01_02";

	private const string TYPE10000_EXRUSH_EFFECT = "ef_btl_wex1_spear_01_03";

	private const string TYPE10001_WING_EFFECT = "ef_btl_ast1_twinsword_01";

	private const string TYPE10001_WEAPON_EFFECT = "ef_btl_ast1_twinsword_02";

	private const string TYPE10001_ACTION_EFFECT = "ef_btl_ast1_twinsword_03";

	private InGameSettingsManager.Evolve parameter;

	private Player owner;

	private bool isSelf;

	private float[] gauge;

	private uint execEvolveId;

	private Transform execEffect;

	private Transform execEffect2;

	private Transform execEffect3;

	private float execSec;

	private float decreaseValue;

	private InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff[] execBuffs;

	public bool isExec
	{
		get;
		private set;
	}

	public static void Load(LoadingQueue queue, uint evolveId)
	{
		queue.CacheSE(10000091, null);
		switch (evolveId)
		{
		case 10000u:
			queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.evolve.type10000.rushSeId, null);
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_01");
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_02");
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_03");
			break;
		case 10001u:
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_01");
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_02");
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_03");
			break;
		}
	}

	public void Init(Player player)
	{
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.evolve;
		owner = player;
		isSelf = (player is Self);
		gauge = new float[3];
		execEvolveId = 0u;
		execSec = 0f;
		decreaseValue = 0f;
		isExec = false;
	}

	public void SetWeaponInfo()
	{
		if (isSelf)
		{
			uint evolveWeaponId = owner.GetEvolveWeaponId();
			if (evolveWeaponId != 0)
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveIcon(evolveWeaponId);
				MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveIcon(evolveWeaponId);
			}
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(gauge[owner.weaponIndex] / 1000f);
			MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(IsGaugeFull());
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(gauge[owner.weaponIndex] / 1000f);
			MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(IsGaugeFull());
		}
	}

	public void Start(uint evolveId)
	{
		execEvolveId = evolveId;
		decreaseValue = 0f;
		isExec = false;
		execBuffs = null;
		InGameSettingsManager.Evolve.TypeAbstract typeAbstract;
		switch (execEvolveId)
		{
		default:
			return;
		case 10000u:
			typeAbstract = _start10000();
			break;
		case 10001u:
			typeAbstract = _start10001();
			break;
		}
		if (typeAbstract.healValue > 0)
		{
			owner.OnGetHeal(typeAbstract.healValue, HEAL_TYPE.NONE, false, HEAL_EFFECT_TYPE.BASIS, true);
		}
		if (!object.ReferenceEquals(typeAbstract.healTypes, null))
		{
			for (int i = 0; i < typeAbstract.healTypes.Length; i++)
			{
				owner.DoHealType(typeAbstract.healTypes[i]);
			}
		}
		if (!object.ReferenceEquals(typeAbstract.buffs, null) && typeAbstract.buffs.Length > 0)
		{
			execBuffs = typeAbstract.buffs;
			if (isSelf)
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
			}
		}
		execSec = typeAbstract.execSec;
		if (execSec <= 0f)
		{
			ResetCurrentGauge();
		}
	}

	public void End()
	{
		switch (execEvolveId)
		{
		case 10000u:
			_end10000();
			break;
		case 10001u:
			_end10001();
			break;
		}
		ReleaseEffect(ref execEffect, true);
		ReleaseEffect(ref execEffect2, true);
		ReleaseEffect(ref execEffect3, true);
		execEvolveId = 0u;
		isExec = false;
		execBuffs = null;
		if (isSelf)
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.PlayChangeEvolveIcon(false);
			MonoBehaviourSingleton<UIEnduranceStatus>.I.PlayChangeEvolveIcon(false);
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
	}

	public bool Execute(ref float rSec)
	{
		rSec = 0f;
		if (execEvolveId == 0)
		{
			return false;
		}
		if (isExec)
		{
			return false;
		}
		if (execSec <= 0f)
		{
			return false;
		}
		rSec = execSec;
		isExec = true;
		decreaseValue = 1000f / execSec;
		return true;
	}

	public bool Update()
	{
		DecreaseCurrentGauge(decreaseValue * Time.get_deltaTime());
		return GetCurrentGauge() <= 0f;
	}

	private void ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && !object.ReferenceEquals(t, null))
		{
			EffectManager.ReleaseEffect(t.get_gameObject(), isPlayEndAnimation, false);
			t = null;
		}
	}

	public bool IsExistSpecialAction()
	{
		uint num = execEvolveId;
		if (num == 10001)
		{
			return true;
		}
		return false;
	}

	public float GetSpecialActionSec()
	{
		uint num = execEvolveId;
		if (num == 10001)
		{
			return parameter.type10001.specialLoopSec;
		}
		return 0f;
	}

	public int GetExecBuffValue(BuffParam.BUFFTYPE type)
	{
		if (object.ReferenceEquals(execBuffs, null))
		{
			return 0;
		}
		for (int i = 0; i < execBuffs.Length; i++)
		{
			InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff evolveBuff = execBuffs[i];
			if (evolveBuff.type == type)
			{
				return evolveBuff.value;
			}
		}
		return 0;
	}

	public void ResetGauge(bool isAll)
	{
		for (int i = 0; i < 3; i++)
		{
			if (isAll || i == owner.weaponIndex)
			{
				gauge[i] = 0f;
			}
		}
		if (isSelf)
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(0f);
			MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(false);
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(0f);
			MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(false);
		}
	}

	public void ResetCurrentGauge()
	{
		ResetGauge(false);
	}

	public void SetGauge(float value, int index)
	{
		if (index >= 0 && index < 3)
		{
			if (value < 0f)
			{
				value = 0f;
			}
			if (value >= 1000f)
			{
				value = 1000f;
			}
			gauge[index] = value;
			if (isSelf && index == owner.weaponIndex)
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(gauge[index] / 1000f);
				MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(IsGaugeFull());
				MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(gauge[index] / 1000f);
				MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(IsGaugeFull());
			}
		}
	}

	public void SetCurrentGauge(float value)
	{
		SetGauge(value, owner.weaponIndex);
	}

	public float GetGauge(int index)
	{
		if (index < 0 || index >= 3)
		{
			return 0f;
		}
		return gauge[index];
	}

	public float GetCurrentGauge()
	{
		return GetGauge(owner.weaponIndex);
	}

	public void IncreaseGauge(AttackHitInfo.ATTACK_TYPE atkType, Player.ATTACK_MODE atkMode, int index)
	{
		if (execEvolveId == 0)
		{
			float num = _GetIncreaseValue(atkType, atkMode);
			if (!(num <= 0f))
			{
				float num2 = gauge[index];
				gauge[index] += owner.CalcWaveMatchSpGauge(num);
				if (gauge[index] > 1000f)
				{
					gauge[index] = 1000f;
				}
				if (isSelf && index == owner.weaponIndex && num2 != gauge[index])
				{
					MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(gauge[index] / 1000f);
					MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(gauge[index] / 1000f);
					if (IsGaugeFull())
					{
						MonoBehaviourSingleton<UIPlayerStatus>.I.PlayChangeEvolveIcon(true);
						MonoBehaviourSingleton<UIEnduranceStatus>.I.PlayChangeEvolveIcon(true);
					}
				}
			}
		}
	}

	public void IncreaseCurrentGauge(AttackHitInfo.ATTACK_TYPE atkType, Player.ATTACK_MODE atkMode)
	{
		IncreaseGauge(atkType, atkMode, owner.weaponIndex);
	}

	public void DecreaseGauge(float value, int index)
	{
		gauge[index] -= value;
		if (gauge[index] <= 0f)
		{
			gauge[index] = 0f;
		}
		if (isSelf && index == owner.weaponIndex)
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(gauge[index] / 1000f);
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(gauge[index] / 1000f);
		}
	}

	public void DecreaseCurrentGauge(float value)
	{
		DecreaseGauge(value, owner.weaponIndex);
	}

	public bool IsGaugeFull()
	{
		return gauge[owner.weaponIndex] >= 1000f;
	}

	private float _GetIncreaseValue(AttackHitInfo.ATTACK_TYPE atkType, Player.ATTACK_MODE atkMode)
	{
		EQUIPMENT_TYPE eQUIPMENT_TYPE = Player.ConvertAttackModeToEquipmentType(atkMode);
		int i = 0;
		for (int num = parameter.gaugeInfo.Length; i < num; i++)
		{
			InGameSettingsManager.Evolve.GaugeInfo gaugeInfo = parameter.gaugeInfo[i];
			if (gaugeInfo.type == eQUIPMENT_TYPE)
			{
				return gaugeInfo.value;
			}
		}
		return 0f;
	}

	private InGameSettingsManager.Evolve.TypeAbstract _start10000()
	{
		AppMain.Delay(parameter.type10000.execEffectDelay, delegate
		{
			EffectManager.GetEffect("ef_btl_wex1_spear_01_01", owner.FindNode(string.Empty));
		});
		ReleaseEffect(ref execEffect, true);
		execEffect = EffectManager.GetEffect("ef_btl_wex1_spear_01_02", owner.loader.wepR);
		return parameter.type10000;
	}

	private void _end10000()
	{
	}

	public bool IsExecLeviathan()
	{
		return execEvolveId == 10000;
	}

	public float GetLeviathanRushDistanceRate()
	{
		if (execEvolveId != 10000)
		{
			return 0f;
		}
		return parameter.type10000.rushDistanceRate;
	}

	public void GetLeviathanExRushDamageRate(out float oMin, out float oMax, out float oFull)
	{
		oMin = parameter.type10000.damageRateMin;
		oMax = parameter.type10000.damageRateMax;
		oFull = parameter.type10000.damageRateFull;
	}

	public void PlayLeviathanEffect()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (execEvolveId == 10000)
		{
			SoundManager.PlayOneShotSE(parameter.type10000.rushSeId, owner, owner.FindNode(string.Empty));
			Transform effect = EffectManager.GetEffect("ef_btl_wex1_spear_01_03", owner.FindNode("Move"));
			effect.set_localPosition(new Vector3(0f, 1f, 0f));
		}
	}

	private InGameSettingsManager.Evolve.TypeAbstract _start10001()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		ReleaseEffect(ref execEffect, true);
		execEffect = EffectManager.GetEffect("ef_btl_ast1_twinsword_02", owner.loader.wepR);
		ReleaseEffect(ref execEffect2, true);
		execEffect2 = EffectManager.GetEffect("ef_btl_ast1_twinsword_02", owner.loader.wepL);
		ReleaseEffect(ref execEffect3, true);
		execEffect3 = EffectManager.GetEffect("ef_btl_ast1_twinsword_01", owner.FindNode("Spine01"));
		execEffect3.set_localRotation(Quaternion.Euler(new Vector3(90f, -90f, 0f)));
		return parameter.type10001;
	}

	private void _end10001()
	{
	}

	public bool IsExecSphinx()
	{
		return execEvolveId == 10001;
	}

	public float GetSphinxRangeUpRate()
	{
		if (execEvolveId != 10001)
		{
			return 1f;
		}
		return parameter.type10001.rangeUp;
	}

	public float GetSphinxElementDamageUpRate()
	{
		if (execEvolveId != 10001)
		{
			return 1f;
		}
		return parameter.type10001.elementDamageRate;
	}

	public void PlaySphinxActionEffect()
	{
		if (execEvolveId == 10001)
		{
			AppMain.Delay(1f, delegate
			{
				//IL_002b: Unknown result type (might be due to invalid IL or missing references)
				Transform effect = EffectManager.GetEffect("ef_btl_ast1_twinsword_03", owner.FindNode("Move"));
				effect.set_localPosition(new Vector3(0f, 3.7f, 1.5f));
			});
		}
	}
}
