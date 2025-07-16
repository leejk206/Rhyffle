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

    //이곳에 Poolable 적용시키면 될 듯

    //lane에 length 길이만큼 judge 위치에 생성
    public GameObject CreateBasic(int lane, int length, int judge)
    {
        GameObject basic = GameObject.Instantiate(basicNote);
        basic.GetComponent<BasicNote>().judge = judge;
        basic.GetComponent<BasicNote>().Set(lane, length);

        return basic;
    }

}
