using UnityEngine;

public class ControlObject : DisableNotifyMonoBehaviour
{
	public virtual Vector3 _position
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base._transform.get_position();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			base._transform.set_position(value);
		}
	}

	public virtual Quaternion _rotation
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base._transform.get_rotation();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			base._transform.set_rotation(value);
		}
	}

	public virtual Vector3 _forward
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base._transform.get_forward();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			base._transform.set_forward(value);
		}
	}

	public virtual Vector3 _right
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base._transform.get_right();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			base._transform.set_right(value);
		}
	}

	public virtual Vector3 _up
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base._transform.get_up();
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			base._transform.set_up(value);
		}
	}

	public virtual void _LookAt(Vector3 pos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base._transform.LookAt(pos);
	}
}
