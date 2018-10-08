using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeTableSet
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

	public ChairPoint GetNearSitPoint(HomePlayerCharacterBase character)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		float num = 3.40282347E+38f;
		ChairPoint result = null;
		Vector3 position = character.get_transform().get_position();
		for (int i = 0; i < chairSitPoints.Count; i++)
		{
			float num2 = Vector3.Distance(position, chairSitPoints[i].get_transform().get_position());
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		float num = 3.40282347E+38f;
		TablePoint result = null;
		Vector3 position = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara.get_transform().get_position();
		for (int i = 0; i < tablePoints.Count; i++)
		{
			float num2 = Vector3.Distance(position, tablePoints[i].get_transform().get_position());
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
		yield return (object)this.StartCoroutine(CreateTable());
		yield return (object)this.StartCoroutine(CreateChair());
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
		Transform tablePoint = ResourceUtility.Realizes(loadTablePoints.loadedObject, this.get_transform(), -1);
		tablePoints = new List<TablePoint>(2);
		Utility.ForEach(tablePoint, delegate(Transform o)
		{
			if (o.GetComponent<TablePoint>() != null)
			{
				((_003CCreateTable_003Ec__IteratorF6)/*Error near IL_00bf: stateMachine*/)._003C_003Ef__this.tablePoints.Add(o.GetComponent<TablePoint>());
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
		Transform chairPoint = ResourceUtility.Realizes(loadChairPoints.loadedObject, this.get_transform(), -1);
		chairSitPoints = new List<ChairPoint>(8);
		Utility.ForEach(chairPoint, delegate(Transform o)
		{
			if (o.get_name().StartsWith("SIT"))
			{
				((_003CCreateChair_003Ec__IteratorF7)/*Error near IL_00bf: stateMachine*/)._003C_003Ef__this.chairSitPoints.Add(o.GetComponent<ChairPoint>());
			}
			return false;
		});
	}
}
