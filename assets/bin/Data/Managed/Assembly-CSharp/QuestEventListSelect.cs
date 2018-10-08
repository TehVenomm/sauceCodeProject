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

	public unsafe override void UpdateUI()
	{
		SetActive((Enum)UI.BTN_SORT, false);
		if (eventLocation == null || eventLocation.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetGrid(UI.GRD_QUEST, "QuestEventListSelectItem", eventLocation.Length, true, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
