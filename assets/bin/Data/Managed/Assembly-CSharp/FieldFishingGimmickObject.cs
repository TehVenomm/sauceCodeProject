using UnityEngine;

public class FieldFishingGimmickObject : FieldGatherGimmickObject
{
	public const string kFishingMarkerName = "ef_btl_target_fishing_01";

	public const string kFishingToolModelName = "Fishingrod";

	public const string kFishingToolNodeName = "R_Wep";

	public const string kFishingEffectOmen = "ef_btl_fishing_01";

	public const string kFishingEffectHook = "ef_btl_fishing_02";

	public const string kFishingEffectGet = "ef_btl_fishing_03";

	public override string GetObjectName()
	{
		return "Fishing";
	}

	public override string GetMarkerName()
	{
		return "ef_btl_target_fishing_01";
	}

	public override GATHER_GIMMICK_TYPE GetType()
	{
		return GATHER_GIMMICK_TYPE.FISHING;
	}

	public override Character.ACTION_ID GetTargetActionId()
	{
		return (Character.ACTION_ID)39;
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
