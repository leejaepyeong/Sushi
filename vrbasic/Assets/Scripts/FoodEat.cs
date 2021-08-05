using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodEat : MonoBehaviour
{
    public int score;

    bool isEat = false;

    public GameObject Target;
    public GameObject Plat;

    public NavMeshAgent nav;
    public Rigidbody rig;
    public AudioSource audio;

    private void Start()
    {
        nav.SetDestination(Target.transform.position);

        nav.speed = GameManager.instance.level * 0.5f + 2.5f;

        
    }


    IEnumerator EatFood()
    {
        Destroy(Plat);
        isEat = true;

        audio.Play();
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.GetScore(score);
        
        Destroy(gameObject);
        isEat = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isEat)
        {
            StartCoroutine(EatFood());
        }
        else if(other.tag == "Goal")
        {
            Destroy(gameObject);
            GameManager.instance.GetScore(-1 * score);
        }
    }
}
