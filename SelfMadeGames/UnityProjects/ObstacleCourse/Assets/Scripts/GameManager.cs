using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // this class controles game actions like obstacles, timers, death and respawn

    //to Inject
    private List<Transform> _respawnPointsList;
    private List<DisappearingPlatform> _dissappearingPlatforms;
    private IPlayerDeathHandler _playerDeathHandler;
    private CoroutinePerformer _coroutinePerformer;


    [Inject]
    private void Construct(IPlayerDeathHandler playerDeathHandler, List<Transform> respawnPoints,
        CoroutinePerformer coroutinePerformer, List<DisappearingPlatform> dissappearingPlatforms)
    {
        _playerDeathHandler = playerDeathHandler;
        _respawnPointsList = respawnPoints;
        _coroutinePerformer = coroutinePerformer;
        _dissappearingPlatforms = dissappearingPlatforms;
    }

    private void Start()
    {
            _playerDeathHandler.PlayerDied.AddListener(OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        float playerZPosition = _playerDeathHandler.PlayerTransform.position.z;

        Transform respawnPoint = FindRespawnPoint(playerZPosition);

        _coroutinePerformer.StartCoroutine(Respawn(respawnPoint));

    }

    private IEnumerator Respawn(Transform respawnPoint)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUserInteraction();

        if (respawnPoint != null)
        {
            _playerDeathHandler.PlayerTransform.position = respawnPoint.position;
            _playerDeathHandler.TriggerPlayerRespawned();
        }

        ResetPlatforms();
    }

    private Transform FindRespawnPoint(float playerZ)
    {
        return _respawnPointsList
            .Where(respawnPoint => respawnPoint.position.z < playerZ)
            .OrderByDescending(respawnPoint => respawnPoint.position.z)
            .FirstOrDefault();
    }

    private void ResetPlatforms()
    {
        foreach (var platform in _dissappearingPlatforms)
        {
            platform.Reset();
        }
    }

}
