using System.Collections.Generic;
using UnityEngine;

public class UIStatusGizmoBase : MonoBehaviour
{
	protected static List<UIStatusGizmoBase> uiList = new List<UIStatusGizmoBase>();

	protected static int listUpdateCount = 0;

	private UIPanel basePanel;

	protected float screenZ;

	public int depthOffset;

	protected new Transform transform;

	[HideInInspector]
	public bool isHostPlayer;

	public UIPanel BasePanel => basePanel ?? (basePanel = GetComponent<UIPanel>());

	public float ScreenZ => screenZ;

	protected virtual void SortAll()
	{
		uiList.Sort(delegate(UIStatusGizmoBase a, UIStatusGizmoBase b)
		{
			if (a.screenZ == b.screenZ)
			{
				return 0;
			}
			return (a.screenZ < b.screenZ) ? 1 : (-1);
		});
		int i = 0;
		for (int count = uiList.Count; i < count; i++)
		{
			UIStatusGizmoBase uIStatusGizmoBase = uiList[i];
			int num = i * 10;
			if (uIStatusGizmoBase.depthOffset != num)
			{
				AdjustDepth(uIStatusGizmoBase.gameObject, num - uIStatusGizmoBase.depthOffset);
				uIStatusGizmoBase.depthOffset = num;
			}
		}
	}

	protected static void AdjustDepth(GameObject set_object, int depth)
	{
		if (depth != 0 && !((Object)set_object == (Object)null))
		{
			set_object.GetComponentsInChildren(true, Temporary.uiWidgetList);
			int i = 0;
			for (int count = Temporary.uiWidgetList.Count; i < count; i++)
			{
				UIWidget uIWidget = Temporary.uiWidgetList[i];
				uIWidget.depth += depth;
			}
			Temporary.uiWidgetList.Clear();
		}
	}

	protected virtual void OnEnable()
	{
		uiList.Add(this);
		transform = base.gameObject.transform;
	}

	protected virtual void OnDisable()
	{
		uiList.Remove(this);
	}

	private void Update()
	{
		listUpdateCount = 0;
	}

	private void LateUpdate()
	{
		UpdateParam();
		listUpdateCount++;
		if (listUpdateCount >= uiList.Count)
		{
			SortAll();
		}
	}

	protected virtual void UpdateParam()
	{
	}

	protected void SetActiveSafe(GameObject target, bool active)
	{
		if (!((Object)target == (Object)null) && target.activeSelf != active)
		{
			target.SetActive(active);
		}
	}
}
