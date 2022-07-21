using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFire
{
    void Fire();
}
abstract class Weapon : MonoBehaviour, IFire
{
    [SerializeField] protected int ammoCount;
    [SerializeField] protected float timeReload;
    [SerializeField] protected int countPrompt;
    [SerializeField] protected int shotForce;
    [SerializeField] protected float shotInterval;

    [SerializeField] protected GameObject ammoPrefab;
    [SerializeField] protected Transform firePoint;
    
    [Header("Audio Clipes")]
    [SerializeField] protected AudioClip shot;
    [SerializeField] protected AudioClip reload;
    [SerializeField] protected AudioClip noAmmo;

    public List<GameObject> ammo;
    public int ammoInPrompt;

    protected AudioSource _audioSource;
    protected Animator _animator;
    protected bool canFire = false;
    protected static readonly int Shot = Animator.StringToHash("Shot");

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        for (int i = 0; i < ammoCount; i++)
        {
            ammo.Add(SpawnAmmo());
        }
    }
    GameObject SpawnAmmo()
    {
        GameObject ammo = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        ammo.SetActive(false);
        return ammo;
    }

    public virtual void Fire()
    {
        StartCoroutine(FireCorutine());
    }

    public virtual void Reload()
    {
        _animator.SetTrigger("Reload");
        _audioSource.clip = reload;
        _audioSource.Play();
        canFire = false;
        StartCoroutine(DefaultReload());
    }
    IEnumerator DefaultReload()
    {
        for (int i = 0; i < ammoCount; i++)
        {
            ammo[i].SetActive(false);
            ammo[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(timeReload);
        ammoInPrompt = ammoCount;
        canFire = true;
        StopCoroutine(DefaultReload());
    }

    IEnumerator FireCorutine()
    {
        if (ammoInPrompt > 0 && canFire)
        {
            canFire = false;
            ammo[ammoInPrompt - 1].transform.position = firePoint.transform.position;
            ammo[ammoInPrompt - 1].transform.eulerAngles = new Vector3(firePoint.transform.eulerAngles.x,180,firePoint.transform.eulerAngles.z);
            ammo[ammoInPrompt - 1].SetActive(true);
            ammo[ammoInPrompt - 1].GetComponent<Rigidbody>().AddForce(firePoint.forward * shotForce);
            ammoInPrompt--;
            
            _animator.SetTrigger(Shot);
            _audioSource.clip = shot;
            _audioSource.Play();
            firePoint.GetComponent<ParticleSystem>()?.Play();
            
            yield return new WaitForSeconds(shotInterval);
            canFire = true;
        }
        else if (ammoInPrompt == 0)
        {
            _audioSource.clip = noAmmo;
            _audioSource.Play();
        }
        StopCoroutine(FireCorutine());
    }
}