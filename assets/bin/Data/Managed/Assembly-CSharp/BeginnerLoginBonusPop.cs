using System;
using System.Collections;
using UnityEngine;

public class BeginnerLoginBonusPop : GameSection
{
	private enum UI
	{
		OBJ_IMG_ROOT,
		TEX,
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

	private const string POP_IMAGE_NAME = "BLBP";

	private LoadingQueue loadQueue;

	private State currentState;

	private bool stateInitialized;

	private float showTimer;

	private bool skipRequest;

	public override void Initialize()
	{
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		InitTween((Enum)UI.OBJ_IMG_ROOT);
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.LOGINBONUS_IMAGE, "BLBP");
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		if (lo_image.loadedObject == null)
		{
			yield return null;
		}
		Transform texture = GetCtrl(UI.TEX);
		UITexture uiTexture = texture.GetComponent<UITexture>();
		Texture image = uiTexture.mainTexture = (lo_image.loadedObject as Texture);
		base.Initialize();
	}

	private void Update()
	{
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
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: false);
		bool wait = true;
		PlayAudio(AUDIO.START, 1.3f);
		PlayTween((Enum)UI.OBJ_IMG_ROOT, forward: true, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			yield return 0;
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
				SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: true);
			}
			if (skipRequest && 1.2f < showTimer)
			{
				wait = false;
			}
			yield return 0;
		}
		ChangeState(State.END);
	}

	private IEnumerator EndAnimation()
	{
		bool wait = true;
		PlayTween((Enum)UI.OBJ_IMG_ROOT, forward: false, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			yield return 0;
		}
		GameSection.BackSection();
	}

	private void OnQuery_SKIP()
	{
		skipRequest = true;
	}
}
