// Decompiled with JetBrains decompiler
// Type: JackpotWinDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class JackpotWinDialog : GameSection
{
  private int pamelaAnimIndex;
  private Dictionary<int, string> pamelaOursAnimList = new Dictionary<int, string>()
  {
    {
      0,
      "HAPPY"
    },
    {
      1,
      "BOOK"
    },
    {
      2,
      "IDLE_01"
    }
  };
  private Dictionary<int, string> pamelaTheirsAnimList = new Dictionary<int, string>()
  {
    {
      0,
      "THINK"
    },
    {
      1,
      "YES"
    },
    {
      2,
      "TALK_01"
    },
    {
      3,
      "IDLE_01"
    }
  };
  private Dictionary<int, string> pamelaActiveList;
  private int dragonAnimIndex;
  private Dictionary<int, string> dragonOursAnimList = new Dictionary<int, string>()
  {
    {
      0,
      "HAPPY"
    },
    {
      1,
      "IDLE_02"
    }
  };
  private Dictionary<int, string> dragonTheirsAnimList = new Dictionary<int, string>()
  {
    {
      0,
      "NO"
    },
    {
      1,
      "IDLE_02"
    }
  };
  private Dictionary<int, string> dragonActiveList;
  private NPCLoader pamelaLoader;
  private NPCLoader dragonLoader;
  private JackportNumber jackportNumber;
  private Transform fireball_;
  private bool isOurs;
  private FortuneWheelManager.JackpotWinData data;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.data = GameSection.GetEventData() as FortuneWheelManager.JackpotWinData;
    if (this.data != null)
    {
      this.isOurs = this.IsOurWin(int.Parse(this.data.userId));
      SoundManager.RequestBGM(this.isOurs ? 191 : 10, false);
      this.pamelaActiveList = this.isOurs ? this.pamelaOursAnimList : this.pamelaTheirsAnimList;
      this.dragonActiveList = this.isOurs ? this.dragonOursAnimList : this.dragonTheirsAnimList;
      this.jackportNumber = ((Component) this.GetCtrl((Enum) JackpotWinDialog.UI.JACKPOT_NUMBER)).GetComponent<JackportNumber>();
      this.UpdateNPC();
      yield return (object) null;
      base.Initialize();
    }
  }

  private void OnDisable() => SoundManager.RequestBGM(13);

  public override void UpdateUI()
  {
    this.SetActive((Enum) JackpotWinDialog.UI.BTN_CLAIM, this.isOurs);
    this.SetActive((Enum) JackpotWinDialog.UI.BTN_CLOSE_NEXT, !this.isOurs);
    this.SetView(this.data.jackpot, this.isOurs, this.data.userName);
  }

  private void SetView(string jackpot, bool isOurs, string hunterWinName = "")
  {
    this.jackportNumber.ShowNumber(jackpot);
    Transform ctrl = this.GetCtrl((Enum) JackpotWinDialog.UI.OBJ_JACKPOT_GROUP);
    this.SetActive(((Component) ctrl).transform, (Enum) JackpotWinDialog.UI.OBJ_OURS, isOurs);
    this.SetActive(((Component) ctrl).transform, (Enum) JackpotWinDialog.UI.OBJ_THEIRS, !isOurs);
    this.SetActive((Enum) JackpotWinDialog.UI.BTN_SHARESCREENSHOT, isOurs);
    if (!isOurs)
    {
      this.SetLabelText(((Component) this.GetCtrl((Enum) JackpotWinDialog.UI.OBJ_THEIRS)).transform, (Enum) JackpotWinDialog.UI.LBL_HUNTER_WIN, string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 3U), (object) hunterWinName));
    }
    else
    {
      this.SetActive(this.GetCtrl((Enum) JackpotWinDialog.UI.OBJ_OURS), (Enum) JackpotWinDialog.UI.SPR_INFO_GRAND, this.data.percentage == 100);
      this.SetActive(this.GetCtrl((Enum) JackpotWinDialog.UI.OBJ_OURS), (Enum) JackpotWinDialog.UI.SPR_INFO, this.data.percentage != 100);
    }
  }

  private bool IsOurWin(int id) => MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == id;

  private void UpdateNPC()
  {
    this.SetRenderNPCModel((Enum) JackpotWinDialog.UI.TEX_NPCMODEL_PAMELA, 0, new Vector3(0.4f, -1.3f, 6.87f), new Vector3(0.0f, -157.64f, 0.0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>) (loader =>
    {
      this.pamelaLoader = loader;
      this.pamelaLoader.GetAnimator().Play(this.pamelaActiveList[this.pamelaAnimIndex]);
      SoundManager.PlayVoice((int) Enum.Parse(typeof (JackpotWinDialog.PamelaVoice), this.pamelaActiveList[this.pamelaAnimIndex]));
    }));
    this.SetRenderNPCModel((Enum) JackpotWinDialog.UI.TEX_NPCMODEL_DRAGON, 6, new Vector3(-0.1f, -2.18f, 12f), new Vector3(0.0f, 169f, 0.0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>) (loader =>
    {
      this.dragonLoader = loader;
      this.dragonLoader.GetAnimator().Play(this.dragonActiveList[this.dragonAnimIndex]);
      SoundManager.PlayVoice(this.isOurs ? 600026 : 600024);
    }));
  }

  private void Update()
  {
    if (this.pamelaActiveList != null && Object.op_Inequality((Object) this.pamelaLoader, (Object) null) && this.pamelaAnimIndex < this.pamelaActiveList.Count)
    {
      AnimatorStateInfo animatorStateInfo = this.pamelaLoader.GetAnimator().GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0)
      {
        ++this.pamelaAnimIndex;
        if (this.pamelaAnimIndex < this.pamelaActiveList.Count)
        {
          if (!this.pamelaActiveList[this.pamelaAnimIndex].Contains("IDLE"))
            SoundManager.PlayOneShotUISE((int) Enum.Parse(typeof (JackpotWinDialog.PamelaVoice), this.pamelaActiveList[this.pamelaAnimIndex]));
          this.pamelaLoader.GetAnimator().Play(this.pamelaActiveList[this.pamelaAnimIndex]);
        }
      }
    }
    if (this.dragonActiveList == null || !Object.op_Inequality((Object) this.dragonLoader, (Object) null) || this.dragonAnimIndex >= this.dragonActiveList.Count)
      return;
    AnimatorStateInfo animatorStateInfo1 = this.dragonLoader.GetAnimator().GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo1).IsName(this.dragonActiveList[this.dragonAnimIndex]))
      return;
    ++this.dragonAnimIndex;
    if (this.dragonAnimIndex >= this.dragonActiveList.Count)
      return;
    this.dragonLoader.GetAnimator().Play(this.dragonActiveList[this.dragonAnimIndex]);
  }

  public void OnQuery_SHARE()
  {
  }

  public void OnQuery_CLOSE() => GameSection.BackSection();

  private enum UI
  {
    TEX_NPCMODEL_PAMELA,
    TEX_NPCMODEL_DRAGON,
    BTN_CLOSE_NEXT,
    BTN_CLAIM,
    OBJ_JACKPOT_GROUP,
    OBJ_OURS,
    OBJ_THEIRS,
    LBL_HUNTER_WIN,
    JACKPOT_NUMBER,
    SPR_INFO,
    SPR_INFO_GRAND,
    BTN_SHARESCREENSHOT,
  }

  private enum PamelaVoice
  {
    THINK = 8,
    YES = 213, // 0x000000D5
    TALK_01 = 219, // 0x000000DB
    BOOK = 223, // 0x000000DF
    HAPPY = 228, // 0x000000E4
  }
}
