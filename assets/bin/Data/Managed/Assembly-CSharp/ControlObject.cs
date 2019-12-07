using UnityEngine;

public class ControlObject : DisableNotifyMonoBehaviour
{
	public virtual Vector3 _position
	{
		get
		{
			return base._transform.position;
		}
		set
		{
			base._transform.position = value;
		}
	}

	public virtual Quaternion _rotation
	{
		get
		{
			return base._transform.rotation;
		}
		set
		{
			base._transform.rotation = value;
		}
	}

	public virtual Vector3 _forward
	{
		get
		{
			return base._transform.forward;
		}
		set
		{
			base._transform.forward = value;
		}
	}

	public virtual Vector3 _right
	{
		get
		{
			return base._transform.right;
		}
		set
		{
			base._transform.right = value;
		}
	}

	public virtual Vector3 _up
	{
		get
		{
			return base._transform.up;
		}
		set
		{
			base._transform.up = value;
		}
	}

	public virtual void _LookAt(Vector3 pos)
	{
		base._transform.LookAt(pos);
	}
}
