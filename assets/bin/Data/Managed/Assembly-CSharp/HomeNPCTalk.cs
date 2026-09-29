// Decompiled with JetBrains decompiler
// Type: HomeNPCTalk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class HomeNPCTalk : GameSection
{
  public int npcID { get; private set; }

  public override void Initialize()
  {
    this.npcID = (int) GameSection.GetEventData();
    base.Initialize();
  }

  public override void UpdateUI() => this.SetFullScreenButton((Enum) HomeNPCTalk.UI.BTN_BACK);

  private enum UI
  {
    BTN_BACK,
  }
}
