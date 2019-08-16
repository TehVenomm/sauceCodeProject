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
		SetTransform(this.get_transform());
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
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)41))
		{
			if (targetMarker == null)
			{
				targetMarker = EffectManager.GetEffect("ef_btl_target_fishing_02", m_transform);
			}
			if (targetMarker != null)
			{
				Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_position();
				Quaternion rotation = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_rotation();
				Vector3 val = position - m_transform.get_position();
				Vector3 pos = val.get_normalized() + Vector3.get_up() + m_transform.get_position();
				targetMarker.Set(pos, rotation);
			}
		}
		else if (targetMarker != null)
		{
			EffectManager.ReleaseEffect(targetMarker.get_gameObject());
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (owner == null)
		{
			return m_transform.get_position();
		}
		return m_transform.get_position() + owner._forward * -1f;
	}

	public Quaternion GetCoopRot()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (owner == null)
		{
			return Quaternion.get_identity();
		}
		return Quaternion.LookRotation(owner._forward, Vector3.get_up());
	}

	public void SetPosition(Vector3 pos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		m_transform.set_position(pos);
	}
}
