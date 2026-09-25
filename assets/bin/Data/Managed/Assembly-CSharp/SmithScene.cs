// Decompiled with JetBrains decompiler
// Type: SmithScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithScene : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(true);
    MonoBehaviourSingleton<StatusStageManager>.I.SetDisableSmithCharacterActivate(false);
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<SmithManager>.I.InitSmithData();
    base.Exit();
  }
}
