using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Note ��ũ��Ʈ���� �浹 ȸ�Ǹ� ���� �̷��� info, json�� �ٿ��� �۸���
//info�� ������ ��Ʈ�� ���� ����
//json�� json ���Ͽ��� ������ ��� ��Ʈ�� ���� ����

[System.Serializable]
public class NoteJson
{
    public BasicNoteInfo[] NormalNotes;
    public HoldNoteInfo[] HoldNotes;
    public SlideNoteInfo[] SlideNotes;
    public FlickNoteInfo[] FlickNotes;
}


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
#endregion

//�� �κп� ���ؼ� ���� ���� ����, SlideNote�� ��쿡�� �Ϲ����� BasicNote�� class�� �״�� �����ٽᵵ ������ �߻����� �ʱ� ����
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

#endregion

//�켱�� �̷��� �����ϰ� �ʿ��ϴٸ� ���� ���Ǹ� ���Ͽ� �� �Ʒ� �ø����� ������ ����
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

#endregion

#region HoldNote
[System.Serializable]
public class HoldNoteInfo
{
    public int position;
    public int line;
    public int noteType; //0: ����, 1: �߰�, 2: ��
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


#endregion


public class NoteType {
    
}
