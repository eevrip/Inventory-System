using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;
    private float speed;
    private bool isPickingUp;
    private Rig rig;
    private bool isRightHandPick = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rig = GetComponent<RigBuilder>().layers[0].rig;
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetHorizontalSpeed();
        animator.SetFloat("velocity",speed);
        //animator.SetTrigger("pickingUp");
    }
    public void SetIsPickingUp()
    {
        //isPickingUp = pickingUp;
        animator.SetTrigger("pickingUp");
        
    }
    public void SetIsAttacking()
    {
        if (isRightHandPick)
        {
            rig.weight = 1f;
            StartCoroutine(ExecuteAfter(0.5f));
        }
        animator.SetTrigger("attack");
       

    }
    public void SetLeftHandPickingUp()
    {   
        isRightHandPick = false;
        rig.weight = 1f;
        animator.SetLayerWeight(1, 0);//right hand pick
        animator.SetLayerWeight(2, 1);//left hand pick
       // animator.SetLayerWeight(3, 1);//right hand attack

    }
    public void SetRightHandPickingUp()
    {
        isRightHandPick = true;
        rig.weight = 0f; 
        animator.SetLayerWeight(1, 1);//right hand pick
        animator.SetLayerWeight(2, 0);//left hand pick
       // animator.SetLayerWeight(3, 0);//right hand attack

    }
    private IEnumerator ExecuteAfter(float time)
    {

        yield return new WaitForSeconds(time);
        rig.weight = 0f;
    }
}
