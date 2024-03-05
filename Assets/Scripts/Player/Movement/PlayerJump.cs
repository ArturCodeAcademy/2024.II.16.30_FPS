using UnityEngine;

[RequireComponent(typeof(CharacterControllerGravity))]
public class PlayerJump : MonoBehaviour
{
    [Header("Params")]
    [SerializeField, Min(0.5f)] private float _jumpHeight = 1f;
    [SerializeField, Min(1)] private int _maxJump = 3;

    [Header("Components")]
    [SerializeField] private CharacterControllerGravity _characterControllerGravity;

    private int _leftJumps;
    private float _jumpVelocity;

    private void Awake()
    {
        RefreshJumpsCount();
        _jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * _jumpHeight);
    }

    private void OnEnable()
    {
        _characterControllerGravity.Land += RefreshJumpsCount;
    }

    private void OnDisable()
    {
        _characterControllerGravity.Land -= RefreshJumpsCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _leftJumps --> 0)
            _characterControllerGravity.SetVelocity(_jumpVelocity);
    }

    private void RefreshJumpsCount(object sender = default, LandEventArgs e = default)
    {
        _leftJumps = _maxJump;
    }

#if UNITY_EDITOR

    [ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
        if (TryGetComponent(out CharacterControllerGravity ccg))
            _characterControllerGravity = ccg;
    }

#endif
}
