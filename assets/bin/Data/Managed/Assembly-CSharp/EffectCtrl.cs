using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
	[Header("-- Effect Settings --")]
	[Tooltip("ル\u30fcプエフェクトかどうか")]
	public bool loop;

	[Tooltip("管理対象のParticleSystemの配列")]
	public ParticleSystem[] particles;

	[Tooltip("particlesが空の場合、自動的に検索するかどうか")]
	public bool autoCollectParticles = true;

	[Tooltip("管理対象のAnimator\n空の場合はこのGameObjectにアタッチされたAnimatorが使用される")]
	public Animator animator;

	[Header("-- Loop End Behaviour --")]
	[Tooltip("ル\u30fcプを抜ける時にパ\u30fcティクルを停止するかどうか")]
	public bool stopParticle = true;

	[Tooltip("ル\u30fcプを抜ける時にAnimatorのENDを再生するかどうか")]
	public bool changeStateToEND = true;

	[Tooltip("AnimatorのENDを再生する時のクロスフェ\u30fcド時間（秒）")]
	public float crossFadeTimeToEND = 0.1f;

	[Header("-- Wait Destroy --")]
	[Tooltip("指定時間を待ってから削除（秒）\n0に設定すると待たない")]
	public float waitTime = 0.2f;

	[Tooltip("パ\u30fcティクルが全て消えてから削除")]
	public bool waitParticlePlaying = true;

	[Tooltip("アニメが最後まで再生されてから削除")]
	public bool waitAnimationPlaying = true;

	[Tooltip("同時に再生される可能性のあるAudioClip")]
	[Header("-- Audio Destroy --")]
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

	private void Awake()
	{
		_transform = base.transform;
		if ((particles == null || particles.Length == 0) && autoCollectParticles)
		{
			particles = GetComponentsInChildren<ParticleSystem>();
		}
		if ((Object)animator == (Object)null)
		{
			Animator component = GetComponent<Animator>();
			if ((Object)component != (Object)null)
			{
				animator = component;
			}
			else
			{
				animator = null;
			}
		}
		if ((Object)animator != (Object)null)
		{
			defaultStateHash = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			int stateID = Animator.StringToHash("END");
			if (animator.HasState(0, stateID))
			{
				endStateHash = stateID;
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
		if (!loop || loopEnd)
		{
			if (waitTime > 0f)
			{
				timer += Time.deltaTime;
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
					ParticleSystem particleSystem = particles[i];
					if ((Object)particleSystem != (Object)null && particleSystem.isPlaying)
					{
						particleSystem.Stop(true);
						return;
					}
				}
			}
			if (waitAnimationPlaying && (Object)animator != (Object)null)
			{
				if (animator.IsInTransition(0))
				{
					return;
				}
				AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.loop && currentAnimatorStateInfo.normalizedTime < 1f)
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
				ParticleSystem particleSystem = particles[i];
				if ((Object)particleSystem != (Object)null)
				{
					particleSystem.Stop(true);
				}
			}
		}
		if (changeStateToEND && (Object)animator != (Object)null && endStateHash != 0 && isPlayEndAnimation)
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
		if (!((Object)animator == (Object)null) && animator.HasState(0, stateNameHash))
		{
			animator.Play(stateNameHash);
		}
	}

	public void CrossFade(int stateNameHash, float transitionDuration)
	{
		if (!((Object)animator == (Object)null) && animator.HasState(0, stateNameHash))
		{
			animator.CrossFade(stateNameHash, transitionDuration);
		}
	}

	public bool IsCurrentState(int stateNameHash)
	{
		if ((Object)animator == (Object)null)
		{
			return false;
		}
		return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateNameHash;
	}

	public void Pause(bool pause)
	{
		if (isPause != pause)
		{
			if (pause)
			{
				if (base.gameObject.activeInHierarchy)
				{
					if (!object.ReferenceEquals(animator, null))
					{
						int shortNameHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
						if (shortNameHash == 0)
						{
							return;
						}
						pauseStateHash = shortNameHash;
						animator.Stop();
					}
					base.gameObject.SetActive(false);
					isPause = true;
				}
			}
			else
			{
				base.gameObject.SetActive(true);
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
		if (particles != null)
		{
			if (particles.Length == 0)
			{
				particles = GetComponentsInChildren<ParticleSystem>(true);
			}
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].GetComponent<ParticleSystemRenderer>().sharedMaterial.renderQueue = renderQueue;
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
				ParticleSystem particleSystem = particles[i];
				if ((Object)particleSystem != (Object)null)
				{
					particleSystem.Clear(true);
					particleSystem.Play(true);
				}
			}
		}
		if ((Object)animator != (Object)null && defaultStateHash != 0)
		{
			animator.Rebind();
			animator.Play(defaultStateHash, 0, 0f);
		}
		isPause = false;
		pauseStateHash = 0;
	}

	public void DestroyGameObject()
	{
		if (!((Object)base.gameObject == (Object)null) && (!MonoBehaviourSingleton<EffectManager>.IsValid() || !MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(base.gameObject, false)))
		{
			Object.Destroy(base.gameObject);
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
		if (!((Object)loopAudioClip == (Object)clip))
		{
			if ((Object)loopAudioObject != (Object)null)
			{
				loopAudioObject.Stop(0);
			}
			loopAudioObject = SoundManager.PlaySE(clip, true, _transform);
			if ((Object)loopAudioObject != (Object)null)
			{
				loopAudioClip = clip;
			}
		}
	}

	private void __FUNCTION__StopLoopSE()
	{
		if ((Object)loopAudioObject != (Object)null)
		{
			loopAudioObject.Stop(0);
		}
		loopAudioObject = null;
		loopAudioClip = null;
	}
}
