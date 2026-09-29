// Decompiled with JetBrains decompiler
// Type: SymbolMakeListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SymbolMakeListItem : MonoBehaviour
{
  [SerializeField]
  private BoxCollider m_Colider;
  [SerializeField]
  private SymbolTexture m_Texture;
  [SerializeField]
  private UIButton m_Button;
  public Action<int> onButton;

  public int symbolId { get; protected set; }

  public bool IsReady { get; protected set; }

  private void Awake()
  {
    this.m_Button.CacheDefaultColor();
    this.m_Button.tweenTarget = (GameObject) null;
  }

  public void Init(int id, SymbolTable.SymbolType type)
  {
    this.symbolId = Singleton<SymbolTable>.I.GetSymbolIndex(type, id);
    this.m_Texture.Initilize(type);
    this.m_Texture.LoadSymbol(this.symbolId);
    if (type != SymbolTable.SymbolType.FRAME_OUTLINE)
      return;
    ++this.m_Texture.GetTexture().depth;
  }

  public void OnButton()
  {
    if (!this.m_Texture.isReady || this.onButton == null)
      return;
    this.onButton(this.symbolId);
  }

  public void SetButtonActive(bool isActive)
  {
    ((Collider) this.m_Colider).enabled = isActive;
    ((Behaviour) this.m_Button).enabled = isActive;
  }
}
