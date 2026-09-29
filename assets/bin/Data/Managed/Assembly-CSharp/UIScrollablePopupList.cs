// Decompiled with JetBrains decompiler
// Type: UIScrollablePopupList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIScrollablePopupList : UILabel
{
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

  public static void CreatePopup(
    Transform popup_transform,
    Transform parent_ctrl,
    int max_num,
    UIScrollablePopupList.ATTACH_DIRECTION direction,
    bool adjust_size,
    string[] texts,
    bool[] button_enable,
    int select_index,
    Action<int> callback = null)
  {
    UIScrollablePopupList._CreatePopup(popup_transform, parent_ctrl, max_num, direction, adjust_size, (Transform) null, texts, button_enable, select_index, callback);
  }

  public static void CreatePopupItem(
    Transform popup_transform,
    Transform parent_ctrl,
    int max_num,
    UIScrollablePopupList.ATTACH_DIRECTION direction,
    bool adjust_size,
    Transform item_prefab,
    string[] texts,
    bool[] button_enable,
    int select_index,
    Action<int> callback = null)
  {
    UIScrollablePopupList.firstItemIsTextOnly = true;
    UIScrollablePopupList._CreatePopup(popup_transform, parent_ctrl, max_num, direction, adjust_size, item_prefab, texts, button_enable, select_index, callback);
    if (!Object.op_Inequality((Object) item_prefab, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) item_prefab).gameObject);
  }

  private static void _CreatePopup(
    Transform popup_transform,
    Transform parent_ctrl,
    int max_num,
    UIScrollablePopupList.ATTACH_DIRECTION direction,
    bool adjust_size,
    Transform item_prefab,
    string[] texts,
    bool[] button_enable,
    int select_index,
    Action<int> callback = null)
  {
    if (Object.op_Equality((Object) popup_transform, (Object) null) || Object.op_Equality((Object) parent_ctrl, (Object) null))
      return;
    UIWidget component1 = ((Component) parent_ctrl).GetComponent<UIWidget>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return;
    float num1 = 0.0f;
    float num2 = 0.0f;
    switch (direction)
    {
      case UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM:
        num2 = (float) (-component1.height / 2);
        break;
      case UIScrollablePopupList.ATTACH_DIRECTION.LEFT:
        num1 = (float) -component1.width;
        num2 = (float) (component1.height / 2);
        break;
      case UIScrollablePopupList.ATTACH_DIRECTION.RIGHT:
        num1 = (float) component1.width;
        num2 = (float) (component1.height / 2);
        break;
    }
    popup_transform.parent = parent_ctrl;
    popup_transform.localPosition = new Vector3(num1, num2, 0.0f);
    popup_transform.localScale = Vector3.one;
    UIScrollablePopupList component2 = ((Component) popup_transform).GetComponent<UIScrollablePopupList>();
    if (adjust_size)
      component2.minPopFrameWidth = component1.width;
    component2.maxItemNum = max_num;
    component2.SetItem(item_prefab, texts, button_enable, select_index, callback);
  }

  protected override void Awake()
  {
    this.isFinished = false;
    this.isUpdateTween = false;
    this.selectIndex = -1;
    this.twCtrl = ((Component) this).GetComponentInChildren<UITweenCtrl>();
    this.twAlpha = ((Component) this).GetComponent<TweenAlpha>();
    this.del = new EventDelegate((EventDelegate.Callback) (() => this.CloseCallback()));
    base.Awake();
  }

  private new void Start()
  {
    this.gridAncor = ((Component) this.grid).GetComponent<UIWidget>().leftAnchor.target;
    base.Start();
  }

  private void LateUpdate()
  {
    if (!this.isUpdateTween || !Object.op_Inequality((Object) this.scroll, (Object) null) || !Object.op_Inequality((Object) this.grid, (Object) null) || !Object.op_Inequality((Object) this.tw, (Object) null) || this.isFinished)
      return;
    if ((double) this.tw.tweenFactor < 1.0)
    {
      this.grid.cellHeight = (float) this.itemHeight * this.tw.tweenFactor;
    }
    else
    {
      this.grid.cellHeight = (float) this.itemHeight;
      this.isFinished = true;
      this.isUpdateTween = false;
    }
    this.scroll.ResetPosition();
    this.scroll.MoveRelative(new Vector3(0.0f, this.grid.cellHeight * (float) this.selectIndex));
    this.grid.Reposition();
    if (!this.isFinished)
      return;
    ((Component) this.grid).GetComponent<UIWidget>().SetAnchor((Transform) null);
  }

  private void StartTween()
  {
    this.isFinished = false;
    this.isUpdateTween = true;
    ((Component) this.grid).GetComponent<UIWidget>().SetAnchor(this.gridAncor);
    this.twCtrl.Reset();
    this.twCtrl.Play(onFinished: (EventDelegate.Callback) (() => { }));
  }

  private void ClosePopupCallBack()
  {
    if (this.closePopupCallback == null)
      return;
    this.closePopupCallback(this.selectIndex);
  }

  public void SetItem(
    Transform item_prefab,
    string[] texts,
    bool[] button_enable,
    int select_index,
    Action<int> close_callback)
  {
    this.closePopupCallback = close_callback;
    if (this.isFinished || this.isUpdateTween)
    {
      this.ClosePopupCallBack();
    }
    else
    {
      this.selectIndex = select_index;
      this.SetItemText(item_prefab, texts, button_enable);
    }
  }

  public void SetItemText(Transform item_prefab, string[] texts, bool[] button_enable)
  {
    if (this.isFinished || this.isUpdateTween)
    {
      this.ClosePopupCallBack();
    }
    else
    {
      this.textItem = texts;
      this.buttonEnable = button_enable;
      int width = this.SetGridItem(item_prefab);
      int height = (int) ((double) ((float) Mathf.Min(this.textItem.Length, this.maxItemNum) + 0.5f) * (double) this.itemHeight);
      Vector4 baseClipRegion = this.scroll.panel.baseClipRegion;
      this.scroll.panel.SetRect(baseClipRegion.x, baseClipRegion.y, (float) width, (float) height);
      Vector3 localPosition = ((Component) this.scroll).transform.localPosition;
      localPosition.y = (float) -((double) height * 0.5);
      ((Component) this.scroll).transform.localPosition = localPosition;
      Vector2 clipOffset = this.scroll.panel.clipOffset;
      clipOffset.y = 0.0f;
      this.scroll.panel.clipOffset = clipOffset;
      this.selectFrameSprite.width = width - 10;
      this.selectFrameSprite.height = this.itemHeight;
      ((Component) this.selectFrameSprite).transform.localScale = Vector3.one;
      this.tw.to = height;
      ((Component) this.tw).GetComponent<UIWidget>().width = width;
      this.expandTarget_A.height = height;
      this.expandTarget_B.height = height;
      Vector2 vector2 = Vector2.op_Implicit(((Component) this.expandTarget_A).transform.localPosition);
      vector2.x = (float) width * 0.5f;
      vector2.y = (float) -height * 0.5f;
      ((Component) this.expandTarget_A).transform.localPosition = Vector2.op_Implicit(vector2);
      ((Component) this.expandTarget_B).transform.localPosition = Vector2.op_Implicit(vector2);
      this.twAlpha.RemoveOnFinished(this.del);
      this.objRoot.SetActive(true);
      this.scroll.ResetPosition();
      this.ClickItem(this.selectIndex, this.GetGridChild(this.selectIndex));
      this.StartTween();
    }
  }

  private int SetGridItem(Transform item_prefab)
  {
    int base_max_width = this.minPopFrameWidth;
    if (this.textItem != null && this.textItem.Length != 0)
    {
      this.DeleteGridChildren();
      UIWidget[] uiWidgetArray = new UIWidget[this.textItem.Length];
      int index1 = 0;
      for (int length = this.textItem.Length; index1 < length; ++index1)
      {
        GameObject go;
        if (Object.op_Equality((Object) item_prefab, (Object) null) || UIScrollablePopupList.firstItemIsTextOnly)
        {
          UIScrollablePopupList.firstItemIsTextOnly = false;
          go = new GameObject();
          go.layer = 5;
          ((Object) go).name = index1.ToString();
          base_max_width = this.CreateItem(go, index1, base_max_width);
        }
        else
        {
          go = ResourceUtility.Instantiate<GameObject>(((Component) item_prefab).gameObject);
          go.layer = 5;
          ((Object) go).name = index1.ToString();
          base_max_width = this.CreatePrefabItem(go, index1, base_max_width);
        }
        UIWidget component = go.GetComponent<UIWidget>();
        uiWidgetArray[index1] = component;
        go.AddComponent<BoxCollider>();
        go.AddComponent<UIDragScrollView>();
        go.AddComponent<UIGameSceneEventSender>();
        UIButton btn = go.AddComponent<UIButton>();
        btn.hover = component.color;
        btn.pressed = component.color;
        btn.onClick.Add(new EventDelegate((EventDelegate.Callback) (() =>
        {
          int result = -1;
          if (!int.TryParse(((Object) btn).name, out result))
            return;
          this.selectIndex = result;
          if (result < 0)
            return;
          this.ClickItem(this.selectIndex, ((Component) btn).transform);
          this.CloseCallback();
        })));
        ((Behaviour) btn).enabled = this.buttonEnable[index1];
        if (Object.op_Equality((Object) go.GetComponent<UIButtonScale>(), (Object) null))
        {
          UIButtonScale uiButtonScale = go.gameObject.AddComponent<UIButtonScale>();
          uiButtonScale.tweenTarget = go.transform;
          uiButtonScale.hover = new Vector3(1f, 1f, 1f);
          uiButtonScale.pressed = new Vector3(1.3f, 1.3f, 1.3f);
          uiButtonScale.duration = 0.05f;
        }
        this.grid.AddChild(go.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;
      }
      int index2 = 0;
      for (int length = this.textItem.Length; index2 < length; ++index2)
      {
        BoxCollider component = ((Component) ((Component) uiWidgetArray[index2]).transform).GetComponent<BoxCollider>();
        UIWidget uiWidget = uiWidgetArray[index2];
        component.size = new Vector3((float) base_max_width, (float) this.itemHeight, 1f);
        uiWidget.width = base_max_width;
        uiWidget.height = this.itemHeight;
        uiWidget.autoResizeBoxCollider = true;
        uiWidget.ResizeCollider();
      }
      this.grid.Reposition();
    }
    return base_max_width;
  }

  public int CreateItem(GameObject go, int index, int base_max_width)
  {
    UILabel component1 = ((Component) this).GetComponent<UILabel>();
    UIWidget component2 = ((Component) this).GetComponent<UIWidget>();
    int num1 = base_max_width;
    UILabel uiLabel = go.AddComponent<UILabel>();
    uiLabel.pivot = component2.pivot;
    uiLabel.bitmapFont = component1.bitmapFont;
    uiLabel.trueTypeFont = component1.trueTypeFont;
    uiLabel.fontSize = component1.fontSize;
    uiLabel.fontStyle = component1.fontStyle;
    uiLabel.text = this.textItem[index];
    uiLabel.color = this.buttonEnable[index] ? component1.color : Color.gray;
    uiLabel.alpha = 1f;
    uiLabel.alignment = component1.alignment;
    uiLabel.cachedTransform.localPosition = component1.cachedTransform.localPosition;
    uiLabel.AssumeNaturalSize();
    int width = uiLabel.width;
    int num2 = Mathf.Max(num1, width);
    uiLabel.overflowMethod = UILabel.Overflow.ShrinkContent;
    return num2;
  }

  public int CreatePrefabItem(GameObject go, int index, int base_max_width)
  {
    int num1 = base_max_width;
    UILabel component = ((Component) this).GetComponent<UILabel>();
    UISprite componentInChildren1 = go.GetComponentInChildren<UISprite>();
    UILabel componentInChildren2 = ((Component) componentInChildren1).GetComponentInChildren<UILabel>();
    componentInChildren2.text = this.textItem[index];
    componentInChildren2.color = this.buttonEnable[index] ? component.color : Color.gray;
    componentInChildren2.alpha = 1f;
    componentInChildren2.cachedTransform.localPosition = component.cachedTransform.localPosition;
    int num2 = componentInChildren2.width + componentInChildren1.width;
    return Mathf.Max(num1, num2);
  }

  public void ClosePopup()
  {
    this.twAlpha.RemoveOnFinished(this.del);
    this.twAlpha.onFinished.Add(this.del);
    this.twAlpha.PlayReverse();
  }

  private void CloseCallback()
  {
    this.scroll.ResetPosition();
    Vector2 clipOffset = this.scroll.panel.clipOffset;
    clipOffset.y = 0.0f;
    this.scroll.panel.clipOffset = clipOffset;
    ((Component) this.scroll).transform.localPosition = Vector3.zero;
    this.twCtrl.Reset();
    this.twAlpha.RemoveOnFinished(this.del);
    this.DeleteGridChildren();
    this.objRoot.SetActive(false);
    this.isFinished = false;
    this.isUpdateTween = false;
    this.ClosePopupCallBack();
  }

  private void DeleteGridChildren()
  {
    this.DetachSelectFrame();
    this.grid.GetChildList().ForEach((Action<Transform>) (t =>
    {
      if (!this.grid.RemoveChild(t))
        return;
      ((Component) t).transform.parent = (Transform) null;
      Object.Destroy((Object) ((Component) t).gameObject);
    }));
    this.grid.Reposition();
  }

  private void ClickItem(int index, Transform parent)
  {
    if (!Object.op_Inequality((Object) this.selectFrameSprite, (Object) null))
      return;
    Transform transform = ((Component) this.selectFrameSprite).transform;
    if (index >= 0)
    {
      transform.parent = parent;
      transform.localPosition = Vector3.zero;
      ((Component) this.selectFrameSprite).gameObject.SetActive(true);
    }
    else
    {
      ((Component) this.selectFrameSprite).gameObject.SetActive(false);
      transform.parent = ((Component) this).transform;
      transform.localPosition = Vector3.zero;
    }
  }

  private void DetachSelectFrame() => this.ClickItem(-1, (Transform) null);

  private Transform GetGridChild(int index)
  {
    return Object.op_Equality((Object) this.grid, (Object) null) ? (Transform) null : ((Component) this.grid).transform.Find(index.ToString());
  }

  public enum ATTACH_DIRECTION
  {
    BOTTOM,
    LEFT,
    RIGHT,
  }
}
