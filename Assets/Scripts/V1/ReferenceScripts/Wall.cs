using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Wall : MonoBehaviour
    {
        [SerializeField]
        private AudioClip chopSound1;
        private AudioClip chopSound2;
        public Sprite dmgSprite;
        public int hp = 3;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DamageWall(int loss)
        {
            //SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);

            spriteRenderer.sprite = dmgSprite;       
            hp -= loss;
            if (hp <= 0)
                gameObject.SetActive(false);
        }
    }
}
