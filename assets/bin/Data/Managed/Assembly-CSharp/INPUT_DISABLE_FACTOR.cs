// Decompiled with JetBrains decompiler
// Type: INPUT_DISABLE_FACTOR
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Flags]
public enum INPUT_DISABLE_FACTOR
{
  INGAME_MENU = 1,
  INGAME_LOBBY = 2,
  INGAME_EXTRA = 4,
  INGAME_CHAT = 8,
  INGAME_SKILL = 16, // 0x00000010
  INGAME_TUTORIAL = 32, // 0x00000020
  INGAME_EVENT = 64, // 0x00000040
  INGAME_GRAB = 128, // 0x00000080
  INGAME_COMMAND = 256, // 0x00000100
  DEBUG = -2147483648, // 0x80000000
}
