using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.IO;
using System.Net;
using Newtonsoft.Json;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	//ScoreManager scoreManager;

	//int lastChangeCounter;

	// Use this for initialization
	void Start () {
		//scoreManager = GameObject.FindObjectOfType<ScoreManager>();

		//lastChangeCounter = scoreManager.GetChangeCounter();
		Top10Users();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(scoreManager == null) {
			Debug.LogError("You forgot to add the score manager component to a game object!");
			return;
		}

		if(scoreManager.GetChangeCounter() == lastChangeCounter) {
			// No change since last update!
			return;
		}

		lastChangeCounter = scoreManager.GetChangeCounter();
		*/
		

		//string[] names = scoreManager.GetPlayerNames("kills");
		
		
	}

	public void Top10Users(){
		while(this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}
		string URL = "http://ec2-18-218-147-158.us-east-2.compute.amazonaws.com.com:8081/top10users/";
		string[] username = new string[10];
		int[] score = new int[10];
		try {
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create (URL);
			HttpWebResponse response = (HttpWebResponse) request.GetResponse ();
			Stream stream = response.GetResponseStream ();
			string responseBody = new StreamReader (stream).ReadToEnd ();

			Debug.Log (responseBody);

			UserData[] players = JsonConvert.DeserializeObject<UserData[]> (responseBody);
			if (players.Length == 0) {
				Debug.LogError ("Error in query");
			} else {
				username = new string[players.Length];
				score = new int[players.Length];
				for (int i = 0; i < players.Length; i++) {
					username[i] = players[i].username;
					score[i] = players[i].score;
				}
			}
		} catch (WebException ex) {
			Debug.LogError (ex);
		}
		for(int i=0; i<username.Length; i++){
			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform);
			go.transform.Find ("Username").GetComponent<Text>().text = username[i];
			go.transform.Find ("Kills").GetComponent<Text>().text = (score[i]/10).ToString() + " m";
		}
	}
}
