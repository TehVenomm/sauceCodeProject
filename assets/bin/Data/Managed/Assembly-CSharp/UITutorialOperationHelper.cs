using System;
using UnityEngine;

public class UITutorialOperationHelper : MonoBehaviour
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
			autoControlMark.SetActive(value: true);
		}

		public void HideAutoControlMark()
		{
			autoControlMark.SetActive(value: false);
		}

		public UISprite ShowTapFinger()
		{
			ShowTutorialWidget(tapFingerSprite);
			return tapFingerSprite;
		}

		public void HideTapFinger(Action onHided = null)
		{
			HideTutorialWidget(tapFingerSprite, onHided);
		}

		public void ShowTapIcon(int index = 0)
		{
			tapIconSprite.gameObject.SetActive(value: true);
			tapIconSprite.enabled = true;
			tapCharacters[index].gameObject.SetActive(value: true);
			tapCharacters[index].enabled = true;
		}

		public void HideTapIcon()
		{
			tapIconSprite.gameObject.SetActive(value: false);
			tapIconSprite.enabled = false;
			for (int i = 0; i < tapCharacters.Length; i++)
			{
				tapCharacters[i].gameObject.SetActive(value: false);
				tapCharacters[i].enabled = false;
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
			ShowTutorialWidget(longTapFingerSprite);
			return longTapFingerSprite;
		}

		public void HideLongTapFinger(Action onHided = null)
		{
			HideTutorialWidget(longTapFingerSprite, onHided);
		}

		public void ShowComplete()
		{
			complete.gameObject.SetActive(value: true);
			complete.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			complete.Play();
		}

		public void HideComplete()
		{
			complete.gameObject.SetActive(value: false);
		}

		public void ShowGoodJob()
		{
			good_job.gameObject.SetActive(value: true);
			good_job.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			good_job.Play();
		}

		public void HideGoodJob()
		{
			good_job.gameObject.SetActive(value: false);
		}

		public void ShowExcellent()
		{
			excellent.gameObject.SetActive(value: true);
			excellent.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			excellent.Play();
		}

		public void HideExcellent()
		{
			excellent.gameObject.SetActive(value: false);
		}

		public void ShowSplendid()
		{
			splendid.gameObject.SetActive(value: true);
			splendid.Reset();
			SoundManager.PlayOneShotUISE(SE_ID_COMPLETE);
			splendid.Play();
		}

		public void HideSplendid()
		{
			splendid.gameObject.SetActive(value: false);
		}

		public void ShowEnemyCount(int count)
		{
			CurrentEnemyShow = ((count > CurrentEnemyShow) ? count : CurrentEnemyShow);
			if (OnShowEnemyCount)
			{
				return;
			}
			if (count == 0)
			{
				enemy_counts[count].gameObject.SetActive(value: true);
				return;
			}
			if (enemy_counts[0].gameObject.activeInHierarchy)
			{
				enemy_counts[0].gameObject.SetActive(value: true);
			}
			OnShowEnemyCount = true;
			enemy_counts[count].gameObject.SetActive(value: true);
			enemy_counts[count].Reset();
			enemy_counts[count].Play(forward: true, delegate
			{
				OnShowEnemyCount = false;
				enemy_counts[count].gameObject.SetActive(value: false);
				if (CurrentEnemyShow > count)
				{
					ShowEnemyCount(count + 1);
				}
			});
		}

		public void HideEnemyCount()
		{
			for (int i = 0; i < enemy_counts.Length; i++)
			{
				enemy_counts[i].gameObject.SetActive(value: false);
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
			ShowTutorialWidget(tutorialText);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}

		public void ShowHelpPicture()
		{
			ShowTutorialWidget(helpPicture);
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
			ShowTutorialWidget(tutorialText);
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
			ShowTutorialWidget(tutorialText);
		}

		public void HideHelpText(Action onHided = null)
		{
			HideTutorialWidget(tutorialText, onHided);
		}

		public void ShowComboHelpText()
		{
			ShowTutorialWidget(comboText);
		}

		public void HideComboHelpText(Action onHided = null)
		{
			HideTutorialWidget(comboText, onHided);
		}

		public void ShowHelpPicture()
		{
			ShowTutorialWidget(helpPicture);
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
			ShowTutorialWidget(tutorialText);
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
			ShowTutorialWidget(helpPicture0);
		}

		public void HideHelpPicture0(Action onHided = null)
		{
			HideTutorialWidget(helpPicture0, onHided);
		}

		public void ShowHelpPicture1()
		{
			ShowTutorialWidget(helpPicture1);
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
		private UnityEngine.Object logoAnimationPrefab;

		private GameObject logoAnimationGameObject;

		public void ShowHelpPicture0()
		{
			ShowTutorialWidget(helpPicture0);
		}

		public void HideHelpPicture0(Action onHided = null)
		{
			HideTutorialWidget(helpPicture0, onHided);
		}

		public void ShowHelpPicture1()
		{
			ShowTutorialWidget(helpPicture1);
		}

		public void HideHelpPicture1(Action onHided = null)
		{
			HideTutorialWidget(helpPicture1, onHided);
		}

		public void ShowHelpPicture2()
		{
			ShowTutorialWidget(helpPicture2);
		}

		public void HideHelpPicture2(Action onHided = null)
		{
			HideTutorialWidget(helpPicture2, onHided);
		}

		public void ShowHelpPicture3()
		{
			ShowTutorialWidget(helpPicture3);
		}

		public void HideHelpPicture3(Action onHided = null)
		{
			HideTutorialWidget(helpPicture3, onHided);
		}

		public void PlayLogoAnimation()
		{
			logoAnimationGameObject = ResourceUtility.Realizes(logoAnimationPrefab, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform).gameObject;
		}

		public void StopLogoAnimation()
		{
			UnityEngine.Object.Destroy(logoAnimationGameObject);
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

		[SerializeField]
		private UITweenCtrl moveCtrl;

		[SerializeField]
		private UITweenCtrl attackCtrl;

		public void SetLabel(int counter)
		{
			if (okLbl != null)
			{
				okLbl.text = string.Format(STR_BASIC_NEW, counter);
			}
		}

		public bool isActive()
		{
			return helpUI.activeInHierarchy;
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
					HideHelpPicture();
				}));
			}
			if (helpUI != null)
			{
				helpUI.SetActive(value: true);
			}
			helpImage_1.SetActive(value: false);
			helpImage_2.SetActive(value: false);
			helpImageIOS_1.SetActive(value: false);
			helpImageIOS_2.SetActive(value: false);
			if (moveCtrl != null)
			{
				moveCtrl.gameObject.SetActive(value: true);
				moveCtrl.Play();
			}
			if (attackCtrl != null)
			{
				attackCtrl.gameObject.SetActive(value: true);
				attackCtrl.Play();
			}
		}

		public void HideHelpPicture(Action onHided = null)
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
			if (helpUI != null)
			{
				helpUI.SetActive(value: false);
				if (OnCloseHelp != null)
				{
					OnCloseHelp();
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

	public static void ShowTutorialWidget(UIWidget widget, float duration = 0.3f)
	{
		widget.gameObject.SetActive(value: true);
		widget.alpha = 0f;
		TweenAlpha.Begin(widget.gameObject, duration, 1f);
	}

	public static void HideTutorialWidget(UIWidget widget, Action onHided)
	{
		TweenAlpha ta = TweenAlpha.Begin(widget.gameObject, 0.2f, 0f);
		ta.onFinished.Clear();
		ta.AddOnFinished(delegate
		{
			widget.gameObject.SetActive(value: false);
			if (onHided != null)
			{
				onHided();
			}
			UnityEngine.Object.Destroy(ta);
		});
	}

	private void Awake()
	{
		countTime = (int)WAITING_BASIC_TIME;
		if (_basicNewHelper != null)
		{
			_basicNewHelper.HideHelpPicture();
		}
	}

	private void Update()
	{
		if (_basicNewHelper.isActive() && countTime >= 0)
		{
			_basicNewHelper.SetLabel(countTime);
			counterTimer += Time.deltaTime;
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
			_basicNewHelper.HideHelpPicture();
		}
	}
}
