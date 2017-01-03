using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : AbstrackPlayer, IPlayer
{
    [SerializeField]
    private Rigidbody rigid;

    private Vector3 vel;

    public bool isDead;

    private CommandDirection dir;

    [SerializeField]
    private ParticleSystem parDeath;
    void Start()
    {
        vel = Vector3.zero;
        pos = transform.position;
        dir = CommandDirection.NONE;
    }

    public void ExecuteCommandDirection(List<CommandDirection> listCmdDir)
    {

    }
    public void ExecuteCommandDirection(CommandDirection cmdDir)
    {
        dir = cmdDir;
        if (cmdDir == CommandDirection.NORTH)
        {
            vel = Vector3.forward;
        }
        else if (cmdDir == CommandDirection.SOUTH)
        {
            vel = Vector3.back;


        }
        else if (cmdDir == CommandDirection.EAST)
        {
            vel = Vector3.right;
        }
        else if (cmdDir == CommandDirection.WEST)
        {
            vel = Vector3.left;
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (dir != CommandDirection.NONE)
            {
                Ray rayDown = new Ray(transform.position, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(rayDown, out hit, 0.2f))
                {
                    if (hit.collider.gameObject.tag == "Ground")
                    {
                        bool isPlayerHitTheWall = false;
                        Ray ray = new Ray(transform.position, vel);

                        if (Physics.Raycast(ray, out hit, 0.25f))
                        {
                            if (hit.collider.tag == "wall")
                            {
                                vel.x *= (-1);
                                vel.z *= (-1);
                            }
                        }
                    }

                }
                else
                {
                    if (!isDead)
                    {
                        vel.y = (-1);
                    }

                }

                {
                    pos = pos + vel * Time.deltaTime*2;
                    transform.position = pos;
                }
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shadow")
        {
            Death();
        }
        if (other.gameObject.tag == "Coin")
        {
            mainGameScript.ExeEatCoin(other.gameObject);
        }
        if (other.gameObject.tag == "plane")
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        parDeath.Play();
        mainGameScript.PlayerDeath();
    }
    public void Init(float posX, float posY, int delay)
    {

    }

    public void UpdatePosition(List<Vector3> pos)
    {

    }
}

