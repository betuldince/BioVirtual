using UnityEngine;
using System.Collections;

public class RandomVolumeUp : MonoBehaviour
{
    [SerializeField] AudioSource birdSource;

    private float time;
    private float lenghtBird;

    private void Start()
    {
        lenghtBird = birdSource.clip.length;
    }
    void Update()
    {
        time += Time.deltaTime;

        if (time > lenghtBird)
        {
            time = 0;

            if (Random.Range(0,1) < 0.5f)
            {
                birdSource.volume = 0.7f;

            }else{
            	birdSource.volume = 0.3f;
            }

        }


        
    }

}
