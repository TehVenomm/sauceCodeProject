using System;
using System.Collections.Generic;
using UnityEngine;

public class ContinusAttackParam
{
	[Serializable]
	public class ContinusAtkData
	{
		public int eventIndex;

		public float endTime;

		public AnimEventCollider eventCollider;

		public Transform effectTrans;

		public void Release()
		{
			if (effectTrans != null)
			{
				EffectManager.ReleaseEffect(effectTrans.get_gameObject());
				effectTrans = null;
			}
			if (eventCollider != null)
			{
				eventCollider.Destroy();
				eventCollider = null;
			}
		}
	}

	[Serializable]
	public class SyncParam
	{
		public List<SyncData> syncDataList = new List<SyncData>();
	}

	[Serializable]
	public class SyncData
	{
		public int eventIndex;

		public float endTime;
	}

	private Character m_owner;

	private List<ContinusAtkData> m_continusAtkDataList = new List<ContinusAtkData>();

	public ContinusAttackParam(Character chara)
	{
		m_owner = chara;
	}

	public void Register(int eventIndex, float endTime, AnimEventCollider eventCollider, Transform effectTrans)
	{
		ContinusAtkData continusAtkData = SearchByIndex(eventIndex);
		if (continusAtkData != null)
		{
			continusAtkData.Release();
			m_continusAtkDataList.Remove(continusAtkData);
		}
		continusAtkData = new ContinusAtkData();
		continusAtkData.eventIndex = eventIndex;
		continusAtkData.endTime = endTime;
		continusAtkData.eventCollider = eventCollider;
		continusAtkData.effectTrans = effectTrans;
		m_continusAtkDataList.Add(continusAtkData);
	}

	public void Update()
	{
		for (int num = m_continusAtkDataList.Count - 1; num >= 0; num--)
		{
			ContinusAtkData continusAtkData = m_continusAtkDataList[num];
			continusAtkData.endTime -= Time.get_deltaTime();
			if (continusAtkData.endTime <= 0f)
			{
				continusAtkData.Release();
				m_continusAtkDataList.Remove(continusAtkData);
			}
		}
	}

	public void RemoveAll()
	{
		if (m_continusAtkDataList != null)
		{
			foreach (ContinusAtkData continusAtkData in m_continusAtkDataList)
			{
				continusAtkData.Release();
			}
			m_continusAtkDataList.Clear();
		}
	}

	public ContinusAtkData SearchByIndex(int eventIndex)
	{
		int count = m_continusAtkDataList.Count;
		for (int i = 0; i < count; i++)
		{
			if (m_continusAtkDataList[i].eventIndex == eventIndex)
			{
				return m_continusAtkDataList[i];
			}
		}
		return null;
	}

	public SyncParam CreateSyncParam()
	{
		SyncParam syncParam = new SyncParam();
		foreach (ContinusAtkData continusAtkData in m_continusAtkDataList)
		{
			SyncData syncData = new SyncData();
			syncData.eventIndex = continusAtkData.eventIndex;
			syncData.endTime = continusAtkData.endTime;
			syncParam.syncDataList.Add(syncData);
		}
		return syncParam;
	}

	public void ApplySyncParam(SyncParam syncParam)
	{
		if (syncParam == null)
		{
			RemoveAll();
			return;
		}
		List<SyncData> syncDataList = syncParam.syncDataList;
		if (syncDataList == null)
		{
			RemoveAll();
		}
		else
		{
			foreach (SyncData item in syncDataList)
			{
				ContinusAtkData continusAtkData = SearchByIndex(item.eventIndex);
				if (continusAtkData != null)
				{
					continusAtkData.endTime = item.endTime;
				}
				else
				{
					m_owner.CreateContinusAttackBySyncData(item);
				}
			}
		}
	}
}
