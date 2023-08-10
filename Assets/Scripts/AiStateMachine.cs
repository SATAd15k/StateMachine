using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum state
{
    Shoot,
    Run,
    Patrol
}

public class AiStateMachine : MonoBehaviour
{
    [SerializeField] private state _aiState = state.Shoot;

    private SpriteRenderer _sprite;

    [SerializeField] private Transform _player;

    [SerializeField] private Transform _eyes1;
    [SerializeField] private Transform _eyes2;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        NextState();
    }

    private void NextState()
    {
        switch (_aiState)
        {
            case state.Shoot:
                StartCoroutine(ShootState());
                break;
            case state.Run:
                StartCoroutine(RunState());
                break;
            case state.Patrol:
                StartCoroutine(PatrolState());
                break;
            default:
                Debug.LogError("You Shouldn't be here");
                break;
        }
    }

    private IEnumerator PatrolState() // Unity uses IEnumerator to make state machines
    {
        Debug.Log("Patrol State");
        while (_aiState == state.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, 15f * Time.deltaTime); //Quaternions cannot be + together only *

            // From A to B calc is done by B - A
            Vector2 directionToPlayer = _player.position - transform.position;

            if (Vector2.Angle(transform.right, directionToPlayer) <5f)
            {
                _aiState = state.Run;
            }

            /* (Input.GetKeyDown(KeyCode.Space))
            {
                _aiState = state.Run;
            }*/
            yield return null;
        }
        yield return null;
        Debug.Log("End Patrol");
        NextState();
    }

    private IEnumerator RunState()
    {
        Debug.Log("Run State");
        float startTime = Time.time;
        while (_aiState == state.Run)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.2f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.2f + 1f;

            transform.localScale = new Vector3(wave2, wave, 1);

            float shimmy = Mathf.Sin(Time.time * 30f) * 0.9f + 0.3f;
            transform.position += transform.right * shimmy * Time.deltaTime;

            if (Time.time - startTime> 3f)
            {
                _aiState = state.Shoot;
            }
            
            yield return null;
        }
        transform.localScale = Vector3.one;
        yield return null;
        Debug.Log("End Run");
        NextState();
    }

    /*
    private IEnumerator WiggleState()
    {
        Debug.Log("Run State");
        float startTime = Time.time;
        while (_aiState == state.Wiggle)
        {
            float wave = Mathf.Sin(Time.time * 30f) * 0.2f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.2f + 1f;

            _eyes1.transform.localScale = new Vector3(wave2, wave, 1);
            _eyes2.transform.localScale = new Vector3(wave2, wave, 1);

            if (Time.time - startTime > 2f)
            {
                _aiState = state.Run;
            }

            yield return null;
        }
        transform.localScale = Vector3.one;
        yield return null;
        Debug.Log("End Run");
        NextState();
    }
    */

    private IEnumerator ShootState()
    {
        Debug.Log("Shoot State");
        int colorChangeCount = 0;
        while (_aiState == state.Shoot)
        {
            yield return new WaitForSeconds(0.5f);
            _sprite.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));
            colorChangeCount++;

            if (colorChangeCount >= 5)
            {
                _aiState = state.Patrol;
            }


            yield return null;
        }
        yield return null;
        Debug.Log("End Shoot");
        NextState();
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
