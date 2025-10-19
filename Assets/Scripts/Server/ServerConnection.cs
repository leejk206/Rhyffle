using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[System.Serializable]
public class UserInfo
{
    public string userid;
    public string timezone;
    public string country;
    public string nickname;
    public string email;
    public string bDay;
    public string regDay;
}

[System.Serializable]
public class CardInfo
{
    public string userid;
    public string card_baseid;
    public string pack_id;
    public string card_particle_id;
    public string is_double_sided;
    public string double_side_activated;
    public string ability_id;
    public string ability_level;
    public string unique_ability_level;
    public string ability_id_back;
    public string ability_level_back;
    public string unique_ability_level_back;
    public string bookmarked;
}


public class ServerConnection : MonoBehaviour
{
    //테스트용 카드 입력칸
    public CardInfo cardInfoTest;

    string id ="";

    public void RegisterTest()
    {
        // 임시로 만든 countryTimeZone이다 이를 추후 수정해야함!
        UserInfo current = new UserInfo();
        current.country = "korea";
        current.timezone = "Asia1";
        current.regDay = DateTime.Now.ToString("yyyy-MM-dd");
        StartCoroutine(Register(current));
    }

    public void GetInfoTest()
    {
        UserInfo current = new UserInfo();
        current.userid = id;
        StartCoroutine(GetInfo(current));
    }

    public void SetNicknameTest()
    {
        UserInfo current = new UserInfo();
        current.nickname = "Somba";
        current.userid = id;
        StartCoroutine (SetNickname(current));
    }

    public void SetEmailTest()
    {
        UserInfo current = new UserInfo();
        current.email = "Somba@gmail.com";
        current.userid = id;
        StartCoroutine(SetEmail(current));
    }

    public void SetBirthDateTest()
    {
        UserInfo current = new UserInfo();
        current.bDay = "2020-08-17";
        current.userid = id;
        StartCoroutine(SetBirthDate(current));
    }

    public void AddCardTest()
    {
        StartCoroutine(AddCard(cardInfoTest));
    }

    IEnumerator Register(UserInfo countT)
    {
        string UserInfoJson = JsonConvert.SerializeObject(countT);
        Debug.Log(UserInfoJson);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(UserInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/userinfo/register", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string str = request.downloadHandler.text;
            //이 부분은 추후 삭제 필요!!!
            JObject jobj = JObject.Parse(str);

            id = (string)jobj["userid"];
            cardInfoTest.userid = id;
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    // 이 아래의 Set함수들은 추후 하나의 함수로 종합할 예정
    // 우선 부분별로 나눠놓았음

    IEnumerator SetNickname(UserInfo userinfo)
    {
        string userInfoJson = JsonConvert.SerializeObject(userinfo);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(userInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/userinfo/setnickname", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    IEnumerator SetEmail(UserInfo userinfo)
    {
        string userInfoJson = JsonConvert.SerializeObject(userinfo);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(userInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/userinfo/setemail", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    IEnumerator SetBirthDate(UserInfo userinfo)
    {
        string userInfoJson = JsonConvert.SerializeObject(userinfo);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(userInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/userinfo/register", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    IEnumerator GetInfo(UserInfo userinfo)
    {
        string UserInfoJson = JsonConvert.SerializeObject(userinfo);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(UserInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/userinfo/getinfo", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 이곳에서 받는 str는 json의 형태여서 json으로 바로 바꿔주면 된다
            string str = request.downloadHandler.text;
            

            Debug.Log(str);
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    IEnumerator AddCard(CardInfo cardInfo)
    {
        string cardInfoJson = JsonConvert.SerializeObject(cardInfo);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(cardInfoJson);

        UnityWebRequest request = new UnityWebRequest("http://localhost:8080/cards/create", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 이곳에서 받는 str는 json의 형태여서 json으로 바로 바꿔주면 된다
            string str = request.downloadHandler.text;


            Debug.Log(str);
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    IEnumerator GetCard()
    {
        yield return null;
    }
}
