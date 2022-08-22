using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickmanManager : MonoBehaviour
{
    GameObject playerParent;
    GameObject boss;
    int stickmanHealt=2;
    public GameObject particleEffect;

    private void Start()
    {
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if (playerParent.GetComponent<PlayerController>().isRun==false && gameObject.tag=="member")
        {
            gameObject.GetComponent<Animator>().SetBool("Run", false);
        }
        else if(gameObject.tag == "member")
        {
            gameObject.GetComponent<Animator>().SetBool("Run", true);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stickman") && transform.parent != null )
        {
            other.gameObject.transform.parent = playerParent.transform;
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            other.gameObject.GetComponent<Animator>().SetBool("Run", true);
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = GetComponentInChildren<SkinnedMeshRenderer>().material;
            other.gameObject.tag = "member";
            playerParent.GetComponent<GameManager>().stickmanList.Add(other.gameObject);
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Die");
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            playerParent.GetComponent<GameManager>().enemyList.Remove(other.gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("Die");
            StartCoroutine(RemoveFromList());
        }

        if (other.gameObject.CompareTag("Boss"))
            InvokeRepeating("DamageBoss", 0, 1f);

        if (other.gameObject.CompareTag("obstacle"))
        {
            GameObject effect= Instantiate(particleEffect,transform.position,Quaternion.identity);
            Destroy(effect, 1);
            playerParent.GetComponent<GameManager>().stickmanList.Remove(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.parent = null;
        }

        if (other.gameObject.CompareTag("jump"))
        {
            transform.DOMoveY(2.5f, 0.3f).SetLoops(2,LoopType.Yoyo);
        }

    }

    void DamageBoss()
    {
        Debug.Log("Hit");
        boss.GetComponent<BossManager>().TakeDamageFromPlayers();
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForSeconds(1.6f);
        playerParent.GetComponent<GameManager>().stickmanList.Remove(gameObject);
        gameObject.SetActive(false);
    }

    public void StickmanTakeDamage()
    {
        stickmanHealt--;
        if (stickmanHealt==0)
        {
            GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);
            playerParent.GetComponent<GameManager>().stickmanList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    


}
