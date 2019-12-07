using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailDistanceDecayMiniDialog : GameSection
{
	private enum UI
	{
		GRAPH_START
	}

	private const float GRAPH_HEIGHT = 150f;

	private const float GRAPH_WIDTH = 300f;

	private const float MAX_RATE = 1f;

	private const float MAX_DISTANCE = 30f;

	private GameObject circlePrefab;

	private GameObject barPrefab;

	private Transform startPoint;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DamageDistanceTable";
		}
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject distanceCircleObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, "DistanceCircle");
		LoadObject distanceBarObject = loadingQueue.Load(RESOURCE_CATEGORY.UI, "DistanceBar");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		circlePrefab = (distanceCircleObject.loadedObject as GameObject);
		barPrefab = (distanceBarObject.loadedObject as GameObject);
		startPoint = GetCtrl(UI.GRAPH_START);
		int id = (int)(GameSection.GetEventData() as object[])[0];
		CreateGraph((uint)id);
		base.Initialize();
	}

	private void CreateGraph(uint id)
	{
		DamageDistanceTable.DamageDistanceData data = Singleton<DamageDistanceTable>.I.GetData(id);
		for (int i = 0; i < data.points.Length; i++)
		{
			DamageDistanceTable.DamagePoint damagePoint = data.points[i];
			CreatePoint(damagePoint);
			bool num = i >= data.points.Length - 1;
			DamageDistanceTable.DamagePoint damagePoint2 = null;
			if (!num)
			{
				damagePoint2 = data.points[i + 1];
			}
			else if ((float)damagePoint.distance < 30f)
			{
				damagePoint2 = new DamageDistanceTable.DamagePoint();
				damagePoint2.distance = 30f;
				damagePoint2.rate = damagePoint.rate;
			}
			if (damagePoint2 != null)
			{
				CreatePoint(damagePoint2);
				CreateBar(damagePoint, damagePoint2);
			}
		}
	}

	private void CreatePoint(DamageDistanceTable.DamagePoint point)
	{
		Transform transform = ResourceUtility.Realizes(circlePrefab, startPoint, 5);
		Vector3 pos = GetPos(point);
		transform.transform.localPosition = pos;
	}

	private void CreateBar(DamageDistanceTable.DamagePoint point, DamageDistanceTable.DamagePoint nextPoint)
	{
		Vector3 pos = GetPos(point);
		Vector3 pos2 = GetPos(nextPoint);
		float angle = GetAngle(pos, pos2);
		float num = Vector3.Distance(pos, pos2);
		Transform transform = ResourceUtility.Realizes(barPrefab, startPoint, 5);
		transform.transform.localPosition = pos;
		transform.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.GetComponent<UISprite>().width = (int)num;
	}

	private Vector3 GetPos(DamageDistanceTable.DamagePoint point)
	{
		float x = (float)point.distance / 30f * 300f;
		float y = (float)point.rate / 1f * 150f;
		return new Vector3(x, y, 0f);
	}

	private float GetAngle(Vector3 pos, Vector3 next)
	{
		Vector3 normalized = (next - pos).normalized;
		float num = Vector3.Angle(Vector3.right, normalized);
		if (Vector3.Cross(Vector3.right, normalized).z < 0f)
		{
			return 0f - num;
		}
		return num;
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
