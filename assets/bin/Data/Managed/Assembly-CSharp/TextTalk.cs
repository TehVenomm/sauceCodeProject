using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TextTalk : MonoBehaviour
{
	private const float CONST_BREAK_TIME = 1f;

	private const float DEFAULT_NUM_PER_SEC = 30f;

	private bool isStart;

	private UILabel lbl;

	private List<string[]> textList;

	private string[] text;

	private float speed;

	private int page;

	private TagAnalyzer analyzer = new TagAnalyzer();

	private StringBuilder sb = new StringBuilder();

	private int textIndex;

	private int textCount;

	private float textAnimTime;

	private bool isComplete;

	private bool isPause;

	private float breakTime;

	private Action pageEndCallback;

	private Action<string, string> tagCallback;

	public void Initialize(Transform lbl_t, List<string[]> _text_list, Action page_end_call_back = null, Action<string, string> tag_call_back = null, int num_per_sec = 0)
	{
		if (!((UnityEngine.Object)lbl_t == (UnityEngine.Object)null) && _text_list != null && _text_list.Count != 0)
		{
			lbl = lbl_t.GetComponent<UILabel>();
			if (!((UnityEngine.Object)lbl == (UnityEngine.Object)null))
			{
				isStart = false;
				lbl.text = string.Empty;
				page = 0;
				textList = _text_list;
				text = textList[page++];
				speed = Mathf.Max(1f, (num_per_sec != 0) ? ((float)num_per_sec) : 30f);
				sb.Remove(0, sb.ToString().Length);
				textIndex = 0;
				textCount = 0;
				textAnimTime = 0f;
				breakTime = 0f;
				isComplete = false;
				pageEndCallback = page_end_call_back;
				tagCallback = tag_call_back;
			}
		}
	}

	public void StartTalk()
	{
		isStart = true;
	}

	public bool IsComplete()
	{
		return isStart && isComplete;
	}

	public bool IsTalking()
	{
		return isStart && !isComplete;
	}

	public void SkipOneStep()
	{
		if (isStart)
		{
			if (isPause)
			{
				NextPage();
			}
			else if (breakTime > 0f)
			{
				breakTime = 0f;
			}
			else
			{
				textAnimTime = (float)text[textIndex].Length;
			}
		}
	}

	public void SkipAll()
	{
		int index = textList.Count - 1;
		string[] array = textList[index];
		sb.Remove(0, sb.ToString().Length);
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			sb.Append(array[i]);
		}
		lbl.text = sb.ToString();
		isComplete = true;
	}

	private void NextPage()
	{
		if (isStart)
		{
			text = textList[page++];
			sb.Remove(0, sb.ToString().Length);
			lbl.text = sb.ToString();
			textIndex = 0;
			textCount = 0;
			textAnimTime = 0f;
			breakTime = 0f;
			isPause = false;
		}
	}

	private void Update()
	{
		if (isStart && !((UnityEngine.Object)lbl == (UnityEngine.Object)null) && text != null && text.Length != 0 && !isComplete && !isPause)
		{
			float num = Time.deltaTime;
			if (breakTime > 0f)
			{
				breakTime -= num;
				if (breakTime > 0f)
				{
					return;
				}
				num = 0f - breakTime;
			}
			textAnimTime += num;
			int num2 = (int)(textAnimTime * speed);
			if (num2 > textCount)
			{
				while (num2 > textCount)
				{
					if (textCount >= text[textIndex].Length)
					{
						if (textIndex == text.Length - 1)
						{
							textAnimTime = 0f;
							if (page < textList.Count)
							{
								isPause = true;
							}
							else
							{
								isComplete = true;
							}
							if (pageEndCallback != null)
							{
								pageEndCallback();
							}
						}
						else
						{
							sb.AppendLine();
							textCount = 0;
							textAnimTime = 0f;
							textIndex++;
							breakTime = 1f;
						}
						break;
					}
					int num3 = textCount;
					textCount = analyzer.Analyze(text[textIndex], textIndex, textCount);
					if (!analyzer.IsFindTag())
					{
						sb.Append(text[textIndex][textCount++]);
					}
					else if (tagCallback != null)
					{
						tagCallback(analyzer.findTag, analyzer.findTagText);
					}
				}
				lbl.text = sb.ToString();
			}
		}
	}
}
