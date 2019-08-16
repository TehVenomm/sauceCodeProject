using rhyme;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class EeLSettings
{
	[CompilerGenerated]
	private static ResourceLoadFunc _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static SoundFunc _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static InitFxFunc _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static QueryDestroyFxFunc _003C_003Ef__mg_0024cache3;

	[CompilerGenerated]
	private static GetShaderFunc _003C_003Ef__mg_0024cache4;

	public unsafe static void Startup()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		if (_003C_003Ef__mg_0024cache0 == null)
		{
			_003C_003Ef__mg_0024cache0 = new ResourceLoadFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		rymFXManager.ResourceLoadDelegate = _003C_003Ef__mg_0024cache0;
		if (_003C_003Ef__mg_0024cache1 == null)
		{
			_003C_003Ef__mg_0024cache1 = new SoundFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		rymFXManager.PlaySoundDelegate = _003C_003Ef__mg_0024cache1;
		if (_003C_003Ef__mg_0024cache2 == null)
		{
			_003C_003Ef__mg_0024cache2 = new InitFxFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		rymFXManager.InitFxDelegate = _003C_003Ef__mg_0024cache2;
		if (_003C_003Ef__mg_0024cache3 == null)
		{
			_003C_003Ef__mg_0024cache3 = new QueryDestroyFxFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		rymFXManager.QueryDestroyFxDelegate = _003C_003Ef__mg_0024cache3;
		rymFXManager.MeshBounds = new Bounds(Vector3.get_zero(), new Vector3(100000f, 100000f, 100000f));
		rymFXManager.EnableLog = false;
		if (_003C_003Ef__mg_0024cache4 == null)
		{
			_003C_003Ef__mg_0024cache4 = new GetShaderFunc((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		rymFXManager.GetShaderDelegate = _003C_003Ef__mg_0024cache4;
		rymFXTrail.ComplementMode = 0;
		rymFXManager.ClearPoolObjects();
		rymTPool<rymXorShift>.Precreate(32);
		rymTPool<EmitParam>.Precreate(16);
		rymTPool<PtclWorkBlock>.Precreate(32);
		rymTPool<rymList<PtclWorkBlock>>.Precreate(32);
		rymTPool<PtclWork>.Precreate(512);
		rymTPool<WorkAccess>.Precreate(8);
		rymTPool<ApplyParam>.Precreate(8);
		rymTPool<rymFXParticle2ChildPlug>.Precreate(32);
		rymTPool<rymFXTrailPoint>.Precreate(64);
		rymTPool<rymList<rymFXTrailPoint>>.Precreate(32);
		rymTPool<rymFXTrailParam>.Precreate(8);
		rymTPool<rymFX4KeyAnimValue>.Precreate(32);
		rymTPool<Param>.Precreate(128);
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
		rymTPool<Param>.Precreate(16);
		object[] array = new object[96];
		int i = 0;
		int num = 0;
		for (; i < 32; i++)
		{
			rymList<Vector2> val = rymTPool<rymList<Vector2>>.Get();
			val.set_Capacity(400);
			rymList<Vector3> val2 = rymTPool<rymList<Vector3>>.Get();
			val2.set_Capacity(400);
			rymList<Color> val3 = rymTPool<rymList<Color>>.Get();
			val3.set_Capacity(400);
			array[num++] = val;
			array[num++] = val2;
			array[num++] = val3;
		}
		int j = 0;
		int num5 = 0;
		for (; j < 32; j++)
		{
			rymList<Vector2> val4 = array[num5++] as rymList<Vector2>;
			rymList<Vector3> val5 = array[num5++] as rymList<Vector3>;
			rymList<Color> val6 = array[num5++] as rymList<Color>;
			rymTPool<rymList<Vector2>>.Release(ref val4);
			rymTPool<rymList<Vector3>>.Release(ref val5);
			rymTPool<rymList<Color>>.Release(ref val6);
		}
		rymTPool<rymList<int>>.poolCountLimit = 32;
		rymFXManager.EnableMaterialCache = false;
	}

	private static void OnResourceLoad(ResourceLoadWork work)
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
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(textureNameList.get_Item(i));
					if (!string.IsNullOrEmpty(fileNameWithoutExtension))
					{
						work.textures[i] = component.Get<Texture>(fileNameWithoutExtension);
					}
				}
			}
			else
			{
				Debug.LogWarning((object)work.fx.get_name());
			}
		}
		work.fx.ResourceLoadComplete();
	}

	private static void PlaySound(rymFX fx, rymFXSoundInfo info)
	{
		if (!fx.get_enabled() || (info.loop && info.audio_source != null) || string.IsNullOrEmpty(info.clip_name))
		{
			return;
		}
		ResourceLink component = fx.GetComponent<ResourceLink>();
		if (!(component != null))
		{
			return;
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.clip_name);
		AudioClip val = component.Get<AudioClip>(fileNameWithoutExtension);
		if (val != null)
		{
			AudioObject audioObject = SoundManager.PlaySE(val, info.loop, fx.get__transform());
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
			Log.Error(LOG.RESOURCE, "{0} is not found. ({1})", fileNameWithoutExtension, fx.get_name());
		}
	}

	private static void InitFx(rymFX fx, bool binary)
	{
		if (fx.get_gameObject().get_layer() != 5)
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
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(fx.get_gameObject(), no_stock_to_destroy: false))
		{
			return false;
		}
		return true;
	}
}
