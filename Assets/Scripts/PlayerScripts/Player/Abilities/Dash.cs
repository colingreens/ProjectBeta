using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Dash : MonoBehaviour
    {
        public CharacterManager Player;

        [SerializeField]
        private InputEvent onDashPress;        
        [SerializeField]
        private Ability dashAbility;
        [SerializeField]
        private FloatVariable dashCoolDown;

        private float dashTimeLeft; 

        // Start is called before the first frame update
        void Start()
        {
            onDashPress.onKeyPress += OnKeyEvent;
            dashTimeLeft = dashCoolDown.Value;
        }

        // Update is called once per frame
        void Update()
        {
            dashTimeLeft -= Time.deltaTime;
        }

        public void OnKeyEvent()
        {
            if (dashTimeLeft < float.Epsilon)
            {
                Player.Velocity.x =+ Player.PositionInfo.facingPosition * dashAbility.Execute();
                dashTimeLeft = dashCoolDown.Value;
            }
                       
        }
    }
}
