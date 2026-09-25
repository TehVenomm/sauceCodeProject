// Decompiled with JetBrains decompiler
// Type: QuestRoomUserInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestRoomUserInfo : MonoBehaviour
{
  private Transform model;
  private PlayerLoader loader;
  private PlayerAnimCtrl animCtrl;
  private UIRenderTexture renderTexture;
  private CharaInfo userInfo;
  private int userIndex = -1;
  private System.Action animEndCallback;

  public void LoadModel(int index, CharaInfo user_info)
  {
    if (user_info == null)
    {
      if (index <= 3 && index >= 0)
        this.userIndex = index;
      this.DeleteModel();
      this.userIndex = -1;
      this.userInfo = (CharaInfo) null;
    }
    else
    {
      this.userIndex = index;
      this.userInfo = user_info;
      if (index > 3)
        return;
      UITexture componentInChildren = ((Component) this).GetComponentInChildren<UITexture>();
      if (MonoBehaviourSingleton<OutGameSettingsManager>.I.questSelect.isRightDepthForward)
        componentInChildren.depth = index;
      else
        componentInChildren.depth = 3 - index;
      this.renderTexture = UIRenderTexture.Get(componentInChildren);
      this.renderTexture.nearClipPlane = 4f;
      if (this.userInfo.sex == 1 && Object.op_Inequality((Object) this.renderTexture.modelTransform, (Object) null) && (double) MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerFemaleCameraOffsetY != 0.0)
        this.renderTexture.modelTransform.localPosition = new Vector3(this.renderTexture.modelTransform.localPosition.x, this.renderTexture.modelTransform.localPosition.y + MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerFemaleCameraOffsetY, this.renderTexture.modelTransform.localPosition.z);
      this.model = Utility.CreateGameObject("PlayerModel", this.renderTexture.modelTransform);
      this.model.localPosition = new Vector3(0.0f, -1.1f, 8f);
      this.model.eulerAngles = new Vector3(0.0f, 180f, 0.0f);
      this.StartCoroutine(this.Loading());
    }
  }

  private IEnumerator Loading()
  {
    this.renderTexture.enableTexture = false;
    if (this.userInfo != null && this.userIndex >= 0)
    {
      bool flag = this.userInfo.userId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
      foreach (Component component in this.model)
        Object.Destroy((Object) component.gameObject);
      PlayerLoadInfo load_info = new PlayerLoadInfo();
      load_info.Apply(this.userInfo, true, true, true, true);
      bool wait = true;
      this.loader = ((Component) this.model).gameObject.AddComponent<PlayerLoader>();
      this.loader.StartLoad(load_info, this.renderTexture.renderLayer, 90, false, false, false, false, false, false, true, true, SHADER_TYPE.UI, (PlayerLoader.OnCompleteLoad) (o =>
      {
        wait = false;
        float num = this.userInfo.sex == 0 ? MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleMale : MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleFemale;
        ((Component) this.loader).transform.localScale = ((Component) this.loader).transform.localScale.Mul(new Vector3(num, num, num));
      }));
      int voice_id = -1;
      if (!flag)
      {
        voice_id = this.loader.GetVoiceId(ACTION_VOICE_EX_ID.ALLIVE_01);
        LoadingQueue lo_queue = new LoadingQueue((MonoBehaviour) this);
        lo_queue.CacheActionVoice(voice_id);
        while (lo_queue.IsLoading())
          yield return (object) null;
        lo_queue = (LoadingQueue) null;
      }
      while (wait)
        yield return (object) null;
      this.animCtrl = PlayerAnimCtrl.Get(this.loader.animator, PlayerAnimCtrl.battleAnims[load_info.weaponModelID / 1000], on_change: new Action<PlayerAnimCtrl, PLCA>(this.OnAnimChange), on_end: new Action<PlayerAnimCtrl, PLCA>(this.OnAnimEnd));
      this.renderTexture.enableTexture = true;
      if (voice_id > 0)
        SoundManager.PlayActionVoice(voice_id);
    }
  }

  public void PlayAnim(PLCA anim)
  {
    if (Object.op_Equality((Object) this.animCtrl, (Object) null))
      return;
    this.animCtrl.Play(anim);
  }

  private void OnAnimChange(PlayerAnimCtrl anim_ctrl, PLCA anim)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null))
      return;
    bool flag = anim_ctrl.IsPlaying(PlayerAnimCtrl.battleAnims);
    if (Object.op_Inequality((Object) this.loader.wepL, (Object) null))
      ((Component) this.loader.wepL).gameObject.SetActive(flag);
    if (!Object.op_Inequality((Object) this.loader.wepR, (Object) null))
      return;
    ((Component) this.loader.wepR).gameObject.SetActive(flag);
  }

  private void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
  {
    if (this.animEndCallback == null)
      return;
    this.animEndCallback();
  }

  public bool IsAllSameEquip(CharaInfo new_info)
  {
    return (this.userInfo == null || new_info == null || this.userInfo.isEqualAccessory(new_info.accessory)) && this.IsSameEquipItemID(new_info, (QuestRoomUserInfo.TypeCondition) (type => type < EQUIPMENT_TYPE.ARMOR)) && this.IsSameEquipItemID(new_info, (QuestRoomUserInfo.TypeCondition) (type => type == EQUIPMENT_TYPE.ARMOR)) && this.IsSameEquipItemID(new_info, (QuestRoomUserInfo.TypeCondition) (type => type == EQUIPMENT_TYPE.HELM)) && this.IsSameEquipItemID(new_info, (QuestRoomUserInfo.TypeCondition) (type => type == EQUIPMENT_TYPE.ARM)) && this.IsSameEquipItemID(new_info, (QuestRoomUserInfo.TypeCondition) (type => type == EQUIPMENT_TYPE.LEG));
  }

  private int GetEquipItemID(QuestRoomUserInfo.TypeCondition condition, CharaInfo target_info = null)
  {
    if (target_info == null)
      target_info = this.userInfo;
    if (target_info == null || target_info.equipSet == null || target_info.equipSet.Count == 0)
      return 0;
    int temp_id = 0;
    target_info.equipSet.ForEach((Action<CharaInfo.EquipItem>) (equip =>
    {
      if (temp_id != 0 || equip == null || equip.eId == 0)
        return;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equip.eId);
      if (equipItemData == null || !condition(equipItemData.type))
        return;
      temp_id = equip.eId;
    }));
    return temp_id;
  }

  private bool IsSameEquipItemID(CharaInfo new_info, QuestRoomUserInfo.TypeCondition condition)
  {
    return this.GetEquipItemID(condition) == this.GetEquipItemID(condition, new_info);
  }

  private void OnDisable() => this.DeleteModel();

  public void DeleteModel()
  {
    if (AppMain.isApplicationQuit)
      return;
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
    {
      Object.DestroyImmediate((Object) this.renderTexture);
      this.renderTexture = (UIRenderTexture) null;
    }
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.model).gameObject);
    this.model = (Transform) null;
    this.loader = (PlayerLoader) null;
    this.animCtrl = (PlayerAnimCtrl) null;
  }

  public void SetOnEmotion(
    RoomEmotion.OnEmotion on_emotion,
    System.Action anim_end_callback,
    UIChatItem[] target)
  {
    RoomEmotion roomEmotion = ((Component) this).GetComponent<RoomEmotion>();
    if (Object.op_Equality((Object) roomEmotion, (Object) null))
      roomEmotion = ((Component) this).gameObject.AddComponent<RoomEmotion>();
    roomEmotion.SetChatItem(target);
    roomEmotion.SetOnEmotion(on_emotion);
    this.animEndCallback = anim_end_callback;
  }

  private delegate bool TypeCondition(EQUIPMENT_TYPE type);
}
