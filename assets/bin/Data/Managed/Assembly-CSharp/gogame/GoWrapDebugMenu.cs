// Decompiled with JetBrains decompiler
// Type: gogame.GoWrapDebugMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace gogame;

public class GoWrapDebugMenu : CustomWindow
{
  public static readonly GoWrapDebugMenu INSTANCE = new GoWrapDebugMenu();

  public GoWrapDebugMenu()
    : base("goWrap_DebugMenu", "goWrap Debug Menu")
  {
  }

  public static void ShowGoWrapDebugMenu() => GoWrapDebugMenu.INSTANCE.Show();

  protected override void DoShow(GameObject mainPanelContainer)
  {
    FlowLayoutGroup flowLayoutGroup = mainPanelContainer.AddComponent<FlowLayoutGroup>();
    flowLayoutGroup.spacing = new Vector2(10f, 10f);
    flowLayoutGroup.horizontal = true;
    flowLayoutGroup.padding = new RectOffset(10, 10, 10, 10);
    GameObject gameObject1 = UIHelper.NewGameObject("ShowGoWrapMenuButton", mainPanelContainer);
    ContentSizeFitter contentSizeFitter1 = gameObject1.AddComponent<ContentSizeFitter>();
    contentSizeFitter1.horizontalFit = (ContentSizeFitter.FitMode) 2;
    contentSizeFitter1.verticalFit = (ContentSizeFitter.FitMode) 2;
    LayoutElement layoutElement1 = gameObject1.AddComponent<LayoutElement>();
    layoutElement1.preferredWidth = 100f;
    layoutElement1.preferredHeight = 30f;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((UnityEvent) UIHelper.MakeButton(gameObject1, "Menu", this.ArialFont, 14, Color.black, Color.white).onClick).AddListener(GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9__3_0 = new UnityAction((object) GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDoShow\u003Eb__3_0))));
    GameObject gameObject2 = UIHelper.NewGameObject("TestTrackingButton", mainPanelContainer);
    ContentSizeFitter contentSizeFitter2 = gameObject2.AddComponent<ContentSizeFitter>();
    contentSizeFitter2.horizontalFit = (ContentSizeFitter.FitMode) 2;
    contentSizeFitter2.verticalFit = (ContentSizeFitter.FitMode) 2;
    LayoutElement layoutElement2 = gameObject2.AddComponent<LayoutElement>();
    layoutElement2.preferredWidth = 100f;
    layoutElement2.preferredHeight = 30f;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((UnityEvent) UIHelper.MakeButton(gameObject2, "Test tracking", this.ArialFont, 14, Color.black, Color.white).onClick).AddListener(GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9__3_1 ?? (GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9__3_1 = new UnityAction((object) GoWrapDebugMenu.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDoShow\u003Eb__3_1))));
    GameObject gameObject3 = UIHelper.NewGameObject("ExitButton", mainPanelContainer);
    ContentSizeFitter contentSizeFitter3 = gameObject3.AddComponent<ContentSizeFitter>();
    contentSizeFitter3.horizontalFit = (ContentSizeFitter.FitMode) 2;
    contentSizeFitter3.verticalFit = (ContentSizeFitter.FitMode) 2;
    LayoutElement layoutElement3 = gameObject3.AddComponent<LayoutElement>();
    layoutElement3.preferredWidth = 100f;
    layoutElement3.preferredHeight = 30f;
    // ISSUE: method pointer
    ((UnityEvent) UIHelper.MakeButton(gameObject3, "Exit", this.ArialFont, 14, Color.black, Color.white).onClick).AddListener(new UnityAction((object) this, __methodptr(Close)));
  }
}
