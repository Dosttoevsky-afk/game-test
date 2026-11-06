
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Настройки игрока")]
    public Transform player;

    [Header("Зона видимости")]
    public float viewDistance = 10f;
    [Range(0, 360)]
    public float viewAngle = 120f;

    [Header("Настройки преследования")]
    public float chaseDistance = 15f;
    public float stopDistance = 2f;

    private NavMeshAgent agent;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null || agent == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Если игрок попал в зону видимости
        if (!isChasing && CanSeePlayer())
        {
            isChasing = true;
        }

        // Если моб уже преследует игрока
        if (isChasing)
        {
            // Если игрок слишком далеко, прекращаем погоню
            if (distance > chaseDistance)
            {
                isChasing = false;
                agent.ResetPath();
                return;
            }

            // Бежим за игроком
            if (distance > stopDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Проверяем расстояние
        if (distance > viewDistance)
            return false;

        // Проверяем угол обзора
        float angle = Vector3.Angle(
            transform.forward,
            direction
        );

        if (angle > viewAngle / 2f)
            return false;

        // Проверяем, нет ли препятствия между мобом и игроком
        if (Physics.Raycast(
            transform.position + Vector3.up,
            direction.normalized,
            out RaycastHit hit,
            viewDistance))
        {
            if (hit.transform == player ||
                hit.transform.IsChildOf(player))
            {
                return true;
            }
        }

        return false;
    }
}
