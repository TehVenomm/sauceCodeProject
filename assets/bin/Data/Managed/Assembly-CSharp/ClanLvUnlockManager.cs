// Decompiled with JetBrains decompiler
// Type: ClanLvUnlockManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanLvUnlockManager : MonoBehaviour
{
  public static readonly string CLAN_STAGE_LV1_NAME = "HP007D_01";
  public static readonly string CLAN_STAGE_LV2_NAME = "HP007D_02";
  public static readonly string CLAN_STAGE_LV3_NAME = "HP007D_03";
  private UserClanData m_clanData;

  private void Start()
  {
    this.m_clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData;
    this.UnLockObject();
  }

  private void UnLockObject()
  {
    this.LockItem(ClanLvUnlockManager.ClanUnlockLv.QUEST_BOARD, GameObject.Find("ClanQuestBoard"), GameObject.Find("ClanQuestBoardA"));
    this.LockItem(ClanLvUnlockManager.ClanUnlockLv.NOTICE_BOARD, GameObject.Find("HP007_board01"), GameObject.Find("HP007_board02"), GameObject.Find("HP007_board03"), GameObject.Find("NoticeBoard"), GameObject.Find("NoticeBoardA"));
  }

  private void LockItem(ClanLvUnlockManager.ClanUnlockLv item, params GameObject[] objs)
  {
    if (this.m_clanData == null)
    {
      Debug.LogError((object) "m_clanData is null");
    }
    else
    {
      if ((ClanLvUnlockManager.ClanUnlockLv) this.m_clanData.level >= item)
        return;
      foreach (GameObject gameObject in objs)
      {
        if (!Object.op_Equality((Object) gameObject, (Object) null))
          gameObject.SetActive(false);
      }
    }
  }

  public static string CallGetLoadStageName(int lv)
  {
    if (lv == 0)
      lv = 1;
    string loadStageName = string.Empty;
    List<ClanLvUnlockManager.ClanUnlockLv> clanUnlockLvList = new List<ClanLvUnlockManager.ClanUnlockLv>();
    clanUnlockLvList.Add(ClanLvUnlockManager.ClanUnlockLv.STAGE_LV1);
    clanUnlockLvList.Add(ClanLvUnlockManager.ClanUnlockLv.STAGE_LV2);
    clanUnlockLvList.Add(ClanLvUnlockManager.ClanUnlockLv.STAGE_LV3);
    clanUnlockLvList.RemoveAll((Predicate<ClanLvUnlockManager.ClanUnlockLv>) (s => s > (ClanLvUnlockManager.ClanUnlockLv) lv));
    clanUnlockLvList.Sort((Comparison<ClanLvUnlockManager.ClanUnlockLv>) ((a, b) => b - a));
    switch (clanUnlockLvList[0])
    {
      case ClanLvUnlockManager.ClanUnlockLv.STAGE_LV1:
        loadStageName = ClanLvUnlockManager.CLAN_STAGE_LV1_NAME;
        break;
      case ClanLvUnlockManager.ClanUnlockLv.STAGE_LV2:
        loadStageName = ClanLvUnlockManager.CLAN_STAGE_LV2_NAME;
        break;
      case ClanLvUnlockManager.ClanUnlockLv.STAGE_LV3:
        loadStageName = ClanLvUnlockManager.CLAN_STAGE_LV3_NAME;
        break;
    }
    return loadStageName;
  }

  public enum ClanUnlockLv
  {
    STAGE_LV1 = 1,
    QUEST_BOARD = 2,
    NOTICE_BOARD = 5,
    STAGE_LV2 = 10, // 0x0000000A
    STAGE_LV3 = 15, // 0x0000000F
  }
}
