﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
        {
            new ApiResource("resource_catalog")
            {
                    Scopes = { "catalog_fullpermission" }
            },
            new ApiResource("resource_photo_stock")
            {
                    Scopes = { "photo_stock_fullpermission" }
            },
            new ApiResource("resource_basket")
            {
                    Scopes = { "basket_fullpermission" }
            }, 
            new ApiResource("resource_discount")
            {
                    Scopes = { "discount_fullpermission" }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        // kimler token alıcak onu belirliyoz
        public static IEnumerable<IdentityResource> IdentityResources() 
        { 
            // kullanıcı bilgilerine hangi clientlar erişebilir dolduruyoruz.
            return new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(), // sub claim dolu olmalı
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name="roles",
                    DisplayName="Roles",
                    Description = "Kullanıcı Rolleri", 
                    UserClaims = new[]{"role"} },
            };
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new ApiScope[] 
            {
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope("basket_fullpermission","Basket API için full erişim"),
                new ApiScope("discount_fullpermission","Dsicount API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new Client[] 
            {
                new Client
                {
                    ClientId = "WebMvcClient",
                    ClientName = "Asp.Net Core MVC",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = 
                    { 
                        "catalog_fullpermission", 
                        "photo_stock_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName 
                    }
                },
                new Client
                {
                    ClientId = "WebMvcClientForUser",
                    ClientName = "Asp.Net Core MVC",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        "basket_fullpermission",
                        "discount_fullpermission",
                        IdentityServerConstants.StandardScopes.Email, 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName, // Identitye istek atmak için gerekli
                        IdentityServerConstants.StandardScopes.OfflineAccess,// refresh token ile yeni bir token alınabilmeyi sağlıyor
                        "roles"
                    },
                    AccessTokenLifetime = 1*60*60, // 1 saat
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
        }
    }
}