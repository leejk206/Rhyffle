using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHandler : MonoBehaviour
{
    // 씬 이동을 위한 버튼 핸들러
    
    // 홈 버튼
    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    
    // 게임 플레이 -> 앨범 선택 화면 이동
    public void GoToSelectAlbumScene()
    {
        SceneManager.LoadScene("SelectAlbum");
    }
    
    // 
    public void GoToSelectSongScene()
    {
        SceneManager.LoadScene("SelectSong");
    }
    
    // 덱 관리(메인) -> 덱 빌딩 씬
    public void GoToDeckBuildScene()
    {
        SceneManager.LoadScene("DeckBuildScene");
    }
    
    public void GoToCardLlistScene()
    {
        SceneManager.LoadScene("CardListScene");
    }

    // 곡 선택 씬 -> 게임 씬 (로딩 씬 추가)
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
