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

    public GameObject CreateHold(List<HoldNoteInfo> holdBodyInfo)
    {
        GameObject holdBody = GameObject.Instantiate(holdNoteBody);
        HoldNoteBody holdBodyComp = holdBody.GetComponent<HoldNoteBody>();


        return holdBody;
    }

}
