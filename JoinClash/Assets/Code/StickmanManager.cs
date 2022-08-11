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
            other.gameObject.SetActive(false);
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
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
        }

        if (other.gameObject.CompareTag("jump"))
        {
            transform.DOJump(transform.position + new Vector3(0, 0, 5), 3, 1, 1);
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
