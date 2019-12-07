using UnityEngine;

public class FieldBingoObject : FieldGimmickObject
{
	private Transform targetMarker;

	private Transform _transform;

	private uint deliveryId;

	private uint bingoId;

	private float markerOffsetY = 1f;

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
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				string a = array2[0];
				if (a == "cdid")
				{
					uint.TryParse(array2[1], out result);
				}
			}
		}
		if (result != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(result))
		{
			return false;
		}
		return true;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
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
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "did":
					uint.TryParse(array2[1], out deliveryId);
					break;
				case "bid":
					uint.TryParse(array2[1], out bingoId);
					break;
				case "ty":
					float.TryParse(array2[1], out markerOffsetY);
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
	}

	public override void UpdateTargetMarker(bool isNear)
	{
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

	public void OpenBingo()
	{
		if (!IsValidBingo())
		{
			return;
		}
		if (deliveryId != 0 && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)deliveryId))
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
			if (deliveryTableData != null && deliveryTableData.readScriptId != 0)
			{
				MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int)deliveryTableData.readScriptId, isSend: true);
				return;
			}
		}
		if (bingoId != 0)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("FieldBingoObject", base.gameObject, "BINGO", new object[2]
				{
					bingoId,
					false
				});
			}
		}
	}

	public override string GetObjectName()
	{
		return "Bingo";
	}

	protected override void Awake()
	{
		_transform = base.transform;
		Utility.SetLayerWithChildren(base.transform, 19);
	}

	private bool IsValidBingo()
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
		if (deliveryId != 0 && !Singleton<DeliveryTable>.IsValid())
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
