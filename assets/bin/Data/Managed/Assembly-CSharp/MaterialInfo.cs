using UnityEngine;

public class MaterialInfo : MonoBehaviour
{
	public UILabel lbl;

	public UIWidget widget;

	public string nowSectionName;

	private bool isEnableParentScroll;

	public void Initialize(string section_name)
	{
		nowSectionName = section_name;
		SetEnableInfo(is_enable: false);
	}

	public void SetText(string text)
	{
		lbl.text = text;
	}

	public void SetEnableInfo(bool is_enable)
	{
		lbl.gameObject.SetActive(is_enable);
		widget.gameObject.SetActive(is_enable);
	}

	public void Send(bool is_touch, Transform button, string item_name, Transform parent_scroll)
	{
		if (lbl == null)
		{
			return;
		}
		SetEnableInfo(is_touch);
		if (!is_touch)
		{
			return;
		}
		SetText(item_name);
		Transform transform = base.transform;
		transform.parent = button.parent;
		transform.localScale = Vector3.one;
		UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
		BoxCollider component = button.GetComponent<BoxCollider>();
		UIWidget uIWidget = widget;
		Transform transform2 = base.transform;
		Vector3 vector = transform2.position;
		UIRoot componentInParent2 = base.gameObject.GetComponentInParent<UIRoot>();
		float y = (float)((int)(component.size.y * 0.5f) + uIWidget.height / 2) * componentInParent2.transform.localScale.y + component.transform.position.y;
		vector = new Vector3(transform2.position.x, y);
		if (componentInParent != null)
		{
			UIPanel component2 = componentInParent.GetComponent<UIPanel>();
			Vector3 worldPos = vector + new Vector3(0f, (float)(uIWidget.height / 2) * componentInParent2.transform.localScale.y);
			if (!component2.IsVisible(worldPos))
			{
				vector.y = componentInParent.transform.position.y + ((component2.GetViewSize().y - (float)uIWidget.height) / 2f + component2.clipOffset.y) * componentInParent2.transform.localScale.y;
			}
		}
		float num = component.transform.position.x;
		float num2 = num * (1f / componentInParent2.transform.localScale.x);
		float num3 = UIVirtualScreen.screenWidth * 0.5f;
		int num4 = uIWidget.width / 2;
		float num5 = num2 - (float)num4;
		float num6 = num2 + (float)num4;
		if (0f - num3 > num5)
		{
			num = (0f - num3 + (float)num4) * componentInParent2.transform.localScale.x;
		}
		else if (num3 < num6)
		{
			num = (num3 - (float)num4) * componentInParent2.transform.localScale.x;
		}
		vector = new Vector3(num, vector.y);
		transform.transform.position = vector;
		isEnableParentScroll = (componentInParent != null);
		if (componentInParent != null)
		{
			transform.parent = (parent_scroll ?? componentInParent.transform.parent);
			transform.localScale = Vector3.one;
		}
		transform.gameObject.SetActive(value: false);
		transform.gameObject.SetActive(value: true);
	}

	private void adjustPosY(Transform button)
	{
		Vector3 vector = Vector3.zero;
		UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
		BoxCollider component = button.GetComponent<BoxCollider>();
		UIWidget component2 = GetComponent<UIWidget>();
		Transform transform = base.transform;
		UIRoot componentInParent2 = base.gameObject.GetComponentInParent<UIRoot>();
		float y = (float)((int)(component.size.y * 0.5f) + component2.height / 2) * componentInParent2.transform.localScale.y + component.transform.position.y;
		vector = new Vector3(transform.position.x, y);
		if (componentInParent != null)
		{
			UIPanel component3 = componentInParent.GetComponent<UIPanel>();
			Vector3 worldPos = vector + new Vector3(0f, (float)(component2.height / 2) * componentInParent2.transform.localScale.y);
			if (!component3.IsVisible(worldPos))
			{
				vector.y = componentInParent.transform.position.y + (component3.GetViewSize().y / 2f + component3.clipOffset.y) * componentInParent2.transform.localScale.y;
			}
		}
		transform.position = vector;
	}

	public void UpdatePosision(Transform button)
	{
		if (isEnableParentScroll)
		{
			UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
			if (componentInParent != null && componentInParent.isDragging)
			{
				isEnableParentScroll = false;
				SetEnableInfo(is_enable: false);
			}
		}
	}
}
