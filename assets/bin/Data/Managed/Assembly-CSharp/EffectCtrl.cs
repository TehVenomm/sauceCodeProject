using UnityEngine;

public class EffectCtrl
{
	[Tooltip("ル\u30fcプエフェクトかどうか")]
	[Header("-- Effect Settings --")]
	public bool loop;

	[Tooltip("管理対象のParticleSystemの配列")]
	public ParticleSystem[] particles;

	[Tooltip("particlesが空の場合、自動的に検索するかどうか")]
	public bool autoCollectParticles = true;

	[Tooltip("管理対象のAnimator\n空の場合はこのGameObjectにアタッチされたAnimatorが使用される")]
	public Animator animator;

	[Tooltip("ル\u30fcプを抜ける時にパ\u30fcティクルを停止するかどうか")]
	[Header("-- Loop End Behaviour --")]
	public bool stopParticle = true;

	[Tooltip("ル\u30fcプを抜ける時にAnimatorのENDを再生するかどうか")]
	public bool changeStateToEND = true;

	[Tooltip("AnimatorのENDを再生する時のクロスフェ\u30fcド時間（秒）")]
	public float crossFadeTimeToEND = 0.1f;

	[Tooltip("指定時間を待ってから削除（秒）\n0に設定すると待たない")]
	[Header("-- Wait Destroy --")]
	public float waitTime = 0.2f;

	[Tooltip("パ\u30fcティクルが全て消えてから削除")]
	public bool waitParticlePlaying = true;

	[Tooltip("アニメが最後まで再生されてから削除")]
	public bool waitAnimationPlaying = true;

	[Header("-- Audio Destroy --")]
	[Tooltip("同時に再生される可能性のあるAudioClip")]
	public AudioClip attachedAudioClip;

	[Tooltip("同時に再生される可能性のあるAudioClipのSE設定ID")]
	public int attachedAudioSettingID = 40000035;

	private Transform _transform;

	private bool loopEnd;

	private float timer;

	private int defaultStateHash;

	private int endStateHash;

	private AudioObject loopAudioObject;

	private AudioClip loopAudioClip;

	private bool isPause;

	private int pauseStateHash;

	public EffectCtrl()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		if ((particles == null || particles.Length == 0) && autoCollectParticles)
		{
			particles = this.GetComponentsInChildren<ParticleSystem>();
		}
		if (animator == null)
		{
			Animator component = this.GetComponent<Animator>();
			if (component != null)
			{
				animator = component;
			}
			else
			{
				animator = null;
			}
		}
		if (animator != null)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
			defaultStateHash = currentAnimatorStateInfo.get_fullPathHash();
			int num = Animator.StringToHash("END");
			if (animator.HasState(0, num))
			{
				endStateHash = num;
			}
		}
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			__FUNCTION__StopLoopSE();
		}
	}

	private void Update()
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		if (!loop || loopEnd)
		{
			if (waitTime > 0f)
			{
				timer += Time.get_deltaTime();
				if (timer < waitTime)
				{
					return;
				}
			}
			if (waitParticlePlaying)
			{
				int i = 0;
				for (int num = particles.Length; i < num; i++)
				{
					ParticleSystem val = particles[i];
					if (val != null && val.get_isPlaying())
					{
						val.Stop(true);
						return;
					}
				}
			}
			if (waitAnimationPlaying && animator != null)
			{
				if (animator.IsInTransition(0))
				{
					return;
				}
				AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.get_loop() && currentAnimatorStateInfo.get_normalizedTime() < 1f)
				{
					return;
				}
			}
			DestroyGameObject();
		}
	}

	public void EndLoop(bool isPlayEndAnimation = true)
	{
		loopEnd = true;
		if (stopParticle)
		{
			int i = 0;
			for (int num = particles.Length; i < num; i++)
			{
				ParticleSystem val = particles[i];
				if (val != null)
				{
					val.Stop(true);
				}
			}
		}
		if (changeStateToEND && animator != null && endStateHash != 0 && isPlayEndAnimation)
		{
			if (crossFadeTimeToEND > 0f)
			{
				animator.CrossFade(endStateHash, crossFadeTimeToEND, 0);
			}
			else
			{
				animator.Play(endStateHash, 0, 0f);
			}
		}
	}

	public void Play(string stateName)
	{
		Play(Animator.StringToHash(stateName));
	}

	public void Play(int stateNameHash)
	{
		if (!(animator == null) && animator.HasState(0, stateNameHash))
		{
			animator.Play(stateNameHash);
		}
	}

	public void CrossFade(int stateNameHash, float transitionDuration)
	{
		if (!(animator == null) && animator.HasState(0, stateNameHash))
		{
			animator.CrossFade(stateNameHash, transitionDuration);
		}
	}

	public bool IsCurrentState(int stateNameHash)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (animator == null)
		{
			return false;
		}
		AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		return currentAnimatorStateInfo.get_shortNameHash() == stateNameHash;
	}

	public void Pause(bool pause)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (isPause != pause)
		{
			if (pause)
			{
				if (this.get_gameObject().get_activeInHierarchy())
				{
					if (!object.ReferenceEquals(animator, null))
					{
						AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
						int shortNameHash = currentAnimatorStateInfo.get_shortNameHash();
						if (shortNameHash == 0)
						{
							return;
						}
						pauseStateHash = shortNameHash;
						animator.Stop();
					}
					this.get_gameObject().SetActive(false);
					isPause = true;
				}
			}
			else
			{
				this.get_gameObject().SetActive(true);
				if (!object.ReferenceEquals(animator, null))
				{
					animator.Play(pauseStateHash);
					pauseStateHash = 0;
				}
				isPause = false;
			}
		}
	}

	public void SetRenderQueue(int renderQueue)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (particles != null)
		{
			if (particles.Length == 0)
			{
				particles = this.GetComponentsInChildren<ParticleSystem>(true);
			}
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(renderQueue);
			}
		}
	}

	public void Reset()
	{
		timer = 0f;
		loopEnd = false;
		if (particles != null)
		{
			int i = 0;
			for (int num = particles.Length; i < num; i++)
			{
				ParticleSystem val = particles[i];
				if (val != null)
				{
					val.Clear(true);
					val.Play(true);
				}
			}
		}
		if (animator != null && defaultStateHash != 0)
		{
			animator.Rebind();
			animator.Play(defaultStateHash, 0, 0f);
		}
		isPause = false;
		pauseStateHash = 0;
	}

	public void DestroyGameObject()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!(this.get_gameObject() == null) && (!MonoBehaviourSingleton<EffectManager>.IsValid() || !MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(this.get_gameObject(), false)))
		{
			Object.Destroy(this.get_gameObject());
		}
	}

	private void OnDisable()
	{
		if (loopEnd)
		{
			DestroyGameObject();
		}
	}

	private void __FUNCTION__PlayOneShotSE(AudioClip clip)
	{
		SoundManager.PlaySE(clip, false, _transform);
	}

	private void __FUNCTION__PlayLoopSE(AudioClip clip)
	{
		if (!(loopAudioClip == clip))
		{
			if (loopAudioObject != null)
			{
				loopAudioObject.Stop(0);
			}
			loopAudioObject = SoundManager.PlaySE(clip, true, _transform);
			if (loopAudioObject != null)
			{
				loopAudioClip = clip;
			}
		}
	}

	private void __FUNCTION__StopLoopSE()
	{
		if (loopAudioObject != null)
		{
			loopAudioObject.Stop(0);
		}
		loopAudioObject = null;
		loopAudioClip = null;
	}
}
