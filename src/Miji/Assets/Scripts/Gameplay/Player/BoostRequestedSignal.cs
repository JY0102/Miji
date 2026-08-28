using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// A가 F2 받침을 발동했다. B(동행자)가 구독해 A 밑으로 파고든다.
    ///
    /// 「게임이 B를 항상 제자리에 둔다」(이원 무브셋 3절) — 이것은 플레이어 명령이 아니라
    /// 물리 사건 신호다. B는 어디에 있든 이 신호에 반응해 스냅한다(「멀어서 못 했다」 금지).
    /// </summary>
    public readonly struct BoostRequestedSignal
    {
        public readonly Vector2 Position;
        public readonly int Facing;

        public BoostRequestedSignal(Vector2 position, int facing)
        {
            Position = position;
            Facing = facing;
        }
    }
}
