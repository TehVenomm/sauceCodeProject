// Decompiled with JetBrains decompiler
// Type: HomePlayerCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class HomePlayerCharacter : HomePlayerCharacterBase
{
  private PlayerLoader _playerLoader;

  private PlayerLoader Load(
    HomePlayerCharacter chara,
    GameObject go,
    FriendCharaInfo chara_info,
    PlayerLoader.OnCompleteLoad callback)
  {
    this._playerLoader = go.AddComponent<PlayerLoader>();
    PlayerLoadInfo player_load_info = new PlayerLoadInfo();
    if (chara_info != null)
    {
      player_load_info.Apply((CharaInfo) chara_info, false, true, true, true);
      chara.sexType = chara_info.sex;
    }
    else
    {
      int sex = Random.Range(0, 2);
      int face_type_id;
      int hair_style_id;
      if (sex == 0)
      {
        int[] hasManFaceIndexes = Singleton<AvatarTable>.I.defaultHasManFaceIndexes;
        face_type_id = hasManFaceIndexes[Random.Range(0, hasManFaceIndexes.Length)];
        int[] hasManHeadIndexes = Singleton<AvatarTable>.I.defaultHasManHeadIndexes;
        hair_style_id = hasManHeadIndexes[Random.Range(0, hasManHeadIndexes.Length)];
      }
      else
      {
        int[] womanFaceIndexes = Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes;
        face_type_id = womanFaceIndexes[Random.Range(0, womanFaceIndexes.Length)];
        int[] womanHeadIndexes = Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes;
        hair_style_id = womanHeadIndexes[Random.Range(0, womanHeadIndexes.Length)];
      }
      int[] skinColorIndexes = Singleton<AvatarTable>.I.defaultHasSkinColorIndexes;
      int skin_color_id = skinColorIndexes[Random.Range(0, skinColorIndexes.Length)];
      int[] hairColorIndexes = Singleton<AvatarTable>.I.defaultHasHairColorIndexes;
      int hair_color_id = hairColorIndexes[Random.Range(0, hairColorIndexes.Length)];
      player_load_info.SetFace(sex, face_type_id, skin_color_id);
      player_load_info.SetHair(sex, hair_style_id, hair_color_id);
      OutGameSettingsManager.HomeScene.RandomEquip randomEquip = GameSceneGlobalSettings.GetCurrentIHomeManager().GetSceneSetting().randomEquip;
      uint equip_body_item_id = (uint) Utility.Lot<int>(randomEquip.bodys);
      uint equip_head_item_id = (uint) Utility.Lot<int>(randomEquip.helms);
      uint equip_arm_item_id = (uint) Utility.Lot<int>(randomEquip.arms);
      uint equip_leg_item_id = (uint) Utility.Lot<int>(randomEquip.legs);
      player_load_info.SetEquipBody(sex, equip_body_item_id);
      if (Random.Range(0, 4) != 0)
        player_load_info.SetEquipHead(sex, equip_head_item_id);
      player_load_info.SetEquipArm(sex, equip_arm_item_id);
      player_load_info.SetEquipLeg(sex, equip_leg_item_id);
      chara.sexType = sex;
    }
    this._playerLoader.StartLoad(player_load_info, 0, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, callback);
    return this._playerLoader;
  }

  protected override ModelLoaderBase LoadModel()
  {
    return (ModelLoaderBase) this.Load(this, ((Component) this).gameObject, this.charaInfo, (PlayerLoader.OnCompleteLoad) null);
  }

  protected override void InitAnim()
  {
    base.InitAnim();
    if (Random.Range(0, 8) == 0)
      this.animCtrl.SetMoveRunAnim(this.sexType);
    this.animator.speed = Random.Range(0.8f, 1.2f);
  }

  public override bool DispatchEvent()
  {
    if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null) || HomeBase.OnAfterGacha2Tutorial || this.GetFriendCharaInfo() == null)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (HomePlayerCharacter), ((Component) this).gameObject, "HOME_FRIENDS", (object) this.GetFriendCharaInfo());
    return true;
  }
}
