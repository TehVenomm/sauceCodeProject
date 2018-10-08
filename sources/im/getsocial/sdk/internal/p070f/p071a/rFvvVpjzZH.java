package im.getsocial.sdk.internal.p070f.p071a;

import android.support.v4.view.PointerIconCompat;
import com.google.android.gms.games.GamesStatusCodes;
import com.google.android.gms.nearby.connection.ConnectionsStatusCodes;

/* renamed from: im.getsocial.sdk.internal.f.a.rFvvVpjzZH */
public enum rFvvVpjzZH {
    UnknownError(0),
    MissingFields(1),
    InvalidSession(2),
    IdentityAlreadyExists(3),
    InvalidUserOrPassword(4),
    PasswordsDontMatch(5),
    InvalidToken(6),
    PlatformNotEnabled(7),
    AppSignatureMismatch(8),
    MissingEmailAddress(9),
    EMFieldCannotBeNull(1000),
    EMFieldHasInvalidLength(PointerIconCompat.TYPE_ALIAS),
    EMInvalidProperties(PointerIconCompat.TYPE_GRAB),
    EMInvalidEnumGiven(1030),
    EMInvalidData(1060),
    EMOther(1099),
    EMResourceAlreadyExists(5000),
    EMNotAuthenticated(5040),
    EMFieldMismatch(5050),
    EMUnauthorized(5060),
    EMNotFound(5070),
    EMFieldMustBeUnique(5080),
    EMNeedsSDK6Migration(5090),
    AFOlderXorNewer(GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_NOT_TRUSTED_TESTER),
    AFInvalidNewer(GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_INVALID_MULTIPLAYER_TYPE),
    AFInvalidOlder(GamesStatusCodes.STATUS_MULTIPLAYER_DISABLED),
    AFInvalidLanguage(GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_INVALID_OPERATION),
    AFInvalidUser(6005),
    AFInvalidImageUrl(6006),
    AFNotEnoughPermissions(6007),
    AFActivityNotFound(6008),
    AFAuthorActivityNotFound(6009),
    AFRelatedActivityNotFound(6010),
    AFBanForbidden(6011),
    SGInvalidParam(GamesStatusCodes.STATUS_REAL_TIME_MESSAGE_SEND_FAILED),
    SGInvalidData(GamesStatusCodes.STATUS_INVALID_REAL_TIME_ROOM_ID),
    SGInvalidJson(GamesStatusCodes.STATUS_PARTICIPANT_NOT_CONNECTED),
    SGRequiredParam(GamesStatusCodes.STATUS_REAL_TIME_ROOM_NOT_JOINED),
    SGUnathorized(GamesStatusCodes.STATUS_REAL_TIME_INACTIVE_ROOM),
    SGNotFound(7006),
    SGGraphLoop(GamesStatusCodes.STATUS_OPERATION_IN_FLIGHT),
    SGMethodNotAllowed(7008),
    SGInvalidProvider(7009),
    SGProviderError(7010),
    SIErrBadRequest(8001),
    SIErrUnknownError(8002),
    SIErrInvalidApp(8003),
    SIErrResourceNotFound(ConnectionsStatusCodes.STATUS_CONNECTION_REJECTED),
    SIErrCampaignAlreadyExists(ConnectionsStatusCodes.API_CONNECTION_FAILED_ALREADY_IN_USE),
    SIErrMissingCampaignID(8051),
    SIErrMarketingTokenExists(8052),
    SIErrMissingMarketingLinkFields(8053),
    SIErrInvalidCampaignID(8054),
    SIErrProcessAppOpenNoMatch(8055),
    SIErrInsufficientPermissions(8056),
    SIErrInvalidToken(8057),
    IrisInvalidPlatfromCreds(GamesStatusCodes.STATUS_VIDEO_UNSUPPORTED),
    IrisIOSCertProblem(GamesStatusCodes.STATUS_VIDEO_PERMISSION_ERROR),
    IrisRegWrongPlatform(GamesStatusCodes.STATUS_VIDEO_STORAGE_ERROR),
    IrisNotValidInput(GamesStatusCodes.STATUS_VIDEO_UNEXPECTED_CAPTURE_ERROR),
    IrisIOSCertValidationProblem(9005),
    IrisIOSSandboxCertForProd(GamesStatusCodes.STATUS_VIDEO_ALREADY_CAPTURING),
    IrisIOSCertBadBundleID(9007),
    IrisTNDuplicateName(9011),
    IrisTNPayloadTooBig(9012),
    IrisTNInProgress(9013),
    TalosRequestError(10001);
    
    public final int value;

    private rFvvVpjzZH(int i) {
        this.value = i;
    }

    public static rFvvVpjzZH findByValue(int i) {
        switch (i) {
            case 0:
                return UnknownError;
            case 1:
                return MissingFields;
            case 2:
                return InvalidSession;
            case 3:
                return IdentityAlreadyExists;
            case 4:
                return InvalidUserOrPassword;
            case 5:
                return PasswordsDontMatch;
            case 6:
                return InvalidToken;
            case 7:
                return PlatformNotEnabled;
            case 8:
                return AppSignatureMismatch;
            case 9:
                return MissingEmailAddress;
            case 1000:
                return EMFieldCannotBeNull;
            case PointerIconCompat.TYPE_ALIAS /*1010*/:
                return EMFieldHasInvalidLength;
            case PointerIconCompat.TYPE_GRAB /*1020*/:
                return EMInvalidProperties;
            case 1030:
                return EMInvalidEnumGiven;
            case 1060:
                return EMInvalidData;
            case 1099:
                return EMOther;
            case 5000:
                return EMResourceAlreadyExists;
            case 5040:
                return EMNotAuthenticated;
            case 5050:
                return EMFieldMismatch;
            case 5060:
                return EMUnauthorized;
            case 5070:
                return EMNotFound;
            case 5080:
                return EMFieldMustBeUnique;
            case 5090:
                return EMNeedsSDK6Migration;
            case GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_NOT_TRUSTED_TESTER /*6001*/:
                return AFOlderXorNewer;
            case GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_INVALID_MULTIPLAYER_TYPE /*6002*/:
                return AFInvalidNewer;
            case GamesStatusCodes.STATUS_MULTIPLAYER_DISABLED /*6003*/:
                return AFInvalidOlder;
            case GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_INVALID_OPERATION /*6004*/:
                return AFInvalidLanguage;
            case 6005:
                return AFInvalidUser;
            case 6006:
                return AFInvalidImageUrl;
            case 6007:
                return AFNotEnoughPermissions;
            case 6008:
                return AFActivityNotFound;
            case 6009:
                return AFAuthorActivityNotFound;
            case 6010:
                return AFRelatedActivityNotFound;
            case 6011:
                return AFBanForbidden;
            case GamesStatusCodes.STATUS_REAL_TIME_MESSAGE_SEND_FAILED /*7001*/:
                return SGInvalidParam;
            case GamesStatusCodes.STATUS_INVALID_REAL_TIME_ROOM_ID /*7002*/:
                return SGInvalidData;
            case GamesStatusCodes.STATUS_PARTICIPANT_NOT_CONNECTED /*7003*/:
                return SGInvalidJson;
            case GamesStatusCodes.STATUS_REAL_TIME_ROOM_NOT_JOINED /*7004*/:
                return SGRequiredParam;
            case GamesStatusCodes.STATUS_REAL_TIME_INACTIVE_ROOM /*7005*/:
                return SGUnathorized;
            case 7006:
                return SGNotFound;
            case GamesStatusCodes.STATUS_OPERATION_IN_FLIGHT /*7007*/:
                return SGGraphLoop;
            case 7008:
                return SGMethodNotAllowed;
            case 7009:
                return SGInvalidProvider;
            case 7010:
                return SGProviderError;
            case 8001:
                return SIErrBadRequest;
            case 8002:
                return SIErrUnknownError;
            case 8003:
                return SIErrInvalidApp;
            case ConnectionsStatusCodes.STATUS_CONNECTION_REJECTED /*8004*/:
                return SIErrResourceNotFound;
            case ConnectionsStatusCodes.API_CONNECTION_FAILED_ALREADY_IN_USE /*8050*/:
                return SIErrCampaignAlreadyExists;
            case 8051:
                return SIErrMissingCampaignID;
            case 8052:
                return SIErrMarketingTokenExists;
            case 8053:
                return SIErrMissingMarketingLinkFields;
            case 8054:
                return SIErrInvalidCampaignID;
            case 8055:
                return SIErrProcessAppOpenNoMatch;
            case 8056:
                return SIErrInsufficientPermissions;
            case 8057:
                return SIErrInvalidToken;
            case GamesStatusCodes.STATUS_VIDEO_UNSUPPORTED /*9001*/:
                return IrisInvalidPlatfromCreds;
            case GamesStatusCodes.STATUS_VIDEO_PERMISSION_ERROR /*9002*/:
                return IrisIOSCertProblem;
            case GamesStatusCodes.STATUS_VIDEO_STORAGE_ERROR /*9003*/:
                return IrisRegWrongPlatform;
            case GamesStatusCodes.STATUS_VIDEO_UNEXPECTED_CAPTURE_ERROR /*9004*/:
                return IrisNotValidInput;
            case 9005:
                return IrisIOSCertValidationProblem;
            case GamesStatusCodes.STATUS_VIDEO_ALREADY_CAPTURING /*9006*/:
                return IrisIOSSandboxCertForProd;
            case 9007:
                return IrisIOSCertBadBundleID;
            case 9011:
                return IrisTNDuplicateName;
            case 9012:
                return IrisTNPayloadTooBig;
            case 9013:
                return IrisTNInProgress;
            case 10001:
                return TalosRequestError;
            default:
                return null;
        }
    }
}
