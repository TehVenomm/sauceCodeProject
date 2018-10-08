using System;
using System.Collections;
using UnityEngine;

public class UIExplorePlayerStatus
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

	public unsafe void Initialize(ExplorePlayerStatus playerStatus)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (this.playerStatus != playerStatus)
		{
			Clear();
			this.playerStatus = playerStatus;
			playerStatus.onUpdateHp += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			playerStatus.onUpdateWeapon += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			playerStatus.onUpdateBuff += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		if (playerStatus.isInitialized)
		{
			OnInitializeStatus();
		}
		else
		{
			playerStatus.onInitialize += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			this.get_gameObject().SetActive(false);
		}
	}

	private unsafe void Clear()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		if (playerStatus != null)
		{
			playerStatus.onInitialize -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			playerStatus.onUpdateHp -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			playerStatus.onUpdateWeapon -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			playerStatus.onUpdateBuff -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
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
		hpGauge.SetPercent((float)playerStatus.hp / (float)playerStatus.hpMax, true);
	}

	private void UpdateWeapon()
	{
		string spriteName = UIWeaponChange.WEAPONICON_PATH[(int)playerStatus.weaponType];
		weaponIcon.spriteName = spriteName;
	}

	private void UpdateStatusIcon()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (statusIcon.HasActiveMultipleBuffIcon(playerStatus.buff, playerStatus.extraStatus))
		{
			if (rotateUpdate == null)
			{
				rotateUpdate = RotateUpdateStatusIcon();
				this.StartCoroutine(rotateUpdate);
			}
		}
		else
		{
			if (rotateUpdate != null)
			{
				this.StopCoroutine(rotateUpdate);
				rotateUpdate = null;
			}
			checkStatusType = 0;
			statusIcon.RotatedUpdateStatusIcon(checkStatusType, playerStatus.buff, playerStatus.extraStatus);
		}
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
			yield return (object)null;
		}
	}
}
