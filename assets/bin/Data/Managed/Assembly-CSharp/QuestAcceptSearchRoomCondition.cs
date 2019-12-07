using UnityEngine;

public class QuestAcceptSearchRoomCondition : QuestSearchRoomCondition
{
	public override void Initialize()
	{
		base.Initialize();
		SetActive(UI.PRIORITY_ROOT, is_visible: true);
		UIWidget component = GetComponent<UIWidget>(UI.OBJ_FRAME);
		if (component != null)
		{
			component.height = 722;
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
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
