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

	private ParticleSystemRenderer[] particleRenders;

	private void Awake()
	{
		_transform = base.transform;
		if ((particles == null || particles.Length == 0) && autoCollectParticles)
		{
			particles = GetComponentsInChildren<ParticleSystem>();
			particleRenders = GetComponentsInChildren<ParticleSystemRenderer>();
		}
		if (animator == null)
		{
			Animator component = GetComponent<Animator>();
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
		if (particleRenders != null && MonoBehaviourSingleton<InGameCameraCuller>.IsValid())
		{
			int i = 0;
			for (int num = particleRenders.Length; i < num; i++)
			{
				ParticleSystem particleSystem = particles[i];
				ParticleSystemRenderer particleSystemRenderer = particleRenders[i];
				if (particleSystem != null && particleSystem.isPlaying && particleSystemRenderer != null)
				{
					particleSystemRenderer.enabled = CheckShow(particleSystemRenderer.bounds);
				}
			}
		}
		if (loop && !loopEnd)
		{
			return;
		}
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
			int j = 0;
			for (int num2 = particles.Length; j < num2; j++)
			{
				ParticleSystem particleSystem2 = particles[j];
				if (particleSystem2 != null && particleSystem2.isPlaying)
				{
					particleSystem2.Stop(withChildren: true);
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
			if (!currentAnimatorStateInfo.loop && currentAnimatorStateInfo.normalizedTime < 1f)
			{
				return;
			}
		}
		DestroyGameObject();
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
				if (particleSystem != null)
				{
					particleSystem.Stop(withChildren: true);
				}
			}
		}
		if ((changeStateToEND && animator != null && endStateHash != 0) & isPlayEndAnimation)
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
		if (animator == null)
		{
			return false;
		}
		return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateNameHash;
	}

	public void Pause(bool pause)
	{
		if (isPause == pause)
		{
			return;
		}
		if (pause)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if ((object)animator != null)
			{
				int shortNameHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
				if (shortNameHash == 0)
				{
					return;
				}
				pauseStateHash = shortNameHash;
				animator.enabled = false;
			}
			base.gameObject.SetActive(value: false);
			isPause = true;
		}
		else
		{
			base.gameObject.SetActive(value: true);
			if ((object)animator != null)
			{
				animator.enabled = true;
				animator.Play(pauseStateHash);
				pauseStateHash = 0;
			}
			isPause = false;
		}
	}

	public bool CheckShow(Bounds bound)
	{
		if (!MonoBehaviourSingleton<InGameCameraCuller>.IsValid())
		{
			return true;
		}
		return MonoBehaviourSingleton<InGameCameraCuller>.I.IsVisible(bound);
	}

	public void SetRenderQueue(int renderQueue)
	{
		if (particles != null)
		{
			if (particles.Length == 0)
			{
				particles = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
				particleRenders = GetComponentsInChildren<ParticleSystemRenderer>(includeInactive: true);
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
				if (particleSystem != null)
				{
					particleSystem.Clear(withChildren: true);
					particleSystem.Play(withChildren: true);
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
		if (!(base.gameObject == null) && (!MonoBehaviourSingleton<EffectManager>.IsValid() || !MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(base.gameObject, no_stock_to_destroy: false)))
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
		SoundManager.PlaySE(clip, loop: false, _transform);
	}

	private void __FUNCTION__PlayLoopSE(AudioClip clip)
	{
		if (!(loopAudioClip == clip))
		{
			if (loopAudioObject != null)
			{
				loopAudioObject.Stop();
			}
			loopAudioObject = SoundManager.PlaySE(clip, loop: true, _transform);
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
			loopAudioObject.Stop();
		}
		loopAudioObject = null;
		loopAudioClip = null;
	}
}
