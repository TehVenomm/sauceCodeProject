using System.Collections;
using UnityEngine;

public class QuestResultDirector : MonoBehaviour, AnimationEventProxy.IEvent
{
	public float[] playerAnimTimings;

	public Animation cameraAnim;

	public Animation cameraAnimTrial;

	private bool skip;

	public PlayerLoader[] players
	{
		get;
		set;
	}

	public Animation targetAnim
	{
		get;
		private set;
	}

	private void Start()
	{
		if (QuestManager.IsValidTrial())
		{
			targetAnim = cameraAnimTrial;
		}
		else
		{
			targetAnim = cameraAnim;
		}
		targetAnim.GetComponent<AnimationEventProxy>().listener = this;
		int i = 0;
		for (int num = playerAnimTimings.Length; i < num; i++)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.functionName = "OnEventInt";
			animationEvent.intParameter = i;
			animationEvent.time = playerAnimTimings[i];
			targetAnim.clip.AddEvent(animationEvent);
		}
	}

	private void Update()
	{
		if (!(targetAnim == null))
		{
			Transform transform = targetAnim.transform;
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = transform.position;
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = transform.rotation;
			float x = transform.localScale.x;
			if (x > 0f)
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = Utility.HorizontalToVerticalFOV(x);
			}
		}
	}

	void AnimationEventProxy.IEvent.OnEvent()
	{
	}

	void AnimationEventProxy.IEvent.OnEventStr(string str)
	{
	}

	void AnimationEventProxy.IEvent.OnEventInt(int i)
	{
		if (i < players.Length && players[i] != null)
		{
			players[i].animator.Play(players[i].GetWinMotionState(), 0, 0f);
		}
	}

	public void Skip()
	{
		if (!skip)
		{
			skip = true;
			StartCoroutine(DoSkip());
		}
	}

	private IEnumerator DoSkip()
	{
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		Time.timeScale = 100f;
	}

	private void OnDestroy()
	{
		if (targetAnim != null && skip)
		{
			targetAnim.clip.SampleAnimation(targetAnim.gameObject, targetAnim.clip.length);
			Update();
			Time.timeScale = 1f;
		}
	}
}
