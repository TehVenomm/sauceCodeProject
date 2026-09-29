.class public Ljp/colopl/util/HttpMultipartRequest;
.super Ljava/lang/Object;
.source "HttpMultipartRequest.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;
    }
.end annotation


# static fields
.field private static final BOUNDARY:Ljava/lang/String; = "----------V2ymHFg03ehbqgZCaKO6jy"

.field private static final COUNT_UNIT_BYTES:I = 0x1000

.field private static final TAG:Ljava/lang/String; = "HttpMultipartRequest"


# instance fields
.field private connectionTimeout:I

.field private cookies:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private file:Ljava/io/File;

.field private mCallback:Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;

.field private postData:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;"
        }
    .end annotation
.end field

.field private readingTimeout:I

.field private url:Ljava/lang/String;

.field private userAgent:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/io/File;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)V
    .locals 1
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
            "Ljava/lang/String;",
            "Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;",
            ")V"
        }
    .end annotation

    .line 59
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const v0, 0xdbba0

    .line 32
    iput v0, p0, Ljp/colopl/util/HttpMultipartRequest;->connectionTimeout:I

    .line 33
    iput v0, p0, Ljp/colopl/util/HttpMultipartRequest;->readingTimeout:I

    if-eqz p1, :cond_0

    if-eqz p4, :cond_0

    .line 63
    iput-object p1, p0, Ljp/colopl/util/HttpMultipartRequest;->url:Ljava/lang/String;

    .line 64
    iput-object p2, p0, Ljp/colopl/util/HttpMultipartRequest;->postData:Ljava/util/List;

    .line 65
    iput-object p3, p0, Ljp/colopl/util/HttpMultipartRequest;->cookies:Ljava/util/List;

    .line 66
    iput-object p4, p0, Ljp/colopl/util/HttpMultipartRequest;->file:Ljava/io/File;

    .line 67
    iput-object p5, p0, Ljp/colopl/util/HttpMultipartRequest;->userAgent:Ljava/lang/String;

    .line 68
    iput-object p6, p0, Ljp/colopl/util/HttpMultipartRequest;->mCallback:Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;

    return-void

    .line 61
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    invoke-direct {p1}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p1
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/lang/String;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)V
    .locals 7
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
            "Ljava/lang/String;",
            "Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;",
            ")V"
        }
    .end annotation

    .line 46
    new-instance v4, Ljava/io/File;

    invoke-direct {v4, p4}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v5, p5

    move-object v6, p6

    invoke-direct/range {v0 .. v6}, Ljp/colopl/util/HttpMultipartRequest;-><init>(Ljava/lang/String;Ljava/util/List;Ljava/util/List;Ljava/io/File;Ljava/lang/String;Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;)V

    return-void
.end method

.method private getBoundaryMessage(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 88
    new-instance v0, Ljava/lang/StringBuffer;

    const-string v1, "--"

    invoke-direct {v0, v1}, Ljava/lang/StringBuffer;-><init>(Ljava/lang/String;)V

    const-string v1, "----------V2ymHFg03ehbqgZCaKO6jy"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "\r\n"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    if-eqz p1, :cond_0

    .line 91
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/apache/http/NameValuePair;

    const-string v2, "Content-Disposition: form-data; name=\""

    .line 92
    invoke-virtual {v0, v2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    invoke-interface {v1}, Lorg/apache/http/NameValuePair;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v2, "\"\r\n"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v2, "\r\n"

    .line 93
    invoke-virtual {v0, v2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    invoke-interface {v1}, Lorg/apache/http/NameValuePair;->getValue()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "\r\n"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "--"

    .line 94
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "----------V2ymHFg03ehbqgZCaKO6jy"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "\r\n"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    goto :goto_0

    :cond_0
    const-string p1, "\\."

    .line 97
    invoke-virtual {p2, p1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 98
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "image/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    array-length v2, p1

    add-int/lit8 v2, v2, -0x1

    aget-object p1, p1, v2

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    const-string v1, "Content-Disposition: form-data; name=\"file\""

    .line 99
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "; filename=\""

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string p2, "\"\r\n"

    invoke-virtual {v0, p2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string p2, "Content-Type: "

    .line 100
    invoke-virtual {v0, p2}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string p1, "\r\n\r\n"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 102
    invoke-virtual {v0}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private getImageBytes(Ljava/io/File;)[B
    .locals 4

    const/16 v0, 0xa

    .line 106
    new-array v0, v0, [B

    .line 108
    new-instance v1, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v1}, Ljava/io/ByteArrayOutputStream;-><init>()V

    const/4 v2, 0x0

    .line 110
    :try_start_0
    new-instance v3, Ljava/io/FileInputStream;

    invoke-direct {v3, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 111
    :goto_0
    :try_start_1
    invoke-virtual {v3, v0}, Ljava/io/FileInputStream;->read([B)I

    move-result p1

    if-lez p1, :cond_0

    .line 112
    invoke-virtual {v1, v0}, Ljava/io/ByteArrayOutputStream;->write([B)V
    :try_end_1
    .catch Ljava/io/FileNotFoundException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 119
    :cond_0
    :try_start_2
    invoke-virtual {v1}, Ljava/io/ByteArrayOutputStream;->close()V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0

    .line 120
    :catch_0
    :try_start_3
    invoke-virtual {v3}, Ljava/io/FileInputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_7

    goto :goto_4

    :catchall_0
    move-exception p1

    move-object v2, v3

    goto :goto_5

    :catch_1
    move-exception p1

    move-object v2, v3

    goto :goto_1

    :catch_2
    move-exception p1

    move-object v2, v3

    goto :goto_3

    :catchall_1
    move-exception p1

    goto :goto_5

    :catch_3
    move-exception p1

    .line 117
    :goto_1
    :try_start_4
    invoke-virtual {p1}, Ljava/io/IOException;->printStackTrace()V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    .line 119
    :try_start_5
    invoke-virtual {v1}, Ljava/io/ByteArrayOutputStream;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_4

    :catch_4
    if-eqz v2, :cond_1

    .line 120
    :goto_2
    :try_start_6
    invoke-virtual {v2}, Ljava/io/FileInputStream;->close()V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_7

    goto :goto_4

    :catch_5
    move-exception p1

    .line 115
    :goto_3
    :try_start_7
    invoke-virtual {p1}, Ljava/io/FileNotFoundException;->printStackTrace()V
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_1

    .line 119
    :try_start_8
    invoke-virtual {v1}, Ljava/io/ByteArrayOutputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_6

    :catch_6
    if-eqz v2, :cond_1

    goto :goto_2

    .line 122
    :catch_7
    :cond_1
    :goto_4
    invoke-virtual {v1}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object p1

    return-object p1

    .line 119
    :goto_5
    :try_start_9
    invoke-virtual {v1}, Ljava/io/ByteArrayOutputStream;->close()V
    :try_end_9
    .catch Ljava/io/IOException; {:try_start_9 .. :try_end_9} :catch_8

    :catch_8
    if-eqz v2, :cond_2

    .line 120
    :try_start_a
    invoke-virtual {v2}, Ljava/io/FileInputStream;->close()V
    :try_end_a
    .catch Ljava/io/IOException; {:try_start_a .. :try_end_a} :catch_9

    :catch_9
    :cond_2
    throw p1
.end method


# virtual methods
.method public send()Ljava/lang/String;
    .locals 8

    const/4 v0, 0x0

    .line 136
    :try_start_0
    new-instance v1, Ljava/net/URL;

    iget-object v2, p0, Ljp/colopl/util/HttpMultipartRequest;->url:Ljava/lang/String;

    invoke-direct {v1, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    const-string v2, "Content-Type"

    const-string v3, "multipart/form-data; boundary=----------V2ymHFg03ehbqgZCaKO6jy"

    .line 138
    invoke-virtual {v1, v2, v3}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "User-Agent"

    .line 140
    iget-object v3, p0, Ljp/colopl/util/HttpMultipartRequest;->userAgent:Ljava/lang/String;

    invoke-virtual {v1, v2, v3}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 142
    iget v2, p0, Ljp/colopl/util/HttpMultipartRequest;->connectionTimeout:I

    invoke-virtual {v1, v2}, Ljava/net/URLConnection;->setConnectTimeout(I)V

    .line 144
    iget v2, p0, Ljp/colopl/util/HttpMultipartRequest;->readingTimeout:I

    invoke-virtual {v1, v2}, Ljava/net/URLConnection;->setReadTimeout(I)V

    .line 146
    move-object v2, v1

    check-cast v2, Ljava/net/HttpURLConnection;

    const-string v3, "POST"

    invoke-virtual {v2, v3}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const/4 v2, 0x1

    .line 148
    invoke-virtual {v1, v2}, Ljava/net/URLConnection;->setDoOutput(Z)V

    .line 150
    iget-object v2, p0, Ljp/colopl/util/HttpMultipartRequest;->cookies:Ljava/util/List;

    if-eqz v2, :cond_1

    .line 151
    new-instance v2, Ljava/lang/StringBuffer;

    invoke-direct {v2}, Ljava/lang/StringBuffer;-><init>()V

    .line 152
    iget-object v3, p0, Ljp/colopl/util/HttpMultipartRequest;->cookies:Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 153
    invoke-virtual {v2, v4}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v4, "; "

    invoke-virtual {v2, v4}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    goto :goto_0

    :cond_0
    const-string v3, "Cookie"

    .line 155
    invoke-virtual {v2}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v3, v2}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 158
    :cond_1
    invoke-virtual {v1}, Ljava/net/URLConnection;->connect()V

    .line 161
    invoke-virtual {v1}, Ljava/net/URLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v2

    .line 162
    iget-object v3, p0, Ljp/colopl/util/HttpMultipartRequest;->postData:Ljava/util/List;

    iget-object v4, p0, Ljp/colopl/util/HttpMultipartRequest;->file:Ljava/io/File;

    invoke-virtual {v4}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-direct {p0, v3, v4}, Ljp/colopl/util/HttpMultipartRequest;->getBoundaryMessage(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/String;->getBytes()[B

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/io/OutputStream;->write([B)V

    .line 163
    iget-object v3, p0, Ljp/colopl/util/HttpMultipartRequest;->file:Ljava/io/File;

    invoke-direct {p0, v3}, Ljp/colopl/util/HttpMultipartRequest;->getImageBytes(Ljava/io/File;)[B

    move-result-object v3

    .line 164
    array-length v4, v3

    const/4 v5, 0x0

    :cond_2
    :goto_1
    if-ge v5, v4, :cond_4

    add-int/lit16 v6, v5, 0x1000

    if-le v6, v4, :cond_3

    sub-int v6, v4, v5

    goto :goto_2

    :cond_3
    const/16 v6, 0x1000

    .line 170
    :goto_2
    invoke-virtual {v2, v3, v5, v6}, Ljava/io/OutputStream;->write([BII)V

    add-int/2addr v5, v6

    .line 172
    iget-object v6, p0, Ljp/colopl/util/HttpMultipartRequest;->mCallback:Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;

    if-eqz v6, :cond_2

    .line 174
    iget-object v6, p0, Ljp/colopl/util/HttpMultipartRequest;->mCallback:Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;

    invoke-interface {v6, v5, v4}, Ljp/colopl/util/HttpMultipartRequest$ProgressCallback;->progress(II)V

    goto :goto_1

    :cond_4
    const-string v3, "\r\n------------V2ymHFg03ehbqgZCaKO6jy--\r\n"

    .line 178
    invoke-virtual {v3}, Ljava/lang/String;->getBytes()[B

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/io/OutputStream;->write([B)V

    .line 179
    invoke-virtual {v2}, Ljava/io/OutputStream;->close()V

    .line 181
    move-object v2, v1

    check-cast v2, Ljava/net/HttpURLConnection;

    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v2

    const/16 v3, 0xc8

    if-ne v2, v3, :cond_5

    .line 184
    new-instance v2, Ljp/colopl/util/DoneHandlerInputStream;

    invoke-virtual {v1}, Ljava/net/URLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3

    invoke-direct {v2, v3}, Ljp/colopl/util/DoneHandlerInputStream;-><init>(Ljava/io/InputStream;)V

    .line 185
    invoke-static {v2}, Ljp/colopl/util/StringUtil;->convertToString(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v2
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    move-object v0, v2

    :cond_5
    if-eqz v1, :cond_6

    .line 191
    :goto_3
    check-cast v1, Ljava/net/HttpURLConnection;

    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    goto :goto_5

    :catch_0
    move-exception v2

    goto :goto_4

    :catchall_0
    move-exception v1

    move-object v7, v1

    move-object v1, v0

    move-object v0, v7

    goto :goto_6

    :catch_1
    move-exception v2

    move-object v1, v0

    :goto_4
    :try_start_2
    const-string v3, ""

    .line 188
    invoke-virtual {v2}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-static {v3, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v1, :cond_6

    goto :goto_3

    :cond_6
    :goto_5
    return-object v0

    :catchall_1
    move-exception v0

    :goto_6
    if-eqz v1, :cond_7

    .line 191
    check-cast v1, Ljava/net/HttpURLConnection;

    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_7
    throw v0
.end method

.method public setConnectionTimeout(I)V
    .locals 0

    .line 76
    iput p1, p0, Ljp/colopl/util/HttpMultipartRequest;->connectionTimeout:I

    return-void
.end method

.method public setReadingTimeout(I)V
    .locals 0

    .line 84
    iput p1, p0, Ljp/colopl/util/HttpMultipartRequest;->readingTimeout:I

    return-void
.end method
