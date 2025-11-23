using NUnit.Framework;
using SafeVault.Utils;
using SafeVault.Data;

[TestFixture]
public class TestSecurityFixes
{
    [Test]
    public void SQLInjectionBlocked()
    {
        var repo = new UserRepository("Server=localhost;Database=SafeVault;Uid=root;Pwd=;");
        var malicious = "admin'; DROP TABLE Users; --";

        Assert.DoesNotThrow(() =>
        {
            var safe = InputValidator.SanitizeUsername(malicious);
            repo.AddUser(safe, "safe@example.com");
        });
    }

    [Test]
    public void XSSBlocked()
    {
        var malicious = "<script>alert('xss')</script>";
        var sanitized = InputValidator.SanitizeUsername(malicious);
        var safeOutput = InputValidator.EncodeForHtml(sanitized);

        Assert.IsFalse(safeOutput.Contains("<script>"));
        Assert.IsFalse(safeOutput.Contains(">"));
    }
}