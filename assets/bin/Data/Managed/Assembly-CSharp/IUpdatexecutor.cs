using System;
using System.Collections;

public interface IUpdatexecutor
{
	void UpdateAllUI(Action _action);

	void InvokeCoroutine(IEnumerator _enumerator);
}
