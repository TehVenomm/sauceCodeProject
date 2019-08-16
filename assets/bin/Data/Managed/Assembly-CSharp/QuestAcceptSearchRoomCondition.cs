using System;
using UnityEngine;

public class QuestAcceptSearchRoomCondition : QuestSearchRoomCondition
{
	public override void Initialize()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		SetActive((Enum)UI.PRIORITY_ROOT, is_visible: true);
		UIWidget component = base.GetComponent<UIWidget>((Enum)UI.OBJ_FRAME);
		if (component != null)
		{
			component.height = 722;
			this.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
			component.UpdateAnchors();
		}
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdatePriorityToggles();
	}

	private void UpdatePriorityToggles()
	{
		SetToggle(GetCtrl(UI.TGL_BTN_FRIEND), searchRequest.isFs == 1);
		SetToggle(GetCtrl(UI.TGL_BTN_CLAN), searchRequest.isCs == 1);
	}
}
