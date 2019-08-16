using System.Collections.Generic;

public class ManualCoroutineList
{
	public List<ManualCoroutine> list = new List<ManualCoroutine>();

	public List<int> stack = new List<int>();

	public void Add(ManualCoroutine mc)
	{
		list.Add(mc);
	}

	public void Remove(int id)
	{
		while (true)
		{
			int num = list.FindIndex((ManualCoroutine o) => o.id == id);
			if (num == -1)
			{
				break;
			}
			list[num].Clear();
			list.RemoveAt(num);
		}
	}

	public void SetActive(int id, bool is_active = true)
	{
		list.ForEach(delegate(ManualCoroutine o)
		{
			if (o.id == id)
			{
				o.active = is_active;
			}
		});
	}

	public void SetActiveToggle(int id, bool is_active = true)
	{
		bool inv_is_active = !is_active;
		list.ForEach(delegate(ManualCoroutine o)
		{
			if (o.id == id)
			{
				o.active = is_active;
			}
			else
			{
				o.active = inv_is_active;
			}
		});
	}

	public void Push(int id)
	{
		stack.Add(id);
		SetActiveToggle(id);
	}

	public void Pop()
	{
		if (stack.Count > 0)
		{
			stack.RemoveAt(stack.Count - 1);
		}
		if (stack.Count > 0)
		{
			SetActiveToggle(stack[stack.Count - 1]);
		}
	}

	public int Peek()
	{
		if (stack.Count > 0)
		{
			return stack[stack.Count - 1];
		}
		return -1;
	}

	public int GetActiveCount(int id)
	{
		int count = 0;
		list.ForEach(delegate(ManualCoroutine o)
		{
			if (o.id == id && o.active && o.isEnabled)
			{
				count++;
			}
		});
		return count;
	}
}
