using System;
using System.Collections;
using UnityEngine;

public class HomeCountdown : GameSection
{
	private enum UI
	{
		OBJ_COUNTDOWN_ROOT,
		TEX_COUNTDOWN,
		BTN_SKIP_FULL_SCREEN
	}

	private enum AUDIO
	{
		START = 40000388
	}

	private enum State
	{
		START,
		SHOW,
		END
	}

	private bool ready;

	private LoadingQueue loadQueue;

	private int showID;

	private State currentState;

	private bool stateInitialized;

	private float showTimer;

	private bool skipRequest;

	public override void Initialize()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		ready = false;
		showID = (int)GameSection.GetEventData();
		PlayerPrefs.SetInt("COUNTDOWN_SHOWED_REMAIN", showID);
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		InitTween((Enum)UI.OBJ_COUNTDOWN_ROOT);
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		string name = ResourceName.GetCountdownImage(showID);
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.COUNTDOWN_IMAGE, name, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (lo_image.loadedObject == null)
		{
			yield return (object)null;
		}
		Transform texture = GetCtrl(UI.TEX_COUNTDOWN);
		UITexture uiTexture = texture.GetComponent<UITexture>();
		Texture image = uiTexture.mainTexture = (lo_image.loadedObject as Texture);
		ready = true;
		base.Initialize();
	}

	private void Update()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (!stateInitialized)
		{
			switch (currentState)
			{
			case State.START:
				this.StartCoroutine(StartAnimation());
				stateInitialized = true;
				break;
			case State.SHOW:
				showTimer = 0f;
				this.StartCoroutine(ShowCountdown());
				stateInitialized = true;
				break;
			case State.END:
				this.StartCoroutine(EndAnimation());
				stateInitialized = true;
				break;
			}
		}
	}

	private void ChangeState(State nextState)
	{
		stateInitialized = false;
		currentState = nextState;
	}

	private IEnumerator StartAnimation()
	{
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, false);
		bool wait = true;
		PlayAudio(AUDIO.START, 1.3f, false);
		PlayTween((Enum)UI.OBJ_COUNTDOWN_ROOT, true, (EventDelegate.Callback)delegate
		{
			((_003CStartAnimation_003Ec__Iterator7E)/*Error near IL_0062: stateMachine*/)._003Cwait_003E__0 = false;
		}, true, 0);
		while (wait)
		{
			yield return (object)0;
		}
		ChangeState(State.SHOW);
	}

	private IEnumerator ShowCountdown()
	{
		bool wait = true;
		Transform skip = GetCtrl(UI.BTN_SKIP_FULL_SCREEN);
		while (wait)
		{
			showTimer += Time.get_deltaTime();
			if (1.2f < showTimer && !skip.get_gameObject().get_activeSelf())
			{
				SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, true);
			}
			if (skipRequest && 1.2f < showTimer)
			{
				wait = false;
			}
			yield return (object)0;
		}
		ChangeState(State.END);
	}

	private IEnumerator EndAnimation()
	{
		bool wait = true;
		PlayTween((Enum)UI.OBJ_COUNTDOWN_ROOT, false, (EventDelegate.Callback)delegate
		{
			((_003CEndAnimation_003Ec__Iterator80)/*Error near IL_0035: stateMachine*/)._003Cwait_003E__0 = false;
		}, true, 0);
		while (wait)
		{
			yield return (object)0;
		}
		DispatchEvent("BACK", null);
	}

	private void OnQuery_SKIP()
	{
		skipRequest = true;
	}
}
