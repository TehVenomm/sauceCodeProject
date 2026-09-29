// Decompiled with JetBrains decompiler
// Type: TradingPostLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TradingPostLog : GameSection
{
  private void OnQuery_HELP()
  {
    GameSection.ChangeEvent("[BACK]");
    this.RequestEvent("HELP");
  }

  private void OnQuery_HISTORY()
  {
    GameSection.ChangeEvent("[BACK]");
    this.RequestEvent("HISTORY");
  }

  private void OnQuery_ACTIVE()
  {
    GameSection.ChangeEvent("[BACK]");
    this.RequestEvent("ACTIVE");
  }

  private void OnQuery_INVENTORY()
  {
    GameSection.ChangeEvent("[BACK]");
    this.RequestEvent("TO_STORAGE");
  }
}
