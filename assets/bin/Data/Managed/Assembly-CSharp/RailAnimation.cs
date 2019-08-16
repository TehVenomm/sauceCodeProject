using UnityEngine;

public class RailAnimation : MonoBehaviour
{
	public const string DEFAULT_RAIL_ANIM_NAME = "DefaultRailAnim";

	public AnimationClip railAnimClip;

	private AnimationState currentAnimState;

	private AnimationState nextAnimState;

	private FloatInterpolator changeInterp = new FloatInterpolator();

	private bool addAnim;

	private float _rate;

	public Animation _animation
	{
		get;
		private set;
	}

	public float rate
	{
		get
		{
			return _rate;
		}
		set
		{
			_rate = value;
			if (_rate < 0f)
			{
				_rate = 0f;
			}
			else if (_rate > 1f)
			{
				_rate = 1f;
			}
			if (currentAnimState == null)
			{
				if (railAnimClip == null)
				{
					return;
				}
				AddRailAnimClip(railAnimClip, "DefaultRailAnim");
			}
			currentAnimState.set_time(currentAnimState.get_length() * _rate);
		}
	}

	public bool enabledRail
	{
		get
		{
			return this.get_enabled();
		}
		set
		{
			this.set_enabled(value);
			_animation.set_enabled(value);
		}
	}

	public RailAnimation()
		: this()
	{
	}

	private void Awake()
	{
		_animation = this.GetComponent<Animation>();
		if (_animation == null)
		{
			_animation = this.get_gameObject().AddComponent<Animation>();
			_animation.set_playAutomatically(false);
			addAnim = true;
		}
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit && addAnim)
		{
			Object.DestroyImmediate(_animation);
			_animation = null;
			addAnim = false;
		}
	}

	private void Update()
	{
		if (nextAnimState == null)
		{
			if (currentAnimState != null)
			{
				currentAnimState.set_enabled(true);
				currentAnimState.set_weight(1f);
			}
			return;
		}
		changeInterp.Update();
		currentAnimState.set_weight(changeInterp.Get());
		nextAnimState.set_weight(1f - currentAnimState.get_weight());
		if (!changeInterp.IsPlaying())
		{
			currentAnimState.set_enabled(false);
			currentAnimState.set_weight(0f);
			currentAnimState = nextAnimState;
			nextAnimState = null;
			_rate = currentAnimState.get_time() / currentAnimState.get_length();
		}
	}

	public void AddRailAnimClip(AnimationClip anim_clip, string name)
	{
		if (!(anim_clip == null))
		{
			_animation.AddClip(anim_clip, name);
			AnimationState val = _animation.get_Item(name);
			val.set_speed(0f);
			val.set_enabled(false);
			val.set_blendMode(0);
			val.set_wrapMode(1);
			val.set_enabled(false);
			val.set_weight(0f);
			if (currentAnimState == null)
			{
				currentAnimState = val;
				val.set_enabled(true);
				val.set_weight(1f);
			}
		}
	}

	public void ChangeRail(string anim_clip_name, float time, float rate = -1f)
	{
		nextAnimState = _animation.get_Item(anim_clip_name);
		if (!(nextAnimState == null) && !(nextAnimState == currentAnimState))
		{
			nextAnimState.set_weight(1f - currentAnimState.get_weight());
			nextAnimState.set_enabled(true);
			if (rate >= 0f)
			{
				nextAnimState.set_time(nextAnimState.get_length() * rate);
			}
			changeInterp.Set(time, currentAnimState.get_weight(), 0f, null, 0f);
			changeInterp.Play();
		}
	}

	public bool IsChanging()
	{
		return changeInterp.IsPlaying();
	}
}
