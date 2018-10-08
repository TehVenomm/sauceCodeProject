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
			yield return (object)null;
		}
		faderPanel = MonoBehaviourSingleton<UIManager>.I.faderPanel;
		faderTexture = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<UITexture>();
		faderSprite = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<UISprite>();
		faderTweenAlpha = MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.FADER).GetComponent<TweenAlpha>();
		faderTweenAlpha.SetOnFinished(new EventDelegate(OnFaderTweenFinised));
		faderTweenAlpha.get_gameObject().SetActive(false);
	}

	private void Update()
	{
	}

	private void OnFaderTweenFinised()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(faderTexture))
		{
			faderTexture.alpha = faderTweenAlpha.to;
		}
		if (Object.op_Implicit(faderSprite))
		{
			faderSprite.alpha = faderTweenAlpha.to;
		}
		this.StartCoroutine(DoFaderTweenFinised());
	}

	private IEnumerator DoFaderTweenFinised()
	{
		yield return (object)null;
		isChanging = false;
		if (!isOut)
		{
			faderPanel.depth = 4000;
			faderTweenAlpha.get_gameObject().SetActive(false);
			OnEnd();
		}
	}

	private void OnEnd()
	{
		isTransing = false;
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, false);
	}

	private void FadeOut(Color color, float time, int depth)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		color.a = 0f;
		if (Object.op_Implicit(faderTexture))
		{
			faderTexture.color = color;
		}
		if (Object.op_Implicit(faderSprite))
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		faderTweenAlpha.from = faderTweenAlpha.to;
		faderTweenAlpha.to = to;
		faderTweenAlpha.get_gameObject().SetActive(true);
		faderTweenAlpha.duration = time;
		faderTweenAlpha.set_enabled(true);
		faderTweenAlpha.ResetToBeginning();
	}

	private void Begin(TYPE type)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (isTransing)
		{
			Log.Error("transing now.");
		}
		else
		{
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.TRANSITION, true);
			isOut = true;
			isChanging = true;
			isTransing = true;
			currentType = type;
			switch (type)
			{
			case TYPE.BLACK:
				FadeOut(Color.get_black(), 0.15f, 4000);
				break;
			case TYPE.WHITE:
				FadeOut(Color.get_white(), 0.25f, 4000);
				break;
			case TYPE.LOADING:
				MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(true);
				FadeOut(Color.get_black(), 0.25f, 4000);
				break;
			case TYPE.AUTO_EVENT:
				FadeOut(Color.get_black(), 0.1f, 7000);
				break;
			}
			MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(true);
			MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(true);
		}
	}

	private void End()
	{
		if (isChanging)
		{
			Log.Error("changing now.");
		}
		else
		{
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
				MonoBehaviourSingleton<UIManager>.I.loading.ShowTips(false);
				FadeIn(0.25f);
				break;
			case TYPE.AUTO_EVENT:
				FadeIn(0.1f);
				break;
			}
			MonoBehaviourSingleton<UIManager>.I.loading.ShowRushUI(false);
			MonoBehaviourSingleton<UIManager>.I.loading.ShowArenaUI(false);
			if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.TRANSITION_END);
			}
		}
	}

	public Coroutine Out(TYPE type = TYPE.BLACK)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		if (type == TYPE.NONE)
		{
			return null;
		}
		return this.StartCoroutine(DoOut(type));
	}

	private IEnumerator DoOut(TYPE type)
	{
		while (isChanging)
		{
			yield return (object)null;
		}
		if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			MonoBehaviourSingleton<TransitionManager>.I.Begin(type);
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return (object)null;
			}
		}
	}

	public Coroutine In()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		if (currentType == TYPE.NONE)
		{
			return null;
		}
		return this.StartCoroutine(DoIn());
	}

	private IEnumerator DoIn()
	{
		while (isChanging)
		{
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
		{
			MonoBehaviourSingleton<TransitionManager>.I.End();
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return (object)null;
			}
		}
	}
}
