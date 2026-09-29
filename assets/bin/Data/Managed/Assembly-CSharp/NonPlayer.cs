// Decompiled with JetBrains decompiler
// Type: NonPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class NonPlayer : Player
{
  private float npcAtk;

  public int npcId { get; set; }

  public NPCTable.NPCData npcTableData { get; set; }

  public int lv { get; set; }

  public int lv_index { get; set; }

  protected override void Awake() => base.Awake();

  public override void OnSetPlayerStatus(
    int _level,
    int _atk,
    int _def,
    int _hp,
    bool send_packet = true,
    StageObjectManager.PlayerTransferInfo transfer_info = null,
    bool usingRealAtk = false)
  {
    base.OnSetPlayerStatus(_level, _atk, _def, _hp, send_packet, transfer_info);
    if (!usingRealAtk)
    {
      this.npcAtk = (float) _atk;
      this.playerAtk = 0.0f;
    }
    else
      this.npcAtk = this.playerAtk = (float) _atk;
  }

  public override void InitParameter()
  {
    this.ResetStatusParam();
    this.attack.normal = this.npcAtk;
    this.defense.normal = this.playerDef;
    this.defense.fire = this.playerDef;
    this.defense.water = this.playerDef;
    this.defense.thunder = this.playerDef;
    this.defense.soil = this.playerDef;
    this.defense.light = this.playerDef;
    this.defense.dark = this.playerDef;
    this.defenseThreshold = 0;
    this.defenseCoefficient = new AtkAttribute();
    this.hpMax = this.playerHp;
    if (this.hp > this.hpMax)
      this.hp = this.healHp = this.hpMax;
    NpcLevelTable.NpcLevelData npcLevel = Singleton<NpcLevelTable>.I.GetNpcLevel((uint) this.lv, this.lv_index);
    if (npcLevel == null)
      return;
    this.attack.fire = (float) npcLevel.atk_attribute[0];
    this.attack.water = (float) npcLevel.atk_attribute[1];
    this.attack.thunder = (float) npcLevel.atk_attribute[2];
    this.attack.soil = (float) npcLevel.atk_attribute[3];
    this.attack.light = (float) npcLevel.atk_attribute[4];
    this.attack.dark = (float) npcLevel.atk_attribute[5];
    this.tolerance.normal = 0.0f;
    this.tolerance.fire = (float) npcLevel.tolerance[0];
    this.tolerance.water = (float) npcLevel.tolerance[1];
    this.tolerance.thunder = (float) npcLevel.tolerance[2];
    this.tolerance.soil = (float) npcLevel.tolerance[3];
    this.tolerance.light = (float) npcLevel.tolerance[4];
    this.tolerance.dark = (float) npcLevel.tolerance[5];
  }

  public NonPlayer.eNpcAllayState CanGoPray(StageObject client)
  {
    if (this.isDead || this.IsStone() || !this.isNpc || this.controller == null || this.controller.brain == null || this.controller.brain.targetCtrl == null)
      return NonPlayer.eNpcAllayState.CANNOT;
    StageObject allyTarget = this.controller.brain.targetCtrl.GetAllyTarget();
    if (allyTarget == null)
      return NonPlayer.eNpcAllayState.CAN;
    return Object.op_Equality((Object) allyTarget, (Object) client) ? NonPlayer.eNpcAllayState.SAME : NonPlayer.eNpcAllayState.ANOTHER;
  }

  public bool NPCSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
  {
    bool flag = this.ActSkillAction(skill_index);
    SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
    if (actSkillParam == null)
      return false;
    if (flag)
    {
      if (actSkillParam.tableData.type == SKILL_SLOT_TYPE.ATTACK)
        MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      {
        Debug.Log((object) "NPCSkillAction OnUses");
        MonoBehaviourSingleton<InGameProgress>.I.OnSkillUse(this.skillInfo.actSkillParam);
      }
    }
    return flag;
  }

  public void ActPose(bool force_sync = false, bool recieve = false)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 48 /*0x30*/;
    this.PlayMotion(48 /*0x30*/);
  }

  protected override string GetMotionStateName(int motion_id, string _layerName = "")
  {
    if (motion_id - 115 >= 0 && motion_id - 115 < Player.subMotionStateName.Length)
    {
      Character.stateNameBuilder.Length = 0;
      Character.stateNameBuilder.Append(_layerName == "" ? "Base Layer." : _layerName);
      Character.stateNameBuilder.Append(Player.subMotionStateName[motion_id - 115]);
      return Character.stateNameBuilder.ToString();
    }
    return motion_id < 115 ? NonPlayer._GetMotionStateName(motion_id, _layerName) : (string) null;
  }

  private static string _GetMotionStateName(int motion_id, string _layerName)
  {
    string str = string.IsNullOrEmpty(_layerName) ? "Base Layer." : _layerName;
    if (motion_id >= 115)
      return (string) null;
    Character.stateNameBuilder.Length = 0;
    Character.stateNameBuilder.Append(str);
    if (motion_id == 48 /*0x30*/)
      Character.stateNameBuilder.Append(Character.poseStateName);
    else if (motion_id >= 15 && motion_id <= 114)
      Character.stateNameBuilder.AppendFormat(Character.motionStateName[15], (object) (motion_id - 15));
    else
      Character.stateNameBuilder.Append(Character.motionStateName[motion_id]);
    return Character.stateNameBuilder.ToString();
  }

  public enum eNpcAllayState
  {
    CANNOT,
    SAME,
    ANOTHER,
    CAN,
  }
}
