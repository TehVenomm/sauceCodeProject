using System.Collections;
using UnityEngine;

public class UIExplorePlayerStatus : MonoBehaviour
{
	[SerializeField]
	private UILabel nameLabel;

	[SerializeField]
	private UIHGauge hpGauge;

	[SerializeField]
	private UISprite weaponIcon;

	[SerializeField]
	private UIStatusIcon statusIcon;

	[SerializeField]
	private float statusIconRotationInterval = 0.78f;

	private float nextStatusIconRotate;

	private int checkStatusType;

	private ExplorePlayerStatus playerStatus;

	private IEnumerator rotateUpdate;

	public UIExplorePlayerStatus()
		: this()
	{
	}

	public void Initialize(ExplorePlayerStatus playerStatus)
	{
		if (this.playerStatus != playerStatus)
		{
			Clear();
			this.playerStatus = playerStatus;
			playerStatus.onUpdateHp += UpdateHp;
			playerStatus.onUpdateWeapon += UpdateWeapon;
			playerStatus.onUpdateBuff += UpdateStatusIcon;
		}
		if (playerStatus.isInitialized)
		{
			OnInitializeStatus();
			return;
		}
		playerStatus.onInitialize += OnInitializeStatus;
		this.get_gameObject().SetActive(false);
	}

	private void Clear()
	{
		if (playerStatus != null)
		{
			playerStatus.onInitialize -= OnInitializeStatus;
			playerStatus.onUpdateHp -= UpdateHp;
			playerStatus.onUpdateWeapon -= UpdateWeapon;
			playerStatus.onUpdateBuff -= UpdateStatusIcon;
			playerStatus = null;
			if (rotateUpdate != null)
			{
				this.StopCoroutine(rotateUpdate);
				rotateUpdate = null;
			}
		}
	}

	private void OnInitializeStatus()
	{
		this.get_gameObject().SetActive(true);
		SetName();
		UpdateHp();
		UpdateWeapon();
		UpdateStatusIcon();
	}

	private void OnDestroy()
	{
		Clear();
	}

	private void SetName()
	{
		nameLabel.text = playerStatus.userName;
	}

	private void UpdateHp()
	{
		hpGauge.SetPercent((float)playerStatus.hp / (float)playerStatus.hpMax);
	}

	private void UpdateWeapon()
	{
		string spriteName = UIWeaponChange.WEAPONICON_PATH[(int)playerStatus.weaponType];
		weaponIcon.spriteName = spriteName;
	}

	private void UpdateStatusIcon()
	{
		if (statusIcon.HasActiveMultipleBuffIcon(playerStatus.buff, playerStatus.extraStatus))
		{
			if (rotateUpdate == null)
			{
				rotateUpdate = RotateUpdateStatusIcon();
				this.StartCoroutine(rotateUpdate);
			}
			return;
		}
		if (rotateUpdate != null)
		{
			this.StopCoroutine(rotateUpdate);
			rotateUpdate = null;
		}
		checkStatusType = 0;
		statusIcon.RotatedUpdateStatusIcon(checkStatusType, playerStatus.buff, playerStatus.extraStatus);
	}

	private IEnumerator RotateUpdateStatusIcon()
	{
		while (true)
		{
			nextStatusIconRotate -= Time.get_deltaTime();
			if (nextStatusIconRotate <= 0f)
			{
				checkStatusType = statusIcon.RotatedUpdateStatusIcon(checkStatusType, playerStatus.buff, playerStatus.extraStatus);
				checkStatusType++;
				nextStatusIconRotate = statusIconRotationInterval;
			}
			yield return null;
		}
	}
}
