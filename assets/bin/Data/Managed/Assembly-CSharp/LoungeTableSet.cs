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

	public LoungeTableSet()
		: this()
	{
	}

	public ChairPoint GetNearSitPoint(Vector3 pos)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		float num = float.MaxValue;
		ChairPoint result = null;
		for (int i = 0; i < chairSitPoints.Count; i++)
		{
			float num2 = Vector3.Distance(pos, chairSitPoints[i].get_transform().get_position());
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		float num = float.MaxValue;
		TablePoint result = null;
		Vector3 val = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? MonoBehaviourSingleton<ClanManager>.I.IHomePeople.selfChara.get_transform().get_position() : MonoBehaviourSingleton<LoungeManager>.I.IHomePeople.selfChara.get_transform().get_position();
		for (int i = 0; i < tablePoints.Count; i++)
		{
			float num2 = Vector3.Distance(val, tablePoints[i].get_transform().get_position());
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
		yield return this.StartCoroutine(CreateTable());
		yield return this.StartCoroutine(CreateChair());
		isInitialized = true;
	}

	private IEnumerator CreateTable()
	{
		string TablePointsName = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? "ClanTablePoints" : "LoungeTablePoints";
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadTablePoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			TablePointsName
		});
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		Transform tablePoint = ResourceUtility.Realizes(loadTablePoints.loadedObject, this.get_transform());
		tablePoints = new List<TablePoint>(2);
		Utility.ForEach(tablePoint, delegate(Transform o)
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
		string ChairPointsName = (!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? "ClanChairPoints" : "LoungeChairPoints";
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadChairPoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			ChairPointsName
		});
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		Transform chairPoint = ResourceUtility.Realizes(loadChairPoints.loadedObject, this.get_transform());
		chairSitPoints = new List<ChairPoint>(8);
		Utility.ForEach(chairPoint, delegate(Transform o)
		{
			if (o.get_name().StartsWith("SIT"))
			{
				chairSitPoints.Add(o.GetComponent<ChairPoint>());
			}
			return false;
		});
	}
}
