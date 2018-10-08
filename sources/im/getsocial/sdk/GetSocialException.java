package im.getsocial.sdk;

import im.getsocial.sdk.internal.p030e.jjbQypPegg;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadPoolExecutor;

public class GetSocialException extends RuntimeException {
    /* renamed from: b */
    private static final List<String> f1100b;
    /* renamed from: a */
    private final int f1101a;

    static {
        List arrayList = new ArrayList();
        f1100b = arrayList;
        arrayList.add(jjbQypPegg.class.getPackage().getName());
        f1100b.add(ThreadPoolExecutor.class.getSimpleName());
    }

    public GetSocialException(int i, String str) {
        this(i, str, null);
    }

    public GetSocialException(int i, String str, Throwable th) {
        super(str, th);
        this.f1101a = i;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            if (this.f1101a != ((GetSocialException) obj).f1101a) {
                return false;
            }
        }
        return true;
    }

    public int getErrorCode() {
        return this.f1101a;
    }

    public int hashCode() {
        return this.f1101a;
    }

    public void printStackTrace(PrintWriter printWriter) {
        for (StackTraceElement stackTraceElement : getStackTrace()) {
            Object obj = 1;
            for (String contains : f1100b) {
                obj = stackTraceElement.getClassName().contains(contains) ? null : obj;
            }
            if (obj != null) {
                printWriter.println(stackTraceElement);
            }
        }
    }
}
