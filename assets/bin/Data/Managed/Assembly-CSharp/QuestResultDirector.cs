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

	public QuestResultDirector()
		: this()
	{
	}

	private void Start()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
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
			AnimationEvent val = new AnimationEvent();
			val.set_functionName("OnEventInt");
			val.set_intParameter(i);
			val.set_time(playerAnimTimings[i]);
			targetAnim.get_clip().AddEvent(val);
		}
	}

	private void Update()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetAnim == null))
		{
			Transform transform = targetAnim.get_transform();
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(transform.get_position());
			MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_rotation(transform.get_rotation());
			Vector3 localScale = transform.get_localScale();
			float x = localScale.x;
			if (x > 0f)
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera.set_fieldOfView(Utility.HorizontalToVerticalFOV(x));
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
			this.StartCoroutine(DoSkip());
		}
	}

	private IEnumerator DoSkip()
	{
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		Time.set_timeScale(100f);
	}

	private void OnDestroy()
	{
		if (targetAnim != null && skip)
		{
			targetAnim.get_clip().SampleAnimation(targetAnim.get_gameObject(), targetAnim.get_clip().get_length());
			Update();
			Time.set_timeScale(1f);
		}
	}
}
