// Decompiled with JetBrains decompiler
// Type: UITutorialDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITutorialDialog : MonoBehaviour
{
  private const int oneLine = 0;
  private const int twoLine = 1;
  private const int threeLine = 2;
  private const int threeLineLabel = 3;
  private const int threeLineLabel2 = 4;
  [SerializeField]
  private UIPanel[] root;
  [SerializeField]
  private UISprite[] messageLine0;
  [SerializeField]
  private UISprite[] messageLine1;
  [SerializeField]
  private UISprite[] messageLine2;
  [SerializeField]
  private UIAtlas[] atlases;
  public UILabel lbGreeting;
  public UILabel lbChargeWaypoint;

  public void Open(int atlasIndex0, string spriteName0)
  {
    ((Component) this.root[1]).gameObject.SetActive(false);
    ((Component) this.root[2]).gameObject.SetActive(false);
    if (!((Component) this.root[0]).gameObject.activeInHierarchy)
      ((Component) this.root[0]).gameObject.SetActive(true);
    this.messageLine0[0].atlas = this.atlases[atlasIndex0];
    this.messageLine0[0].spriteName = spriteName0;
    this.root[0].alpha = 0.0f;
    TweenAlpha.Begin(((Component) this.root[0]).gameObject, 0.3f, 1f);
  }

  public void Open(int atlasIndex0, string spriteName0, int atlasIndex1, string spriteName1)
  {
    ((Component) this.root[0]).gameObject.SetActive(false);
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
    ((Component) this.root[0]).gameObject.SetActive(false);
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

  public void OpenThreeLineLabel()
  {
    if (((Component) this.root[1]).gameObject.activeInHierarchy)
      TweenAlpha.Begin(((Component) this.root[1]).gameObject, 0.0f, 0.0f);
    if (!((Component) this.root[3]).gameObject.activeInHierarchy)
      ((Component) this.root[3]).gameObject.SetActive(true);
    this.lbGreeting.supportEncoding = true;
    this.root[3].alpha = 0.0f;
    TweenAlpha.Begin(((Component) this.root[3]).gameObject, 0.3f, 1f);
  }

  public void HideThreeLineLabel()
  {
    TweenAlpha.Begin(((Component) this.root[3]).gameObject, 0.3f, 0.0f);
  }

  public void OpenThreeLineLabel2()
  {
    if (!((Component) this.root[4]).gameObject.activeInHierarchy)
      ((Component) this.root[4]).gameObject.SetActive(true);
    this.lbChargeWaypoint.supportEncoding = true;
    this.root[4].alpha = 1f;
    TweenAlpha.Begin(((Component) this.root[4]).gameObject, 3f, 1f).AddOnFinished((EventDelegate.Callback) (() => ((Component) this.root[4]).gameObject.SetActive(false)));
  }

  public bool isThreeLineLabel2Active() => ((Behaviour) this.root[4]).isActiveAndEnabled;

  public bool isTwoLineGameObjectActive() => ((Component) this.root[1]).gameObject.activeSelf;

  public void HideThreeLineLabel2() => ((Component) this.root[4]).gameObject.SetActive(false);

  public void Close(int lineIndex = 0, System.Action onClose = null)
  {
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

  public void CloseaLLImmediately(int lineIndex = 0)
  {
    ((Component) this.root[lineIndex]).gameObject.SetActive(false);
  }
}
