// Decompiled with JetBrains decompiler
// Type: UISimpleAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UISimpleAnnounce : UIAnnounceBase<UISimpleAnnounce>
{
  [SerializeField]
  protected GameObject rootObj;
  [SerializeField]
  protected UILabel titleLabel;
  [SerializeField]
  protected UILabel contentsLabel;
  private Vector3 LocalPositionOrg;

  public void Announce(string title, string contents)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.titleLabel.text = title;
    this.contentsLabel.text = contents;
  }

  protected override void OnStart() => ((Component) this).gameObject.SetActive(false);

  protected override void OnAfterAnimation() => ((Component) this).gameObject.SetActive(false);
}
