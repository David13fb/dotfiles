using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.UI;

namespace Metroidvania
{
    public class UIHealthController : MonoBehaviour
    {
        [SerializeField] Slider healthBar;
        
        public void setHealthInBar(float h){
            healthBar.value = h;
        }
    }
}
