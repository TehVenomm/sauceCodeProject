using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviourSingleton<TransitionManager>
{
	public enum TYPE
	{
		NONE,
		BLACK,
		WHITE,
		LOADING,
		NEW_FILEDOPEN,
		AUTO_EVENT
	}

	private const float BLACK_FADE_OUT_TIME = 0.15f;

	private const float BLACK_FADE_IN_TIME = 0.15f;

	private const float WHITE_FADE_OUT_TIME = 0.25f;

	private const float WHITE_FADE_IN_TIME = 0.25f;

	private const float LOADING_FADE_OUT_TIME = 0.25f;

	private const float LOADING_FADE_IN_TIME = 0.25f;

	private const float AUTO_EVENT_FADE_OUT_TIME = 0.1f;

	private const float AUTO_EVENT_FADE_IN_TIME = 0.1f;

	private UIPanel faderPanel;

	private UITexture faderTexture;

	private UISprite faderSprite;

	private TweenAlpha faderTweenAlpha;

	private bool isOut;

	private TYPE currentType;

	public bool isTransing
	{
		get;
		private set;
	}

	public bool isChanging
	{
		get;
		private set;
	}

	private IEnumerator Start()
	{
		while (!MonoBehaviourSingleton<UIManager>.IsValid() || MonoBehaviourSingleton<UIManager>.I.isLoading)
		{
			yield return null;
		}
		faderPanel = MonoBehaviourSingleton<UIManager>.I.faderPanel;
		faderTexture = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<UITexture>();
		faderSprite = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<UISprite>();
		faderTweenAlpha = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<TweenAlpha>();
		faderTweenAlpha.SetOnFinished(new EventDelegate(OnFaderTweenFinised));
		faderTweenAlpha.gameObject.SetActive(value: false);
	}

	private void Update()
	{
	}

	private void OnFaderTweenFinised()
	{
		if ((bool)faderTexture)
		{
			faderTexture.alpha = faderTweenAlpha.to;
		}
		if ((bool)faderSprite)
		{
			faderSprite.alpha = faderTweenAlpha.to;
		}
		StartCoroutine(DoFaderTweenFinised());
	}

	private IEnumerator DoFaderTweenFinised()
	{
		yield return null;
		isChanging = false;
		if (!isOut)
		{
			faderPanel.depth = 4000;
			faderTweenAlpha.gameObject.SetActive(value: false);
			OnEnd();
		}
	}

	private void OnEnd()
	{
		isTransing = false;
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, is_disable: false);
	}

	private void FadeOut(Color color, float time, int depth)
	{
		color.a = 0f;
		if ((bool)faderTexture)
		{
			faderTexture.color = color;
		}
		if ((bool)faderSprite)
		{
			faderSprite.color = color;
		}
		faderPanel.depth = depth;
		SetFade(1f, time);
	}

	private void FadeIn(float time)
	{
		SetFade(0f, time);
	}

	private void SetFade(float to, float time)
	{
		faderTweenAlpha.from = faderTweenAlpha.to;
		faderTweenAlpha.to = to;
		faderTweenAlpha.gameObject.SetActive(value: true);
		faderTweenAlpha.duration = time;
		faderTweenAlpha.enabled = true;
		faderTweenAlpha.ResetToBeginning();
	}

	private void Begin(TYPE type)
	{
		if (isTransing)
		{
			Log.Error("transing now.");
			return;
		}
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, is_disable: true);
		isOut = true;
		isChanging = true;
		isTransing = true;
		currentType = type;
		switch (type)
		{
		case TYPE.BLACK:
			FadeOut(Color.black, 0.15f, 4000);
			break;
		case TYPE.WHITE:
			FadeOut(Color.white, 0.25f, 4000);
			break;
		case TYPE.LOADING:
			MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(is_show: true);
			FadeOut(Color.black, 0.25f, 4000);
			break;
		case TYPE.AUTO_EVENT:
			FadeOut(Color.black, 0.1f, 7000);
			break;
		}
		MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(is_show: true);
		MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(isShow: true);
	}

	private void End()
	{
		if (isChanging)
		{
			Log.Error("changing now.");
			return;
		}
		isOut = false;
		isChanging = true;
		switch (currentType)
		{
		case TYPE.BLACK:
			FadeIn(0.15f);
			break;
		case TYPE.WHITE:
			FadeIn(0.25f);
			break;
		case TYPE.LOADING:
			MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(is_show: false);
			if (MonoBehaviourSingleton<UIManager>.I.isShowingGGTutorialMessage)
			{
				MonoBehaviourSingleton<UIManager>.I.HideGGTutorialMessage();
			}
			FadeIn(0.25f);
			break;
		case TYPE.AUTO_EVENT:
			FadeIn(0.1f);
			break;
		}
		MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(is_show: false);
		MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(isShow: false);
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.TRANSITION_END);
		}
	}

	public Coroutine Out(TYPE type = TYPE.BLACK)
	{
		if (type == TYPE.NONE)
		{
			return null;
		}
		return StartCoroutine(DoOut(type));
	}

	private IEnumerator DoOut(TYPE type)
	{
		while (isChanging)
		{
			yield return null;
		}
		if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			MonoBehaviourSingleton<TransitionManager>.I.Begin(type);
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return null;
			}
		}
	}

	public Coroutine In()
	{
		if (currentType == TYPE.NONE)
		{
			return null;
		}
		return StartCoroutine(DoIn());
	}

	private IEnumerator DoIn()
	{
		while (isChanging)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			MonoBehaviourSingleton<TransitionManager>.I.End();
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return null;
			}
		}
	}
}
