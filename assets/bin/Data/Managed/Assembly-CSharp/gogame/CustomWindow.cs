// Decompiled with JetBrains decompiler
// Type: gogame.CustomWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace gogame;

public abstract class CustomWindow
{
  private readonly string _name;
  private readonly UnityEvent _onClose = new UnityEvent();
  private readonly UnityEvent _onOpen = new UnityEvent();
  private readonly string _title;
  public readonly Font ArialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
  private Text _logText;
  private GameObject _rootContainer;

  public CustomWindow(string name, string title)
  {
    this._name = name;
    this._title = title;
  }

  public void AddOpenListener(UnityAction listener) => this._onOpen.AddListener(listener);

  public void AddCloseListener(UnityAction listener) => this._onClose.AddListener(listener);

  protected abstract void DoShow(GameObject mainPanelContainer);

  public void Close()
  {
    Object.Destroy((Object) this._rootContainer);
    this._onClose.Invoke();
  }

  public void Log(string text)
  {
    if (Object.op_Equality((Object) this._logText, (Object) null))
      return;
    this._logText.text = text;
  }

  public void Show()
  {
    this._rootContainer = UIHelper.NewGameObject(this._name);
    GameObject parent = UIHelper.NewGameObject("Canvas", this._rootContainer);
    parent.AddComponent<Canvas>().renderMode = (RenderMode) 0;
    CanvasScaler canvasScaler = parent.AddComponent<CanvasScaler>();
    canvasScaler.uiScaleMode = (CanvasScaler.ScaleMode) 1;
    canvasScaler.screenMatchMode = (CanvasScaler.ScreenMatchMode) 0;
    canvasScaler.referenceResolution = new Vector2(640f, 1136f);
    canvasScaler.matchWidthOrHeight = 1f;
    canvasScaler.referencePixelsPerUnit = 100f;
    parent.AddComponent<GraphicRaycaster>();
    GameObject gameObject1 = UIHelper.NewGameObject("RootPanel", parent);
    RectTransform rectTransform1 = gameObject1.AddComponent<RectTransform>();
    rectTransform1.offsetMin = new Vector2(10f, 10f);
    rectTransform1.offsetMax = new Vector2(-10f, -10f);
    rectTransform1.anchorMin = new Vector2(0.0f, 0.0f);
    rectTransform1.anchorMax = new Vector2(1f, 1f);
    VerticalLayoutGroup verticalLayoutGroup1 = gameObject1.AddComponent<VerticalLayoutGroup>();
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup1).childControlWidth = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup1).childControlHeight = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup1).childForceExpandWidth = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup1).childForceExpandHeight = false;
    UIHelper.SetBackgroundColor(gameObject1, new Color(0.0f, 0.0f, 0.0f, 0.75f));
    GameObject gameObject2 = UIHelper.NewGameObject("TitlePanel", gameObject1);
    gameObject2.AddComponent<LayoutElement>().preferredHeight = 30f;
    HorizontalLayoutGroup horizontalLayoutGroup = gameObject2.AddComponent<HorizontalLayoutGroup>();
    ((HorizontalOrVerticalLayoutGroup) horizontalLayoutGroup).childControlWidth = true;
    ((HorizontalOrVerticalLayoutGroup) horizontalLayoutGroup).childControlHeight = true;
    ((HorizontalOrVerticalLayoutGroup) horizontalLayoutGroup).childForceExpandWidth = false;
    ((HorizontalOrVerticalLayoutGroup) horizontalLayoutGroup).childForceExpandHeight = false;
    UIHelper.SetBackgroundColor(gameObject2, new Color(0.0f, 0.0f, 0.0f, 0.5f));
    GameObject gameObject3 = UIHelper.NewGameObject("BodyPanel", gameObject1);
    gameObject3.AddComponent<LayoutElement>().flexibleHeight = 1f;
    VerticalLayoutGroup verticalLayoutGroup2 = gameObject3.AddComponent<VerticalLayoutGroup>();
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup2).childControlWidth = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup2).childControlHeight = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup2).childForceExpandWidth = true;
    ((HorizontalOrVerticalLayoutGroup) verticalLayoutGroup2).childForceExpandHeight = false;
    UIHelper.SetBackgroundColor(gameObject3, new Color(1f, 1f, 1f, 0.5f));
    GameObject gameObject4 = UIHelper.NewGameObject("TitleText", gameObject2);
    LayoutElement layoutElement1 = gameObject4.AddComponent<LayoutElement>();
    layoutElement1.flexibleWidth = 1f;
    layoutElement1.preferredHeight = 30f;
    Text text = gameObject4.AddComponent<Text>();
    text.text = this._title;
    text.font = this.ArialFont;
    text.fontSize = 14;
    ((Graphic) text).color = Color.white;
    text.alignment = (TextAnchor) 4;
    GameObject gameObject5 = UIHelper.NewGameObject("CloseButton", gameObject2);
    LayoutElement layoutElement2 = gameObject5.AddComponent<LayoutElement>();
    layoutElement2.preferredWidth = 30f;
    layoutElement2.preferredHeight = 30f;
    // ISSUE: method pointer
    ((UnityEvent) UIHelper.MakeButton(gameObject5, "x", this.ArialFont, 14, Color.black, Color.white).onClick).AddListener(new UnityAction((object) this, __methodptr(Close)));
    GameObject gameObject6 = UIHelper.NewGameObject("MainPanel", gameObject3);
    gameObject6.AddComponent<LayoutElement>().flexibleHeight = 0.7f;
    UIHelper.SetBackgroundColor(gameObject6, new Color(0.0f, 0.0f, 0.0f, 0.5f));
    GameObject gameObject7 = UIHelper.NewGameObject("LogPanel", gameObject3);
    gameObject7.AddComponent<LayoutElement>().flexibleHeight = 0.3f;
    UIHelper.SetBackgroundColor(gameObject7, new Color(1f, 1f, 1f, 0.5f));
    GameObject gameObject8 = UIHelper.NewGameObject("LogText", gameObject7);
    RectTransform rectTransform2 = gameObject8.AddComponent<RectTransform>();
    rectTransform2.offsetMin = new Vector2(5f, 1f);
    rectTransform2.offsetMax = new Vector2(-5f, -1f);
    rectTransform2.anchorMin = new Vector2(0.0f, 0.0f);
    rectTransform2.anchorMax = new Vector2(1f, 1f);
    this._logText = gameObject8.AddComponent<Text>();
    this._logText.font = this.ArialFont;
    ((Graphic) this._logText).color = Color.black;
    this._logText.fontSize = 12;
    this._logText.horizontalOverflow = (HorizontalWrapMode) 0;
    this._logText.verticalOverflow = (VerticalWrapMode) 0;
    this._logText.alignment = (TextAnchor) 0;
    this.DoShow(gameObject6);
    this._onOpen.Invoke();
  }
}
