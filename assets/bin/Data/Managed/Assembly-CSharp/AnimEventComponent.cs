using UnityEngine;

public class AnimEventComponent : MonoBehaviour
{
	public AnimEventData animEventData;

	public Animator animator;

	public IAnimEvent listener;

	private AnimEventProcessor processer;

	private void Start()
	{
		if ((Object)animator == (Object)null)
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
		if ((Object)animator != (Object)null && (Object)animEventData != (Object)null && listener != null)
		{
			processer = new AnimEventProcessor(animEventData, animator, listener);
		}
	}
}
