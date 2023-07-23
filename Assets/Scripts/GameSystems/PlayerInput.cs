using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerInput : MonoBehaviour
{
    PlayerUnit player;
    void Start()
    {
        player = GetComponent<PlayerUnit>();
    }

    void Update()
    {
        InputBehaviour();
    }
    private void InputBehaviour()
    {
        var horizontalInput = Input.GetAxis(GlobalVar.HORIZONTAL_AXIS);
        var jumpInput = Input.GetButtonDown(GlobalVar.JUMP_INPUT);
        var attackInput = Input.GetButton(GlobalVar.ATTACK_INPUT);
        var superAttackInput = Input.GetButtonDown(GlobalVar.SUPER_ATTACK_INPUT);
        player.Movement(horizontalInput, jumpInput);
        player.AttackInput(attackInput);
        if(superAttackInput)
            player.SuperAttackInit();
    }
}
