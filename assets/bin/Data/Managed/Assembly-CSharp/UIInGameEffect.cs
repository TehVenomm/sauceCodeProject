using UnityEngine;

public class UIInGameEffect
{
	[SerializeField]
	protected UITweenCtrl tweenCtrl;

	public UIInGameEffect()
		: this()
	{
	}

	private void Start()
	{
		tweenCtrl.Reset();
		tweenCtrl.Play(true, null);
	}

	private void Update()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int num = tweenCtrl.tweens.Length; i < num; i++)
		{
			if (tweenCtrl.tweens[i].style != UITweener.Style.Loop && tweenCtrl.tweens[i].get_isActiveAndEnabled())
			{
				return;
			}
		}
		Object.Destroy(this.get_gameObject());
	}
}
