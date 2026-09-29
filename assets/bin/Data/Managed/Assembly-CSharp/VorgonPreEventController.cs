// Decompiled with JetBrains decompiler
// Type: VorgonPreEventController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class VorgonPreEventController : MonoBehaviour
{
  public static readonly int[] NPC_ID_LIST = new int[3]
  {
    991,
    992,
    993
  };
  public static readonly int[] NPC_WEAPON_ID_LIST = new int[3]
  {
    10440010,
    10240000,
    10000000
  };
  private const int ENERGY_BALL_INDEX = 14;
  private Enemy enemy;
  private QuestManager.VorgonQuetType questType;
  private bool finish;
  private bool vorgonBarrierBrokenProc;
  private bool npcStartedTalking;
  private bool npcAnnouncedHint;

  private IEnumerator Start()
  {
    while (Object.op_Equality((Object) this.enemy, (Object) null))
    {
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
        this.enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      yield return (object) null;
    }
    if (MonoBehaviourSingleton<QuestManager>.IsValid())
      this.questType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
    while (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
      yield return (object) null;
    if (this.questType == QuestManager.VorgonQuetType.BATTLE_WITH_VORGON)
      ((Component) this.enemy).GetComponentInChildren<EnemyBrain>().actionCtrl.actions[14].isForceDisable = true;
  }

  private void UpdateWyburnBattle()
  {
    if ((double) this.enemy.hp / (double) this.enemy.hpMax > 0.33000001311302185)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.BattleComplete();
    this.finish = true;
  }

  private void UpdateVorgonBattle()
  {
    if (!this.npcStartedTalking)
    {
      this.npcStartedTalking = true;
      this.StartCoroutine("UpdateNPCTalking");
    }
    if (this.vorgonBarrierBrokenProc && !this.npcAnnouncedHint && !this.enemy.IsValidBarrier)
    {
      this.StopCoroutine("UpdateNPCTalking");
      this.StartCoroutine("UpdateNPCTalkingAfterBreakedBariier");
      this.npcAnnouncedHint = true;
    }
    if ((double) this.enemy.hp / (double) this.enemy.hpMax > 0.5 && (double) MonoBehaviourSingleton<InGameProgress>.I.remaindTime >= 84.0)
      return;
    this.StopCoroutine("UpdateNPCTalking");
    this.StopCoroutine("UpdateNPCTalkingAfterBreakedBariier");
    EnemyBrain componentInChildren = ((Component) this.enemy).GetComponentInChildren<EnemyBrain>();
    for (int index = 0; index < componentInChildren.actionCtrl.actions.Count; ++index)
    {
      componentInChildren.actionCtrl.actions[index].isForceDisable = index == 14;
      this.finish = true;
    }
  }

  private IEnumerator UpdateNPCTalking()
  {
    GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
    yield return (object) new WaitForSeconds(5f);
    Player hound_A = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[0] as Player;
    Player hound_B = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[1] as Player;
    Player beginner = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[2] as Player;
    hound_A.uiPlayerStatusGizmo.SetChatDuration(4f);
    hound_B.uiPlayerStatusGizmo.SetChatDuration(4f);
    beginner.uiPlayerStatusGizmo.SetChatDuration(4f);
    beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_0"));
    yield return (object) new WaitForSeconds(5f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_1"));
    yield return (object) new WaitForSeconds(10f);
    hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_2"));
    yield return (object) new WaitForSeconds(4f);
    hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_3"));
    yield return (object) new WaitForSeconds(6f);
    beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_4"));
    yield return (object) new WaitForSeconds(4f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_5"));
    yield return (object) new WaitForSeconds(10f);
    hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_6"));
    yield return (object) new WaitForSeconds(5f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_7"));
    yield return (object) new WaitForSeconds(4f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_8"));
    yield return (object) new WaitForSeconds(4f);
    if ((int) this.enemy.BarrierHp > 1)
      this.enemy.BarrierHp = (XorInt) 1;
    this.vorgonBarrierBrokenProc = true;
  }

  private IEnumerator UpdateNPCTalkingAfterBreakedBariier()
  {
    GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
    Player hound_A = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[0] as Player;
    Player hound_B = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[1] as Player;
    Player beginner = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[2] as Player;
    hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_9"));
    yield return (object) new WaitForSeconds(4f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_10"));
    yield return (object) new WaitForSeconds(4f);
    beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_11"));
    yield return (object) new WaitForSeconds(20f);
    hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_12"));
    EnemyBrain componentInChildren = ((Component) this.enemy).GetComponentInChildren<EnemyBrain>();
    for (int index = 0; index < componentInChildren.actionCtrl.actions.Count; ++index)
    {
      componentInChildren.actionCtrl.actions[index].isForceDisable = index != 14;
      this.finish = true;
    }
    yield return (object) new WaitForSeconds(5f);
    hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_13"));
    yield return (object) null;
  }

  private void UpdateNPC()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> nonplayerList = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList;
    for (int index = 0; index < nonplayerList.Count; ++index)
    {
      Player player = nonplayerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null))
      {
        int num = (int) ((double) player.hpMax * 0.800000011920929);
        if (player.hp < num)
          player.hp = num;
      }
    }
  }

  private void Update()
  {
    if (this.finish)
      return;
    switch (this.questType)
    {
      case QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN:
        this.UpdateWyburnBattle();
        break;
      case QuestManager.VorgonQuetType.BATTLE_WITH_VORGON:
        this.UpdateNPC();
        this.UpdateVorgonBattle();
        break;
    }
  }
}
