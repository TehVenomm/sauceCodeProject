// Decompiled with JetBrains decompiler
// Type: OffLineQuestRoomBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class OffLineQuestRoomBase : GameSection
{
  private QuestTable.QuestTableData questData;
  private Coroutine preDownloadCoroutine;
  private bool goToInGame;
  private OffLineQuestRoomBase.UI[] weaponIcon = new OffLineQuestRoomBase.UI[3]
  {
    OffLineQuestRoomBase.UI.SPR_WEAPON_1,
    OffLineQuestRoomBase.UI.SPR_WEAPON_2,
    OffLineQuestRoomBase.UI.SPR_WEAPON_3
  };
  protected CharaInfo userInfo;
  private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[5]
  {
    "Sword",
    "Brade",
    "Lance",
    "Edge",
    "Arrow"
  };
  private const int ROOM_MEMBER_MAX = 4;
  protected int equipSetNo;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI() => this.UpdateUser();

  protected void UpdateUser()
  {
    this.SetGrid((Enum) OffLineQuestRoomBase.UI.GRD_PLAYER_INFO, "", 1, false, (Func<int, Transform, Transform>) ((i, t) => this.Realizes("QuestRoomUserInfoSelf", t, false)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.UpdateRoomUserInfo(t, i);
      this.SetEvent(t, (Enum) OffLineQuestRoomBase.UI.BTN_NAME_BG, "CHANGE_EQUIP", i);
      this.SetEvent(t, (Enum) OffLineQuestRoomBase.UI.BTN_FRAME, "CHANGE_EQUIP", i);
    }));
  }

  protected virtual void UpdateRoomUserInfo(Transform trans, int index)
  {
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_USER_EMPTY, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_USER_BATTLE, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_USER_READY, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_USER_READY_WAIT, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.OBJ_CHAT, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_WEAPON_1, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_WEAPON_2, false);
    this.SetActive(trans, (Enum) OffLineQuestRoomBase.UI.SPR_WEAPON_3, false);
    QuestRoomUserInfo component = ((Component) trans).GetComponent<QuestRoomUserInfo>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.userInfo = this.GetUserCharaInfo(index);
    if (this.userInfo == null)
    {
      component.LoadModel(index, (CharaInfo) null);
    }
    else
    {
      int weapon_index = 0;
      this.userInfo.equipSet.ForEach((Action<CharaInfo.EquipItem>) (data =>
      {
        if (data == null)
          return;
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) data.eId);
        if (equipItemData == null || !equipItemData.IsWeapon())
          return;
        this.SetActive(trans, (Enum) this.weaponIcon[weapon_index], true);
        int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemData.type);
        this.SetSprite(trans, (Enum) this.weaponIcon[weapon_index], OffLineQuestRoomBase.ITEM_TYPE_ICON_SPRITE_NAME[equipmentTypeIndex]);
        ++weapon_index;
      }));
      component.LoadModel(index, this.userInfo);
      SimpleStatus finalStatus = this.GetUserEquipCalculator().GetFinalStatus(0, (int) this.userInfo.hp, (int) this.userInfo.atk, (int) this.userInfo.def);
      this.SetLabelText(trans, (Enum) OffLineQuestRoomBase.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
      this.SetLabelText(trans, (Enum) OffLineQuestRoomBase.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
      this.SetLabelText(trans, (Enum) OffLineQuestRoomBase.UI.LBL_HP, finalStatus.hp.ToString());
      this.SetLabelText(trans, (Enum) OffLineQuestRoomBase.UI.LBL_NAME, this.userInfo.name);
      this.SetLabelText(trans, (Enum) OffLineQuestRoomBase.UI.LBL_LV, this.userInfo.level.ToString());
    }
  }

  protected virtual CharaInfo GetUserCharaInfo(int setNo)
  {
    this.equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    return MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo().charaInfo;
  }

  protected virtual EquipSetCalculator GetUserEquipCalculator()
  {
    return MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(this.equipSetNo);
  }

  private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
  {
    this.SetActive(root, _enum, is_active);
    if (!is_active)
      return;
    this.ResetTween(root, _enum);
    this.PlayTween(root, _enum, is_input_block: false);
  }

  private enum UI
  {
    GRD_PLAYER_INFO,
    LBL_NAME,
    LBL_LV,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_USER_READY,
    SPR_USER_READY_WAIT,
    SPR_USER_EMPTY,
    SPR_USER_BATTLE,
    BTN_EMO_0,
    BTN_EMO_1,
    BTN_EMO_2,
    SPR_WEAPON_1,
    SPR_WEAPON_2,
    SPR_WEAPON_3,
    BTN_NAME_BG,
    BTN_FRAME,
    OBJ_CHAT,
  }

  protected class ResourceInfo
  {
    public RESOURCE_CATEGORY category;
    public string packageName;

    public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
    {
      this.category = category;
      this.packageName = packageName;
    }
  }
}
