using System.Collections;
using UnityEngine;

public class QuestResultDirector : AnimationEventProxy.IEvent
{
	public float[] playerAnimTimings;

	public Animation cameraAnim;

	private bool skip;

	public PlayerLoader[] players
	{
		get;
		set;
	}

	public QuestResultDirector()
		: this()
	{
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
			players[i].animator.Play("win", 0, 0f);
		}
	}

	private void Start()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		cameraAnim.GetComponent<AnimationEventProxy>().listener = this;
		int i = 0;
		for (int num = playerAnimTimings.Length; i < num; i++)
		{
			AnimationEvent val = new AnimationEvent();
			val.set_functionName("OnEventInt");
			val.set_intParameter(i);
			val.set_time(playerAnimTimings[i]);
			cameraAnim.get_clip().AddEvent(val);
		}
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Transform val = cameraAnim.get_transform();
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(val.get_position());
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_rotation(val.get_rotation());
		Vector3 localScale = val.get_localScale();
		float x = localScale.x;
		if (x > 0f)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.set_fieldOfView(Utility.HorizontalToVerticalFOV(x));
		}
	}

	public void Skip()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (!skip)
		{
			skip = true;
			this.StartCoroutine(DoSkip());
		}
	}

	private IEnumerator DoSkip()
	{
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		Time.set_timeScale(100f);
	}

	private void OnDestroy()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (skip)
		{
			cameraAnim.get_clip().SampleAnimation(cameraAnim.get_gameObject(), cameraAnim.get_clip().get_length());
			Update();
			Time.set_timeScale(1f);
		}
	}
}
