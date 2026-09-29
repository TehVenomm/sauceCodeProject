// Decompiled with JetBrains decompiler
// Type: UIStaticPanelChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class UIStaticPanelChanger : UIStaticPanelRotateCheck
{
  private int unlockCount;

  public void Lock()
  {
    --this.unlockCount;
    if (this.unlockCount > 0)
      return;
    this.unlockCount = 0;
  }

  public void UnLock()
  {
    ++this.unlockCount;
    if (this.unlockCount <= 0)
      return;
    this.panel.widgetsAreStatic = false;
  }

  protected override void Update()
  {
    if (this.unlockCount > 0)
      return;
    base.Update();
  }
}
