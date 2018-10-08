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
		if (!MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.AddComponent<DropTargetMarkerManeger>();
		}
	}

	protected override void Awake()
	{
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
		if (!((Object)drowMesh != (Object)null))
		{
			drowMesh = new Mesh();
			drowMesh.vertices = new Vector3[4]
			{
				new Vector3(0.03f, 0.03f, 0f),
				new Vector3(-0.03f, -0.03f, 0f),
				new Vector3(-0.03f, 0.03f, 0f),
				new Vector3(0.03f, -0.03f, 0f)
			};
			drowMesh.uv = new Vector2[4]
			{
				new Vector2(1f, 1f),
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 0f)
			};
			drowMesh.triangles = new int[6]
			{
				0,
				1,
				2,
				3,
				1,
				0
			};
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
				if ((Object)targetList[i].target == (Object)enemy._transform)
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
		if (!((Object)portal == (Object)null))
		{
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if ((Object)targetList[i].target == (Object)portal._transform)
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
			if (!((Object)targetList[num].target != (Object)target))
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
		if (active)
		{
			int i = 0;
			for (int count = targetList.Count; i < count; i++)
			{
				if ((Object)targetList[i].target == (Object)null)
				{
					delList.Add(targetList[i]);
				}
				else if (!targetList[i].target.gameObject.activeSelf)
				{
					targetList[i].active = false;
				}
				else
				{
					if ((Object)targetList[i].targetEnemy != (Object)null)
					{
						if (targetList[i].targetEnemy.isHiding)
						{
							targetList[i].active = false;
							continue;
						}
						if ((Object)targetList[i].targetEnemy.uiEnemyStatusGizmo != (Object)null)
						{
							targetList[i].targetEnemy.uiEnemyStatusGizmo.SetTargetIcon(drowMaterial.mainTexture);
							targetList[i].active = false;
							continue;
						}
					}
					Vector3 vector = Camera.main.WorldToViewportPoint(targetList[i].target.position);
					if (vector.x < -0f || vector.x > 1f || vector.y < -0f || vector.y > 1f)
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
						targetList[i].pos = targetList[i].target.position + targetList[i].offset + param.offset;
						TargetInfo targetInfo = targetList[i];
						Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles;
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
		target.GetComponentsInChildren(Temporary.colliderList);
		float num = 0f;
		int i = 0;
		for (int count = Temporary.colliderList.Count; i < count; i++)
		{
			Collider collider = Temporary.colliderList[i];
			Vector3 center = collider.bounds.center;
			float y = center.y;
			float num2 = y;
			Vector3 size = collider.bounds.size;
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
		if (active && !((Object)drowMaterial == (Object)null))
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
