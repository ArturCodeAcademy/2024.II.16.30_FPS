using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerGravity))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public bool Alive => !Health.IsDead;

    [field: SerializeField] public LayerMask PlayerLayerMask;

    [field: Header("Components")]
    [field: SerializeField] public Camera Camera { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public CharacterControllerGravity CharacterControllerGravity { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    private void Awake()
    {
		Instance = this;
	}
}
