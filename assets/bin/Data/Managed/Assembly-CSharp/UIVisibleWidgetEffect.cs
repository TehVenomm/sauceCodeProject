using UnityEngine;

public class UIVisibleWidgetEffect
{
	private UIPanel panel;

	private UIWidget widget;

	private string sectionName;

	private string effectName;

	private Transform effect;

	private int setRendererQueue = -1;

	public UIVisibleWidgetEffect()
		: this()
	{
	}

	public static void Set(UIPanel panel, UIWidget widget, string effect_name, string current_section_name)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (!(widget == null))
		{
			UIVisibleWidgetEffect uIVisibleWidgetEffect = widget.GetComponent<UIVisibleWidgetEffect>();
			if (effect_name == null)
			{
				if (uIVisibleWidgetEffect != null)
				{
					Object.Destroy(uIVisibleWidgetEffect);
				}
			}
			else
			{
				if (uIVisibleWidgetEffect == null)
				{
					uIVisibleWidgetEffect = widget.get_gameObject().AddComponent<UIVisibleWidgetEffect>();
				}
				uIVisibleWidgetEffect.panel = panel;
				uIVisibleWidgetEffect.widget = widget;
				if (string.IsNullOrEmpty(uIVisibleWidgetEffect.sectionName))
				{
					uIVisibleWidgetEffect.sectionName = (current_section_name ?? MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
				}
				if (uIVisibleWidgetEffect.effectName != effect_name)
				{
					uIVisibleWidgetEffect.DeleteEffect();
				}
				uIVisibleWidgetEffect.effectName = effect_name;
			}
		}
	}

	public static void OneShot(UIPanel panel, UIWidget widget, string effect_name, string current_section_name)
	{
		EffectManager.GetUIEffect(effect_name, widget.cachedTransform, 0f, 1, widget);
	}

	private void LateUpdate()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (sectionName == MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() != null && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().state == UIBehaviour.STATE.OPEN && (panel == null || panel.IsVisible(widget.cachedTransform.get_position())))
		{
			if (effect == null)
			{
				effect = EffectManager.GetUIEffect(effectName, widget.cachedTransform, 0f, 1, widget);
				if (effect == null)
				{
					this.set_enabled(false);
				}
				else
				{
					_SetRendererQueue();
				}
			}
		}
		else
		{
			DeleteEffect();
		}
	}

	private void DeleteEffect()
	{
		if (effect != null)
		{
			EffectManager.ReleaseEffect(ref effect);
		}
	}

	private void OnDisable()
	{
		DeleteEffect();
	}

	private void _SetRendererQueue()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (effect != null && setRendererQueue != -1)
		{
			Renderer[] componentsInChildren = effect.GetComponentsInChildren<Renderer>(true);
			Renderer[] array = componentsInChildren;
			foreach (Renderer val in array)
			{
				val.get_material().set_renderQueue(setRendererQueue);
			}
		}
	}

	public void SetRendererQueue(int setQueue)
	{
		setRendererQueue = setQueue;
		_SetRendererQueue();
	}
}
