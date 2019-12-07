using UnityEngine;

public class FieldGimmickCoopFishing : FieldGatherGimmickObject
{
	private const string OBJECT_NAME = "CoopFishing";

	public const string COOP_FISHING_MARKER_NAME = "ef_btl_target_fishing_02";

	private Transform targetMarker;

	private Player owner;

	private bool isActive;

	protected override void Awake()
	{
		SetTransform(base.transform);
		isActive = true;
	}

	public override string GetObjectName()
	{
		return "CoopFishing";
	}

	public override string GetMarkerName()
	{
		return "ef_btl_target_fishing_02";
	}

	public override bool IsUseOnly()
	{
		return false;
	}

	public override GATHER_GIMMICK_TYPE GetGatherGimmickType()
	{
		return GATHER_GIMMICK_TYPE.COOP_FISHING;
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)41))
		{
			if (targetMarker == null)
			{
				targetMarker = EffectManager.GetEffect("ef_btl_target_fishing_02", m_transform);
			}
			if (targetMarker != null)
			{
				Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.position;
				Quaternion rotation = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.rotation;
				Vector3 pos = (position - m_transform.position).normalized + Vector3.up + m_transform.position;
				targetMarker.Set(pos, rotation);
			}
		}
		else if (targetMarker != null)
		{
			EffectManager.ReleaseEffect(targetMarker.gameObject);
		}
	}

	public override bool IsSearchableNearest()
	{
		if (!isActive)
		{
			return false;
		}
		return !(owner is Self);
	}

	public void SetOwner(Player player)
	{
		owner = player;
	}

	public int GetOwnerPlayerId()
	{
		return owner.id;
	}

	public int GetOwnerClientId()
	{
		return owner.coopClientId;
	}

	public int GetOwnerUserId()
	{
		return owner.createInfo.charaInfo.userId;
	}

	public void Deactivate()
	{
		isActive = false;
	}

	public Vector3 GetCoopPos()
	{
		if (owner == null)
		{
			return m_transform.position;
		}
		return m_transform.position + owner._forward * -1f;
	}

	public Quaternion GetCoopRot()
	{
		if (owner == null)
		{
			return Quaternion.identity;
		}
		return Quaternion.LookRotation(owner._forward, Vector3.up);
	}

	public void SetPosition(Vector3 pos)
	{
		m_transform.position = pos;
	}
}
