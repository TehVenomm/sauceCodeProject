using UnityEngine;

public class SkillGrowProgress : MonoBehaviour
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
		gaugeGrow.get_gameObject().SetActive(true);
		gaugeExceed.get_gameObject().SetActive(false);
		progressBar.foregroundWidget = gaugeGrow;
	}

	public void SetExceedMode()
	{
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
