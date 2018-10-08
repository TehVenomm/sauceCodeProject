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
			playerLoadInfo.Apply(chara_info, false, true, true, true);
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
			OutGameSettingsManager.HomeScene.RandomEquip randomEquip = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.randomEquip;
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
		_playerLoader.StartLoad(playerLoadInfo, 0, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, callback, true, -1);
		return _playerLoader;
	}

	protected override ModelLoaderBase LoadModel()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		return Load(this, this.get_gameObject(), charaInfo, null);
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		if (Random.Range(0, 8) == 0)
		{
			animCtrl.SetMoveRunAnim(sexType);
		}
		animator.set_speed(Random.Range(0.8f, 1.2f));
	}

	public override bool DispatchEvent()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor(0) != null)
		{
			return false;
		}
		if (HomeBase.OnAfterGacha2Tutorial)
		{
			return false;
		}
		if (GetFriendCharaInfo() != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomePlayerCharacter", this.get_gameObject(), "HOME_FRIENDS", GetFriendCharaInfo(), null, true);
			return true;
		}
		return false;
	}
}
