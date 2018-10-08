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
		SetEnableInfo(false);
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
		if (!((Object)lbl == (Object)null))
		{
			SetEnableInfo(is_touch);
			if (is_touch)
			{
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
				Vector3 size = component.size;
				int num = (int)(size.y * 0.5f) + uIWidget.height / 2;
				float num2 = (float)num;
				Vector3 localScale = componentInParent2.transform.localScale;
				float num3 = num2 * localScale.y;
				Vector3 position = component.transform.position;
				float y = num3 + position.y;
				Vector3 position2 = transform2.position;
				vector = new Vector3(position2.x, y);
				if ((Object)componentInParent != (Object)null)
				{
					UIPanel component2 = componentInParent.GetComponent<UIPanel>();
					Vector3 a = vector;
					float num4 = (float)(uIWidget.height / 2);
					Vector3 localScale2 = componentInParent2.transform.localScale;
					Vector3 worldPos = a + new Vector3(0f, num4 * localScale2.y);
					if (!component2.IsVisible(worldPos))
					{
						Vector3 position3 = componentInParent.transform.position;
						float y2 = position3.y;
						Vector2 viewSize = component2.GetViewSize();
						float num5 = (viewSize.y - (float)uIWidget.height) / 2f;
						Vector2 clipOffset = component2.clipOffset;
						float num6 = num5 + clipOffset.y;
						Vector3 localScale3 = componentInParent2.transform.localScale;
						vector.y = y2 + num6 * localScale3.y;
					}
				}
				Vector3 position4 = component.transform.position;
				float num7 = position4.x;
				float num8 = num7;
				Vector3 localScale4 = componentInParent2.transform.localScale;
				float num9 = num8 * (1f / localScale4.x);
				float num10 = UIVirtualScreen.screenWidth * 0.5f;
				int num11 = uIWidget.width / 2;
				float num12 = num9 - (float)num11;
				float num13 = num9 + (float)num11;
				if (0f - num10 > num12)
				{
					float num14 = 0f - num10 + (float)num11;
					Vector3 localScale5 = componentInParent2.transform.localScale;
					num7 = num14 * localScale5.x;
				}
				else if (num10 < num13)
				{
					float num15 = num10 - (float)num11;
					Vector3 localScale6 = componentInParent2.transform.localScale;
					num7 = num15 * localScale6.x;
				}
				vector = new Vector3(num7, vector.y);
				transform.transform.position = vector;
				isEnableParentScroll = ((Object)componentInParent != (Object)null);
				if ((Object)componentInParent != (Object)null)
				{
					transform.parent = (parent_scroll ?? componentInParent.transform.parent);
					transform.localScale = Vector3.one;
				}
				transform.gameObject.SetActive(false);
				transform.gameObject.SetActive(true);
			}
		}
	}

	private void adjustPosY(Transform button)
	{
		Vector3 vector = Vector3.zero;
		UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
		BoxCollider component = button.GetComponent<BoxCollider>();
		UIWidget component2 = GetComponent<UIWidget>();
		Transform transform = base.transform;
		UIRoot componentInParent2 = base.gameObject.GetComponentInParent<UIRoot>();
		Vector3 size = component.size;
		int num = (int)(size.y * 0.5f) + component2.height / 2;
		float num2 = (float)num;
		Vector3 localScale = componentInParent2.transform.localScale;
		float num3 = num2 * localScale.y;
		Vector3 position = component.transform.position;
		float y = num3 + position.y;
		Vector3 position2 = transform.position;
		vector = new Vector3(position2.x, y);
		if ((Object)componentInParent != (Object)null)
		{
			UIPanel component3 = componentInParent.GetComponent<UIPanel>();
			Vector3 a = vector;
			float num4 = (float)(component2.height / 2);
			Vector3 localScale2 = componentInParent2.transform.localScale;
			Vector3 worldPos = a + new Vector3(0f, num4 * localScale2.y);
			if (!component3.IsVisible(worldPos))
			{
				Vector3 position3 = componentInParent.transform.position;
				float y2 = position3.y;
				Vector2 viewSize = component3.GetViewSize();
				float num5 = viewSize.y / 2f;
				Vector2 clipOffset = component3.clipOffset;
				float num6 = num5 + clipOffset.y;
				Vector3 localScale3 = componentInParent2.transform.localScale;
				vector.y = y2 + num6 * localScale3.y;
			}
		}
		transform.position = vector;
	}

	public void UpdatePosision(Transform button)
	{
		if (isEnableParentScroll)
		{
			UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
			if ((Object)componentInParent != (Object)null && componentInParent.isDragging)
			{
				isEnableParentScroll = false;
				SetEnableInfo(false);
			}
		}
	}
}
