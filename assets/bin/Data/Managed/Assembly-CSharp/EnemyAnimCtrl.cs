using System;
using UnityEngine;

public class EnemyAnimCtrl : MonoBehaviour, IAnimEvent
{
	private Action onSignal;

	private AnimEventProcessor animEvent;

	private EnemyLoader loader;

	private Camera renderCamera;

	private bool isFieldQuest;

	protected CharacterStampCtrl stepCtrl;

	protected bool enableEventMove;

	protected Vector3 eventMoveVelocity = Vector3.zero;

	public Transform blurNode
	{
		get;
		protected set;
	}

	public void Init(EnemyLoader _loader, Camera render_camrea, bool is_field_quest = false)
	{
		loader = _loader;
		renderCamera = render_camrea;
		isFieldQuest = is_field_quest;
		animEvent = new AnimEventProcessor(_loader.animEventData, _loader.animator, this);
		loader.body.gameObject.AddComponent<EnemyAnimCtrlProxy>().enemyAnimCtrl = this;
		if (!isFieldQuest)
		{
			return;
		}
		EnemyParam componentInChildren = base.gameObject.GetComponentInChildren<EnemyParam>();
		if (componentInChildren != null)
		{
			if (componentInChildren.stampInfos != null && componentInChildren.stampInfos.Length != 0)
			{
				stepCtrl = base.gameObject.AddComponent<CharacterStampCtrl>();
				stepCtrl.Init(componentInChildren.stampInfos, null, is_direction: true);
				stepCtrl.stampDistance = 999f;
				stepCtrl.effectLayer = 18;
			}
			UnityEngine.Object.DestroyImmediate(componentInChildren);
			componentInChildren = null;
		}
	}

	public void OnAnimatorMove()
	{
		if (loader != null && loader.animator != null && loader.animator.applyRootMotion)
		{
			base.transform.position += loader.animator.deltaPosition;
			base.transform.rotation *= loader.animator.deltaRotation;
		}
	}

	private void Update()
	{
		if (animEvent != null)
		{
			animEvent.Update();
		}
		if (enableEventMove)
		{
			base.transform.position += eventMoveVelocity * Time.deltaTime;
		}
	}

	public void OnAnimEvent(AnimEventData.EventData data)
	{
		if (stepCtrl != null && stepCtrl.OnAnimEvent(data))
		{
			return;
		}
		switch (data.id)
		{
		case AnimEventFormat.ID.RADIAL_BLUR_START:
		{
			float time3 = data.floatArgs[0];
			float num3 = data.floatArgs[1];
			string name = data.stringArgs[0];
			bool flag = (data.intArgs[0] != 0) ? true : false;
			Transform transform = Utility.Find(loader.body, name);
			if (transform == null)
			{
				transform = loader.body;
			}
			if (renderCamera == null && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				if (flag)
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time3, num3, transform);
				}
				else
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time3, num3, transform.position);
				}
			}
			else
			{
				Vector2 center = renderCamera.WorldToScreenPoint(transform.position);
				center.x /= Screen.width;
				center.y = Mathf.Lerp(0.5f, 1f, center.y / (float)Screen.height);
				MonoBehaviourSingleton<FilterManager>.I.StartTubulanceFilter(num3, center, null);
			}
			if (!isFieldQuest)
			{
				loader.animator.speed = 0f;
				if (onSignal != null)
				{
					onSignal();
					onSignal = null;
				}
			}
			break;
		}
		case AnimEventFormat.ID.RADIAL_BLUR_CHANGE:
		{
			float time2 = data.floatArgs[0];
			float num = data.floatArgs[1];
			if (num <= 0f)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time2);
			}
			else
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.ChangeRadialBlurFilter(time2, num);
			}
			break;
		}
		case AnimEventFormat.ID.RADIAL_BLUR_END:
		{
			float time = data.floatArgs[0];
			MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time);
			break;
		}
		case AnimEventFormat.ID.SE_ONESHOT:
		{
			int num2 = data.intArgs[0];
			if (num2 != 0)
			{
				SoundManager.PlayOneShotSE(num2, null, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform);
			}
			break;
		}
		case AnimEventFormat.ID.FIELD_QUEST_UI_OPEN:
			if (isFieldQuest && onSignal != null)
			{
				onSignal();
				onSignal = null;
			}
			break;
		case AnimEventFormat.ID.MOVE_FORWARD_START:
		{
			float d = data.floatArgs[0];
			enableEventMove = true;
			eventMoveVelocity = Vector3.forward * d;
			break;
		}
		case AnimEventFormat.ID.MOVE_END:
			enableEventMove = false;
			eventMoveVelocity = Vector3.zero;
			break;
		}
	}

	public void PlayQuestStartAnim(Action on_complete)
	{
		if (loader == null || loader.animEventData == null || !loader.animEventData.name.Contains("QENM"))
		{
			on_complete?.Invoke();
			return;
		}
		onSignal = on_complete;
		if (isFieldQuest)
		{
			loader.animator.CrossFade("ATTACK_FIELD", 0f);
		}
		else
		{
			loader.animator.CrossFade("ATTACK", 0.1f);
		}
	}
}
