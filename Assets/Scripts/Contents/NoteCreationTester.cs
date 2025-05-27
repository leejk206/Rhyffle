using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class NoteCreationTester : MonoBehaviour
{
    bool timerUp = true;
    float speed = 480;
    float timer2 = 0;
    float timer = 0.5f;
    public List<GameObject> notes;
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0.25f)
        {
            timer = 0;
            GameObject g = Instantiate(prefab);
            int lane = UnityEngine.Random.Range(0, 21);
            int length = UnityEngine.Random.Range(1, 21 - lane);
            g.GetComponent<Note>().set(lane, length);
            notes.Add(g);
        }

        if(timer2 >= 10)
        {
            timerUp = !timerUp;
            timer2 = 0;
        }


        for (int i = 0; i < notes.Count; i++)
        {
            try
            {
                notes[i].GetComponent<Note>().drop(speed * 2);
            }
            catch (Exception e)
            {
                notes.RemoveAt(i);
            }
        }
        if (timerUp)
        {
            speed += 144 * Time.deltaTime;
        }
        else
        {
            speed -= 144 * Time.deltaTime;
        }
        timer2 += Time.deltaTime;
        timer += Time.deltaTime;
    }
}
