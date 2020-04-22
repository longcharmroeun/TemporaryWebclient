﻿using CloudflareSolverRe.Constants;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace CloudflareSolverRe.Types
{
    public struct SolveResult
    {
        public bool Success;
        public string FailReason;
        public DetectResult DetectResult;
        public string UserAgent;
        public SessionCookies Cookies;
        internal DetectResult? NewDetectResult;
        internal HttpResponseMessage Response;

        public static readonly SolveResult NoProtection = new SolveResult
        {
            Success = true,
            Cookies = new SessionCookies(null)
        };

        public static readonly SolveResult Banned = new SolveResult
        {
            Success = false,
            FailReason = Errors.IpAddressIsBanned,
        };

        public static readonly SolveResult Unknown = new SolveResult
        {
            Success = false,
            FailReason = Errors.UnknownProtectionDetected,
        };

        public SolveResult(bool success, string layer, string failReason, DetectResult detectResult, [Optional]SessionCookies cookies, [Optional]string userAgent, [Optional]HttpResponseMessage response)
        {
            Success = success;
            FailReason = !string.IsNullOrEmpty(failReason) ? $"Cloudflare [{layer}]: {failReason}" : null;
            DetectResult = detectResult;
            Cookies = cookies;
            UserAgent = userAgent;
            NewDetectResult = null;
            Response = response;
        }

        public SolveResult(bool success, string failReason, DetectResult detectResult, [Optional]SessionCookies cookies, [Optional]string userAgent, [Optional]HttpResponseMessage response)
        {
            Success = success;
            FailReason = failReason;
            DetectResult = detectResult;
            Cookies = cookies;
            UserAgent = userAgent;
            NewDetectResult = null;
            Response = response;
        }
    }
}
