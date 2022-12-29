using UnityEngine;

public class RandomVolumeUp : MonoBehaviour
{
    [SerializeField] AudioSource source;

    private const float HIGH_VOLUME = 0.7f;
    private const float LOWER_VOLUME = 0.3f;
    private float time;
    private float _lenghtSource;

    private void Start()
    {
        _lenghtSource = source.clip.length;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > _lenghtSource)
        {
            time = 0;

            if (Random.Range(0,1) < 0.5f)
            {
                source.volume = HIGH_VOLUME;

            }else{

            	source.volume = LOWER_VOLUME;
            }
        }
    }
}
