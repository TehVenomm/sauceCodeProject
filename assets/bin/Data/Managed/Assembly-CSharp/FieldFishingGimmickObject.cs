using System.Collections.Generic;
using UnityEngine;

public class FieldFishingGimmickObject : FieldGatherGimmickObject
{
	public const string kFishingMarkerName = "ef_btl_target_fishing_01";

	public const string kFishingEffectGet = "ef_btl_fishing_03";

	public const string kFishingToolModelName = "Fishingrod";

	public const string kFishingToolNodeName = "R_Wep";

	private const int kDefaultModelIndex = 0;

	protected int _modelIndex;

	public int modelIndex => _modelIndex;

	public static string[] GetEffectNames(int index)
	{
		List<string> list = new List<string>();
		list.Add("ef_btl_target_fishing_01");
		list.Add("ef_btl_fishing_03");
		InGameSettingsManager.FishingParam fishingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
		list.Add(fishingParam.omenEffect[index]);
		list.Add(fishingParam.hookEffect[index]);
		list.Add("ef_btl_target_fishing_02");
		return list.ToArray();
	}

	public static int GetModelIndex(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return 0;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			string text = array2[0];
			if (text != null && text == "mi")
			{
				int result = 0;
				if (int.TryParse(array2[1], out result))
				{
					return result;
				}
			}
		}
		return 0;
	}

	public static string ConvertModelIndexToName(int idx)
	{
		return $"CMN_fishing{idx + 1:D2}";
	}

	protected override void ParseParam(string value2)
	{
		_modelIndex = 0;
		base.ParseParam(value2);
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				string text = array2[0];
				if (text != null && text == "mi")
				{
					int.TryParse(array2[1], out _modelIndex);
				}
			}
		}
	}

	public override string GetObjectName()
	{
		return "Fishing";
	}

	public override string GetMarkerName()
	{
		return "ef_btl_target_fishing_01";
	}

	public override GATHER_GIMMICK_TYPE GetGatherGimmickType()
	{
		return GATHER_GIMMICK_TYPE.FISHING;
	}

	public override Character.ACTION_ID GetTargetActionId()
	{
		return (Character.ACTION_ID)40;
	}

	public override bool StartAction(Player player, bool isSend)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (!base.StartAction(player, isSend))
		{
			return false;
		}
		Vector3 val = player._transform.get_position() - m_transform.get_position();
		if (val.get_sqrMagnitude() < sqlRadius)
		{
			player._transform.set_position(m_transform.get_position() + val.get_normalized() * radius * 0.9f);
		}
		return true;
	}
}
