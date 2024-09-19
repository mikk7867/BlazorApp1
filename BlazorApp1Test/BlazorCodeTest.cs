using BlazorApp1.Components.Pages;
using Bunit.TestDoubles;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// test ikke logget ind, logget ind, og logget ind som admin

namespace BlazorApp1Test;

public class BlazorCodeTest
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
        var authStatus = cut.Instance.AuthStatus;
        var adminStatus = cut.Instance.AdminStatus;

        // Assert
        Assert.False(authStatus);
        Assert.False(adminStatus);
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
        var authStatus = cut.Instance.AuthStatus;
        var adminStatus = cut.Instance.AdminStatus;

        // Assert
        Assert.True(authStatus);
        Assert.False(adminStatus);
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
        var authStatus = cut.Instance.AuthStatus;
        var adminStatus = cut.Instance.AdminStatus;

        // Assert
        Assert.True(authStatus);
        Assert.True(adminStatus);
    }
}