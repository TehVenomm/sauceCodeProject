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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject distanceCircleObject = loadQueue.Load(RESOURCE_CATEGORY.UI, "DistanceCircle", false);
		LoadObject distanceBarObject = loadQueue.Load(RESOURCE_CATEGORY.UI, "DistanceBar", false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		circlePrefab = (distanceCircleObject.loadedObject as GameObject);
		barPrefab = (distanceBarObject.loadedObject as GameObject);
		startPoint = GetCtrl(UI.GRAPH_START);
		object[] datas = GameSection.GetEventData() as object[];
		int id = (int)datas[0];
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
			bool flag = i >= data.points.Length - 1;
			DamageDistanceTable.DamagePoint damagePoint2 = null;
			if (!flag)
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(circlePrefab, startPoint, 5);
		Vector3 pos = GetPos(point);
		val.get_transform().set_localPosition(pos);
	}

	private void CreateBar(DamageDistanceTable.DamagePoint point, DamageDistanceTable.DamagePoint nextPoint)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 pos = GetPos(point);
		Vector3 pos2 = GetPos(nextPoint);
		float angle = GetAngle(pos, pos2);
		float num = Vector3.Distance(pos, pos2);
		Transform val = ResourceUtility.Realizes(barPrefab, startPoint, 5);
		val.get_transform().set_localPosition(pos);
		val.get_transform().set_localRotation(Quaternion.AngleAxis(angle, Vector3.get_forward()));
		UISprite component = val.GetComponent<UISprite>();
		component.width = (int)num;
	}

	private Vector3 GetPos(DamageDistanceTable.DamagePoint point)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)point.distance / 30f * 300f;
		float num2 = (float)point.rate / 1f * 150f;
		return new Vector3(num, num2, 0f);
	}

	private float GetAngle(Vector3 pos, Vector3 next)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = next - pos;
		Vector3 normalized = val.get_normalized();
		float num = Vector3.Angle(Vector3.get_right(), normalized);
		Vector3 val2 = Vector3.Cross(Vector3.get_right(), normalized);
		if (val2.z < 0f)
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
