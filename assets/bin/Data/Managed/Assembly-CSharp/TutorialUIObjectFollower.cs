using UnityEngine;

public class TutorialUIObjectFollower : MonoBehaviour
{
	private Transform _transform;

	private UISprite sprite;

	private Vector3 offset;

	private bool isInScrollView;

	private Rect scrollViewSize;

	private UIScrollView scroll;

	public Transform target
	{
		get;
		private set;
	}

	private void Awake()
	{
		_transform = base.transform;
		sprite = GetComponent<UISprite>();
	}

	public void Setup(Transform target, Vector2 offset)
	{
		this.target = target;
		this.offset = offset.ToVector3XY();
		scroll = target.GetComponentInParent<UIScrollView>();
		if ((bool)scroll)
		{
			isInScrollView = true;
			CalcScrollRect(scroll);
		}
	}

	private void CalcScrollRect(UIScrollView scroll)
	{
		UIPanel component = scroll.GetComponent<UIPanel>();
		float x = component.finalClipRegion.x - component.finalClipRegion.z * 0.5f;
		float y = component.finalClipRegion.y - component.finalClipRegion.w * 0.5f;
		scrollViewSize = new Rect(new Vector2(x, y) * scroll.transform.lossyScale.x + scroll.transform.position.ToVector2XY(), new Vector2(component.finalClipRegion.z, component.finalClipRegion.w) * scroll.transform.lossyScale.x);
	}

	private void LateUpdate()
	{
		if (!target)
		{
			TutorialMessage.RemoveCursor(_transform);
			return;
		}
		if (!target.gameObject.activeInHierarchy)
		{
			sprite.enabled = false;
			return;
		}
		if (!sprite.enabled)
		{
			sprite.enabled = true;
		}
		if (isInScrollView)
		{
			Vector3 vector = target.position + offset;
			if (vector.y > scrollViewSize.yMax || vector.y < scrollViewSize.yMin)
			{
				sprite.enabled = false;
				CalcScrollRect(scroll);
				return;
			}
			if (!sprite.enabled)
			{
				sprite.enabled = true;
			}
		}
		_transform.position = target.position + offset;
	}
}
