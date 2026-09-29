// Decompiled with JetBrains decompiler
// Type: UI_LastTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UI_LastTutorial : MonoBehaviour
{
  [SerializeField]
  private UIPanel lastTutorialPanel;
  [SerializeField]
  private UISprite lastTutorialSprite;
  [SerializeField]
  private UIButton lastTutorialButton;

  public void OpenLastTutorial()
  {
    this.lastTutorialPanel.depth = 6500;
    ((Component) this.lastTutorialPanel).gameObject.SetActive(true);
    this.lastTutorialButton.onClick.Clear();
    TweenAlpha.Begin(((Component) this.lastTutorialSprite).gameObject, 0.3f, 1f).AddOnFinished((EventDelegate.Callback) (() => this.lastTutorialButton.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.CloseLastTutorial)))));
  }

  public void CloseLastTutorial()
  {
    TweenAlpha.Begin(((Component) this.lastTutorialSprite).gameObject, 0.3f, 0.0f).AddOnFinished((EventDelegate.Callback) (() =>
    {
      if (AppMain.isApplicationQuit)
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }));
  }
}
