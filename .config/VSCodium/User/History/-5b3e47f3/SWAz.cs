using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.UI;

namespace Metroidvania
{
    public class UIHealthController : MonoBehaviour
    {
        [SerializeField] Slider healthBar;
        [SerializeField] Slider manaBar;

        public void setHealthInBar(float h){
            healthBar.value = h;
        }
        public void setManaBar(float h){
            manaBar.value = h;
        }
       
    }
}
