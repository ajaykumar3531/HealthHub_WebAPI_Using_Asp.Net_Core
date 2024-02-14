using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.StatusCodes
{
    public static class StatusCodes
    {
        //
        // Summary:
        //     HTTP status code 100.
        public const int Status100Continue = 100;
        //
        // Summary:
        //     HTTP status code 413.
        public const int Status413RequestEntityTooLarge = 413;
        //
        // Summary:
        //     HTTP status code 413.
        public const int Status413PayloadTooLarge = 413;
        //
        // Summary:
        //     HTTP status code 414.
        public const int Status414RequestUriTooLong = 414;
        //
        // Summary:
        //     HTTP status code 414.
        public const int Status414UriTooLong = 414;
        //
        // Summary:
        //     HTTP status code 415.
        public const int Status415UnsupportedMediaType = 415;
        //
        // Summary:
        //     HTTP status code 416.
        public const int Status416RequestedRangeNotSatisfiable = 416;
        //
        // Summary:
        //     HTTP status code 416.
        public const int Status416RangeNotSatisfiable = 416;
        //
        // Summary:
        //     HTTP status code 417.
        public const int Status417ExpectationFailed = 417;
        //
        // Summary:
        //     HTTP status code 418.
        public const int Status418ImATeapot = 418;
        //
        // Summary:
        //     HTTP status code 419.
        public const int Status419AuthenticationTimeout = 419;
        //
        // Summary:
        //     HTTP status code 421.
        public const int Status421MisdirectedRequest = 421;
        //
        // Summary:
        //     HTTP status code 422.
        public const int Status422UnprocessableEntity = 422;
        //
        // Summary:
        //     HTTP status code 423.
        public const int Status423Locked = 423;
        //
        // Summary:
        //     HTTP status code 424.
        public const int Status424FailedDependency = 424;
        //
        // Summary:
        //     HTTP status code 426.
        public const int Status426UpgradeRequired = 426;
        //
        // Summary:
        //     HTTP status code 428.
        public const int Status428PreconditionRequired = 428;
        //
        // Summary:
        //     HTTP status code 429.
        public const int Status429TooManyRequests = 429;
        //
        // Summary:
        //     HTTP status code 431.
        public const int Status431RequestHeaderFieldsTooLarge = 431;
        //
        // Summary:
        //     HTTP status code 451.
        public const int Status451UnavailableForLegalReasons = 451;
        //
        // Summary:
        //     HTTP status code 499. This is an unofficial status code originally defined by
        //     Nginx and is commonly used in logs when the client has disconnected.
        public const int Status499ClientClosedRequest = 499;
        //
        // Summary:
        //     HTTP status code 500.
        public const int Status500InternalServerError = 500;
        //
        // Summary:
        //     HTTP status code 501.
        public const int Status501NotImplemented = 501;
        //
        // Summary:
        //     HTTP status code 502.
        public const int Status502BadGateway = 502;
        //
        // Summary:
        //     HTTP status code 503.
        public const int Status503ServiceUnavailable = 503;
        //
        // Summary:
        //     HTTP status code 504.
        public const int Status504GatewayTimeout = 504;
        //
        // Summary:
        //     HTTP status code 505.
        public const int Status505HttpVersionNotsupported = 505;
        //
        // Summary:
        //     HTTP status code 506.
        public const int Status506VariantAlsoNegotiates = 506;
        //
        // Summary:
        //     HTTP status code 507.
        public const int Status507InsufficientStorage = 507;
        //
        // Summary:
        //     HTTP status code 508.
        public const int Status508LoopDetected = 508;
        //
        // Summary:
        //     HTTP status code 412.
        public const int Status412PreconditionFailed = 412;
        //
        // Summary:
        //     HTTP status code 411.
        public const int Status411LengthRequired = 411;
        //
        // Summary:
        //     HTTP status code 410.
        public const int Status410Gone = 410;
        //
        // Summary:
        //     HTTP status code 409.
        public const int Status409Conflict = 409;
        //
        // Summary:
        //     HTTP status code 101.
        public const int Status101SwitchingProtocols = 101;
        //
        // Summary:
        //     HTTP status code 102.
        public const int Status102Processing = 102;
        //
        // Summary:
        //     HTTP status code 200.
        public const int Status200OK = 200;
        //
        // Summary:
        //     HTTP status code 201.
        public const int Status201Created = 201;
        //
        // Summary:
        //     HTTP status code 202.
        public const int Status202Accepted = 202;
        //
        // Summary:
        //     HTTP status code 203.
        public const int Status203NonAuthoritative = 203;
        //
        // Summary:
        //     HTTP status code 204.
        public const int Status204NoContent = 204;
        //
        // Summary:
        //     HTTP status code 205.
        public const int Status205ResetContent = 205;
        //
        // Summary:
        //     HTTP status code 206.
        public const int Status206PartialContent = 206;
        //
        // Summary:
        //     HTTP status code 207.
        public const int Status207MultiStatus = 207;
        //
        // Summary:
        //     HTTP status code 208.
        public const int Status208AlreadyReported = 208;
        //
        // Summary:
        //     HTTP status code 226.
        public const int Status226IMUsed = 226;
        //
        // Summary:
        //     HTTP status code 300.
        public const int Status300MultipleChoices = 300;
        //
        // Summary:
        //     HTTP status code 301.
        public const int Status301MovedPermanently = 301;
        //
        // Summary:
        //     HTTP status code 510.
        public const int Status510NotExtended = 510;
        //
        // Summary:
        //     HTTP status code 302.
        public const int Status302Found = 302;
        //
        // Summary:
        //     HTTP status code 304.
        public const int Status304NotModified = 304;
        //
        // Summary:
        //     HTTP status code 305.
        public const int Status305UseProxy = 305;
        //
        // Summary:
        //     HTTP status code 306.
        public const int Status306SwitchProxy = 306;
        //
        // Summary:
        //     HTTP status code 307.
        public const int Status307TemporaryRedirect = 307;
        //
        // Summary:
        //     HTTP status code 308.
        public const int Status308PermanentRedirect = 308;
        //
        // Summary:
        //     HTTP status code 400.
        public const int Status400BadRequest = 400;
        //
        // Summary:
        //     HTTP status code 401.
        public const int Status401Unauthorized = 401;
        //
        // Summary:
        //     HTTP status code 402.
        public const int Status402PaymentRequired = 402;
        //
        // Summary:
        //     HTTP status code 403.
        public const int Status403Forbidden = 403;
        //
        // Summary:
        //     HTTP status code 404.
        public const int Status404NotFound = 404;
        //
        // Summary:
        //     HTTP status code 405.
        public const int Status405MethodNotAllowed = 405;
        //
        // Summary:
        //     HTTP status code 406.
        public const int Status406NotAcceptable = 406;
        //
        // Summary:
        //     HTTP status code 407.
        public const int Status407ProxyAuthenticationRequired = 407;
        //
        // Summary:
        //     HTTP status code 408.
        public const int Status408RequestTimeout = 408;
        //
        // Summary:
        //     HTTP status code 303.
        public const int Status303SeeOther = 303;
        //
        // Summary:
        //     HTTP status code 511.
        public const int Status511NetworkAuthenticationRequired = 511;
    }
}
