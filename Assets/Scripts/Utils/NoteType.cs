using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region BasicNote
[System.Serializable]
public class BasicNoteJson
{
    public BasicNoteLane[] lanes;
}

[System.Serializable]
public class BasicNoteLane
{
    public int[] time;
    public int[] length;
}
#endregion

public class NoteType {
    
}
