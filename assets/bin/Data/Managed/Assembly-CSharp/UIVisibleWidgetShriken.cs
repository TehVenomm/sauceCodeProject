using UnityEngine;

public class UIVisibleWidgetShriken : MonoBehaviour
{
	private UIPanel panel;

	private UIWidget widget;

	private string sectionName;

	private Transform effect;

	private Transform[] children;

	public static void Set(UIPanel panel, UIWidget widget)
	{
		Set(panel, widget, null);
	}

	public static void Set(UIPanel panel, UIWidget widget, string current_section_name)
	{
		if (widget == null)
		{
			return;
		}
		UIVisibleWidgetShriken uIVisibleWidgetShriken = widget.GetComponent<UIVisibleWidgetShriken>();
		if (uIVisibleWidgetShriken == null)
		{
			uIVisibleWidgetShriken = widget.gameObject.AddComponent<UIVisibleWidgetShriken>();
		}
		uIVisibleWidgetShriken.panel = panel;
		uIVisibleWidgetShriken.widget = widget;
		if (string.IsNullOrEmpty(uIVisibleWidgetShriken.sectionName))
		{
			uIVisibleWidgetShriken.sectionName = (current_section_name ?? MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
		}
		if (uIVisibleWidgetShriken.transform.childCount > 0)
		{
			uIVisibleWidgetShriken.children = new Transform[uIVisibleWidgetShriken.transform.childCount];
			for (int i = 0; i < uIVisibleWidgetShriken.transform.childCount; i++)
			{
				uIVisibleWidgetShriken.children[i] = uIVisibleWidgetShriken.transform.GetChild(i);
			}
		}
	}

	public static void Remove(UIWidget widget)
	{
		if (!(widget == null))
		{
			UIVisibleWidgetShriken component = widget.GetComponent<UIVisibleWidgetShriken>();
			if (component != null)
			{
				Object.Destroy(component);
			}
		}
	}

	private void LateUpdate()
	{
		if (panel != null && children != null)
		{
			bool active = IsVisibleCompletely(panel, widget);
			for (int i = 0; i < children.Length; i++)
			{
				children[i].gameObject.SetActive(active);
			}
		}
	}

	public bool IsVisibleCompletely(UIPanel p, UIWidget w)
	{
		if (p == null || w == null)
		{
			return true;
		}
		for (int i = 0; i < 4; i++)
		{
			if (!p.IsVisible(widget.worldCorners[i]))
			{
				return false;
			}
		}
		return true;
	}

	private void OnDisable()
	{
	}
}
