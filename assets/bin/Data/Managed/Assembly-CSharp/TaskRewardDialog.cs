using System;

public class TaskRewardDialog : GameSection
{
	private enum UI
	{
		LBL_ACHIEVE_NAME,
		LBL_ITEM_NAME
	}

	public override void Initialize()
	{
		TaskTop.TaskData taskData = GameSection.GetEventData() as TaskTop.TaskData;
		SetLabelText((Enum)UI.LBL_ACHIEVE_NAME, taskData.tableData.title);
		SetLabelText((Enum)UI.LBL_ITEM_NAME, taskData.tableData.GetRewardString());
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
