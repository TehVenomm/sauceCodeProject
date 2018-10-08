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

	void AnimationEventProxy.IEvent.OnEvent()
	{
	}

	void AnimationEventProxy.IEvent.OnEventStr(string str)
	{
	}

	void AnimationEventProxy.IEvent.OnEventInt(int i)
	{
		if (i < players.Length && (Object)players[i] != (Object)null)
		{
			players[i].animator.Play("win", 0, 0f);
		}
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
		if (!((Object)targetAnim == (Object)null))
		{
			Transform transform = targetAnim.transform;
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = transform.position;
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = transform.rotation;
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			if (x > 0f)
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView = Utility.HorizontalToVerticalFOV(x);
			}
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
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		Time.timeScale = 100f;
	}

	private void OnDestroy()
	{
		if ((Object)targetAnim != (Object)null && skip)
		{
			targetAnim.clip.SampleAnimation(targetAnim.gameObject, targetAnim.clip.length);
			Update();
			Time.timeScale = 1f;
		}
	}
}
