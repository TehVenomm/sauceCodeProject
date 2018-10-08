using System;
using System.Text;
using UnityEngine;

public class InGameQuestAcceptInvitation : QuestAcceptInvitation
{
	protected new enum UI
	{
		GRD_QUEST,
		LBL_HOST_NAME,
		LBL_HOST_LV,
		TGL_MEMBER_1,
		TGL_MEMBER_2,
		TGL_MEMBER_3,
		LBL_LV,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		STR_NON_LIST,
		BTN_CONDITION,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		LBL_CONDITION_A,
		LBL_CONDITION_B,
		LBL_CONDITION_DIFFICULTY,
		SPR_CONDITION_DIFFICULTY,
		STR_NO_CONDITION,
		LBL_CONDITION_ENEMY,
		OBJ_NPC_MESSAGE,
		SPR_WINDOW_BASE,
		SPR_ICON_DOUBLE,
		OBJ_SEARCH_INFO_ROOT,
		OBJ_QUEST_INFO_ROOT,
		OBJ_ORDER_QUEST_INFO_ROOT,
		SPR_ORDER_RARITY_FRAME,
		SPR_CHALLENGE_NOT_CLEAR,
		LBL_CHALLENGE_NOT_CLEAR,
		SPR_ICON_DEFENSE_BATTLE,
		LBL_RECRUTING_MEMBERS,
		TBL_QUEST,
		SCR_QUEST,
		LBL_LOUNGE_NAME,
		LBL_LABEL,
		OBJ_SYMBOL,
		TEX_STAMP,
		TGL_L_MEMBER_1,
		TGL_L_MEMBER_2,
		TGL_L_MEMBER_3,
		TGL_L_MEMBER_4,
		TGL_L_MEMBER_5,
		TGL_L_MEMBER_6,
		TGL_L_MEMBER_7,
		TEX_MODEL,
		LBL_NAME,
		LBL_LEVEL,
		SPR_ICON_HOST,
		SPR_ICON_FIRST_MET,
		OBJ_DEGREE_FRAME_ROOT,
		GRD_LIST,
		SPR_BLACKLIST_ICON,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		OBJ_LOUNGE,
		OBJ_FIELD,
		OBJ_QUEST,
		LBL_PLAYING_QUEST,
		LBL_PLAYING_READY,
		LBL_AREA_NAME,
		LBL_GUILD_NAME,
		SPR_EMBLEM_LAYER_1,
		SPR_EMBLEM_LAYER_2,
		SPR_EMBLEM_LAYER_3
	}

	private bool isInActiveRotate;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
	}

	public override void UpdateUI()
	{
		if (isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		isInActiveRotate = false;
		base.UpdateUI();
	}

	public override void OnQuery_SELECT_ROOM()
	{
		if (GameSceneEvent.current != null)
		{
			object userData = GameSceneEvent.current.userData;
			if (userData != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(MonoBehaviourSingleton<PartyManager>.I.partys[(int)userData].partyNumber);
				stringBuilder.Append("_");
				stringBuilder.Append(PartyManager.GenerateToken());
				PlayerPrefs.SetString("im", stringBuilder.ToString());
			}
		}
		GameSceneEvent.Cancel();
		BackToHome();
	}

	protected override void OnQuery_SELECT_LOUNGE()
	{
		int index = (int)GameSection.GetEventData();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges[index].loungeNumber);
		stringBuilder.Append("_");
		stringBuilder.Append(LoungeMatchingManager.GenerateToken());
		PlayerPrefs.SetString("il", stringBuilder.ToString());
		BackToHome();
	}

	private void OnCloseDialog_InGameGuildInvitedJoinDialog()
	{
		RefreshUI();
	}

	private void BackToHome()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			if (QuestManager.IsValidInGame())
			{
				MonoBehaviourSingleton<InGameProgress>.I.InviteInQuest();
			}
			else
			{
				MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
			}
		}
	}

	private unsafe void Reposition(bool isPortrait)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (!(base._transform == null))
		{
			Transform val = Utility.Find(base._transform, "SCR_QUEST");
			if (val != null)
			{
				UIPanel panel = val.GetComponent<UIPanel>();
				if (panel != null)
				{
					if (isPortrait)
					{
						panel.clipRange = new Vector4(0f, 0f, 460f, 500f);
					}
					else
					{
						panel.clipRange = new Vector4(0f, 0f, 460f, 260f);
					}
				}
				UIScrollView component = val.GetComponent<UIScrollView>();
				if (component != null)
				{
					component.ResetPosition();
					AppMain i = MonoBehaviourSingleton<AppMain>.I;
					_003CReposition_003Ec__AnonStorey3A3 _003CReposition_003Ec__AnonStorey3A;
					i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)_003CReposition_003Ec__AnonStorey3A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
			}
			UIScreenRotationHandler[] componentsInChildren = this.get_gameObject().GetComponentsInChildren<UIScreenRotationHandler>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].InvokeRotate();
			}
			UpdateAnchors();
		}
	}

	private void OnScreenRotate(bool isPortrait)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (base.transferUI != null)
		{
			isInActiveRotate = !base.transferUI.get_gameObject().get_activeInHierarchy();
		}
		else if (base.collectUI != null)
		{
			isInActiveRotate = !base.collectUI.get_gameObject().get_activeInHierarchy();
		}
		if (!isInActiveRotate)
		{
			Reposition(isPortrait);
		}
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		base.Exit();
	}
}
