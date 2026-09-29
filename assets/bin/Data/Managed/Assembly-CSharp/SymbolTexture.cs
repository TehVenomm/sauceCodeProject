// Decompiled with JetBrains decompiler
// Type: SymbolTexture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class SymbolTexture : MonoBehaviour
{
  [SerializeField]
  private UITexture m_Texture;
  private int symbolId;
  private int oldSymbolId;
  public bool isReady;
  private SymbolTable.SymbolType symbolType;
  private IEnumerator m_CoroutineLoadSymbol;

  public void Initilize(SymbolTable.SymbolType type)
  {
    this.symbolType = type;
    this.isReady = false;
    this.oldSymbolId = 0;
    this.symbolId = 0;
    this.SetActiveComponents(false);
  }

  public void LoadSymbol(int symbolIndex)
  {
    this.oldSymbolId = this.symbolId;
    this.symbolId = Singleton<SymbolTable>.I.GetSymbolID(this.symbolType, symbolIndex);
    if (this.oldSymbolId == this.symbolId)
      return;
    this.SetActiveComponents(false);
    this.RequestLoadSymbol();
  }

  public void SetColor(int colorIndex)
  {
    this.m_Texture.color = Singleton<SymbolTable>.I.GetColor(this.symbolType, colorIndex);
  }

  public UITexture GetTexture() => this.m_Texture;

  public void SetActiveComponents(bool isActive) => this.m_Texture.alpha = isActive ? 1f : 0.0f;

  private void RequestLoadSymbol()
  {
    this.CancelLoadSymbol();
    this.m_CoroutineLoadSymbol = this.CoroutineLoadSymbol();
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this._Update());
  }

  private IEnumerator CoroutineLoadSymbol()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_symbol = this.symbolType == SymbolTable.SymbolType.FRAME_OUTLINE ? load_queue.LoadSymbolFrame(this.symbolId) : load_queue.LoadSymbol(this.symbolId);
    while (load_queue.IsLoading())
      yield return (object) null;
    if (Object.op_Inequality(lo_symbol.loadedObject, (Object) null))
    {
      this.m_Texture.mainTexture = (Texture) (lo_symbol.loadedObject as Texture2D);
      this.SetActiveComponents(true);
      this.isReady = true;
    }
    this.m_CoroutineLoadSymbol = (IEnumerator) null;
  }

  private void CancelLoadSymbol() => this.m_CoroutineLoadSymbol = (IEnumerator) null;

  private IEnumerator _Update()
  {
    while (this.m_CoroutineLoadSymbol != null && this.m_CoroutineLoadSymbol.MoveNext())
      yield return (object) null;
  }

  private void OnEnable()
  {
    if (this.m_CoroutineLoadSymbol == null)
      return;
    this.RequestLoadSymbol();
  }
}
