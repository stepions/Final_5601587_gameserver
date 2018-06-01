using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {

	string URL;
	public GameObject loginPanel;
	public InputField inputUsername, inputPassword, inputEmail, inputConfirmPassword;

	public void LoginButton () {
		loginPanel.SetActive (true);
		gameObject.SetActive (false);
	}

	public void RegisterButtton () {
		if(inputPassword.text.Trim()!=inputConfirmPassword.text.Trim()){
			Debug.LogError ("Password doen't match.");
			return;
		}
		URL = "http://ec2-18-218-147-158.us-east-2.compute.amazonaws.com:8081/register?name=" + inputUsername.text.Trim () + "&pass=" + inputPassword.text.Trim () + "&email=" + inputEmail.text.Trim ();
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