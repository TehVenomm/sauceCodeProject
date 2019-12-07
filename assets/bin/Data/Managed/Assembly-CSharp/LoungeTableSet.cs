using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeTableSet : MonoBehaviour
{
	public bool isInitialized
	{
		get;
		private set;
	}

	public List<TablePoint> tablePoints
	{
		get;
		private set;
	}

	public List<ChairPoint> chairSitPoints
	{
		get;
		private set;
	}

	public ChairPoint GetNearSitPoint(Vector3 pos)
	{
		float num = float.MaxValue;
		ChairPoint result = null;
		for (int i = 0; i < chairSitPoints.Count; i++)
		{
			float num2 = Vector3.Distance(pos, chairSitPoints[i].transform.position);
			if (num > num2)
			{
				result = chairSitPoints[i];
				num = num2;
			}
		}
		return result;
	}

	public TablePoint GetNearTablePoint()
	{
		float num = float.MaxValue;
		TablePoint result = null;
		Vector3 a = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara.transform.position : MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara.transform.position;
		for (int i = 0; i < tablePoints.Count; i++)
		{
			float num2 = Vector3.Distance(a, tablePoints[i].transform.position);
			if (num > num2)
			{
				result = tablePoints[i];
				num = num2;
			}
		}
		return result;
	}

	private IEnumerator Start()
	{
		yield return StartCoroutine(CreateTable());
		yield return StartCoroutine(CreateChair());
		isInitialized = true;
	}

	private IEnumerator CreateTable()
	{
		string text = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? "ClanTablePoints" : "LoungeTablePoints";
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadTablePoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			text
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Transform transform = ResourceUtility.Realizes(loadTablePoints.loadedObject, base.transform);
		tablePoints = new List<TablePoint>(2);
		Utility.ForEach(transform, delegate(Transform o)
		{
			if (o.GetComponent<TablePoint>() != null)
			{
				tablePoints.Add(o.GetComponent<TablePoint>());
			}
			return false;
		});
	}

	private IEnumerator CreateChair()
	{
		string text = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? "ClanChairPoints" : "LoungeChairPoints";
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadChairPoints = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			text
		});
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Transform transform = ResourceUtility.Realizes(loadChairPoints.loadedObject, base.transform);
		chairSitPoints = new List<ChairPoint>(8);
		Utility.ForEach(transform, delegate(Transform o)
		{
			if (o.name.StartsWith("SIT"))
			{
				chairSitPoints.Add(o.GetComponent<ChairPoint>());
			}
			return false;
		});
	}
}
