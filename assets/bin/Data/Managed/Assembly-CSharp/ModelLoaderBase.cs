using UnityEngine;

public abstract class ModelLoaderBase : MonoBehaviour
{
	public abstract bool IsLoading();

	public abstract Animator GetAnimator();

	public abstract void SetEnabled(bool is_enable);

	public abstract Transform GetHead();

	public static void SetEnabled(Renderer[] renderers, bool is_enable)
	{
		if (renderers != null)
		{
			int i = 0;
			for (int num = renderers.Length; i < num; i++)
			{
				renderers[i].enabled = is_enable;
			}
		}
	}
}
