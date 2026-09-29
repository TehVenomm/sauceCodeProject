// Decompiled with JetBrains decompiler
// Type: ExplorePlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ExplorePlayerStatus
{
  public List<int> extraStatus = new List<int>();
  private int weaponEquipmentId;
  private EquipItemTable.EquipItemData weaponEquipItemData;
  private CharaInfo charaInfo;

  public bool isInitialized
  {
    get
    {
      return this.weaponEquipItemData != null && Object.op_Inequality((Object) this.coopClient, (Object) null);
    }
  }

  public int hp { get; private set; }

  public BuffParam buff { get; private set; }

  public EQUIPMENT_TYPE weaponType
  {
    get
    {
      return this.weaponEquipItemData == null ? EQUIPMENT_TYPE.ONE_HAND_SWORD : this.weaponEquipItemData.type;
    }
  }

  public ELEMENT_TYPE weaponElementType
  {
    get
    {
      return this.weaponEquipItemData == null ? ELEMENT_TYPE.MAX : (ELEMENT_TYPE) this.weaponEquipItemData.GetElemAtkType();
    }
  }

  public CoopClient coopClient { get; private set; }

  public int userId => this.charaInfo.userId;

  public string userName => this.charaInfo.name;

  public int hpMax { get; private set; }

  public int givenTotalDamage { get; private set; }

  public bool isSelf { get; private set; }

  public event System.Action onInitialize;

  public event System.Action onUpdateWeapon;

  public event System.Action onUpdateBuff;

  public event System.Action onUpdateHp;

  public ExplorePlayerStatus(CharaInfo charaInfo, bool isSelf)
  {
    this.isSelf = isSelf;
    this.charaInfo = charaInfo;
    int _hp;
    MonoBehaviourSingleton<StatusManager>.I.CalcUserStatusParam(charaInfo, out int _, out int _, out _hp);
    this.hpMax = _hp;
    this.buff = new BuffParam((Character) null);
  }

  public void Activate(CoopClient coopClient)
  {
    this.coopClient = coopClient;
    this.charaInfo = coopClient.userInfo;
  }

  public void Sync(Coop_Model_RoomSyncPlayerStatus status)
  {
    this.UpdatePlayerStatus(status.hp, status.buff, status.wid);
    List<int> extraStatus = this.extraStatus;
    if (status.exst != null)
      this.extraStatus = status.exst;
    if (extraStatus == null || extraStatus.Count != this.extraStatus.Count)
    {
      if (this.onUpdateBuff == null)
        return;
      this.onUpdateBuff();
    }
    else
    {
      for (int index = 0; index < extraStatus.Count; ++index)
      {
        int num = extraStatus[index];
        if (!status.exst.Contains(num))
        {
          if (this.onUpdateBuff == null)
            break;
          this.onUpdateBuff();
          break;
        }
      }
    }
  }

  public void SyncFromPlayer(Player player)
  {
    if (!Object.op_Implicit((Object) player) || !player.isInitialized)
      return;
    this.UpdatePlayerStatus(player.hp, player.buffParam.CreateSyncParam(), player.weaponData.eId);
    List<int> intList = (List<int>) null;
    for (int index = 0; index < UIStatusIcon.NON_BUFF_STATUS.Length; ++index)
    {
      UIStatusIcon.STATUS_TYPE status = UIStatusIcon.NON_BUFF_STATUS[index];
      if (Coop_Model_RoomSyncPlayerStatus.StatusEnabled(player, status))
      {
        if (!this.extraStatus.Contains((int) status))
        {
          if (intList == null)
            intList = new List<int>();
          intList.Add((int) status);
        }
      }
      else if (this.extraStatus.Contains((int) status) && intList == null)
        intList = new List<int>();
    }
    if (intList == null)
      return;
    this.extraStatus = intList;
    if (this.onUpdateBuff == null)
      return;
    this.onUpdateBuff();
  }

  private void UpdatePlayerStatus(
    int hp,
    BuffParam.BuffSyncParam buffSyncParam,
    int weaponEquipmentId)
  {
    if (this.hp != hp)
    {
      this.hp = hp;
      if (this.hp > this.hpMax)
        this.hpMax = hp;
      if (this.onUpdateHp != null)
        this.onUpdateHp();
    }
    if (buffSyncParam != null)
    {
      this.buff.SetSyncParamForExplorePlayerStatus(buffSyncParam);
      if (this.onUpdateBuff != null)
        this.onUpdateBuff();
    }
    if (this.weaponEquipmentId == weaponEquipmentId)
      return;
    this.weaponEquipmentId = weaponEquipmentId;
    int num = this.weaponEquipItemData == null ? 1 : 0;
    this.weaponEquipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) weaponEquipmentId);
    if (num != 0 && this.onInitialize != null)
      this.onInitialize();
    if (this.onUpdateWeapon == null)
      return;
    this.onUpdateWeapon();
  }

  public void SyncTotalDamageToBoss(int total)
  {
    if (total <= this.givenTotalDamage)
      return;
    this.givenTotalDamage = total;
  }

  public InGameRecorder.PlayerRecord CreateInGameRecord(CharaInfo _charaInfo)
  {
    if (_charaInfo != null)
      this.charaInfo = _charaInfo;
    InGameRecorder.PlayerRecord inGameRecord = new InGameRecorder.PlayerRecord();
    inGameRecord.id = this.isSelf ? 0 : this.charaInfo.userId;
    inGameRecord.isNPC = false;
    inGameRecord.isSelf = this.isSelf;
    inGameRecord.charaInfo = this.charaInfo;
    inGameRecord.beforeLevel = (int) this.charaInfo.level;
    inGameRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(this.charaInfo, true, true, true, false);
    inGameRecord.animID = inGameRecord.playerLoadInfo.weaponModelID / 1000;
    inGameRecord.givenTotalDamage = this.givenTotalDamage;
    return inGameRecord;
  }
}
