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
		_transform = base.transform;
		SphereCollider componentInChildren = _transform.GetComponentInChildren<SphereCollider>();
		if ((object)componentInChildren != null)
		{
			radius = componentInChildren.radius;
			sqlRadius = radius * radius;
		}
		Utility.SetLayerWithChildren(base.transform, 19);
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
				string a = array2[0];
				if (a == "lid")
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
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized + Vector3.up + _transform.position;
				targetMarkerTrans.Set(pos, rotation);
			}
		}
		else if (targetMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(targetMarkerTrans.gameObject);
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
			else if (MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(player.id) == null)
			{
				flag = true;
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
