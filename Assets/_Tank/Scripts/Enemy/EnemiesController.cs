using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using _Tank.Scripts;
using _Tank.Scripts.Factory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    #region attributes

    private readonly List<EnemyBase> m_Registry = new();
    private readonly WaitForSeconds m_Wait = new (1f);
    
    [Inject] private PlayerMovement m_Player;
    [Inject] private EnemyFactory m_Factory;
    [Inject] private TankHealth m_TankHealth;
    
    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private int m_MaxEnemies;
    [SerializeField] private EnemyType[] m_EnemyTypes;

    private IEnumerator m_SpawnRoutine;

    #endregion

    #region engine methods

    private void Start()
    {
        m_SpawnRoutine = SpawnRoutine();
        StartCoroutine(m_SpawnRoutine);
        m_TankHealth.Killed += StopSpawn;
    }

    private void OnDisable()
    {
        m_TankHealth.Killed -= StopSpawn;
    }

    #endregion

    #region public methods

    public void StopSpawn()
    {
        if (m_SpawnRoutine != null)
        {
            StopCoroutine(m_SpawnRoutine);
            m_SpawnRoutine = null;
        }

        foreach (EnemyBase enemyBase in m_Registry)
        {
            if(enemyBase != null)
                enemyBase.Stop();
        }
    }

    public void EnemyDead(EnemyBase _Enemy)
    {
        m_Registry.Remove(_Enemy);
        m_Factory.ReturnToPool(_Enemy);
        SpawnNew();
    }

    private void SpawnNew()
    {
        if(m_Registry.Count >= m_MaxEnemies)
            return;

        int spawnId = Random.Range(0, m_SpawnPoints.Length);
        int enemyId = Random.Range(0, m_EnemyTypes.Length);
            
        EnemyBase enemy = m_Factory.GetObjectOfType(m_EnemyTypes[enemyId]);
        m_Registry.Add(enemy);
        enemy.Spawn(m_SpawnPoints[spawnId], this);
    }

    #endregion

    #region service methods

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return m_Wait;

            foreach (EnemyBase enemyBase in m_Registry)
            {
                if(enemyBase != null)
                    enemyBase.UpdateTarget(m_Player.transform.position);
            }
            
            SpawnNew();
        }
    }

    #endregion
}
