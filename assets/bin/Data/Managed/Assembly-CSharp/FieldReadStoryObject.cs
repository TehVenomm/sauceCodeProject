using System.Collections.Generic;
using UnityEngine;

public class FieldReadStoryObject : FieldGimmickObject
{
	private Transform targetMarker;

	private Transform _transform;

	private uint deliveryId;

	private List<int> subStoryIds = new List<int>();

	private uint endDeliveryId;

	private float markerOffsetY;

	private float sqlRadius = 4f;

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
		int num = 0;
		uint result2 = 0u;
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
			case "did":
				uint.TryParse(array2[1], out result);
				break;
			case "sid":
			{
				int result3 = 0;
				if (int.TryParse(array2[1], out result3))
				{
					num = result3;
				}
				break;
			}
			case "edid":
				uint.TryParse(array2[1], out result2);
				break;
			}
		}
		if (result == 0 && num == 0)
		{
			return false;
		}
		if (result2 != 0 && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2))
		{
			return false;
		}
		if (result != 0)
		{
			if (!MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result))
			{
				return false;
			}
			if (num == 0)
			{
				return !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(result) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)result);
			}
		}
		return true;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		if (npcId != -1 && Singleton<NPCTable>.IsValid())
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
			case "did":
				uint.TryParse(array2[1], out deliveryId);
				break;
			case "sid":
				if (int.TryParse(array2[1], out int result))
				{
					subStoryIds.Add(result);
				}
				break;
			case "edid":
				uint.TryParse(array2[1], out endDeliveryId);
				break;
			case "ty":
				float.TryParse(array2[1], out markerOffsetY);
				break;
			case "r":
				if (!float.TryParse(array2[1], out sqlRadius))
				{
					sqlRadius = 4f;
				}
				break;
			case "npcId":
				int.TryParse(array2[1], out npcId);
				break;
			case "npcScale":
				float.TryParse(array2[1], out npcScale);
				break;
			}
		}
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)39))
		{
			string readStoryTargetEffectName = ResourceName.GetReadStoryTargetEffectName();
			if (targetMarker == null && !string.IsNullOrEmpty(readStoryTargetEffectName))
			{
				targetMarker = EffectManager.GetEffect(readStoryTargetEffectName, _transform);
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

	public void StartReadStory()
	{
		if (!IsValidReadStory() || !Singleton<DeliveryTable>.IsValid())
		{
			return;
		}
		int num = 0;
		bool isSend = true;
		if (deliveryId != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)deliveryId))
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
			if (deliveryTableData != null && deliveryTableData.readScriptId != 0)
			{
				num = (int)deliveryTableData.readScriptId;
				isSend = true;
			}
		}
		if (num == 0 && subStoryIds.Count > 0)
		{
			num = subStoryIds[Random.Range(0, subStoryIds.Count)];
			isSend = false;
		}
		if (num != 0)
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory(num, isSend);
		}
	}

	public override string GetObjectName()
	{
		return "ReadStory";
	}

	protected override void Awake()
	{
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
	}

	private bool IsValidReadStory()
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
}
