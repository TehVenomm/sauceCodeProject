public class BaseBanner : GameSection
{
	private enum UI
	{
		BANNER
	}

	public override string overrideBackKeyEvent => "CLOSE";

	public override void UpdateUI()
	{
		UITexture component = FindCtrl(base._transform, UI.BANNER).GetComponent<UITexture>();
		ResourceLoad.LoadCommonTexture(component, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
	}
}
