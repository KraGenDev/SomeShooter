using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RPG : Weapon
{
    public GameObject rocket;
    private void OnEnable()
    {
        PlayerInput.Fire1 += Fire;
        PlayerInput.Reload += Reload;
    }

    private void OnDisable()
    {
        PlayerInput.Fire1 -= Fire;
        PlayerInput.Reload -= Reload;
    }

    public override void Fire()
    {
        if (canFire)
        {
            rocket.GetComponent<Rigidbody>().isKinematic = false;
            rocket.transform.parent = null;
            rocket.GetComponent<Rigidbody>()?.AddForce(firePoint.forward * shotForce);
            rocket.GetComponent<ParticleSystem>()?.Play();
            ammoInPrompt--;
            _animator.SetTrigger(Shot);
            _audioSource.clip = shot;
            _audioSource.Play();
            firePoint.GetComponent<ParticleSystem>()?.Play();
            canFire = false;
        }
    }

    public override void Reload()
    {
        if (!canFire && ammoInPrompt == 0)
        {
            rocket = Instantiate(ammoPrefab, firePoint.transform.position, firePoint.transform.rotation,transform);
            rocket.GetComponent<Rigidbody>().isKinematic = true;
            ammoInPrompt++;
            canFire = true;
        }
    }
}
