using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteCreator
{
    public GameObject basicNote;
    public GameObject slideNote;
    public GameObject flickNote;
    public GameObject holdNoteBody;
    public GameObject holdNote;

    //�̰��� Poolable �����Ű�� �� ��

    //lane�� length ���̸�ŭ judge ��ġ�� ����
    public GameObject CreateBasic(int lane, int length, int judge)
    {
        GameObject basic = GameObject.Instantiate(basicNote);
        basic.GetComponent<BasicNote>().judge = judge;
        basic.GetComponent<BasicNote>().Set(lane, length);

        return basic;
    }

}
