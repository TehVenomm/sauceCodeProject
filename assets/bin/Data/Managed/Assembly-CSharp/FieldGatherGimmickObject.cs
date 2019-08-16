using System.Collections.Generic;
using UnityEngine;

public class FieldGatherGimmickObject : FieldGimmickObject
{
	protected Transform _transform;

	protected Transform targetMarkerTrans;

	protected float radius = 1.5f;

	protected float sqlRadius = 2.25f;

	protected List<Player> userList = new List<Player>();

	public int lotId
	{
		get;
		protected set;
	}

	protected override void Awake()
	{
		_transform = this.get_transform();
		SphereCollider componentInChildren = _transform.GetComponentInChildren<SphereCollider>();
		if (!object.ReferenceEquals(componentInChildren, null))
		{
			radius = componentInChildren.get_radius();
			sqlRadius = radius * radius;
		}
		Utility.SetLayerWithChildren(this.get_transform(), 19);
	}

	protected override void ParseParam(string value2)
	{
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
				if (text != null && text == "lid")
				{
					int result = 0;
					int.TryParse(array2[1], out result);
					lotId = result;
				}
			}
		}
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (CanUse() && isNear && self != null && self.IsChangeableAction(GetTargetActionId()))
		{
			if (targetMarkerTrans == null)
			{
				targetMarkerTrans = EffectManager.GetEffect(GetMarkerName(), _transform);
			}
			if (targetMarkerTrans != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() + Vector3.get_up() + _transform.get_position();
				targetMarkerTrans.Set(pos, rotation);
			}
		}
		else if (targetMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(targetMarkerTrans.get_gameObject());
		}
	}

	public virtual bool StartAction(Player player, bool isSend = false)
	{
		if (!IsValid())
		{
			return false;
		}
		OnUseStart(player, isSend);
		return true;
	}

	public void OnUseStart(Player player, bool isSend = false)
	{
		if (!(player == null) && !userList.Contains(player))
		{
			userList.Add(player);
			if (isSend)
			{
				player.playerSender.OnGatherGimmickInfo(m_id, isUsed: true);
			}
		}
	}

	public void OnUseEnd(Player player, bool isSend = false)
	{
		if (!(player == null) && userList.Contains(player))
		{
			userList.Remove(player);
			if (isSend)
			{
				player.playerSender.OnGatherGimmickInfo(m_id, isUsed: false);
			}
		}
	}

	protected virtual bool IsValid()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self == null)
		{
			return false;
		}
		if (self.isDead)
		{
			return false;
		}
		return true;
	}

	public override float GetTargetSqrRadius()
	{
		return sqlRadius;
	}

	public override string GetObjectName()
	{
		return "GatherGimmick";
	}

	public virtual string GetMarkerName()
	{
		return "ef_btl_target_common_01";
	}

	public virtual GATHER_GIMMICK_TYPE GetGatherGimmickType()
	{
		return GATHER_GIMMICK_TYPE.NONE;
	}

	public virtual Character.ACTION_ID GetTargetActionId()
	{
		return (Character.ACTION_ID)28;
	}

	public virtual bool IsUseOnly()
	{
		return true;
	}

	public virtual bool CanUse()
	{
		if (!IsUseOnly())
		{
			return true;
		}
		for (int i = 0; i < userList.Count; i++)
		{
			bool flag = false;
			Player player = userList[i];
			if (player == null)
			{
				flag = true;
			}
			else
			{
				StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(player.id);
				if (stageObject == null)
				{
					flag = true;
				}
			}
			if (flag)
			{
				userList.RemoveAt(i);
				i--;
			}
		}
		return userList.Count == 0;
	}
}
