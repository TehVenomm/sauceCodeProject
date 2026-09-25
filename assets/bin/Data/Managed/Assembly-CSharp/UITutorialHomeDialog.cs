// Decompiled with JetBrains decompiler
// Type: UITutorialHomeDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITutorialHomeDialog : MonoBehaviour
{
  private const int oneLine = 0;
  private const int twoLine = 1;
  private const int threeLine = 2;
  [SerializeField]
  private UIPanel[] root;
  [SerializeField]
  private UISprite[] messageLine0;
  [SerializeField]
  private UISprite[] messageLine1;
  [SerializeField]
  private UISprite[] messageLine2;
  [SerializeField]
  private UIPanel lastTutorialPanel;
  [SerializeField]
  private UISprite lastTutorialSprite;
  [SerializeField]
  private UIButton lastTutorialButton;
  private UIAtlas lastTutorialAtlas;
  [SerializeField]
  private UIAtlas[] atlases;
  [SerializeField]
  private GameObject afterGacha2Tutorial;
  [SerializeField]
  private UILabel afterGacha2TutorialMessage;

  public void OpenMessage(string message)
  {
    if (!Object.op_Inequality((Object) this.afterGacha2TutorialMessage, (Object) null))
      return;
    this.afterGacha2TutorialMessage.text = message;
  }

  public void OpenAfterGacha2()
  {
    this.afterGacha2Tutorial.SetActive(true);
    this.afterGacha2Tutorial.GetComponent<UIPanel>().alpha = 0.0f;
    TweenAlpha.Begin(this.afterGacha2Tutorial, 0.3f, 1f);
  }

  public void CloseAfterGacha2(System.Action onClose = null)
  {
    TweenAlpha.Begin(this.afterGacha2Tutorial, 0.3f, 0.0f);
  }

  public void Open(int atlasIndex0, string spriteName0)
  {
    ((Component) this.root[2]).gameObject.SetActive(false);
    ((Component) this.root[1]).gameObject.SetActive(false);
    if (!((Component) this.root[0]).gameObject.activeInHierarchy)
      ((Component) this.root[0]).gameObject.SetActive(true);
    this.messageLine0[0].atlas = this.atlases[atlasIndex0];
    this.messageLine0[0].spriteName = spriteName0;
    this.root[0].alpha = 0.0f;
    TweenAlpha.Begin(((Component) this.root[0]).gameObject, 0.3f, 1f);
  }

  public void Open(int atlasIndex0, string spriteName0, int atlasIndex1, string spriteName1)
  {
    ((Component) this.root[2]).gameObject.SetActive(false);
    if (!((Component) this.root[1]).gameObject.activeInHierarchy)
      ((Component) this.root[1]).gameObject.SetActive(true);
    this.messageLine0[1].atlas = this.atlases[atlasIndex0];
    this.messageLine0[1].spriteName = spriteName0;
    this.messageLine1[1].atlas = this.atlases[atlasIndex1];
    this.messageLine1[1].spriteName = spriteName1;
    this.root[1].alpha = 0.0f;
    TweenAlpha.Begin(((Component) this.root[1]).gameObject, 0.3f, 1f);
  }

  public void Open(
    int atlasIndex0,
    string spriteName0,
    int atlasIndex1,
    string spriteName1,
    int atlasIndex2,
    string spriteName2)
  {
    ((Component) this.root[1]).gameObject.SetActive(false);
    if (!((Component) this.root[2]).gameObject.activeInHierarchy)
      ((Component) this.root[2]).gameObject.SetActive(true);
    this.messageLine0[2].atlas = this.atlases[atlasIndex0];
    this.messageLine0[2].spriteName = spriteName0;
    this.messageLine1[2].atlas = this.atlases[atlasIndex1];
    this.messageLine1[2].spriteName = spriteName1;
    this.messageLine2[2].atlas = this.atlases[atlasIndex2];
    this.messageLine2[2].spriteName = spriteName2;
    this.root[2].alpha = 0.0f;
    TweenAlpha.Begin(((Component) this.root[2]).gameObject, 0.3f, 1f);
  }

  public void SetLastTutorialAtlas(UIAtlas atlas) => this.lastTutorialAtlas = atlas;

  public void OpenLastTutorial()
  {
    if (!Object.op_Inequality((Object) this.lastTutorialAtlas, (Object) null))
      return;
    ((Component) this.lastTutorialPanel).gameObject.SetActive(true);
    ((Component) this.lastTutorialSprite).gameObject.SetActive(true);
    this.lastTutorialSprite.atlas = this.lastTutorialAtlas;
    this.lastTutorialSprite.spriteName = "Tutorial_Matome";
    this.lastTutorialButton.onClick.Clear();
    TweenAlpha.Begin(((Component) this.lastTutorialSprite).gameObject, 0.3f, 1f).AddOnFinished((EventDelegate.Callback) (() => this.lastTutorialButton.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.CloseLastTutorial)))));
  }

  public void CloseLastTutorial()
  {
    TweenAlpha ta = TweenAlpha.Begin(((Component) this.lastTutorialSprite).gameObject, 0.3f, 0.0f);
    ta.AddOnFinished((EventDelegate.Callback) (() =>
    {
      if (Object.op_Inequality((Object) this.lastTutorialAtlas, (Object) null))
        Object.Destroy((Object) this.lastTutorialAtlas);
      Object.DestroyImmediate((Object) ta);
      Object.DestroyImmediate((Object) ((Component) this.lastTutorialSprite).gameObject);
      this.lastTutorialSprite = (UISprite) null;
      this.lastTutorialButton = (UIButton) null;
      this.lastTutorialAtlas = (UIAtlas) null;
    }));
  }

  public void Close(int lineIndex = 0, System.Action onClose = null)
  {
    if (this.afterGacha2Tutorial.gameObject.activeSelf)
      this.CloseAfterGacha2(onClose);
    TweenAlpha ta = TweenAlpha.Begin(((Component) this.root[lineIndex]).gameObject, 0.3f, 0.0f);
    if (onClose == null)
      return;
    ta.AddOnFinished((EventDelegate.Callback) (() =>
    {
      Object.DestroyImmediate((Object) ta);
      if (onClose == null)
        return;
      onClose();
    }));
  }
}
