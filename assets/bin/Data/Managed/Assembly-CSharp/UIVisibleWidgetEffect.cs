using UnityEngine;

public class UIVisibleWidgetEffect : MonoBehaviour
{
	private UIPanel panel;

	private UIWidget widget;

	private string sectionName;

	private string effectName;

	private Transform effect;

	private int setRendererQueue = -1;

	public static void Set(UIPanel panel, UIWidget widget, string effect_name, string current_section_name)
	{
		if (!((Object)widget == (Object)null))
		{
			UIVisibleWidgetEffect uIVisibleWidgetEffect = widget.GetComponent<UIVisibleWidgetEffect>();
			if (effect_name == null)
			{
				if ((Object)uIVisibleWidgetEffect != (Object)null)
				{
					Object.Destroy(uIVisibleWidgetEffect);
				}
			}
			else
			{
				if ((Object)uIVisibleWidgetEffect == (Object)null)
				{
					uIVisibleWidgetEffect = widget.gameObject.AddComponent<UIVisibleWidgetEffect>();
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
		if (sectionName == MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() && (Object)MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() != (Object)null && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().state == UIBehaviour.STATE.OPEN && ((Object)panel == (Object)null || panel.IsVisible(widget.cachedTransform.position)))
		{
			if ((Object)effect == (Object)null)
			{
				effect = EffectManager.GetUIEffect(effectName, widget.cachedTransform, 0f, 1, widget);
				if ((Object)effect == (Object)null)
				{
					base.enabled = false;
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
		if ((Object)effect != (Object)null)
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
		if ((Object)effect != (Object)null && setRendererQueue != -1)
		{
			Renderer[] componentsInChildren = effect.GetComponentsInChildren<Renderer>(true);
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.renderQueue = setRendererQueue;
			}
		}
	}

	public void SetRendererQueue(int setQueue)
	{
		setRendererQueue = setQueue;
		_SetRendererQueue();
	}
}
