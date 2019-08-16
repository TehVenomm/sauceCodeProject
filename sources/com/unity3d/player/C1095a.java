package com.unity3d.player;

import android.os.Build.VERSION;
import java.net.InetAddress;
import java.net.Socket;
import javax.net.ssl.HandshakeCompletedEvent;
import javax.net.ssl.HandshakeCompletedListener;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLPeerUnverifiedException;
import javax.net.ssl.SSLSession;
import javax.net.ssl.SSLSocket;
import javax.net.ssl.SSLSocketFactory;

/* renamed from: com.unity3d.player.a */
public final class C1095a extends SSLSocketFactory {

    /* renamed from: c */
    private static volatile SSLSocketFactory f551c;

    /* renamed from: d */
    private static final Object[] f552d = new Object[0];

    /* renamed from: e */
    private static final boolean f553e;

    /* renamed from: a */
    private final SSLSocketFactory f554a;

    /* renamed from: b */
    private final C1096a f555b = new C1096a();

    /* renamed from: com.unity3d.player.a$a */
    final class C1096a implements HandshakeCompletedListener {
        C1096a() {
        }

        public final void handshakeCompleted(HandshakeCompletedEvent handshakeCompletedEvent) {
            SSLSession session = handshakeCompletedEvent.getSession();
            session.getCipherSuite();
            session.getProtocol();
            try {
                session.getPeerPrincipal().getName();
            } catch (SSLPeerUnverifiedException e) {
            }
        }
    }

    static {
        boolean z = false;
        if (VERSION.SDK_INT >= 16 && VERSION.SDK_INT < 20) {
            z = true;
        }
        f553e = z;
    }

    private C1095a() {
        SSLContext instance = SSLContext.getInstance("TLS");
        instance.init(null, null, null);
        this.f554a = instance.getSocketFactory();
    }

    /* renamed from: a */
    private static Socket m510a(Socket socket) {
        if (socket != null && (socket instanceof SSLSocket) && f553e) {
            ((SSLSocket) socket).setEnabledProtocols(((SSLSocket) socket).getSupportedProtocols());
        }
        return socket;
    }

    /* renamed from: a */
    public static SSLSocketFactory m511a() {
        synchronized (f552d) {
            if (f551c != null) {
                SSLSocketFactory sSLSocketFactory = f551c;
                return sSLSocketFactory;
            }
            try {
                C1095a aVar = new C1095a();
                f551c = aVar;
                return aVar;
            } catch (Exception e) {
                C1104e.Log(5, "CustomSSLSocketFactory: Failed to create SSLSocketFactory (" + e.getMessage() + ")");
                return null;
            }
        }
    }

    public final Socket createSocket() {
        return m510a(this.f554a.createSocket());
    }

    public final Socket createSocket(String str, int i) {
        return m510a(this.f554a.createSocket(str, i));
    }

    public final Socket createSocket(String str, int i, InetAddress inetAddress, int i2) {
        return m510a(this.f554a.createSocket(str, i, inetAddress, i2));
    }

    public final Socket createSocket(InetAddress inetAddress, int i) {
        return m510a(this.f554a.createSocket(inetAddress, i));
    }

    public final Socket createSocket(InetAddress inetAddress, int i, InetAddress inetAddress2, int i2) {
        return m510a(this.f554a.createSocket(inetAddress, i, inetAddress2, i2));
    }

    public final Socket createSocket(Socket socket, String str, int i, boolean z) {
        return m510a(this.f554a.createSocket(socket, str, i, z));
    }

    public final String[] getDefaultCipherSuites() {
        return this.f554a.getDefaultCipherSuites();
    }

    public final String[] getSupportedCipherSuites() {
        return this.f554a.getSupportedCipherSuites();
    }
}
