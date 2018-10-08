using UnityEngine;

public class SkillGrowProgress : MonoBehaviour
{
	public UIProgressBar progressBar;

	public UISprite gaugeNormal;

	public UISprite gaugeGrow;

	public UISprite gaugeExceed;

	public void SetGrowMode()
	{
		gaugeGrow.gameObject.SetActive(true);
		gaugeExceed.gameObject.SetActive(false);
		progressBar.foregroundWidget = gaugeGrow;
	}

	public void SetExceedMode()
	{
		gaugeGrow.gameObject.SetActive(false);
		gaugeExceed.gameObject.SetActive(true);
		progressBar.foregroundWidget = gaugeExceed;
	}

	public void SetBaseGauge(bool is_visible, float fill_amount)
	{
		gaugeNormal.enabled = is_visible;
		gaugeNormal.fillAmount = fill_amount;
	}
}
