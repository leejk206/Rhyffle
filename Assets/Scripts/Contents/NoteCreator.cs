using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteCreator : MonoBehaviour 
{
    public GameObject basicNote;
    public GameObject slideNote;
    public GameObject flickNote;
    public GameObject holdNoteBody;
    public GameObject holdNote;

    //이곳에 Poolable 적용시키면 될 듯

    //lane에 length 길이만큼 judge 위치에 생성
    public GameObject CreateBasic(BasicNoteInfo basicNoteInfo)
    {
        GameObject basic = GameObject.Instantiate(basicNote);
        BasicNote basicComp = basic.GetComponent<BasicNote>();
        basicComp.SetJudge(basicNoteInfo.position);
        basicComp.Set(basicNoteInfo.line, basicNoteInfo.length);

        return basic;
    }
    public GameObject CreateSlide(SlideNoteInfo slideNoteInfo)
    { 
        GameObject slide = GameObject.Instantiate(slideNote);
        SlideNote slideComp = slide.GetComponent<SlideNote>();
        slideComp.SetJudge(slideNoteInfo.position);
        slideComp.Set(slideNoteInfo.line, slideNoteInfo.length);

        return slide;
    }

    public GameObject CreateFlick(FlickNoteInfo flickNoteInfo)
    {
        GameObject flick = GameObject.Instantiate(flickNote);
        FlickNote flickComp = flick.GetComponent<FlickNote>();
        flickComp.SetJudge(flickNoteInfo.position);
        flickComp.Set(flickNoteInfo.line,flickNoteInfo.length);
        flickComp.SetDir(flickNoteInfo.dir);

        return flick;
    }

    public GameObject CreateHoldBody(List<HoldNoteInfo> holdBodyInfo, float playerSpeed)
    {
        GameObject holdBody = GameObject.Instantiate(holdNoteBody);
        HoldNoteBody holdBodyComp = holdBody.GetComponent<HoldNoteBody>();
        int holdStart = holdBodyInfo[0].position;
        int holddiff = 0;
        for(int i=0; i < holdBodyInfo.Count; i++)
        {
            holddiff = holdBodyInfo[i].position - holdStart;
            holdBodyComp.AddNotes(CreateHold(holdBodyInfo[i], 10 + holddiff / 16 * 2.5f * playerSpeed / 4).GetComponent<HoldNote>());
        }

        return holdBody;
    }

    public GameObject CreateHold(HoldNoteInfo holdNoteInfo, float height)
    {
        GameObject hold = GameObject.Instantiate(holdNote);
        HoldNote holdComp = hold.GetComponent<HoldNote>();
        holdComp.SetJudge(holdNoteInfo.position);
        holdComp.Set(holdNoteInfo.line,holdNoteInfo.length, height);

        return hold;
    }

}
