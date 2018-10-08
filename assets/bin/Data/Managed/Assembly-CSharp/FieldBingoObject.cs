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

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		base.Initialize(pointData);
		ParseParam(pointData.value2);
		if (Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.I.GetNPCData(npcId)?.LoadModel(this.get_gameObject(), true, true, delegate(Animator animator)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				Transform transform = animator.get_gameObject().get_transform();
				transform.set_localScale(transform.get_localScale() * npcScale);
			}, false);
		}
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
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)38))
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
			EffectManager.ReleaseEffect(targetMarker.get_gameObject(), true, false);
		}
	}

	public void OpenBingo()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		if (IsValidBingo())
		{
			if (deliveryId != 0 && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)deliveryId))
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
				if (deliveryTableData != null && deliveryTableData.readScriptId != 0)
				{
					MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int)deliveryTableData.readScriptId, true);
					return;
				}
			}
			if (bingoId != 0)
			{
				SoundManager.PlaySystemSE(SoundID.UISE.POP_QUEST, 1f);
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("FieldBingoObject", this.get_gameObject(), "BINGO", new object[2]
					{
						bingoId,
						false
					}, null, true);
				}
			}
		}
	}

	public override string GetObjectName()
	{
		return "Bingo";
	}

	protected override void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
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
