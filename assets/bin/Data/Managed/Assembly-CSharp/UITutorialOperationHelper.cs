using System;
using UnityEngine;

public class UITutorialOperationHelper
{
	[Serializable]
	public class TutorialCommon
	{
		[SerializeField]
		private GameObject autoControlMark;

		[SerializeField]
		private UISprite tapFingerSprite;

		[SerializeField]
		private UISprite tapIconSprite;

		[SerializeField]
		private UISprite[] tapCharacters;

		[SerializeField]
		private UISprite longTapFingerSprite;

		[SerializeField]
		private UISprite fingerSprite;

		[SerializeField]
		private UITweenCtrl complete;

		[SerializeField]
		private UITweenCtrl good_job;

		[SerializeField]
		private UITweenCtrl excellent;

		[SerializeField]
		private UITweenCtrl splendid;

		[SerializeField]
		private UITweenCtrl[] enemy_counts;

		public bool OnShowEnemyCount;

		public int CurrentEnemyShow;

		public void ShowAutoControlMark()
		{
			autoControlMark.SetActive(true);
		}

		public void HideAutoControlMark()
		{
			autoControlMark.SetActive(false);
		}

		public UISprite ShowTapFinger()
		{
			ShowTutorialWidget(tapFingerSprite, 0.3f);
			return tapFingerSprite;
		}

		public void HideTapFinger(Action onHided = null)
		{
			HideTutorialWidget(tapFingerSprite, onHided);
		}

		public void ShowTapIcon(int index = 0)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			tapIconSprite.get_gameObject().SetActive(true);
			tapIconSprite.set_enabled(true);
			tapCharacters[index].get_gameObject().SetActive(true);
			tapCharacters[index].set_enabled(true);
		}

		public void HideTapIcon()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			tapIconSprite.get_gameObject().SetActive(false);
			tapIconSprite.set_enabled(false);
			for (int i = 0; i < tapCharacters.Length; i++)
			{
				tapCharacters[i].get_gameObject().SetActive(false);
				tapCharacters[i].set_enabled(false);
			}
		}

		public UISprite ShowFinger(float duration = 0.3f)
		{
			ShowTutorialWidget(fingerSprite, duration);
			return fingerSprite;
		}

		public void HideFinger(Action onHided = null)
		{
			HideTutorialWidget(fingerSprite, onHided);
		}

		public UISprite ShowLongTapFinger()
		{
			ShowTutorialWidget(longTapFingerSprite, 0.3f);
			return longTapFingerSprite;
		}

		public void HideLongTapFinger(Action onHided = null)
		{
			HideTutorialWidget(longTapFingerSprite, onHided);
		}

		public void ShowComplete()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			complete.get_gameObject().SetActive(true);
			complete.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			complete.Play(true, null);
		}

		public void HideComplete()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			complete.get_gameObject().SetActive(false);
		}

		public void ShowGoodJob()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			good_job.get_gameObject().SetActive(true);
			good_job.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			good_job.Play(true, null);
		}

		public void HideGoodJob()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			good_job.get_gameObject().SetActive(false);
		}

		public void ShowExcellent()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			excellent.get_gameObject().SetActive(true);
			excellent.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			excellent.Play(true, null);
		}

		public void HideExcellent()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			excellent.get_gameObject().SetActive(false);
		}

		public void ShowSplendid()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			splendid.get_gameObject().SetActive(true);
			splendid.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			splendid.Play(true, null);
		}

		public void HideSplendid()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			splendid.get_gameObject().SetActive(false);
		}

		public void ShowEnemyCount(int count)
		{
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			CurrentEnemyShow = ((count <= CurrentEnemyShow) ? CurrentEnemyShow : count);
			if (!OnShowEnemyCount)
			{
				if (count == 0)
				{
					enemy_counts[count].get_gameObject().SetActive(true);
				}
				else
				{
					if (enemy_counts[0].get_gameObject().get_activeInHierarchy())
					{
						enemy_counts[0].get_gameObject().SetActive(true);
					}
					OnShowEnemyCount = true;
					enemy_counts[count].get_gameObject().SetActive(true);
					enemy_counts[count].Reset();
					enemy_counts[count].Play(true, delegate
					{
						//IL_001e: Unknown result type (might be due to invalid IL or missing references)
						OnShowEnemyCount = false;
						enemy_counts[count].get_gameObject().SetActive(false);
						if (CurrentEnemyShow > count)
						{
							ShowEnemyCount(count + 1);
						}
					});
				}
			}
		}

		public void HideEnemyCount()
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			for (int i = 0; i < enemy_counts.Length; i++)
			{
				enemy_counts[i].get_gameObject().SetActive(false);
			}
		}
	}

	[Serializable]
	public class TutorialMove
	{
		[SerializeField]
		private UIWidget tutorialText;

		[SerializeField]
		private UIWidget helpPicture;

		[SerializeField]
		private UIWidget helpPicture_ios;

		public void ShowHelpText()
		{
			ShowTutorialWidget(tutorialText, 0.3f);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}

		public void ShowHelpPicture()
		{
			ShowTutorialWidget(helpPicture, 0.3f);
		}

		public void HideHelpPicture(Action onHided = null)
		{
			HideTutorialWidget(helpPicture, onHided);
		}
	}

	[Serializable]
	public class TutorialAvoid
	{
		[SerializeField]
		private UIWidget tutorialText;

		public void ShowHelpText()
		{
			ShowTutorialWidget(tutorialText, 0.3f);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}
	}

	[Serializable]
	public class TutorialAttack
	{
		[SerializeField]
		private UIWidget tutorialText;

		[SerializeField]
		private UIWidget comboText;

		[SerializeField]
		private UIWidget helpPicture;

		[SerializeField]
		private UIWidget helpPicture_ios;

		public void ShowHelpText()
		{
			ShowTutorialWidget(tutorialText, 0.3f);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}

		public void ShowComboHelpText()
		{
			ShowTutorialWidget(comboText, 0.3f);
		}

		public void HideComboHelpText(Action onHided = null)
		{
			HideTutorialWidget(comboText, onHided);
		}

		public void ShowHelpPicture()
		{
			ShowTutorialWidget(helpPicture, 0.3f);
		}

		public void HideHelpPicture(Action onHided = null)
		{
			HideTutorialWidget(helpPicture, onHided);
		}
	}

	[Serializable]
	public class TutorialGuard
	{
		[SerializeField]
		private UIWidget tutorialText;

		public void ShowHelpText()
		{
			ShowTutorialWidget(tutorialText, 0.3f);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}
	}

	[Serializable]
	public class TutorialBattle
	{
		[SerializeField]
		private UISprite helpPicture0;

		[SerializeField]
		private UISprite helpPicture1;

		public void ShowHelpPicture0()
		{
			ShowTutorialWidget(helpPicture0, 0.3f);
		}

		public void HideHelpPicture0(Action onHided = null)
		{
			HideTutorialWidget(helpPicture0, onHided);
		}

		public void ShowHelpPicture1()
		{
			ShowTutorialWidget(helpPicture1, 0.3f);
		}

		public void HideHelpPicture1(Action onHided = null)
		{
			HideTutorialWidget(helpPicture1, onHided);
		}
	}

	[Serializable]
	public class TutorialBoss
	{
		[SerializeField]
		private UISprite helpPicture0;

		[SerializeField]
		private UISprite helpPicture1;

		[SerializeField]
		private UISprite helpPicture2;

		[SerializeField]
		private UISprite helpPicture3;

		[SerializeField]
		private Object logoAnimationPrefab;

		private GameObject logoAnimationGameObject;

		public void ShowHelpPicture0()
		{
			ShowTutorialWidget(helpPicture0, 0.3f);
		}

		public void HideHelpPicture0(Action onHided = null)
		{
			HideTutorialWidget(helpPicture0, onHided);
		}

		public void ShowHelpPicture1()
		{
			ShowTutorialWidget(helpPicture1, 0.3f);
		}

		public void HideHelpPicture1(Action onHided = null)
		{
			HideTutorialWidget(helpPicture1, onHided);
		}

		public void ShowHelpPicture2()
		{
			ShowTutorialWidget(helpPicture2, 0.3f);
		}

		public void HideHelpPicture2(Action onHided = null)
		{
			HideTutorialWidget(helpPicture2, onHided);
		}

		public void ShowHelpPicture3()
		{
			ShowTutorialWidget(helpPicture3, 0.3f);
		}

		public void HideHelpPicture3(Action onHided = null)
		{
			HideTutorialWidget(helpPicture3, onHided);
		}

		public void PlayLogoAnimation()
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			logoAnimationGameObject = ResourceUtility.Realizes(logoAnimationPrefab, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform, -1).get_gameObject();
		}

		public void StopLogoAnimation()
		{
			Object.Destroy(logoAnimationGameObject);
		}
	}

	[Serializable]
	public class TutorialBasicNew
	{
		[SerializeField]
		private GameObject helpUI;

		[SerializeField]
		private GameObject helpImage_1;

		[SerializeField]
		private GameObject helpImage_2;

		[SerializeField]
		private GameObject helpImageIOS_1;

		[SerializeField]
		private GameObject helpImageIOS_2;

		[SerializeField]
		private UIButton okBtn;

		[SerializeField]
		private UILabel okLbl;

		private Action OnCloseHelp;

		public void SetLabel(int counter)
		{
			if (okLbl != null)
			{
				okLbl.text = string.Format(STR_BASIC_NEW, counter);
			}
		}

		public bool isActive()
		{
			return helpUI.get_activeInHierarchy();
		}

		public void ShowHelpPicture(Action OnCloseHelp)
		{
			if (OnCloseHelp != null)
			{
				this.OnCloseHelp = OnCloseHelp;
			}
			if (okBtn != null)
			{
				okBtn.onClick.Add(new EventDelegate(delegate
				{
					HideHelpPicture(null);
				}));
			}
			if (helpUI != null)
			{
				helpUI.SetActive(true);
			}
			if (helpImage_1 != null && helpImage_2 != null)
			{
				helpImage_1.SetActive(true);
				helpImage_2.SetActive(true);
			}
			if (helpImageIOS_1 != null && helpImageIOS_2 != null)
			{
				helpImageIOS_1.SetActive(false);
				helpImageIOS_2.SetActive(false);
			}
		}

		public void HideHelpPicture(Action onHided = null)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
			if (helpUI != null)
			{
				helpUI.SetActive(false);
				if (OnCloseHelp != null)
				{
					OnCloseHelp.Invoke();
				}
			}
		}
	}

	public static readonly string STR_BASIC_NEW = "LET'S FIGHT! ({0})";

	public static readonly float WAITING_BASIC_TIME = 10f;

	public static readonly int SE_ID_COMPLETE = 40000100;

	public static readonly int SE_ID_THUNDERSTORM_01 = 40000110;

	public static readonly int SE_ID_DRAGON_FLUTTER_01 = 40000111;

	public static readonly int SE_ID_DRAGON_LANDING = 40000112;

	public static readonly int SE_ID_DRAGON_CALL_01 = 40000113;

	public static readonly int SE_ID_THUNDERSTORM_02 = 40000114;

	public static readonly int SE_ID_DRAGON_CALL_02 = 40000115;

	public static readonly int SE_ID_DRAGON_CALL_03 = 40000116;

	public static readonly int SE_ID_DRAGON_FLUTTER_02 = 40000117;

	public static readonly int SE_ID_DRAGON_CALL_04 = 40000118;

	public static readonly int SE_ID_TITLELOGO = 40000119;

	[SerializeField]
	private TutorialCommon _commonHelper = new TutorialCommon();

	[SerializeField]
	private TutorialMove _moveHelper = new TutorialMove();

	[SerializeField]
	private TutorialAvoid _avoidHelper = new TutorialAvoid();

	[SerializeField]
	private TutorialAttack _attackHelper = new TutorialAttack();

	[SerializeField]
	private TutorialGuard _guardHelper = new TutorialGuard();

	[SerializeField]
	private TutorialBattle _battleHelper = new TutorialBattle();

	[SerializeField]
	private TutorialBoss _bossHelper = new TutorialBoss();

	[SerializeField]
	private TutorialBasicNew _basicNewHelper = new TutorialBasicNew();

	[SerializeField]
	private Transform _fingerMove;

	[SerializeField]
	private Transform _fingerRolling;

	[SerializeField]
	private Transform _fingerRoot;

	[SerializeField]
	private Transform _fingerAttack;

	private float counterTimer;

	private int countTime;

	public TutorialCommon commonHelper => _commonHelper;

	public TutorialMove moveHelper => _moveHelper;

	public TutorialAvoid avoidHelper => _avoidHelper;

	public TutorialAttack attackHelper => _attackHelper;

	public TutorialGuard guardHelper => _guardHelper;

	public TutorialBattle battleHelper => _battleHelper;

	public TutorialBoss bossHelper => _bossHelper;

	public TutorialBasicNew basicNewHelper => _basicNewHelper;

	public Transform fingerMove => _fingerMove;

	public Transform fingerRolling => _fingerRolling;

	public Transform fingerRoot => _fingerRoot;

	public Transform fingerAttack => _fingerAttack;

	public UITutorialOperationHelper()
		: this()
	{
	}

	public static void ShowTutorialWidget(UIWidget widget, float duration = 0.3f)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		widget.get_gameObject().SetActive(true);
		widget.alpha = 0f;
		TweenAlpha.Begin(widget.get_gameObject(), duration, 1f);
	}

	public static void HideTutorialWidget(UIWidget widget, Action onHided)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		TweenAlpha ta = TweenAlpha.Begin(widget.get_gameObject(), 0.2f, 0f);
		ta.onFinished.Clear();
		ta.AddOnFinished(delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			widget.get_gameObject().SetActive(false);
			if (onHided != null)
			{
				onHided.Invoke();
			}
			Object.Destroy(ta);
		});
	}

	private void Awake()
	{
		countTime = (int)WAITING_BASIC_TIME;
		if (_basicNewHelper != null)
		{
			_basicNewHelper.HideHelpPicture(null);
		}
	}

	private void Update()
	{
		if (_basicNewHelper.isActive() && countTime >= 0)
		{
			_basicNewHelper.SetLabel(countTime);
			counterTimer += Time.get_deltaTime();
			if (counterTimer >= 1f)
			{
				countTime--;
				counterTimer = 0f;
				if (countTime >= 0)
				{
					_basicNewHelper.SetLabel(countTime);
				}
			}
		}
		else if (_basicNewHelper.isActive())
		{
			_basicNewHelper.HideHelpPicture(null);
		}
	}
}
