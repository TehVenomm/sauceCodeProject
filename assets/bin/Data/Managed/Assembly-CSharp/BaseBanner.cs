public class BaseBanner : GameSection
{
	private enum UI
	{
		BANNER
	}

	public override string overrideBackKeyEvent => "CLOSE";

	public override void UpdateUI()
	{
		ResourceLoad.LoadCommonTexture(FindCtrl(base._transform, UI.BANNER).GetComponent<UITexture>(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
	}
}
