using rhyme;
using System.Collections.Generic;
using UnityEngine;

public static class Temporary
{
	public static List<Component> componentList;

	public static List<Renderer> rendererList;

	public static List<rymFX> fxList;

	public static List<EffectCtrl> effectCtrlList;

	public static List<Trail> trailList;

	public static List<TargetPoint> targetPointList;

	public static List<Collider> colliderList;

	public static List<BoxCollider> boxColliderList;

	public static List<UISprite> uiSpriteList;

	public static List<UITexture> uiTextureList;

	public static List<UIButton> uiButtonList;

	public static List<UIWidget> uiWidgetList;

	public static List<UIRect> uiRectList;

	public static List<UILabel> uiLabelList;

	public static List<UIGameSceneEventSender> uiGameSceneEventSender;

	public static List<ItemIcon> itemIconList;

	public static List<ParticleSystem> particleList;

	public static void Initialize()
	{
		componentList = new List<Component>(32);
		rendererList = new List<Renderer>(32);
		fxList = new List<rymFX>(32);
		effectCtrlList = new List<EffectCtrl>(32);
		trailList = new List<Trail>(32);
		targetPointList = new List<TargetPoint>(32);
		colliderList = new List<Collider>(32);
		boxColliderList = new List<BoxCollider>(32);
		uiSpriteList = new List<UISprite>(32);
		uiTextureList = new List<UITexture>(32);
		uiButtonList = new List<UIButton>(32);
		uiWidgetList = new List<UIWidget>(32);
		uiRectList = new List<UIRect>(32);
		uiLabelList = new List<UILabel>(32);
		uiGameSceneEventSender = new List<UIGameSceneEventSender>(32);
		itemIconList = new List<ItemIcon>(32);
		particleList = new List<ParticleSystem>(32);
	}
}
