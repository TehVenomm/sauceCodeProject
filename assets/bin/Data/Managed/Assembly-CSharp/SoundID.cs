// Decompiled with JetBrains decompiler
// Type: SoundID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SoundID : MonoBehaviour
{
  public enum AudioSettinID
  {
    Default,
    IngameBoss,
    IngameField,
    Lounge,
  }

  public enum UISE
  {
    INVALID = -1, // 0xFFFFFFFF
    CLICK = 40000001, // 0x02625A01
    OK = 40000002, // 0x02625A02
    CANCEL = 40000003, // 0x02625A03
    POPUP = 40000004, // 0x02625A04
    MENU_OPEN = 40000005, // 0x02625A05
    DIALOG_GOOD = 40000006, // 0x02625A06
    DIALOG_COMMON = 40000007, // 0x02625A07
    SELECT1 = 40000009, // 0x02625A09
    DIALOG_IMPTNT = 40000010, // 0x02625A0A
    GET_PRIZE = 40000018, // 0x02625A12
    CHAT_BALOON = 40000022, // 0x02625A16
    POP_QUEST = 40000123, // 0x02625A7B
  }

  public enum BGM
  {
    INVALID = -1, // 0xFFFFFFFF
    NONE = 0,
    TITLE = 1,
    HOME = 2,
    QUEST_LIST = 3,
    RESULT_WIN = 4,
    THEME_SERIOUS = 5,
    MYHOUSE = 6,
    GACHA_TOP = 7,
    GACHA_QUEST = 8,
    GACHA_MAGI = 9,
    RESULT_LOSE = 10, // 0x0000000A
    TITLE_LOGO = 11, // 0x0000000B
    BOSS_WARNING = 12, // 0x0000000C
    OPENING = 13, // 0x0000000D
    DEBRIEFING = 14, // 0x0000000E
    GATHER = 108, // 0x0000006C
    FIELD_NORMAL = 112, // 0x00000070
    FIELD_MYSTERIOUS = 113, // 0x00000071
    FIELD_PASSIONATE = 114, // 0x00000072
    LOUNGE = 153, // 0x00000099
    JACKPOT_WIN = 191, // 0x000000BF
  }

  public enum ConfigID
  {
    RESULT_COUNTER = 90000001, // 0x055D4A81
  }
}
