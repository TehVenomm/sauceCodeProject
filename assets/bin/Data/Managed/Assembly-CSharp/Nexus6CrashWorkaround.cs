// Decompiled with JetBrains decompiler
// Type: Nexus6CrashWorkaround
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class Nexus6CrashWorkaround
{
  private static readonly string temporaryObjectName = "n6cw";
  private static Mesh mesh;
  private static Material material;

  public static bool disable { get; set; }

  public static void Apply(Camera camera)
  {
    if (Nexus6CrashWorkaround.disable)
      return;
    Nexus6CrashWorkaround.Remove(camera);
    if (!Object.op_Implicit((Object) Nexus6CrashWorkaround.mesh))
      Nexus6CrashWorkaround.mesh = Nexus6CrashWorkaround.CreateMesh();
    if (!Object.op_Implicit((Object) Nexus6CrashWorkaround.material))
      Nexus6CrashWorkaround.material = Nexus6CrashWorkaround.CreateMaterial();
    Transform transform = Nexus6CrashWorkaround.CreateObject(Nexus6CrashWorkaround.mesh, Nexus6CrashWorkaround.material);
    transform.parent = ((Component) camera).transform;
    transform.localPosition = Vector3.op_Multiply(Vector3.op_Multiply(Vector3.forward, camera.nearClipPlane + camera.farClipPlane), 0.5f);
    transform.localScale = Vector3.one;
    ((Component) transform).gameObject.layer = Nexus6CrashWorkaround.GetEnabledLayer(camera);
  }

  public static void Remove(Camera camera)
  {
    Transform transform = ((Component) camera).transform.Find(Nexus6CrashWorkaround.temporaryObjectName);
    if (!Object.op_Implicit((Object) transform))
      return;
    Object.Destroy((Object) ((Component) transform).gameObject);
  }

  private static int GetEnabledLayer(Camera camera)
  {
    int cullingMask = camera.cullingMask;
    int enabledLayer = 0;
    for (int index = 0; index < 32 /*0x20*/; ++index)
    {
      if ((cullingMask & 1) != 0)
        return enabledLayer;
      cullingMask >>= 1;
      ++enabledLayer;
    }
    return 0;
  }

  private static Transform CreateObject(Mesh mesh, Material material)
  {
    GameObject gameObject = new GameObject(Nexus6CrashWorkaround.temporaryObjectName);
    gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
    ((Renderer) gameObject.AddComponent<MeshRenderer>()).sharedMaterial = material;
    return gameObject.transform;
  }

  private static Mesh CreateMesh()
  {
    Mesh mesh = new Mesh();
    ((Object) mesh).name = "n6cw_mesh";
    mesh.vertices = new Vector3[3]
    {
      new Vector3(0.0f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, 1f / 1000f),
      new Vector3(0.0f, 1f / 1000f, 0.0f)
    };
    mesh.triangles = new int[3]{ 0, 1, 2 };
    return mesh;
  }

  private static Material CreateMaterial()
  {
    Material material = new Material(ResourceUtility.FindShader("mobile/Custom/tex_color"));
    ((Object) material).name = "n6cw_mat";
    material.mainTexture = Resources.Load("Texture/White") as Texture;
    material.color = Color.clear;
    return material;
  }
}
