using System.Collections.Generic;
using UnityEngine;

public class WaveMatchDropResource
{
	public const string kCommonEffectName = "ef_btl_target_dropitem_01";

	private Dictionary<string, LoadObject> dropModelList;

	public void Clear()
	{
		if (dropModelList != null)
		{
			dropModelList.Clear();
		}
		dropModelList = null;
	}

	public void Cache(LoadingQueue loadQueue)
	{
		if (!Singleton<WaveMatchDropTable>.IsValid())
		{
			return;
		}
		List<WaveMatchDropTable.WaveMatchDropData> allData = Singleton<WaveMatchDropTable>.I.GetAllData();
		if (allData == null)
		{
			return;
		}
		dropModelList = new Dictionary<string, LoadObject>();
		dropModelList.Clear();
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < allData.Count; i++)
		{
			WaveMatchDropTable.WaveMatchDropData waveMatchDropData = allData[i];
			if (!dropModelList.ContainsKey(waveMatchDropData.model))
			{
				dropModelList.Add(waveMatchDropData.model, loadQueue.Load(RESOURCE_CATEGORY.STAGE_GIMMICK, waveMatchDropData.model));
			}
			if (!waveMatchDropData.getEffect.IsNullOrWhiteSpace() && !list.Contains(waveMatchDropData.getEffect))
			{
				loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchDropData.getEffect);
				list.Add(waveMatchDropData.getEffect);
			}
			if (waveMatchDropData.getSE != 0 && !list2.Contains(waveMatchDropData.getSE))
			{
				loadQueue.CacheSE(waveMatchDropData.getSE);
				list2.Add(waveMatchDropData.getSE);
			}
		}
		loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_dropitem_01");
		list.Clear();
		list = null;
		list2.Clear();
		list2 = null;
	}

	public void Create(Coop_Model_WaveMatchDrop model)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		if (!Singleton<WaveMatchDropTable>.IsValid() || dropModelList == null || model.fiIds == null || model.fiIds.Count == 0)
		{
			return;
		}
		for (int i = 0; i < model.fiIds.Count; i++)
		{
			uint num = (uint)model.fiIds[i];
			WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(num);
			if (data != null && dropModelList.ContainsKey(data.model))
			{
				Vector3 zero = Vector3.get_zero();
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					InGameSettingsManager.FieldDropItem fieldDrop = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
					float value = Random.get_value();
					float value2 = Random.get_value();
					float value3 = Random.get_value();
					float num2 = (!(Random.get_value() > 0.5f)) ? 1f : (-1f);
					float num3 = (!(Random.get_value() > 0.5f)) ? 1f : (-1f);
					zero._002Ector(Mathf.Lerp(fieldDrop.offsetMin.x, fieldDrop.offsetMax.x, value) * num2, Mathf.Lerp(fieldDrop.offsetMin.y, fieldDrop.offsetMax.y, value2), Mathf.Lerp(fieldDrop.offsetMin.z, fieldDrop.offsetMax.z, value3) * num3);
				}
				OnCreate(MonoBehaviourSingleton<StageObjectManager>.I.waveMatchDropObjIndex++, num, new Vector3((float)model.x, 0f, (float)model.z), zero, model.sec, send: true);
			}
		}
	}

	public void OnCreate(int manageId, uint dataId, Vector3 basePos, Vector3 offset, float sec, bool send = false)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(dataId);
		if (data == null || !dropModelList.ContainsKey(data.model))
		{
			return;
		}
		LoadObject loadObject = dropModelList[data.model];
		if (loadObject == null)
		{
			return;
		}
		Transform val = ResourceUtility.Realizes(loadObject.loadedObject, MonoBehaviourSingleton<StageObjectManager>.I._transform);
		if (val == null)
		{
			return;
		}
		WaveMatchDropObject waveMatchDropObject = null;
		switch (data.type)
		{
		case WAVEMATCH_ITEM_TYPE.HEAL_HP:
			waveMatchDropObject = val.get_gameObject().AddComponent<WaveMatchDropObjectHealHp>();
			break;
		case WAVEMATCH_ITEM_TYPE.HEAL_SKILL:
			waveMatchDropObject = val.get_gameObject().AddComponent<WaveMatchDropObjectHealSkill>();
			break;
		case WAVEMATCH_ITEM_TYPE.CLOCK:
			waveMatchDropObject = val.get_gameObject().AddComponent<WaveMatchDropObjectClock>();
			break;
		default:
			waveMatchDropObject = val.get_gameObject().AddComponent<WaveMatchDropObject>();
			break;
		}
		if (waveMatchDropObject == null)
		{
			val = null;
			return;
		}
		waveMatchDropObject.Initialize(manageId, basePos, offset, sec, data);
		if (send)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnCreateWaveMatchDropObject(manageId, dataId, basePos, offset, sec);
		}
	}
}
