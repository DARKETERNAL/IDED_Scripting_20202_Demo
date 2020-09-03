using UnityEngine;

public class EnemyStateMachine
{
    private float attackDistance;
    private float warningDistance;

    private IState idleState;
    private IState warningState;
    private IState attackState;

    private IState currentState;

    public EnemyStateMachine(EnemyController enemy)
    {
        idleState = new IdleState();
        warningState = new WarningState(enemy.gameObject);
        attackState = new AttackState(enemy);

        attackDistance = enemy.AttackDistance;
        warningDistance = enemy.WarningDistance;

        currentState = idleState;
    }

    public void Execute(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackDistance)
        {
            if (currentState != attackState)
            {
                Debug.Log("Changed to attack");
                ChangeState(attackState);
            }
        }
        else if (distanceToPlayer <= warningDistance)
        {
            if (currentState != warningState)
            {
                Debug.Log("Changed to warning");
                ChangeState(warningState);
            }
        }
        else if (currentState != idleState)
        {
            Debug.Log("Changed to idle");
            ChangeState(idleState);
        }

        currentState.Execute();
    }

    private void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.Init();
    }
}