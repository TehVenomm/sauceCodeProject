using UnityEngine;

public class UI_LastTutorial : MonoBehaviour
{
	[SerializeField]
	private UIPanel lastTutorialPanel;

	[SerializeField]
	private UISprite lastTutorialSprite;

	[SerializeField]
	private UIButton lastTutorialButton;

	public void OpenLastTutorial()
	{
		lastTutorialPanel.depth = 6500;
		lastTutorialPanel.gameObject.SetActive(true);
		lastTutorialButton.onClick.Clear();
		TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.gameObject, 0.3f, 1f);
		tweenAlpha.AddOnFinished(delegate
		{
			lastTutorialButton.onClick.Add(new EventDelegate(CloseLastTutorial));
		});
	}

	public void CloseLastTutorial()
	{
		TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.gameObject, 0.3f, 0f);
		tweenAlpha.AddOnFinished(delegate
		{
			if (!AppMain.isApplicationQuit)
			{
				Object.Destroy(base.gameObject);
			}
		});
	}
}
