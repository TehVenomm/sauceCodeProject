using UnityEngine;

public class MiniMapIcon : MonoBehaviour
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
			if (_isOver != value)
			{
				icon.gameObject.SetActive(!value);
				if (overIcon != null)
				{
					overIcon.gameObject.SetActive(value);
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

	private void Awake()
	{
		icon.gameObject.SetActive(value: true);
		if (overIcon != null)
		{
			overIcon.gameObject.SetActive(value: false);
		}
		_trasform = base.transform;
	}

	public virtual void Initialize(MonoBehaviour root_object)
	{
		base.gameObject.SetActive(value: false);
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
		if (!(target == null))
		{
			if (isInitialized)
			{
				base.gameObject.SetActive(value: true);
				isInitialized = false;
			}
			bool flag = true;
			Vector3 localPosition = target.position;
			localPosition.x = (localPosition.x - center_x) * scaling;
			localPosition.y = (localPosition.z - center_y) * scaling;
			localPosition.z = 0f;
			if (localPosition.magnitude > ui_radius)
			{
				flag = false;
				localPosition = localPosition.normalized * ui_radius;
			}
			isOver = !flag;
			_trasform.localPosition = localPosition;
			_trasform.localRotation = Quaternion.Inverse(_trasform.parent.localRotation);
		}
	}
}
