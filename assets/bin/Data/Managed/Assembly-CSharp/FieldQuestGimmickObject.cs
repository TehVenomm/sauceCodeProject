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
		if (Singleton<QuestTable>.I.GetQuestData(result3) == null)
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
			string a = array2[0];
			if (!(a == "gvid"))
			{
				if (a == "qid" && uint.TryParse(array2[1], out uint result))
				{
					questData = Singleton<QuestTable>.I.GetQuestData(result);
				}
			}
			else if (uint.TryParse(array2[1], out gvid))
			{
				viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gvid);
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
				string a = array2[0];
				if (a == "gvid" && uint.TryParse(array2[1], out uint result))
				{
					return Singleton<FieldMapTable>.I.GetGatherPointViewData(result);
				}
			}
		}
		return null;
	}

	protected override void CreateModel()
	{
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
				effect.gameObject.SetActive(value: true);
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized * viewData.targetEffectShift + Vector3.up * viewData.targetEffectHeight + _transform.position;
				effect.Set(pos, rotation);
			}
		}
		sqlRadius = viewData.targetRadius * viewData.targetRadius;
		if (viewData.colRadius > 0f)
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.center = new Vector3(0f, 0f, 0f);
			sphereCollider.radius = viewData.colRadius;
		}
	}
}
