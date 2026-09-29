// Decompiled with JetBrains decompiler
// Type: GameSectionType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class GameSectionType
{
  public static bool IsDialog(this GAME_SECTION_TYPE type)
  {
    return type == GAME_SECTION_TYPE.DIALOG || type == GAME_SECTION_TYPE.PAGE_DIALOG || type == GAME_SECTION_TYPE.SINGLE_DIALOG || type == GAME_SECTION_TYPE.COMMON_DIALOG;
  }

  public static bool IsSingle(this GAME_SECTION_TYPE type)
  {
    return type == GAME_SECTION_TYPE.SINGLE_DIALOG || type == GAME_SECTION_TYPE.COMMON_DIALOG;
  }
}
