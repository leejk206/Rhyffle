using UnityEngine;

public class AudioTest : MonoBehaviour
{
    float ct = 0;
    public float timer;
    bool played = false;
    public bool paused = false;
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            ct += Time.deltaTime;
            if (!played)
            {
                if (ct > 2)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                    played = true;
                }
            }
        }
    }

    public void PauseSong()
    {
        gameObject.GetComponent<AudioSource>().Pause();
        paused = true;
    }

    public void ResumeSong()
    {
        gameObject.GetComponent<AudioSource>().UnPause();
        paused = false;
    }

}
