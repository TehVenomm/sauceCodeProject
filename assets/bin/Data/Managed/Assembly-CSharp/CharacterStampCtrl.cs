using rhyme;
using UnityEngine;

public class CharacterStampCtrl
{
	private bool isPlayer;

	private bool isSelf;

	public StageObject.StampInfo[] stampInfos;

	public bool enableAutoStampEffect = true;

	public float stampDistance = 5f;

	public int effectLayer = -1;

	public Transform _transform
	{
		get;
		private set;
	}

	public Character owner
	{
		get;
		private set;
	}

	public bool isDirection
	{
		get;
		protected set;
	}

	public StampNode[] stampNodes
	{
		get;
		set;
	}

	public CharacterStampCtrl()
		: this()
	{
	}

	public void Init(StageObject.StampInfo[] stamp_nodes, Character _owner, bool is_direction = false)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		stampInfos = stamp_nodes;
		isDirection = is_direction;
		_transform = this.get_transform();
		owner = _owner;
		isPlayer = (_owner is Player);
		isSelf = (_owner is Self);
		enableAutoStampEffect = true;
		stampNodes = this.get_gameObject().GetComponentsInChildren<StampNode>();
	}

	private void Update()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		if ((isDirection || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0) && (isDirection || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 1 || !FieldManager.IsValidInGameNoQuest() || !isPlayer || isSelf))
		{
			bool flag = false;
			if (isDirection || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 2)
			{
				flag = true;
			}
			if (stampNodes != null && stampNodes.Length > 0 && stampInfos != null && stampInfos.Length > 0 && (flag || CheckDistance()))
			{
				Vector3 position = _transform.get_position();
				float y = position.y;
				int i = 0;
				for (int num = stampNodes.Length; i < num; i++)
				{
					StampNode stampNode = stampNodes[i];
					if (stampNode.UpdateStamp(y) && enableAutoStampEffect)
					{
						StageObject.StampInfo stamp_info = (!(owner != null)) ? stampInfos[0] : ((owner.actionID != Character.ACTION_ID.ATTACK || stampInfos.Length < 2) ? stampInfos[0] : stampInfos[1]);
						PlayStampEffect(stamp_info, stampNode);
					}
				}
			}
		}
	}

	public bool OnAnimEvent(AnimEventData.EventData data)
	{
		switch (data.id)
		{
		case AnimEventFormat.ID.STAMP:
		{
			if (!CheckDistance())
			{
				return true;
			}
			int num = data.intArgs[0];
			if (stampInfos == null || stampNodes == null)
			{
				return true;
			}
			int i = 0;
			for (int num2 = stampNodes.Length; i < num2; i++)
			{
				StampNode stampNode = stampNodes[i];
				int j = 0;
				for (int num3 = stampNode.triggers.Length; j < num3; j++)
				{
					StampNode.StampTrigger stampTrigger = stampNode.triggers[j];
					if (stampTrigger.eventID == num)
					{
						StageObject.StampInfo stamp_info = stampInfos[stampTrigger.StampInfoID];
						PlayStampEffect(stamp_info, stampNode);
						break;
					}
				}
			}
			return true;
		}
		case AnimEventFormat.ID.AUTO_STAMP_ON:
			enableAutoStampEffect = true;
			return true;
		case AnimEventFormat.ID.AUTO_STAMP_OFF:
			enableAutoStampEffect = false;
			return true;
		default:
			return false;
		}
	}

	protected void PlayStampEffect(StageObject.StampInfo stamp_info, StampNode stamp_node)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = stamp_node._transform.get_position();
		position += stamp_node._transform.get_rotation() * stamp_node.scaledeOffset;
		position = StageManager.FitHeight(position);
		string effectName = stamp_info.effectName;
		if (!string.IsNullOrEmpty(effectName))
		{
			EffectManager.OneShot(effectName, position, _transform.get_rotation(), _transform.get_localScale() * stamp_info.effectScale, isSelf, delegate(Transform effect)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				SceneSettingsManager.ApplyEffect(effect.get_gameObject().GetComponent<rymFX>(), true);
				if (effectLayer != -1)
				{
					Utility.SetLayerWithChildren(effect, effectLayer);
				}
			});
		}
		if (stamp_info.shakeCameraPercent > 0f && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(position, stamp_info.shakeCameraPercent, stamp_info.shakeCycleTime);
		}
		if (stamp_info.seID != 0)
		{
			SoundManager.PlayOneShotSE(stamp_info.seID, position);
		}
	}

	private bool CheckDistance()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return true;
		}
		return Vector3.Distance(MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_position(), _transform.get_position()) < stampDistance;
	}
}
