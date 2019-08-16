using UnityEngine;

public class UIInGameEffect : MonoBehaviour
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
		tweenCtrl.Play();
	}

	private void Update()
	{
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
