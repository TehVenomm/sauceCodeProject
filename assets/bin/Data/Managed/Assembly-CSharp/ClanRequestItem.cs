using Network;
using System;
using UnityEngine;

public class ClanRequestItem : UIBehaviour
{
	private enum UI
	{
		LBL_QUEST_INFO,
		LBL_QUEST_NAME,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_TYPE_DAILY_TEXT,
		TEX_NPC,
		SPR_GAUGE,
		LBL_GAUGE,
		SPR_FRAME_CLEAR_BG,
		SPR_FRAME_NOT_CLEAR_BG,
		LBL_GET_PT_NUM,
		SPR_CLEAE_ICON,
		LBL_QUEST_TIME
	}

	public virtual void Setup(Transform t, ClanDelivery info)
	{
		SetNPCIcon(t, info.npcId);
		SetDeliveryName(t, info.name);
		SetDeliveryInfo(t, info.description);
		if (info.isComplete)
		{
			SetActive(t, UI.SPR_FRAME_CLEAR_BG, is_visible: true);
			SetActive(t, UI.SPR_FRAME_NOT_CLEAR_BG, is_visible: false);
			SetActive(t, UI.SPR_CLEAE_ICON, is_visible: true);
		}
		else
		{
			SetActive(t, UI.SPR_FRAME_NOT_CLEAR_BG, is_visible: true);
			SetActive(t, UI.SPR_FRAME_CLEAR_BG, is_visible: false);
			SetActive(t, UI.SPR_CLEAE_ICON, is_visible: false);
		}
		SetDeliveryType(t, info.deliveryType);
		SetClearGauge(t, info.count, info.needCount);
		SetDeliveryPoint(t, info.exp);
		SetTimeLimit(t, info.remainTime);
	}

	protected virtual void SetNPCIcon(Transform t, int npcid)
	{
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(npcid);
		SetNPCIcon(t, UI.TEX_NPC, nPCData.npcModelID);
	}

	protected virtual void SetDeliveryName(Transform t, string name)
	{
		SetLabelText(t, UI.LBL_QUEST_NAME, name);
	}

	protected virtual void SetDeliveryInfo(Transform t, string info)
	{
		SetLabelText(t, UI.LBL_QUEST_INFO, info);
	}

	protected virtual void SetDeliveryType(Transform t, int type)
	{
		SetActive(t, UI.SPR_TYPE_DAILY_TEXT, type == 1);
	}

	protected virtual void SetClearGauge(Transform t, int count, int needCount)
	{
		Transform transform = FindCtrl(t, UI.SPR_GAUGE);
		if (transform != null)
		{
			if (needCount == 0)
			{
				transform.localScale = new Vector3(0f, 1f, 1f);
			}
			else
			{
				transform.localScale = new Vector3(Mathf.Clamp((float)count / (float)needCount, 0f, 1f), 1f, 1f);
			}
		}
		SetLabelText(t, UI.LBL_GAUGE, count.ToString() + "/" + needCount.ToString());
	}

	protected virtual void SetDeliveryPoint(Transform t, int point)
	{
		SetLabelText(t, UI.LBL_GET_PT_NUM, "x" + point.ToString());
	}

	protected virtual void SetTimeLimit(Transform t, int limit)
	{
		TimeSpan span = TimeSpan.FromSeconds(limit);
		SetLabelText(t, UI.LBL_QUEST_TIME, "[Time] left \n" + GetRemainTimeText(span));
	}

	public static string GetRemainTimeText(TimeSpan span)
	{
		string text = "";
		if (span.Seconds > 0)
		{
			span = span.Add(TimeSpan.FromMinutes(1.0));
		}
		if (span.Days > 0)
		{
			return text + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0u), span.Days);
		}
		if (span.Hours > 0)
		{
			return text + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1u), span.Hours);
		}
		if (span.Minutes > 0)
		{
			return text + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), span.Minutes);
		}
		if (text == "")
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), 0);
		}
		return text;
	}
}
