// Decompiled with JetBrains decompiler
// Type: ExploreMapLocation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ExploreMapLocation : MonoBehaviour
{
  [SerializeField]
  private int _mapId;
  private Texture2D _icon;

  public int mapId
  {
    get => this._mapId;
    set => this._mapId = value;
  }

  public Texture2D icon
  {
    get => this._icon;
    set => this._icon = value;
  }
}
