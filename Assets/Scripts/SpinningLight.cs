using UnityEngine;
namespace NanoMesh
{
    public class SpinningLight : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private GameObject lightAnchor;
        private void Update()
        {
            transform.RotateAround(lightAnchor.transform.position, Vector3.left, speed * Time.deltaTime);
        }
    }
}
