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
		SetFullScreenButton(UI.BTN_SKIP_FULL_SCREEN);
		InitTween(UI.OBJ_IMG_ROOT);
		StartCoroutine(DoInitialize());
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
		Texture texture2 = GetCtrl(UI.TEX).GetComponent<UITexture>().mainTexture = (lo_image.loadedObject as Texture);
		base.Initialize();
	}

	private void Update()
	{
		if (!stateInitialized)
		{
			switch (currentState)
			{
			case State.START:
				StartCoroutine(StartAnimation());
				stateInitialized = true;
				break;
			case State.SHOW:
				showTimer = 0f;
				StartCoroutine(ShowCountdown());
				stateInitialized = true;
				break;
			case State.END:
				StartCoroutine(EndAnimation());
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
		SetActive(UI.BTN_SKIP_FULL_SCREEN, is_visible: false);
		bool wait = true;
		PlayAudio(AUDIO.START, 1.3f);
		PlayTween(UI.OBJ_IMG_ROOT, forward: true, delegate
		{
			wait = false;
		});
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
			showTimer += Time.deltaTime;
			if (1.2f < showTimer && !skip.gameObject.activeSelf)
			{
				SetActive(UI.BTN_SKIP_FULL_SCREEN, is_visible: true);
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
		PlayTween(UI.OBJ_IMG_ROOT, forward: false, delegate
		{
			wait = false;
		});
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
