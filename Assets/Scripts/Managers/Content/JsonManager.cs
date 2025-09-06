using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

public class JsonManager
{
    NoteJson noteJson;

    //filePath혹은 int와 같은 코드로 설정해도 됨
    //특정 filePath를 읽어서 각각의 노트 Json을 읽고 생성

    // 이 부분을 나중에 제거 필요 테스트용임
    // 사용할 json파일 이름을 넣으면 됨
    string path = "\\DAKI1_VIOLET_rough.json";

    //파일 경로 설정
    public void SetPath(string path)
    {
        this.path = path;
    }

    // filepath에 해당하는 경로의 json 파일을 찾음
    // 잘 굴러가는지 확인하는 방법은
    // C:\Users\사용자이름\AppData\LocalLow\DefaultCompany\Rhyffle 폴더에 해당 json 파일을 넣으면 됨

    // 예를 들어 chart.json 파일을 사용하고 싶다면
    // C:\Users\사용자이름\AppData\LocalLow\DefaultCompany\Rhyffle 폴더에 chart.json을 넣고
    // 위의 path 변수를 "\\chart.json"으로 변경하면 작동함
    public void LoadJson()
    {
        string loadPath = Application.persistentDataPath + path;
        string jsonString = File.ReadAllText(loadPath);
        noteJson = JsonConvert.DeserializeObject<NoteJson>(jsonString);
    }

    // 읽은 Json 정보를 전달하는 함수
    public NoteJson ReturnJson()
    {
        return noteJson;
    }

}
