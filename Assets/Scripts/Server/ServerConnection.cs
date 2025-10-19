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
    //�׽�Ʈ�� ī�� �Է�ĭ
    public CardInfo cardInfoTest;

    string id ="";

    public void RegisterTest()
    {
        // �ӽ÷� ���� countryTimeZone�̴� �̸� ���� �����ؾ���!
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
            //�� �κ��� ���� ���� �ʿ�!!!
            JObject jobj = JObject.Parse(str);

            id = (string)jobj["userid"];
            cardInfoTest.userid = id;
        }
        else
        {
            Debug.LogError("Fail");
        }
    }

    // �� �Ʒ��� Set�Լ����� ���� �ϳ��� �Լ��� ������ ����
    // �켱 �κк��� ����������

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
            // �̰����� �޴� str�� json�� ���¿��� json���� �ٷ� �ٲ��ָ� �ȴ�
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
            // �̰����� �޴� str�� json�� ���¿��� json���� �ٷ� �ٲ��ָ� �ȴ�
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
