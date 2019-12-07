using UnityEngine;

public class AnimEventComponent : MonoBehaviour
{
	public AnimEventData animEventData;

	public Animator animator;

	public IAnimEvent listener;

	private AnimEventProcessor processer;

	private void Start()
	{
		if (animator == null)
		{
			animator = base.gameObject.GetComponent<Animator>();
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
