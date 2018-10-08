using UnityEngine;

public class AnimEventComponent
{
	public AnimEventData animEventData;

	public Animator animator;

	public IAnimEvent listener;

	private AnimEventProcessor processer;

	public AnimEventComponent()
		: this()
	{
	}

	private void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (animator == null)
		{
			animator = this.get_gameObject().GetComponent<Animator>();
		}
		Run();
	}

	private void Update()
	{
		if (processer != null)
		{
			processer.Update();
		}
	}

	public void Run()
	{
		if (animator != null && animEventData != null && listener != null)
		{
			processer = new AnimEventProcessor(animEventData, animator, listener);
		}
	}
}
