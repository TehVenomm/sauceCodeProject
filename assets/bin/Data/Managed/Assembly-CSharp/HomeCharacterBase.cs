using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomeCharacterBase : MonoBehaviour
{
	public enum STATE
	{
		FREE,
		OUT_CONTROLL,
		BACK_POSITION,
		LEAVE,
		STOP,
		STOP_END
	}

	public Vector3 defaultPosition = Vector3.zero;

	public Quaternion defaultRotation = Quaternion.identity;

	private static readonly PLCA[] talkAnims = new PLCA[3]
	{
		PLCA.IDLE_01,
		PLCA.IDLE_02,
		PLCA.IDLE_03
	};

	protected int sexType;

	protected HomePeople homePeople;

	protected Transform namePlate;

	protected Animator animator;

	protected PlayerAnimCtrl animCtrl;

	protected CapsuleCollider moveCollider;

	protected TransformInterpolator interpolator;

	protected WayPoint wayPoint;

	protected List<WayPoint> wayHistory = new List<WayPoint>();

	protected float waitTime;

	protected float discussionTimer;

	protected STATE state;

	protected ManualCoroutineList coroutines = new ManualCoroutineList();

	public bool isLoading
	{
		get;
		protected set;
	}

	public ModelLoaderBase loader
	{
		get;
		protected set;
	}

	public Transform _transform
	{
		get;
		private set;
	}

	public Vector3 moveTargetPos
	{
		get;
		protected set;
	}

	public Vector3 emotionTargetPos
	{
		get;
		set;
	}

	public bool isStop
	{
		get
		{
			if ((Object)animator == (Object)null)
			{
				return true;
			}
			return !animator.applyRootMotion;
		}
	}

	public void SetHomePeople(HomePeople homePeople)
	{
		this.homePeople = homePeople;
	}

	public void SetWaitTime(float time)
	{
		waitTime = time;
	}

	public void SetWayPoint(WayPoint wayPoint)
	{
		this.wayPoint = wayPoint;
	}

	public virtual FriendCharaInfo GetFriendCharaInfo()
	{
		return null;
	}

	public Transform GetNamePlate()
	{
		return namePlate;
	}

	public void StopDiscussion()
	{
		discussionTimer = -1f;
	}

	public virtual bool DispatchEvent()
	{
		return false;
	}

	public void PushOutControll()
	{
		state = STATE.OUT_CONTROLL;
		coroutines.Push(1);
	}

	public void PushBackPosition()
	{
		state = STATE.BACK_POSITION;
		coroutines.Push(2);
	}

	public void PushLeave()
	{
		state = STATE.LEAVE;
		coroutines.Push(3);
	}

	public void PopState()
	{
		coroutines.Pop();
		state = (STATE)coroutines.Peek();
	}

	public void StopMoving()
	{
		if (state != STATE.STOP)
		{
			state = STATE.STOP;
			if (this is HomePlayerCharacter)
			{
				animCtrl.PlayIdleAnims(sexType, false);
			}
			if (this is LoungeMoveNPC)
			{
				animCtrl.PlayDefault(false);
			}
			coroutines.Push(4);
		}
	}

	public void SetVisible(bool is_visible)
	{
		if (!((Object)loader == (Object)null) && !isLoading)
		{
			loader.SetEnabled(is_visible);
			moveCollider.enabled = is_visible;
			if ((Object)namePlate != (Object)null)
			{
				namePlate.gameObject.SetActive(is_visible);
			}
			base.enabled = is_visible;
		}
	}

	protected abstract ModelLoaderBase LoadModel();

	protected virtual void InitAnim()
	{
		animCtrl = PlayerAnimCtrl.Get(animator, (sexType != 0) ? PLCA.IDLE_01_F : PLCA.IDLE_01, OnAnimPlay, null, OnAnimEnd);
		animCtrl.moveAnim = PLCA.WALK;
	}

	protected virtual void InitCollider()
	{
		SetCollider(1.8f, 0.5f);
	}

	protected void SetCollider(float height, float radius)
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.isKinematic = true;
		rigidbody.drag = 0f;
		rigidbody.angularDrag = 100f;
		rigidbody.useGravity = false;
		rigidbody.constraints = (RigidbodyConstraints)84;
		CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
		capsuleCollider.direction = 1;
		capsuleCollider.height = height;
		capsuleCollider.radius = radius;
		capsuleCollider.center = new Vector3(0f, height * 0.5f, 0f);
		moveCollider = capsuleCollider;
	}

	protected virtual bool IsVisibleNamePlate()
	{
		return true;
	}

	protected virtual void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		animator.applyRootMotion = ((anim == anim_ctrl.moveAnim) ? true : false);
	}

	protected void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (this is LoungeMoveNPC)
		{
			animCtrl.PlayDefault(false);
		}
		else
		{
			animCtrl.PlayIdleAnims(sexType, false);
		}
	}

	private void Awake()
	{
		isLoading = true;
		_transform = base.transform;
	}

	private IEnumerator Start()
	{
		loader = LoadModel();
		while (loader.IsLoading())
		{
			yield return (object)null;
		}
		CreateNamePlate();
		UpdateNamePlatePos();
		InitCollider();
		ChangeScale();
		interpolator = base.gameObject.AddComponent<TransformInterpolator>();
		animator = loader.GetAnimator();
		if (!((Object)animator == (Object)null))
		{
			animator.gameObject.AddComponent<RootMotionProxy>();
			InitAnim();
			coroutines.Add(new ManualCoroutine(0, this, DoFreeMove(), false, null, null));
			coroutines.Add(new ManualCoroutine(1, this, DoOutControll(), false, null, null));
			coroutines.Add(new ManualCoroutine(2, this, DoBackPosition(), false, null, null));
			coroutines.Add(new ManualCoroutine(3, this, DoLeave(), false, null, null));
			coroutines.Add(new ManualCoroutine(4, this, DoStop(), false, null, null));
			coroutines.Push(0);
			isLoading = false;
		}
	}

	protected virtual void CreateNamePlate()
	{
		if (GetFriendCharaInfo() != null)
		{
			namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateNamePlate(GetFriendCharaInfo().name);
		}
	}

	protected virtual void ChangeScale()
	{
	}

	private void LateUpdate()
	{
		UpdateNamePlatePos();
	}

	protected virtual void UpdateNamePlatePos()
	{
		if (!((Object)namePlate == (Object)null))
		{
			if (!GameSaveData.instance.headName)
			{
				namePlate.gameObject.SetActive(false);
			}
			else
			{
				Vector3 position = _transform.position + new Vector3(0f, 1.9f, 0f);
				position = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(position);
				position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
				if (position.z >= 0f && IsVisibleNamePlate())
				{
					position.z = 0f;
					namePlate.gameObject.SetActive(true);
					namePlate.position = position;
				}
				else
				{
					namePlate.gameObject.SetActive(false);
				}
			}
		}
	}

	private bool CheckBackPosition(Vector3 startDir)
	{
		Vector3 from = defaultPosition - _transform.position;
		from.Normalize();
		Vector3 position = _transform.position;
		if (position.x < defaultPosition.x || position.z > defaultPosition.z)
		{
			return true;
		}
		float angle = 0f - Vector3.Angle(from, Vector3.forward);
		_transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
		return false;
	}

	protected virtual void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			if ((Object)homePeople != (Object)null)
			{
				homePeople.OnDestroyHomeCharacter(this);
			}
			if ((Object)namePlate != (Object)null)
			{
				Object.DestroyImmediate(namePlate.gameObject);
				namePlate = null;
			}
		}
	}

	private IEnumerator DoFreeMove()
	{
		while (true)
		{
			if (waitTime > 0f)
			{
				WaitInFreeMove();
				yield return (object)null;
			}
			else if (discussionTimer != 0f)
			{
				if (discussionTimer < 0f)
				{
					if (this is HomeNPCCharacter && (Object)homePeople.selfChara != (Object)null)
					{
						HomeNPCCharacter npc = (HomeNPCCharacter)this;
						if ((Object)npc != (Object)null && npc.nearAnim != PLCA.IDLE_01)
						{
							if ((homePeople.selfChara._transform.position.ToVector2XZ() - _transform.position.ToVector2XZ()).sqrMagnitude < 9f)
							{
								PlayNearAnim(npc);
							}
							else if (animCtrl.playingAnim != animCtrl.defaultAnim)
							{
								animCtrl.PlayDefault(false);
							}
							yield return (object)null;
							continue;
						}
					}
					if (this is HomePlayerCharacter)
					{
						if (Random.Range(0, 10) == 0)
						{
							animCtrl.Play(PlayerAnimCtrl.emotionAnims, false);
							discussionTimer = Random.Range(2f, 4f);
						}
						else if (Random.Range(0, 2) == 0)
						{
							animCtrl.Play(PlayerAnimCtrl.talkAnims, false);
							discussionTimer = Random.Range(5f, 10f);
						}
						else
						{
							animCtrl.PlayIdleAnims(sexType, false);
							discussionTimer = Random.Range(3f, 6f);
						}
					}
				}
				else
				{
					discussionTimer -= Time.deltaTime;
				}
				yield return (object)null;
			}
			else
			{
				SetupNextWayPoint();
				yield return (object)null;
				if (MonoBehaviourSingleton<HomeManager>.IsValid())
				{
					moveTargetPos = MonoBehaviourSingleton<HomeManager>.I.HomePeople.GetTargetPos(this, wayPoint);
				}
				else if (MonoBehaviourSingleton<LoungeManager>.IsValid())
				{
					moveTargetPos = MonoBehaviourSingleton<LoungeManager>.I.HomePeople.GetTargetPos(this, wayPoint);
				}
				while (true)
				{
					animCtrl.PlayMove(false);
					Vector3 pos = _transform.position;
					Vector3 diff = moveTargetPos - pos;
					Vector2 dir3 = diff.ToVector2XZ().normalized;
					Vector3 eulerAngles = Quaternion.LookRotation(dir3.ToVector3XZ()).eulerAngles;
					float rot2 = eulerAngles.y;
					float vel2 = 0f;
					Vector3 eulerAngles2 = _transform.eulerAngles;
					rot2 = Mathf.SmoothDampAngle(eulerAngles2.y, rot2, ref vel2, 0.1f);
					_transform.eulerAngles = new Vector3(0f, rot2, 0f);
					if (diff.magnitude < 0.75f)
					{
						break;
					}
					yield return (object)null;
				}
				if (wayPoint.name.StartsWith("LEAF"))
				{
					break;
				}
				if (wayPoint.name.StartsWith("WAIT"))
				{
					while (true)
					{
						Vector3 eulerAngles3 = wayPoint.transform.eulerAngles;
						float dir2 = eulerAngles3.y;
						float vel = 0f;
						Vector3 eulerAngles4 = _transform.eulerAngles;
						dir2 = Mathf.SmoothDampAngle(eulerAngles4.y, dir2, ref vel, 0.1f);
						_transform.eulerAngles = new Vector3(0f, dir2, 0f);
						vel = Mathf.Abs(vel);
						if (vel > 15f)
						{
							animCtrl.Play(PLCA.WALK, false);
						}
						else if (!string.IsNullOrEmpty(wayPoint.waitAnimStateName))
						{
							PLCA motion = PlayerAnimCtrl.StringToEnum(wayPoint.waitAnimStateName);
							animCtrl.Play(motion, false);
						}
						else if (this is LoungeMoveNPC)
						{
							animCtrl.PlayDefault(false);
						}
						else
						{
							animCtrl.PlayIdleAnims(sexType, false);
						}
						animator.applyRootMotion = false;
						if (vel < 0.01f)
						{
							break;
						}
						yield return (object)null;
					}
					animCtrl.PlayIdleAnims(sexType, false);
					waitTime = Random.Range(3f, 8f);
				}
				else if (wayPoint.name == "CENTER")
				{
					waitTime = Random.Range(-3f, 8f);
				}
			}
		}
		Object.Destroy(base.gameObject);
	}

	protected virtual void PlayNearAnim(HomeNPCCharacter npc)
	{
		animCtrl.Play(npc.nearAnim, false);
	}

	private void WaitInFreeMove()
	{
		waitTime -= Time.deltaTime;
		if (!string.IsNullOrEmpty(wayPoint.waitAnimStateName))
		{
			PLCA anim = PlayerAnimCtrl.StringToEnum(wayPoint.waitAnimStateName);
			animCtrl.Play(anim, false);
		}
		else if (this is LoungeMoveNPC)
		{
			animCtrl.PlayDefault(false);
		}
		else
		{
			animCtrl.PlayIdleAnims(sexType, false);
		}
	}

	private void SetupNextWayPoint()
	{
		if (!wayHistory.Contains(this.wayPoint))
		{
			wayHistory.Add(this.wayPoint);
		}
		List<WayPoint> list = new List<WayPoint>();
		int i = 0;
		for (int num = this.wayPoint.links.Length; i < num; i++)
		{
			WayPoint wayPoint = this.wayPoint.links[i];
			if (!((Object)wayPoint == (Object)null) && !wayHistory.Contains(wayPoint))
			{
				list.Add(wayPoint);
			}
		}
		if (list.Count > 0)
		{
			this.wayPoint = list[Random.Range(0, list.Count)];
		}
		else
		{
			this.wayPoint = this.wayPoint.links[Random.Range(0, this.wayPoint.links.Length)];
		}
	}

	private IEnumerator DoOutControll()
	{
		while (true)
		{
			yield return (object)null;
		}
	}

	private IEnumerator DoBackPosition()
	{
		while (true)
		{
			yield return (object)null;
			float maxAngle = 320f;
			_transform.rotation = Quaternion.AngleAxis(maxAngle, Vector3.up);
			animCtrl.SetMoveRunAnim(1);
			animCtrl.Play(PLCA.RUN_F, false);
			float stumblePercent = (float)Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).stumblePercent;
			Vector3 savePos2;
			if (stumblePercent > (float)Random.Range(0, 100))
			{
				yield return (object)new WaitForSeconds(0.5f);
				animCtrl.moveAnim = PLCA.STUN;
				animCtrl.Play(PLCA.STUN, false);
				yield return (object)new WaitForSeconds(0.5f);
				savePos2 = _transform.position;
				while (!animCtrl.IsPlayingIdleAnims(0))
				{
					_transform.position = savePos2;
					yield return (object)null;
				}
				animCtrl.moveAnim = PLCA.RUN_F;
				animCtrl.Play(PLCA.RUN_F, false);
			}
			Vector3 startDir = defaultPosition - _transform.position;
			startDir.Normalize();
			while (!CheckBackPosition(startDir))
			{
				yield return (object)null;
			}
			animCtrl.PlayDefault(false);
			yield return (object)new WaitForSeconds(0.3f);
			savePos2 = _transform.position;
			animCtrl.moveAnim = PLCA.TURN_L;
			animCtrl.Play(PLCA.TURN_L, false);
			yield return (object)null;
			while (!animCtrl.IsPlayingIdleAnims(0))
			{
				_transform.position = savePos2;
				yield return (object)null;
			}
			animCtrl.SetMoveRunAnim(1);
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private IEnumerator DoLeave()
	{
		while (true)
		{
			yield return (object)null;
			while (!animCtrl.IsPlayingIdleAnims(0))
			{
				yield return (object)null;
			}
			base.gameObject.SetActive(false);
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private IEnumerator DoStop()
	{
		while (true)
		{
			yield return (object)null;
			if (this is HomePlayerCharacter)
			{
				animCtrl.Play(talkAnims, false);
			}
			while (!GetSelfCharacter().IsEnableControl())
			{
				yield return (object)null;
			}
			if (this is HomePlayerCharacter)
			{
				animCtrl.PlayIdleAnims(sexType, false);
			}
			if (this is LoungeMoveNPC)
			{
				animCtrl.PlayDefault(false);
			}
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private HomeSelfCharacter GetSelfCharacter()
	{
		return MonoBehaviourSingleton<HomeManager>.IsValid() ? MonoBehaviourSingleton<HomeManager>.I.HomePeople.selfChara : ((!MonoBehaviourSingleton<LoungeManager>.IsValid()) ? MonoBehaviourSingleton<GuildStageManager>.I.HomePeople.selfChara : MonoBehaviourSingleton<LoungeManager>.I.HomePeople.selfChara);
	}
}
