﻿using System;
using System.Net.Http;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.AppId;
using VaultSharp.Backends.Authentication.Models.Certificate;
using VaultSharp.Backends.Authentication.Models.Custom;
using VaultSharp.Backends.Authentication.Models.GitHub;
using VaultSharp.Backends.Authentication.Models.LDAP;
using VaultSharp.Backends.Authentication.Models.Token;
using VaultSharp.Backends.Authentication.Models.UsernamePassword;
using VaultSharp.Backends.Authentication.Providers.AppId;
using VaultSharp.Backends.Authentication.Providers.Certificate;
using VaultSharp.Backends.Authentication.Providers.Custom;
using VaultSharp.Backends.Authentication.Providers.GitHub;
using VaultSharp.Backends.Authentication.Providers.LDAP;
using VaultSharp.Backends.Authentication.Providers.Token;
using VaultSharp.Backends.Authentication.Providers.UsernamePassword;
using VaultSharp.DataAccess;

namespace VaultSharp.Backends.Authentication.Providers
{
    internal static class AuthenticationProviderFactory
    {
        public static IAuthenticationProvider CreateAuthenticationProvider(IAuthenticationInfo authenticationInfo, Uri baseAddress, TimeSpan? serviceTimeout = null)
        {
            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.AppId)
            {
                return new AppIdAuthenticationProvider(authenticationInfo as AppIdAuthenticationInfo, new HttpDataAccessManager(baseAddress, serviceTimeout: serviceTimeout));
            }

            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.GitHub)
            {
                return new GitHubAuthenticationProvider(authenticationInfo as GitHubAuthenticationInfo, new HttpDataAccessManager(baseAddress, serviceTimeout: serviceTimeout));
            }

            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.LDAP)
            {
                return new LDAPAuthenticationProvider(authenticationInfo as LDAPAuthenticationInfo, new HttpDataAccessManager(baseAddress, serviceTimeout: serviceTimeout));
            }

            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.Certificate)
            {
                var certificationInfo = authenticationInfo as CertificateAuthenticationInfo;

                var handler = new WebRequestHandler();
                handler.ClientCertificates.Add(certificationInfo.ClientCertificate);

                return new CertificateAuthenticationProvider(certificationInfo, new HttpDataAccessManager(baseAddress, handler, serviceTimeout: serviceTimeout));
            }

            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.Token)
            {
                return new TokenAuthenticationProvider(authenticationInfo as TokenAuthenticationInfo);
            }

            if (authenticationInfo.AuthenticationBackendType == AuthenticationBackendType.UsernamePassword)
            {
                return new UsernamePasswordAuthenticationProvider(authenticationInfo as UsernamePasswordAuthenticationInfo, new HttpDataAccessManager(baseAddress, serviceTimeout: serviceTimeout));
            }

            var customAuthenticationInfo = authenticationInfo as CustomAuthenticationInfo;

            if (customAuthenticationInfo != null)
            {
                return new CustomAuthenticationProvider(customAuthenticationInfo);
            }

            throw new NotSupportedException("The requested authentication backend type is not supported: " + authenticationInfo.AuthenticationBackendType);
        }
    }
}