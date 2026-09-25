// Decompiled with JetBrains decompiler
// Type: ClanPeople
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanPeople : LoungePeople
{
  protected override IEnumerator Start()
  {
    this.creater = new OutGameCharacterCreater();
    this.charas = new List<HomeCharacterBase>(16 /*0x10*/);
    if (MonoBehaviourSingleton<ClanManager>.IsValid())
      this.loungePlayers = new List<LoungePlayer>(8);
    this.peopleRoot = Utility.CreateGameObject("PeopleRoot", ((Component) this).transform);
    yield return (object) this.StartCoroutine(this.LocateLoungePeople());
    this.isInitialized = true;
    this.isPeopleInitialized = true;
  }

  protected override IEnumerator LocateLoungePeople()
  {
    OutGameSettingsManager.HomeScene.NPC[] npcArray = MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene.npcs;
    for (int index = 0; index < npcArray.Length; ++index)
    {
      OutGameSettingsManager.HomeScene.NPC npc = npcArray[index];
      HomeCharacterBase homeCharacterBase;
      if (string.IsNullOrEmpty(npc.wayPointName))
      {
        homeCharacterBase = this.creater.CreateNPC((IHomePeople) this, this.peopleRoot, npc);
      }
      else
      {
        yield return (object) this.StartCoroutine(this.LoadLoungeWayPoint(npc.wayPointName));
        homeCharacterBase = this.creater.CreateLoungeMoveNPC((IHomePeople) this, this.peopleRoot, this.centerPoint, npc);
      }
      if (Object.op_Inequality((Object) homeCharacterBase, (Object) null))
        this.charas.Add(homeCharacterBase);
      npc = (OutGameSettingsManager.HomeScene.NPC) null;
    }
    npcArray = (OutGameSettingsManager.HomeScene.NPC[]) null;
    while (this.IsLoadingCharacter())
      yield return (object) null;
  }

  public override bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry)
  {
    if (slotInfo == null || slotInfo.userInfo == null)
      return false;
    CharaInfo userInfo = slotInfo.userInfo;
    if (Object.op_Inequality((Object) this.GetLoungePlayer(userInfo.userId), (Object) null))
      return false;
    LoungePlayer loungePlayer = this.loungePlayers.Count <= MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_BASE_MAX_DISP_MEMBER_NUM - 1 ? this.creater.CreateLoungePlayer<LoungePlayer>((IHomePeople) this, (OutGameSettingsManager.LoungeScene) MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene, this.peopleRoot, userInfo, useMovingEntry) : (LoungePlayer) this.creater.CreateLoungePlayer<LoungeLightweightPlayer>((IHomePeople) this, (OutGameSettingsManager.LoungeScene) MonoBehaviourSingleton<OutGameSettingsManager>.I.clanScene, this.peopleRoot, userInfo, useMovingEntry);
    loungePlayer.SetClanChatEvent();
    this.charas.Add((HomeCharacterBase) loungePlayer);
    this.loungePlayers.Add(loungePlayer);
    return true;
  }
}
