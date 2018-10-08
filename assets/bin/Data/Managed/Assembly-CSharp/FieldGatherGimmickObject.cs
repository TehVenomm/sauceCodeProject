using System.Collections.Generic;
using UnityEngine;

public class FieldGatherGimmickObject : FieldGimmickObject
{
	private Transform _transform;

	private Transform targetMarkerTrans;

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
		if (!object.ReferenceEquals(componentInChildren, null))
		{
			radius = componentInChildren.radius;
			sqlRadius = radius * radius;
		}
		Utility.SetLayerWithChildren(base.transform, 19);
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		ParseParam(pointData.value2);
	}

	private void ParseParam(string value2)
	{
		if (!value2.IsNullOrWhiteSpace())
		{
			string[] array = value2.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':');
				if (array2 != null && array2.Length == 2)
				{
					switch (array2[0])
					{
					case "lid":
					{
						int result = 0;
						int.TryParse(array2[1], out result);
						lotId = result;
						break;
					}
					}
				}
			}
		}
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (canUse() && isNear && (Object)self != (Object)null && self.IsChangeableAction(GetTargetActionId()))
		{
			if ((Object)targetMarkerTrans == (Object)null)
			{
				targetMarkerTrans = EffectManager.GetEffect(GetMarkerName(), _transform);
			}
			if ((Object)targetMarkerTrans != (Object)null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized + Vector3.up + _transform.position;
				targetMarkerTrans.Set(pos, rotation);
			}
		}
		else if ((Object)targetMarkerTrans != (Object)null)
		{
			EffectManager.ReleaseEffect(targetMarkerTrans.gameObject, true, false);
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
		if (!((Object)player == (Object)null) && !userList.Contains(player))
		{
			userList.Add(player);
			if (isSend)
			{
				player.playerSender.OnGatherGimmickInfo(m_id, true);
			}
		}
	}

	public void OnUseEnd(Player player, bool isSend = false)
	{
		if (!((Object)player == (Object)null) && userList.Contains(player))
		{
			userList.Remove(player);
			if (isSend)
			{
				player.playerSender.OnGatherGimmickInfo(m_id, false);
			}
		}
	}

	private bool IsValid()
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
		if ((Object)self == (Object)null)
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

	public new virtual GATHER_GIMMICK_TYPE GetType()
	{
		return GATHER_GIMMICK_TYPE.NONE;
	}

	public virtual Character.ACTION_ID GetTargetActionId()
	{
		return (Character.ACTION_ID)27;
	}

	public virtual bool isUseOnly()
	{
		return true;
	}

	public virtual bool canUse()
	{
		if (!isUseOnly())
		{
			return true;
		}
		for (int i = 0; i < userList.Count; i++)
		{
			bool flag = false;
			Player player = userList[i];
			if ((Object)player == (Object)null)
			{
				flag = true;
			}
			else
			{
				StageObject x = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(player.id);
				if ((Object)x == (Object)null)
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
