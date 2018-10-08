using Network;
using System;
using System.Collections;
using UnityEngine;

public class QuestRoomUserInfo : MonoBehaviour
{
	private delegate bool TypeCondition(EQUIPMENT_TYPE type);

	private Transform model;

	private PlayerLoader loader;

	private PlayerAnimCtrl animCtrl;

	private UIRenderTexture renderTexture;

	private CharaInfo userInfo;

	private int userIndex = -1;

	private Action animEndCallback;

	public void LoadModel(int index, CharaInfo user_info)
	{
		if (user_info == null)
		{
			if (index <= 3 && index >= 0)
			{
				userIndex = index;
			}
			DeleteModel();
			userIndex = -1;
			userInfo = null;
		}
		else
		{
			userIndex = index;
			userInfo = user_info;
			if (index <= 3)
			{
				UITexture componentInChildren = GetComponentInChildren<UITexture>();
				if (MonoBehaviourSingleton<OutGameSettingsManager>.I.questSelect.isRightDepthForward)
				{
					componentInChildren.depth = index;
				}
				else
				{
					componentInChildren.depth = 3 - index;
				}
				renderTexture = UIRenderTexture.Get(componentInChildren, -1f, false, -1);
				renderTexture.nearClipPlane = 4f;
				model = Utility.CreateGameObject("PlayerModel", renderTexture.modelTransform, -1);
				model.localPosition = new Vector3(0f, -1.1f, 8f);
				model.eulerAngles = new Vector3(0f, 180f, 0f);
				StartCoroutine(Loading());
			}
		}
	}

	private IEnumerator Loading()
	{
		renderTexture.enableTexture = false;
		if (userInfo != null && userIndex >= 0)
		{
			bool is_owner = userInfo.userId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
			foreach (Transform item in model)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			PlayerLoadInfo load_info = new PlayerLoadInfo();
			load_info.Apply(userInfo, true, true, true, true);
			bool wait = true;
			loader = model.gameObject.AddComponent<PlayerLoader>();
			loader.StartLoad(load_info, renderTexture.renderLayer, 90, false, false, false, false, false, false, true, true, SHADER_TYPE.UI, delegate
			{
				((_003CLoading_003Ec__Iterator120)/*Error near IL_0168: stateMachine*/)._003Cwait_003E__4 = false;
				float num = (((_003CLoading_003Ec__Iterator120)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.userInfo.sex != 0) ? MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleFemale : MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleMale;
				((_003CLoading_003Ec__Iterator120)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.loader.transform.localScale = ((_003CLoading_003Ec__Iterator120)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.loader.transform.localScale.Mul(new Vector3(num, num, num));
			}, true, -1);
			int voice_id = -1;
			if (!is_owner)
			{
				voice_id = loader.GetVoiceId(ACTION_VOICE_EX_ID.ALLIVE_01);
				LoadingQueue lo_queue = new LoadingQueue(this);
				lo_queue.CacheActionVoice(voice_id, null);
				while (lo_queue.IsLoading())
				{
					yield return (object)null;
				}
			}
			while (wait)
			{
				yield return (object)null;
			}
			animCtrl = PlayerAnimCtrl.Get(loader.animator, PlayerAnimCtrl.battleAnims[load_info.weaponModelID / 1000], null, OnAnimChange, OnAnimEnd);
			renderTexture.enableTexture = true;
			if (voice_id > 0)
			{
				SoundManager.PlayActionVoice(voice_id, 1f, 0u, null, null);
			}
		}
	}

	public void PlayAnim(PLCA anim)
	{
		if (!((UnityEngine.Object)animCtrl == (UnityEngine.Object)null))
		{
			animCtrl.Play(anim, false);
		}
	}

	private void OnAnimChange(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (!((UnityEngine.Object)loader == (UnityEngine.Object)null))
		{
			bool active = anim_ctrl.IsPlaying(PlayerAnimCtrl.battleAnims);
			if ((UnityEngine.Object)loader.wepL != (UnityEngine.Object)null)
			{
				loader.wepL.gameObject.SetActive(active);
			}
			if ((UnityEngine.Object)loader.wepR != (UnityEngine.Object)null)
			{
				loader.wepR.gameObject.SetActive(active);
			}
		}
	}

	private void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (animEndCallback != null)
		{
			animEndCallback();
		}
	}

	public bool IsAllSameEquip(CharaInfo new_info)
	{
		if (userInfo != null && new_info != null && !userInfo.isEqualAccessory(new_info.accessory))
		{
			return false;
		}
		if (IsSameEquipItemID(new_info, (EQUIPMENT_TYPE type) => type < EQUIPMENT_TYPE.ARMOR) && IsSameEquipItemID(new_info, (EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARMOR) && IsSameEquipItemID(new_info, (EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.HELM) && IsSameEquipItemID(new_info, (EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.ARM) && IsSameEquipItemID(new_info, (EQUIPMENT_TYPE type) => type == EQUIPMENT_TYPE.LEG))
		{
			return true;
		}
		return false;
	}

	private int GetEquipItemID(TypeCondition condition, CharaInfo target_info = null)
	{
		if (target_info == null)
		{
			target_info = userInfo;
		}
		if (target_info == null)
		{
			return 0;
		}
		if (target_info.equipSet == null || target_info.equipSet.Count == 0)
		{
			return 0;
		}
		int temp_id = 0;
		target_info.equipSet.ForEach(delegate(CharaInfo.EquipItem equip)
		{
			if (temp_id == 0 && equip != null && equip.eId != 0)
			{
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equip.eId);
				if (equipItemData != null && condition(equipItemData.type))
				{
					temp_id = equip.eId;
				}
			}
		});
		return temp_id;
	}

	private bool IsSameEquipItemID(CharaInfo new_info, TypeCondition condition)
	{
		return GetEquipItemID(condition, null) == GetEquipItemID(condition, new_info);
	}

	private void OnDisable()
	{
		DeleteModel();
	}

	public void DeleteModel()
	{
		if (!AppMain.isApplicationQuit)
		{
			if ((UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
			{
				UnityEngine.Object.DestroyImmediate(renderTexture);
				renderTexture = null;
			}
			if ((UnityEngine.Object)model != (UnityEngine.Object)null)
			{
				UnityEngine.Object.Destroy(model.gameObject);
				model = null;
				loader = null;
				animCtrl = null;
			}
		}
	}

	public void SetOnEmotion(RoomEmotion.OnEmotion on_emotion, Action anim_end_callback, UIChatItem[] target)
	{
		RoomEmotion roomEmotion = GetComponent<RoomEmotion>();
		if ((UnityEngine.Object)roomEmotion == (UnityEngine.Object)null)
		{
			roomEmotion = base.gameObject.AddComponent<RoomEmotion>();
		}
		roomEmotion.SetChatItem(target);
		roomEmotion.SetOnEmotion(on_emotion);
		animEndCallback = anim_end_callback;
	}
}
