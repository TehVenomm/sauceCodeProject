using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	public Transform tweenTarget;

	public Vector3 hover = Vector3.get_zero();

	public Vector3 pressed = Vector3.get_zero();

	public float duration = 0.2f;

	private Quaternion mRot;

	private bool mStarted;

	public UIButtonRotation()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_000c: Unknown result type (might be due to invalid IL or missing references)
	//IL_0011: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null)
			{
				tweenTarget = this.get_transform();
			}
			mRot = tweenTarget.get_localRotation();
		}
	}

	private void OnEnable()
	{
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(this.get_gameObject()));
		}
	}

	private void OnDisable()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (mStarted && tweenTarget != null)
		{
			TweenRotation component = tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = mRot;
				component.set_enabled(false);
			}
		}
	}

	private void OnPress(bool isPressed)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled())
		{
			if (!mStarted)
			{
				Start();
			}
			TweenRotation.Begin(tweenTarget.get_gameObject(), duration, isPressed ? (mRot * Quaternion.Euler(pressed)) : ((!UICamera.IsHighlighted(this.get_gameObject())) ? mRot : (mRot * Quaternion.Euler(hover)))).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnHover(bool isOver)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled())
		{
			if (!mStarted)
			{
				Start();
			}
			TweenRotation.Begin(tweenTarget.get_gameObject(), duration, (!isOver) ? mRot : (mRot * Quaternion.Euler(hover))).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (this.get_enabled() && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			OnHover(isSelected);
		}
	}
}
