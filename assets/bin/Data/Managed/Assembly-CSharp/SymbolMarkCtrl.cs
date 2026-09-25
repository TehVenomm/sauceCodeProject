// Decompiled with JetBrains decompiler
// Type: SymbolMarkCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class SymbolMarkCtrl : MonoBehaviour
{
  [SerializeField]
  private SymbolTexture markTexture;
  [SerializeField]
  private SymbolTexture frameTexture;
  [SerializeField]
  private SymbolTexture patternTexture;
  [SerializeField]
  private SymbolTexture frameOutLineTexture;

  public void Initilize()
  {
    this.markTexture.Initilize(SymbolTable.SymbolType.MARK);
    this.frameTexture.Initilize(SymbolTable.SymbolType.FRAME);
    this.frameOutLineTexture.Initilize(SymbolTable.SymbolType.FRAME_OUTLINE);
    this.patternTexture.Initilize(SymbolTable.SymbolType.PATTERN);
  }

  public void SetSize(int size)
  {
    this.markTexture.GetTexture().width = size;
    this.markTexture.GetTexture().height = size;
    this.frameTexture.GetTexture().width = size;
    this.frameTexture.GetTexture().height = size;
    this.frameOutLineTexture.GetTexture().width = size;
    this.frameOutLineTexture.GetTexture().height = size;
    this.patternTexture.GetTexture().width = size;
    this.patternTexture.GetTexture().height = size;
  }

  public void LoadSymbol(ClanSymbolData data)
  {
    this.markTexture.LoadSymbol(data.m);
    this.markTexture.SetColor(data.mo);
    this.frameTexture.LoadSymbol(data.f);
    this.frameOutLineTexture.LoadSymbol(data.f);
    this.frameTexture.SetColor(data.fo);
    this.patternTexture.LoadSymbol(data.p);
    this.patternTexture.SetColor(data.po);
  }
}
