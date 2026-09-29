// Decompiled with JetBrains decompiler
// Type: ChairPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChairPoint : MonoBehaviour
{
  public ChairPoint.CHAIR_TYPE chairType;
  public ChairPoint dir;

  public HomePlayerCharacterBase sittingChara { get; private set; }

  public void SetSittingCharacter(HomePlayerCharacterBase chara) => this.sittingChara = chara;

  public void ResetSittingCharacter() => this.sittingChara = (HomePlayerCharacterBase) null;

  public enum CHAIR_TYPE
  {
    NORMAL,
    BENTCH,
    SOFA,
  }
}
