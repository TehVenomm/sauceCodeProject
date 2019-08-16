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
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "sdid":
					uint.TryParse(array2[1], out result);
					break;
				case "edid":
					uint.TryParse(array2[1], out result2);
					break;
				}
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
			Singleton<NPCTable>.I.GetNPCData(npcId)?.LoadModel(this.get_gameObject(), need_shadow: true, enable_light_probe: true, delegate(Animator animator)
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				Transform transform = animator.get_gameObject().get_transform();
				transform.set_localScale(transform.get_localScale() * npcScale);
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
				if (uint.TryParse(array2[1], out uint result))
				{
					string text = StringTable.Get(STRING_CATEGORY.GIMMICK, result);
					if (!text.IsNullOrWhiteSpace())
					{
						messageList.Add(text);
					}
				}
				break;
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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (isNear && IsValid())
		{
			if (targetMarker == null && !string.IsNullOrEmpty("ef_btl_target_readstory_01"))
			{
				targetMarker = EffectManager.GetEffect("ef_btl_target_readstory_01", _transform);
			}
			if (targetMarker != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() + Vector3.get_up() + _transform.get_position();
				pos.y += markerOffsetY;
				targetMarker.Set(pos, rotation);
			}
		}
		else if (targetMarker != null)
		{
			EffectManager.ReleaseEffect(targetMarker.get_gameObject());
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
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = _transform.get_position();
		position.y += chatOffsetY;
		return position;
	}
}
