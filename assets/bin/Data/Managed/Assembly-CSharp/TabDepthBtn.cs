// Decompiled with JetBrains decompiler
// Type: TabDepthBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TabDepthBtn : MonoBehaviour
{
  [SerializeField]
  private UIButton m_buttonObject;
  [SerializeField]
  private UISprite m_backGroundSprite;
  [SerializeField]
  private UISprite m_labelSprite;
  private int startDepth;
  public TabDepthBtn.STATE currentState;

  protected virtual string GetBtnSelectedSprite() => "PartyBtn_on_02";

  protected virtual string GetBtnUnSelectedSprite() => "PartyBtn_off_02";

  protected virtual string GetTabSelectedSprite()
  {
    this.m_labelSprite.spriteName.Replace("off", "on");
    return this.m_labelSprite.spriteName.Replace("off", "on");
  }

  protected virtual string GetTabUnSelectedSprite()
  {
    this.m_labelSprite.spriteName.Replace("off", "on");
    return this.m_labelSprite.spriteName.Replace("on", "off");
  }

  protected virtual int GetDepthOffSet() => 30;

  public virtual void Initilize()
  {
    this.startDepth = this.m_backGroundSprite.depth;
    this.UnSelect();
  }

  public void SetState(TabDepthBtn.STATE state) => this.currentState = state;

  public void Select()
  {
    this.SetState(TabDepthBtn.STATE.SELECTED);
    this.SetTab();
  }

  public void UnSelect()
  {
    this.SetState(TabDepthBtn.STATE.UNSELECTED);
    this.SetTab();
  }

  protected virtual void SetTab()
  {
    int num = this.currentState == TabDepthBtn.STATE.SELECTED ? this.GetDepthOffSet() : this.startDepth;
    string str1 = this.currentState == TabDepthBtn.STATE.SELECTED ? this.GetBtnSelectedSprite() : this.GetBtnUnSelectedSprite();
    string str2 = this.currentState == TabDepthBtn.STATE.SELECTED ? this.GetTabSelectedSprite() : this.GetTabUnSelectedSprite();
    this.m_backGroundSprite.depth = num;
    this.m_buttonObject.normalSprite = str1;
    this.m_buttonObject.SetState(UIButtonColor.State.Normal, true);
    this.m_labelSprite.spriteName = str2;
  }

  public enum STATE
  {
    SELECTED,
    UNSELECTED,
  }
}
