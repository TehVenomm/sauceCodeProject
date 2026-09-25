// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemDetailEquipDialog : ItemDetailEquip
{
  private const int SE_ID = 40000021;
  private AudioClip m_AudioClip;

  public override void Initialize()
  {
    base.Initialize();
    this.StoreAudioClip();
    this.PlayAudio();
  }

  private void StoreAudioClip()
  {
    string se = ResourceName.GetSE(40000021);
    if (string.IsNullOrEmpty(se))
      return;
    Transform child = ((Component) this).transform.GetChild(0);
    if (Object.op_Equality((Object) child, (Object) null))
      return;
    ResourceLink component = ((Component) child).GetComponent<ResourceLink>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.m_AudioClip = component.Get<AudioClip>(se);
  }

  private void PlayAudio()
  {
    if (!Object.op_Inequality((Object) this.m_AudioClip, (Object) null))
      return;
    SoundManager.PlayOneshotJingle(this.m_AudioClip, 40000021);
  }
}
