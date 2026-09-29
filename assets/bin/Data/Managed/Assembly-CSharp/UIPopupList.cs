// Decompiled with JetBrains decompiler
// Type: UIPopupList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
  public static UIPopupList current;
  private static GameObject mChild;
  private static float mFadeOutComplete;
  private const float animSpeed = 0.15f;
  public UIAtlas atlas;
  public UIFont bitmapFont;
  public Font trueTypeFont;
  public int fontSize = 16 /*0x10*/;
  public FontStyle fontStyle;
  public string backgroundSprite;
  public string highlightSprite;
  public UIPopupList.Position position;
  public NGUIText.Alignment alignment = NGUIText.Alignment.Left;
  public List<string> items = new List<string>();
  public List<object> itemData = new List<object>();
  public Vector2 padding = Vector2.op_Implicit(new Vector3(4f, 4f));
  public Color textColor = Color.white;
  public Color backgroundColor = Color.white;
  public Color highlightColor = new Color(0.882352948f, 0.784313738f, 0.5882353f, 1f);
  public bool isAnimated = true;
  public bool isLocalized;
  public UIPopupList.OpenOn openOn;
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
  private UIPopupList.LegacyEvent mLegacyEvent;
  [NonSerialized]
  private bool mExecuting;
  private bool mUseDynamicFont;
  private bool mTweening;
  public GameObject source;

  public Object ambigiousFont
  {
    get
    {
      if (Object.op_Inequality((Object) this.trueTypeFont, (Object) null))
        return (Object) this.trueTypeFont;
      return Object.op_Inequality((Object) this.bitmapFont, (Object) null) ? (Object) this.bitmapFont : (Object) this.font;
    }
    set
    {
      switch (value)
      {
        case Font _:
          this.trueTypeFont = value as Font;
          this.bitmapFont = (UIFont) null;
          this.font = (UIFont) null;
          break;
        case UIFont _:
          this.bitmapFont = value as UIFont;
          this.trueTypeFont = (Font) null;
          this.font = (UIFont) null;
          break;
      }
    }
  }

  [Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
  public UIPopupList.LegacyEvent onSelectionChange
  {
    get => this.mLegacyEvent;
    set => this.mLegacyEvent = value;
  }

  public static bool isOpen
  {
    get
    {
      if (!Object.op_Inequality((Object) UIPopupList.current, (Object) null))
        return false;
      return Object.op_Inequality((Object) UIPopupList.mChild, (Object) null) || (double) UIPopupList.mFadeOutComplete > (double) Time.unscaledTime;
    }
  }

  public string value
  {
    get => this.mSelectedItem;
    set
    {
      this.mSelectedItem = value;
      if (this.mSelectedItem == null || this.mSelectedItem == null)
        return;
      this.TriggerCallbacks();
    }
  }

  public object data
  {
    get
    {
      int index = this.items.IndexOf(this.mSelectedItem);
      return index <= -1 || index >= this.itemData.Count ? (object) null : this.itemData[index];
    }
  }

  public bool isColliderEnabled
  {
    get
    {
      Collider component1 = ((Component) this).GetComponent<Collider>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        return component1.enabled;
      Collider2D component2 = ((Component) this).GetComponent<Collider2D>();
      return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
    }
  }

  [Obsolete("Use 'value' instead")]
  public string selection
  {
    get => this.value;
    set => this.value = value;
  }

  private bool isValid
  {
    get
    {
      return Object.op_Inequality((Object) this.bitmapFont, (Object) null) || Object.op_Inequality((Object) this.trueTypeFont, (Object) null);
    }
  }

  private int activeFontSize
  {
    get
    {
      return !Object.op_Inequality((Object) this.trueTypeFont, (Object) null) && !Object.op_Equality((Object) this.bitmapFont, (Object) null) ? this.bitmapFont.defaultSize : this.fontSize;
    }
  }

  private float activeFontScale
  {
    get
    {
      return !Object.op_Inequality((Object) this.trueTypeFont, (Object) null) && !Object.op_Equality((Object) this.bitmapFont, (Object) null) ? (float) this.fontSize / (float) this.bitmapFont.defaultSize : 1f;
    }
  }

  public void Clear()
  {
    this.items.Clear();
    this.itemData.Clear();
  }

  public void AddItem(string text)
  {
    this.items.Add(text);
    this.itemData.Add((object) null);
  }

  public void AddItem(string text, object data)
  {
    this.items.Add(text);
    this.itemData.Add(data);
  }

  public void RemoveItem(string text)
  {
    int index = this.items.IndexOf(text);
    if (index == -1)
      return;
    this.items.RemoveAt(index);
    this.itemData.RemoveAt(index);
  }

  public void RemoveItemByData(object data)
  {
    int index = this.itemData.IndexOf(data);
    if (index == -1)
      return;
    this.items.RemoveAt(index);
    this.itemData.RemoveAt(index);
  }

  protected void TriggerCallbacks()
  {
    if (this.mExecuting)
      return;
    this.mExecuting = true;
    UIPopupList current = UIPopupList.current;
    UIPopupList.current = this;
    if (this.mLegacyEvent != null)
      this.mLegacyEvent(this.mSelectedItem);
    if (EventDelegate.IsValid(this.onChange))
      EventDelegate.Execute(this.onChange);
    else if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.functionName))
      this.eventReceiver.SendMessage(this.functionName, (object) this.mSelectedItem, (SendMessageOptions) 1);
    UIPopupList.current = current;
    this.mExecuting = false;
  }

  private void OnEnable()
  {
    if (EventDelegate.IsValid(this.onChange))
    {
      this.eventReceiver = (GameObject) null;
      this.functionName = (string) null;
    }
    if (Object.op_Inequality((Object) this.font, (Object) null))
    {
      if (this.font.isDynamic)
      {
        this.trueTypeFont = this.font.dynamicFont;
        this.fontStyle = this.font.dynamicFontStyle;
        this.mUseDynamicFont = true;
      }
      else if (Object.op_Equality((Object) this.bitmapFont, (Object) null))
      {
        this.bitmapFont = this.font;
        this.mUseDynamicFont = false;
      }
      this.font = (UIFont) null;
    }
    if ((double) this.textScale != 0.0)
    {
      this.fontSize = Object.op_Inequality((Object) this.bitmapFont, (Object) null) ? Mathf.RoundToInt((float) this.bitmapFont.defaultSize * this.textScale) : 16 /*0x10*/;
      this.textScale = 0.0f;
    }
    if (!Object.op_Equality((Object) this.trueTypeFont, (Object) null) || !Object.op_Inequality((Object) this.bitmapFont, (Object) null) || !this.bitmapFont.isDynamic)
      return;
    this.trueTypeFont = this.bitmapFont.dynamicFont;
    this.bitmapFont = (UIFont) null;
  }

  private void OnValidate()
  {
    Font trueTypeFont = this.trueTypeFont;
    UIFont bitmapFont = this.bitmapFont;
    this.bitmapFont = (UIFont) null;
    this.trueTypeFont = (Font) null;
    if (Object.op_Inequality((Object) trueTypeFont, (Object) null) && (Object.op_Equality((Object) bitmapFont, (Object) null) || !this.mUseDynamicFont))
    {
      this.bitmapFont = (UIFont) null;
      this.trueTypeFont = trueTypeFont;
      this.mUseDynamicFont = true;
    }
    else if (Object.op_Inequality((Object) bitmapFont, (Object) null))
    {
      if (bitmapFont.isDynamic)
      {
        this.trueTypeFont = bitmapFont.dynamicFont;
        this.fontStyle = bitmapFont.dynamicFontStyle;
        this.fontSize = bitmapFont.defaultSize;
        this.mUseDynamicFont = true;
      }
      else
      {
        this.bitmapFont = bitmapFont;
        this.mUseDynamicFont = false;
      }
    }
    else
    {
      this.trueTypeFont = trueTypeFont;
      this.mUseDynamicFont = true;
    }
  }

  private void Start()
  {
    if (Object.op_Inequality((Object) this.textLabel, (Object) null))
    {
      EventDelegate.Add(this.onChange, new EventDelegate.Callback(this.textLabel.SetCurrentSelection));
      this.textLabel = (UILabel) null;
    }
    if (!Application.isPlaying || !string.IsNullOrEmpty(this.mSelectedItem) || this.items.Count <= 0)
      return;
    this.value = this.items[0];
  }

  private void OnLocalize()
  {
    if (!this.isLocalized)
      return;
    this.TriggerCallbacks();
  }

  private void Highlight(UILabel lbl, bool instant)
  {
    if (!Object.op_Inequality((Object) this.mHighlight, (Object) null))
      return;
    this.mHighlightedLabel = lbl;
    if (this.mHighlight.GetAtlasSprite() == null)
      return;
    Vector3 highlightPosition = this.GetHighlightPosition();
    if (!instant && this.isAnimated)
    {
      TweenPosition.Begin(((Component) this.mHighlight).gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
      if (this.mTweening)
        return;
      this.mTweening = true;
      this.StartCoroutine("UpdateTweenPosition");
    }
    else
      this.mHighlight.cachedTransform.localPosition = highlightPosition;
  }

  private Vector3 GetHighlightPosition()
  {
    if (Object.op_Equality((Object) this.mHighlightedLabel, (Object) null) || Object.op_Equality((Object) this.mHighlight, (Object) null))
      return Vector3.zero;
    UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
    if (atlasSprite == null)
      return Vector3.zero;
    float pixelSize = this.atlas.pixelSize;
    return Vector3.op_Addition(this.mHighlightedLabel.cachedTransform.localPosition, new Vector3(-((float) atlasSprite.borderLeft * pixelSize), (float) atlasSprite.borderTop * pixelSize, 1f));
  }

  private IEnumerator UpdateTweenPosition()
  {
    if (Object.op_Inequality((Object) this.mHighlight, (Object) null) && Object.op_Inequality((Object) this.mHighlightedLabel, (Object) null))
    {
      TweenPosition tp = ((Component) this.mHighlight).GetComponent<TweenPosition>();
      while (Object.op_Inequality((Object) tp, (Object) null) && ((Behaviour) tp).enabled)
      {
        tp.to = this.GetHighlightPosition();
        yield return (object) null;
      }
      tp = (TweenPosition) null;
    }
    this.mTweening = false;
  }

  private void OnItemHover(GameObject go, bool isOver)
  {
    if (!isOver)
      return;
    this.Highlight(go.GetComponent<UILabel>(), false);
  }

  private void OnItemPress(GameObject go, bool isPressed)
  {
    if (!isPressed)
      return;
    this.Select(go.GetComponent<UILabel>(), true);
    this.value = go.GetComponent<UIEventListener>().parameter as string;
    UIPlaySound[] components = ((Component) this).GetComponents<UIPlaySound>();
    int index = 0;
    for (int length = components.Length; index < length; ++index)
    {
      UIPlaySound uiPlaySound = components[index];
      if (uiPlaySound.trigger == UIPlaySound.Trigger.OnClick)
        NGUITools.PlaySound(uiPlaySound.audioClip, uiPlaySound.volume, 1f);
    }
    this.CloseSelf();
  }

  private void Select(UILabel lbl, bool instant) => this.Highlight(lbl, instant);

  private void OnNavigate(KeyCode key)
  {
    if (!((Behaviour) this).enabled || !Object.op_Equality((Object) UIPopupList.current, (Object) this))
      return;
    int num1 = this.mLabelList.IndexOf(this.mHighlightedLabel);
    if (num1 == -1)
      num1 = 0;
    int num2;
    if (key == 273)
    {
      if (num1 <= 0)
        return;
      this.Select(this.mLabelList[num2 = num1 - 1], false);
    }
    else
    {
      if (key != 274 || num1 + 1 >= this.mLabelList.Count)
        return;
      this.Select(this.mLabelList[num2 = num1 + 1], false);
    }
  }

  private void OnKey(KeyCode key)
  {
    if (!((Behaviour) this).enabled || !Object.op_Equality((Object) UIPopupList.current, (Object) this) || key != UICamera.current.cancelKey0 && key != UICamera.current.cancelKey1)
      return;
    this.OnSelect(false);
  }

  private void OnDisable() => this.CloseSelf();

  private void OnSelect(bool isSelected)
  {
    if (isSelected)
      return;
    this.CloseSelf();
  }

  public static void Close()
  {
    if (!Object.op_Inequality((Object) UIPopupList.current, (Object) null))
      return;
    UIPopupList.current.CloseSelf();
    UIPopupList.current = (UIPopupList) null;
  }

  public void CloseSelf()
  {
    if (!Object.op_Inequality((Object) UIPopupList.mChild, (Object) null) || !Object.op_Equality((Object) UIPopupList.current, (Object) this))
      return;
    this.StopCoroutine("CloseIfUnselected");
    this.mSelection = (GameObject) null;
    this.mLabelList.Clear();
    if (this.isAnimated)
    {
      UIWidget[] componentsInChildren1 = UIPopupList.mChild.GetComponentsInChildren<UIWidget>();
      int index1 = 0;
      for (int length = componentsInChildren1.Length; index1 < length; ++index1)
      {
        UIWidget uiWidget = componentsInChildren1[index1];
        Color color = uiWidget.color;
        color.a = 0.0f;
        TweenColor.Begin(((Component) uiWidget).gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
      }
      Collider[] componentsInChildren2 = UIPopupList.mChild.GetComponentsInChildren<Collider>();
      int index2 = 0;
      for (int length = componentsInChildren2.Length; index2 < length; ++index2)
        componentsInChildren2[index2].enabled = false;
      Object.Destroy((Object) UIPopupList.mChild, 0.15f);
      UIPopupList.mFadeOutComplete = Time.unscaledTime + Mathf.Max(0.1f, 0.15f);
    }
    else
    {
      Object.Destroy((Object) UIPopupList.mChild);
      UIPopupList.mFadeOutComplete = Time.unscaledTime + 0.1f;
    }
    this.mBackground = (UISprite) null;
    this.mHighlight = (UISprite) null;
    UIPopupList.mChild = (GameObject) null;
    UIPopupList.current = (UIPopupList) null;
  }

  private void AnimateColor(UIWidget widget)
  {
    Color color = widget.color;
    widget.color = new Color(color.r, color.g, color.b, 0.0f);
    TweenColor.Begin(((Component) widget).gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
  }

  private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
  {
    Vector3 localPosition = widget.cachedTransform.localPosition;
    Vector3 vector3 = placeAbove ? new Vector3(localPosition.x, bottom, localPosition.z) : new Vector3(localPosition.x, 0.0f, localPosition.z);
    widget.cachedTransform.localPosition = vector3;
    TweenPosition.Begin(((Component) widget).gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
  }

  private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
  {
    GameObject gameObject = ((Component) widget).gameObject;
    Transform cachedTransform = widget.cachedTransform;
    float num = (float) ((double) this.activeFontSize * (double) this.activeFontScale + (double) this.mBgBorder * 2.0);
    cachedTransform.localScale = new Vector3(1f, num / (float) widget.height, 1f);
    TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
    if (!placeAbove)
      return;
    Vector3 localPosition = cachedTransform.localPosition;
    cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float) widget.height + num, localPosition.z);
    TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
  }

  private void Animate(UIWidget widget, bool placeAbove, float bottom)
  {
    this.AnimateColor(widget);
    this.AnimatePosition(widget, placeAbove, bottom);
  }

  private void OnClick()
  {
    if (this.mOpenFrame == Time.frameCount)
      return;
    if (Object.op_Equality((Object) UIPopupList.mChild, (Object) null))
    {
      if (this.openOn == UIPopupList.OpenOn.DoubleClick || this.openOn == UIPopupList.OpenOn.Manual || this.openOn == UIPopupList.OpenOn.RightClick && UICamera.currentTouchID != -2)
        return;
      this.Show();
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mHighlightedLabel, (Object) null))
        return;
      this.OnItemPress(((Component) this.mHighlightedLabel).gameObject, true);
    }
  }

  private void OnDoubleClick()
  {
    if (this.openOn != UIPopupList.OpenOn.DoubleClick)
      return;
    this.Show();
  }

  private IEnumerator CloseIfUnselected()
  {
    do
    {
      yield return (object) null;
    }
    while (!Object.op_Inequality((Object) UICamera.selectedObject, (Object) this.mSelection));
    this.CloseSelf();
  }

  public void Show()
  {
    if (((Behaviour) this).enabled && NGUITools.GetActive(((Component) this).gameObject) && Object.op_Equality((Object) UIPopupList.mChild, (Object) null) && Object.op_Inequality((Object) this.atlas, (Object) null) && this.isValid && this.items.Count > 0)
    {
      this.mLabelList.Clear();
      this.StopCoroutine("CloseIfUnselected");
      UICamera.selectedObject = UICamera.hoveredObject ?? ((Component) this).gameObject;
      this.mSelection = UICamera.selectedObject;
      this.source = UICamera.selectedObject;
      if (Object.op_Equality((Object) this.source, (Object) null))
      {
        Debug.LogError((object) "Popup list needs a source object...");
      }
      else
      {
        this.mOpenFrame = Time.frameCount;
        if (Object.op_Equality((Object) this.mPanel, (Object) null))
        {
          this.mPanel = UIPanel.Find(((Component) this).transform);
          if (Object.op_Equality((Object) this.mPanel, (Object) null))
            return;
        }
        UIPopupList.mChild = new GameObject("Drop-down List");
        UIPopupList.mChild.layer = ((Component) this).gameObject.layer;
        UIPopupList.current = this;
        Transform transform = UIPopupList.mChild.transform;
        transform.parent = this.mPanel.cachedTransform;
        Vector3 vector3_1;
        Vector3 vector3_2;
        Vector3 position;
        if (this.openOn == UIPopupList.OpenOn.Manual && Object.op_Inequality((Object) this.mSelection, (Object) ((Component) this).gameObject))
        {
          vector3_1 = this.mPanel.cachedTransform.InverseTransformPoint(this.mPanel.anchorCamera.ScreenToWorldPoint(Vector2.op_Implicit(UICamera.lastEventPosition)));
          vector3_2 = vector3_1;
          transform.localPosition = vector3_1;
          position = transform.position;
        }
        else
        {
          Bounds relativeWidgetBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, ((Component) this).transform, false, false);
          vector3_1 = ((Bounds) ref relativeWidgetBounds).min;
          vector3_2 = ((Bounds) ref relativeWidgetBounds).max;
          transform.localPosition = vector3_1;
          position = transform.position;
        }
        this.StartCoroutine("CloseIfUnselected");
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        this.mBackground = NGUITools.AddSprite(UIPopupList.mChild, this.atlas, this.backgroundSprite);
        this.mBackground.pivot = UIWidget.Pivot.TopLeft;
        this.mBackground.depth = NGUITools.CalculateNextDepth(((Component) this.mPanel).gameObject);
        this.mBackground.color = this.backgroundColor;
        Vector4 border = this.mBackground.border;
        this.mBgBorder = border.y;
        this.mBackground.cachedTransform.localPosition = new Vector3(0.0f, border.y, 0.0f);
        this.mHighlight = NGUITools.AddSprite(UIPopupList.mChild, this.atlas, this.highlightSprite);
        this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
        this.mHighlight.color = this.highlightColor;
        UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
        if (atlasSprite == null)
          return;
        float borderTop = (float) atlasSprite.borderTop;
        float num1 = (float) this.activeFontSize * this.activeFontScale;
        float num2 = 0.0f;
        float num3 = -this.padding.y;
        List<UILabel> uiLabelList = new List<UILabel>();
        if (!this.items.Contains(this.mSelectedItem))
          this.mSelectedItem = (string) null;
        int index1 = 0;
        for (int count = this.items.Count; index1 < count; ++index1)
        {
          string key = this.items[index1];
          UILabel lbl = NGUITools.AddWidget<UILabel>(UIPopupList.mChild);
          ((Object) lbl).name = index1.ToString();
          lbl.pivot = UIWidget.Pivot.TopLeft;
          lbl.bitmapFont = this.bitmapFont;
          lbl.trueTypeFont = this.trueTypeFont;
          lbl.fontSize = this.fontSize;
          lbl.fontStyle = this.fontStyle;
          lbl.text = this.isLocalized ? Localization.Get(key) : key;
          lbl.color = this.textColor;
          lbl.cachedTransform.localPosition = new Vector3(border.x + this.padding.x - lbl.pivotOffset.x, num3, -1f);
          lbl.overflowMethod = UILabel.Overflow.ResizeFreely;
          lbl.alignment = this.alignment;
          uiLabelList.Add(lbl);
          num3 = num3 - num1 - this.padding.y;
          num2 = Mathf.Max(num2, lbl.printedSize.x);
          UIEventListener uiEventListener = UIEventListener.Get(((Component) lbl).gameObject);
          uiEventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
          uiEventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
          uiEventListener.parameter = (object) key;
          if (this.mSelectedItem == key || index1 == 0 && string.IsNullOrEmpty(this.mSelectedItem))
            this.Highlight(lbl, true);
          this.mLabelList.Add(lbl);
        }
        float num4 = Mathf.Max(num2, (float) ((double) vector3_2.x - (double) vector3_1.x - ((double) border.x + (double) this.padding.x) * 2.0));
        float num5 = num4;
        Vector3 vector3_3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_3).\u002Ector(num5 * 0.5f, (float) (-(double) num1 * 0.5), 0.0f);
        Vector3 vector3_4;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_4).\u002Ector(num5, num1 + this.padding.y, 1f);
        int index2 = 0;
        for (int count = uiLabelList.Count; index2 < count; ++index2)
        {
          UILabel uiLabel = uiLabelList[index2];
          NGUITools.AddWidgetCollider(((Component) uiLabel).gameObject);
          uiLabel.autoResizeBoxCollider = false;
          BoxCollider component1 = ((Component) uiLabel).GetComponent<BoxCollider>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            vector3_3.z = component1.center.z;
            component1.center = vector3_3;
            component1.size = vector3_4;
          }
          else
          {
            BoxCollider2D component2 = ((Component) uiLabel).GetComponent<BoxCollider2D>();
            ((Collider2D) component2).offset = Vector2.op_Implicit(vector3_3);
            component2.size = Vector2.op_Implicit(vector3_4);
          }
        }
        int num6 = Mathf.RoundToInt(num4);
        float num7 = num4 + (float) (((double) border.x + (double) this.padding.x) * 2.0);
        float num8 = num3 - border.y;
        this.mBackground.width = Mathf.RoundToInt(num7);
        this.mBackground.height = Mathf.RoundToInt(-num8 + border.y);
        int index3 = 0;
        for (int count = uiLabelList.Count; index3 < count; ++index3)
        {
          UILabel uiLabel = uiLabelList[index3];
          uiLabel.overflowMethod = UILabel.Overflow.ShrinkContent;
          uiLabel.width = num6;
        }
        float num9 = 2f * this.atlas.pixelSize;
        float num10 = (float) ((double) num7 - ((double) border.x + (double) this.padding.x) * 2.0 + (double) atlasSprite.borderLeft * (double) num9);
        float num11 = num1 + borderTop * num9;
        this.mHighlight.width = Mathf.RoundToInt(num10);
        this.mHighlight.height = Mathf.RoundToInt(num11);
        bool placeAbove = this.position == UIPopupList.Position.Above;
        if (this.position == UIPopupList.Position.Auto)
        {
          UICamera cameraForLayer = UICamera.FindCameraForLayer(this.mSelection.layer);
          if (Object.op_Inequality((Object) cameraForLayer, (Object) null))
            placeAbove = (double) cameraForLayer.cachedCamera.WorldToViewportPoint(position).y < 0.5;
        }
        if (this.isAnimated)
        {
          this.AnimateColor((UIWidget) this.mBackground);
          if ((double) Time.timeScale == 0.0 || (double) Time.timeScale >= 0.10000000149011612)
          {
            float bottom = num8 + num1;
            this.Animate((UIWidget) this.mHighlight, placeAbove, bottom);
            int index4 = 0;
            for (int count = uiLabelList.Count; index4 < count; ++index4)
              this.Animate((UIWidget) uiLabelList[index4], placeAbove, bottom);
            this.AnimateScale((UIWidget) this.mBackground, placeAbove, bottom);
          }
        }
        if (placeAbove)
        {
          vector3_1.y = vector3_2.y - border.y;
          vector3_2.y = vector3_1.y + (float) this.mBackground.height;
          vector3_2.x = vector3_1.x + (float) this.mBackground.width;
          transform.localPosition = new Vector3(vector3_1.x, vector3_2.y - border.y, vector3_1.z);
        }
        else
        {
          vector3_2.y = vector3_1.y + border.y;
          vector3_1.y = vector3_2.y - (float) this.mBackground.height;
          vector3_2.x = vector3_1.x + (float) this.mBackground.width;
        }
        Transform parent = this.mPanel.cachedTransform.parent;
        if (Object.op_Inequality((Object) parent, (Object) null))
        {
          Vector3 vector3_5 = this.mPanel.cachedTransform.TransformPoint(vector3_1);
          Vector3 vector3_6 = this.mPanel.cachedTransform.TransformPoint(vector3_2);
          vector3_1 = parent.InverseTransformPoint(vector3_5);
          vector3_2 = parent.InverseTransformPoint(vector3_6);
        }
        Vector3 constrainOffset = this.mPanel.CalculateConstrainOffset(Vector2.op_Implicit(vector3_1), Vector2.op_Implicit(vector3_2));
        Vector3 vector3_7 = Vector3.op_Addition(transform.localPosition, constrainOffset);
        vector3_7.x = Mathf.Round(vector3_7.x);
        vector3_7.y = Mathf.Round(vector3_7.y);
        transform.localPosition = vector3_7;
      }
    }
    else
      this.OnSelect(false);
  }

  public enum Position
  {
    Auto,
    Above,
    Below,
  }

  public enum OpenOn
  {
    ClickOrTap,
    RightClick,
    DoubleClick,
    Manual,
  }

  public delegate void LegacyEvent(string val);
}
