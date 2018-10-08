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
		SetActive(UI.OBJ_CAPTION_1, false);
		SetActive(UI.OBJ_CAPTION_2, false);
		SetActive(UI.OBJ_CAPTION_3, false);
		SetActive(UI.SPR_BADGE, false);
		SetActive(UI.SPR_NAMEPLATE, false);
		SetActive(UI.OBJ_QUEST_BALLOON, false);
		SetActive(UI.OBJ_EVENT_BALLOON, false);
		SetActive(UI.OBJ_LOUNGE_QUEST_BALLOON, false);
		SetActive(UI.OBJ_BACK_1, false);
		SetActive(UI.OBJ_BACK_2, false);
		SetActive(UI.OBJ_BACK_3, false);
		SetActive(UI.OBJ_LOUNGE_NAMEPLATE, false);
		SetActive(UI.OBJ_CHAT_APPEAL, false);
		SetActive(UI.OBJ_STAMP_APPEAL, false);
	}

	public void AttachBackButton(UIBehaviour target_ui, int button_index)
	{
		UI uI = (UI)(15 + button_index);
		Transform transform = Attach(target_ui, GetCtrl(uI));
		transform.gameObject.name = UI.OBJ_BACK.ToString();
	}

	public void AttachCaption(UIBehaviour target_ui, int button_index, string caption)
	{
		if (!string.IsNullOrEmpty(caption) && button_index != 0)
		{
			UI uI = (UI)(0 + button_index);
			Transform transform = Attach(target_ui, GetCtrl(uI));
			transform.gameObject.name = UI.OBJ_CAPTION.ToString();
			SetLabelText(transform, UI.LBL_CAPTION, caption);
			UITweenCtrl component = transform.gameObject.GetComponent<UITweenCtrl>();
			if ((Object)component != (Object)null)
			{
				component.Reset();
				int i = 0;
				for (int num = component.tweens.Length; i < num; i++)
				{
					component.tweens[i].ResetToBeginning();
				}
				component.Play(true, null);
			}
		}
	}

	public void AttachBadge(UIWidget target_widget, int num, SpriteAlignment align, int offset_x = 5, int offset_y = 5, bool is_scale_normalize = false)
	{
		if (!((Object)target_widget == (Object)null))
		{
			Transform transform = target_widget.transform;
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
			Transform transform2 = FindCtrl(transform, UI.LBL_BADGE);
			if ((Object)transform2 != (Object)null)
			{
				if (text == null)
				{
					Object.DestroyImmediate(FindCtrl(transform, UI.SPR_BADGE).gameObject);
				}
				else
				{
					transform2.GetComponent<UILabel>().text = text;
				}
			}
			else if (text != null)
			{
				Transform transform3 = Attach(target_widget, GetCtrl(UI.SPR_BADGE), align, offset_x, offset_y);
				SetLabelText(transform3, UI.LBL_BADGE, text);
				if (is_scale_normalize)
				{
					Vector3 localScale = MonoBehaviourSingleton<UIManager>.I.uiRootTransform.localScale;
					float x = localScale.x;
					Vector3 lossyScale = transform3.lossyScale;
					float x2 = x / lossyScale.x;
					float y = localScale.y;
					Vector3 lossyScale2 = transform3.lossyScale;
					Vector3 vector2 = transform3.localScale = new Vector3(x2, y / lossyScale2.y);
				}
			}
		}
	}

	public Transform CreateQuestBalloon(BALLOON_TYPE type, Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_QUEST_BALLOON), parent);
		SetActive(root, UI.OBJ_QUEST_BALLOON, true);
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
		SetActive(root, UI.OBJ_EVENT_BALLOON, true);
		SetActive(root, UI.SPR_EVENT_BALLOON, type == EVENT_BALLOON_TYPE.NEW);
		SetActive(root, UI.SPR_EVENT_BALLOON_C, type == EVENT_BALLOON_TYPE.COMPLETABLE);
		Transform transform = null;
		switch (type)
		{
		default:
			return FindCtrl(root, UI.SPR_EVENT_BALLOON);
		case EVENT_BALLOON_TYPE.COMPLETABLE:
			return FindCtrl(root, UI.SPR_EVENT_BALLOON_C);
		}
	}

	public Transform CreatePointShopBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_POINT_SHOP_BALLOON), parent);
		SetActive(root, UI.OBJ_POINT_SHOP_BALLOON, true);
		return FindCtrl(root, UI.SPR_POINT_SHOP_BALLOON);
	}

	public Transform CreateBingoBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_BINGO_BALLOON), parent);
		SetActive(root, UI.OBJ_BINGO_BALLOON, true);
		return FindCtrl(root, UI.SPR_BINGO_BALLOON);
	}

	public Transform CreateExploreBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_EXPLORE_BALLOON), parent);
		SetActive(root, UI.OBJ_EXPLORE_BALLOON, true);
		return FindCtrl(root, UI.SPR_EXPLORE_BALLOON);
	}

	public Transform CreateLoungeQuestBalloon(Transform parent)
	{
		Transform root = Clone(GetCtrl(UI.OBJ_LOUNGE_QUEST_BALLOON), parent);
		SetActive(root, UI.OBJ_LOUNGE_QUEST_BALLOON, true);
		return FindCtrl(root, UI.SPR_LOUNGE_QUEST_BALLOON);
	}

	public Transform CreateNamePlate(string text)
	{
		Transform transform = Clone(GetCtrl(UI.SPR_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
		SetLabelText(transform, UI.LBL_NAMEPLATE, text);
		return transform;
	}

	public Transform CreateLoungeNamePlate(string text)
	{
		Transform transform = Clone(GetCtrl(UI.OBJ_LOUNGE_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
		SetLabelText(transform, UI.LBL_NAMEPLATE, text);
		return transform;
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
		base_ui.gameObject.SetActive(true);
		Transform result = ResourceUtility.Realizes(base_ui.gameObject, parent, -1);
		base_ui.gameObject.SetActive(false);
		return result;
	}

	private Transform Attach(UIBehaviour target_ui, Transform base_ui)
	{
		UIVirtualScreen component = target_ui.collectUI.GetComponent<UIVirtualScreen>();
		if ((Object)component == (Object)null)
		{
			return null;
		}
		Transform transform = Clone(base_ui, component.transform);
		UIWidget component2 = transform.GetComponent<UIWidget>();
		if ((Object)component2 != (Object)null)
		{
			if (component2.leftAnchor != null)
			{
				component2.SetAnchor(component.gameObject);
			}
			else
			{
				component2.SetAnchor((GameObject)null);
			}
		}
		return transform;
	}

	private Transform Attach(UIWidget target_widget, Transform base_ui, SpriteAlignment align, int offset_x, int offset_y)
	{
		if ((Object)target_widget == (Object)null)
		{
			return null;
		}
		Transform transform = Clone(base_ui, target_widget.transform);
		UISprite component = transform.GetComponent<UISprite>();
		int num = target_widget.width >> 1;
		int num2 = target_widget.height >> 1;
		int num3 = component.width >> 1;
		int num4 = component.height >> 1;
		int num5 = num - num3;
		int num6 = -num + num3;
		int num7 = num2 - num4;
		int num8 = -num2 + num4;
		switch (align)
		{
		case SpriteAlignment.TopLeft:
		case SpriteAlignment.LeftCenter:
		case SpriteAlignment.BottomLeft:
			num5 -= num;
			num6 -= num;
			break;
		case SpriteAlignment.TopRight:
		case SpriteAlignment.RightCenter:
		case SpriteAlignment.BottomRight:
			num5 += num;
			num6 += num;
			break;
		}
		switch (align)
		{
		case SpriteAlignment.TopLeft:
		case SpriteAlignment.TopCenter:
		case SpriteAlignment.TopRight:
			num8 += num2;
			num7 += num2;
			break;
		case SpriteAlignment.BottomLeft:
		case SpriteAlignment.BottomCenter:
		case SpriteAlignment.BottomRight:
			num8 -= num2;
			num7 -= num2;
			break;
		}
		num5 += offset_x;
		num6 += offset_x;
		num8 += offset_y;
		num7 += offset_y;
		component.SetAnchor(target_widget.gameObject, num5, num7, num6, num8);
		return transform;
	}
}
