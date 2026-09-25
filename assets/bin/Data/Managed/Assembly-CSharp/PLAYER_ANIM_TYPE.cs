// Decompiled with JetBrains decompiler
// Type: PLAYER_ANIM_TYPE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class PLAYER_ANIM_TYPE
{
  public const int STATUS_M = -70;
  public const int STATUS_F = -80;
  public const int INGAME = -1;
  public const int INGAME_0 = 0;
  public const int INGAME_1 = 1;
  public const int INGAME_2 = 2;
  public const int INGAME_4 = 4;
  public const int INGAME_5 = 5;
  public const int STATUS_M_0 = 70;
  public const int STATUS_M_1 = 71;
  public const int STATUS_M_2 = 72;
  public const int STATUS_M_4 = 74;
  public const int STATUS_M_5 = 75;
  public const int STATUS_F_0 = 80 /*0x50*/;
  public const int STATUS_F_1 = 81;
  public const int STATUS_F_2 = 82;
  public const int STATUS_F_4 = 84;
  public const int STATUS_F_5 = 85;
  public const int ROOM = 90;
  public const int RESULT = 91;
  public const int DEBUG = 92;
  public const int CHARA_MAKE = 98;
  public const int HOME = 99;

  public static int GetStatus(int sex) => sex != 0 ? -80 : -70;
}
