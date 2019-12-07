using rhyme;
using System.IO;
using System.Text;
using UnityEngine;

public static class EeLSettings
{
	public static void Startup()
	{
		rymFXManager.ResourceLoadDelegate = OnResourceLoad;
		rymFXManager.PlaySoundDelegate = PlaySound;
		rymFXManager.InitFxDelegate = InitFx;
		rymFXManager.QueryDestroyFxDelegate = OnQueryDestroyFx;
		rymFXManager.MeshBounds = new Bounds(Vector3.zero, new Vector3(100000f, 100000f, 100000f));
		rymFXManager.EnableLog = false;
		rymFXManager.GetShaderDelegate = GetShader;
		rymFXTrail.ComplementMode = 0;
		rymFXManager.ClearPoolObjects();
		rymTPool<rymXorShift>.Precreate(32);
		rymTPool<rymFXParticle2.EmitParam>.Precreate(16);
		rymTPool<rymFXParticle2.PtclWorkBlock>.Precreate(32);
		rymTPool<rymList<rymFXParticle2.PtclWorkBlock>>.Precreate(32);
		rymTPool<rymFXParticle2.PtclWork>.Precreate(512);
		rymTPool<rymFX4KeyAnimValue.WorkAccess>.Precreate(8);
		rymTPool<rymFXParticleForceBase.ApplyParam>.Precreate(8);
		rymTPool<rymFXParticle2ChildPlug>.Precreate(32);
		rymTPool<rymFXTrail.rymFXTrailPoint>.Precreate(64);
		rymTPool<rymList<rymFXTrail.rymFXTrailPoint>>.Precreate(32);
		rymTPool<rymFXTrail.rymFXTrailParam>.Precreate(8);
		rymTPool<rymFX4KeyAnimValue>.Precreate(32);
		rymTPool<rymFX4KeyAnimValue.Param>.Precreate(128);
		rymTPool<rymMemReader>.Precreate(1);
		rymTPool<StringBuilder>.Precreate(1);
		rymTPool<rymList<string>>.Precreate(16);
		rymTPool<rymList<float>>.Precreate(512);
		rymTPool<rymList<int>>.Precreate(16);
		rymTPool<rymFXWorkFixPos>.Precreate(8);
		rymTPool<rymFXWorkFixRot>.Precreate(8);
		rymTPool<rymFXWorkFixScale>.Precreate(8);
		rymTPool<rymFXWorkFixColor>.Precreate(8);
		rymTPool<rymFXWorkLinearPos>.Precreate(8);
		rymTPool<rymFXWorkLinearRot>.Precreate(8);
		rymTPool<rymFXWorkLinearScale>.Precreate(8);
		rymTPool<rymFXWorkLinearColor>.Precreate(8);
		rymTPool<rymList<rymFXWork>>.Precreate(32);
		rymTPool<rymFXSpeedForce>.Precreate(8);
		rymTPool<rymFXAccelForce>.Precreate(8);
		rymTPool<rymFXShakeForce>.Precreate(8);
		rymTPool<rymFXScrewForce>.Precreate(8);
		rymTPool<rymFXAbsorbForce>.Precreate(8);
		rymTPool<rymList<rymFXParticleForceBase>>.Precreate(32);
		rymTPool<rymFXSoundTrigger>.Precreate(8);
		rymTPool<rymList<rymFXTrigger>>.Precreate(8);
		rymTPool<rymFXPlug>.Precreate(32);
		rymTPool<rymList<rymFXPlug>>.Precreate(16);
		rymTPool<rymList<int>>.Precreate(16);
		rymTPool<rymFXSoundInfo>.Precreate(4);
		rymTPool<rymList<rymFXSoundInfo>>.Precreate(4);
		rymTPool<rymFXParticle2>.Precreate(16);
		rymTPool<rymFXSprite>.Precreate(16);
		rymTPool<rymFXTrail>.Precreate(8);
		rymTPool<rymList<rymFXObject>>.Precreate(16);
		rymTPool<rymFXParticle2ChildWork>.Precreate(16);
		rymTPool<rymFXParticle2ChildParam>.Precreate(16);
		rymTPool<rymList<rymFXObjectBase>>.Precreate(32);
		rymTPool<rymFXParticle2.Param>.Precreate(16);
		object[] array = new object[96];
		int i = 0;
		int num = 0;
		for (; i < 32; i++)
		{
			rymList<Vector2> rymList = rymTPool<rymList<Vector2>>.Get();
			rymList.Capacity = 400;
			rymList<Vector3> rymList2 = rymTPool<rymList<Vector3>>.Get();
			rymList2.Capacity = 400;
			rymList<Color> rymList3 = rymTPool<rymList<Color>>.Get();
			rymList3.Capacity = 400;
			array[num++] = rymList;
			array[num++] = rymList2;
			array[num++] = rymList3;
		}
		int j = 0;
		int num2 = 0;
		for (; j < 32; j++)
		{
			rymList<Vector2> obj = array[num2++] as rymList<Vector2>;
			rymList<Vector3> obj2 = array[num2++] as rymList<Vector3>;
			rymList<Color> obj3 = array[num2++] as rymList<Color>;
			rymTPool<rymList<Vector2>>.Release(ref obj);
			rymTPool<rymList<Vector3>>.Release(ref obj2);
			rymTPool<rymList<Color>>.Release(ref obj3);
		}
		rymTPool<rymList<int>>.poolCountLimit = 32;
		rymFXManager.EnableMaterialCache = false;
	}

	private static void OnResourceLoad(rymFX.ResourceLoadWork work)
	{
		if (work.fx == null)
		{
			return;
		}
		ResourceLink component = work.fx.GetComponent<ResourceLink>();
		if (component != null)
		{
			rymList<string> textureNameList = work.fx.GetTextureNameList();
			if (textureNameList != null)
			{
				int i = 0;
				for (int size = textureNameList.size; i < size; i++)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(textureNameList[i]);
					if (!string.IsNullOrEmpty(fileNameWithoutExtension))
					{
						work.textures[i] = component.Get<Texture>(fileNameWithoutExtension);
					}
				}
			}
			else
			{
				Debug.LogWarning(work.fx.name);
			}
		}
		work.fx.ResourceLoadComplete();
	}

	private static void PlaySound(rymFX fx, rymFXSoundInfo info)
	{
		if (!fx.enabled || (info.loop && info.audio_source != null) || string.IsNullOrEmpty(info.clip_name))
		{
			return;
		}
		ResourceLink component = fx.GetComponent<ResourceLink>();
		if (!(component != null))
		{
			return;
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.clip_name);
		AudioClip audioClip = component.Get<AudioClip>(fileNameWithoutExtension);
		if (audioClip != null)
		{
			AudioObject audioObject = SoundManager.PlaySE(audioClip, info.loop, fx._transform);
			if (audioObject != null && info.loop)
			{
				EffectInfoComponent component2 = fx.GetComponent<EffectInfoComponent>();
				if (component2 != null)
				{
					component2.SetLoopAudioObject(audioObject);
				}
			}
		}
		else
		{
			Log.Error(LOG.RESOURCE, "{0} is not found. ({1})", fileNameWithoutExtension, fx.name);
		}
	}

	private static void InitFx(rymFX fx, bool binary)
	{
		if (fx.gameObject.layer != 5)
		{
			SceneSettingsManager.ApplyEffect(fx, force: false);
		}
	}

	private static Shader GetShader(string name)
	{
		return ResourceUtility.FindShader(name);
	}

	private static bool OnQueryDestroyFx(rymFX fx)
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(fx.gameObject, no_stock_to_destroy: false))
		{
			return false;
		}
		return true;
	}
}
