using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    #region Tags

    #endregion
    #region PlayerAnimationVar
    public static string PA_SUPER_ATTACK = "IsSuperAttack";
    public static string PA_ATTACK = "IsAttack";
    public static string PA_RUNING = "IsRuning";
    public static string PA_JUMPING = "IsJump";
    public static string PA_FALLING = "IsFall";
    public static string PA_GROUNDED = "IsGrounded";
    public static string PA_ATTACK_COUNTER = "IsAttackClickCounter";
    public static string PA_DEATH = "IsDeathTrigger";
    #endregion
    #region BanditEnemyAnimationVar
    public static string BEA_ATTACK = "IsAttack";
    public static string BEA_HURT = "IsHurt";
    public static string BEA_DEATH = "IsDeathTrigger";
    #endregion
    #region PlayerInput
    public static string VERTICAL_AXIS = "Vertical";
    public static string HORIZONTAL_AXIS = "Horizontal";
    public static string JUMP_INPUT = "Jump";
    public static string ATTACK_INPUT = "Fire1";
    public static string SUPER_ATTACK_INPUT = "Fire2";
    #endregion
    #region Layers
    public static string MOVE_PLATFORM_LAYER = "MovePlatform";
    public static string WATER_LAYER = "Water";
    public static string DAMAGE_LAYER = "Damage";
    #endregion
}
