.class public Ljp/colopl/util/HTTP;
.super Ljava/lang/Object;
.source "HTTP.java"


# static fields
.field private static CONNECT_TIMEOUT:I = 0x2710

.field private static CONNECT_TIMEOUT_FOR_UPLOAD:I = 0x493e0

.field private static READ_TIMEOUT:I = 0x2710

.field private static READ_TIMEOUT_FOR_UPLOAD:I = 0x493e0

.field private static userAgent:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 83
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static createCookies(Ljp/colopl/config/Config;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljp/colopl/config/Config;",
            ")",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 369
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 370
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getAndroidTokenCookieName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/config/Config;->generateToken()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 371
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/config/Session;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 372
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getAppVersionCookieName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 373
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getOSVersionCookieName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getOSVersion()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 374
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getAppTypeCookieName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/config/Config;->getAppTypeCookieValue()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 375
    sget-boolean p0, Ljp/colopl/config/Config;->debuggable:Z

    if-eqz p0, :cond_0

    .line 376
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    .line 378
    :try_start_0
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "lhmovdkjedd,"

    invoke-virtual {p0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v1, v2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Ljp/colopl/util/Crypto;->encryptMD5(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 379
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "_dyhsiuj="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 381
    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    .line 383
    :goto_0
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "_yomr="

    invoke-virtual {p0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v1, v2}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    return-object v0
.end method

.method public static get(Ljava/lang/String;Ljava/util/List;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 114
    :try_start_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    :try_start_1
    const-string v1, "User-Agent"

    .line 116
    invoke-static {}, Ljp/colopl/util/HTTP;->getUserAgent()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p0, v1, v2}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "apv"

    .line 119
    sget-object v2, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    invoke-virtual {p0, v1, v2}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 121
    move-object v1, p0

    check-cast v1, Ljava/net/HttpURLConnection;

    const-string v2, "GET"

    invoke-virtual {v1, v2}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const/4 v1, 0x0

    .line 122
    invoke-virtual {p0, v1}, Ljava/net/URLConnection;->setUseCaches(Z)V

    .line 123
    sget v1, Ljp/colopl/util/HTTP;->CONNECT_TIMEOUT:I

    invoke-virtual {p0, v1}, Ljava/net/URLConnection;->setConnectTimeout(I)V

    .line 124
    sget v1, Ljp/colopl/util/HTTP;->READ_TIMEOUT:I

    invoke-virtual {p0, v1}, Ljava/net/URLConnection;->setReadTimeout(I)V

    if-eqz p1, :cond_1

    .line 126
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 127
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 128
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ";"

    .line 129
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    :cond_0
    const-string p1, "Cookie"

    .line 131
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, p1, v1}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 133
    :cond_1
    invoke-virtual {p0}, Ljava/net/URLConnection;->connect()V

    .line 135
    new-instance p1, Ljp/colopl/util/DoneHandlerInputStream;

    invoke-virtual {p0}, Ljava/net/URLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v1

    invoke-direct {p1, v1}, Ljp/colopl/util/DoneHandlerInputStream;-><init>(Ljava/io/InputStream;)V

    .line 136
    invoke-static {p1}, Ljp/colopl/util/StringUtil;->convertToString(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2

    .line 137
    :try_start_2
    move-object v0, p0

    check-cast v0, Ljava/net/HttpURLConnection;

    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0

    return-object p1

    :catch_0
    move-object v0, p1

    goto :goto_1

    :catch_1
    move-object p0, v0

    :catch_2
    :goto_1
    if-eqz p0, :cond_2

    .line 141
    check-cast p0, Ljava/net/HttpURLConnection;

    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_2
    return-object v0
.end method

.method private static getUserAgent()Ljava/lang/String;
    .locals 1

    .line 97
    sget-object v0, Ljp/colopl/util/HTTP;->userAgent:Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, "Android"

    .line 98
    sput-object v0, Ljp/colopl/util/HTTP;->userAgent:Ljava/lang/String;

    .line 100
    :cond_0
    sget-object v0, Ljp/colopl/util/HTTP;->userAgent:Ljava/lang/String;

    return-object v0
.end method

.method public static image(Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/net/MalformedURLException;,
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 358
    :try_start_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openStream()Ljava/io/InputStream;

    move-result-object p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 359
    :try_start_1
    invoke-static {p0}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p0, :cond_0

    .line 362
    :try_start_2
    invoke-virtual {p0}, Ljava/io/InputStream;->close()V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0

    :catch_0
    :cond_0
    return-object v0

    :catchall_0
    move-exception v0

    move-object v2, v0

    move-object v0, p0

    move-object p0, v2

    goto :goto_0

    :catchall_1
    move-exception p0

    :goto_0
    if-eqz v0, :cond_1

    :try_start_3
    invoke-virtual {v0}, Ljava/io/InputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1

    :catch_1
    :cond_1
    throw p0
.end method

.method public static post(Ljava/lang/String;Ljava/util/List;Ljava/util/List;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    const-string v0, "https"

    .line 198
    invoke-virtual {p0, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_3

    .line 201
    :try_start_0
    new-instance v0, Ljava/net/URL;

    invoke-direct {v0, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 202
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p0

    check-cast p0, Ljavax/net/ssl/HttpsURLConnection;

    .line 203
    new-instance v0, Ljp/colopl/util/TLSSocketFactory;

    invoke-direct {v0}, Ljp/colopl/util/TLSSocketFactory;-><init>()V

    invoke-virtual {p0, v0}, Ljavax/net/ssl/HttpsURLConnection;->setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V

    const-string v0, "POST"

    .line 204
    invoke-virtual {p0, v0}, Ljavax/net/ssl/HttpsURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const-string v0, "Content-Type"

    const-string v2, "application/x-www-form-urlencoded;; UTF-8"

    .line 205
    invoke-virtual {p0, v0, v2}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "Accept"

    const-string v2, "application/json"

    .line 206
    invoke-virtual {p0, v0, v2}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 207
    invoke-virtual {p0, v1}, Ljavax/net/ssl/HttpsURLConnection;->setDoOutput(Z)V

    .line 208
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const-string v1, "Cookie"

    .line 209
    invoke-virtual {p0, v1, v0}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string p2, "apv"

    .line 211
    sget-object v0, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    invoke-virtual {p0, p2, v0}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 213
    invoke-virtual {p0}, Ljavax/net/ssl/HttpsURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object p2

    .line 214
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/OutputStreamWriter;

    const-string v2, "UTF-8"

    invoke-direct {v1, p2, v2}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;)V

    const-string v1, "UTF-8"

    .line 216
    invoke-static {p1, v1}, Lorg/apache/http/client/utils/URLEncodedUtils;->format(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/io/BufferedWriter;->write(Ljava/lang/String;)V

    .line 217
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->flush()V

    .line 218
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->close()V

    .line 219
    invoke-virtual {p2}, Ljava/io/OutputStream;->close()V

    const-string p1, "Location"

    .line 221
    invoke-virtual {p0, p1}, Ljavax/net/ssl/HttpsURLConnection;->getHeaderField(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 222
    invoke-virtual {p0}, Ljavax/net/ssl/HttpsURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object p2

    .line 223
    new-instance v0, Ljava/io/InputStreamReader;

    invoke-direct {v0, p2}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;)V

    .line 224
    new-instance p2, Ljava/io/BufferedReader;

    invoke-direct {p2, v0}, Ljava/io/BufferedReader;-><init>(Ljava/io/Reader;)V

    .line 225
    invoke-virtual {p2}, Ljava/io/BufferedReader;->readLine()Ljava/lang/String;

    move-result-object p2

    if-eqz p1, :cond_1

    .line 226
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_1

    return-object p1

    :cond_1
    if-eqz p2, :cond_2

    .line 228
    invoke-virtual {p2}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_2

    return-object p2

    .line 230
    :cond_2
    invoke-virtual {p0}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto/16 :goto_2

    :catch_0
    move-exception p0

    .line 233
    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    const-string p1, "HTTP"

    .line 234
    invoke-virtual {p0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p0

    invoke-static {p1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    goto/16 :goto_2

    .line 238
    :cond_3
    :try_start_1
    new-instance v0, Ljava/net/URL;

    invoke-direct {v0, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 239
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p0

    check-cast p0, Ljava/net/HttpURLConnection;

    const-string v0, "POST"

    .line 240
    invoke-virtual {p0, v0}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const-string v0, "Content-Type"

    const-string v2, "application/x-www-form-urlencoded;; UTF-8"

    .line 241
    invoke-virtual {p0, v0, v2}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "Accept"

    const-string v2, "application/json"

    .line 242
    invoke-virtual {p0, v0, v2}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 243
    invoke-virtual {p0, v1}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    .line 244
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const-string v1, "Cookie"

    .line 245
    invoke-virtual {p0, v1, v0}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_1

    :cond_4
    const-string p2, "apv"

    .line 247
    sget-object v0, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    invoke-virtual {p0, p2, v0}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 249
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object p2

    .line 250
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/OutputStreamWriter;

    const-string v2, "UTF-8"

    invoke-direct {v1, p2, v2}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;)V

    const-string v1, "UTF-8"

    .line 252
    invoke-static {p1, v1}, Lorg/apache/http/client/utils/URLEncodedUtils;->format(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/io/BufferedWriter;->write(Ljava/lang/String;)V

    .line 253
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->flush()V

    .line 254
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->close()V

    .line 255
    invoke-virtual {p2}, Ljava/io/OutputStream;->close()V

    const-string p1, "Location"

    .line 257
    invoke-virtual {p0, p1}, Ljava/net/HttpURLConnection;->getHeaderField(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 258
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object p2

    .line 259
    new-instance v0, Ljava/io/InputStreamReader;

    invoke-direct {v0, p2}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;)V

    .line 260
    new-instance p2, Ljava/io/BufferedReader;

    invoke-direct {p2, v0}, Ljava/io/BufferedReader;-><init>(Ljava/io/Reader;)V

    .line 261
    invoke-virtual {p2}, Ljava/io/BufferedReader;->readLine()Ljava/lang/String;

    move-result-object p2

    if-eqz p1, :cond_5

    .line 262
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_5

    return-object p1

    :cond_5
    if-eqz p2, :cond_6

    .line 264
    invoke-virtual {p2}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_6

    return-object p2

    .line 266
    :cond_6
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception p0

    .line 269
    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    const-string p1, "HTTP"

    .line 270
    invoke-virtual {p0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p0

    invoke-static {p1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_2
    const/4 p0, 0x0

    return-object p0
.end method

.method public static postMultipart(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/io/File;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)Ljava/lang/String;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/io/File;",
            "Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 302
    new-instance v7, Ljp/colopl/util/HttpMultipartRequest;

    invoke-static {}, Ljp/colopl/util/HTTP;->getUserAgent()Ljava/lang/String;

    move-result-object v5

    move-object v0, v7

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v6, p4

    invoke-direct/range {v0 .. v6}, Ljp/colopl/util/HttpMultipartRequest;-><init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/io/File;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)V

    .line 303
    sget p0, Ljp/colopl/util/HTTP;->CONNECT_TIMEOUT_FOR_UPLOAD:I

    invoke-virtual {v7, p0}, Ljp/colopl/util/HttpMultipartRequest;->setConnectionTimeout(I)V

    .line 304
    sget p0, Ljp/colopl/util/HTTP;->READ_TIMEOUT_FOR_UPLOAD:I

    invoke-virtual {v7, p0}, Ljp/colopl/util/HttpMultipartRequest;->setReadingTimeout(I)V

    .line 305
    invoke-virtual {v7}, Ljp/colopl/util/HttpMultipartRequest;->send()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static postMultipart(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)Ljava/lang/String;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/lang/String;",
            "Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 286
    new-instance v7, Ljp/colopl/util/HttpMultipartRequest;

    invoke-static {}, Ljp/colopl/util/HTTP;->getUserAgent()Ljava/lang/String;

    move-result-object v5

    move-object v0, v7

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v6, p4

    invoke-direct/range {v0 .. v6}, Ljp/colopl/util/HttpMultipartRequest;-><init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/lang/String;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)V

    .line 287
    sget p0, Ljp/colopl/util/HTTP;->CONNECT_TIMEOUT_FOR_UPLOAD:I

    invoke-virtual {v7, p0}, Ljp/colopl/util/HttpMultipartRequest;->setConnectionTimeout(I)V

    .line 288
    sget p0, Ljp/colopl/util/HTTP;->READ_TIMEOUT_FOR_UPLOAD:I

    invoke-virtual {v7, p0}, Ljp/colopl/util/HttpMultipartRequest;->setReadingTimeout(I)V

    .line 289
    invoke-virtual {v7}, Ljp/colopl/util/HttpMultipartRequest;->send()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static postXML(Ljava/lang/String;Ljava/lang/String;)Ljava/io/InputStream;
    .locals 1

    const/4 v0, 0x0

    .line 309
    invoke-static {p0, p1, v0}, Ljp/colopl/util/HTTP;->postXML(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)Ljava/io/InputStream;

    move-result-object p0

    return-object p0
.end method

.method public static postXML(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)Ljava/io/InputStream;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/io/InputStream;"
        }
    .end annotation

    .line 314
    new-instance v0, Lorg/apache/http/impl/client/DefaultHttpClient;

    invoke-direct {v0}, Lorg/apache/http/impl/client/DefaultHttpClient;-><init>()V

    .line 316
    invoke-interface {v0}, Lorg/apache/http/client/HttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v1

    const-string v2, "http.protocol.handle-redirects"

    const/4 v3, 0x1

    invoke-interface {v1, v2, v3}, Lorg/apache/http/params/HttpParams;->setBooleanParameter(Ljava/lang/String;Z)Lorg/apache/http/params/HttpParams;

    .line 317
    invoke-interface {v0}, Lorg/apache/http/client/HttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v1

    const-string v2, "http.protocol.expect-continue"

    const/4 v3, 0x0

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Lorg/apache/http/params/HttpParams;->setParameter(Ljava/lang/String;Ljava/lang/Object;)Lorg/apache/http/params/HttpParams;

    .line 318
    invoke-interface {v0}, Lorg/apache/http/client/HttpClient;->getParams()Lorg/apache/http/params/HttpParams;

    move-result-object v1

    .line 319
    sget v2, Ljp/colopl/util/HTTP;->CONNECT_TIMEOUT:I

    invoke-static {v1, v2}, Lorg/apache/http/params/HttpConnectionParams;->setConnectionTimeout(Lorg/apache/http/params/HttpParams;I)V

    .line 320
    sget v2, Ljp/colopl/util/HTTP;->READ_TIMEOUT:I

    invoke-static {v1, v2}, Lorg/apache/http/params/HttpConnectionParams;->setSoTimeout(Lorg/apache/http/params/HttpParams;I)V

    const-string v2, "Android"

    .line 322
    invoke-static {v1, v2}, Lorg/apache/http/params/HttpProtocolParams;->setUserAgent(Lorg/apache/http/params/HttpParams;Ljava/lang/String;)V

    .line 323
    new-instance v1, Lorg/apache/http/client/methods/HttpPost;

    invoke-direct {v1, p0}, Lorg/apache/http/client/methods/HttpPost;-><init>(Ljava/lang/String;)V

    if-eqz p2, :cond_0

    .line 325
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    const-string v2, "Cookie"

    .line 326
    invoke-virtual {v1, v2, p2}, Lorg/apache/http/client/methods/HttpPost;->addHeader(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 331
    :cond_0
    :try_start_0
    new-instance p0, Lorg/apache/http/entity/StringEntity;

    invoke-direct {p0, p1}, Lorg/apache/http/entity/StringEntity;-><init>(Ljava/lang/String;)V

    .line 332
    invoke-virtual {v1, p0}, Lorg/apache/http/client/methods/HttpPost;->setEntity(Lorg/apache/http/HttpEntity;)V

    const-string p0, "Content-Type"

    const-string p1, "application/xml; charset=UTF-8"

    .line 333
    invoke-virtual {v1, p0, p1}, Lorg/apache/http/client/methods/HttpPost;->setHeader(Ljava/lang/String;Ljava/lang/String;)V

    .line 334
    invoke-interface {v0, v1}, Lorg/apache/http/client/HttpClient;->execute(Lorg/apache/http/client/methods/HttpUriRequest;)Lorg/apache/http/HttpResponse;

    move-result-object p0

    if-eqz p0, :cond_1

    .line 336
    invoke-interface {p0}, Lorg/apache/http/HttpResponse;->getEntity()Lorg/apache/http/HttpEntity;

    move-result-object p0

    if-eqz p0, :cond_1

    .line 338
    invoke-interface {p0}, Lorg/apache/http/HttpEntity;->getContent()Ljava/io/InputStream;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    .line 342
    :catch_0
    invoke-virtual {v1}, Lorg/apache/http/client/methods/HttpPost;->abort()V

    :cond_1
    const/4 p0, 0x0

    return-object p0
.end method
