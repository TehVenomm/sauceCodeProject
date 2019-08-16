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

	public UICenterOnChildCtrl()
		: this()
	{
	}

	public static UICenterOnChildCtrl Get(GameObject go)
	{
		UICenterOnChild component = go.GetComponent<UICenterOnChild>();
		if (component == null)
		{
			Log.Error("UICenterOnChild is not found.");
			return null;
		}
		UICenterOnChildCtrl uICenterOnChildCtrl = go.GetComponent<UICenterOnChildCtrl>();
		if (uICenterOnChildCtrl == null)
		{
			uICenterOnChildCtrl = go.AddComponent<UICenterOnChildCtrl>();
		}
		return uICenterOnChildCtrl;
	}

	private void Start()
	{
		if (centerOnChild == null)
		{
			centerOnChild = this.GetComponent<UICenterOnChild>();
			if (centerOnChild == null)
			{
				Object.Destroy(this);
				return;
			}
			springStrength = centerOnChild.springStrength;
			centerOnChild.onFinished = OnFinised;
			centerOnChild.onCenter = OnCenter;
		}
		if (reserveTarget != null)
		{
			Centering(reserveTarget, is_instant: true);
			reserveTarget = null;
		}
	}

	private void OnCenter(GameObject go)
	{
		if (go.get_transform() != lastTarget)
		{
			lastTarget = go.get_transform();
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
		if (centerOnChild == null)
		{
			reserveTarget = target;
			return;
		}
		if (is_instant)
		{
			centerOnChild.springStrength = 99999f;
			lastTarget = null;
		}
		centerOnChild.CenterOn(target);
	}
}
