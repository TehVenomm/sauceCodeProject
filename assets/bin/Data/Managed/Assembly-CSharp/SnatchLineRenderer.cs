using UnityEngine;

public class SnatchLineRenderer : MonoBehaviour
{
	private LineRenderer lineRenderer;

	private void Start()
	{
		Transform transform = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.snatchLine, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
		if (!((Object)transform == (Object)null))
		{
			lineRenderer = transform.GetComponent<LineRenderer>();
			if ((Object)lineRenderer != (Object)null)
			{
				lineRenderer.enabled = false;
			}
		}
	}

	private void OnDestroy()
	{
		if ((Object)lineRenderer != (Object)null)
		{
			Object.Destroy(lineRenderer.gameObject);
			lineRenderer = null;
		}
	}

	public void SetVisible()
	{
		if ((Object)lineRenderer != (Object)null)
		{
			lineRenderer.enabled = true;
		}
	}

	public void SetInvisible()
	{
		if ((Object)lineRenderer != (Object)null)
		{
			lineRenderer.enabled = false;
		}
	}

	public void SetPositonStart(Vector3 pos)
	{
		if ((Object)lineRenderer != (Object)null)
		{
			lineRenderer.SetPosition(0, pos);
		}
	}

	public void SetPositionEnd(Vector3 pos)
	{
		if ((Object)lineRenderer != (Object)null)
		{
			lineRenderer.SetPosition(1, pos);
		}
	}
}
