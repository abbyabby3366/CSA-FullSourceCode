using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using csa.Library;

namespace csa.Admin.Helpers
{
    public class GeneralHelper
    {
        public static string TranslateError(int errorCode, string errorMsg = "")
        {
            string output = string.Empty;

            switch (errorCode)
            {
                case ((int)ErrorCode.OK):
                    output = Resources.Lang.ErrorSuccess;
                    break;

                case ((int)ErrorCode.FAILED):
                    output = Resources.Lang.ErrorFailed;
                    break;
                case ((int)ErrorCode.DATA_EXISTED):
                    output = Resources.Lang.ErrorDataExisted;
                    break;
                case ((int)ErrorCode.DATA_NOT_FOUND):
                    output = Resources.Lang.ErrorDataNotFound;
                    break;
                case ((int)ErrorCode.INPUT_DATA_MISMATCH):
                    output = Resources.Lang.ErrorInputDataMisMatch;
                    break;
                case ((int)ErrorCode.INVALID_INPUT):
                    output = Resources.Lang.ErrorInvalidInput;
                    break;
                case ((int)ErrorCode.INVALID_INPUT_FORMAT):
                    output = Resources.Lang.ErrorInvalidInputFormat;
                    break;
                case ((int)ErrorCode.REQUIRED_PARAMETER_NOT_SUPPLIED):
                    output = Resources.Lang.ErrorParameterNotSupplied;
                    break;

                case ((int)ErrorCode.INVALID_REQUEST):
                    output = Resources.Lang.ErrorInvalidRequest;
                    break;
                case ((int)ErrorCode.INVALID_DATA):
                    output = Resources.Lang.ErrorInvalidData;
                    break;

                case ((int)ErrorCode.INVALID_HASHING):
                    output = Resources.Lang.ErrorInvalidHashing;
                    break;
                case ((int)ErrorCode.INVALID_TIMESTAMP):
                    output = Resources.Lang.ErrorInvalidTimestamp;
                    break;

                case ((int)ErrorCode.INVALID_ACCESS):
                    output = Resources.Lang.ErrorInvalidAccess;
                    break;
                case ((int)ErrorCode.INACTIVE_ACCOUNT):
                    output = Resources.Lang.ErrorAccountInactive;
                    break;
                case ((int)ErrorCode.INVALID_CREDENTIAL):
                    output = Resources.Lang.ErrorInvalidCredential;
                    break;
                case ((int)ErrorCode.INVALID_CAPTCHA):
                    output = Resources.Lang.ErrorInvalidCaptcha;
                    break;

                case ((int)ErrorCode.CONNECTION_FAILED):
                    output = Resources.Lang.ErrorConnectionFailed;
                    break;
                case ((int)ErrorCode.NOT_RESPONDING):
                    output = Resources.Lang.ErrorNotResponding;
                    break;
                case ((int)ErrorCode.SESSION_TIMEOUT):
                    output = Resources.Lang.ErrorSessionTimeout;
                    break;
                case ((int)ErrorCode.SERVER_MAINTENANACE):
                    output = Resources.Lang.ErrorServerMaintnance;
                    break;

                case ((int)ErrorCode.UNKNOWN_ERROR):
                    output = Resources.Lang.ErrorUnknown;
                    break;

                //custom error

                case ((int)ErrorCode.CUSTOM_ERROR):
                    output = errorMsg;
                    break;

                default:
                    output = $"({errorCode}) {errorMsg}";
                    break;
            }

            return output;
        }
    }
}