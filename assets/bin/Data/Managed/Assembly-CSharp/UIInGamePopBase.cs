using UnityEngine;

public class UIInGamePopBase : MonoBehaviour
{
	[SerializeField]
	protected UITweenCtrl tweenCtrl;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected UIButton button;

	[SerializeField]
	protected UISprite buttonSprite;

	[SerializeField]
	protected string[] buttonSpriteName;

	[SerializeField]
	protected GameObject[] icons;

	protected bool isPopMenu;

	private bool isUnLock;

	private bool isLockReq;

	private float lockTimer;

	protected virtual void Awake()
	{
		if (isPopMenu)
		{
			if (button != null)
			{
				button.normalSprite = buttonSpriteName[0];
			}
			else if (buttonSprite != null)
			{
				buttonSprite.spriteName = buttonSpriteName[0];
			}
			icons[0].SetActive(value: true);
			icons[1].SetActive(value: false);
		}
		else
		{
			if (button != null)
			{
				button.normalSprite = buttonSpriteName[1];
			}
			else if (buttonSprite != null)
			{
				buttonSprite.spriteName = buttonSpriteName[1];
			}
			icons[0].SetActive(value: false);
			icons[1].SetActive(value: true);
		}
	}

	public virtual void OnClickPopMenu()
	{
		tweenCtrl.Reset();
		tweenCtrl.Skip(isPopMenu);
		isPopMenu = !isPopMenu;
		tweenCtrl.Play(isPopMenu, delegate
		{
			isLockReq = true;
			lockTimer = 0.1f;
		});
		if (!isUnLock)
		{
			panelChange.UnLock();
			isUnLock = true;
		}
		isLockReq = false;
		if (isPopMenu)
		{
			if (button != null)
			{
				button.normalSprite = buttonSpriteName[0];
			}
			else if (buttonSprite != null)
			{
				buttonSprite.spriteName = buttonSpriteName[0];
			}
			icons[0].SetActive(value: true);
			icons[1].SetActive(value: false);
			SoundManager.PlaySystemSE(SoundID.UISE.MENU_OPEN);
		}
		else
		{
			if (button != null)
			{
				button.normalSprite = buttonSpriteName[1];
			}
			else if (buttonSprite != null)
			{
				buttonSprite.spriteName = buttonSpriteName[1];
			}
			icons[0].SetActive(value: false);
			icons[1].SetActive(value: true);
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
		}
	}

	protected virtual void LateUpdate()
	{
		if (isLockReq)
		{
			lockTimer -= Time.deltaTime;
			if (!(lockTimer > 0f))
			{
				panelChange.Lock();
				isLockReq = false;
				isUnLock = false;
			}
		}
	}
}
