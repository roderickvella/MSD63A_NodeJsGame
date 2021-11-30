using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class SignInScreen : MonoBehaviour
{   
    public void RegisterUser()
    {
        string username = GameObject.Find("CanvasSignIn/Background/InputUsername").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("CanvasSignIn/Background/InputPassword").GetComponent<TMP_InputField>().text;

        User_Model newUser = new User_Model() { password = password, username = username };

        RestClient.Post(GameManager.baseURI + "/api/auth/signup", newUser).Then(response =>
        {
            print(response.StatusCode.ToString());
            JObject jObject = JObject.Parse(response.Text);
            print(jObject.GetValue("message"));

        })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });

    }

    public void SignIn()
    {
        string username = GameObject.Find("CanvasSignIn/Background/InputUsername").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("CanvasSignIn/Background/InputPassword").GetComponent<TMP_InputField>().text;

        User_Model user = new User_Model() { password = password, username = username };
        RestClient.Post<User_Model>(GameManager.baseURI + "/api/auth/signin", user).Then(response =>
        {
            //save login data in PlayerPrefs
            PlayerPrefs.SetString("JWT", response.accessToken);
            PlayerPrefs.SetInt("ID", response.id);
            PlayerPrefs.SetString("Username", response.username);

            print("Logged in...jwt token saved in SharedPreferences");
        })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });
    }
}
