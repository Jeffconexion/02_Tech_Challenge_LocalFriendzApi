// --------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.UI.Configuration;
using LocalFriendzApi.UI.Endpoints;
using LocalFriendzApi.UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddServices();
builder.AddDocumentation();
builder.AddLogging();
builder.ExternalServices();
builder.AddFluentValidation();

var app = builder.Build();
app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseLoggingMiddleware();
app.UseHttpsRedirection();

app.Run();
