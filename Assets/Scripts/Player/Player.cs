using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerGravity))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [field: SerializeField] public LayerMask PlayerLayerMask;

    [field: Header("Components")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public CharacterControllerGravity CharacterControllerGravity { get; private set; }

    private void Awake()
    {
		Instance = this;
	}
}
