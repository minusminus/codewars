using CodeWars.Solutions2.BlaineIsAPain;

namespace CodeWars.Solutions2.Tests.BlaineIsAPain;

[TestFixture]
internal class TrackLoaderTests
{
    public void Load_KataExample__LoadsCorrectly()
    {
        const string TrackDescription = @"
                                /------------\
/-------------\                /             |
|             |               /              S
|             |              /               |
|        /----+--------------+------\        |
\       /     |              |      |        |      
 \      |     \              |      |        |
 |      |      \-------------+------+--------+---\            
 |      |                    |      |        |   |
 \------+------S-------------+------/        /   |
        |                    |              /    |
        \--------------------+-------------/     |
                             |                   |
/-------------\              |                   |        
|             |              |             /-----+----\      
|             |              |             |     |     \    
\-------------+--------------+-----S-------+-----/      \   
              |              |             |             \
              |              |             |             |
              |              \-------------+-------------/
              |                            |
              \----------------------------/
";

        TrackLoader.Load(TrackDescription);
    }
}
