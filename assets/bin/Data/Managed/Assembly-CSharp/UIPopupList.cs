using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	public enum Position
	{
		Auto,
		Above,
		Below
	}

	public enum OpenOn
	{
		ClickOrTap,
		RightClick,
		DoubleClick,
		Manual
	}

	public delegate void LegacyEvent(string val);

	private const float animSpeed = 0.15f;

	public static UIPopupList current;

	private static GameObject mChild;

	private static float mFadeOutComplete;

	public UIAtlas atlas;

	public UIFont bitmapFont;

	public Font trueTypeFont;

	public int fontSize = 16;

	public FontStyle fontStyle;

	public string backgroundSprite;

	public string highlightSprite;

	public Position position;

	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	public List<string> items = new List<string>();

	public List<object> itemData = new List<object>();

	public Vector2 padding = new Vector3(4f, 4f);

	public Color textColor = Color.white;

	public Color backgroundColor = Color.white;

	public Color highlightColor = new Color(0.882352948f, 0.784313738f, 0.5882353f, 1f);

	public bool isAnimated = true;

	public bool isLocalized;

	public OpenOn openOn;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[SerializeField]
	[HideInInspector]
	private string mSelectedItem;

	[HideInInspector]
	[SerializeField]
	private UIPanel mPanel;

	[HideInInspector]
	[SerializeField]
	private UISprite mBackground;

	[HideInInspector]
	[SerializeField]
	private UISprite mHighlight;

	[HideInInspector]
	[SerializeField]
	private UILabel mHighlightedLabel;

	[HideInInspector]
	[SerializeField]
	private List<UILabel> mLabelList = new List<UILabel>();

	[SerializeField]
	[HideInInspector]
	private float mBgBorder;

	[NonSerialized]
	private GameObject mSelection;

	[NonSerialized]
	private int mOpenFrame;

	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	[SerializeField]
	[HideInInspector]
	private float textScale;

	[SerializeField]
	[HideInInspector]
	private UIFont font;

	[SerializeField]
	[HideInInspector]
	private UILabel textLabel;

	private LegacyEvent mLegacyEvent;

	[NonSerialized]
	private bool mExecuting;

	private bool mUseDynamicFont;

	private bool mTweening;

	public GameObject source;

	public UnityEngine.Object ambigiousFont
	{
		get
		{
			if ((UnityEngine.Object)trueTypeFont != (UnityEngine.Object)null)
			{
				return trueTypeFont;
			}
			if ((UnityEngine.Object)bitmapFont != (UnityEngine.Object)null)
			{
				return bitmapFont;
			}
			return font;
		}
		set
		{
			if (value is Font)
			{
				trueTypeFont = (value as Font);
				bitmapFont = null;
				font = null;
			}
			else if (value is UIFont)
			{
				bitmapFont = (value as UIFont);
				trueTypeFont = null;
				font = null;
			}
		}
	}

	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public LegacyEvent onSelectionChange
	{
		get
		{
			return mLegacyEvent;
		}
		set
		{
			mLegacyEvent = value;
		}
	}

	public static bool isOpen => (UnityEngine.Object)current != (UnityEngine.Object)null && ((UnityEngine.Object)mChild != (UnityEngine.Object)null || mFadeOutComplete > Time.unscaledTime);

	public string value
	{
		get
		{
			return mSelectedItem;
		}
		set
		{
			mSelectedItem = value;
			if (mSelectedItem != null && mSelectedItem != null)
			{
				TriggerCallbacks();
			}
		}
	}

	public object data
	{
		get
		{
			int num = items.IndexOf(mSelectedItem);
			return (num <= -1 || num >= itemData.Count) ? null : itemData[num];
		}
	}

	public bool isColliderEnabled
	{
		get
		{
			Collider component = GetComponent<Collider>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component.enabled;
			}
			Collider2D component2 = GetComponent<Collider2D>();
			return (UnityEngine.Object)component2 != (UnityEngine.Object)null && component2.enabled;
		}
	}

	[Obsolete("Use 'value' instead")]
	public string selection
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	private bool isValid => (UnityEngine.Object)bitmapFont != (UnityEngine.Object)null || (UnityEngine.Object)trueTypeFont != (UnityEngine.Object)null;

	private int activeFontSize => (!((UnityEngine.Object)trueTypeFont != (UnityEngine.Object)null) && !((UnityEngine.Object)bitmapFont == (UnityEngine.Object)null)) ? bitmapFont.defaultSize : fontSize;

	private float activeFontScale => (!((UnityEngine.Object)trueTypeFont != (UnityEngine.Object)null) && !((UnityEngine.Object)bitmapFont == (UnityEngine.Object)null)) ? ((float)fontSize / (float)bitmapFont.defaultSize) : 1f;

	public void Clear()
	{
		items.Clear();
		itemData.Clear();
	}

	public void AddItem(string text)
	{
		items.Add(text);
		itemData.Add(null);
	}

	public void AddItem(string text, object data)
	{
		items.Add(text);
		itemData.Add(data);
	}

	public void RemoveItem(string text)
	{
		int num = items.IndexOf(text);
		if (num != -1)
		{
			items.RemoveAt(num);
			itemData.RemoveAt(num);
		}
	}

	public void RemoveItemByData(object data)
	{
		int num = itemData.IndexOf(data);
		if (num != -1)
		{
			items.RemoveAt(num);
			itemData.RemoveAt(num);
		}
	}

	protected void TriggerCallbacks()
	{
		if (!mExecuting)
		{
			mExecuting = true;
			UIPopupList uIPopupList = current;
			current = this;
			if (mLegacyEvent != null)
			{
				mLegacyEvent(mSelectedItem);
			}
			if (EventDelegate.IsValid(onChange))
			{
				EventDelegate.Execute(onChange);
			}
			else if ((UnityEngine.Object)eventReceiver != (UnityEngine.Object)null && !string.IsNullOrEmpty(functionName))
			{
				eventReceiver.SendMessage(functionName, mSelectedItem, SendMessageOptions.DontRequireReceiver);
			}
			current = uIPopupList;
			mExecuting = false;
		}
	}

	private void OnEnable()
	{
		if (EventDelegate.IsValid(onChange))
		{
			eventReceiver = null;
			functionName = null;
		}
		if ((UnityEngine.Object)font != (UnityEngine.Object)null)
		{
			if (font.isDynamic)
			{
				trueTypeFont = font.dynamicFont;
				fontStyle = font.dynamicFontStyle;
				mUseDynamicFont = true;
			}
			else if ((UnityEngine.Object)bitmapFont == (UnityEngine.Object)null)
			{
				bitmapFont = font;
				mUseDynamicFont = false;
			}
			font = null;
		}
		if (textScale != 0f)
		{
			fontSize = ((!((UnityEngine.Object)bitmapFont != (UnityEngine.Object)null)) ? 16 : Mathf.RoundToInt((float)bitmapFont.defaultSize * textScale));
			textScale = 0f;
		}
		if ((UnityEngine.Object)trueTypeFont == (UnityEngine.Object)null && (UnityEngine.Object)bitmapFont != (UnityEngine.Object)null && bitmapFont.isDynamic)
		{
			trueTypeFont = bitmapFont.dynamicFont;
			bitmapFont = null;
		}
	}

	private void OnValidate()
	{
		Font x = trueTypeFont;
		UIFont uIFont = bitmapFont;
		bitmapFont = null;
		trueTypeFont = null;
		if ((UnityEngine.Object)x != (UnityEngine.Object)null && ((UnityEngine.Object)uIFont == (UnityEngine.Object)null || !mUseDynamicFont))
		{
			bitmapFont = null;
			trueTypeFont = x;
			mUseDynamicFont = true;
		}
		else if ((UnityEngine.Object)uIFont != (UnityEngine.Object)null)
		{
			if (uIFont.isDynamic)
			{
				trueTypeFont = uIFont.dynamicFont;
				fontStyle = uIFont.dynamicFontStyle;
				fontSize = uIFont.defaultSize;
				mUseDynamicFont = true;
			}
			else
			{
				bitmapFont = uIFont;
				mUseDynamicFont = false;
			}
		}
		else
		{
			trueTypeFont = x;
			mUseDynamicFont = true;
		}
	}

	private void Start()
	{
		if ((UnityEngine.Object)textLabel != (UnityEngine.Object)null)
		{
			EventDelegate.Add(onChange, textLabel.SetCurrentSelection);
			textLabel = null;
		}
		if (Application.isPlaying && string.IsNullOrEmpty(mSelectedItem) && items.Count > 0)
		{
			value = items[0];
		}
	}

	private void OnLocalize()
	{
		if (isLocalized)
		{
			TriggerCallbacks();
		}
	}

	private void Highlight(UILabel lbl, bool instant)
	{
		if ((UnityEngine.Object)mHighlight != (UnityEngine.Object)null)
		{
			mHighlightedLabel = lbl;
			UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
			if (atlasSprite != null)
			{
				Vector3 highlightPosition = GetHighlightPosition();
				if (!instant && isAnimated)
				{
					TweenPosition.Begin(mHighlight.gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
					if (!mTweening)
					{
						mTweening = true;
						StartCoroutine("UpdateTweenPosition");
					}
				}
				else
				{
					mHighlight.cachedTransform.localPosition = highlightPosition;
				}
			}
		}
	}

	private Vector3 GetHighlightPosition()
	{
		if ((UnityEngine.Object)mHighlightedLabel == (UnityEngine.Object)null || (UnityEngine.Object)mHighlight == (UnityEngine.Object)null)
		{
			return Vector3.zero;
		}
		UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return Vector3.zero;
		}
		float pixelSize = atlas.pixelSize;
		float num = (float)atlasSprite.borderLeft * pixelSize;
		float y = (float)atlasSprite.borderTop * pixelSize;
		return mHighlightedLabel.cachedTransform.localPosition + new Vector3(0f - num, y, 1f);
	}

	private IEnumerator UpdateTweenPosition()
	{
		if ((UnityEngine.Object)mHighlight != (UnityEngine.Object)null && (UnityEngine.Object)mHighlightedLabel != (UnityEngine.Object)null)
		{
			TweenPosition tp = mHighlight.GetComponent<TweenPosition>();
			while ((UnityEngine.Object)tp != (UnityEngine.Object)null && tp.enabled)
			{
				tp.to = GetHighlightPosition();
				yield return (object)null;
			}
		}
		mTweening = false;
	}

	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			Highlight(component, false);
		}
	}

	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			Select(go.GetComponent<UILabel>(), true);
			UIEventListener component = go.GetComponent<UIEventListener>();
			value = (component.parameter as string);
			UIPlaySound[] components = GetComponents<UIPlaySound>();
			int i = 0;
			for (int num = components.Length; i < num; i++)
			{
				UIPlaySound uIPlaySound = components[i];
				if (uIPlaySound.trigger == UIPlaySound.Trigger.OnClick)
				{
					NGUITools.PlaySound(uIPlaySound.audioClip, uIPlaySound.volume, 1f);
				}
			}
			CloseSelf();
		}
	}

	private void Select(UILabel lbl, bool instant)
	{
		Highlight(lbl, instant);
	}

	private void OnNavigate(KeyCode key)
	{
		if (base.enabled && (UnityEngine.Object)current == (UnityEngine.Object)this)
		{
			int num = mLabelList.IndexOf(mHighlightedLabel);
			if (num == -1)
			{
				num = 0;
			}
			switch (key)
			{
			case KeyCode.UpArrow:
				if (num > 0)
				{
					Select(mLabelList[--num], false);
				}
				break;
			case KeyCode.DownArrow:
				if (num + 1 < mLabelList.Count)
				{
					Select(mLabelList[++num], false);
				}
				break;
			}
		}
	}

	private void OnKey(KeyCode key)
	{
		if (base.enabled && (UnityEngine.Object)current == (UnityEngine.Object)this && (key == UICamera.current.cancelKey0 || key == UICamera.current.cancelKey1))
		{
			OnSelect(false);
		}
	}

	private void OnDisable()
	{
		CloseSelf();
	}

	private void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			CloseSelf();
		}
	}

	public static void Close()
	{
		if ((UnityEngine.Object)current != (UnityEngine.Object)null)
		{
			current.CloseSelf();
			current = null;
		}
	}

	public void CloseSelf()
	{
		if ((UnityEngine.Object)mChild != (UnityEngine.Object)null && (UnityEngine.Object)current == (UnityEngine.Object)this)
		{
			StopCoroutine("CloseIfUnselected");
			mSelection = null;
			mLabelList.Clear();
			if (isAnimated)
			{
				UIWidget[] componentsInChildren = mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					UIWidget uIWidget = componentsInChildren[i];
					Color color = uIWidget.color;
					color.a = 0f;
					TweenColor.Begin(uIWidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
				}
				Collider[] componentsInChildren2 = mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				for (int num2 = componentsInChildren2.Length; j < num2; j++)
				{
					componentsInChildren2[j].enabled = false;
				}
				UnityEngine.Object.Destroy(mChild, 0.15f);
				mFadeOutComplete = Time.unscaledTime + Mathf.Max(0.1f, 0.15f);
			}
			else
			{
				UnityEngine.Object.Destroy(mChild);
				mFadeOutComplete = Time.unscaledTime + 0.1f;
			}
			mBackground = null;
			mHighlight = null;
			mChild = null;
			current = null;
		}
	}

	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)activeFontSize * activeFontScale + mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		AnimateColor(widget);
		AnimatePosition(widget, placeAbove, bottom);
	}

	private void OnClick()
	{
		if (mOpenFrame != Time.frameCount)
		{
			if ((UnityEngine.Object)mChild == (UnityEngine.Object)null)
			{
				if (openOn != OpenOn.DoubleClick && openOn != OpenOn.Manual && (openOn != OpenOn.RightClick || UICamera.currentTouchID == -2))
				{
					Show();
				}
			}
			else if ((UnityEngine.Object)mHighlightedLabel != (UnityEngine.Object)null)
			{
				OnItemPress(mHighlightedLabel.gameObject, true);
			}
		}
	}

	private void OnDoubleClick()
	{
		if (openOn == OpenOn.DoubleClick)
		{
			Show();
		}
	}

	private IEnumerator CloseIfUnselected()
	{
		do
		{
			yield return (object)null;
		}
		while (!((UnityEngine.Object)UICamera.selectedObject != (UnityEngine.Object)mSelection));
		CloseSelf();
	}

	public void Show()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && (UnityEngine.Object)mChild == (UnityEngine.Object)null && (UnityEngine.Object)atlas != (UnityEngine.Object)null && isValid && items.Count > 0)
		{
			mLabelList.Clear();
			StopCoroutine("CloseIfUnselected");
			UICamera.selectedObject = (UICamera.hoveredObject ?? base.gameObject);
			mSelection = UICamera.selectedObject;
			source = UICamera.selectedObject;
			if ((UnityEngine.Object)source == (UnityEngine.Object)null)
			{
				Debug.LogError("Popup list needs a source object...");
			}
			else
			{
				mOpenFrame = Time.frameCount;
				if ((UnityEngine.Object)mPanel == (UnityEngine.Object)null)
				{
					mPanel = UIPanel.Find(base.transform);
					if ((UnityEngine.Object)mPanel == (UnityEngine.Object)null)
					{
						return;
					}
				}
				mChild = new GameObject("Drop-down List");
				mChild.layer = base.gameObject.layer;
				current = this;
				Transform transform = mChild.transform;
				transform.parent = mPanel.cachedTransform;
				Vector3 localPosition;
				Vector3 vector;
				Vector3 v;
				if (openOn == OpenOn.Manual && (UnityEngine.Object)mSelection != (UnityEngine.Object)base.gameObject)
				{
					localPosition = UICamera.lastEventPosition;
					vector = mPanel.cachedTransform.InverseTransformPoint(mPanel.anchorCamera.ScreenToWorldPoint(localPosition));
					v = vector;
					transform.localPosition = vector;
					localPosition = transform.position;
				}
				else
				{
					Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(mPanel.cachedTransform, base.transform, false, false);
					vector = bounds.min;
					v = bounds.max;
					transform.localPosition = vector;
					localPosition = transform.position;
				}
				StartCoroutine("CloseIfUnselected");
				transform.localRotation = Quaternion.identity;
				transform.localScale = Vector3.one;
				mBackground = NGUITools.AddSprite(mChild, atlas, backgroundSprite);
				mBackground.pivot = UIWidget.Pivot.TopLeft;
				mBackground.depth = NGUITools.CalculateNextDepth(mPanel.gameObject);
				mBackground.color = backgroundColor;
				Vector4 border = mBackground.border;
				mBgBorder = border.y;
				mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
				mHighlight = NGUITools.AddSprite(mChild, atlas, highlightSprite);
				mHighlight.pivot = UIWidget.Pivot.TopLeft;
				mHighlight.color = highlightColor;
				UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
				if (atlasSprite != null)
				{
					float num = (float)atlasSprite.borderTop;
					float num2 = (float)activeFontSize;
					float activeFontScale = this.activeFontScale;
					float num3 = num2 * activeFontScale;
					float num4 = 0f;
					float num5 = 0f - padding.y;
					List<UILabel> list = new List<UILabel>();
					if (!items.Contains(mSelectedItem))
					{
						mSelectedItem = null;
					}
					int i = 0;
					for (int count = items.Count; i < count; i++)
					{
						string text = items[i];
						UILabel uILabel = NGUITools.AddWidget<UILabel>(mChild);
						uILabel.name = i.ToString();
						uILabel.pivot = UIWidget.Pivot.TopLeft;
						uILabel.bitmapFont = bitmapFont;
						uILabel.trueTypeFont = trueTypeFont;
						uILabel.fontSize = fontSize;
						uILabel.fontStyle = fontStyle;
						uILabel.text = ((!isLocalized) ? text : Localization.Get(text));
						uILabel.color = textColor;
						Transform cachedTransform = uILabel.cachedTransform;
						float num6 = border.x + padding.x;
						Vector2 pivotOffset = uILabel.pivotOffset;
						cachedTransform.localPosition = new Vector3(num6 - pivotOffset.x, num5, -1f);
						uILabel.overflowMethod = UILabel.Overflow.ResizeFreely;
						uILabel.alignment = alignment;
						list.Add(uILabel);
						num5 -= num3;
						num5 -= padding.y;
						float a = num4;
						Vector2 printedSize = uILabel.printedSize;
						num4 = Mathf.Max(a, printedSize.x);
						UIEventListener uIEventListener = UIEventListener.Get(uILabel.gameObject);
						uIEventListener.onHover = OnItemHover;
						uIEventListener.onPress = OnItemPress;
						uIEventListener.parameter = text;
						if (mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(mSelectedItem)))
						{
							Highlight(uILabel, true);
						}
						mLabelList.Add(uILabel);
					}
					num4 = Mathf.Max(num4, v.x - vector.x - (border.x + padding.x) * 2f);
					float num7 = num4;
					Vector3 vector2 = new Vector3(num7 * 0.5f, (0f - num3) * 0.5f, 0f);
					Vector3 vector3 = new Vector3(num7, num3 + padding.y, 1f);
					int j = 0;
					for (int count2 = list.Count; j < count2; j++)
					{
						UILabel uILabel2 = list[j];
						NGUITools.AddWidgetCollider(uILabel2.gameObject);
						uILabel2.autoResizeBoxCollider = false;
						BoxCollider component = uILabel2.GetComponent<BoxCollider>();
						if ((UnityEngine.Object)component != (UnityEngine.Object)null)
						{
							Vector3 center = component.center;
							vector2.z = center.z;
							component.center = vector2;
							component.size = vector3;
						}
						else
						{
							BoxCollider2D component2 = uILabel2.GetComponent<BoxCollider2D>();
							component2.offset = vector2;
							component2.size = vector3;
						}
					}
					int width = Mathf.RoundToInt(num4);
					num4 += (border.x + padding.x) * 2f;
					num5 -= border.y;
					mBackground.width = Mathf.RoundToInt(num4);
					mBackground.height = Mathf.RoundToInt(0f - num5 + border.y);
					int k = 0;
					for (int count3 = list.Count; k < count3; k++)
					{
						UILabel uILabel3 = list[k];
						uILabel3.overflowMethod = UILabel.Overflow.ShrinkContent;
						uILabel3.width = width;
					}
					float num8 = 2f * atlas.pixelSize;
					float f = num4 - (border.x + padding.x) * 2f + (float)atlasSprite.borderLeft * num8;
					float f2 = num3 + num * num8;
					mHighlight.width = Mathf.RoundToInt(f);
					mHighlight.height = Mathf.RoundToInt(f2);
					bool flag = position == Position.Above;
					if (position == Position.Auto)
					{
						UICamera uICamera = UICamera.FindCameraForLayer(mSelection.layer);
						if ((UnityEngine.Object)uICamera != (UnityEngine.Object)null)
						{
							Vector3 vector4 = uICamera.cachedCamera.WorldToViewportPoint(localPosition);
							flag = (vector4.y < 0.5f);
						}
					}
					if (isAnimated)
					{
						AnimateColor(mBackground);
						if (Time.timeScale == 0f || Time.timeScale >= 0.1f)
						{
							float bottom = num5 + num3;
							Animate(mHighlight, flag, bottom);
							int l = 0;
							for (int count4 = list.Count; l < count4; l++)
							{
								Animate(list[l], flag, bottom);
							}
							AnimateScale(mBackground, flag, bottom);
						}
					}
					if (flag)
					{
						vector.y = v.y - border.y;
						v.y = vector.y + (float)mBackground.height;
						v.x = vector.x + (float)mBackground.width;
						transform.localPosition = new Vector3(vector.x, v.y - border.y, vector.z);
					}
					else
					{
						v.y = vector.y + border.y;
						vector.y = v.y - (float)mBackground.height;
						v.x = vector.x + (float)mBackground.width;
					}
					Transform parent = mPanel.cachedTransform.parent;
					if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
					{
						vector = mPanel.cachedTransform.TransformPoint(vector);
						v = mPanel.cachedTransform.TransformPoint(v);
						vector = parent.InverseTransformPoint(vector);
						v = parent.InverseTransformPoint(v);
					}
					Vector3 b = mPanel.CalculateConstrainOffset(vector, v);
					localPosition = transform.localPosition + b;
					localPosition.x = Mathf.Round(localPosition.x);
					localPosition.y = Mathf.Round(localPosition.y);
					transform.localPosition = localPosition;
				}
			}
		}
		else
		{
			OnSelect(false);
		}
	}
}
