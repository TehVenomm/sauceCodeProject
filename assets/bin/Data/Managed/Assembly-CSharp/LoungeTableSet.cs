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

	public ChairPoint GetNearSitPoint(HomePlayerCharacterBase character)
	{
		float num = 3.40282347E+38f;
		ChairPoint result = null;
		Vector3 position = character.transform.position;
		for (int i = 0; i < chairSitPoints.Count; i++)
		{
			float num2 = Vector3.Distance(position, chairSitPoints[i].transform.position);
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
		float num = 3.40282347E+38f;
		TablePoint result = null;
		Vector3 position = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara.transform.position;
		for (int i = 0; i < tablePoints.Count; i++)
		{
			float num2 = Vector3.Distance(position, tablePoints[i].transform.position);
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
		yield return (object)StartCoroutine(CreateTable());
		yield return (object)StartCoroutine(CreateChair());
		isInitialized = true;
	}

	private IEnumerator CreateTable()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadTablePoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			"LoungeTablePoints"
		}, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		Transform tablePoint = ResourceUtility.Realizes(loadTablePoints.loadedObject, base.transform, -1);
		tablePoints = new List<TablePoint>(2);
		Utility.ForEach(tablePoint, delegate(Transform o)
		{
			if ((Object)o.GetComponent<TablePoint>() != (Object)null)
			{
				((_003CCreateTable_003Ec__IteratorF8)/*Error near IL_00bf: stateMachine*/)._003C_003Ef__this.tablePoints.Add(o.GetComponent<TablePoint>());
			}
			return false;
		});
	}

	private IEnumerator CreateChair()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadChairPoints = loadQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemOutGame", new string[1]
		{
			"LoungeChairPoints"
		}, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		Transform chairPoint = ResourceUtility.Realizes(loadChairPoints.loadedObject, base.transform, -1);
		chairSitPoints = new List<ChairPoint>(8);
		Utility.ForEach(chairPoint, delegate(Transform o)
		{
			if (o.name.StartsWith("SIT"))
			{
				((_003CCreateChair_003Ec__IteratorF9)/*Error near IL_00bf: stateMachine*/)._003C_003Ef__this.chairSitPoints.Add(o.GetComponent<ChairPoint>());
			}
			return false;
		});
	}
}
