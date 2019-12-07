using Network;
using UnityEngine;

public class HomePlayerCharacter : HomePlayerCharacterBase
{
	private PlayerLoader _playerLoader;

	private PlayerLoader Load(HomePlayerCharacter chara, GameObject go, FriendCharaInfo chara_info, PlayerLoader.OnCompleteLoad callback)
	{
		_playerLoader = go.AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		if (chara_info != null)
		{
			playerLoadInfo.Apply(chara_info, need_weapon: false, need_helm: true, need_leg: true, is_priority_visual_equip: true);
			chara.sexType = chara_info.sex;
		}
		else
		{
			int num = Random.Range(0, 2);
			int face_type_id;
			int hair_style_id;
			if (num == 0)
			{
				int[] defaultHasManFaceIndexes = Singleton<AvatarTable>.I.defaultHasManFaceIndexes;
				face_type_id = defaultHasManFaceIndexes[Random.Range(0, defaultHasManFaceIndexes.Length)];
				int[] defaultHasManHeadIndexes = Singleton<AvatarTable>.I.defaultHasManHeadIndexes;
				hair_style_id = defaultHasManHeadIndexes[Random.Range(0, defaultHasManHeadIndexes.Length)];
			}
			else
			{
				int[] defaultHasWomanFaceIndexes = Singleton<AvatarTable>.I.defaultHasWomanFaceIndexes;
				face_type_id = defaultHasWomanFaceIndexes[Random.Range(0, defaultHasWomanFaceIndexes.Length)];
				int[] defaultHasWomanHeadIndexes = Singleton<AvatarTable>.I.defaultHasWomanHeadIndexes;
				hair_style_id = defaultHasWomanHeadIndexes[Random.Range(0, defaultHasWomanHeadIndexes.Length)];
			}
			int[] defaultHasSkinColorIndexes = Singleton<AvatarTable>.I.defaultHasSkinColorIndexes;
			int skin_color_id = defaultHasSkinColorIndexes[Random.Range(0, defaultHasSkinColorIndexes.Length)];
			int[] defaultHasHairColorIndexes = Singleton<AvatarTable>.I.defaultHasHairColorIndexes;
			int hair_color_id = defaultHasHairColorIndexes[Random.Range(0, defaultHasHairColorIndexes.Length)];
			playerLoadInfo.SetFace(num, face_type_id, skin_color_id);
			playerLoadInfo.SetHair(num, hair_style_id, hair_color_id);
			OutGameSettingsManager.HomeScene.RandomEquip randomEquip = GameSceneGlobalSettings.GetCurrentIHomeManager().GetSceneSetting().randomEquip;
			uint equip_body_item_id = (uint)Utility.Lot(randomEquip.bodys);
			uint equip_head_item_id = (uint)Utility.Lot(randomEquip.helms);
			uint equip_arm_item_id = (uint)Utility.Lot(randomEquip.arms);
			uint equip_leg_item_id = (uint)Utility.Lot(randomEquip.legs);
			playerLoadInfo.SetEquipBody(num, equip_body_item_id);
			if (Random.Range(0, 4) != 0)
			{
				playerLoadInfo.SetEquipHead(num, equip_head_item_id);
			}
			playerLoadInfo.SetEquipArm(num, equip_arm_item_id);
			playerLoadInfo.SetEquipLeg(num, equip_leg_item_id);
			chara.sexType = num;
		}
		_playerLoader.StartLoad(playerLoadInfo, 0, 99, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: true, need_dev_frame_instantiate: true, SHADER_TYPE.NORMAL, callback);
		return _playerLoader;
	}

	protected override ModelLoaderBase LoadModel()
	{
		return Load(this, base.gameObject, charaInfo, null);
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		if (Random.Range(0, 8) == 0)
		{
			animCtrl.SetMoveRunAnim(sexType);
		}
		animator.speed = Random.Range(0.8f, 1.2f);
	}

	public override bool DispatchEvent()
	{
		if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor() != null)
		{
			return false;
		}
		if (HomeBase.OnAfterGacha2Tutorial)
		{
			return false;
		}
		if (GetFriendCharaInfo() != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomePlayerCharacter", base.gameObject, "HOME_FRIENDS", GetFriendCharaInfo());
			return true;
		}
		return false;
	}
}
