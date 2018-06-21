using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HundredBalls
{
    public class PaddleScripts : MonoBehaviour
    {

        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("isOpened", true);
            }
            else
            {
                anim.SetBool("isOpened", false);
            }
        }
    }
}