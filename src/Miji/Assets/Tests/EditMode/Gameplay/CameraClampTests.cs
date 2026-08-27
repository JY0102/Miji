using Miji.Gameplay.View;
using NUnit.Framework;
using UnityEngine;

namespace Miji.Gameplay.Tests
{
    /// <summary>룸 클램프의 순수 계산. 카메라 절반 크기(half extents) 기준으로 조인다.</summary>
    public class CameraClampTests
    {
        static readonly Rect Room = new(-10f, -6f, 20f, 12f); // x[-10,10] y[-6,6]
        static readonly Vector2 Half = new(4f, 3f);

        [Test]
        public void InsideRoom_Unchanged()
        {
            var pos = new Vector2(1f, 0f);
            Assert.That(CameraFollower.ClampToRoom(pos, Room, Half), Is.EqualTo(pos));
        }

        [Test]
        public void PastEdge_ClampsSoViewStaysInside()
        {
            var clamped = CameraFollower.ClampToRoom(new Vector2(20f, 0f), Room, Half);
            Assert.That(clamped.x, Is.EqualTo(Room.xMax - Half.x)); // 6
        }

        [Test]
        public void PastBottom_ClampsUp()
        {
            var clamped = CameraFollower.ClampToRoom(new Vector2(0f, -20f), Room, Half);
            Assert.That(clamped.y, Is.EqualTo(Room.yMin + Half.y)); // -3
        }

        [Test]
        public void RoomSmallerThanView_CentersOnRoom()
        {
            var tiny = new Rect(0f, 0f, 4f, 2f); // 화면(8x6)보다 작다
            var clamped = CameraFollower.ClampToRoom(new Vector2(99f, 99f), tiny, Half);
            Assert.That(clamped, Is.EqualTo(tiny.center));
        }
    }
}
