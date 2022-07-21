using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


abstract class ArtileryController : MonoBehaviour
{
    [Header("Components of Artillery")]
    [SerializeField] private Transform shieldOfArtillery; //щит артилерии либо то что нужно вращать для коректироваки
    [SerializeField] private Transform targetForShield;// цель к которой вращается "щит"
    [SerializeField] private GameObject ammoPrefab;// префаб снаряда
    [SerializeField] private Transform positionForShot;//позиция появления снаряда
    [SerializeField] private List<GameObject> ammonation;// масив снарядов


    [Header("Parameters")]
    [SerializeField] private int countAmmo;// боезапас
    [SerializeField] private int forceShot; // сила выстрела
    [SerializeField] protected byte maxVerticalAngle;// максимальный угол подъёма "щита"
    [SerializeField] protected int maxHorizontalAngle;// максимальный угол поворота "щита"
    [SerializeField] protected float timeReload;
    private byte actualNumberAmmo; // номер снаряда 
    private bool isReady;


    [Header("Other Game Compontnts")] 
    [SerializeField] private Transform poolForAmmo;// родительский объект для масива
    [SerializeField] private ParticleSystem shotEffect; 
    [SerializeField] private AudioClip shotSound;// еффект выстрела
    private Animator animator;
    private AudioSource audioSource;
    
    void Start()
    {
        ammonation.Capacity = countAmmo;
        animator = shieldOfArtillery.parent.gameObject.GetComponent<Animator>();
        audioSource = shieldOfArtillery.parent.gameObject.GetComponent<AudioSource>();
        audioSource.clip = shotSound;

        for (int i = 0; i < countAmmo; i++)
        {
            ammonation.Add(SpawnAmmo());
        }
        isReady = true;
    }

    GameObject SpawnAmmo()
    {
        GameObject ammo = Instantiate(ammoPrefab, transform.position, Quaternion.identity, poolForAmmo);
        ammo.SetActive(false);
        return ammo;
    }
    void Update()
    {
        //targetForShield.localRotation = Quaternion.Euler(0,0,0);//вращение цели 
        //shieldOfArtillery.rotation = Quaternion.Lerp(shieldOfArtillery.rotation, targetForShield.rotation,1 * Time.deltaTime);//плавное вращение "щита" за целью
        shieldOfArtillery.DORotate(targetForShield.rotation.eulerAngles,1);
    }
    
    public void Shot()
    {
        if (actualNumberAmmo < countAmmo && isReady)
        {
            animator.SetTrigger("Shot");
            audioSource.Play();
            ammonation[actualNumberAmmo].SetActive(true);
            ammonation[actualNumberAmmo].transform.position = positionForShot.position;
            ammonation[actualNumberAmmo].transform.rotation = positionForShot.rotation;
            ammonation[actualNumberAmmo].GetComponent<Rigidbody>()?.AddForce(positionForShot.transform.forward * forceShot, ForceMode.Impulse);
            actualNumberAmmo++; 
            shotEffect.Play();
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReady = false;
        while(true)
        {
            if (actualNumberAmmo < countAmmo)
            {
                yield return new WaitForSeconds(timeReload);
                isReady = true;
                break;
            }
            else
            {
                Debug.Log("No Ammo");
                break;
            }
        }
    }
}
