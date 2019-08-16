using UnityEngine;

public class FieldQuestGimmickObject : FieldGatherGimmickObject
{
	private bool isUsing;

	private QuestTable.QuestTableData questData;

	private const int kGatherModelIndex = 10916;

	private uint gvid;

	public FieldMapTable.GatherPointViewTableData viewData
	{
		get;
		private set;
	}

	public static bool IsValidParam(string value2)
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
		uint result3 = 0u;
		uint result4 = 0u;
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
				case "gvid":
					uint.TryParse(array2[1], out result4);
					break;
				case "qid":
					uint.TryParse(array2[1], out result3);
					break;
				}
			}
		}
		if (result4 == 0)
		{
			return false;
		}
		if (result2 != 0 && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2))
		{
			return false;
		}
		if (result != 0 && !MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result))
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)result))
		{
			return false;
		}
		QuestTable.QuestTableData questTableData = Singleton<QuestTable>.I.GetQuestData(result3);
		if (questTableData == null)
		{
			return false;
		}
		return true;
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
			case "gvid":
				if (uint.TryParse(array2[1], out gvid))
				{
					viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gvid);
				}
				break;
			case "qid":
				if (uint.TryParse(array2[1], out uint result))
				{
					questData = Singleton<QuestTable>.I.GetQuestData(result);
				}
				break;
			}
		}
	}

	public override bool CanUse()
	{
		return !isUsing;
	}

	public bool StartAction(Player player)
	{
		if (!IsValid())
		{
			return false;
		}
		OnUseStart(player);
		return true;
	}

	public void OnUseStart(Player player)
	{
		if (!(player == null) && player is Self)
		{
			isUsing = true;
			player.playerSender.OnActQuestGimmick(m_id);
		}
	}

	protected override bool IsValid()
	{
		if (isUsing)
		{
			return false;
		}
		return base.IsValid();
	}

	public void OnEndAction()
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.OpenAllDropObject();
		}
		MonoBehaviourSingleton<InGameProgress>.I.GimmickQuestDirection(questData.questID);
	}

	public override string GetObjectName()
	{
		return "QuestGimmick";
	}

	public override string GetMarkerName()
	{
		if (viewData != null)
		{
			return viewData.targetEffectName;
		}
		return "ef_btl_target_unknown_01";
	}

	public static FieldMapTable.GatherPointViewTableData GetGatherPointData(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return null;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				string text = array2[0];
				if (text != null && text == "gvid" && uint.TryParse(array2[1], out uint result))
				{
					return Singleton<FieldMapTable>.I.GetGatherPointViewData(result);
				}
			}
		}
		return null;
	}

	protected override void CreateModel()
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
		{
			return;
		}
		LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldGimmickObject.ConvertModelIndexToKey(m_gimmickType, (int)gvid));
		if (loadObject != null)
		{
			modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform);
		}
		if (!(modelTrans != null) || viewData == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(viewData.gatherEffectName))
		{
			Transform effect = EffectManager.GetEffect(viewData.gatherEffectName, _transform);
			if (effect != null)
			{
				effect.get_gameObject().SetActive(true);
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() * viewData.targetEffectShift + Vector3.get_up() * viewData.targetEffectHeight + _transform.get_position();
				effect.Set(pos, rotation);
			}
		}
		sqlRadius = viewData.targetRadius * viewData.targetRadius;
		if (viewData.colRadius > 0f)
		{
			SphereCollider val2 = this.get_gameObject().AddComponent<SphereCollider>();
			val2.set_center(new Vector3(0f, 0f, 0f));
			val2.set_radius(viewData.colRadius);
		}
	}
}
