using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] CinemachineCamera DeathVirtualCamera;
    [SerializeField] Transform WeaponCamera;

    int _gameOverCameraPriority = 20;

    public override void Death()
    {
        WeaponCamera.parent = null;
        DeathVirtualCamera.Priority = _gameOverCameraPriority;
        Destroy(this.gameObject);
    }

}
