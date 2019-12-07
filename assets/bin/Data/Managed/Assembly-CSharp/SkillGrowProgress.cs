using UnityEngine;

public class SkillGrowProgress : MonoBehaviour
{
	public UIProgressBar progressBar;

	public UISprite gaugeNormal;

	public UISprite gaugeGrow;

	public UISprite gaugeExceed;

	public void SetGrowMode()
	{
		gaugeGrow.gameObject.SetActive(value: true);
		gaugeExceed.gameObject.SetActive(value: false);
		progressBar.foregroundWidget = gaugeGrow;
	}

	public void SetExceedMode()
	{
		gaugeGrow.gameObject.SetActive(value: false);
		gaugeExceed.gameObject.SetActive(value: true);
		progressBar.foregroundWidget = gaugeExceed;
	}

	public void SetBaseGauge(bool is_visible, float fill_amount)
	{
		gaugeNormal.enabled = is_visible;
		gaugeNormal.fillAmount = fill_amount;
	}
}
