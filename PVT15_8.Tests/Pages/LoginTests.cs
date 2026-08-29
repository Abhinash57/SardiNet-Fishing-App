using Bunit;
using PVT15_8.Mudweb.Pages;
using PVT15_8.Mudweb.Tests.Helpers;

namespace PVT15_8.Mudweb.Tests.Pages;

public class LoginTests : IClassFixture<BunitTestContext>
{
    private readonly BunitTestContext _ctx;

    public LoginTests(BunitTestContext ctx) => _ctx = ctx;

    [Fact]
    public void Login_RendersEmailAndPasswordFields()
    {
        var cut = _ctx.Render<Login>();
        Assert.Contains("E-mail", cut.Markup);
        Assert.Contains("Password", cut.Markup);
    }

    [Fact]
    public void Login_RendersLoginButton()
    {
        var cut = _ctx.Render<Login>();
        Assert.Contains(cut.FindAll("button"), b => b.TextContent.Contains("Logga in"));
    }

    [Fact]
    public void Login_NoErrorAlertOnInitialRender()
    {
        var cut = _ctx.Render<Login>();
        Assert.DoesNotContain("mud-alert", cut.Markup);
    }
}