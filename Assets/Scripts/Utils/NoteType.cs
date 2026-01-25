using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Note 스크립트와의 충돌 회피를 위해 이렇게 info, json을 붙여서 작명함
//info는 각각의 노트에 대한 정보
//json은 json 파일에서 가져온 모든 노트에 대한 정보
#region BasicNote
[System.Serializable]
public class BasicNoteInfo
{
    public int position;
    public int line;
    public int length;

    public BasicNoteInfo(int pos, int line, int length)
    {
        position = pos;
        this.line = line;
        this.length = length;
    }

}


[System.Serializable]
public class BasicNoteJson
{
    public BasicNoteInfo[] notes;
}
#endregion

//이 부분에 대해서 추후 검토 예정, SlideNote의 경우에는 일반적인 BasicNote의 class를 그대로 가져다써도 문제가 발생하지 않기 때문
#region SlideNote
[System.Serializable]
public class SlideNoteInfo
{
    public int position;
    public int line;
    public int length;

    public SlideNoteInfo(int position, int line, int length)
    {
        this.position = position;
        this.line = line;
        this.length=length;
    }
}


[System.Serializable]
public class SlideNoteJson
{
    public SlideNoteInfo[] notes;
}
#endregion

//우선은 이렇게 구현하고 필요하다면 추후 논의를 통하여 위 아래 플릭으로 나누어 구현
#region FlickNote
[System.Serializable]
public class FlickNoteInfo
{
    public int position;
    public int line;
    public int length;
    public int dir;
    public FlickNoteInfo(int position, int line, int length, int dir)
    {
        this.position=position;
        this.line=line;
        this.length=length;
        this.dir=dir;
    }
}


[System.Serializable]
public class FlickNoteJson
{
    public FlickNoteInfo[] notes;
}
#endregion

#region HoldNote
[System.Serializable]
public class HoldNoteInfo
{
    public int position;
    public int line;
    public int noteType;
    public int count;
    public int length;

    public HoldNoteInfo(int position, int line, int noteType, int count, int length)
    {
        this.position = position;
        this.line = line;
        this.noteType=noteType;
        this.count=count;
        this.length = length;
    }
}


[System.Serializable]
public class HoldNoteJson
{
    public HoldNoteInfo[] notes;
}
#endregion


public class NoteType {
    
}
