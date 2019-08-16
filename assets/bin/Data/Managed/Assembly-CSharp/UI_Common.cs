using System;
using UnityEngine;

public class UI_Common : UIBehaviour
{
	private enum UI
	{
		OBJ_CAPTION,
		OBJ_CAPTION_1,
		OBJ_CAPTION_2,
		OBJ_CAPTION_3,
		SPR_BADGE,
		LBL_BADGE,
		SPR_NAMEPLATE,
		LBL_NAMEPLATE,
		OBJ_QUEST_BALLOON,
		SPR_QUEST_BALLOON_N,
		SPR_QUEST_BALLOON_R,
		OBJ_EVENT_BALLOON,
		SPR_EVENT_BALLOON,
		OBJ_BACK,
		LBL_CAPTION,
		OBJ_BACK_1,
		OBJ_BACK_2,
		OBJ_BACK_3,
		SPR_QUEST_BALLOON_E,
		SPR_QUEST_BALLOON_CN,
		SPR_QUEST_BALLOON_CE,
		SPR_QUEST_BALLOON_SC,
		SPR_EVENT_BALLOON_C,
		OBJ_POINT_SHOP_BALLOON,
		SPR_POINT_SHOP_BALLOON,
		OBJ_BINGO_BALLOON,
		SPR_BINGO_BALLOON,
		OBJ_EXPLORE_BALLOON,
		SPR_EXPLORE_BALLOON,
		OBJ_LOUNGE_QUEST_BALLOON,
		SPR_LOUNGE_QUEST_BALLOON,
		OBJ_LOUNGE_NAMEPLATE,
		SPR_LOUNGE_NAMEPLATE,
		OBJ_CHAT_APPEAL,
		OBJ_STAMP_APPEAL
	}

	public enum BALLOON_TYPE
	{
		NEW_NORMAL_L,
		NEW_NORMAL_R,
		NEW_DAILY,
		COMPLETABLE_NORMAL_L,
		COMPLETABLE_DAILY,
		POINT_SHOP,
		NEW_SHADOW_CHALLENGE
	}

	public enum EVENT_BALLOON_TYPE
	{
		NONE,
		NEW,
		COMPLETABLE
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_CAPTION_1, is_visible: false);
		SetActive((Enum)UI.OBJ_CAPTION_2, is_visible: false);
		SetActive((Enum)UI.OBJ_CAPTION_3, is_visible: false);
		SetActive((Enum)UI.SPR_BADGE, is_visible: false);
		SetActive((Enum)UI.SPR_NAMEPLATE, is_visible: false);
		SetActive((Enum)UI.OBJ_QUEST_BALLOON, is_visible: false);
		SetActive((Enum)UI.OBJ_EVENT_BALLOON, is_visible: false);
		SetActive((Enum)UI.OBJ_LOUNGE_QUEST_BALLOON, is_visible: false);
		SetActive((Enum)UI.OBJ_BACK_1, is_visible: false);
		SetActive((Enum)UI.OBJ_BACK_2, is_visible: false);
		SetActive((Enum)UI.OBJ_BACK_3, is_visible: false);
		SetActive((Enum)UI.OBJ_LOUNGE_NAMEPLATE, is_visible: false);
		SetActive((Enum)UI.OBJ_CHAT_APPEAL, is_visible: false);
		SetActive((Enum)UI.OBJ_STAMP_APPEAL, is_visible: false);
	}

	public void AttachBackButton(UIBehaviour target_ui, int button_index)
	{
		UI uI = (UI)(15 + button_index);
		Transform val = Attach(target_ui, GetCtrl(uI));
		val.get_gameObject().set_name(UI.OBJ_BACK.ToString());
	}

	public void AttachCaption(UIBehaviour target_ui, int button_index, string caption)
	{
		if (string.IsNullOrEmpty(caption) || button_index == 0)
		{
			return;
		}
		Transform val = Attach(target_ui, GetCtrl((UI)button_index));
		val.get_gameObject().set_name(UI.OBJ_CAPTION.ToString());
		SetLabelText(val, UI.LBL_CAPTION, caption);
		UITweenCtrl componentInChildren = val.get_gameObject().GetComponentInChildren<UITweenCtrl>();
		if (componentInChildren != null)
		{
			componentInChildren.Reset();
			int i = 0;
			for (int num = componentInChildren.tweens.Length; i < num; i++)
			{
				componentInChildren.tweens[i].ResetToBeginning();
			}
			componentInChildren.Play();
		}
	}

	public void AttachBadge(UIWidget target_widget, int num, SpriteAlignment align, int offset_x = 5, int offset_y = 5, bool is_scale_normalize = false)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		if (target_widget == null)
		{
			return;
		}
		Transform transform = target_widget.get_transform();
		string text = null;
		if (num < 0)
		{
			text = "!";
		}
		else if (num > 99)
		{
			text = "99+";
		}
		else if (num != 0)
		{
			text = num.ToString();
		}
		Transform val = FindCtrl(transform, UI.LBL_BADGE);
		if (val != null)
		{
			if (text == null)
			{
				Object.DestroyImmediate(FindCtrl(transform, UI.SPR_BADGE).get_gameObject());
			}
			else
			{
				val.GetComponent<UILabel>().text = text;
			}
		}
		else if (text != null)
		{
			Transform val2 = Attach(target_widget, GetCtrl(UI.SPR_BADGE), align, offset_x, offset_y);
			SetLabelText(val2, UI.LBL_BADGE, text);
			if (is_scale_normalize)
			{
				Vector3 localScale = MonoBehaviourSingleton<UIManager>.I.uiRootTransform.get_localScale();
				float x = localScale.x;
				Vector3 lossyScale = val2.get_lossyScale();
				float num2 = x / lossyScale.x;
				float y = localScale.y;
				Vector3 lossyScale2 = val2.get_lossyScale();
				Vector3 localScale2 = default(Vector3);
				localScale2._002Ector(num2, y / lossyScale2.y);
				val2.set_localScale(localScale2);
			}
		}
	}

	public Transform CreateQuestBalloon(BALLOON_TYPE type, Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_QUEST_BALLOON), parent);
		SetActive(root, UI.OBJ_QUEST_BALLOON, is_visible: true);
		SetActive(root, UI.SPR_QUEST_BALLOON_N, type == BALLOON_TYPE.NEW_NORMAL_L);
		SetActive(root, UI.SPR_QUEST_BALLOON_R, type == BALLOON_TYPE.NEW_NORMAL_R);
		SetActive(root, UI.SPR_QUEST_BALLOON_E, type == BALLOON_TYPE.NEW_DAILY);
		SetActive(root, UI.SPR_QUEST_BALLOON_CN, type == BALLOON_TYPE.COMPLETABLE_NORMAL_L);
		SetActive(root, UI.SPR_QUEST_BALLOON_CE, type == BALLOON_TYPE.COMPLETABLE_DAILY);
		SetActive(root, UI.SPR_QUEST_BALLOON_SC, type == BALLOON_TYPE.NEW_SHADOW_CHALLENGE);
		switch (type)
		{
		default:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_N);
		case BALLOON_TYPE.NEW_NORMAL_R:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_R);
		case BALLOON_TYPE.NEW_DAILY:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_E);
		case BALLOON_TYPE.COMPLETABLE_NORMAL_L:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_CN);
		case BALLOON_TYPE.COMPLETABLE_DAILY:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_CE);
		case BALLOON_TYPE.NEW_SHADOW_CHALLENGE:
			return FindCtrl(root, UI.SPR_QUEST_BALLOON_SC);
		}
	}

	public Transform CreateEventBalloon(Transform parent, EVENT_BALLOON_TYPE type)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_EVENT_BALLOON), parent);
		SetActive(root, UI.OBJ_EVENT_BALLOON, is_visible: true);
		SetActive(root, UI.SPR_EVENT_BALLOON, type == EVENT_BALLOON_TYPE.NEW);
		SetActive(root, UI.SPR_EVENT_BALLOON_C, type == EVENT_BALLOON_TYPE.COMPLETABLE);
		Transform val = null;
		if (type == EVENT_BALLOON_TYPE.NEW || type != EVENT_BALLOON_TYPE.COMPLETABLE)
		{
			return FindCtrl(root, UI.SPR_EVENT_BALLOON);
		}
		return FindCtrl(root, UI.SPR_EVENT_BALLOON_C);
	}

	public Transform CreatePointShopBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_POINT_SHOP_BALLOON), parent);
		SetActive(root, UI.OBJ_POINT_SHOP_BALLOON, is_visible: true);
		return FindCtrl(root, UI.SPR_POINT_SHOP_BALLOON);
	}

	public Transform CreateBingoBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_BINGO_BALLOON), parent);
		SetActive(root, UI.OBJ_BINGO_BALLOON, is_visible: true);
		return FindCtrl(root, UI.SPR_BINGO_BALLOON);
	}

	public Transform CreateExploreBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_EXPLORE_BALLOON), parent);
		SetActive(root, UI.OBJ_EXPLORE_BALLOON, is_visible: true);
		return FindCtrl(root, UI.SPR_EXPLORE_BALLOON);
	}

	public Transform CreateLoungeQuestBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_LOUNGE_QUEST_BALLOON), parent);
		SetActive(root, UI.OBJ_LOUNGE_QUEST_BALLOON, is_visible: true);
		return FindCtrl(root, UI.SPR_LOUNGE_QUEST_BALLOON);
	}

	public Transform CreateNamePlate(string text)
	{
		Transform val = Clone(GetCtrl(UI.SPR_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
		SetLabelText(val, UI.LBL_NAMEPLATE, text);
		return val;
	}

	public Transform CreateLoungeNamePlate(string text)
	{
		Transform val = Clone(GetCtrl(UI.OBJ_LOUNGE_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
		SetLabelText(val, UI.LBL_NAMEPLATE, text);
		return val;
	}

	public Transform CreateChatAppeal()
	{
		return Clone(GetCtrl(UI.OBJ_CHAT_APPEAL), MonoBehaviourSingleton<UIManager>.I._transform);
	}

	public Transform CreateStampAppeal()
	{
		return Clone(GetCtrl(UI.OBJ_STAMP_APPEAL), MonoBehaviourSingleton<UIManager>.I._transform);
	}

	private Transform Clone(Transform base_ui, Transform parent)
	{
		base_ui.get_gameObject().SetActive(true);
		Transform result = ResourceUtility.Realizes(base_ui.get_gameObject(), parent);
		base_ui.get_gameObject().SetActive(false);
		return result;
	}

	private Transform Attach(UIBehaviour target_ui, Transform base_ui)
	{
		UIVirtualScreen component = target_ui.collectUI.GetComponent<UIVirtualScreen>();
		if (component == null)
		{
			return null;
		}
		Transform val = Clone(base_ui, component.get_transform());
		UIWidget component2 = val.GetComponent<UIWidget>();
		if (component2 != null)
		{
			if (component2.leftAnchor != null)
			{
				if (FixedPanelNGUI.CheckResolutionCanFix())
				{
					if (component2.leftAnchor.target.get_name() == "UI_Root")
					{
						component2.SetAnchor(FixedPanelNGUI.Root);
					}
					else
					{
						component2.SetAnchor(component.get_gameObject());
					}
				}
				else
				{
					component2.SetAnchor(component.get_gameObject());
				}
			}
			else
			{
				((UIRect)component2).SetAnchor(null);
			}
		}
		return val;
	}

	private Transform Attach(UIWidget target_widget, Transform base_ui, SpriteAlignment align, int offset_x, int offset_y)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Invalid comparison between Unknown and I4
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Invalid comparison between Unknown and I4
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Invalid comparison between Unknown and I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Invalid comparison between Unknown and I4
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Invalid comparison between Unknown and I4
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Invalid comparison between Unknown and I4
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Invalid comparison between Unknown and I4
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Invalid comparison between Unknown and I4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Invalid comparison between Unknown and I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Invalid comparison between Unknown and I4
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Invalid comparison between Unknown and I4
		if (target_widget == null)
		{
			return null;
		}
		Transform val = Clone(base_ui, target_widget.get_transform());
		UISprite component = val.GetComponent<UISprite>();
		int num = target_widget.width >> 1;
		int num2 = target_widget.height >> 1;
		int num3 = component.width >> 1;
		int num4 = component.height >> 1;
		int num5 = num - num3;
		int num6 = -num + num3;
		int num7 = num2 - num4;
		int num8 = -num2 + num4;
		if ((int)align == 4 || (int)align == 6 || (int)align == 1)
		{
			num5 -= num;
			num6 -= num;
		}
		else if ((int)align == 5 || (int)align == 8 || (int)align == 3)
		{
			num5 += num;
			num6 += num;
		}
		if ((int)align == 2 || (int)align == 1 || (int)align == 3)
		{
			num8 += num2;
			num7 += num2;
		}
		else if ((int)align == 7 || (int)align == 6 || (int)align == 8)
		{
			num8 -= num2;
			num7 -= num2;
		}
		num5 += offset_x;
		num6 += offset_x;
		num8 += offset_y;
		num7 += offset_y;
		component.SetAnchor(target_widget.get_gameObject(), num5, num7, num6, num8);
		return val;
	}
}
