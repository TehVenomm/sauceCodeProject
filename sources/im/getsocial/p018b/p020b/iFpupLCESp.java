package im.getsocial.p018b.p020b;

import android.support.v4.media.session.PlaybackStateCompat;

/* renamed from: im.getsocial.b.b.iFpupLCESp */
final class iFpupLCESp {
    /* renamed from: a */
    static QCXFOjcJkE f1001a;
    /* renamed from: b */
    static long f1002b;

    private iFpupLCESp() {
    }

    /* renamed from: a */
    static QCXFOjcJkE m769a() {
        synchronized (iFpupLCESp.class) {
            try {
                if (f1001a != null) {
                    QCXFOjcJkE qCXFOjcJkE = f1001a;
                    f1001a = qCXFOjcJkE.f988f;
                    QCXFOjcJkE qCXFOjcJkE2 = null;
                    qCXFOjcJkE.f988f = null;
                    f1002b -= PlaybackStateCompat.ACTION_PLAY_FROM_URI;
                    return qCXFOjcJkE;
                }
            } finally {
                while (true) {
                    break;
                }
                qCXFOjcJkE2 = iFpupLCESp.class;
            }
        }
        return new QCXFOjcJkE();
    }

    /* renamed from: a */
    static void m770a(QCXFOjcJkE qCXFOjcJkE) {
        if (qCXFOjcJkE.f988f != null || qCXFOjcJkE.f989g != null) {
            throw new IllegalArgumentException();
        } else if (!qCXFOjcJkE.f986d) {
            synchronized (iFpupLCESp.class) {
                try {
                    Object obj = ((f1002b + PlaybackStateCompat.ACTION_PLAY_FROM_URI) > PlaybackStateCompat.ACTION_PREPARE_FROM_SEARCH ? 1 : ((f1002b + PlaybackStateCompat.ACTION_PLAY_FROM_URI) == PlaybackStateCompat.ACTION_PREPARE_FROM_SEARCH ? 0 : -1));
                    if (obj > null) {
                        return;
                    }
                    f1002b += PlaybackStateCompat.ACTION_PLAY_FROM_URI;
                    qCXFOjcJkE.f988f = f1001a;
                    qCXFOjcJkE.f985c = 0;
                    qCXFOjcJkE.f984b = 0;
                    f1001a = qCXFOjcJkE;
                } finally {
                    Class cls = iFpupLCESp.class;
                }
            }
        }
    }
}
