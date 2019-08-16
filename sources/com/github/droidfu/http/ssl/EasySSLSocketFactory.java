package com.github.droidfu.http.ssl;

import java.io.IOException;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSocket;
import javax.net.ssl.TrustManager;
import org.apache.http.conn.ConnectTimeoutException;
import org.apache.http.conn.scheme.LayeredSocketFactory;
import org.apache.http.conn.scheme.SocketFactory;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;

public class EasySSLSocketFactory implements SocketFactory, LayeredSocketFactory {
    private SSLContext sslcontext = null;

    private static SSLContext createEasySSLContext() throws IOException {
        try {
            SSLContext instance = SSLContext.getInstance("TLS");
            instance.init(null, new TrustManager[]{new TrivialTrustManager()}, null);
            return instance;
        } catch (Exception e) {
            throw new IOException(e.getMessage());
        }
    }

    private SSLContext getSSLContext() throws IOException {
        if (this.sslcontext == null) {
            this.sslcontext = createEasySSLContext();
        }
        return this.sslcontext;
    }

    public Socket connectSocket(Socket socket, String str, int i, InetAddress inetAddress, int i2, HttpParams httpParams) throws IOException, UnknownHostException, ConnectTimeoutException {
        int connectionTimeout = HttpConnectionParams.getConnectionTimeout(httpParams);
        int soTimeout = HttpConnectionParams.getSoTimeout(httpParams);
        InetSocketAddress inetSocketAddress = new InetSocketAddress(str, i);
        SSLSocket sSLSocket = (SSLSocket) (socket != null ? socket : createSocket());
        if (inetAddress != null || i2 > 0) {
            if (i2 < 0) {
                i2 = 0;
            }
            sSLSocket.bind(new InetSocketAddress(inetAddress, i2));
        }
        sSLSocket.connect(inetSocketAddress, connectionTimeout);
        sSLSocket.setSoTimeout(soTimeout);
        return sSLSocket;
    }

    public Socket createSocket() throws IOException {
        return getSSLContext().getSocketFactory().createSocket();
    }

    public Socket createSocket(Socket socket, String str, int i, boolean z) throws IOException, UnknownHostException {
        return getSSLContext().getSocketFactory().createSocket();
    }

    public boolean equals(Object obj) {
        return obj != null && obj.getClass().equals(EasySSLSocketFactory.class);
    }

    public int hashCode() {
        return EasySSLSocketFactory.class.hashCode();
    }

    public boolean isSecure(Socket socket) throws IllegalArgumentException {
        return true;
    }
}
