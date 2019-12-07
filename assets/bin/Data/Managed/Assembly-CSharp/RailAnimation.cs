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
			currentAnimState.time = currentAnimState.length * _rate;
		}
	}

	public bool enabledRail
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
			_animation.enabled = value;
		}
	}

	private void Awake()
	{
		_animation = GetComponent<Animation>();
		if (_animation == null)
		{
			_animation = base.gameObject.AddComponent<Animation>();
			_animation.playAutomatically = false;
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
				currentAnimState.enabled = true;
				currentAnimState.weight = 1f;
			}
			return;
		}
		changeInterp.Update();
		currentAnimState.weight = changeInterp.Get();
		nextAnimState.weight = 1f - currentAnimState.weight;
		if (!changeInterp.IsPlaying())
		{
			currentAnimState.enabled = false;
			currentAnimState.weight = 0f;
			currentAnimState = nextAnimState;
			nextAnimState = null;
			_rate = currentAnimState.time / currentAnimState.length;
		}
	}

	public void AddRailAnimClip(AnimationClip anim_clip, string name)
	{
		if (!(anim_clip == null))
		{
			_animation.AddClip(anim_clip, name);
			AnimationState animationState = _animation[name];
			animationState.speed = 0f;
			animationState.enabled = false;
			animationState.blendMode = AnimationBlendMode.Blend;
			animationState.wrapMode = WrapMode.Once;
			animationState.enabled = false;
			animationState.weight = 0f;
			if (currentAnimState == null)
			{
				currentAnimState = animationState;
				animationState.enabled = true;
				animationState.weight = 1f;
			}
		}
	}

	public void ChangeRail(string anim_clip_name, float time, float rate = -1f)
	{
		nextAnimState = _animation[anim_clip_name];
		if (!(nextAnimState == null) && !(nextAnimState == currentAnimState))
		{
			nextAnimState.weight = 1f - currentAnimState.weight;
			nextAnimState.enabled = true;
			if (rate >= 0f)
			{
				nextAnimState.time = nextAnimState.length * rate;
			}
			changeInterp.Set(time, currentAnimState.weight, 0f, null, 0f);
			changeInterp.Play();
		}
	}

	public bool IsChanging()
	{
		return changeInterp.IsPlaying();
	}
}
