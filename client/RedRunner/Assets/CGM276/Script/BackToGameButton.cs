using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

public class BackToGameButton : MonoBehaviour {

	public void BackToGameButtonClick(){
		SceneManager.LoadScene("Login");
	}
	
}
