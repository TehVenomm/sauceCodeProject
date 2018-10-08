using UnityEngine;

public class MiniMapIcon
{
	[SerializeField]
	protected UISprite icon;

	[SerializeField]
	protected UISprite overIcon;

	private bool _isOver;

	private bool isInitialized;

	public bool isOver
	{
		get
		{
			return _isOver;
		}
		set
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			if (_isOver != value)
			{
				icon.get_gameObject().SetActive(!value);
				if (overIcon != null)
				{
					overIcon.get_gameObject().SetActive(value);
				}
			}
			_isOver = value;
		}
	}

	public Transform target
	{
		get;
		set;
	}

	public Transform _trasform
	{
		get;
		set;
	}

	public MiniMapIcon()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		icon.get_gameObject().SetActive(true);
		if (overIcon != null)
		{
			overIcon.get_gameObject().SetActive(false);
		}
		_trasform = this.get_transform();
	}

	public virtual void Initialize(MonoBehaviour root_object)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(false);
		isInitialized = true;
	}

	public void SetIconSprite(string spriteName)
	{
		if (icon != null)
		{
			icon.spriteName = spriteName;
		}
		if (overIcon != null)
		{
			overIcon.spriteName = spriteName;
		}
	}

	public void UpdateIcon(float center_x, float center_y, float scaling, float ui_radius)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null))
		{
			if (isInitialized)
			{
				this.get_gameObject().SetActive(true);
				isInitialized = false;
			}
			bool flag = true;
			Vector3 localPosition = target.get_position();
			localPosition.x = (localPosition.x - center_x) * scaling;
			localPosition.y = (localPosition.z - center_y) * scaling;
			localPosition.z = 0f;
			if (localPosition.get_magnitude() > ui_radius)
			{
				flag = false;
				localPosition = localPosition.get_normalized() * ui_radius;
			}
			isOver = !flag;
			_trasform.set_localPosition(localPosition);
			_trasform.set_localRotation(Quaternion.Inverse(_trasform.get_parent().get_localRotation()));
		}
	}
}
