using System.Collections.Generic;
using UnityEngine;

public class FieldCarriableBuffPointGimmickObject : FieldCarriableGimmickObject
{
	protected class BuffData
	{
		public float buffRadius = kDefaultBuffRadius;

		public List<uint> buffIdList = new List<uint>();

		public List<BuffParam.BuffData> buffParamList = new List<BuffParam.BuffData>();
	}

	public static readonly string kPutEffectName = "ef_btl_trap_01_02";

	public static readonly int kPutSEId = 10000058;

	private static readonly int kShiftIndex = 1000;

	private static readonly string kHeadEffectNameFormat = "ef_btl_trap_03_{0:D2}_01";

	private static readonly string kBuffEffectNameFormat = "ef_btl_trap_03_{0:D2}_02";

	private static readonly float kDefaultBuffRadius = 2.5f;

	protected Transform buffEffect;

	protected Transform headEffect;

	protected bool isTargetPlayer;

	protected List<BuffData> buffList;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		if (buffList.IsNullOrEmpty())
		{
			return;
		}
		int i = 0;
		for (int count = buffList.Count; i < count; i++)
		{
			int j = 0;
			for (int count2 = buffList[i].buffIdList.Count; j < count2; j++)
			{
				BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(buffList[i].buffIdList[j]);
				if (data != null)
				{
					BuffParam.BuffData buffData = new BuffParam.BuffData();
					buffData.type = data.type;
					buffData.valueType = data.valueType;
					buffData.value = data.value;
					buffData.time = data.duration;
					buffData.interval = data.interval;
					buffList[i].buffParamList.Add(buffData);
				}
			}
		}
	}

	protected override void ParseParam(string value2)
	{
		base.ParseParam(value2);
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		buffList = new List<BuffData>();
		for (int i = 0; i <= maxLv; i++)
		{
			buffList.Add(new BuffData());
		}
		string[] array = new string[2]
		{
			"bf",
			"r"
		};
		string[] array2 = value2.Split(',');
		for (int j = 0; j < array2.Length; j++)
		{
			string[] array3 = array2[j].Split(':');
			if (array3 == null || array3.Length != 2)
			{
				continue;
			}
			if (array3[0] == "pl")
			{
				isTargetPlayer = (int.Parse(array3[1]) > 0);
				continue;
			}
			for (int k = 0; k < array.Length; k++)
			{
				int result = 0;
				if (array3[0].StartsWith(array[k]) && int.TryParse(array3[0].Replace(array[k], string.Empty), out result) && result <= maxLv)
				{
					switch (array[k])
					{
					case "bf":
						buffList[result].buffIdList.Add(uint.Parse(array3[1]));
						break;
					case "r":
						buffList[result].buffRadius = float.Parse(array3[1]);
						break;
					}
					break;
				}
			}
		}
	}

	protected override void OnStartCarry(Player owner)
	{
		base.OnStartCarry(owner);
		if (buffEffect != null)
		{
			EffectManager.ReleaseEffect(buffEffect.get_gameObject());
			buffEffect = null;
		}
		if (headEffect != null)
		{
			EffectManager.ReleaseEffect(headEffect.get_gameObject());
			headEffect = null;
		}
	}

	protected override void OnEndCarry()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		base.OnEndCarry();
		if (buffEffect == null)
		{
			buffEffect = EffectManager.GetEffect(GetBuffEffectNameByModelIndex(modelIndex), GetTransform());
			Transform obj = buffEffect;
			obj.set_localScale(obj.get_localScale() * (buffList[currentLv].buffRadius / kDefaultBuffRadius));
		}
		if (headEffect == null)
		{
			headEffect = EffectManager.GetEffect(GetHeadEffectNameByModelIndex(modelIndex), GetTransform());
		}
		EffectManager.OneShot(kPutEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().get_position());
	}

	protected override void OnEvolved()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		base.OnEvolved();
		if (buffEffect != null)
		{
			EffectManager.ReleaseEffect(buffEffect.get_gameObject());
			buffEffect = null;
		}
		if (headEffect != null)
		{
			EffectManager.ReleaseEffect(headEffect.get_gameObject());
			headEffect = null;
		}
		buffEffect = EffectManager.GetEffect(GetBuffEffectNameByModelIndex(modelIndex), GetTransform());
		Transform obj = buffEffect;
		obj.set_localScale(obj.get_localScale() * (buffList[currentLv].buffRadius / kDefaultBuffRadius));
		headEffect = EffectManager.GetEffect(GetHeadEffectNameByModelIndex(modelIndex), GetTransform());
	}

	private void LateUpdate()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (buffList.IsNullOrEmpty() || buffList[currentLv].buffParamList.IsNullOrEmpty() || buffEffect == null)
		{
			return;
		}
		BuffData buffData = buffList[currentLv];
		List<StageObject> list = null;
		list = ((!isTargetPlayer) ? MonoBehaviourSingleton<StageObjectManager>.I.enemyList : MonoBehaviourSingleton<StageObjectManager>.I.playerList);
		for (int i = 0; i < list.Count; i++)
		{
			Character character = list[i] as Character;
			if (character == null || character.isDead)
			{
				continue;
			}
			float num = Vector3.Magnitude(GetTransform().get_position() - character._transform.get_position());
			if (num > buffData.buffRadius)
			{
				continue;
			}
			for (int j = 0; j < buffData.buffParamList.Count; j++)
			{
				if (!character.IsValidBuff(buffData.buffParamList[j].type))
				{
					character.OnBuffStart(buffData.buffParamList[j]);
				}
			}
		}
	}

	public static string GetHeadEffectNameByModelIndex(int index)
	{
		return string.Format(kHeadEffectNameFormat, index + 1);
	}

	public static string GetBuffEffectNameByModelIndex(int index)
	{
		return string.Format(kBuffEffectNameFormat, index + 1);
	}
}
