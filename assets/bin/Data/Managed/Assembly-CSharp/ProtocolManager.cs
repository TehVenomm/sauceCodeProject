// Decompiled with JetBrains decompiler
// Type: ProtocolManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;

#nullable disable
public class ProtocolManager : MonoBehaviourSingleton<ProtocolManager>
{
  private System.Action reserves;

  public bool isReserved => this.reserves != null;

  public void Reserve(System.Action send) => this.reserves += send;

  private void Update()
  {
    if (this.reserves == null || AppMain.isReset)
      return;
    Protocol.Resend((System.Action) (() =>
    {
      System.Action reserves = this.reserves;
      this.reserves = (System.Action) null;
      reserves();
    }));
  }

  public void OnDiff(BaseModelDiff diff)
  {
    if (Utility.IsExist((ICollection) diff.user))
      MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.user[0]);
    if (Utility.IsExist((ICollection) diff.unlockStamps))
      MonoBehaviourSingleton<UserInfoManager>.I.UpdateUnlockStamps(diff.unlockStamps[0]);
    if (Utility.IsExist((ICollection) diff.unlockDegrees))
      MonoBehaviourSingleton<UserInfoManager>.I.UpdateUnlockDegrees(diff.unlockDegrees[0]);
    if (Utility.IsExist((ICollection) diff.selectedDegree))
      MonoBehaviourSingleton<UserInfoManager>.I.UpdateSelectedDegrees(diff.selectedDegree[0]);
    if (Utility.IsExist((ICollection) diff.userClan))
      MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.userClan[0]);
    if (Utility.IsExist((ICollection) diff.status))
    {
      MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.status[0]);
      MonoBehaviourSingleton<PresentManager>.I.OnDiff(diff.status[0]);
      MonoBehaviourSingleton<GatherManager>.I.OnDiff(diff.status[0]);
    }
    if (Utility.IsExist((ICollection) diff.equipSet))
      MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.equipSet[0]);
    if (Utility.IsExist((ICollection) diff.uniqueEquipSet))
      MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.uniqueEquipSet[0]);
    if (Utility.IsExist((ICollection) diff.accessorySet))
      MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.accessorySet[0]);
    if (Utility.IsExist((ICollection) diff.uniqueAccessorySet))
      MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.uniqueAccessorySet[0]);
    if (Utility.IsExist((ICollection) diff.item))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.item[0]);
    if (Utility.IsExist((ICollection) diff.expiredItem))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.expiredItem[0]);
    if (Utility.IsExist((ICollection) diff.equipItem))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.equipItem[0]);
    if (Utility.IsExist((ICollection) diff.skillItem))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.skillItem[0]);
    if (Utility.IsExist((ICollection) diff.abilityItem))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.abilityItem[0]);
    if (Utility.IsExist((ICollection) diff.accessory))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.accessory[0]);
    if (Utility.IsExist((ICollection) diff.skillItemEquipSlot))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.skillItemEquipSlot[0]);
    if (Utility.IsExist((ICollection) diff.skillItemUniqueEquipSlot))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.skillItemUniqueEquipSlot[0]);
    if (Utility.IsExist((ICollection) diff.questItem))
      MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.questItem[0]);
    if (Utility.IsExist((ICollection) diff.clearStatusQuest))
      MonoBehaviourSingleton<QuestManager>.I.OnDiff(diff.clearStatusQuest[0]);
    if (Utility.IsExist((ICollection) diff.clearStatusDelivery))
      MonoBehaviourSingleton<DeliveryManager>.I.OnDiff(diff.clearStatusDelivery[0]);
    if (Utility.IsExist((ICollection) diff.clearStatusQuestEnemySpecies))
      MonoBehaviourSingleton<QuestManager>.I.OnDiff(diff.clearStatusQuestEnemySpecies[0]);
    if (Utility.IsExist((ICollection) diff.delivery))
      MonoBehaviourSingleton<DeliveryManager>.I.OnDiff(diff.delivery[0]);
    if (Utility.IsExist((ICollection) diff.gatherPoint))
      MonoBehaviourSingleton<GatherManager>.I.OnDiff(diff.gatherPoint[0]);
    if (Utility.IsExist((ICollection) diff.blacklist))
      MonoBehaviourSingleton<BlackListManager>.I.OnDiff(diff.blacklist[0]);
    if (Utility.IsExist((ICollection) diff.traveled))
      MonoBehaviourSingleton<WorldMapManager>.I.OnDiff(diff.traveled[0]);
    if (Utility.IsExist((ICollection) diff.portal))
    {
      MonoBehaviourSingleton<WorldMapManager>.I.OnDiff(diff.portal[0]);
      MonoBehaviourSingleton<FieldManager>.I.OnDiff(diff.portal[0]);
    }
    if (Utility.IsExist((ICollection) diff.friend))
      MonoBehaviourSingleton<FriendManager>.I.OnDiff(diff.friend[0]);
    if (Utility.IsExist((ICollection) diff.message))
      MonoBehaviourSingleton<FriendManager>.I.OnDiff(diff.message[0]);
    if (Utility.IsExist((ICollection) diff.boost))
      MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.boost[0]);
    if (Utility.IsExist((ICollection) diff.notice))
      MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.notice[0]);
    if (Utility.IsExist((ICollection) diff.fieldGather))
      MonoBehaviourSingleton<FieldManager>.I.OnDiff(diff.fieldGather[0]);
    if (Utility.IsExist((ICollection) diff.fieldGrowthGather))
      MonoBehaviourSingleton<FieldManager>.I.OnDiff(diff.fieldGrowthGather[0]);
    if (Utility.IsExist((ICollection) diff.achievement))
      MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.achievement[0]);
    if (Utility.IsExist((ICollection) diff.task))
      MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.task[0]);
    if (Utility.IsExist((ICollection) diff.equipCollection))
      MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.equipCollection[0]);
    if (Utility.IsExist((ICollection) diff.constDefine))
      MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.constDefine[0]);
    if (Utility.IsExist((ICollection) diff.visual))
      MonoBehaviourSingleton<GlobalSettingsManager>.I.OnDiff(diff.visual[0]);
    if (!Utility.IsExist((ICollection) diff.userGuildRequest))
      return;
    MonoBehaviourSingleton<GuildRequestManager>.I.OnDiff(diff.userGuildRequest[0]);
  }
}
