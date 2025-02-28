using Microsoft.AspNetCore.Authorization;

public sealed class HasPermissionAttribute(Permission permission)
    : AuthorizeAttribute(policy: permission.ToString()) { }
