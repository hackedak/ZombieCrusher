using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed;   //for storing speed in x, y and z direction
    public float xSpeed = 8f, zSpeed = 15f;   
    public float acceleration = 18f, decelaration = 18f;
    public float lowSound, normalSound, highSound;
    public AudioClip engineOnSound, engineOffSound;
    
    protected float rotationSpeed = 10f; // used in giving a turning effect for the tank when changing direction
    protected float maxAngle = 10f;

    private bool isSlow;
    private AudioSource soundManager;

  // On awake, initialize tank speed
    void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        speed  = new Vector3(0f,0f, zSpeed);
    }

    

    // functions for controlling tank

    protected void MoveLeft()
    {
        speed = new Vector3(-xSpeed/2f, 0f, speed.z);
    }

    protected void MoveRight()
    {
        speed = new Vector3(xSpeed/2f, 0f, speed.z);
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        if(isSlow){
            isSlow = false;
            soundManager.Stop();
            soundManager.clip = engineOnSound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, zSpeed);

    }

    protected void MoveSlow()
    {
        if(isSlow){
            isSlow = true;
            soundManager.Stop();
            soundManager.clip = engineOffSound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, decelaration);
        
    }

    protected void MoveFast(){
        speed = new Vector3(speed.x, 0f, acceleration);
    }

}// class
