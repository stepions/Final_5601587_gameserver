using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Net;
using Newtonsoft.Json;

using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

public class Login : MonoBehaviour {

	string URL;
	public GameObject registerPanel;
	public InputField inputUsername, inputPassword;

	public void LoginButton(){
		URL = "http://ec2-18-218-147-158.us-east-2.compute.amazonaws.com:8081/login/" + inputUsername.text.Trim () + "/" + inputPassword.text.Trim ();
		try {
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create (URL);
			HttpWebResponse response = (HttpWebResponse) request.GetResponse ();
			Stream stream = response.GetResponseStream ();
			string responseBody = new StreamReader (stream).ReadToEnd ();

			Debug.Log (responseBody);

			UserData[] players = JsonConvert.DeserializeObject<UserData[]> (responseBody);
			if (players.Length == 0) {
				Debug.LogError ("Wrong Username or Password.");
				inputUsername.text = "";
				inputPassword.text = "";
			} else {
				GlobalData.username = players[0].username;
				GlobalData.score = players[0].score;
				if(SaveGame.Serializer==null){
					SaveGame.Serializer = new SaveGameBinarySerializer ();
				}
				SaveGame.Save<float> ( "highScore", Mathf.Floor(GlobalData.score) );
				SceneManager.LoadScene("Play");
			}
		} catch (WebException ex) {
			Debug.LogError (ex);
		}
	}

	public void RegisterButton(){
		registerPanel.SetActive(true);
		gameObject.SetActive(false);
	}

}
