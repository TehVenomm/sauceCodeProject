using UnityEngine;

public class UICenterOnChildCtrl : MonoBehaviour
{
	public UICenterOnChild.OnCenterCallback onCenter;

	public SpringPanel.OnFinished onFinished;

	private UICenterOnChild centerOnChild;

	private float springStrength;

	private Transform reserveTarget;

	public Transform lastTarget
	{
		get;
		private set;
	}

	public static UICenterOnChildCtrl Get(GameObject go)
	{
		UICenterOnChild component = go.GetComponent<UICenterOnChild>();
		if ((Object)component == (Object)null)
		{
			Log.Error("UICenterOnChild is not found.");
			return null;
		}
		UICenterOnChildCtrl uICenterOnChildCtrl = go.GetComponent<UICenterOnChildCtrl>();
		if ((Object)uICenterOnChildCtrl == (Object)null)
		{
			uICenterOnChildCtrl = go.AddComponent<UICenterOnChildCtrl>();
		}
		return uICenterOnChildCtrl;
	}

	private void Start()
	{
		if ((Object)centerOnChild == (Object)null)
		{
			centerOnChild = GetComponent<UICenterOnChild>();
			if ((Object)centerOnChild == (Object)null)
			{
				Object.Destroy(this);
				return;
			}
			springStrength = centerOnChild.springStrength;
			centerOnChild.onFinished = OnFinised;
			centerOnChild.onCenter = OnCenter;
		}
		if ((Object)reserveTarget != (Object)null)
		{
			Centering(reserveTarget, true);
			reserveTarget = null;
		}
	}

	private void OnCenter(GameObject go)
	{
		if ((Object)go.transform != (Object)lastTarget)
		{
			lastTarget = go.transform;
			if (onCenter != null)
			{
				onCenter(go);
			}
		}
	}

	private void OnFinised()
	{
		centerOnChild.springStrength = springStrength;
		if (onFinished != null)
		{
			onFinished();
		}
	}

	public void Centering(Transform target, bool is_instant = false)
	{
		if ((Object)centerOnChild == (Object)null)
		{
			reserveTarget = target;
		}
		else
		{
			if (is_instant)
			{
				centerOnChild.springStrength = 99999f;
				lastTarget = null;
			}
			centerOnChild.CenterOn(target);
		}
	}
}
