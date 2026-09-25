// Decompiled with JetBrains decompiler
// Type: UIPlaySoundCustom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("ProjectUI/UIPlaySoundCustom")]
public class UIPlaySoundCustom : MonoBehaviour
{
  public SoundID.UISE SEType = SoundID.UISE.INVALID;
  public int SEID;
  private string SEName = string.Empty;
  public UIPlaySoundCustom.Trigger trigger;
  public ResourceLink ResourceLink;
  [Range(0.0f, 1f)]
  public float volume = 1f;
  private const float pitch = 1f;

  private bool DoesNeedToFindSource() => this.SEType == SoundID.UISE.INVALID && this.SEID > 0;

  private void FindSource()
  {
    this.SEName = ResourceName.GetSE(this.SEID);
    if (Object.op_Inequality((Object) this.ResourceLink, (Object) null))
      return;
    for (Transform parent = ((Component) this).gameObject.transform.parent; Object.op_Inequality((Object) parent, (Object) null) && !(((Object) parent).name == "UI Root"); parent = ((Component) parent).transform.parent)
    {
      ResourceLink component = ((Component) parent).GetComponent<ResourceLink>();
      if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) component.Get<AudioClip>(this.SEName), (Object) null))
      {
        this.ResourceLink = component;
        break;
      }
    }
  }

  public void Start()
  {
    if (!this.DoesNeedToFindSource())
      return;
    this.FindSource();
  }

  public void Play()
  {
    if (this.DoesNeedToFindSource())
      this.KeyOnById();
    else
      this.KeyOnSystemSE();
  }

  private void KeyOnById()
  {
    if (Object.op_Equality((Object) this.ResourceLink, (Object) null) || string.IsNullOrEmpty(this.SEName))
      return;
    this.KeyOn(this.ResourceLink.Get<AudioClip>(this.SEName), this.SEID);
  }

  private void KeyOnSystemSE()
  {
    if (this.SEType == SoundID.UISE.INVALID || !MonoBehaviourSingleton<SoundManager>.IsValid() || !MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      return;
    SoundManager.PlaySystemSE(this.SEType);
  }

  private void KeyOn(AudioClip clip, int id)
  {
    if (Object.op_Equality((Object) clip, (Object) null))
      return;
    SoundManager.PlayUISE(clip, this.volume, false, (Transform) null, id);
  }

  public enum Trigger
  {
    OnClick,
    OnMouseOver,
    OnMouseOut,
    OnPress,
    OnRelease,
    Custom,
    OnEnable,
    OnDisable,
  }
}
