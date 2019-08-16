using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStatusGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected Rigidbody2D rigidbody;

	[SerializeField]
	protected GameObject nearUI;

	[SerializeField]
	protected UIHGauge gaugeUI;

	[SerializeField]
	protected UIHGauge healGaugeUI;

	[SerializeField]
	protected UIHGauge shieldGaugeUI;

	[SerializeField]
	protected UILabel nameLabel;

	[SerializeField]
	protected GameObject farUI;

	[SerializeField]
	protected GameObject arrowUI;

	[SerializeField]
	protected UILabel distanceLabel;

	[SerializeField]
	protected UISprite vitalSprite;

	[SerializeField]
	protected UISprite hostEffect;

	[SerializeField]
	protected GameObject chatUI;

	[SerializeField]
	protected UILabel chatLabel;

	[SerializeField]
	protected TweenScale chatTween;

	[SerializeField]
	protected GameObject chatStampUI;

	[SerializeField]
	protected UITexture chatStampTexture;

	[SerializeField]
	protected TweenScale chatStampTween;

	[SerializeField]
	protected UIHGauge prayerGauge;

	[SerializeField]
	protected UIHGauge prayerGaugeAdd;

	[SerializeField]
	protected UILabel prayerGaugeText;

	[SerializeField]
	protected GameObject nowPrayer;

	[SerializeField]
	protected UISprite prayerGaugeSprite;

	[SerializeField]
	protected UISprite prayerGaugeAddSprite;

	[SerializeField]
	protected Color prayerGaugeColor1;

	[SerializeField]
	protected Color prayerGaugeColor2;

	[SerializeField]
	protected Color prayerGaugeColor3;

	[SerializeField]
	[Tooltip("自キャラ表示時間")]
	protected float selfShowTime = 5f;

	[SerializeField]
	[Tooltip("矢印横表示時のXオフセット")]
	protected float arrowSideOffset = 25f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン横オフセット")]
	protected float screenSideOffset = 36f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 107f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット、フィ\u30fcルド時")]
	protected float screenBottomFieldOffset = 107f;

	[SerializeField]
	[Tooltip("チャット横オフセット")]
	protected float chatSideOffset = 60f;

	[SerializeField]
	[Tooltip("チャット上オフセット")]
	protected float chatTopOffset = 120f;

	[SerializeField]
	[Tooltip("チャットスタンプ横オフセット")]
	protected float chatStampSideOffset = 60f;

	[SerializeField]
	[Tooltip("チャットスタンプ上オフセット")]
	protected float chatStampTopOffset = 120f;

	[SerializeField]
	protected UISprite friendIcon;

	[SerializeField]
	protected float nameLabelOffsetWithFriendIconX;

	[SerializeField]
	protected GameObject emotionUI;

	[SerializeField]
	protected TweenScale emotionTweenS;

	[SerializeField]
	protected TweenAlpha emotionTweenA;

	private Player _targetPlayer;

	protected float damagedTimer = -1f;

	protected Vector3 chatUILocalPos = Vector3.get_zero();

	protected Transform chatTransform;

	protected Vector3 chatStampUILocalPos = Vector3.get_zero();

	protected Transform chatStampTransform;

	protected Vector3 emotionUILocalPos = Vector3.get_zero();

	protected Transform emotionTransform;

	protected Transform arrowTransform;

	private int currentUserId = -1;

	public Player targetPlayer
	{
		get
		{
			return _targetPlayer;
		}
		set
		{
			_targetPlayer = value;
			if (_targetPlayer != null)
			{
				this.get_gameObject().SetActive(true);
				currentUserId = -1;
				UpdateParam();
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	public bool isVisible
	{
		get;
		protected set;
	}

	public UIPlayerStatusGizmo()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		isVisible = true;
	}

	protected override void OnEnable()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		base.OnEnable();
		if (chatUI != null)
		{
			chatTransform = chatUI.get_transform();
			chatUILocalPos = chatTransform.get_localPosition();
			chatUI.SetActive(false);
		}
		if (chatStampUI != null)
		{
			chatStampTransform = chatStampUI.get_transform();
			chatStampUILocalPos = chatStampTransform.get_localPosition();
			chatStampUI.SetActive(false);
		}
		if (emotionUI != null)
		{
			emotionTransform = emotionUI.get_transform();
			emotionUILocalPos = emotionTransform.get_localPosition();
			emotionUI.SetActive(false);
		}
		if (chatTween != null)
		{
			chatTween.SetOnFinished(OnFinishChat);
		}
		if (chatStampTween != null)
		{
			chatStampTween.SetOnFinished(OnFinishedChatStamp);
		}
		if (prayerGauge != null)
		{
			prayerGauge.get_gameObject().SetActive(false);
		}
		if (prayerGaugeAdd != null)
		{
			prayerGaugeAdd.get_gameObject().SetActive(false);
		}
		nearUI.SetActive(false);
		farUI.SetActive(false);
		if (arrowUI != null)
		{
			arrowTransform = arrowUI.get_transform();
			arrowUI.SetActive(false);
		}
		if (friendIcon != null)
		{
			friendIcon.get_gameObject().SetActive(false);
		}
		if (hostEffect != null)
		{
			hostEffect.set_enabled(false);
		}
	}

	protected override void UpdateParam()
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		if (targetPlayer == null || !targetPlayer.get_gameObject().get_activeSelf() || targetPlayer.isLoading || (!MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && targetPlayer.isStopCounter) || (!targetPlayer.isCoopInitialized && targetPlayer.IsPuppet()) || !isVisible)
		{
			SetActiveSafe(nearUI, active: false);
			SetActiveSafe(farUI, active: false);
			if (arrowUI != null)
			{
				SetActiveSafe(arrowUI, active: false);
			}
			if (prayerGauge != null)
			{
				SetActiveSafe(prayerGauge.get_gameObject(), active: false);
			}
			return;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			isHostPlayer = (MonoBehaviourSingleton<CoopManager>.I.GetPartyOwnerPlayerID() == targetPlayer.id);
		}
		Vector3 position = targetPlayer._position;
		position.y += targetPlayer.playerParameter.uiHeight;
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, position);
		screenZ = screenUIPosition.z;
		screenUIPosition.z = 0f;
		float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyPlayerStatusGizmo)
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenSideOffsetPortrait;
				screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenBottomOffsetPortrait;
			}
			else
			{
				screenSideOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenSideOffsetLandScape;
				screenBottomOffset = SpecialDeviceManager.SpecialDeviceInfo.UIPlayerStatusGizmoScreenBottomOffsetLandScape;
			}
		}
		Vector3 val = screenUIPosition;
		bool flag = false;
		float num2 = Screen.get_width();
		float num3 = Screen.get_height();
		if (screenUIPosition.x < screenSideOffset * num)
		{
			screenUIPosition.x = screenSideOffset * num;
			flag = true;
		}
		else if (screenUIPosition.x > num2 - screenSideOffset * num)
		{
			screenUIPosition.x = num2 - screenSideOffset * num;
			flag = true;
		}
		float num4 = screenBottomOffset;
		if (FieldManager.IsValidInGameNoQuest())
		{
			num4 = screenBottomFieldOffset;
		}
		if (screenUIPosition.y < num4 * num)
		{
			screenUIPosition.y = num4 * num;
			flag = true;
		}
		Vector3 val2 = screenUIPosition;
		if (chatUI.get_activeSelf())
		{
			if (val2.x < chatSideOffset * num)
			{
				val2.x = chatSideOffset * num;
			}
			else if (val2.x > num2 - chatSideOffset * num)
			{
				val2.x = num2 - chatSideOffset * num;
			}
			if (val2.y > num3 - chatTopOffset * num)
			{
				val2.y = num3 - chatTopOffset * num;
			}
		}
		Vector3 val3 = screenUIPosition;
		if (chatStampUI.get_activeSelf() || emotionUI.get_activeSelf())
		{
			if (val3.x < chatStampSideOffset * num)
			{
				val3.x = chatStampSideOffset * num;
			}
			else if (val3.x > num2 - chatStampSideOffset * num)
			{
				val3.x = num2 - chatStampSideOffset * num;
			}
			if (val3.y > num3 - chatStampTopOffset * num)
			{
				val3.y = num3 - chatStampTopOffset * num;
			}
		}
		Vector3 val4 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
		Vector3 val5 = val4;
		Vector3 val6 = val4;
		if (chatUI.get_activeSelf())
		{
			val5 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val2);
		}
		if (chatStampUI.get_activeSelf() || emotionUI.get_activeSelf())
		{
			val6 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val3);
		}
		Vector3 val7 = transform.get_position() - val4;
		if (val7.get_sqrMagnitude() >= 2E-05f)
		{
			transform.set_position(val4);
		}
		if (chatUI.get_activeSelf())
		{
			Matrix4x4 worldToLocalMatrix = transform.get_worldToLocalMatrix();
			Vector3 localPosition = worldToLocalMatrix.MultiplyPoint3x4(val5);
			localPosition.y += chatUILocalPos.y;
			localPosition.z = 0f;
			chatTransform.set_localPosition(localPosition);
		}
		if (chatStampUI.get_activeSelf() || emotionUI.get_activeSelf())
		{
			Matrix4x4 worldToLocalMatrix2 = transform.get_worldToLocalMatrix();
			Vector3 localPosition2 = worldToLocalMatrix2.MultiplyPoint3x4(val6);
			localPosition2.y += chatUILocalPos.y;
			localPosition2.z = 0f;
			chatStampTransform.set_localPosition(localPosition2);
			emotionTransform.set_localPosition(localPosition2);
		}
		if (prayerGauge != null)
		{
			if (targetPlayer.revivalTimePercent > 0f)
			{
				SetActiveSafe(prayerGauge.get_gameObject(), active: true);
				prayerGauge.SetPercent(targetPlayer.revivalTimePercent);
				if (nowPrayer != null)
				{
					SetActiveSafe(nowPrayer.get_gameObject(), targetPlayer.IsPrayed());
				}
				if (targetPlayer.IsPrayed())
				{
					if (targetPlayer.isDead)
					{
						prayerGaugeText.text = string.Format(StringTable.Get(STRING_CATEGORY.IN_GAME, 1011u));
					}
					else if (targetPlayer.IsStone())
					{
						prayerGaugeText.text = string.Format(StringTable.Get(STRING_CATEGORY.IN_GAME, 1012u));
					}
					SetActiveSafe(prayerGaugeAdd.get_gameObject(), active: true);
					prayerGaugeAdd.SetPercent(targetPlayer.revivalTimePercent);
					switch (targetPlayer.prayerIds.Count)
					{
					default:
						SetUISpriteColor(prayerGaugeSprite, prayerGaugeColor1);
						SetUISpriteColor(prayerGaugeAddSprite, prayerGaugeColor1);
						break;
					case 2:
						SetUISpriteColor(prayerGaugeSprite, prayerGaugeColor2);
						SetUISpriteColor(prayerGaugeAddSprite, prayerGaugeColor2);
						break;
					case 3:
						SetUISpriteColor(prayerGaugeSprite, prayerGaugeColor3);
						SetUISpriteColor(prayerGaugeAddSprite, prayerGaugeColor3);
						break;
					}
				}
				else
				{
					SetActiveSafe(prayerGaugeAdd.get_gameObject(), active: false);
				}
			}
			else
			{
				SetActiveSafe(prayerGauge.get_gameObject(), active: false);
			}
		}
		Self self = targetPlayer as Self;
		if (self != null)
		{
			bool flag2 = false;
			if (damagedTimer >= 0f)
			{
				if (Time.get_time() - damagedTimer >= selfShowTime)
				{
					damagedTimer = -1f;
				}
				else
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				SetActiveSafe(nearUI, active: false);
				SetActiveSafe(farUI, active: false);
				return;
			}
		}
		if ((flag && FieldManager.IsValidInGameNoQuest()) || !GameSaveData.instance.headName)
		{
			SetActiveSafe(nearUI, active: false);
			SetActiveSafe(farUI, active: false);
			return;
		}
		if (targetPlayer.rescueTime > 0f || targetPlayer.stoneRescueTime > 0f)
		{
			flag = true;
		}
		if (flag && self == null)
		{
			SetActiveSafe(nearUI, active: false);
			SetActiveSafe(farUI, active: true);
			if (arrowUI != null)
			{
				Vector3 val8 = val - screenUIPosition;
				if (val8 != Vector3.get_zero())
				{
					float num5 = 90f - Vector3.Angle(Vector3.get_right(), val8);
					arrowTransform.set_eulerAngles(new Vector3(0f, 0f, num5));
					SetActiveSafe(arrowUI, active: true);
					Vector3 localPosition3 = default(Vector3);
					localPosition3._002Ector(0f, 0f, 0f);
					float num6 = Mathf.Sin(num5 * ((float)Math.PI / 180f));
					if (num6 > 0.01f)
					{
						localPosition3.x = arrowSideOffset;
					}
					else if (num6 < -0.01f)
					{
						localPosition3.x = 0f - arrowSideOffset;
					}
					arrowTransform.set_localPosition(localPosition3);
				}
				else
				{
					SetActiveSafe(arrowUI, active: false);
				}
			}
			if (distanceLabel != null)
			{
				if (targetPlayer.rescueTime > 0f)
				{
					distanceLabel.text = string.Empty + Mathf.CeilToInt(targetPlayer.rescueTime) + "\u3000";
				}
				else if (targetPlayer.stoneRescueTime > 0f)
				{
					distanceLabel.text = string.Empty + Mathf.CeilToInt(targetPlayer.stoneRescueTime) + " ";
				}
				else if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
				{
					Vector3 val9 = targetPlayer._position - MonoBehaviourSingleton<StageObjectManager>.I.self._position;
					int num7 = (int)val9.get_magnitude();
					distanceLabel.text = string.Empty + num7 + "m";
				}
			}
			SetVitalSprite(isHostPlayer);
			return;
		}
		SetActiveSafe(nearUI, active: true);
		SetActiveSafe(farUI, active: false);
		if (gaugeUI != null)
		{
			float num8 = (float)targetPlayer.hp / (float)targetPlayer.hpMax;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			if (gaugeUI.nowPercent != num8)
			{
				gaugeUI.SetPercent(num8);
			}
		}
		if (healGaugeUI != null)
		{
			float num9 = (float)targetPlayer.healHp / (float)targetPlayer.hpMax;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			if (healGaugeUI.nowPercent != num9)
			{
				healGaugeUI.SetPercent(num9);
			}
		}
		if (shieldGaugeUI != null)
		{
			float num10 = 0f;
			num10 = ((!targetPlayer.IsValidShield()) ? 0f : ((float)(int)targetPlayer.ShieldHp / (float)(int)targetPlayer.ShieldHpMax));
			if (num10 < 0f)
			{
				num10 = 0f;
			}
			if (shieldGaugeUI.nowPercent != num10)
			{
				shieldGaugeUI.SetPercent(num10);
			}
		}
		if (!(nameLabel != null))
		{
			return;
		}
		if (targetPlayer.createInfo != null && targetPlayer.createInfo.charaInfo != null && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData != null && MonoBehaviourSingleton<FieldManager>.I.fieldData.field != null && MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos != null && currentUserId != targetPlayer.createInfo.charaInfo.userId)
		{
			List<FieldModel.SlotInfo> slotInfos = MonoBehaviourSingleton<FieldManager>.I.fieldData.field.slotInfos;
			FriendCharaInfo friendCharaInfo = null;
			bool flag3 = false;
			int i = 0;
			for (int count = slotInfos.Count; i < count; i++)
			{
				if (slotInfos[i].userId == targetPlayer.createInfo.charaInfo.userId)
				{
					friendCharaInfo = slotInfos[i].userInfo;
				}
			}
			if (friendCharaInfo != null)
			{
				if (friendCharaInfo.follower && friendCharaInfo.following)
				{
					friendIcon.get_gameObject().SetActive(true);
				}
				else
				{
					friendIcon.get_gameObject().SetActive(false);
				}
				currentUserId = targetPlayer.createInfo.charaInfo.userId;
			}
		}
		nameLabel.text = targetPlayer.fullName;
		nameLabel.supportEncoding = true;
	}

	private void SetVitalSprite(bool _isOwner)
	{
		if (vitalSprite == null || hostEffect == null)
		{
			return;
		}
		GameObject gameObject = hostEffect.get_gameObject();
		hostEffect.set_enabled(false);
		string empty = string.Empty;
		if (targetPlayer.hp > 0)
		{
			empty = ((!((float)targetPlayer.hp <= (float)targetPlayer.hpMax * 0.25f)) ? "Ingame_member_vitalsign_green" : "Ingame_member_vitalsign_yellow");
		}
		else if (targetPlayer.IsRescuable() || targetPlayer.IsStone())
		{
			empty = "Ingame_member_vitalsign_red";
			if (!hostEffect.get_enabled() && _isOwner)
			{
				hostEffect.set_enabled(true);
			}
		}
		else
		{
			empty = "Ingame_member_vitalsign_gray";
		}
		vitalSprite.spriteName = empty;
	}

	public void SetUISpriteColor(UISprite sprite, Color c)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (sprite != null)
		{
			Color color = sprite.color;
			if (color.r != c.r || color.g != c.g || color.b != c.b)
			{
				color.r = c.r;
				color.g = c.g;
				color.b = c.b;
				sprite.color = color;
			}
		}
	}

	public void SetVisible(bool visible)
	{
		isVisible = visible;
	}

	public void SayChat(int chatID)
	{
		string chatSayText = MonoBehaviourSingleton<UIChatButtonBase>.I.GetChatSayText(chatID);
		DisplayChat(chatSayText);
	}

	public void SayChat(string message)
	{
		DisplayChat(message);
	}

	public void SetChatDuration(float duration)
	{
		if (chatTween != null)
		{
			chatTween.duration = duration;
		}
	}

	private void DisplayChat(string message)
	{
		if (CanDisplayChat())
		{
			chatLabel.text = message;
			chatUI.SetActive(true);
			if (chatTween != null)
			{
				chatTween.ResetToBeginning();
				chatTween.PlayForward();
			}
		}
	}

	public void SayChatStamp(int stampId)
	{
		this.StartCoroutine(DoDisplayChatStamp(stampId));
	}

	private IEnumerator DoDisplayChatStamp(int stampId)
	{
		chatStampUI.SetActive(false);
		LoadingQueue lqstamp = new LoadingQueue(this);
		LoadObject lostamp = lqstamp.LoadChatStamp(stampId, cache_check: true);
		yield return lqstamp.Wait();
		if (lostamp.loadedObject == null)
		{
			yield break;
		}
		if (chatStampTexture != null)
		{
			chatStampTexture.mainTexture = (lostamp.loadedObject as Texture2D);
		}
		if (CanDisplayChat())
		{
			chatStampUI.SetActive(true);
			if (chatStampTween != null)
			{
				chatStampTween.ResetToBeginning();
				chatStampTween.PlayForward();
			}
		}
	}

	private bool CanDisplayChat()
	{
		return targetPlayer != null && targetPlayer.isInitialized;
	}

	public void OnFinishChat()
	{
		chatUI.SetActive(false);
	}

	public void OnFinishedChatStamp()
	{
		chatStampUI.SetActive(false);
	}

	public void OnDamageSelf()
	{
		damagedTimer = Time.get_time();
	}

	public void OnDispEmotion(bool isDisp)
	{
		emotionUI.SetActive(isDisp);
		if (isDisp)
		{
			emotionTweenS.ResetToBeginning();
			emotionTweenS.PlayForward();
			emotionTweenA.ResetToBeginning();
			emotionTweenA.PlayForward();
		}
	}

	public void SetEmotionDuration(float duration)
	{
		emotionTweenS.duration = duration;
		emotionTweenA.duration = duration;
	}

	protected override void SortAll()
	{
		UIStatusGizmoBase.uiList.Sort(delegate(UIStatusGizmoBase a, UIStatusGizmoBase b)
		{
			if (a.isHostPlayer)
			{
				return 1;
			}
			if (b.isHostPlayer)
			{
				return -1;
			}
			if (a.ScreenZ == b.ScreenZ)
			{
				return 0;
			}
			return (a.ScreenZ < b.ScreenZ) ? 1 : (-1);
		});
		UpdateUIDepth();
	}

	protected void UpdateUIDepth()
	{
		int i = 0;
		for (int count = UIStatusGizmoBase.uiList.Count; i < count; i++)
		{
			UIStatusGizmoBase uIStatusGizmoBase = UIStatusGizmoBase.uiList[i];
			int num = i * 10;
			if (uIStatusGizmoBase.depthOffset != num)
			{
				UIStatusGizmoBase.AdjustDepth(uIStatusGizmoBase.get_gameObject(), num - uIStatusGizmoBase.depthOffset);
				uIStatusGizmoBase.BasePanel.depth = num;
				uIStatusGizmoBase.depthOffset = num;
			}
		}
	}
}
