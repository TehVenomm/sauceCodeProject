using UnityEngine;

public class CustomArrayAttribute : PropertyAttribute
{
	public string propertyPath;

	public CustomArrayAttribute(string displayRelativePropertyPath)
	{
		propertyPath = displayRelativePropertyPath;
	}
}
