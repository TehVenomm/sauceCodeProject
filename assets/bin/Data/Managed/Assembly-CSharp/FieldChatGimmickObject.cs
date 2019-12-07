using System.Collections.Generic;
using UnityEngine;

public class FieldChatGimmickObject : FieldGimmickObject
{
	public const string kTargetEffectName = "ef_btl_target_readstory_01";

	private Transform targetMarker;

	private Transform _transform;

	private UIChatGimmickGizmo _gizmo;

	private List<string> messageList = new List<string>();

	private float markerOffsetY;

	private float chatOffsetY;

	private float sqlRadius = 9f;

	private float npcScale = 1f;

	private int npcId = -1;

	public static bool IsValid(string value2)
	{
		if (!MonoBehaviourSingleton<DeliveryManager>.IsValid())
		{
			return false;
		}
		if (value2.IsNullOrWhiteSpace())
		{
			return false;
		}
		uint result = 0u;
		uint result2 = 0u;
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			string a = array2[0];
			if (!(a == "sdid"))
			{
				if (a == "edid")
				{
					uint.TryParse(array2[1], out result2);
				}
			}
			else
			{
				uint.TryParse(array2[1], out result);
			}
		}
		if (result2 != 0 && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2))
		{
			return false;
		}
		if (result != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result))
		{
			return false;
		}
		return true;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			_gizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateGimmick(this);
		}
		if (Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.I.GetNPCData(npcId)?.LoadModel(base.gameObject, need_shadow: true, enable_light_probe: true, delegate(Animator animator)
			{
				animator.gameObject.transform.localScale *= npcScale;
			}, useSpecialModel: false);
		}
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
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			switch (array2[0])
			{
			case "mid":
			{
				if (uint.TryParse(array2[1], out uint result))
				{
					string text = StringTable.Get(STRING_CATEGORY.GIMMICK, result);
					if (!text.IsNullOrWhiteSpace())
					{
						messageList.Add(text);
					}
				}
				break;
			}
			case "m":
				if (!array2[1].IsNullOrWhiteSpace())
				{
					messageList.Add(array2[1]);
				}
				break;
			case "ty":
				float.TryParse(array2[1], out markerOffsetY);
				break;
			case "cy":
				float.TryParse(array2[1], out chatOffsetY);
				break;
			case "r":
				float.TryParse(array2[1], out sqlRadius);
				break;
			case "npcScale":
				float.TryParse(array2[1], out npcScale);
				break;
			case "npcId":
				int.TryParse(array2[1], out npcId);
				break;
			}
		}
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		if (isNear && IsValid())
		{
			if (targetMarker == null && !string.IsNullOrEmpty("ef_btl_target_readstory_01"))
			{
				targetMarker = EffectManager.GetEffect("ef_btl_target_readstory_01", _transform);
			}
			if (targetMarker != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized + Vector3.up + _transform.position;
				pos.y += markerOffsetY;
				targetMarker.Set(pos, rotation);
			}
		}
		else if (targetMarker != null)
		{
			EffectManager.ReleaseEffect(targetMarker.gameObject);
		}
	}

	public bool StartChat()
	{
		if (!IsValid())
		{
			return false;
		}
		_gizmo.SayChat(messageList[Random.Range(0, messageList.Count)]);
		return true;
	}

	public override string GetObjectName()
	{
		return "ChatGimmick";
	}

	protected override void Awake()
	{
		_transform = base.transform;
		Utility.SetLayerWithChildren(base.transform, 19);
	}

	private bool IsValid()
	{
		if (messageList.IsNullOrEmpty())
		{
			return false;
		}
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
		if (_gizmo == null || _gizmo.isDisp())
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

	public Vector3 GetPosition()
	{
		Vector3 position = _transform.position;
		position.y += chatOffsetY;
		return position;
	}
}
