using UnityEngine;

public class SnatchLineRenderer
{
	private LineRenderer lineRenderer;

	public SnatchLineRenderer()
		: this()
	{
	}

	private void Start()
	{
		Transform val = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.snatchLine, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
		if (!(val == null))
		{
			lineRenderer = val.GetComponent<LineRenderer>();
			if (lineRenderer != null)
			{
				lineRenderer.set_enabled(false);
			}
		}
	}

	private void OnDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (lineRenderer != null)
		{
			Object.Destroy(lineRenderer.get_gameObject());
			lineRenderer = null;
		}
	}

	public void SetVisible()
	{
		if (lineRenderer != null)
		{
			lineRenderer.set_enabled(true);
		}
	}

	public void SetInvisible()
	{
		if (lineRenderer != null)
		{
			lineRenderer.set_enabled(false);
		}
	}

	public void SetPositonStart(Vector3 pos)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (lineRenderer != null)
		{
			lineRenderer.SetPosition(0, pos);
		}
	}

	public void SetPositionEnd(Vector3 pos)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (lineRenderer != null)
		{
			lineRenderer.SetPosition(1, pos);
		}
	}
}
