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
		if (!base.StartAction(player, isSend))
		{
			return false;
		}
		Vector3 vector = player._transform.position - m_transform.position;
		if (vector.sqrMagnitude < sqlRadius)
		{
			player._transform.position = m_transform.position + vector.normalized * radius * 0.9f;
		}
		return true;
	}
}
