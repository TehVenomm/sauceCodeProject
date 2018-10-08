using UnityEngine;

public class TutorialUIObjectFollower
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

	public TutorialUIObjectFollower()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
		sprite = this.GetComponent<UISprite>();
	}

	public void Setup(Transform target, Vector2 offset)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		this.target = target;
		this.offset = offset.ToVector3XY();
		scroll = target.GetComponentInParent<UIScrollView>();
		if (Object.op_Implicit(scroll))
		{
			isInScrollView = true;
			CalcScrollRect(scroll);
		}
	}

	private void CalcScrollRect(UIScrollView scroll)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		UIPanel component = scroll.GetComponent<UIPanel>();
		Vector4 finalClipRegion = component.finalClipRegion;
		float x = finalClipRegion.x;
		Vector4 finalClipRegion2 = component.finalClipRegion;
		float num = x - finalClipRegion2.z * 0.5f;
		Vector4 finalClipRegion3 = component.finalClipRegion;
		float y = finalClipRegion3.y;
		Vector4 finalClipRegion4 = component.finalClipRegion;
		float num2 = y - finalClipRegion4.w * 0.5f;
		Vector2 val = new Vector2(num, num2);
		Vector3 lossyScale = scroll.get_transform().get_lossyScale();
		Vector2 val2 = val * lossyScale.x + Utility.ToVector2XY(scroll.get_transform().get_position());
		Vector4 finalClipRegion5 = component.finalClipRegion;
		float z = finalClipRegion5.z;
		Vector4 finalClipRegion6 = component.finalClipRegion;
		Vector2 val3 = new Vector2(z, finalClipRegion6.w);
		Vector3 lossyScale2 = scroll.get_transform().get_lossyScale();
		scrollViewSize = new Rect(val2, val3 * lossyScale2.x);
	}

	private void LateUpdate()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit(target))
		{
			TutorialMessage.RemoveCursor(_transform);
		}
		else if (!target.get_gameObject().get_activeInHierarchy())
		{
			sprite.set_enabled(false);
		}
		else
		{
			if (!sprite.get_enabled())
			{
				sprite.set_enabled(true);
			}
			if (isInScrollView)
			{
				Vector3 val = target.get_position() + offset;
				if (val.y > scrollViewSize.get_yMax() || val.y < scrollViewSize.get_yMin())
				{
					sprite.set_enabled(false);
					CalcScrollRect(scroll);
					return;
				}
				if (!sprite.get_enabled())
				{
					sprite.set_enabled(true);
				}
			}
			_transform.set_position(target.get_position() + offset);
		}
	}
}
