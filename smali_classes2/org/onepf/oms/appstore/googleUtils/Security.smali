.class public Lorg/onepf/oms/appstore/googleUtils/Security;
.super Ljava/lang/Object;
.source "Security.java"


# static fields
.field private static final KEY_FACTORY_ALGORITHM:Ljava/lang/String; = "RSA"

.field private static final SIGNATURE_ALGORITHM:Ljava/lang/String; = "SHA1withRSA"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static generatePublicKey(Ljava/lang/String;)Ljava/security/PublicKey;
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 77
    :try_start_0
    invoke-static {p0}, Lorg/onepf/oms/appstore/googleUtils/Base64;->decode(Ljava/lang/String;)[B

    move-result-object p0

    const-string v0, "RSA"

    .line 78
    invoke-static {v0}, Ljava/security/KeyFactory;->getInstance(Ljava/lang/String;)Ljava/security/KeyFactory;

    move-result-object v0

    .line 79
    new-instance v1, Ljava/security/spec/X509EncodedKeySpec;

    invoke-direct {v1, p0}, Ljava/security/spec/X509EncodedKeySpec;-><init>([B)V

    invoke-virtual {v0, v1}, Ljava/security/KeyFactory;->generatePublic(Ljava/security/spec/KeySpec;)Ljava/security/PublicKey;

    move-result-object p0
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/security/spec/InvalidKeySpecException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/onepf/oms/appstore/googleUtils/Base64DecoderException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-string v0, "Base64 decoding failed."

    .line 86
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 87
    new-instance v0, Ljava/lang/IllegalArgumentException;

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/Throwable;)V

    throw v0

    :catch_1
    move-exception p0

    const-string v0, "Invalid key specification."

    .line 83
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 84
    new-instance v0, Ljava/lang/IllegalArgumentException;

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/Throwable;)V

    throw v0

    :catch_2
    move-exception p0

    .line 81
    new-instance v0, Ljava/lang/RuntimeException;

    invoke-direct {v0, p0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/Throwable;)V

    throw v0
.end method

.method public static verify(Ljava/security/PublicKey;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 2
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    :try_start_0
    const-string v1, "SHA1withRSA"

    .line 103
    invoke-static {v1}, Ljava/security/Signature;->getInstance(Ljava/lang/String;)Ljava/security/Signature;

    move-result-object v1

    .line 104
    invoke-virtual {v1, p0}, Ljava/security/Signature;->initVerify(Ljava/security/PublicKey;)V

    .line 105
    invoke-virtual {p1}, Ljava/lang/String;->getBytes()[B

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/security/Signature;->update([B)V

    .line 106
    invoke-static {p2}, Lorg/onepf/oms/appstore/googleUtils/Base64;->decode(Ljava/lang/String;)[B

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/security/Signature;->verify([B)Z

    move-result p0

    if-nez p0, :cond_0

    const-string p0, "Signature verification failed."

    .line 107
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/security/InvalidKeyException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/security/SignatureException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/onepf/oms/appstore/googleUtils/Base64DecoderException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :cond_0
    const/4 p0, 0x1

    return p0

    :catch_0
    const-string p0, "Base64 decoding failed."

    .line 118
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    goto :goto_0

    :catch_1
    const-string p0, "Signature exception."

    .line 116
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    goto :goto_0

    :catch_2
    const-string p0, "Invalid key specification."

    .line 114
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    goto :goto_0

    :catch_3
    const-string p0, "NoSuchAlgorithmException."

    .line 112
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    :goto_0
    return v0
.end method

.method public static verifyPurchase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 1
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 58
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 64
    :cond_0
    invoke-static {p0}, Lorg/onepf/oms/appstore/googleUtils/Security;->generatePublicKey(Ljava/lang/String;)Ljava/security/PublicKey;

    move-result-object p0

    .line 65
    invoke-static {p0, p1, p2}, Lorg/onepf/oms/appstore/googleUtils/Security;->verify(Ljava/security/PublicKey;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p0

    return p0

    :cond_1
    :goto_0
    const-string p0, "Purchase verification failed: missing data."

    .line 60
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    const/4 p0, 0x0

    return p0
.end method
