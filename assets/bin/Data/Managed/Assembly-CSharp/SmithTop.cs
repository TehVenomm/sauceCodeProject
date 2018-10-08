public class SmithTop : GameSection
{
	private enum UI
	{
		BTN_EQUIP_CREATE,
		BTN_EQUIP_GROW,
		BTN_SKILL_GROW,
		BTN_TO_STATUS
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	private void OnCloseDialog()
	{
		MonoBehaviourSingleton<SmithManager>.I.InitSmithData();
	}
}
