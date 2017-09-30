// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Framework.Tests.Visual
{
    [TestFixture]
    internal class TestCaseDelayedLoad : TestCase
    {
        private const int panel_count = 2048;

        public TestCaseDelayedLoad()
        {
            FillFlowContainerNoInput flow;
            ScrollContainer scroll;

            Children = new Drawable[]
            {
                scroll = new ScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        flow = new FillFlowContainerNoInput
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        }
                    }
                }
            };

            for (int i = 1; i < panel_count; i++)
                flow.Add(new Container
                {
                    Size = new Vector2(128),
                    Children = new Drawable[]
                    {
                        new DelayedLoadWrapper(new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new TestBox{ RelativeSizeAxes = Axes.Both }
                            }
                        }),
                        new SpriteText { Text = i.ToString() },
                    }
                });

            var childrenWithAvatarsLoaded = flow.Children.Where(c => c.Children.OfType<DelayedLoadWrapper>().First().Children.FirstOrDefault()?.IsLoaded ?? false);

            AddWaitStep(10);
            AddStep("scroll down", () => scroll.ScrollToEnd());
            AddWaitStep(10);
            AddAssert("some loaded", () => childrenWithAvatarsLoaded.Count() > 5);
            AddAssert("not too many loaded", () => childrenWithAvatarsLoaded.Count() < panel_count / 4);
        }

        private class FillFlowContainerNoInput : FillFlowContainer<Container>
        {
            public override bool HandleInput => false;
        }
    }

    public class TestBox : Container
    {
        public TestBox()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new SpriteText
            {
                Colour = Color4.Yellow,
                Text = @"loaded",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }
    }
}