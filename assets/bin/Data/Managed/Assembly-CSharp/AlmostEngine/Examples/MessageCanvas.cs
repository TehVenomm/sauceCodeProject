// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.MessageCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Examples;

public class MessageCanvas : MonoBehaviour
{
  public RectTransform m_MessagePanel;
  public GameObject m_MessagePrefab;
  public float m_DisplayTime = 5f;
  private List<GameObject> m_Messages = new List<GameObject>();

  private void OnEnable()
  {
    this.Clear();
    ScreenshotManager.onResolutionExportSuccessDelegate -= new ScreenshotManager.ExportDelegate(this.ExportSuccessCallback);
    ScreenshotManager.onResolutionExportSuccessDelegate += new ScreenshotManager.ExportDelegate(this.ExportSuccessCallback);
    ScreenshotManager.onResolutionExportFailureDelegate -= new ScreenshotManager.ExportDelegate(this.ExportFailureCallback);
    ScreenshotManager.onResolutionExportFailureDelegate += new ScreenshotManager.ExportDelegate(this.ExportFailureCallback);
  }

  private void OnDisable()
  {
    ScreenshotManager.onResolutionExportSuccessDelegate -= new ScreenshotManager.ExportDelegate(this.ExportSuccessCallback);
    ScreenshotManager.onResolutionExportFailureDelegate -= new ScreenshotManager.ExportDelegate(this.ExportFailureCallback);
  }

  public void ExportSuccessCallback(ScreenshotResolution resolution)
  {
    this.DisplayMessage("Screenshot created : " + resolution.m_FileName);
  }

  public void ExportFailureCallback(ScreenshotResolution resolution)
  {
    this.DisplayMessage("FAILED to create : " + resolution.m_FileName);
  }

  private void Clear()
  {
    this.StopAllCoroutines();
    foreach (GameObject message in this.m_Messages)
      this.StartCoroutine(this.DestroyMessageCoroutine(message));
  }

  public void DisplayMessage(string text)
  {
    GameObject message = Object.Instantiate<GameObject>(this.m_MessagePrefab);
    message.transform.SetParent(((Component) this.m_MessagePanel).transform);
    message.transform.localScale = Vector3.one;
    message.GetComponent<Text>().text = text;
    this.m_Messages.Add(message);
    this.StartCoroutine(this.DestroyMessageCoroutine(message));
  }

  private IEnumerator DestroyMessageCoroutine(GameObject message)
  {
    yield return (object) new WaitForSeconds(this.m_DisplayTime);
    this.m_Messages.Remove(message);
    Object.DestroyImmediate((Object) message);
  }
}
