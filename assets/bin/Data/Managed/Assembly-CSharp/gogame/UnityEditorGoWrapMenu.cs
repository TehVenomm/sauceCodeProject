// Decompiled with JetBrains decompiler
// Type: gogame.UnityEditorGoWrapMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace gogame;

public class UnityEditorGoWrapMenu : CustomWindow
{
  public static readonly UnityEditorGoWrapMenu INSTANCE = new UnityEditorGoWrapMenu();

  public UnityEditorGoWrapMenu()
    : base("goWrap_Menu", "goWrap Menu (UnityEditor)")
  {
    // ISSUE: method pointer
    this.AddOpenListener(new UnityAction((object) this, __methodptr(OnOpen)));
    // ISSUE: method pointer
    this.AddCloseListener(new UnityAction((object) this, __methodptr(OnClose)));
  }

  private void OnOpen() => GoWrap.INSTANCE.SendMessage("handleMenuOpened", (object) "{}");

  private void OnClose() => GoWrap.INSTANCE.SendMessage("handleMenuClosed", (object) "{}");

  public static void ShowGoWrapMenu() => UnityEditorGoWrapMenu.INSTANCE.Show();

  protected override void DoShow(GameObject mainPanelContainer)
  {
    FlowLayoutGroup flowLayoutGroup = mainPanelContainer.AddComponent<FlowLayoutGroup>();
    flowLayoutGroup.spacing = new Vector2(10f, 10f);
    flowLayoutGroup.horizontal = true;
    flowLayoutGroup.padding = new RectOffset(10, 10, 10, 10);
    GameObject gameObject = UIHelper.NewGameObject("ExitButton", mainPanelContainer);
    ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
    contentSizeFitter.horizontalFit = (ContentSizeFitter.FitMode) 2;
    contentSizeFitter.verticalFit = (ContentSizeFitter.FitMode) 2;
    LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
    layoutElement.preferredWidth = 100f;
    layoutElement.preferredHeight = 30f;
    // ISSUE: method pointer
    ((UnityEvent) UIHelper.MakeButton(gameObject, "Exit", this.ArialFont, 14, Color.black, Color.white).onClick).AddListener(new UnityAction((object) this, __methodptr(Close)));
  }
}
