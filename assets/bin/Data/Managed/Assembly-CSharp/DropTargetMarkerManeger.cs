using Network;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetMarkerManeger : MonoBehaviourSingleton<DropTargetMarkerManeger>
{
	public class TargetInfo
	{
		public Enemy targetEnemy;

		public Transform target;

		public Vector3 offset = new Vector3(0f, 0f, 0f);

		public Vector3 pos;

		public Quaternion rot;

		public bool initOffset;

		public bool active;
	}

	private InGameSettingsManager.DropMaker param;

	private Mesh drowMesh;

	private Material drowMaterial;

	private Quaternion defRot;

	private List<uint> targetIDList = new List<uint>();

	private List<uint> targetPortalIDList = new List<uint>();

	private List<TargetInfo> targetList = new List<TargetInfo>();

	private List<TargetInfo> targetStockList = new List<TargetInfo>();

	private List<TargetInfo> delList = new List<TargetInfo>();

	public bool active
	{
		get;
		set;
	}

	public static void Create()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().AddComponent<DropTargetMarkerManeger>();
		}
	}

	protected override void Awake()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		active = true;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			param = MonoBehaviourSingleton<InGameSettingsManager>.I.dropMaker;
			drowMesh = param.mesh;
			drowMaterial = param.material;
			defRot = Quaternion.Euler(param.rotOffset);
		}
		UpdateList();
		if (!(drowMesh != null))
		{
			drowMesh = new Mesh();
			drowMesh.set_vertices((Vector3[])new Vector3[4]
			{
				new Vector3(0.03f, 0.03f, 0f),
				new Vector3(-0.03f, -0.03f, 0f),
				new Vector3(-0.03f, 0.03f, 0f),
				new Vector3(0.03f, -0.03f, 0f)
			});
			drowMesh.set_uv((Vector2[])new Vector2[4]
			{
				new Vector2(1f, 1f),
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 0f)
			});
			drowMesh.set_triangles(new int[6]
			{
				0,
				1,
				2,
				3,
				1,
				0
			});
			drowMesh.RecalculateNormals();
			drowMesh.RecalculateBounds();
		}
	}

	public void UpdateList()
	{
		int i = 0;
		for (int count = targetList.Count; i < count; i++)
		{
			targetStockList.Add(targetList[i]);
		}
		targetList.Clear();
		targetIDList.Clear();
		targetPortalIDList.Clear();
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
		int mapId = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
		int j = 0;
		for (int num = deliveryList.Length; j < num; j++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryList[j].dId);
			if (deliveryTableData != null)
			{
				uint num2 = 0u;
				for (uint num3 = (uint)deliveryTableData.needs.Length; num2 < num3; num2++)
				{
					uint mapID = deliveryTableData.GetMapID(num2);
					if (mapID == 0 || mapID == mapId)
					{
						uint enemyID = deliveryTableData.GetEnemyID(num2);
						if (!targetIDList.Contains(enemyID) && (mapID != 0 || enemyID != 0))
						{
							int have = 0;
							int need = 0;
							MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryList[j].dId, out have, out need, num2);
							if (have < need)
							{
								targetIDList.Add(enemyID);
							}
						}
					}
				}
				uint num4 = 0u;
				for (uint num5 = (uint)deliveryTableData.targetPortalID.Length; num4 < num5; num4++)
				{
					uint item = (uint)deliveryTableData.targetPortalID[num4];
					if (!targetPortalIDList.Contains(item) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryList[j].dId))
					{
						targetPortalIDList.Add(item);
					}
				}
			}
		}
		List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
		int count2 = enemyList.Count;
		for (int k = 0; k < count2; k++)
		{
			CheckTarget(enemyList[k] as Enemy);
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.portalObjectList != null)
		{
			int l = 0;
			for (int count3 = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; l < count3; l++)
			{
				CheckTarget(MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[l]);
			}
		}
	}

	private void _AddTargetInfo(Enemy enemy)
	{
		TargetInfo targetInfo = null;
		if (targetStockList.Count > 0)
		{
			targetInfo = targetStockList[0];
			targetStockList.RemoveAt(0);
		}
		if (targetInfo == null)
		{
			targetInfo = new TargetInfo();
		}
		targetInfo.targetEnemy = enemy;
		targetInfo.target = enemy._transform;
		targetInfo.offset.y = enemy.uiHeight;
		if (enemy.enemyTableData != null)
		{
			targetInfo.offset.y *= enemy.enemyTableData.modelScale;
		}
		targetInfo.initOffset = true;
		targetList.Add(targetInfo);
	}

	private void _AddTargetInfo(PortalObject portal)
	{
		TargetInfo targetInfo = null;
		if (targetStockList.Count > 0)
		{
			targetInfo = targetStockList[0];
			targetStockList.RemoveAt(0);
		}
		if (targetInfo == null)
		{
			targetInfo = new TargetInfo();
		}
		targetInfo.targetEnemy = null;
		targetInfo.target = portal._transform;
		targetInfo.offset.y = param.portalHeight;
		targetInfo.initOffset = true;
		targetList.Add(targetInfo);
	}

	public void CheckTarget(Enemy enemy)
	{
		if (enemy.isInitialized)
		{
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if (targetList[i].target == enemy._transform)
				{
					return;
				}
			}
			bool flag = targetIDList.Contains(0u);
			int num = 0;
			int count2 = targetIDList.Count;
			while (true)
			{
				if (num >= count2)
				{
					return;
				}
				if (targetIDList[num] == enemy.enemyID || flag)
				{
					break;
				}
				num++;
			}
			_AddTargetInfo(enemy);
		}
	}

	public void CheckTarget(PortalObject portal)
	{
		if (!(portal == null))
		{
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if (targetList[i].target == portal._transform)
				{
					return;
				}
			}
			int num = 0;
			int count2 = targetPortalIDList.Count;
			while (true)
			{
				if (num >= count2)
				{
					return;
				}
				if (targetPortalIDList[num] == portal.portalID)
				{
					break;
				}
				num++;
			}
			_AddTargetInfo(portal);
		}
	}

	public void RemoveTarget(Transform target)
	{
		int num = 0;
		int count = targetList.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if (!(targetList[num].target != target))
			{
				break;
			}
			num++;
		}
		targetList[num].targetEnemy = null;
		targetStockList.Add(targetList[num]);
		targetList.Remove(targetList[num]);
	}

	private void Update()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		if (active)
		{
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if (targetList[i].target == null)
				{
					delList.Add(targetList[i]);
				}
				else if (!targetList[i].target.get_gameObject().get_activeSelf())
				{
					targetList[i].active = false;
				}
				else
				{
					if (targetList[i].targetEnemy != null)
					{
						if (targetList[i].targetEnemy.isHiding)
						{
							targetList[i].active = false;
							continue;
						}
						if (targetList[i].targetEnemy.uiEnemyStatusGizmo != null)
						{
							targetList[i].targetEnemy.uiEnemyStatusGizmo.SetTargetIcon(drowMaterial.get_mainTexture());
							targetList[i].active = false;
							continue;
						}
					}
					Vector3 val = Camera.get_main().WorldToViewportPoint(targetList[i].target.get_position());
					if (val.x < -0f || val.x > 1f || val.y < -0f || val.y > 1f)
					{
						targetList[i].active = false;
					}
					else
					{
						if (!targetList[i].initOffset)
						{
							targetList[i].offset.y = CalcHight(targetList[i].target);
							if (targetList[i].offset.y != 0f)
							{
								targetList[i].initOffset = true;
							}
						}
						targetList[i].pos = targetList[i].target.get_position() + targetList[i].offset + param.offset;
						TargetInfo targetInfo = targetList[i];
						Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_eulerAngles();
						targetInfo.rot = Quaternion.Euler(0f - eulerAngles.x, 0f, 0f) * defRot;
						targetList[i].active = true;
					}
				}
			}
			int j = 0;
			for (int count2 = delList.Count; j < count2; j++)
			{
				targetStockList.Add(delList[j]);
				targetList.Remove(delList[j]);
			}
			delList.Clear();
		}
	}

	private float CalcHight(Transform target)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		target.GetComponentsInChildren<Collider>(Temporary.colliderList);
		float num = 0f;
		int i = 0;
		for (int count = Temporary.colliderList.Count; i < count; i++)
		{
			Collider val = Temporary.colliderList[i];
			Bounds bounds = val.get_bounds();
			Vector3 center = bounds.get_center();
			float y = center.y;
			float num2 = y;
			Bounds bounds2 = val.get_bounds();
			Vector3 size = bounds2.get_size();
			y = num2 + size.y;
			if (num < y)
			{
				num = y;
			}
		}
		Temporary.colliderList.Clear();
		return num;
	}

	public void OnPostRender()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (active && !(drowMaterial == null))
		{
			bool flag = false;
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if (targetList[i].active)
				{
					if (!flag)
					{
						drowMaterial.SetPass(0);
						flag = true;
					}
					Graphics.DrawMeshNow(drowMesh, targetList[i].pos, targetList[i].rot);
				}
			}
		}
	}
}
