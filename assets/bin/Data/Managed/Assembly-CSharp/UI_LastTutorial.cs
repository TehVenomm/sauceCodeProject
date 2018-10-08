using UnityEngine;

public class UI_LastTutorial
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.get_gameObject(), 0.3f, 0f);
		tweenAlpha.AddOnFinished(delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (!AppMain.isApplicationQuit)
			{
				Object.Destroy(this.get_gameObject());
			}
		});
	}
}
