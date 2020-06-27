using UnityEngine;

namespace Magic
{
    public class GetCurrentRotation : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            SetRotation();
        }

        public void SetRotation()
        {
            transform.rotation = target.rotation;
        }
    }
}