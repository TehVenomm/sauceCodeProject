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
		Vector4 finalClipRegion = component.finalClipRegion;
		float x = finalClipRegion.x;
		Vector4 finalClipRegion2 = component.finalClipRegion;
		float x2 = x - finalClipRegion2.z * 0.5f;
		Vector4 finalClipRegion3 = component.finalClipRegion;
		float y = finalClipRegion3.y;
		Vector4 finalClipRegion4 = component.finalClipRegion;
		float y2 = y - finalClipRegion4.w * 0.5f;
		Vector2 a = new Vector2(x2, y2);
		Vector3 lossyScale = scroll.transform.lossyScale;
		Vector2 position = a * lossyScale.x + scroll.transform.position.ToVector2XY();
		Vector4 finalClipRegion5 = component.finalClipRegion;
		float z = finalClipRegion5.z;
		Vector4 finalClipRegion6 = component.finalClipRegion;
		Vector2 a2 = new Vector2(z, finalClipRegion6.w);
		Vector3 lossyScale2 = scroll.transform.lossyScale;
		scrollViewSize = new Rect(position, a2 * lossyScale2.x);
	}

	private void LateUpdate()
	{
		if (!(bool)target)
		{
			TutorialMessage.RemoveCursor(_transform);
		}
		else if (!target.gameObject.activeInHierarchy)
		{
			sprite.enabled = false;
		}
		else
		{
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
}
