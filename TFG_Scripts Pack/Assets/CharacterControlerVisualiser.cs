using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControlerVisualiser : MonoBehaviour
{

    private CharacterController cc;

    [SerializeField]
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = cc.isGrounded;


    }
}
