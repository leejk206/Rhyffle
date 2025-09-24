using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

public class JsonManager
{
    NoteJson noteJson;

    //filePathȤ�� int�� ���� �ڵ�� �����ص� ��
    //Ư�� filePath�� �о ������ ��Ʈ Json�� �а� ����

    // �� �κ��� ���߿� ���� �ʿ� �׽�Ʈ����
    // ����� json���� �̸��� ������ ��
    string path = "/DAKI1_VIOLET_rough.json";

    //���� ��� ����
    public void SetPath(string path)
    {
        this.path = path;
    }

    // filepath�� �ش��ϴ� ����� json ������ ã��
    // �� ���������� Ȯ���ϴ� �����
    // C:\Users\������̸�\AppData\LocalLow\DefaultCompany\Rhyffle ������ �ش� json ������ ������ ��

    // ���� ��� chart.json ������ ����ϰ� �ʹٸ�
    // C:\Users\������̸�\AppData\LocalLow\DefaultCompany\Rhyffle ������ chart.json�� �ְ�
    // ���� path ������ "\\chart.json"���� �����ϸ� �۵���
    public void LoadJson()
    {
        string loadPath = Application.persistentDataPath + path;
        string jsonString = File.ReadAllText(loadPath);
        noteJson = JsonConvert.DeserializeObject<NoteJson>(jsonString);
    }

    // ���� Json ������ �����ϴ� �Լ�
    public NoteJson ReturnJson()
    {
        return noteJson;
    }

}
