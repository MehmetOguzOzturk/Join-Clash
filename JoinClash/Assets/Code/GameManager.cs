using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<GameObject> stickmanList = new List<GameObject>();
    public GameObject[] enemyList;

    public float speed;
    public float moveToBoss;
    public float enemyspeed;
    public float minDistanceOfBoss;
    public bool lockOn;



    GameObject boss;
    

    private void Start()
    {
        enemyList= GameObject.FindGameObjectsWithTag("enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if (stickmanList.Count==0)
        {
            GetComponent<PlayerController>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("attack"))
        {
            for (int i = 1; i <= enemyList.Length; i++)
            {
                float distance = Mathf.Abs(stickmanList[stickmanList.Count - i].transform.position.z - enemyList[i - 1].transform.position.z);

                if (distance < 10f && enemyList[i - 1].activeSelf)
                {
                    stickmanList[stickmanList.Count - i].transform.parent = null;
                    stickmanList[stickmanList.Count - i].transform.LookAt(enemyList[i - 1].transform.position);
                    stickmanList[stickmanList.Count - i].transform.Translate(Vector3.forward * speed * Time.deltaTime);

                    enemyList[i - 1].transform.Translate(Vector3.forward * enemyspeed * Time.deltaTime,Space.Self);
                    enemyList[i - 1].GetComponent<Animator>().SetBool("Run",true);
                }

            }
        }

        if (other.gameObject.CompareTag("attackBoss"))
        {
            GetComponent<PlayerController>().isFight = true;

            if (stickmanList.Count == 0)
            {
                boss.GetComponent<Animator>().SetTrigger("GameOver");
                boss.GetComponent<Animator>().SetBool("Run", false);
                boss.GetComponent<Animator>().SetBool("AttackPlayer", false);
            }
                


            foreach (var stickman in stickmanList)
            {
                if (stickman != null)
                {
                    Vector3 bossDistance = boss.transform.position - stickman.transform.position;
                    if (bossDistance.magnitude <= minDistanceOfBoss)
                    {
                        HitBoss(stickman);
                        AttackPlayers(stickman);
                    }
                    else
                        MoveTowardsBoss(stickman);
                }

            }
        }

    }

    private void AttackPlayers(GameObject stickman)
    {
        lockOn=false;

        if (!lockOn)
        {
            boss.transform.DOLookAt(stickman.transform.position,0.25f, AxisConstraint.None, Vector3.up);
            boss.transform.DOMove(stickman.transform.position+new Vector3(0,0,3), 3f);
            boss.GetComponent<Animator>().SetBool("Run", true);
            lockOn = true; 
        }
        if (lockOn)
        {
            boss.GetComponent<Animator>().SetBool("AttackPlayer", true);
        }
        if (stickman==null)
            lockOn = false;

        
    }

    private void HitBoss(GameObject stickman)
    {
        stickman.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stickman.transform.LookAt(boss.transform.position);
        stickman.GetComponent<Animator>().SetBool("Punch", true);

        if (boss.activeSelf==false)
        {
            stickman.GetComponent<Animator>().SetTrigger("Dance");
        }
    }

    private void MoveTowardsBoss(GameObject stickman)
    {
        stickman.GetComponent<Animator>().SetBool("Punch", false);
        stickman.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stickman.GetComponent<Rigidbody>().isKinematic = false;
        stickman.GetComponent<CapsuleCollider>().isTrigger = false;
        stickman.transform.position = Vector3.MoveTowards(stickman.transform.position, boss.transform.position, moveToBoss * Time.fixedDeltaTime);
        stickman.transform.LookAt(boss.transform.position);
    }
}
