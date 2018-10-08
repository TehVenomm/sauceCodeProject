using Network;
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

	[Tooltip("矢印横表示時のXオフセット")]
	[SerializeField]
	protected float arrowSideOffset = 25f;

	[Tooltip("スクリ\u30fcン横オフセット")]
	[SerializeField]
	protected float screenSideOffset = 36f;

	[Tooltip("スクリ\u30fcン下オフセット")]
	[SerializeField]
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

	[Tooltip("チャットスタンプ横オフセット")]
	[SerializeField]
	protected float chatStampSideOffset = 60f;

	[Tooltip("チャットスタンプ上オフセット")]
	[SerializeField]
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

	protected Vector3 chatUILocalPos = Vector3.zero;

	protected Transform chatTransform;

	protected Vector3 chatStampUILocalPos = Vector3.zero;

	protected Transform chatStampTransform;

	protected Vector3 emotionUILocalPos = Vector3.zero;

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
			if ((Object)_targetPlayer != (Object)null)
			{
				base.gameObject.SetActive(true);
				currentUserId = -1;
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(false);
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
		isVisible = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if ((Object)chatUI != (Object)null)
		{
			chatTransform = chatUI.transform;
			chatUILocalPos = chatTransform.localPosition;
			chatUI.SetActive(false);
		}
		if ((Object)chatStampUI != (Object)null)
		{
			chatStampTransform = chatStampUI.transform;
			chatStampUILocalPos = chatStampTransform.localPosition;
			chatStampUI.SetActive(false);
		}
		if ((Object)emotionUI != (Object)null)
		{
			emotionTransform = emotionUI.transform;
			emotionUILocalPos = emotionTransform.localPosition;
			emotionUI.SetActive(false);
		}
		if ((Object)chatTween != (Object)null)
		{
			chatTween.SetOnFinished(OnFinishChat);
		}
		if ((Object)chatStampTween != (Object)null)
		{
			chatStampTween.SetOnFinished(OnFinishedChatStamp);
		}
		if ((Object)prayerGauge != (Object)null)
		{
			prayerGauge.gameObject.SetActive(false);
		}
		if ((Object)prayerGaugeAdd != (Object)null)
		{
			prayerGaugeAdd.gameObject.SetActive(false);
		}
		nearUI.SetActive(false);
		farUI.SetActive(false);
		if ((Object)arrowUI != (Object)null)
		{
			arrowTransform = arrowUI.transform;
			arrowUI.SetActive(false);
		}
		if ((Object)friendIcon != (Object)null)
		{
			friendIcon.gameObject.SetActive(false);
		}
		if ((Object)hostEffect != (Object)null)
		{
			hostEffect.enabled = false;
		}
	}

	protected override void UpdateParam()
	{
		if ((Object)targetPlayer == (Object)null || !targetPlayer.gameObject.activeSelf || targetPlayer.isLoading || (!MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && targetPlayer.isStopCounter) || (!targetPlayer.isCoopInitialized && targetPlayer.IsPuppet()) || !isVisible)
		{
			SetActiveSafe(nearUI, false);
			SetActiveSafe(farUI, false);
			if ((Object)arrowUI != (Object)null)
			{
				SetActiveSafe(arrowUI, false);
			}
			if ((Object)prayerGauge != (Object)null)
			{
				SetActiveSafe(prayerGauge.gameObject, false);
			}
		}
		else
		{
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
			Vector3 a = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.width;
			float num3 = (float)Screen.height;
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
			Vector3 position2 = screenUIPosition;
			if (chatUI.activeSelf)
			{
				if (position2.x < chatSideOffset * num)
				{
					position2.x = chatSideOffset * num;
				}
				else if (position2.x > num2 - chatSideOffset * num)
				{
					position2.x = num2 - chatSideOffset * num;
				}
				if (position2.y > num3 - chatTopOffset * num)
				{
					position2.y = num3 - chatTopOffset * num;
				}
			}
			Vector3 position3 = screenUIPosition;
			if (chatStampUI.activeSelf || emotionUI.activeSelf)
			{
				if (position3.x < chatStampSideOffset * num)
				{
					position3.x = chatStampSideOffset * num;
				}
				else if (position3.x > num2 - chatStampSideOffset * num)
				{
					position3.x = num2 - chatStampSideOffset * num;
				}
				if (position3.y > num3 - chatStampTopOffset * num)
				{
					position3.y = num3 - chatStampTopOffset * num;
				}
			}
			Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
			Vector3 v = vector;
			Vector3 v2 = vector;
			if (chatUI.activeSelf)
			{
				v = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position2);
			}
			if (chatStampUI.activeSelf || emotionUI.activeSelf)
			{
				v2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position3);
			}
			if ((transform.position - vector).sqrMagnitude >= 2E-05f)
			{
				transform.position = vector;
			}
			if (chatUI.activeSelf)
			{
				Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(v);
				localPosition.y += chatUILocalPos.y;
				localPosition.z = 0f;
				chatTransform.localPosition = localPosition;
			}
			if (chatStampUI.activeSelf || emotionUI.activeSelf)
			{
				Vector3 localPosition2 = transform.worldToLocalMatrix.MultiplyPoint3x4(v2);
				localPosition2.y += chatUILocalPos.y;
				localPosition2.z = 0f;
				chatStampTransform.localPosition = localPosition2;
				emotionTransform.localPosition = localPosition2;
			}
			if ((Object)prayerGauge != (Object)null)
			{
				if (targetPlayer.revivalTimePercent > 0f)
				{
					SetActiveSafe(prayerGauge.gameObject, true);
					prayerGauge.SetPercent(targetPlayer.revivalTimePercent, true);
					if ((Object)nowPrayer != (Object)null)
					{
						SetActiveSafe(nowPrayer.gameObject, targetPlayer.IsPrayed());
					}
					if (targetPlayer.IsPrayed())
					{
						SetActiveSafe(prayerGaugeAdd.gameObject, true);
						prayerGaugeAdd.SetPercent(targetPlayer.revivalTimePercent, true);
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
						SetActiveSafe(prayerGaugeAdd.gameObject, false);
					}
				}
				else
				{
					SetActiveSafe(prayerGauge.gameObject, false);
				}
			}
			Self x = targetPlayer as Self;
			if ((Object)x != (Object)null)
			{
				bool flag2 = false;
				if (damagedTimer >= 0f)
				{
					if (Time.time - damagedTimer >= selfShowTime)
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
					SetActiveSafe(nearUI, false);
					SetActiveSafe(farUI, false);
					return;
				}
			}
			if ((flag && FieldManager.IsValidInGameNoQuest()) || !GameSaveData.instance.headName)
			{
				SetActiveSafe(nearUI, false);
				SetActiveSafe(farUI, false);
			}
			else
			{
				if (targetPlayer.rescueTime > 0f)
				{
					flag = true;
				}
				if (flag && (Object)x == (Object)null)
				{
					SetActiveSafe(nearUI, false);
					SetActiveSafe(farUI, true);
					if ((Object)arrowUI != (Object)null)
					{
						Vector3 vector2 = a - screenUIPosition;
						if (vector2 != Vector3.zero)
						{
							float num5 = 90f - Vector3.Angle(Vector3.right, vector2);
							arrowTransform.eulerAngles = new Vector3(0f, 0f, num5);
							SetActiveSafe(arrowUI, true);
							Vector3 localPosition3 = new Vector3(0f, 0f, 0f);
							float num6 = Mathf.Sin(num5 * 0.0174532924f);
							if (num6 > 0.01f)
							{
								localPosition3.x = arrowSideOffset;
							}
							else if (num6 < -0.01f)
							{
								localPosition3.x = 0f - arrowSideOffset;
							}
							arrowTransform.localPosition = localPosition3;
						}
						else
						{
							SetActiveSafe(arrowUI, false);
						}
					}
					if ((Object)distanceLabel != (Object)null)
					{
						if (targetPlayer.rescueTime > 0f)
						{
							distanceLabel.text = string.Empty + Mathf.CeilToInt(targetPlayer.rescueTime) + "\u3000";
						}
						else if ((Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (Object)null)
						{
							int num7 = (int)(targetPlayer._position - MonoBehaviourSingleton<StageObjectManager>.I.self._position).magnitude;
							distanceLabel.text = string.Empty + num7 + "m";
						}
					}
					SetVitalSprite(isHostPlayer);
				}
				else
				{
					SetActiveSafe(nearUI, true);
					SetActiveSafe(farUI, false);
					if ((Object)gaugeUI != (Object)null)
					{
						float num8 = (float)targetPlayer.hp / (float)targetPlayer.hpMax;
						if (num8 < 0f)
						{
							num8 = 0f;
						}
						if (gaugeUI.nowPercent != num8)
						{
							gaugeUI.SetPercent(num8, true);
						}
					}
					if ((Object)healGaugeUI != (Object)null)
					{
						float num9 = (float)targetPlayer.healHp / (float)targetPlayer.hpMax;
						if (num9 < 0f)
						{
							num9 = 0f;
						}
						if (healGaugeUI.nowPercent != num9)
						{
							healGaugeUI.SetPercent(num9, true);
						}
					}
					if ((Object)shieldGaugeUI != (Object)null)
					{
						float num10 = 0f;
						num10 = ((!targetPlayer.IsValidShield()) ? 0f : ((float)(int)targetPlayer.ShieldHp / (float)(int)targetPlayer.ShieldHpMax));
						if (num10 < 0f)
						{
							num10 = 0f;
						}
						if (shieldGaugeUI.nowPercent != num10)
						{
							shieldGaugeUI.SetPercent(num10, true);
						}
					}
					if ((Object)nameLabel != (Object)null)
					{
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
									friendIcon.gameObject.SetActive(true);
								}
								else
								{
									friendIcon.gameObject.SetActive(false);
								}
								currentUserId = targetPlayer.createInfo.charaInfo.userId;
							}
						}
						nameLabel.text = targetPlayer.fullName;
						nameLabel.supportEncoding = true;
					}
				}
			}
		}
	}

	private void SetVitalSprite(bool _isOwner)
	{
		if (!((Object)vitalSprite == (Object)null) && !((Object)hostEffect == (Object)null))
		{
			GameObject gameObject = hostEffect.gameObject;
			hostEffect.enabled = false;
			string empty = string.Empty;
			if (targetPlayer.hp > 0)
			{
				empty = ((!((float)targetPlayer.hp <= (float)targetPlayer.hpMax * 0.25f)) ? "Ingame_member_vitalsign_green" : "Ingame_member_vitalsign_yellow");
			}
			else if (targetPlayer.IsRescuable())
			{
				empty = "Ingame_member_vitalsign_red";
				if (!hostEffect.enabled && _isOwner)
				{
					hostEffect.enabled = true;
				}
			}
			else
			{
				empty = "Ingame_member_vitalsign_gray";
			}
			vitalSprite.spriteName = empty;
		}
	}

	public void SetUISpriteColor(UISprite sprite, Color c)
	{
		if ((Object)sprite != (Object)null)
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
		if ((Object)chatTween != (Object)null)
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
			if ((Object)chatTween != (Object)null)
			{
				chatTween.ResetToBeginning();
				chatTween.PlayForward();
			}
		}
	}

	public void SayChatStamp(int stampId)
	{
		StartCoroutine(DoDisplayChatStamp(stampId));
	}

	private IEnumerator DoDisplayChatStamp(int stampId)
	{
		chatStampUI.SetActive(false);
		LoadingQueue lqstamp = new LoadingQueue(this);
		LoadObject lostamp = lqstamp.LoadChatStamp(stampId, true);
		yield return (object)lqstamp.Wait();
		if (!(lostamp.loadedObject == (Object)null))
		{
			if ((Object)chatStampTexture != (Object)null)
			{
				chatStampTexture.mainTexture = (lostamp.loadedObject as Texture2D);
			}
			if (CanDisplayChat())
			{
				chatStampUI.SetActive(true);
				if ((Object)chatStampTween != (Object)null)
				{
					chatStampTween.ResetToBeginning();
					chatStampTween.PlayForward();
				}
			}
		}
	}

	private bool CanDisplayChat()
	{
		return (Object)targetPlayer != (Object)null && targetPlayer.isInitialized;
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
		damagedTimer = Time.time;
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
				UIStatusGizmoBase.AdjustDepth(uIStatusGizmoBase.gameObject, num - uIStatusGizmoBase.depthOffset);
				uIStatusGizmoBase.BasePanel.depth = num;
				uIStatusGizmoBase.depthOffset = num;
			}
		}
	}
}
