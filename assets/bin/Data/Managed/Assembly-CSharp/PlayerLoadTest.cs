// Decompiled with JetBrains decompiler
// Type: PlayerLoadTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class PlayerLoadTest : MonoBehaviour
{
  public string body;
  public string face;
  public string head;
  private Object loadObject;
  private bool loadError;

  private IEnumerator Start()
  {
    yield return (object) 0;
    GameObject body = (GameObject) null;
    GameObject head = (GameObject) null;
    GameObject face = (GameObject) null;
    GameObject weapon = (GameObject) null;
    yield return (object) this.StartCoroutine(this.LoadObject(RESOURCE_CATEGORY.PLAYER_BDY, "BDY00_000"));
    body = (GameObject) Object.Instantiate(this.loadObject);
    yield return (object) this.StartCoroutine(this.LoadObject(RESOURCE_CATEGORY.PLAYER_HEAD, "HED00_000"));
    head = (GameObject) Object.Instantiate(this.loadObject);
    yield return (object) this.StartCoroutine(this.LoadObject(RESOURCE_CATEGORY.PLAYER_FACE, "PLF00_000"));
    face = (GameObject) Object.Instantiate(this.loadObject);
    yield return (object) this.StartCoroutine(this.LoadObject(RESOURCE_CATEGORY.PLAYER_WEAPON, "WEP00_001"));
    weapon = (GameObject) Object.Instantiate(this.loadObject);
    yield return (object) this.StartCoroutine(this.LoadObject(RESOURCE_CATEGORY.PLAYER_ANIM, "PLC00_AnimCtrl"));
    RuntimeAnimatorController loadObject = this.loadObject as RuntimeAnimatorController;
    Transform parent1 = Utility.Find(body.transform, "Head");
    Transform parent2 = Utility.Find(body.transform, "R_Wep");
    Transform parent3 = Utility.Find(body.transform, "L_Wep");
    Utility.Attach(parent1, head.transform);
    Utility.Attach(parent1, face.transform);
    foreach (MeshRenderer componentsInChild in weapon.GetComponentsInChildren<Renderer>())
    {
      if (((Object) componentsInChild).name.EndsWith("_L"))
        Utility.Attach(parent3, ((Component) componentsInChild).transform.parent);
      else
        Utility.Attach(parent2, ((Component) componentsInChild).transform.parent);
    }
    Object.DestroyImmediate((Object) weapon);
    body.GetComponentInChildren<Animator>().runtimeAnimatorController = loadObject;
    Debug.Log((object) ("End:" + (object) loadObject));
  }

  private IEnumerator LoadObject(RESOURCE_CATEGORY category, string resource_name)
  {
    this.loadObject = (Object) null;
    this.loadError = false;
    MonoBehaviourSingleton<ResourceManager>.I.Load((object) this, category, resource_name, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError));
    while (Object.op_Equality(this.loadObject, (Object) null) && !this.loadError)
      yield return (object) 0;
  }

  private void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
  {
    Debug.Log((object) nameof (OnLoadComplate));
    this.loadObject = objs[0].obj;
  }

  private void OnLoadError(ResourceManager.LoadRequest request, ResourceManager.ERROR_CODE code)
  {
    Debug.Log((object) nameof (OnLoadError));
    this.loadError = true;
  }

  private void Update()
  {
  }
}
