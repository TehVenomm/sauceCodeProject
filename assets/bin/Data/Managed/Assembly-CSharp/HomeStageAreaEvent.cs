using UnityEngine;

public class HomeStageAreaEvent : HomeStageEventBase
{
	public const int EVENT_LAYER = 2;

	public const int EVENT_LAYER_MASK = 4;

	public float noticeRange = 2f;

	public float noticeViewHeight = 3f;

	public string noticeButtonName;

	public Transform _transform
	{
		get;
		private set;
	}

	public SphereCollider _collider
	{
		get;
		private set;
	}

	public float defaultRadius
	{
		get;
		private set;
	}

	protected override int GetLayer()
	{
		return 2;
	}

	protected override void Awake()
	{
		base.Awake();
		_transform = this.get_transform();
		_collider = this.GetComponent<SphereCollider>();
		if (!(_collider == null))
		{
			defaultRadius = _collider.get_radius();
			_collider.set_radius(_collider.get_radius() + noticeRange);
		}
	}
}
