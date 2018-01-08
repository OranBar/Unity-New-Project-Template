/*======= Copyright (c) Immerxive Srl, All rights reserved. ===================

Author: Oran Bar

Purpose: To help with Linq operations. I have enough of writing .ToList().ForEach(), because you can't foreach on a IEnumerable, only on a List. This ends NOW.

Notes:

=============================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using OranUnityUtils;


public static class IEnumerableEx {

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (var item in source)
			action(item);
	}
}
