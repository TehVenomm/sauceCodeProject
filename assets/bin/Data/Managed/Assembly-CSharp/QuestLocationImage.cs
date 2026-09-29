// Decompiled with JetBrains decompiler
// Type: QuestLocationImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
[ExecuteInEditMode]
public class QuestLocationImage : MonoBehaviour
{
  public const int WIDTH = 480;
  public const int HEIGHT = 344;
  public const float ASPECT = 0.716666639f;
  public const int TEX_WIDTH = 1024 /*0x0400*/;
  public const int TEX_HEIGHT = 734;
  public GameObject skyPrefab;
  public Transform spritesNode;
  private RenderTexture renderTexture;

  public Transform sky { get; private set; }

  private void Awake()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
    {
      Camera component = ((Component) this).GetComponent<Camera>();
      if (!Object.op_Inequality((Object) component.targetTexture, (Object) null))
        return;
      component.targetTexture.DiscardContents();
      component.targetTexture = (RenderTexture) null;
    }
    else
      this.Init();
  }

  private void OnEnable()
  {
    if (Application.isPlaying)
      return;
    this.Init();
  }

  private void OnValidate()
  {
    if (Application.isPlaying)
      return;
    this.Init();
  }

  public void Init(int w = 0, int h = 0)
  {
    if (w == 0)
    {
      w = 480;
      h = 344;
    }
    else if (w > 1024 /*0x0400*/)
    {
      h = (int) ((double) w / 1024.0 * 734.0);
      w = 1024 /*0x0400*/;
    }
    if (Object.op_Inequality((Object) this.spritesNode, (Object) null))
    {
      float num = 46.8664856f;
      ((Component) this.spritesNode).transform.localScale = new Vector3(num, num, 1f);
    }
    Camera component = ((Component) this).GetComponent<Camera>();
    ((Component) this).transform.position = new Vector3(0.0f, 500f, 0.0f);
    Utility.SetLayerWithChildren(((Component) this).transform, 5);
    if (Object.op_Equality((Object) component.targetTexture, (Object) null))
    {
      int num = Application.isPlaying ? 1 : 0;
      RenderTexture renderTexture = new RenderTexture(w, h, 24, (RenderTextureFormat) 7);
      if (!Application.isPlaying)
        ((Object) renderTexture).hideFlags = (HideFlags) 61;
      ((Object) renderTexture).name = "(QuestLocationImage)";
      ((Texture) renderTexture).filterMode = (FilterMode) 0;
      component.targetTexture = renderTexture;
      this.renderTexture = renderTexture;
    }
    component.cullingMask = 32 /*0x20*/;
    component.nearClipPlane = 0.0f;
    component.farClipPlane = 100f;
    component.clearFlags = (CameraClearFlags) 2;
    component.backgroundColor = Color.black;
    component.orthographic = true;
    component.orthographicSize = (float) ((int) (480.0 / (double) w * (double) h) / 2 - 1);
    if (!Application.isPlaying || !Object.op_Equality((Object) this.sky, (Object) null) || !Object.op_Inequality((Object) this.skyPrefab, (Object) null))
      return;
    this.sky = ResourceUtility.Realizes((Object) this.skyPrefab, ((Component) this).transform, ((Component) this).gameObject.layer);
    this.sky.localPosition = new Vector3(0.0f, 0.0f, 50f);
    this.sky.localRotation = Quaternion.identity;
    this.sky.localScale = new Vector3(480f, 344f, 1f);
  }
}
