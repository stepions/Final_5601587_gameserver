using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Net;

public class GlobalData {

	public static string username;
	public static int score;

	public static void UpdateScore(){
		string URL = "http://ec2-18-218-147-158.us-east-2.compute.amazonaws.com:8081/user/update?name=" + username + "&score=" + score;
		try {
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create (URL);
			HttpWebResponse response = (HttpWebResponse) request.GetResponse ();
			Stream stream = response.GetResponseStream ();
			string responseBody = new StreamReader (stream).ReadToEnd ();
			Debug.Log (responseBody);
		} catch (WebException ex) {
			Debug.LogError (ex);
		}
	}

}
