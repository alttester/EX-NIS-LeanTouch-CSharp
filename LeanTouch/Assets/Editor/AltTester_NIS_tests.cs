using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;
using System.Threading;

public class AltTester_NIS_tests
{
    public AltDriver altDriver;
    string testScene = "Assets/Plugins/CW/LeanTouch/Examples/05    Direction 4.unity";
    string testScene2 = "Assets/Plugins/CW/LeanTouch/Examples/18    Destroy.unity";
    string testScene3 = "Assets/Plugins/CW/LeanTouch/Examples/15 Tap To Select.unity";
    string testScene4 = "Assets/Plugins/CW/LeanTouch/Examples/01 Finger Down.unity";
    string testScene5 = "Assets/Plugins/CW/LeanTouch/Examples/07 Finger Tap.unity";
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }


    [Test]
    public void TestTapAtCoordinates()
    {
        altDriver.LoadScene(testScene4);
        Thread.Sleep(1000);
        var capsule = altDriver.FindObject(By.NAME,"Red Sphere");
        altDriver.Tap(new AltVector2(100,100));
        Thread.Sleep(500);
        var newCapsule = altDriver.FindObject(By.NAME,"Red Sphere");
        var positionAfterTap = newCapsule.getScreenPosition();
        Assert.AreEqual(positionAfterTap.x,100f);
        Assert.AreEqual(positionAfterTap.y,100f);
    }

    [Test]
    public void TestSwipeUp()
    {
        altDriver.LoadScene(testScene);
        var positions = new[]
        {
            new AltVector2(100,100),
            new AltVector2(100,120),
            new AltVector2(100,150)
        };

        altDriver.MultipointSwipe(positions, 0.1f);
        altDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=N]");
    }
    [Test]
    public void TestSwipeDown()
    {
        altDriver.LoadScene(testScene);
        var positions = new[]
        {
            new AltVector2(200,200),
            new AltVector2(200,180),
            new AltVector2(200,150),
            new AltVector2(200,120),
            new AltVector2(200,100)

        };
        altDriver.MultipointSwipe(positions, 0.1f);
        altDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=S]");
    }
     
    [Test]
    public void TestSwipeRight()
    {
        altDriver.LoadScene(testScene);
        altDriver.Swipe(new AltVector2(100,100),new AltVector2(150,100),0.1f);
        altDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=E]");
    }
     
    [Test]
    public void TestSwipeLeft()
    {
        altDriver.LoadScene(testScene);
        altDriver.Swipe(new AltVector2(100,100),new AltVector2(50,100),0.1f);
        altDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=W]");
    }

    [Test]
    public void TestBeginEndTouch()
    {
        altDriver.LoadScene(testScene4);
        Thread.Sleep(1000);
        var capsule = altDriver.FindObject(By.NAME,"Red Sphere");
        var position =  capsule.getScreenPosition();
        var fingerId = altDriver.BeginTouch(new AltVector2(100,100));
        altDriver.EndTouch(fingerId);
        Thread.Sleep(500);
        var newCapsule = altDriver.FindObject(By.NAME,"Red Sphere");
        var positionAfterTap = newCapsule.getScreenPosition();
        Assert.AreNotEqual(position.x,positionAfterTap.x);
        Assert.AreNotEqual(position.y,positionAfterTap.y);
    }


}