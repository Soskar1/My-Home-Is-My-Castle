using UnityEngine;

namespace Main
{
    public class Game : MonoBehaviour
    {
        public Controls controls;
        
        private void Awake() => controls = new Controls();
        private void OnEnable() => controls.Enable();
        private void OnDisable() => controls.Disable();
    }
}
