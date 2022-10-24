﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ToolRandom {

	static ulong next = 1;
	/* rand:  return pseudo-random integer on 0..32767 */
//	public static int rand()
//	{
//		next = next * 1103515245 + 12345;
//		return (int)((next / 65536) % 32768);
//	}
	/* srand:  set seed for rand() */
	public static void srand(ulong seed)
	{
		UnityEngine.Random.InitState((int)seed)   ;
		next = seed;
	}

	public static int rand_100()
	{
		next = next * 1103515245 + 12345;
		return (int)((next / 65536) % 100);
	}

	//	public static int rand_1000()
	//	{
	//		next = next * 1103515245 + 12345;
	//		return (int)((next / 65536) % 1000);
	//	}

	public static int rand_10000()
	{
		next = next * 1103515245 + 12345;
		return (int)((next / 65536) % 1000);
	}


	public static   List<T> RandomListSort<T>(List<T> list)
	{
		var random = new System.Random();
		var newList = new List<T>();
		foreach (var item in list)
		{
			newList.Insert(random.Next(newList.Count), item);
		}
		return newList;
	}
}
