using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Scope = "member",
        Target = "StkGenCode.Code.Db.#GetSchemaFireBird(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Scope = "member",
        Target = "StkGenCode.Code.Db.#GetSchemaSqlServer(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member",
        Target = "StkGenCode.Code.FileCode.#WriteFile(System.String,System.String)")]
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.