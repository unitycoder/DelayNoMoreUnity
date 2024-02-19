using System.Collections.Generic;
using System.Collections.Immutable;

namespace shared {
    using StoryPointStep = ImmutableArray<StoryPointDialogLine>;
    using StoryPoint = ImmutableArray<ImmutableArray<StoryPointDialogLine>>;

    public partial class Battle {
        public const int LEVEL_NONE = -1;

        public const int LEVEL_SMALL_FOREST = 0;
        public const int LEVEL_FOREST = 1;

        public static StoryPointStep SMALL_FOREST_STORY_POINT_1_STEP_1 = ImmutableArray.Create<StoryPointDialogLine>().AddRange(
                new StoryPointDialogLine {
                        NarratorJoinIndex = 1,
                        NarratorSpeciesId = SPECIES_NONE_CH,
                        Content = "No way, I didn't expect a Goblin here...",
                        DownOrNot = true
                }
            );

        public static StoryPointStep SMALL_FOREST_STORY_POINT_1_STEP_2 = ImmutableArray.Create<StoryPointDialogLine>().AddRange(
                new StoryPointDialogLine {
                    NarratorJoinIndex = MAGIC_JOIN_INDEX_INVALID,
                    NarratorSpeciesId = SPECIES_GOBLIN,
                    Content = "Gwaaaaaaaaaaaaa!!!",
                    DownOrNot = false
                },
                new StoryPointDialogLine {
                    NarratorJoinIndex = 1,
                    NarratorSpeciesId = SPECIES_NONE_CH,
                    Content = "Gross sound as always >_<",
                    DownOrNot = true
                }
            );

        public static StoryPoint SMALL_FOREST_STORY_POINT_1 = ImmutableArray.Create<StoryPointStep>().AddRange(
            SMALL_FOREST_STORY_POINT_1_STEP_1,
            SMALL_FOREST_STORY_POINT_1_STEP_2
            );

        public static ImmutableDictionary<int, StoryPoint> SMALL_FOREST_STORY = ImmutableDictionary.Create<int, StoryPoint>().AddRange(new[]
            {
                new KeyValuePair<int, StoryPoint>(1, SMALL_FOREST_STORY_POINT_1)
            }
        );
    }
}
