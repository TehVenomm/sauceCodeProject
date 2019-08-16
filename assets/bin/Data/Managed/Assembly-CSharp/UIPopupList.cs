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

	public static UIPopupList current;

	private static GameObject mChild;

	private static float mFadeOutComplete;

	private const float animSpeed = 0.15f;

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

	public Vector2 padding = Vector2.op_Implicit(new Vector3(4f, 4f));

	public Color textColor = Color.get_white();

	public Color backgroundColor = Color.get_white();

	public Color highlightColor = new Color(0.882352948f, 40f / 51f, 0.5882353f, 1f);

	public bool isAnimated = true;

	public bool isLocalized;

	public OpenOn openOn;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	[HideInInspector]
	[SerializeField]
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

	[HideInInspector]
	[SerializeField]
	private float mBgBorder;

	[NonSerialized]
	private GameObject mSelection;

	[NonSerialized]
	private int mOpenFrame;

	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	[HideInInspector]
	[SerializeField]
	private float textScale;

	[HideInInspector]
	[SerializeField]
	private UIFont font;

	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	private LegacyEvent mLegacyEvent;

	[NonSerialized]
	private bool mExecuting;

	private bool mUseDynamicFont;

	private bool mTweening;

	public GameObject source;

	public Object ambigiousFont
	{
		get
		{
			if (trueTypeFont != null)
			{
				return trueTypeFont;
			}
			if (bitmapFont != null)
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

	public static bool isOpen => current != null && (mChild != null || mFadeOutComplete > Time.get_unscaledTime());

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
			Collider component = this.GetComponent<Collider>();
			if (component != null)
			{
				return component.get_enabled();
			}
			Collider2D component2 = this.GetComponent<Collider2D>();
			return component2 != null && component2.get_enabled();
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

	private bool isValid => bitmapFont != null || trueTypeFont != null;

	private int activeFontSize => (!(trueTypeFont != null) && !(bitmapFont == null)) ? bitmapFont.defaultSize : fontSize;

	private float activeFontScale => (!(trueTypeFont != null) && !(bitmapFont == null)) ? ((float)fontSize / (float)bitmapFont.defaultSize) : 1f;

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
			else if (eventReceiver != null && !string.IsNullOrEmpty(functionName))
			{
				eventReceiver.SendMessage(functionName, (object)mSelectedItem, 1);
			}
			current = uIPopupList;
			mExecuting = false;
		}
	}

	private void OnEnable()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (EventDelegate.IsValid(onChange))
		{
			eventReceiver = null;
			functionName = null;
		}
		if (font != null)
		{
			if (font.isDynamic)
			{
				trueTypeFont = font.dynamicFont;
				fontStyle = font.dynamicFontStyle;
				mUseDynamicFont = true;
			}
			else if (bitmapFont == null)
			{
				bitmapFont = font;
				mUseDynamicFont = false;
			}
			font = null;
		}
		if (textScale != 0f)
		{
			fontSize = ((!(bitmapFont != null)) ? 16 : Mathf.RoundToInt((float)bitmapFont.defaultSize * textScale));
			textScale = 0f;
		}
		if (trueTypeFont == null && bitmapFont != null && bitmapFont.isDynamic)
		{
			trueTypeFont = bitmapFont.dynamicFont;
			bitmapFont = null;
		}
	}

	private void OnValidate()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Font val = trueTypeFont;
		UIFont uIFont = bitmapFont;
		bitmapFont = null;
		trueTypeFont = null;
		if (val != null && (uIFont == null || !mUseDynamicFont))
		{
			bitmapFont = null;
			trueTypeFont = val;
			mUseDynamicFont = true;
		}
		else if (uIFont != null)
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
			trueTypeFont = val;
			mUseDynamicFont = true;
		}
	}

	private void Start()
	{
		if (textLabel != null)
		{
			EventDelegate.Add(onChange, textLabel.SetCurrentSelection);
			textLabel = null;
		}
		if (Application.get_isPlaying() && string.IsNullOrEmpty(mSelectedItem) && items.Count > 0)
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
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (!(mHighlight != null))
		{
			return;
		}
		mHighlightedLabel = lbl;
		UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Vector3 highlightPosition = GetHighlightPosition();
		if (!instant && isAnimated)
		{
			TweenPosition.Begin(mHighlight.get_gameObject(), 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
			if (!mTweening)
			{
				mTweening = true;
				this.StartCoroutine("UpdateTweenPosition");
			}
		}
		else
		{
			mHighlight.cachedTransform.set_localPosition(highlightPosition);
		}
	}

	private Vector3 GetHighlightPosition()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (mHighlightedLabel == null || mHighlight == null)
		{
			return Vector3.get_zero();
		}
		UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return Vector3.get_zero();
		}
		float pixelSize = atlas.pixelSize;
		float num = (float)atlasSprite.borderLeft * pixelSize;
		float num2 = (float)atlasSprite.borderTop * pixelSize;
		return mHighlightedLabel.cachedTransform.get_localPosition() + new Vector3(0f - num, num2, 1f);
	}

	private IEnumerator UpdateTweenPosition()
	{
		if (mHighlight != null && mHighlightedLabel != null)
		{
			TweenPosition tp = mHighlight.GetComponent<TweenPosition>();
			while (tp != null && tp.get_enabled())
			{
				tp.to = GetHighlightPosition();
				yield return null;
			}
		}
		mTweening = false;
	}

	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			Highlight(component, instant: false);
		}
	}

	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (!isPressed)
		{
			return;
		}
		Select(go.GetComponent<UILabel>(), instant: true);
		UIEventListener component = go.GetComponent<UIEventListener>();
		value = (component.parameter as string);
		UIPlaySound[] components = this.GetComponents<UIPlaySound>();
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

	private void Select(UILabel lbl, bool instant)
	{
		Highlight(lbl, instant);
	}

	private void OnNavigate(KeyCode key)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Invalid comparison between Unknown and I4
		if (!this.get_enabled() || !(current == this))
		{
			return;
		}
		int num = mLabelList.IndexOf(mHighlightedLabel);
		if (num == -1)
		{
			num = 0;
		}
		if ((int)key == 273)
		{
			if (num > 0)
			{
				Select(mLabelList[--num], instant: false);
			}
		}
		else if ((int)key == 274 && num + 1 < mLabelList.Count)
		{
			Select(mLabelList[++num], instant: false);
		}
	}

	private void OnKey(KeyCode key)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && current == this && (key == UICamera.current.cancelKey0 || key == UICamera.current.cancelKey1))
		{
			OnSelect(isSelected: false);
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
		if (current != null)
		{
			current.CloseSelf();
			current = null;
		}
	}

	public void CloseSelf()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (!(mChild != null) || !(current == this))
		{
			return;
		}
		this.StopCoroutine("CloseIfUnselected");
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
				TweenColor.Begin(uIWidget.get_gameObject(), 0.15f, color).method = UITweener.Method.EaseOut;
			}
			Collider[] componentsInChildren2 = mChild.GetComponentsInChildren<Collider>();
			int j = 0;
			for (int num2 = componentsInChildren2.Length; j < num2; j++)
			{
				componentsInChildren2[j].set_enabled(false);
			}
			Object.Destroy(mChild, 0.15f);
			mFadeOutComplete = Time.get_unscaledTime() + Mathf.Max(0.1f, 0.15f);
		}
		else
		{
			Object.Destroy(mChild);
			mFadeOutComplete = Time.get_unscaledTime() + 0.1f;
		}
		mBackground = null;
		mHighlight = null;
		mChild = null;
		current = null;
	}

	private void AnimateColor(UIWidget widget)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.get_gameObject(), 0.15f, color).method = UITweener.Method.EaseOut;
	}

	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = widget.cachedTransform.get_localPosition();
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.set_localPosition(localPosition2);
		GameObject gameObject = widget.get_gameObject();
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = widget.get_gameObject();
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)activeFontSize * activeFontScale + mBgBorder * 2f;
		cachedTransform.set_localScale(new Vector3(1f, num / (float)widget.height, 1f));
		TweenScale.Begin(gameObject, 0.15f, Vector3.get_one()).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.get_localPosition();
			cachedTransform.set_localPosition(new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z));
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
		if (mOpenFrame == Time.get_frameCount())
		{
			return;
		}
		if (mChild == null)
		{
			if (openOn != OpenOn.DoubleClick && openOn != OpenOn.Manual && (openOn != OpenOn.RightClick || UICamera.currentTouchID == -2))
			{
				Show();
			}
		}
		else if (mHighlightedLabel != null)
		{
			OnItemPress(mHighlightedLabel.get_gameObject(), isPressed: true);
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
			yield return null;
		}
		while (!(UICamera.selectedObject != mSelection));
		CloseSelf();
	}

	public void Show()
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && mChild == null && atlas != null && isValid && items.Count > 0)
		{
			mLabelList.Clear();
			this.StopCoroutine("CloseIfUnselected");
			UICamera.selectedObject = (UICamera.hoveredObject ?? this.get_gameObject());
			mSelection = UICamera.selectedObject;
			source = UICamera.selectedObject;
			if (source == null)
			{
				Debug.LogError((object)"Popup list needs a source object...");
				return;
			}
			mOpenFrame = Time.get_frameCount();
			if (mPanel == null)
			{
				mPanel = UIPanel.Find(this.get_transform());
				if (mPanel == null)
				{
					return;
				}
			}
			mChild = new GameObject("Drop-down List");
			mChild.set_layer(this.get_gameObject().get_layer());
			current = this;
			Transform transform = mChild.get_transform();
			transform.set_parent(mPanel.cachedTransform);
			Vector3 val;
			Vector3 val2;
			Vector3 val3;
			if (openOn == OpenOn.Manual && mSelection != this.get_gameObject())
			{
				val = Vector2.op_Implicit(UICamera.lastEventPosition);
				val2 = mPanel.cachedTransform.InverseTransformPoint(mPanel.anchorCamera.ScreenToWorldPoint(val));
				val3 = val2;
				transform.set_localPosition(val2);
				val = transform.get_position();
			}
			else
			{
				Bounds val4 = NGUIMath.CalculateRelativeWidgetBounds(mPanel.cachedTransform, this.get_transform(), considerInactive: false, considerChildren: false);
				val2 = val4.get_min();
				val3 = val4.get_max();
				transform.set_localPosition(val2);
				val = transform.get_position();
			}
			this.StartCoroutine("CloseIfUnselected");
			transform.set_localRotation(Quaternion.get_identity());
			transform.set_localScale(Vector3.get_one());
			mBackground = NGUITools.AddSprite(mChild, atlas, backgroundSprite);
			mBackground.pivot = UIWidget.Pivot.TopLeft;
			mBackground.depth = NGUITools.CalculateNextDepth(mPanel.get_gameObject());
			mBackground.color = backgroundColor;
			Vector4 border = mBackground.border;
			mBgBorder = border.y;
			mBackground.cachedTransform.set_localPosition(new Vector3(0f, border.y, 0f));
			mHighlight = NGUITools.AddSprite(mChild, atlas, highlightSprite);
			mHighlight.pivot = UIWidget.Pivot.TopLeft;
			mHighlight.color = highlightColor;
			UISpriteData atlasSprite = mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = atlasSprite.borderTop;
			float num2 = activeFontSize;
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
				uILabel.set_name(i.ToString());
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
				cachedTransform.set_localPosition(new Vector3(num6 - pivotOffset.x, num5, -1f));
				uILabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uILabel.alignment = alignment;
				list.Add(uILabel);
				num5 -= num3;
				num5 -= padding.y;
				float num7 = num4;
				Vector2 printedSize = uILabel.printedSize;
				num4 = Mathf.Max(num7, printedSize.x);
				UIEventListener uIEventListener = UIEventListener.Get(uILabel.get_gameObject());
				uIEventListener.onHover = OnItemHover;
				uIEventListener.onPress = OnItemPress;
				uIEventListener.parameter = text;
				if (mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(mSelectedItem)))
				{
					Highlight(uILabel, instant: true);
				}
				mLabelList.Add(uILabel);
			}
			num4 = Mathf.Max(num4, val3.x - val2.x - (border.x + padding.x) * 2f);
			float num8 = num4;
			Vector3 val5 = default(Vector3);
			val5._002Ector(num8 * 0.5f, (0f - num3) * 0.5f, 0f);
			Vector3 val6 = default(Vector3);
			val6._002Ector(num8, num3 + padding.y, 1f);
			int j = 0;
			for (int count2 = list.Count; j < count2; j++)
			{
				UILabel uILabel2 = list[j];
				NGUITools.AddWidgetCollider(uILabel2.get_gameObject());
				uILabel2.autoResizeBoxCollider = false;
				BoxCollider component = uILabel2.GetComponent<BoxCollider>();
				if (component != null)
				{
					Vector3 center = component.get_center();
					val5.z = center.z;
					component.set_center(val5);
					component.set_size(val6);
				}
				else
				{
					BoxCollider2D component2 = uILabel2.GetComponent<BoxCollider2D>();
					component2.set_offset(Vector2.op_Implicit(val5));
					component2.set_size(Vector2.op_Implicit(val6));
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
			float num9 = 2f * atlas.pixelSize;
			float num10 = num4 - (border.x + padding.x) * 2f + (float)atlasSprite.borderLeft * num9;
			float num11 = num3 + num * num9;
			mHighlight.width = Mathf.RoundToInt(num10);
			mHighlight.height = Mathf.RoundToInt(num11);
			bool flag = position == Position.Above;
			if (position == Position.Auto)
			{
				UICamera uICamera = UICamera.FindCameraForLayer(mSelection.get_layer());
				if (uICamera != null)
				{
					Vector3 val7 = uICamera.cachedCamera.WorldToViewportPoint(val);
					flag = (val7.y < 0.5f);
				}
			}
			if (isAnimated)
			{
				AnimateColor(mBackground);
				if (Time.get_timeScale() == 0f || Time.get_timeScale() >= 0.1f)
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
				val2.y = val3.y - border.y;
				val3.y = val2.y + (float)mBackground.height;
				val3.x = val2.x + (float)mBackground.width;
				transform.set_localPosition(new Vector3(val2.x, val3.y - border.y, val2.z));
			}
			else
			{
				val3.y = val2.y + border.y;
				val2.y = val3.y - (float)mBackground.height;
				val3.x = val2.x + (float)mBackground.width;
			}
			Transform parent = mPanel.cachedTransform.get_parent();
			if (parent != null)
			{
				val2 = mPanel.cachedTransform.TransformPoint(val2);
				val3 = mPanel.cachedTransform.TransformPoint(val3);
				val2 = parent.InverseTransformPoint(val2);
				val3 = parent.InverseTransformPoint(val3);
			}
			Vector3 val8 = mPanel.CalculateConstrainOffset(Vector2.op_Implicit(val2), Vector2.op_Implicit(val3));
			val = transform.get_localPosition() + val8;
			val.x = Mathf.Round(val.x);
			val.y = Mathf.Round(val.y);
			transform.set_localPosition(val);
		}
		else
		{
			OnSelect(isSelected: false);
		}
	}
}
