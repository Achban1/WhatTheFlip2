using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimationHandlerPlayer1 : MonoBehaviour
{
    PlayerMovement playerController;

    [SpineAnimation] public string idle, run, jump;
    public SkeletonAnimation skeletonAnimation;


    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerMovement>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();

    }

    void Update()
    {
        string animationName = "";

        switch (playerController.state)
        {
            case PlayerState.Idle:
                animationName = idle;
                break;
            case PlayerState.Run:
                animationName = run;
                break;
            case PlayerState.Jump:
                animationName = jump;
                break;
        }

        PlayAnimation(animationName);
    }

    private void PlayAnimation(string animation)
    {
        if (skeletonAnimation.AnimationName != animation)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animation, true);
        }
    }
}
