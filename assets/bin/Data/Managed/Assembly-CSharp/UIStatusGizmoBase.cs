using System.Collections.Generic;
using UnityEngine;

public class UIStatusGizmoBase
{
	protected static List<UIStatusGizmoBase> uiList = new List<UIStatusGizmoBase>();

	protected static int listUpdateCount = 0;

	private UIPanel basePanel;

	protected float screenZ;

	public int depthOffset;

	protected Transform transform;

	[HideInInspector]
	public bool isHostPlayer;

	public UIPanel BasePanel => basePanel ?? (basePanel = this.GetComponent<UIPanel>());

	public float ScreenZ => screenZ;

	public UIStatusGizmoBase()
		: this()
	{
	}

	protected virtual void SortAll()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
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
				AdjustDepth(uIStatusGizmoBase.get_gameObject(), num - uIStatusGizmoBase.depthOffset);
				uIStatusGizmoBase.depthOffset = num;
			}
		}
	}

	protected static void AdjustDepth(GameObject set_object, int depth)
	{
		if (depth != 0 && !(set_object == null))
		{
			set_object.GetComponentsInChildren<UIWidget>(true, Temporary.uiWidgetList);
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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		uiList.Add(this);
		transform = this.get_gameObject().get_transform();
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
		if (!(target == null) && target.get_activeSelf() != active)
		{
			target.SetActive(active);
		}
	}
}
