// Decompiled with JetBrains decompiler
// Type: UIQuestLocationImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIQuestLocationImage : MonoBehaviour
{
  private UITexture uiTexture;
  private int id = -1;
  private Transform image;
  private IEnumerator coroutine;
  private System.Action onLoadStart;
  private System.Action onLoadComplete;

  public static void Set(
    UITexture ui_texture,
    int qli_id,
    System.Action on_load_start,
    System.Action on_load_complete)
  {
    UIQuestLocationImage questLocationImage = ((Component) ui_texture).GetComponent<UIQuestLocationImage>();
    if (Object.op_Equality((Object) questLocationImage, (Object) null))
      questLocationImage = ((Component) ui_texture).gameObject.AddComponent<UIQuestLocationImage>();
    questLocationImage.Load(ui_texture, qli_id, on_load_start, on_load_complete);
  }

  private void Load(
    UITexture ui_texture,
    int qli_id,
    System.Action on_load_start,
    System.Action on_load_complete)
  {
    this.uiTexture = ui_texture;
    this.id = qli_id;
    this.onLoadStart = on_load_start;
    this.onLoadComplete = on_load_complete;
    this.DeleteImage();
    this.StartCoroutine(this.coroutine = this.DoLoad());
  }

  private IEnumerator DoLoad()
  {
    if (this.onLoadStart != null)
      this.onLoadStart();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = loadingQueue.Load(RESOURCE_CATEGORY.QUEST_LOCATION_IMAGE, ResourceName.GetQuestLocationImage(this.id));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.image = ResourceUtility.Realizes(lo_image.loadedObject, MonoBehaviourSingleton<StageManager>.I._transform, 5);
    QuestLocationImage component1 = ((Component) this.image).GetComponent<QuestLocationImage>();
    if (!Object.op_Equality((Object) component1, (Object) null))
    {
      int width = this.uiTexture.width;
      int height = this.uiTexture.height;
      UIRenderTexture.ToRealSize(ref width, ref height);
      component1.Init(width, height);
      Camera component2 = ((Component) this.image).GetComponent<Camera>();
      if (!Object.op_Equality((Object) component2, (Object) null))
      {
        RenderTexture targetTexture = component2.targetTexture;
        if (!Object.op_Equality((Object) targetTexture, (Object) null))
        {
          this.uiTexture.mainTexture = (Texture) targetTexture;
          FloatInterpolator anim = new FloatInterpolator();
          anim.Set(0.25f, 0.0f, 1f, Curves.easeLinear, 0.0f, (AnimationCurve) null);
          anim.Play();
          while (anim.IsPlaying())
          {
            yield return (object) null;
            this.uiTexture.alpha = anim.Update();
          }
          if (this.onLoadComplete != null)
            this.onLoadComplete();
          this.coroutine = (IEnumerator) null;
        }
      }
    }
  }

  private void DeleteImage()
  {
    if (this.coroutine != null)
    {
      this.StopCoroutine(this.coroutine);
      this.coroutine = (IEnumerator) null;
    }
    if (Object.op_Inequality((Object) this.uiTexture, (Object) null))
    {
      this.uiTexture.alpha = 0.0f;
      this.uiTexture.mainTexture = (Texture) null;
    }
    if (!Object.op_Inequality((Object) this.image, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) this.image).gameObject);
    this.image = (Transform) null;
  }

  private void OnEnable()
  {
    if (this.coroutine != null || !Object.op_Equality((Object) this.image, (Object) null) || !Object.op_Inequality((Object) this.uiTexture, (Object) null) || this.id <= -1)
      return;
    this.StartCoroutine(this.coroutine = this.DoLoad());
  }

  private void OnDisable() => this.DeleteImage();
}
