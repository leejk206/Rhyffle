using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneUI : UI_Base
{
    enum Images
    {
        AlbumImage,
        ExtraImage,
        LoadingImage,
        ModeImage,
        UserImage1,
        UserImage2,

    }

    enum TMPs
    {
        Artist,
        SongName,
        ExtraText,
        LoadingText,
        UserIDText1,
        UserIDText2,

    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(TMPs));

        TextMeshProUGUI text = GetTMP((int)TMPs.LoadingText);
        //text.text = "Foo";
    }
}
