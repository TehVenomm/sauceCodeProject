public class SkillGrowProgress
{
	public UIProgressBar progressBar;

	public UISprite gaugeNormal;

	public UISprite gaugeGrow;

	public UISprite gaugeExceed;

	public SkillGrowProgress()
		: this()
	{
	}

	public void SetGrowMode()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		gaugeGrow.get_gameObject().SetActive(true);
		gaugeExceed.get_gameObject().SetActive(false);
		progressBar.foregroundWidget = gaugeGrow;
	}

	public void SetExceedMode()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		gaugeGrow.get_gameObject().SetActive(false);
		gaugeExceed.get_gameObject().SetActive(true);
		progressBar.foregroundWidget = gaugeExceed;
	}

	public void SetBaseGauge(bool is_visible, float fill_amount)
	{
		gaugeNormal.set_enabled(is_visible);
		gaugeNormal.fillAmount = fill_amount;
	}
}
