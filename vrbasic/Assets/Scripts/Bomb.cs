using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bomb : MonoBehaviour
{
    public int Damage; // 1~3
    float bombTime;
    

    public bool isBomb;
    bool isWalk = true;

    public GameObject Target;
    public GameObject BombEffect;

    public Rigidbody rigid;
    public AudioSource audio;
    public NavMeshAgent nav;
    public Animator anim;

    private void Start()
    {
        nav.SetDestination(Target.transform.position);

        nav.speed = GameManager.instance.level * 0.5f + 2.5f;

        anim.SetBool("Walk", isWalk);
    }



    IEnumerator TryBomb()
    {
        isBomb = true;
        nav.isStopped = true;
        nav.enabled = false;

        anim.SetTrigger("Bomb");
        

        yield return new WaitForSeconds(4f);

        audio.Play();
        BombEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);

    }

    IEnumerator DestroyBomb()
    {
        GameManager.instance.GetScore(500);

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Goal") && !isBomb)
        {
            StartCoroutine(TryBomb());
        }
       else if(other.tag == "Trash")
        {
            StartCoroutine(DestroyBomb());
        }
    }

}
