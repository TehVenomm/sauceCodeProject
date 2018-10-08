package io.fabric.sdk.android.services.network;

import java.security.GeneralSecurityException;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.util.LinkedList;

final class CertificateChainCleaner {
    private CertificateChainCleaner() {
    }

    public static X509Certificate[] getCleanChain(X509Certificate[] x509CertificateArr, SystemKeyStore systemKeyStore) throws CertificateException {
        int i = 1;
        LinkedList linkedList = new LinkedList();
        int i2 = systemKeyStore.isTrustRoot(x509CertificateArr[0]) ? 1 : 0;
        linkedList.add(x509CertificateArr[0]);
        int i3 = i2;
        i2 = 1;
        while (i2 < x509CertificateArr.length) {
            if (systemKeyStore.isTrustRoot(x509CertificateArr[i2])) {
                i3 = 1;
            }
            if (!isValidLink(x509CertificateArr[i2], x509CertificateArr[i2 - 1])) {
                break;
            }
            linkedList.add(x509CertificateArr[i2]);
            i2++;
        }
        X509Certificate trustRootFor = systemKeyStore.getTrustRootFor(x509CertificateArr[i2 - 1]);
        if (trustRootFor != null) {
            linkedList.add(trustRootFor);
        } else {
            i = i3;
        }
        if (i != 0) {
            return (X509Certificate[]) linkedList.toArray(new X509Certificate[linkedList.size()]);
        }
        throw new CertificateException("Didn't find a trust anchor in chain cleanup!");
    }

    private static boolean isValidLink(X509Certificate x509Certificate, X509Certificate x509Certificate2) {
        if (!x509Certificate.getSubjectX500Principal().equals(x509Certificate2.getIssuerX500Principal())) {
            return false;
        }
        try {
            x509Certificate2.verify(x509Certificate.getPublicKey());
            return true;
        } catch (GeneralSecurityException e) {
            return false;
        }
    }
}
