package com.facebook.login;

import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.os.Bundle;
import android.os.Parcel;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.ServerProtocol;
import com.facebook.login.LoginClient.Request;
import com.facebook.login.LoginClient.Result;

abstract class NativeAppLoginMethodHandler extends LoginMethodHandler {
    NativeAppLoginMethodHandler(Parcel parcel) {
        super(parcel);
    }

    NativeAppLoginMethodHandler(LoginClient loginClient) {
        super(loginClient);
    }

    private String getError(Bundle bundle) {
        String string = bundle.getString("error");
        return string == null ? bundle.getString(NativeProtocol.BRIDGE_ARG_ERROR_TYPE) : string;
    }

    private String getErrorMessage(Bundle bundle) {
        String string = bundle.getString(AnalyticsEvents.PARAMETER_SHARE_ERROR_MESSAGE);
        return string == null ? bundle.getString(NativeProtocol.BRIDGE_ARG_ERROR_DESCRIPTION) : string;
    }

    private Result handleResultCancel(Request request, Intent intent) {
        Bundle extras = intent.getExtras();
        String error = getError(extras);
        String str = extras.get(NativeProtocol.BRIDGE_ARG_ERROR_CODE) != null ? extras.get(NativeProtocol.BRIDGE_ARG_ERROR_CODE).toString() : null;
        return ServerProtocol.errorConnectionFailure.equals(str) ? Result.createErrorResult(request, error, getErrorMessage(extras), str) : Result.createCancelResult(request, error);
    }

    /* JADX WARNING: type inference failed for: r1v0, types: [com.facebook.login.LoginClient$Result, java.lang.String] */
    /* JADX WARNING: type inference failed for: r0v2 */
    /* JADX WARNING: type inference failed for: r0v3, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r0v10, types: [java.lang.String] */
    /* JADX WARNING: type inference failed for: r0v11 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r1v0, types: [com.facebook.login.LoginClient$Result, java.lang.String]
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY]]
      uses: [?[OBJECT, ARRAY], com.facebook.login.LoginClient$Result, java.lang.String]
      mth insns count: 41
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.core.ProcessClass.lambda$processDependencies$0(ProcessClass.java:49)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:49)
    	at jadx.core.ProcessClass.process(ProcessClass.java:35)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private com.facebook.login.LoginClient.Result handleResultOk(com.facebook.login.LoginClient.Request r8, android.content.Intent r9) {
        /*
            r7 = this;
            r1 = 0
            android.os.Bundle r2 = r9.getExtras()
            java.lang.String r3 = r7.getError(r2)
            java.lang.String r0 = "error_code"
            java.lang.Object r0 = r2.get(r0)
            if (r0 == 0) goto L_0x0047
            java.lang.String r0 = "error_code"
            java.lang.Object r0 = r2.get(r0)
            java.lang.String r0 = r0.toString()
        L_0x001b:
            java.lang.String r4 = r7.getErrorMessage(r2)
            java.lang.String r5 = "e2e"
            java.lang.String r5 = r2.getString(r5)
            boolean r6 = com.facebook.internal.Utility.isNullOrEmpty(r5)
            if (r6 != 0) goto L_0x002e
            r7.logWebLoginCompleted(r5)
        L_0x002e:
            if (r3 != 0) goto L_0x0053
            if (r0 != 0) goto L_0x0053
            if (r4 != 0) goto L_0x0053
            java.util.Set r0 = r8.getPermissions()     // Catch:{ FacebookException -> 0x0049 }
            com.facebook.AccessTokenSource r3 = com.facebook.AccessTokenSource.FACEBOOK_APPLICATION_WEB     // Catch:{ FacebookException -> 0x0049 }
            java.lang.String r4 = r8.getApplicationId()     // Catch:{ FacebookException -> 0x0049 }
            com.facebook.AccessToken r0 = createAccessTokenFromWebBundle(r0, r2, r3, r4)     // Catch:{ FacebookException -> 0x0049 }
            com.facebook.login.LoginClient$Result r1 = com.facebook.login.LoginClient.Result.createTokenResult(r8, r0)     // Catch:{ FacebookException -> 0x0049 }
        L_0x0046:
            return r1
        L_0x0047:
            r0 = r1
            goto L_0x001b
        L_0x0049:
            r0 = move-exception
            java.lang.String r0 = r0.getMessage()
            com.facebook.login.LoginClient$Result r1 = com.facebook.login.LoginClient.Result.createErrorResult(r8, r1, r0)
            goto L_0x0046
        L_0x0053:
            java.util.Collection<java.lang.String> r2 = com.facebook.internal.ServerProtocol.errorsProxyAuthDisabled
            boolean r2 = r2.contains(r3)
            if (r2 != 0) goto L_0x0046
            java.util.Collection<java.lang.String> r2 = com.facebook.internal.ServerProtocol.errorsUserCanceled
            boolean r2 = r2.contains(r3)
            if (r2 == 0) goto L_0x0068
            com.facebook.login.LoginClient$Result r1 = com.facebook.login.LoginClient.Result.createCancelResult(r8, r1)
            goto L_0x0046
        L_0x0068:
            com.facebook.login.LoginClient$Result r1 = com.facebook.login.LoginClient.Result.createErrorResult(r8, r3, r4, r0)
            goto L_0x0046
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.login.NativeAppLoginMethodHandler.handleResultOk(com.facebook.login.LoginClient$Request, android.content.Intent):com.facebook.login.LoginClient$Result");
    }

    /* access modifiers changed from: 0000 */
    public boolean onActivityResult(int i, int i2, Intent intent) {
        Request pendingRequest = this.loginClient.getPendingRequest();
        Result handleResultOk = intent == null ? Result.createCancelResult(pendingRequest, "Operation canceled") : i2 == 0 ? handleResultCancel(pendingRequest, intent) : i2 != -1 ? Result.createErrorResult(pendingRequest, "Unexpected resultCode from authorization.", null) : handleResultOk(pendingRequest, intent);
        if (handleResultOk != null) {
            this.loginClient.completeAndValidate(handleResultOk);
        } else {
            this.loginClient.tryNextHandler();
        }
        return true;
    }

    /* access modifiers changed from: 0000 */
    public abstract boolean tryAuthorize(Request request);

    /* access modifiers changed from: protected */
    public boolean tryIntent(Intent intent, int i) {
        if (intent == null) {
            return false;
        }
        try {
            this.loginClient.getFragment().startActivityForResult(intent, i);
            return true;
        } catch (ActivityNotFoundException e) {
            return false;
        }
    }
}
