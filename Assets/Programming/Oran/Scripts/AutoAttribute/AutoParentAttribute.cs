/* Author: Oran Bar
 * Summary: This attribute automatically assigns a class variable to one of the gameobject's component. 
 * It basically acts as an automatic GetComponentInParent on Awake.
 * Using [AutoChildren(true)], the behaviour can be extendend to act like an AddOrGetComponent: The component will be added if it is not found, instead of an error being thrown.
 * 
 * usage example
 * 
 * public class Foo
 * {
 *		[AutoParent] public BoxCollier myBoxCollier;	//This assigns the variable to the BoxColider attached on your object
 *		
 *		//Methods...
 * }
 * 
 * Copyrights to Oran Bar™
 */

using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AutoParentAttribute : Attribute, IAutoAttribute
{
	private const string MonoBehaviourNameColor = "green";

	private bool logErrorIfMissing = true;

	private Component targetComponent;

	public AutoParentAttribute(bool autoAdd)
	{
		this.logErrorIfMissing = true;
	}

	public AutoParentAttribute(bool autoAdd = false, bool getMadIfMissing = true)
	{
		this.logErrorIfMissing = getMadIfMissing;
	}

	public void Execute(MonoBehaviour mb, Type componentType, Action<MonoBehaviour, object> SetVariableType)
	{
		GameObject go = mb.gameObject;

		if (componentType.IsArray)
		{
			MultipleComponentAssignment(mb, go, componentType, SetVariableType);
		}
		else
		{
			SetVariableType(mb, mb.GetComponentsInParent(componentType, true));
		}
	}

	private void MultipleComponentAssignment(MonoBehaviour mb, GameObject go, Type componentType, Action<MonoBehaviour, object> SetVariable)
	{
		Type listElementType = AutoUtils.GetElementType(componentType);

		MethodInfo method = typeof(GameObject).GetMethods()
			.First(m =>
			{
				bool result = true;
				result = result && m.Name == "GetComponentsInParent";
				result = result && m.IsGenericMethod;
				result = result && m.GetParameters().Length == 1;
				result = result && m.GetParameters()[0].ParameterType == typeof(bool);
				return result;
			});
		//we want to pass true as arg, to get from inactive objs too
		MethodInfo generic = method.MakeGenericMethod(listElementType);
		dynamic componentsToReference = generic.Invoke(go, new object[] { true });

		if (componentsToReference.Length == 0)
		{
			if (logErrorIfMissing)
			{
				Debug.LogError(
					string.Format("[Auto]: <color={3}><b>{1}</b></color> couldn't find any components <color=#cc3300><b>{0}</b></color> on <color=#e68a00>{2}.</color>",
						componentType.Name, mb.GetType().Name, go.name, MonoBehaviourNameColor)
					, go);
			}
			return;
		}

		if (componentType.IsArray)
		{
			SetVariable(mb, componentsToReference);
			//field.SetValue(mb, componentsToReference);
		}
		//else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
		//{
		//	List<Component> list = new List<Component>(componentsToReference);
		//	var argTypes = new[]
		//	{
		//		(IEnumerable<Component>) componentsToReference
		//	};

		//	var listType = typeof(List<>);
		//	var constructedListType = listType.MakeGenericType(componentType);

		//	var componentList = Activator.CreateInstance(constructedListType, argTypes);

		//	field.SetValue(mb, componentList);
		//}
	}














	public void Execute(MonoBehaviour mb, FieldInfo field)
	{
		GameObject go = mb.gameObject;

		Type componentType = field.FieldType;

		Component componentToReference = go.GetComponentInParent(componentType, true);
		if (componentToReference == null)
		{
			if (logErrorIfMissing)
			{
				Debug.LogError(
					string.Format("[Auto]: <color={3}><b>{1}</b></color> couldn't find <color=#cc3300><b>{0}</b></color> on <color=#e68a00>{2}</color>",
						componentType.Name, mb.GetType().Name, go.name, MonoBehaviourNameColor)
					, go);
			}
			return;
		}

		field.SetValue(mb, componentToReference);
	}

	public void Execute(MonoBehaviour mb, PropertyInfo prop)
	{
		GameObject go = mb.gameObject;

		Type componentType = prop.PropertyType;

		Component componentToReference = go.GetComponentInParent(componentType, true);
		if (componentToReference == null)
		{
			if (logErrorIfMissing)
			{
				Debug.LogError(string.Format("[Auto] Error: Script {1} couldn't AutoReference component {0} of GameObject {2}",
					componentType.Name, mb.GetType().Name, go.name), go);
				return;
			}
		}

		prop.SetValue(mb, componentToReference, null);
	}

}