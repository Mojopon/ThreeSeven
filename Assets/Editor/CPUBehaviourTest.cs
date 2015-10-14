using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class CPUBehaviourTest : GridTestFixture
{
    [SetUp]
    public void Initialize() 
    {

    } 

    [Test]
    public void ShouldInputMoveAction()
    {
        IGrid gridMock = Substitute.For<IGrid>();
        ICPUManager cpuManager = new CPUManager(gridMock);

        cpuManager.ChangeCPUMode(CPUMode.Easy);
        cpuManager.OnUpdate();

        gridMock.Received().OnArrowKeyInput(Arg.Any<Direction>());
    }

}
