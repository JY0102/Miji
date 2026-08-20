using Miji.Gameplay.View;
using NUnit.Framework;
using UnityEngine;

namespace Miji.Gameplay.Tests
{
    /// <summary>
    /// TurnView는 순수 계산이라 물리도 코루틴도 필요 없다 — PlayMode가 아니라 여기서 잰다.
    /// (이 어셈블리가 존재하는 이유이기도 하다: Gameplay의 뷰·계산 로직을 EditMode에서 잡는다.)
    /// </summary>
    public class TurnViewTests
    {
        const float Duration = 0.3f;   // 3등분하면 한 칸 0.1

        GameObject go;
        SpriteRenderer sprite;
        Sprite quarter;
        Sprite front;

        [SetUp]
        public void SetUp()
        {
            go = new GameObject("turn-view-test");
            sprite = go.AddComponent<SpriteRenderer>();
            quarter = MakeSprite();
            front = MakeSprite();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(go);
            Object.DestroyImmediate(quarter);
            Object.DestroyImmediate(front);
        }

        static Sprite MakeSprite()
        {
            var tex = new Texture2D(4, 4);
            return Sprite.Create(tex, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f));
        }

        [Test]
        public void 지속시간이_0이면_적용하지_않는다()
        {
            Assert.IsFalse(TurnView.Apply(sprite, 0f, 0f, -1, 1, quarter, front));
        }

        [Test]
        public void 정면_스프라이트가_없으면_적용하지_않는다()
        {
            Assert.IsFalse(TurnView.Apply(sprite, Duration, Duration, -1, 1, quarter, null));
        }

        [Test]
        public void 첫_구간은_출발_방향의_45도다()
        {
            // remaining == duration → elapsed 0 → stage 0
            Assert.IsTrue(TurnView.Apply(sprite, Duration, Duration, -1, 1, quarter, front));
            Assert.AreEqual(quarter, sprite.sprite);
            Assert.IsTrue(sprite.flipX, "출발이 왼쪽(-1)이면 뒤집힌다");
        }

        [Test]
        public void 가운데_구간은_정면이고_뒤집히지_않는다()
        {
            // elapsed 0.15 → stage 1
            Assert.IsTrue(TurnView.Apply(sprite, Duration * 0.5f, Duration, -1, -1, quarter, front));
            Assert.AreEqual(front, sprite.sprite);
            Assert.IsFalse(sprite.flipX, "정면은 대칭이라 방향과 무관하다");
        }

        [Test]
        public void 마지막_구간은_도착_방향의_45도다()
        {
            // elapsed 0.29 → stage 2
            Assert.IsTrue(TurnView.Apply(sprite, Duration * 0.03f, Duration, 1, -1, quarter, front));
            Assert.AreEqual(quarter, sprite.sprite);
            Assert.IsTrue(sprite.flipX, "도착이 왼쪽(-1)이면 뒤집힌다");
        }

        [Test]
        public void _45도가_없으면_정면_한_장으로_버틴다()
        {
            Assert.IsTrue(TurnView.Apply(sprite, Duration, Duration, -1, 1, null, front));
            Assert.AreEqual(front, sprite.sprite);
            Assert.IsFalse(sprite.flipX);
        }
    }
}
