///-----------------------------------------------------------------
///   Class:          PlayerControl
///   Description:    This script is used for creating, loading and saving the data file that holds the high scores for the game
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
#endregion

public class PlayerControl : MonoBehaviour
{

	public static PlayerControl control;
	public List<int> highScores;
	public int score;

	[Serializable]
	class PlayerData
	{
		public int score;
		public List<int> highScores;
	}

	void Awake()
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}
	}

	public void CreateDataFile()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/userInfo.dat");
		PlayerData data = new PlayerData();
		highScores.Add(3);
		data.score = 1;
		data.highScores = highScores;

		bf.Serialize(file, data);
		file.Close();
	}

	public void SaveAll()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/userInfo.dat");
		PlayerData data = new PlayerData();

		data.score = score;

		//Only save score to highscores if it is a top 10 score
		if (highScores.Count < 10)
		{
			highScores.Add(score);
		}
		else
		{
			foreach(int i in highScores)
            {
				if (score > i)
				{
					highScores.Remove(i);
					highScores.Add(score);
				}
			}
		}
		data.highScores = highScores;
		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadAll()
	{
		if (File.Exists(Application.persistentDataPath + "/userInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/userInfo.dat", FileMode.Open);
			if (file.Length == 0)
			{
				file.Close();
				CreateDataFile();
			}
			else
			{
				PlayerData data = (PlayerData)bf.Deserialize(file);
				file.Close();

				highScores = data.highScores;
			}
		}
	}
}


