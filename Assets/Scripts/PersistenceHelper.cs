using UnityEngine;
using System.Collections;
using System;

public class PersistenceHelper
{
	public const string HIGHSCORE_KEY = "HighScore";

	public static void PersistInteger (string key, int value)
	{
		byte[] data = BitConverter.GetBytes(value);
		string base64Data = Convert.ToBase64String(data);

		PlayerPrefs.SetString(key, base64Data);
	}

	public static int ReadInteger (string key)
	{
		int value = 0;
		if (PlayerPrefs.HasKey(key))
		{
			string base64Data = PlayerPrefs.GetString(key);
			byte[] data = Convert.FromBase64String(base64Data);
			value = BitConverter.ToInt32(data, 0);
		}

		return value;
	}
}
