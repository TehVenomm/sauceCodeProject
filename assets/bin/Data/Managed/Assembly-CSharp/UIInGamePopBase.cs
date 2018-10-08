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
			if ((Object)button != (Object)null)
			{
				button.normalSprite = buttonSpriteName[0];
			}
			else if ((Object)buttonSprite != (Object)null)
			{
				buttonSprite.spriteName = buttonSpriteName[0];
			}
			icons[0].SetActive(true);
			icons[1].SetActive(false);
		}
		else
		{
			if ((Object)button != (Object)null)
			{
				button.normalSprite = buttonSpriteName[1];
			}
			else if ((Object)buttonSprite != (Object)null)
			{
				buttonSprite.spriteName = buttonSpriteName[1];
			}
			icons[0].SetActive(false);
			icons[1].SetActive(true);
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
			if ((Object)button != (Object)null)
			{
				button.normalSprite = buttonSpriteName[0];
			}
			else if ((Object)buttonSprite != (Object)null)
			{
				buttonSprite.spriteName = buttonSpriteName[0];
			}
			icons[0].SetActive(true);
			icons[1].SetActive(false);
			SoundManager.PlaySystemSE(SoundID.UISE.MENU_OPEN, 1f);
		}
		else
		{
			if ((Object)button != (Object)null)
			{
				button.normalSprite = buttonSpriteName[1];
			}
			else if ((Object)buttonSprite != (Object)null)
			{
				buttonSprite.spriteName = buttonSpriteName[1];
			}
			icons[0].SetActive(false);
			icons[1].SetActive(true);
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
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
