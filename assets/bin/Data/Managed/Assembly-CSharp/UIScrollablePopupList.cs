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
		if ((UnityEngine.Object)item_prefab != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(item_prefab.gameObject);
		}
	}

	private static void _CreatePopup(Transform popup_transform, Transform parent_ctrl, int max_num, ATTACH_DIRECTION direction, bool adjust_size, Transform item_prefab, string[] texts, bool[] button_enable, int select_index, Action<int> callback = null)
	{
		if (!((UnityEngine.Object)popup_transform == (UnityEngine.Object)null) && !((UnityEngine.Object)parent_ctrl == (UnityEngine.Object)null))
		{
			UIWidget component = parent_ctrl.GetComponent<UIWidget>();
			if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
			{
				float x = 0f;
				float y = 0f;
				switch (direction)
				{
				case ATTACH_DIRECTION.BOTTOM:
					y = (float)(-component.height / 2);
					break;
				case ATTACH_DIRECTION.LEFT:
					x = (float)(-component.width);
					y = (float)(component.height / 2);
					break;
				case ATTACH_DIRECTION.RIGHT:
					x = (float)component.width;
					y = (float)(component.height / 2);
					break;
				}
				popup_transform.parent = parent_ctrl;
				popup_transform.localPosition = new Vector3(x, y, 0f);
				popup_transform.localScale = Vector3.one;
				UIScrollablePopupList component2 = popup_transform.GetComponent<UIScrollablePopupList>();
				if (adjust_size)
				{
					component2.minPopFrameWidth = component.width;
				}
				component2.maxItemNum = max_num;
				component2.SetItem(item_prefab, texts, button_enable, select_index, callback);
			}
		}
	}

	protected override void Awake()
	{
		isFinished = false;
		isUpdateTween = false;
		selectIndex = -1;
		twCtrl = GetComponentInChildren<UITweenCtrl>();
		twAlpha = GetComponent<TweenAlpha>();
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
		if (isUpdateTween && (UnityEngine.Object)scroll != (UnityEngine.Object)null && (UnityEngine.Object)grid != (UnityEngine.Object)null && (UnityEngine.Object)tw != (UnityEngine.Object)null && !isFinished)
		{
			if (tw.tweenFactor < 1f)
			{
				float tweenFactor = tw.tweenFactor;
				grid.cellHeight = (float)itemHeight * tweenFactor;
			}
			else
			{
				grid.cellHeight = (float)itemHeight;
				isFinished = true;
				isUpdateTween = false;
			}
			scroll.ResetPosition();
			scroll.MoveRelative(new Vector3(0f, grid.cellHeight * (float)selectIndex));
			grid.Reposition();
			if (isFinished)
			{
				grid.GetComponent<UIWidget>().SetAnchor((Transform)null);
			}
		}
	}

	private void StartTween()
	{
		isFinished = false;
		isUpdateTween = true;
		grid.GetComponent<UIWidget>().SetAnchor(gridAncor);
		twCtrl.Reset();
		twCtrl.Play(true, delegate
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
		}
		else
		{
			selectIndex = select_index;
			SetItemText(item_prefab, texts, button_enable);
		}
	}

	public void SetItemText(Transform item_prefab, string[] texts, bool[] button_enable)
	{
		if (isFinished || isUpdateTween)
		{
			ClosePopupCallBack();
		}
		else
		{
			textItem = texts;
			buttonEnable = button_enable;
			int num = SetGridItem(item_prefab);
			int num2 = (int)(((float)Mathf.Min(textItem.Length, maxItemNum) + 0.5f) * (float)itemHeight);
			Vector4 baseClipRegion = scroll.panel.baseClipRegion;
			scroll.panel.SetRect(baseClipRegion.x, baseClipRegion.y, (float)num, (float)num2);
			Vector3 localPosition = scroll.transform.localPosition;
			localPosition.y = 0f - (float)num2 * 0.5f;
			scroll.transform.localPosition = localPosition;
			Vector2 clipOffset = scroll.panel.clipOffset;
			clipOffset.y = 0f;
			scroll.panel.clipOffset = clipOffset;
			selectFrameSprite.width = num - 10;
			selectFrameSprite.height = itemHeight;
			selectFrameSprite.transform.localScale = Vector3.one;
			tw.to = num2;
			UIWidget component = tw.GetComponent<UIWidget>();
			component.width = num;
			expandTarget_A.height = num2;
			expandTarget_B.height = num2;
			Vector2 v = expandTarget_A.transform.localPosition;
			v.x = (float)num * 0.5f;
			v.y = (float)(-num2) * 0.5f;
			expandTarget_A.transform.localPosition = v;
			expandTarget_B.transform.localPosition = v;
			twAlpha.RemoveOnFinished(del);
			objRoot.SetActive(true);
			scroll.ResetPosition();
			Transform gridChild = GetGridChild(selectIndex);
			ClickItem(selectIndex, gridChild);
			StartTween();
		}
	}

	private int SetGridItem(Transform item_prefab)
	{
		int num = minPopFrameWidth;
		if (textItem != null && textItem.Length > 0)
		{
			DeleteGridChildren();
			UIWidget[] array = new UIWidget[textItem.Length];
			int i = 0;
			for (int num2 = textItem.Length; i < num2; i++)
			{
				GameObject gameObject = null;
				if ((UnityEngine.Object)item_prefab == (UnityEngine.Object)null || firstItemIsTextOnly)
				{
					firstItemIsTextOnly = false;
					gameObject = new GameObject();
					gameObject.layer = 5;
					gameObject.name = i.ToString();
					num = CreateItem(gameObject, i, num);
				}
				else
				{
					gameObject = ResourceUtility.Instantiate(item_prefab.gameObject);
					gameObject.layer = 5;
					gameObject.name = i.ToString();
					num = CreatePrefabItem(gameObject, i, num);
				}
				UIWidget uIWidget = array[i] = gameObject.GetComponent<UIWidget>();
				gameObject.AddComponent<BoxCollider>();
				gameObject.AddComponent<UIDragScrollView>();
				gameObject.AddComponent<UIGameSceneEventSender>();
				UIButton btn = gameObject.AddComponent<UIButton>();
				btn.hover = uIWidget.color;
				btn.pressed = uIWidget.color;
				btn.onClick.Add(new EventDelegate(delegate
				{
					int result = -1;
					if (int.TryParse(btn.name, out result))
					{
						selectIndex = result;
						if (result >= 0)
						{
							ClickItem(selectIndex, btn.transform);
							CloseCallback();
						}
					}
				}));
				btn.enabled = buttonEnable[i];
				UIButtonScale component = gameObject.GetComponent<UIButtonScale>();
				if ((UnityEngine.Object)component == (UnityEngine.Object)null)
				{
					component = gameObject.gameObject.AddComponent<UIButtonScale>();
					component.tweenTarget = gameObject.transform;
					component.hover = new Vector3(1f, 1f, 1f);
					component.pressed = new Vector3(1.3f, 1.3f, 1.3f);
					component.duration = 0.05f;
				}
				grid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
			}
			int j = 0;
			for (int num3 = textItem.Length; j < num3; j++)
			{
				BoxCollider component2 = array[j].transform.GetComponent<BoxCollider>();
				UIWidget uIWidget2 = array[j];
				component2.size = new Vector3((float)num, (float)itemHeight, 1f);
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
		UILabel component = GetComponent<UILabel>();
		UIWidget component2 = GetComponent<UIWidget>();
		UILabel uILabel = go.AddComponent<UILabel>();
		uILabel.pivot = component2.pivot;
		uILabel.bitmapFont = component.bitmapFont;
		uILabel.trueTypeFont = component.trueTypeFont;
		uILabel.fontSize = component.fontSize;
		uILabel.fontStyle = component.fontStyle;
		uILabel.text = textItem[index];
		uILabel.color = ((!buttonEnable[index]) ? Color.gray : component.color);
		uILabel.alpha = 1f;
		uILabel.alignment = component.alignment;
		uILabel.cachedTransform.localPosition = component.cachedTransform.localPosition;
		uILabel.AssumeNaturalSize();
		int result = Mathf.Max(base_max_width, uILabel.width);
		uILabel.overflowMethod = Overflow.ShrinkContent;
		return result;
	}

	public int CreatePrefabItem(GameObject go, int index, int base_max_width)
	{
		UILabel component = GetComponent<UILabel>();
		UISprite componentInChildren = go.GetComponentInChildren<UISprite>();
		UILabel componentInChildren2 = componentInChildren.GetComponentInChildren<UILabel>();
		componentInChildren2.text = textItem[index];
		componentInChildren2.color = ((!buttonEnable[index]) ? Color.gray : component.color);
		componentInChildren2.alpha = 1f;
		componentInChildren2.cachedTransform.localPosition = component.cachedTransform.localPosition;
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
		scroll.ResetPosition();
		Vector2 clipOffset = scroll.panel.clipOffset;
		clipOffset.y = 0f;
		scroll.panel.clipOffset = clipOffset;
		scroll.transform.localPosition = Vector3.zero;
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
				t.transform.parent = null;
				UnityEngine.Object.Destroy(t.gameObject);
			}
		});
		grid.Reposition();
	}

	private void ClickItem(int index, Transform parent)
	{
		if ((UnityEngine.Object)selectFrameSprite != (UnityEngine.Object)null)
		{
			Transform transform = selectFrameSprite.transform;
			if (index >= 0)
			{
				transform.parent = parent;
				transform.localPosition = Vector3.zero;
				selectFrameSprite.gameObject.SetActive(true);
			}
			else
			{
				selectFrameSprite.gameObject.SetActive(false);
				transform.parent = base.transform;
				transform.localPosition = Vector3.zero;
			}
		}
	}

	private void DetachSelectFrame()
	{
		ClickItem(-1, null);
	}

	private Transform GetGridChild(int index)
	{
		if ((UnityEngine.Object)grid == (UnityEngine.Object)null)
		{
			return null;
		}
		return grid.transform.FindChild(index.ToString());
	}
}
