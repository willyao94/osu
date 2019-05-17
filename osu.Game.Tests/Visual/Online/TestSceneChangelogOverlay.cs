﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using NUnit.Framework;
using osu.Game.Online.API.Requests.Responses;
using osu.Game.Overlays;
using osu.Game.Overlays.Changelog;
using osu.Game.Overlays.Changelog.Header;

namespace osu.Game.Tests.Visual.Online
{
    [TestFixture]
    public class TestSceneChangelogOverlay : OsuTestScene
    {
        private ChangelogOverlay changelog;

        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(BadgeDisplay),
            typeof(StreamBadge),
            typeof(ChangelogHeader),
            typeof(ChangelogContent),
            typeof(ChangelogListing),
            typeof(ChangelogBuild),
            typeof(ChangelogContentGroup),
            typeof(Breadcrumb),
            typeof(BreadcrumbListing),
            typeof(BreadcrumbRelease),
        };

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(changelog = new ChangelogOverlay());
            AddStep(@"Show", changelog.Show);
            AddStep(@"Hide", changelog.Hide);

            AddWaitStep("wait for hide", 3);

            AddStep(@"Show with Lazer 2018.712.0", () =>
            {
                changelog.ShowBuild(new APIChangelogBuild
                {
                    Version = "2018.712.0",
                    UpdateStream = new APIUpdateStream { Name = "lazer" },
                });
                changelog.Show();
            });

            AddWaitStep("wait for show", 3);
            AddStep(@"Hide", changelog.Hide);
            AddWaitStep("wait for hide", 3);

            AddStep(@"Show with listing", () =>
            {
                changelog.ShowListing();
                changelog.Show();
            });
        }
    }
}
