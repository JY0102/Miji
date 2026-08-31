using Miji.Gameplay.Enemies;
using NUnit.Framework;
using static Miji.Gameplay.Enemies.EnemyAI;

namespace Miji.Gameplay.Tests
{
    /// <summary>
    /// EnemyAI의 전이표(PlayMaker로 옮길 부분)는 순수 함수라 여기서 잰다.
    /// 우선순위 = 리시 > 사거리 > 감지. 이게 깨지면 야생이 영역 밖까지 쫓거나 붕괴자가 물러선다.
    /// </summary>
    public class EnemyAITests
    {
        [Test] public void OutOfRange_StaysPatrol()
            => Assert.AreEqual(State.Patrol, NextState(State.Patrol, inAggro: false, inAttackRange: false, beyondLeash: false));

        [Test] public void PlayerNear_PatrolToChase()
            => Assert.AreEqual(State.Chase, NextState(State.Patrol, inAggro: true, inAttackRange: false, beyondLeash: false));

        [Test] public void PlayerClose_ChaseToAttack()
            => Assert.AreEqual(State.Attack, NextState(State.Chase, inAggro: true, inAttackRange: true, beyondLeash: false));

        [Test] public void Wildlife_LeashOverridesEverything()
            => Assert.AreEqual(State.Patrol, NextState(State.Chase, inAggro: true, inAttackRange: true, beyondLeash: true));

        [Test] public void Collapser_NeverLeashes_KeepsChasing()
            // 붕괴자는 leashRange<=0 → beyondLeash가 항상 false로 들어온다.
            => Assert.AreEqual(State.Chase, NextState(State.Chase, inAggro: true, inAttackRange: false, beyondLeash: false));

        [Test] public void Dead_IsTerminal()
            => Assert.AreEqual(State.Dead, NextState(State.Dead, inAggro: true, inAttackRange: true, beyondLeash: false));
    }
}
