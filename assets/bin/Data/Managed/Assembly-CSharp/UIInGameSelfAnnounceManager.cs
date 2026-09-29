// Decompiled with JetBrains decompiler
// Type: UIInGameSelfAnnounceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIInGameSelfAnnounceManager : MonoBehaviourSingleton<UIInGameSelfAnnounceManager>
{
  [SerializeField]
  protected UIInGameSelfAnnounce regionBreak;
  [SerializeField]
  protected UIInGameSelfAnnounce regionDragonArmor;
  [SerializeField]
  protected UIInGameSelfAnnounce supplyInformation;

  public void PlayRegionBreak()
  {
    if (Object.op_Equality((Object) this.regionBreak, (Object) null))
      return;
    this.regionBreak.Play();
  }

  public void PlayDragonArmorBreak()
  {
    if (Object.op_Equality((Object) this.regionDragonArmor, (Object) null))
      return;
    this.regionDragonArmor.Play();
  }

  public void PlaySupplyInformation()
  {
    if (Object.op_Equality((Object) this.supplyInformation, (Object) null))
      return;
    this.supplyInformation.Play();
  }
}
