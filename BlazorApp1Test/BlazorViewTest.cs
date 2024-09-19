using Bunit;
using BlazorApp1;
using BlazorApp1.Components.Pages;
using Bunit.TestDoubles;

// test ikke logget ind, logget ind, og logget ind som admin

namespace BlazorApp1Test;

public class BlazorViewTest
{
    [Fact]
    public void TestNotLogedIn()
    {
        // Arrange
        using var ctx = new TestContext();
        var auth = ctx.AddTestAuthorization();
        auth.SetNotAuthorized();
        var cut = ctx.RenderComponent<Home>();

        // Act
        var paraElm = cut.Find("p");

        // Assert
        var paraElmText = paraElm.TextContent;
        paraElmText.MarkupMatches("You are NOT logged in!");
    }

    [Fact]
    public void TestLogedIn()
    {
        // Arrange
        using var ctx = new TestContext();
        var auth = ctx.AddTestAuthorization();
        auth.SetAuthorized("test@test.test", AuthorizationState.Authorized);
        var cut = ctx.RenderComponent<Home>();

        // Act
        var paraElm = cut.Find("p");

        // Assert
        var paraElmText = paraElm.TextContent;
        paraElmText.MarkupMatches("You are logged in!");
    }

    [Fact]
    public void TestLogedInAdmin()
    {
        // Arrange
        using var ctx = new TestContext();
        var auth = ctx.AddTestAuthorization();
        auth.SetRoles("Admin");
        auth.SetAuthorized("test@test.test", AuthorizationState.Authorized);
        var cut = ctx.RenderComponent<Home>();

        // Act
        var paraElms = cut.FindAll("p");

        // Assert
        Assert.Contains(paraElms, p => p.TextContent == "You are admin!");
    }
}