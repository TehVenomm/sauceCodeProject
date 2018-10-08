package im.getsocial.sdk.internal.p033c.p060k.p062b;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p070f.p071a.KkSvQPDhNi;
import im.getsocial.sdk.internal.p070f.p071a.sqEuGXwfLT;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.c.k.b.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static GetSocialException m1355a(KkSvQPDhNi kkSvQPDhNi) {
        List<sqEuGXwfLT> list = kkSvQPDhNi.f1625a;
        List arrayList = new ArrayList();
        for (sqEuGXwfLT sqeugxwflt : list) {
            int i;
            switch (sqeugxwflt.f1838a) {
                case EMUnauthorized:
                    i = ErrorCode.ILLEGAL_STATE;
                    break;
                case EMResourceAlreadyExists:
                    i = 101;
                    break;
                case InvalidSession:
                    i = ErrorCode.SDK_NOT_INITIALIZED;
                    break;
                case IdentityAlreadyExists:
                    i = 101;
                    break;
                case SIErrProcessAppOpenNoMatch:
                    i = 102;
                    break;
                case PlatformNotEnabled:
                    i = ErrorCode.PLATFORM_DISABLED;
                    break;
                case AppSignatureMismatch:
                    i = ErrorCode.APP_SIGNATURE_MISMATCH;
                    break;
                case MissingFields:
                case EMFieldCannotBeNull:
                case InvalidUserOrPassword:
                case EMFieldHasInvalidLength:
                case EMInvalidProperties:
                case EMInvalidEnumGiven:
                case EMFieldMismatch:
                case EMNotFound:
                case AFOlderXorNewer:
                case AFInvalidNewer:
                case AFInvalidOlder:
                case AFInvalidImageUrl:
                case AFInvalidLanguage:
                    i = ErrorCode.ILLEGAL_ARGUMENT;
                    break;
                case AFActivityNotFound:
                case AFAuthorActivityNotFound:
                    i = ErrorCode.NOT_FOUND;
                    break;
                case AFBanForbidden:
                    i = ErrorCode.USER_IS_BANNED;
                    break;
                default:
                    i = 0;
                    break;
            }
            arrayList.add(new im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg.jjbQypPegg(i, sqeugxwflt.f1839b, sqeugxwflt.f1840c));
        }
        return new im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg(((im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg.jjbQypPegg) arrayList.get(0)).m1134b(), ((im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg.jjbQypPegg) arrayList.get(0)).m1133a(), arrayList);
    }
}
