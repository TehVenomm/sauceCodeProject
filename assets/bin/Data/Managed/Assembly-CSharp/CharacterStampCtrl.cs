using rhyme;
using UnityEngine;

public class CharacterStampCtrl : MonoBehaviour
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

	public void Init(StageObject.StampInfo[] stamp_nodes, Character _owner, bool is_direction = false)
	{
		stampInfos = stamp_nodes;
		isDirection = is_direction;
		_transform = base.transform;
		owner = _owner;
		isPlayer = (_owner is Player);
		isSelf = (_owner is Self);
		enableAutoStampEffect = true;
		stampNodes = base.gameObject.GetComponentsInChildren<StampNode>();
	}

	private void Update()
	{
		if ((!isDirection && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0) || (!isDirection && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1 && FieldManager.IsValidInGameNoQuest() && isPlayer && !isSelf))
		{
			return;
		}
		bool flag = false;
		if (isDirection || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 2)
		{
			flag = true;
		}
		if (stampNodes == null || stampNodes.Length == 0 || stampInfos == null || stampInfos.Length == 0 || (!flag && !CheckDistance()))
		{
			return;
		}
		float y = _transform.position.y;
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
		Vector3 position = stamp_node._transform.position;
		position += stamp_node._transform.rotation * stamp_node.scaledeOffset;
		position = StageManager.FitHeight(position);
		string effectName = stamp_info.effectName;
		if (!string.IsNullOrEmpty(effectName))
		{
			EffectManager.OneShot(effectName, position, _transform.rotation, _transform.localScale * stamp_info.effectScale, isSelf, delegate(Transform effect)
			{
				SceneSettingsManager.ApplyEffect(effect.gameObject.GetComponent<rymFX>(), force: true);
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
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return true;
		}
		return Vector3.Distance(MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.position, _transform.position) < stampDistance;
	}
}
