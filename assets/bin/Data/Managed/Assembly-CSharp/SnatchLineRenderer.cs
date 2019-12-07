using UnityEngine;

public class SnatchLineRenderer : MonoBehaviour
{
	private LineRenderer lineRenderer;

	private void Start()
	{
		Transform transform = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.snatchLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
		if (!(transform == null))
		{
			lineRenderer = transform.GetComponent<LineRenderer>();
			if (lineRenderer != null)
			{
				lineRenderer.enabled = false;
			}
		}
	}

	private void OnDestroy()
	{
		if (lineRenderer != null)
		{
			Object.Destroy(lineRenderer.gameObject);
			lineRenderer = null;
		}
	}

	public void SetVisible()
	{
		if (lineRenderer != null)
		{
			lineRenderer.enabled = true;
		}
	}

	public void SetInvisible()
	{
		if (lineRenderer != null)
		{
			lineRenderer.enabled = false;
		}
	}

	public void SetPositonStart(Vector3 pos)
	{
		if (lineRenderer != null)
		{
			lineRenderer.SetPosition(0, pos);
		}
	}

	public void SetPositionEnd(Vector3 pos)
	{
		if (lineRenderer != null)
		{
			lineRenderer.SetPosition(1, pos);
		}
	}
}
