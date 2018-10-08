using Network;
using System;
using System.Collections;
using UnityEngine;

public class QuestRoomUserInfo
{
	private delegate bool TypeCondition(EQUIPMENT_TYPE type);

	private Transform model;

	private PlayerLoader loader;

	private PlayerAnimCtrl animCtrl;

	private UIRenderTexture renderTexture;

	private CharaInfo userInfo;

	private int userIndex = -1;

	private Action animEndCallback;

	public QuestRoomUserInfo()
		: this()
	{
	}

	public void LoadModel(int index, CharaInfo user_info)
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
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
				UITexture componentInChildren = this.GetComponentInChildren<UITexture>();
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
				model.set_localPosition(new Vector3(0f, -1.1f, 8f));
				model.set_eulerAngles(new Vector3(0f, 180f, 0f));
				this.StartCoroutine(Loading());
			}
		}
	}

	private unsafe IEnumerator Loading()
	{
		renderTexture.enableTexture = false;
		if (userInfo != null && userIndex >= 0)
		{
			bool is_owner = userInfo.userId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
			foreach (Transform item in model)
			{
				Transform t = item;
				Object.Destroy(t.get_gameObject());
			}
			PlayerLoadInfo load_info = new PlayerLoadInfo();
			load_info.Apply(userInfo, true, true, true, true);
			bool wait = true;
			loader = model.get_gameObject().AddComponent<PlayerLoader>();
			loader.StartLoad(load_info, renderTexture.renderLayer, 90, false, false, false, false, false, false, true, true, SHADER_TYPE.UI, delegate
			{
				//IL_004b: Unknown result type (might be due to invalid IL or missing references)
				//IL_005b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_0068: Unknown result type (might be due to invalid IL or missing references)
				//IL_006d: Unknown result type (might be due to invalid IL or missing references)
				((_003CLoading_003Ec__Iterator12C)/*Error near IL_0168: stateMachine*/)._003Cwait_003E__4 = false;
				float num = (((_003CLoading_003Ec__Iterator12C)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.userInfo.sex != 0) ? MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleFemale : MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.playerScaleMale;
				((_003CLoading_003Ec__Iterator12C)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.loader.get_transform().set_localScale(((_003CLoading_003Ec__Iterator12C)/*Error near IL_0168: stateMachine*/)._003C_003Ef__this.loader.get_transform().get_localScale().Mul(new Vector3(num, num, num)));
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
			animCtrl = PlayerAnimCtrl.Get(loader.animator, PlayerAnimCtrl.battleAnims[load_info.weaponModelID / 1000], null, new Action<PlayerAnimCtrl, PLCA>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<PlayerAnimCtrl, PLCA>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			renderTexture.enableTexture = true;
			if (voice_id > 0)
			{
				SoundManager.PlayActionVoice(voice_id, 1f, 0u, null, null);
			}
		}
	}

	public void PlayAnim(PLCA anim)
	{
		if (!(animCtrl == null))
		{
			animCtrl.Play(anim, false);
		}
	}

	private void OnAnimChange(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null))
		{
			bool active = anim_ctrl.IsPlaying(PlayerAnimCtrl.battleAnims);
			if (loader.wepL != null)
			{
				loader.wepL.get_gameObject().SetActive(active);
			}
			if (loader.wepR != null)
			{
				loader.wepR.get_gameObject().SetActive(active);
			}
		}
	}

	private void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (animEndCallback != null)
		{
			animEndCallback.Invoke();
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
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (!AppMain.isApplicationQuit)
		{
			if (renderTexture != null)
			{
				Object.DestroyImmediate(renderTexture);
				renderTexture = null;
			}
			if (model != null)
			{
				Object.Destroy(model.get_gameObject());
				model = null;
				loader = null;
				animCtrl = null;
			}
		}
	}

	public void SetOnEmotion(RoomEmotion.OnEmotion on_emotion, Action anim_end_callback, UIChatItem[] target)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		RoomEmotion roomEmotion = this.GetComponent<RoomEmotion>();
		if (roomEmotion == null)
		{
			roomEmotion = this.get_gameObject().AddComponent<RoomEmotion>();
		}
		roomEmotion.SetChatItem(target);
		roomEmotion.SetOnEmotion(on_emotion);
		animEndCallback = anim_end_callback;
	}
}
