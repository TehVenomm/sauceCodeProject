using System;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollablePopupList : UILabel
{
	public enum ATTACH_DIRECTION
	{
		BOTTOM,
		LEFT,
		RIGHT
	}

	private static bool firstItemIsTextOnly;

	public GameObject objRoot;

	public UIScrollView scroll;

	public UIGrid grid;

	public TweenHeight tw;

	public UISprite selectFrameSprite;

	public int itemHeight;

	public UIWidget expandTarget_A;

	public UIWidget expandTarget_B;

	public int minPopFrameWidth;

	public int maxItemNum = 10;

	private Action<int> closePopupCallback;

	private UITweenCtrl twCtrl;

	private TweenAlpha twAlpha;

	private Transform gridAncor;

	private EventDelegate del;

	private bool isFinished;

	private bool isUpdateTween;

	private int selectIndex;

	private string[] textItem;

	private bool[] buttonEnable;

	public static void CreatePopup(Transform popup_transform, Transform parent_ctrl, int max_num, ATTACH_DIRECTION direction, bool adjust_size, string[] texts, bool[] button_enable, int select_index, Action<int> callback = null)
	{
		_CreatePopup(popup_transform, parent_ctrl, max_num, direction, adjust_size, null, texts, button_enable, select_index, callback);
	}

	public static void CreatePopupItem(Transform popup_transform, Transform parent_ctrl, int max_num, ATTACH_DIRECTION direction, bool adjust_size, Transform item_prefab, string[] texts, bool[] button_enable, int select_index, Action<int> callback = null)
	{
		firstItemIsTextOnly = true;
		_CreatePopup(popup_transform, parent_ctrl, max_num, direction, adjust_size, item_prefab, texts, button_enable, select_index, callback);
		if (item_prefab != null)
		{
			Object.DestroyImmediate(item_prefab.get_gameObject());
		}
	}

	private static void _CreatePopup(Transform popup_transform, Transform parent_ctrl, int max_num, ATTACH_DIRECTION direction, bool adjust_size, Transform item_prefab, string[] texts, bool[] button_enable, int select_index, Action<int> callback = null)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (popup_transform == null || parent_ctrl == null)
		{
			return;
		}
		UIWidget component = parent_ctrl.GetComponent<UIWidget>();
		if (!(component == null))
		{
			float num = 0f;
			float num2 = 0f;
			switch (direction)
			{
			case ATTACH_DIRECTION.BOTTOM:
				num2 = -component.height / 2;
				break;
			case ATTACH_DIRECTION.LEFT:
				num = -component.width;
				num2 = component.height / 2;
				break;
			case ATTACH_DIRECTION.RIGHT:
				num = component.width;
				num2 = component.height / 2;
				break;
			}
			popup_transform.set_parent(parent_ctrl);
			popup_transform.set_localPosition(new Vector3(num, num2, 0f));
			popup_transform.set_localScale(Vector3.get_one());
			UIScrollablePopupList component2 = popup_transform.GetComponent<UIScrollablePopupList>();
			if (adjust_size)
			{
				component2.minPopFrameWidth = component.width;
			}
			component2.maxItemNum = max_num;
			component2.SetItem(item_prefab, texts, button_enable, select_index, callback);
		}
	}

	protected override void Awake()
	{
		isFinished = false;
		isUpdateTween = false;
		selectIndex = -1;
		twCtrl = this.GetComponentInChildren<UITweenCtrl>();
		twAlpha = this.GetComponent<TweenAlpha>();
		del = new EventDelegate(delegate
		{
			CloseCallback();
		});
		base.Awake();
	}

	private new void Start()
	{
		gridAncor = grid.GetComponent<UIWidget>().leftAnchor.target;
		base.Start();
	}

	private void LateUpdate()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		if (isUpdateTween && scroll != null && grid != null && tw != null && !isFinished)
		{
			if (tw.tweenFactor < 1f)
			{
				float tweenFactor = tw.tweenFactor;
				grid.cellHeight = (float)itemHeight * tweenFactor;
			}
			else
			{
				grid.cellHeight = itemHeight;
				isFinished = true;
				isUpdateTween = false;
			}
			scroll.ResetPosition();
			scroll.MoveRelative(new Vector3(0f, grid.cellHeight * (float)selectIndex));
			grid.Reposition();
			if (isFinished)
			{
				((UIRect)grid.GetComponent<UIWidget>()).SetAnchor(null);
			}
		}
	}

	private void StartTween()
	{
		isFinished = false;
		isUpdateTween = true;
		grid.GetComponent<UIWidget>().SetAnchor(gridAncor);
		twCtrl.Reset();
		twCtrl.Play(forward: true, delegate
		{
		});
	}

	private void ClosePopupCallBack()
	{
		if (closePopupCallback != null)
		{
			closePopupCallback(selectIndex);
		}
	}

	public void SetItem(Transform item_prefab, string[] texts, bool[] button_enable, int select_index, Action<int> close_callback)
	{
		closePopupCallback = close_callback;
		if (isFinished || isUpdateTween)
		{
			ClosePopupCallBack();
			return;
		}
		selectIndex = select_index;
		SetItemText(item_prefab, texts, button_enable);
	}

	public void SetItemText(Transform item_prefab, string[] texts, bool[] button_enable)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		if (isFinished || isUpdateTween)
		{
			ClosePopupCallBack();
			return;
		}
		textItem = texts;
		buttonEnable = button_enable;
		int num = SetGridItem(item_prefab);
		int num2 = (int)(((float)Mathf.Min(textItem.Length, maxItemNum) + 0.5f) * (float)itemHeight);
		Vector4 baseClipRegion = scroll.panel.baseClipRegion;
		scroll.panel.SetRect(baseClipRegion.x, baseClipRegion.y, num, num2);
		Vector3 localPosition = scroll.get_transform().get_localPosition();
		localPosition.y = 0f - (float)num2 * 0.5f;
		scroll.get_transform().set_localPosition(localPosition);
		Vector2 clipOffset = scroll.panel.clipOffset;
		clipOffset.y = 0f;
		scroll.panel.clipOffset = clipOffset;
		selectFrameSprite.width = num - 10;
		selectFrameSprite.height = itemHeight;
		selectFrameSprite.get_transform().set_localScale(Vector3.get_one());
		tw.to = num2;
		UIWidget component = tw.GetComponent<UIWidget>();
		component.width = num;
		expandTarget_A.height = num2;
		expandTarget_B.height = num2;
		Vector2 val = Vector2.op_Implicit(expandTarget_A.get_transform().get_localPosition());
		val.x = (float)num * 0.5f;
		val.y = (float)(-num2) * 0.5f;
		expandTarget_A.get_transform().set_localPosition(Vector2.op_Implicit(val));
		expandTarget_B.get_transform().set_localPosition(Vector2.op_Implicit(val));
		twAlpha.RemoveOnFinished(del);
		objRoot.SetActive(true);
		scroll.ResetPosition();
		Transform gridChild = GetGridChild(selectIndex);
		ClickItem(selectIndex, gridChild);
		StartTween();
	}

	private int SetGridItem(Transform item_prefab)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		int num = minPopFrameWidth;
		if (textItem != null && textItem.Length > 0)
		{
			DeleteGridChildren();
			UIWidget[] array = new UIWidget[textItem.Length];
			int i = 0;
			for (int num2 = textItem.Length; i < num2; i++)
			{
				GameObject val = null;
				if (item_prefab == null || firstItemIsTextOnly)
				{
					firstItemIsTextOnly = false;
					val = new GameObject();
					val.set_layer(5);
					val.set_name(i.ToString());
					num = CreateItem(val, i, num);
				}
				else
				{
					val = ResourceUtility.Instantiate<GameObject>(item_prefab.get_gameObject());
					val.set_layer(5);
					val.set_name(i.ToString());
					num = CreatePrefabItem(val, i, num);
				}
				UIWidget uIWidget = array[i] = val.GetComponent<UIWidget>();
				val.AddComponent<BoxCollider>();
				val.AddComponent<UIDragScrollView>();
				val.AddComponent<UIGameSceneEventSender>();
				UIButton btn = val.AddComponent<UIButton>();
				btn.hover = uIWidget.color;
				btn.pressed = uIWidget.color;
				btn.onClick.Add(new EventDelegate(delegate
				{
					int result = -1;
					if (int.TryParse(btn.get_name(), out result))
					{
						selectIndex = result;
						if (result >= 0)
						{
							ClickItem(selectIndex, btn.get_transform());
							CloseCallback();
						}
					}
				}));
				btn.set_enabled(buttonEnable[i]);
				UIButtonScale component = val.GetComponent<UIButtonScale>();
				if (component == null)
				{
					component = val.get_gameObject().AddComponent<UIButtonScale>();
					component.tweenTarget = val.get_transform();
					component.hover = new Vector3(1f, 1f, 1f);
					component.pressed = new Vector3(1.3f, 1.3f, 1.3f);
					component.duration = 0.05f;
				}
				grid.AddChild(val.get_transform());
				val.get_transform().set_localPosition(Vector3.get_zero());
				val.get_transform().set_localEulerAngles(Vector3.get_zero());
				val.get_transform().set_localScale(Vector3.get_one());
			}
			int j = 0;
			for (int num3 = textItem.Length; j < num3; j++)
			{
				BoxCollider component2 = array[j].get_transform().GetComponent<BoxCollider>();
				UIWidget uIWidget2 = array[j];
				component2.set_size(new Vector3((float)num, (float)itemHeight, 1f));
				uIWidget2.width = num;
				uIWidget2.height = itemHeight;
				uIWidget2.autoResizeBoxCollider = true;
				uIWidget2.ResizeCollider();
			}
			grid.Reposition();
		}
		return num;
	}

	public int CreateItem(GameObject go, int index, int base_max_width)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		UILabel component = this.GetComponent<UILabel>();
		UIWidget component2 = this.GetComponent<UIWidget>();
		UILabel uILabel = go.AddComponent<UILabel>();
		uILabel.pivot = component2.pivot;
		uILabel.bitmapFont = component.bitmapFont;
		uILabel.trueTypeFont = component.trueTypeFont;
		uILabel.fontSize = component.fontSize;
		uILabel.fontStyle = component.fontStyle;
		uILabel.text = textItem[index];
		uILabel.color = ((!buttonEnable[index]) ? Color.get_gray() : component.color);
		uILabel.alpha = 1f;
		uILabel.alignment = component.alignment;
		uILabel.cachedTransform.set_localPosition(component.cachedTransform.get_localPosition());
		uILabel.AssumeNaturalSize();
		int result = Mathf.Max(base_max_width, uILabel.width);
		uILabel.overflowMethod = Overflow.ShrinkContent;
		return result;
	}

	public int CreatePrefabItem(GameObject go, int index, int base_max_width)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		UILabel component = this.GetComponent<UILabel>();
		UISprite componentInChildren = go.GetComponentInChildren<UISprite>();
		UILabel componentInChildren2 = componentInChildren.GetComponentInChildren<UILabel>();
		componentInChildren2.text = textItem[index];
		componentInChildren2.color = ((!buttonEnable[index]) ? Color.get_gray() : component.color);
		componentInChildren2.alpha = 1f;
		componentInChildren2.cachedTransform.set_localPosition(component.cachedTransform.get_localPosition());
		return Mathf.Max(base_max_width, componentInChildren2.width + componentInChildren.width);
	}

	public void ClosePopup()
	{
		twAlpha.RemoveOnFinished(del);
		twAlpha.onFinished.Add(del);
		twAlpha.PlayReverse();
	}

	private void CloseCallback()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		scroll.ResetPosition();
		Vector2 clipOffset = scroll.panel.clipOffset;
		clipOffset.y = 0f;
		scroll.panel.clipOffset = clipOffset;
		scroll.get_transform().set_localPosition(Vector3.get_zero());
		twCtrl.Reset();
		twAlpha.RemoveOnFinished(del);
		DeleteGridChildren();
		objRoot.SetActive(false);
		isFinished = false;
		isUpdateTween = false;
		ClosePopupCallBack();
	}

	private void DeleteGridChildren()
	{
		DetachSelectFrame();
		List<Transform> childList = grid.GetChildList();
		childList.ForEach(delegate(Transform t)
		{
			if (grid.RemoveChild(t))
			{
				t.get_transform().set_parent(null);
				Object.Destroy(t.get_gameObject());
			}
		});
		grid.Reposition();
	}

	private void ClickItem(int index, Transform parent)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (selectFrameSprite != null)
		{
			Transform transform = selectFrameSprite.get_transform();
			if (index >= 0)
			{
				transform.set_parent(parent);
				transform.set_localPosition(Vector3.get_zero());
				selectFrameSprite.get_gameObject().SetActive(true);
			}
			else
			{
				selectFrameSprite.get_gameObject().SetActive(false);
				transform.set_parent(this.get_transform());
				transform.set_localPosition(Vector3.get_zero());
			}
		}
	}

	private void DetachSelectFrame()
	{
		ClickItem(-1, null);
	}

	private Transform GetGridChild(int index)
	{
		if (grid == null)
		{
			return null;
		}
		return grid.get_transform().Find(index.ToString());
	}
}
