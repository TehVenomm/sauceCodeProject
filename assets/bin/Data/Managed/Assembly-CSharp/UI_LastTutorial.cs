using UnityEngine;

public class UI_LastTutorial : MonoBehaviour
{
	[SerializeField]
	private UIPanel lastTutorialPanel;

	[SerializeField]
	private UISprite lastTutorialSprite;

	[SerializeField]
	private UIButton lastTutorialButton;

	public UI_LastTutorial()
		: this()
	{
	}

	public void OpenLastTutorial()
	{
		lastTutorialPanel.depth = 6500;
		lastTutorialPanel.get_gameObject().SetActive(true);
		lastTutorialButton.onClick.Clear();
		TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.get_gameObject(), 0.3f, 1f);
		tweenAlpha.AddOnFinished(delegate
		{
			lastTutorialButton.onClick.Add(new EventDelegate(CloseLastTutorial));
		});
	}

	public void CloseLastTutorial()
	{
		TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.get_gameObject(), 0.3f, 0f);
		tweenAlpha.AddOnFinished(delegate
		{
			if (!AppMain.isApplicationQuit)
			{
				Object.Destroy(this.get_gameObject());
			}
		});
	}
}
