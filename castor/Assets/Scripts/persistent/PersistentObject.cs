﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
	void Start ()
	{
		DontDestroyOnLoad (gameObject);

		new PlayerPrefsUpdater ().update();

		SceneManager.LoadScene ("mainMenu");
	}
		
}