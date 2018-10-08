using System.Collections.Generic;
using UnityEngine;

public class WaveMatchDropResource
{
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
		if (Singleton<WaveMatchDropTable>.IsValid())
		{
			List<WaveMatchDropTable.WaveMatchDropData> allData = Singleton<WaveMatchDropTable>.I.GetAllData();
			if (allData != null)
			{
				dropModelList = new Dictionary<string, LoadObject>();
				dropModelList.Clear();
				List<string> list = new List<string>();
				List<int> list2 = new List<int>();
				for (int i = 0; i < allData.Count; i++)
				{
					WaveMatchDropTable.WaveMatchDropData waveMatchDropData = allData[i];
					if (!dropModelList.ContainsKey(waveMatchDropData.model))
					{
						dropModelList.Add(waveMatchDropData.model, loadQueue.Load(RESOURCE_CATEGORY.STAGE_GIMMICK, waveMatchDropData.model, false));
					}
					if (!waveMatchDropData.getEffect.IsNullOrWhiteSpace() && !list.Contains(waveMatchDropData.getEffect))
					{
						loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchDropData.getEffect);
						list.Add(waveMatchDropData.getEffect);
					}
					if (waveMatchDropData.getSE != 0 && !list2.Contains(waveMatchDropData.getSE))
					{
						loadQueue.CacheSE(waveMatchDropData.getSE, null);
						list2.Add(waveMatchDropData.getSE);
					}
				}
				list.Clear();
				list = null;
				list2.Clear();
				list2 = null;
			}
		}
	}

	public void Create(Coop_Model_WaveMatchDrop model)
	{
		if (Singleton<WaveMatchDropTable>.IsValid() && dropModelList != null && model.fiIds != null && model.fiIds.Count != 0)
		{
			for (int i = 0; i < model.fiIds.Count; i++)
			{
				uint num = (uint)model.fiIds[i];
				WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(num);
				if (data != null && dropModelList.ContainsKey(data.model))
				{
					Vector3 offset = Vector3.zero;
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.FieldDropItem fieldDrop = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
						float value = Random.value;
						float value2 = Random.value;
						float value3 = Random.value;
						float num2 = (!(Random.value > 0.5f)) ? 1f : (-1f);
						float num3 = (!(Random.value > 0.5f)) ? 1f : (-1f);
						offset = new Vector3(Mathf.Lerp(fieldDrop.offsetMin.x, fieldDrop.offsetMax.x, value) * num2, Mathf.Lerp(fieldDrop.offsetMin.y, fieldDrop.offsetMax.y, value2), Mathf.Lerp(fieldDrop.offsetMin.z, fieldDrop.offsetMax.z, value3) * num3);
					}
					OnCreate(MonoBehaviourSingleton<StageObjectManager>.I.waveMatchDropObjIndex++, num, new Vector3((float)model.x, 0f, (float)model.z), offset, model.sec, true);
				}
			}
		}
	}

	public void OnCreate(int manageId, uint dataId, Vector3 basePos, Vector3 offset, float sec, bool send = false)
	{
		WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(dataId);
		if (data != null && dropModelList.ContainsKey(data.model))
		{
			LoadObject loadObject = dropModelList[data.model];
			if (loadObject != null)
			{
				Transform transform = ResourceUtility.Realizes(loadObject.loadedObject, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
				if (!((Object)transform == (Object)null))
				{
					WaveMatchDropObject waveMatchDropObject = null;
					switch (data.type)
					{
					case WAVEMATCH_ITEM_TYPE.HEAL_HP:
						waveMatchDropObject = transform.gameObject.AddComponent<WaveMatchDropObjectHealHp>();
						break;
					case WAVEMATCH_ITEM_TYPE.HEAL_SKILL:
						waveMatchDropObject = transform.gameObject.AddComponent<WaveMatchDropObjectHealSkill>();
						break;
					case WAVEMATCH_ITEM_TYPE.CLOCK:
						waveMatchDropObject = transform.gameObject.AddComponent<WaveMatchDropObjectClock>();
						break;
					default:
						waveMatchDropObject = transform.gameObject.AddComponent<WaveMatchDropObject>();
						break;
					}
					if ((Object)waveMatchDropObject == (Object)null)
					{
						transform = null;
					}
					else
					{
						waveMatchDropObject.Initialize(manageId, basePos, offset, sec, data);
						if (send)
						{
							MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnCreateWaveMatchDropObject(manageId, dataId, basePos, offset, sec);
						}
					}
				}
			}
		}
	}
}
