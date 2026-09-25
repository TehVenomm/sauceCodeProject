// Decompiled with JetBrains decompiler
// Type: SmithMaterialDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithMaterialDetail : ItemDetailTop
{
  private int needNum;

  protected override int? GetNeedNum() => new int?(this.needNum);

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.needNum = (int) eventData[1];
    GameSection.SetEventData(eventData[0]);
    base.Initialize();
  }
}
