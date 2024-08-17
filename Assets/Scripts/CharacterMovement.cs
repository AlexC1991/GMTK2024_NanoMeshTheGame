using UnityEngine;
namespace NanoMesh
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private NanoDataContainer nanoDC;
        [SerializeField] private Animator animator;
        
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontal, 0, vertical);
            transform.Translate(movement * speed * Time.deltaTime);
            animator.SetFloat("Blend", movement.magnitude);
        }
    }
}
