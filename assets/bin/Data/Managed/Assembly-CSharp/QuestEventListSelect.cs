using System;
using System.Collections;
using UnityEngine;

public class QuestEventListSelect : QuestListSelectBase
{
	private EventLocationData[] eventLocation;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv = true;
		while (!is_recv)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.BTN_SORT, false);
		if (eventLocation == null || eventLocation.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetGrid(UI.GRD_QUEST, "QuestEventListSelectItem", eventLocation.Length, true, delegate(int i, Transform t, bool is_recycle)
			{
				SetEvent(t, "SELECT_EVENT", i);
				SetTexture(t, UI.TEX_EVENT_BANNER, null);
				SetLabelText(t, UI.LBL_QUEST_NAME, string.Empty);
				SetLabelText(t, UI.LBL_REMAIN_TIME, eventLocation[i].eventAppearRemain);
				if (eventLocation[i].isPayingLocation)
				{
					SetActive(t, UI.SPR_CRYSTAL, true);
					SetActive(t, UI.SPR_FREE_PLAY, eventLocation[i].isFreePlaying);
					SetLabelText(t, UI.LBL_PAYING_REMAIN, eventLocation[i].eventFreePayingRemain);
				}
				else
				{
					SetActive(t, UI.SPR_CRYSTAL, false);
				}
			});
			base.UpdateUI();
		}
	}

	public void OnQuery_SELECT_EVENT()
	{
		GameSection.SetEventData(new object[1]
		{
			QUEST_TYPE.EVENT
		});
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT_EVENT")
		{
			int num = -1;
			EventLocationData eventLocationData = null;
			if (eventLocationData != null)
			{
				num = Array.IndexOf(eventLocation, eventLocationData);
			}
			if (num == -1)
			{
				MonoBehaviourSingleton<QuestManager>.I.EndHowToGetAutoEvent();
				event_name = "AUTO_TARGET_NONE";
				num = 0;
			}
			return new EventData(event_name, num);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}
}
